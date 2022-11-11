using System;
using System.Globalization;
using System.Windows.Data;

namespace AvigilonAlarmDemoApp.UI.Converters
{
    public class DateConverter : IValueConverter
    {

        /// <summary>
        /// Converting date to a proper format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dates = value.ToString();

            var timeOfMostRecentActivation = DateTime.ParseExact(dates,AvigilonAlarmDemoAppResource.DateTimeOfActivation,
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            return timeOfMostRecentActivation.ToLocalTime();
        }

        /// <summary>
        /// Implementation of interface
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }   
}