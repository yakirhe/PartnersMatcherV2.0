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
    /// Interaction logic for AddApartmentC.xaml
    /// </summary>
    public partial class AddApartmentC : UserControl
    {
        private MyViewModel vm;

        public AddApartmentC(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            initCityListBox();
            initComboBox();
        }

        private void initCityListBox()
        {
            List<string> city = Data.citiesList;
            cityCbox.SelectedIndex = 0;
            cityCbox.ItemsSource = city;
        }

        private void initComboBox()
        {
            List<int> numberOfPartners = new List<int> { 2, 3, 4, 5, 6 };
            numberOfPartnersCbox.SelectedIndex = 0;
            numberOfPartnersCbox.ItemsSource = numberOfPartners;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string address = addressTbox.Text;
            string rentalFee = rentalFeeTbox.Text;
            if (rentalFee != "")
            {
                bool isNumber = checkIfNumber(rentalFee);
                if (!isNumber)
                {
                    MessageBox.Show("The rental fee must be a number");
                    return;
                }
            }
            int numOfPartners = Int32.Parse(numberOfPartnersCbox.SelectedItem.ToString());
            bool petFriendly = petFriendlyCB.IsChecked.Value;
            bool smokingFriendly = isSmokingFriendlyCB.IsChecked.Value;
            bool isKosher = isKosherCB.IsChecked.Value;
            string city = cityCbox.SelectedItem.ToString();
            string description = descTb.Text;
            vm.addActivity(vm.VM_UserConnected, numOfPartners, city, address, numOfPartners.ToString(), "Apartment", "Apartment", rentalFee, "", petFriendly, isKosher, smokingFriendly, 0, true, description, "", "", "", "", "", false);
            MessageBox.Show("Activity added successfully");
            this.Visibility = Visibility.Hidden;
        }

        private bool checkIfNumber(string rentalFee)
        {
            int res;
            if (Int32.TryParse(rentalFee, out res))
            {
                return true;
            }
            return false;
        }
    }
}