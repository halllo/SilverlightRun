using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;

namespace SilverlightRun.Util.Web
{
    /// <summary>
    /// Basic http GET and POST request and response helper.
    /// </summary>
    public static class WebHelper
    {
        public static UrlHelper Url(this string url)
        {
            return new UrlHelper(url);
        }

        public static void GetResponseContent(this HttpWebRequest request, string requestContent, Action<string> result)
        {
            byte[] rawContent = requestContent.ToAscii();
            request.GetResponseContent(rawContent, result);
        }

        public static void GetResponseContent(this HttpWebRequest request, byte[] requestContent, Action<string> result)
        {
            request.BeginGetRequestStream((iar) =>
            {
                using (Stream requestStrm = request.EndGetRequestStream(iar))
                {
                    requestStrm.Write(requestContent, 0, requestContent.Length);
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

        public static string OnlyValue(this string element)
        {
            Match match = Regex.Match(element, "<.*?>((.|\n)*?)<.*?>");
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

        public static string Base64ize(this string org)
        {
            return Convert.ToBase64String(
                Encoding.UTF8.GetBytes(org));
        }

        public static string UnBase64ize(this string b64)
        {
            byte[] orgBytes = Convert.FromBase64String(b64);
            return Encoding.UTF8.GetString(orgBytes, 0, orgBytes.Length);
        }
    }
}
