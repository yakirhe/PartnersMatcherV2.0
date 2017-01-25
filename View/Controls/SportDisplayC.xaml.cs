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
    /// Interaction logic for SportDisplayC.xaml
    /// </summary>
    public partial class SportDisplayC
    {
        private SportActivity sa;
        private MyViewModel vm;

        public SportDisplayC(SportActivity sa, MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.sa = sa;
            this.DataContext = sa;
            if (vm.VM_UserConnected.Email == sa.ActivityManager.Email)
            {
                btn.IsEnabled = false;
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("a request was sent to the group manager");
            vm.sendRequest(vm.VM_UserConnected,sa);
        }
    }
}