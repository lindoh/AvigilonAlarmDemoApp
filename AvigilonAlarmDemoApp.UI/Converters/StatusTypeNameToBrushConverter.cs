using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AvigilonAlarmDemoApp.UI.Converters
{
    /// <summary>
    /// Converter to set colours for each status
    /// </summary>
    public class StatusTypeNameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var statusTypeName = (string)value;            
            switch (statusTypeName.ToLower())
            {
                case "active":
                    return new SolidColorBrush(Colors.LemonChiffon);
                case "purged":
                    return new SolidColorBrush(Colors.Gray);
                case "acknowledged":
                    return new SolidColorBrush(Colors.LightGreen);
                case "assigned":
                    return new SolidColorBrush(Colors.LightSteelBlue);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
