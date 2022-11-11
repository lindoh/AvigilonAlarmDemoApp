using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AvigilonAlarmDemoApp.UI.Converters
{
    public class CameraConverter : IValueConverter
    {

        /// <summary>
        /// Converting Camera names to comma seperated value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> cameraModel = (List<string>)value;
            string Cameras = (string.Join(",", cameraModel.Select(x => x.ToString()).ToArray()));
            return Cameras;
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
