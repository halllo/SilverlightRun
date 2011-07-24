using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace SilverlightRun.PhoneSpecific
{
    /// <summary>
    /// Baisc phone functionality.
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

        public SolidColorBrush AccentBrush
        {
            get { return (SolidColorBrush)Application.Current.Resources["PhoneAccentBrush"];}
        }
    }
}
