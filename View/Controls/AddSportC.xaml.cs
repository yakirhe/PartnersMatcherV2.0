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
        MyViewModel vm;

        public AddSportC(MyViewModel vm)
        {
            InitializeComponent();
            initSportTypeCB();
            initCityCB();
            this.vm = vm;
        }

        private void initCityCB()
        {
            List<string> city = new List<string>()
            {
                "Haifa",
                "Jerusalem",
                "Tel aviv",
                "Dimona",
                "Kiryat ata",
                "Netivot",
                "Rehovot",
                "rishon lezion",
                "ashdod",
                "Beer sheva",
            };
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
        }
    }
}
