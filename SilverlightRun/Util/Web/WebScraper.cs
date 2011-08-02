using System;
using System.Collections.Generic;
using System.Net;

namespace SilverlightRun.Util.Web
{
    /// <summary>
    /// Allows basic html parsing.
    /// </summary>
    /// <typeparam name="T">Any type that can be returned by this parser.</typeparam>
    public abstract class WebScraper<T>
    {
        WebClient _webC;
        Action<IEnumerable<T>> _returner;
        IPhoneService _phone;
        string _lastUrl;

        public WebScraper(IPhoneService phone)
        {
            _phone = phone;
            _returner = (l) => { };
            _webC = new WebClient();
            _webC.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webC_DownloadStringCompleted);
        }

        public void Download(Action<IEnumerable<T>> returns)
        {
            _returner = returns;
            _lastUrl = this.ProvideUrl();
            ProvideCachedOrDownload(this._lastUrl);
        }

        private void ProvideCachedOrDownload(string url)
        {
            if (_phone != null && _phone.HasStored("webscraper_" + url))
                OnDownloadSuccessful(_phone.GetStored("webscraper_" + url).ToString());
            else
                DownloadNeeded(url);
        }

        private void DownloadNeeded(string url)
        {
            _webC.DownloadStringAsync(new Uri(url));
        }

        private void webC_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                OnDownloadSuccessful(e.Result);
                CacheDownload(e.Result);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                this.OnDownloadError(ex.Message);
            }
        }

        private void CacheDownload(string content)
        {
            if (_phone != null) _phone.Store("webscraper_" + _lastUrl, content);
        }

        private void OnDownloadSuccessful(string content)
        {
            try
            {
                this._returner(this.ParseRaw(content));
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("A parsing error occurred!\nPlease contact the developer at twitter.com/halllo!");
            }
        }

        private void OnDownloadError(string error)
        {
            if (this.DownloadError != null)
                this.DownloadError(error);
        }

        public event Action<string> DownloadError;

        protected abstract string ProvideUrl();
        protected abstract IEnumerable<T> ParseRaw(string rawContent);
    }
}
