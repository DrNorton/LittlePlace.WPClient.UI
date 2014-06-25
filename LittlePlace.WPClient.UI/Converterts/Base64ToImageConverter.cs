using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace LittlePlace.WPClient.UI.Converterts
{
    public class Base64ToImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var str = (string) value;
                var bytes = System.Convert.FromBase64String(str);
                return ByteArrayToImage(bytes);
            }
            return null;
        }

        public static BitmapSource ByteArrayToImage(byte[] bytes)
        {
            BitmapImage bitmapImage = null;
            try
            {

                if (bytes != null)
                {
                    using (MemoryStream stream = new MemoryStream(bytes, 0, bytes.Length))
                    {
                        bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(stream);
                    }
                }
            }
            catch
            {

            }
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
