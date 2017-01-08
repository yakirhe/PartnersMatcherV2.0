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
    /// Interaction logic for SportViewC.xaml
    /// </summary>
    public partial class SportViewC : UserControl
    {
        public SportViewC()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SportPreviewProperty =
            DependencyProperty.Register("Sport", typeof(SportActivity), typeof(SportViewC));

        public SportActivity Sport
        {
            get { return (SportActivity)GetValue(SportPreviewProperty); }
            set { SetValue(SportPreviewProperty, value); }
        }
    }
}
