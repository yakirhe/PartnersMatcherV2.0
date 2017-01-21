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
    /// Interaction logic for SideMenuC.xaml
    /// </summary>
    public partial class SideMenuC : UserControl
    {
        MyViewModel vm;
        private string selection;

        public string Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        public SideMenuC(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.DataContext = vm;
            Selection = "0";
        }

        /// <summary>
        /// load to the main frame the search activity panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.Selection = "1";
        }

        private void addActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.Selection = "2";
        }

        private void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.Selection = "3";
        }
    }
}
