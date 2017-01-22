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
    /// Interaction logic for AddDateC.xaml
    /// </summary>
    public partial class AddDateC : UserControl
    {
        MyViewModel vm;

        public AddDateC(MyViewModel vm)
        {
            InitializeComponent();
            cityCb.ItemsSource = Data.citiesList;
            cityCb.SelectedIndex = 0;
            this.vm = vm;
        }

        /// <summary>
        /// send the details to the model layer and insert them to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string address = addressTb.Text;
            string budget = budgetTb.Text;
            string city = cityCb.SelectedItem.ToString();
            bool alcohol = alcoholCb.IsChecked.Value;
            bool smoking = smokingCb.IsChecked.Value;
            string description = descTb.Text;
            int num;
            if(!Int32.TryParse(budget,out num))
            {
                MessageBox.Show("Budget must to be a number");
                return;
            }
            if (address == "")
            {
                MessageBox.Show("You didn't write the date address");
                return;
            }
        }
    }
}
