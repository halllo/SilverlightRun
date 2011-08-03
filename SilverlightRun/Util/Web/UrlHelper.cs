using System;
using System.Net;

namespace SilverlightRun.Util.Web
{
    /// <summary>
    /// Basic url action helper.
    /// </summary>
    public class UrlHelper
    {
        string _url;
        public UrlHelper(string url)
        {
            _url = url;
        }

        public void Navigate(CookieContainer cookieJar, Action<string> result)
        {
            HttpWebRequest request = Request(cookieJar);
            request.GetResponseContent(result);
        }

        public HttpWebRequest Request(CookieContainer cookieJar)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(_url);
            request.CookieContainer = cookieJar;
            return request;
        }

        public HttpWebRequest FormSubmit(CookieContainer cookieJar)
        {
            var request = Request(cookieJar);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            return request;
        }
    }
}
