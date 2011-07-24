using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace about
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            string url = (sender as HyperlinkButton).Content.ToString();
            var browser = new Microsoft.Phone.Tasks.WebBrowserTask();
            browser.URL = "http://" + url;
            browser.Show();
        }
    }
}