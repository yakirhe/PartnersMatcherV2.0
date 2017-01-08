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
    /// Interaction logic for DateC.xaml
    /// </summary>
    public partial class DateC : UserControl
    {
        MyViewModel vm;

        public DateC(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.DataContext = DataContext;
            initializeCityBox();
        }

        private void initializeCityBox()
        {
            List<string> cities = new List<string> { "Tel Aviv", "Jerusalem", "Haifa", "Beer Sheva" };
            cityBox.ItemsSource = cities;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cityBox.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a city");
                return;
            }
            vm.filterByLocation(cityBox.SelectedItem.ToString(), "date");
            activityBox.ItemsSource = vm.FilteredDates;
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
