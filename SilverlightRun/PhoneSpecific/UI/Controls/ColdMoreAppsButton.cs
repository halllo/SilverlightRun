using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Tasks;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Starts a marketplace app search on the registered publisher.
    /// </summary>
    public class ColdMoreAppsButton : Button
    {
        string _Publisher;
        public string Publisher
        {
            set
            {
                _Publisher = value;
                Content = "more apps from " + value;
            }
            get
            {
                return _Publisher;
            }
        }

        public ColdMoreAppsButton()
            : base()
        {
            Padding = new Thickness(20, 10, 20, 10);
            Click += (s, e) => SearchMarketplace(_Publisher);
        }

        private void SearchMarketplace(string keyword)
        {
            var search = new MarketplaceSearchTask();
            search.ContentType = MarketplaceContentType.Applications;
            search.SearchTerms = keyword;
            search.Show();
        }
    }
}
