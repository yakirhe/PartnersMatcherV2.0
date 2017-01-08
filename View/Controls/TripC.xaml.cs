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
    /// Interaction logic for TripC.xaml
    /// </summary>
    public partial class TripC : UserControl
    {
        MyViewModel vm;

        public TripC(MyViewModel vm)
        {
            InitializeComponent();
            initializeRegionBox();
            this.vm = vm;
            this.DataContext = vm;
        }

        private void initializeRegionBox()
        {
            List<string> regions = new List<string> { "Middle east", "Scandinavia", "North America", "South America", "Far East", "Caribbean", "Africa", "Australia" };
            regionBox.ItemsSource = regions;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (regionBox.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a region");
                return;
            }
            vm.filterByLocation(regionBox.SelectedItem.ToString(), "trip");
            activityBox.ItemsSource = vm.FilteredTrips;
            resultsLbl.Visibility = Visibility.Visible;
            activityBox.Visibility = Visibility.Visible;
            btn.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (activityBox.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select the activity");
                return;
            }
            MessageBox.Show("You were added to the pending list");
        }
    }
}
