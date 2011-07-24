﻿using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace SilverlightRun.PhoneSpecific.UI
{
    public class PurchaseButton : Button
    {
        public PurchaseButton()
            : base()
        {
            this.Content = "Purchase";
            this.Visibility = License.IsTrial ? Visibility.Visible : Visibility.Collapsed;
            this.Click += (s, e) => License.Purchase();
        }
    }
    public class License
    {
        public static bool IsTrial
        {
            get
            {
                var licenseInformation = new LicenseInformation();
                return licenseInformation.IsTrial();
            }
        }

        public static void Purchase()
        {
            var detailTask = new MarketplaceDetailTask();
            detailTask.Show();
        }

        public static void DisableAppBarMenuWhenTrial(PhoneApplicationPage page, int index)
        {
            var menu = page.ApplicationBar.MenuItems[index] as ApplicationBarMenuItem;
            menu.IsEnabled = !IsTrial;
        }

        public static void DisableAppBarButtonWhenTrial(PhoneApplicationPage page, int index)
        {
            var button = page.ApplicationBar.Buttons[index] as ApplicationBarIconButton;
            button.IsEnabled = !IsTrial;
        }
    }
}
