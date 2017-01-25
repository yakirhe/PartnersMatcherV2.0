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
    /// Interaction logic for AddTripC.xaml
    /// </summary>
    public partial class AddTripC : UserControl
    {
        private MyViewModel vm;

        public AddTripC(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            initRegionCB();
            initNumOfPartners();
        }

        private void initNumOfPartners()
        {
            List<int> numberOfPartners = new List<int> { 2, 3, 4, 5, 6 };
            numOfPartnersCB.SelectedIndex = 0;
            numOfPartnersCB.ItemsSource = numberOfPartners;
        }

        private void initRegionCB()
        {
            regionCb.ItemsSource = Data.regionsList;
            regionCb.SelectedIndex = 0;
        }

        private void publishBtn_Click(object sender, RoutedEventArgs e)
        {
            string region = regionCb.SelectedItem.ToString();
            string destination = destinationsTb.Text;
            string budget = budgetTb.Text;
            int res;
            if (!Int32.TryParse(budget, out res))
            {
                MessageBox.Show("The budget must be a number");
                return;
            }
            string description = descTb.Text;
            int numOfPartners = Int32.Parse(numOfPartnersCB.SelectedItem.ToString());
            vm.addActivity(vm.VM_UserConnected, numOfPartners, "", region, "", "Trip", "Trip", "0", "", false, false, false, Int32.Parse(budget), false, description, "", region, destination, "", "", false);
            MessageBox.Show("Activity added successfully");
            this.Visibility = Visibility.Hidden;
        }
    }
}