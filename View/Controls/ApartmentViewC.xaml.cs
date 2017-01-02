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
    /// Interaction logic for ApartmentViewC.xaml
    /// </summary>
    public partial class ApartmentViewC : UserControl
    {
        public ApartmentViewC()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ApartmentPreviewProperty =
            DependencyProperty.Register("Apartment", typeof(ApartmentActivity), typeof(ApartmentViewC));

        public ApartmentActivity Apartment
        {
            get { return (ApartmentActivity)GetValue(ApartmentPreviewProperty); }
            set { SetValue(ApartmentPreviewProperty, value); }
        }

    }
}
