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
    /// Interaction logic for SportC.xaml
    /// </summary>
    public partial class SportC : UserControl
    {
        private MyViewModel vm;

        public SportC(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.DataContext = vm;
            initializeCityBox();
        }

        private void initializeCityBox()
        {
            List<string> cities = Data.citiesList;
            cityBox.ItemsSource = cities;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cityBox.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a city");
                return;
            }
            vm.filterByLocation(cityBox.SelectedItem.ToString(), "sport");
            activityBox.ItemsSource = vm.FilteredSports;
            resultsLbl.Visibility = Visibility.Visible;
            activityBox.Visibility = Visibility.Visible;
            btn.IsEnabled = false;
            btn.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (activityBox.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select the activity");
                return;
            }
            SportActivity spAct = (SportActivity)activityBox.SelectedItem;
            SportDisplayC sportDisplay = new SportDisplayC(spAct, vm);
            sportDisplay.Show();
        }

        private void activityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (activityBox.SelectedIndex != -1)
            {
                btn.IsEnabled = true;
            }
            else
            {
                btn.IsEnabled = false;
            }
        }
    }
}