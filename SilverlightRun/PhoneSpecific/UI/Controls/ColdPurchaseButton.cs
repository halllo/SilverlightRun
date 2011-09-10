using System.Windows;
using System.Windows.Controls;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Click takes you to the marketplace.
    /// </summary>
    public class ColdPurchaseButton : Button
    {
        public ColdPurchaseButton()
            : base()
        {
            Content = "Purchase";
            Visibility = License.IsTrial ? Visibility.Visible : Visibility.Collapsed;
            Click += (s, e) => License.Purchase();
        }
    }
}
