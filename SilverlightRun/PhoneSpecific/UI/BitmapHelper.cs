using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Allows serialization and deserialization of Bitmaps and strings.
    /// </summary>
    public class BitmapHelper : BitmapSource
    {
        public static WriteableBitmap Of(string img)
        {
            return StringToImage(img);
        }

        public static string Of(WriteableBitmap img)
        {
            return Convert.ToBase64String(BytesOf(img));
        }

        public static byte[] BytesOf(WriteableBitmap img)
        {
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                img.SaveJpeg(stream, img.PixelWidth, img.PixelHeight, 0, 100);
                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        private static WriteableBitmap StringToImage(string img)
        {
            byte[] array = Convert.FromBase64String(img);
            return OfStream(new MemoryStream(array));
        }

        private static WriteableBitmap OfStream(Stream stream)
        {
            var sb = new BitmapHelper();
            sb.SetSource(stream);
            return new WriteableBitmap(sb);
        }
    }
}
