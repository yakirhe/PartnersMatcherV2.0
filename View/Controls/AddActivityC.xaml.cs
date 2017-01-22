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
    /// Interaction logic for AddActivityC.xaml
    /// </summary>
    public partial class AddActivityC : UserControl
    {
        MyViewModel vm;

        public AddActivityC(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            initTypeComboBox();
        }

        private void initTypeComboBox()
        {
            List<string> types = new List<string> { "Apartment", "Trip", "Date", "Sport" };
            activityTypeCb.ItemsSource = types;
        }

        /// <summary>
        /// selected activity type
        /// update the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            switch (activityTypeCb.Text)
            {
                case "Apartment":
                    AddApartmentC addApartmentC = new AddApartmentC(vm);
                    mainGrid.Children.Add(addApartmentC);
                    break;
                case "Trip":
                    break;
                case "Date":
                    AddDateC addDateC = new AddDateC(vm);
                    mainGrid.Children.Add(addDateC);
                    break;
                case "Sport":
                    break;
                default:
                    break;
            }
        }
    }
}
