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
    /// Interaction logic for AddSportC.xaml
    /// </summary>
    public partial class AddSportC : UserControl
    {
        private MyViewModel vm;

        public AddSportC(MyViewModel vm)
        {
            InitializeComponent();
            initSportTypeCB();
            initCityCB();
            initNumOfPlayersCB();
            this.vm = vm;
        }

        private void initNumOfPlayersCB()
        {
            List<int> nums = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            numOfPlayersCB.ItemsSource = nums;
        }

        private void initCityCB()
        {
            List<string> city = Data.citiesList;
            cityCB.SelectedIndex = 0;
            cityCB.ItemsSource = city;
        }

        private void initSportTypeCB()
        {
            List<string> sportType = new List<string>()
            {
                "Tennis",
                "Football",
                "BasketBall",
                "Rugby",
                "Running",
                "Swimming"
            };
            sportTypeCB.SelectedIndex = 0;
            sportTypeCB.ItemsSource = sportType;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sportType = sportTypeCB.SelectedItem.ToString();
            string city = cityCB.SelectedItem.ToString();
            string address = addressTB.Text;
            int numOfPlayers = Int32.Parse(numOfPlayersCB.SelectedItem.ToString());
            string description = descTb.Text;
            vm.addActivity(vm.VM_UserConnected, numOfPlayers, city, address, "", "sport", "sport", "", "", false, false, false, 0, false, description, sportType, "", "", "", "", false);
            MessageBox.Show("Activity added successfully");
            this.Visibility = Visibility.Hidden;
        }
    }
}