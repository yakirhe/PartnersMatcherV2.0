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
        MyViewModel vm;

        public AddApartmentC(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            initComboBox();
        }

        private void initComboBox()
        {
            List<int> numberOfPartners = new List<int> { 2, 3, 4, 5, 6 };
            numberOfPartnersCbox.SelectedIndex = 0;
            numberOfPartnersCbox.ItemsSource = numberOfPartners;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string location = locationTbox.Text;
            string rentalFee = rentalFeeTbox.Text;
            if (rentalFee != "")
            {
                bool isNumber = checkIfNumber(rentalFee);
                return;
            }
            int numOfPartners = Int32.Parse(numberOfPartnersCbox.SelectedItem.ToString());
            //vm.addActivity();
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
