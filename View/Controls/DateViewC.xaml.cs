using PartnersMatcher.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PartnersMatcher.View.Controls
{
    /// <summary>
    /// Interaction logic for DateViewC.xaml
    /// </summary>
    public partial class DateViewC : UserControl
    {
        public DateViewC()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DatePreviewProperty =
            DependencyProperty.Register("Date", typeof(DateActivity), typeof(DateViewC));

        public DateActivity Date
        {
            get { return (DateActivity)GetValue(DatePreviewProperty); }
            set { SetValue(DatePreviewProperty, value); }
        }
    }
}
