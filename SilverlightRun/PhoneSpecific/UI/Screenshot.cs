using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Allows creation, storage and reload of screenshots of arbitrary UIElements.
    /// </summary>
    public class Screenshot
    {
        WriteableBitmap _wbitmap;

        private Screenshot(WriteableBitmap wbitmap)
        {
            _wbitmap = wbitmap;
        }

        public static Screenshot Of(UIElement uie)
        {
            var wb = new WriteableBitmap(uie, null);
            return new Screenshot(wb);
        }

        public static Screenshot Load(string imgName)
        {
            object img;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("img_" + imgName) &&
                null != (img = IsolatedStorageSettings.ApplicationSettings["img_" + imgName]))
            {
                return new Screenshot(BitmapHelper.Of(img.ToString()));
            }
            else return null;
        }

        public ImageSource Source { get { return _wbitmap; } }
        public Image Image { get { return new Image() { Source = this.Source }; } }

        public void Save(string imgName)
        {
            string imgKey = "img_" + imgName;
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(imgKey))
                IsolatedStorageSettings.ApplicationSettings.Add(imgKey, BitmapHelper.Of(_wbitmap));
            else
                IsolatedStorageSettings.ApplicationSettings[imgKey] = BitmapHelper.Of(_wbitmap);
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
