using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace LittlePlace.WPClient.UI.Extensions
{
    public class Helpers
    {
        public static byte[] ImageToBytes(BitmapImage img)
        {
            MemoryStream ms = new MemoryStream();
            WriteableBitmap wb = new WriteableBitmap(img);
            wb.SaveJpeg(ms, img.PixelWidth, img.PixelHeight, 0, 100);
            byte[] imageBytes = ms.ToArray();
            return imageBytes;
        }

        public static BitmapImage BytesToImage(byte[] bytes)
        {
            BitmapImage bitmapImage = new BitmapImage();
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    bitmapImage.SetSource(ms);
                    return bitmapImage;
                }
            }
            finally { bitmapImage = null; }
        }
    }
}
