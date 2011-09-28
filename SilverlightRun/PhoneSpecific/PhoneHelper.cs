using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;

namespace SilverlightRun.PhoneSpecific
{
    /// <summary>
    /// Basic phone functionality like thread dispatching and page naviagation.
    /// Best staticly initialized in App.xaml.cs to be accessible everywhere.
    /// </summary>
    public class PhoneHelper
    {
        Func<PhoneApplicationFrame> _rootFrame;
        public PhoneHelper(Func<PhoneApplicationFrame> rootFrame)
        {
            this._rootFrame = rootFrame;
        }

        public void NavigateTo(string page)
        {
            this._rootFrame().Navigate(new Uri(page, UriKind.Relative));
        }

        public void NavigateBack()
        {
            this._rootFrame().GoBack();
        }

        public ApplicationBarIconButton AppBarButton(PhoneApplicationPage page, int p)
        {
            return page.ApplicationBar.Buttons[p] as ApplicationBarIconButton;
        }

        public ApplicationBarMenuItem AppBarMenu(PhoneApplicationPage page, int p)
        {
            return page.ApplicationBar.MenuItems[p] as ApplicationBarMenuItem;
        }

        public bool HasInternet
        {
            get { return NetworkInterface.GetIsNetworkAvailable(); }
        }

        public void IfNotNull<T>(T obj, Action<T> doAction)
        {
            if (obj != null)
                doAction(obj);
        }

        public R IfNotNull<T, R>(T obj, Func<T, R> doAction)
        {
            if (obj != null)
                return doAction(obj);
            else
                return default(R);
        }

        public void Dispatch(Action toDispatch)
        {
            this._rootFrame().Dispatcher.BeginInvoke(() => toDispatch());
        }

        public bool InDesignMode { get { return DesignerProperties.IsInDesignTool; } }

        public SolidColorBrush AccentBrush
        {
            get { return Resource("PhoneAccentBrush") as SolidColorBrush; }
        }

        public SolidColorBrush ForegroundBrush
        {
            get { return Resource("PhoneForegroundBrush") as SolidColorBrush; }
        }

        public SolidColorBrush BackgroundBrush
        {
            get { return Resource("PhoneBackgroundBrush") as SolidColorBrush; }
        }

        public object Resource(string resource)
        {
            return Application.Current.Resources[resource];
        }

        public T Tag<T>(object sender) where T : class
        {
            return (sender as FrameworkElement).Tag as T;
        }

        public T DataContext<T>(object sender) where T : class
        {
            return (sender as FrameworkElement).DataContext as T;
        }

        public string ResourceFile(string path)
        {
            var resourceStream = ResourceFileStream(path);
            using (var contentStream = new StreamReader(resourceStream)) return contentStream.ReadToEnd();
        }

        public Stream ResourceFileStream(string path)
        {
            var appName = Application.Current.ToString().Replace(".App", "");
            var fullPath = string.Format("/{0};component/{1}", appName, path);
            var resourceStream = Application.GetResourceStream(new Uri(fullPath, UriKind.Relative));
            return resourceStream.Stream;
        }
    }
}
