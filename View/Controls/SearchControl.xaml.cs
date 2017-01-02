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
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {
        MyViewModel vm;

        public SearchControl(MyViewModel vm)
        {
            InitializeComponent();
            List<string> serchGroup = new List<string>()
            {
                "Sport" , "Date","Trip","Apartments"
            };
            this.vm = vm;
            this.DataContext = vm;
            serchBox.ItemsSource = serchGroup;
        }

        private void serchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = serchBox.SelectedItem.ToString();
            switch (value)
            {
                case "Sport":
                    activityGrid.Children.Clear();
                    SportC sport = new SportC();
                    activityGrid.Children.Add(sport);
                    break;
                case "Trip":
                    activityGrid.Children.Clear();
                    TripC trip = new TripC();
                    activityGrid.Children.Add(trip);
                    break;
                case "Date":
                    activityGrid.Children.Clear();
                    DateC date = new DateC();
                    activityGrid.Children.Add(date);
                    break;
                case "Apartments":
                    activityGrid.Children.Clear();
                    ApartmentsC apartments = new ApartmentsC(vm);
                    activityGrid.Children.Add(apartments);
                    break;
            }
        }
    }
}
