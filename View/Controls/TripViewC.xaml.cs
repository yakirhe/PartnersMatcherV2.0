using PartnersMatcher.Model;
using PartnersMatcher.ViewModel;
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
    public partial class TripViewC : UserControl
    {
        public TripViewC()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TripPreviewProperty =
            DependencyProperty.Register("Trip", typeof(TripActivity), typeof(TripViewC));

        public TripActivity Trip
        {
            get { return (TripActivity)GetValue(TripPreviewProperty); }
            set { SetValue(TripPreviewProperty, value); }
        }

    }
}
