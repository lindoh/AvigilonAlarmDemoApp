using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AvigilonAlarmDemoApp.UI.View
{
    /// <summary>
    /// Interaction logic for SuccessWindow.xaml
    /// </summary>
    public partial class SuccessWindow : Window
    {
        public SuccessWindow()
        {
            InitializeComponent();
            System.Windows.Media.ImageSource imageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
               Properties.Resources.App.Handle,
               System.Windows.Int32Rect.Empty,
               System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            Title = "Avigilon Alarm Demo Application";
            Icon = imageSource;
        }
    }
}
