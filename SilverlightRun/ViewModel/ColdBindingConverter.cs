using System;
using System.Windows.Data;

namespace SilverlightRun.ViewModel
{
    /// <summary>
    /// Allows to create binding converters more easily.
    /// </summary>
    /// <typeparam name="S">The source type.</typeparam>
    /// <typeparam name="T">The target type.</typeparam>
    public class ColdBindingConverter<S, T> : IValueConverter
    {
        Func<S, T> _convert;

        public ColdBindingConverter(Func<S, T> convert)
        {
            _convert = convert;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return _convert((S)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
