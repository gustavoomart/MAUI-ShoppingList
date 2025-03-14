using System.Globalization;

namespace Compras.Converters
{
    public class DescriptionFormatterConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var description = value?.ToString();

            if (string.IsNullOrWhiteSpace(description))
                return string.Empty;

            return $"({description})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}
