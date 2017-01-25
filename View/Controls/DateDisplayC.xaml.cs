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
using System.Windows.Shapes;

namespace PartnersMatcher.View.Controls
{
    /// <summary>
    /// Interaction logic for DateDisplayC.xaml
    /// </summary>
    public partial class DateDisplayC : Window
    {
        private DateActivity da;
        private MyViewModel vm;

        public DateDisplayC(DateActivity da, MyViewModel vm)
        {
            this.da = da;
            this.vm = vm;
            this.DataContext = da;
            InitializeComponent();
            if (vm.VM_UserConnected.Email == da.ActivityManager.Email)
            {
                btn.IsEnabled = false;
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("a request was sent to the group manager");
            vm.sendRequest(vm.VM_UserConnected, da);
        }
    }
}
