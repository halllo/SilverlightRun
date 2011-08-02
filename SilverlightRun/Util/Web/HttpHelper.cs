using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace SilverlightRun.Util.Web
{
    /// <summary>
    /// Basic http GET and POST request and response helper.
    /// </summary>
    public static class HttpHelper
    {
        public static void Navigate(this string url, CookieContainer cookieJar, Action<string> result)
        {
            HttpWebRequest request = HttpHelper.Request(url, cookieJar);
            request.GetResponseContent(result);
        }

        public static HttpWebRequest Request(this string url, CookieContainer cookieJar)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = cookieJar;
            return request;
        }

        public static HttpWebRequest FormSubmit(this string url, CookieContainer cookieJar)
        {
            var request = Request(url, cookieJar);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            return request;
        }

        public static void GetResponseContent(this HttpWebRequest request, string requestContent, Action<string> result)
        {
            byte[] rawContent = requestContent.ToAscii();
            request.BeginGetRequestStream((iar) =>
            {
                using (Stream requestStrm = request.EndGetRequestStream(iar))
                {
                    requestStrm.Write(rawContent, 0, rawContent.Length);
                }
                GetResponseContent(request, result);
            }, request);
        }

        public static void GetResponseContent(this HttpWebRequest request, Action<string> result)
        {
            request.BeginGetResponse((iar) =>
            {
                WebResponse response = request.EndGetResponse(iar);
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    result(sr.ReadToEnd());
                }
            }, request);
        }

        public static byte[] ToAscii(this string s)
        {
            byte[] retval = new byte[s.Length];
            for (int ix = 0; ix < s.Length; ++ix)
            {
                char ch = s[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }

        public static string OnlyValue(this Match htmlelement)
        {
            Match match = Regex.Match(htmlelement.Value, "<.*?>((.|\n)*?)<.*?>");
            return match.Groups[1].Value.Trim();
        }

        public static string GetLinkOfLabel(this string source, string label)
        {
            MatchCollection links = Regex.Matches(source, "<a.*?>.*?</a>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (Match link in links)
            {
                MatchCollection targets = Regex.Matches(link.Value, "<a.*?href=\"(.*?)\".*?>.*?" + label + ".*?</a>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                foreach (Match target in targets) return target.Groups[1].Value.Replace("&amp;", "&");
            }
            return "";
        }
    }
}
