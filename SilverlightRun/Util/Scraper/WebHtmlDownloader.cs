using System.Collections.Generic;

namespace SilverlightRun.Util.Scraper
{
    /// <summary>
    /// Allows downloading of a simple web page.
    /// </summary>
    public class WebHtmlDownloader : WebScraper<string>
    {
        string _url;
        private WebHtmlDownloader(string url) : base(null)
        {
            _url = url;
        }

        public static WebHtmlDownloader Url(string url)
        {
            return new WebHtmlDownloader(url);
        }

        protected override string ProvideUrl()
        {
            return _url;
        }

        protected override IEnumerable<string> ParseRaw(string rawContent)
        {
            yield return rawContent;
        }
    }
}
