using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace client
{
    public class FilePathToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (!string.IsNullOrEmpty(path))
            {
                return new BitmapImage(new Uri(path));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}