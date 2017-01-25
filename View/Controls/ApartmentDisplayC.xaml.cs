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
    /// Interaction logic for ApartmentDisplayC.xaml
    /// </summary>
    public partial class ApartmentDisplayC : Window
    {
        private ApartmentActivity apartA;
        private MyViewModel vm;

        public ApartmentDisplayC(ApartmentActivity apartA, MyViewModel vm)
        {
            this.apartA = apartA;
            this.vm = vm;
            this.DataContext = apartA;
            InitializeComponent();
            if (vm.VM_UserConnected.Email == apartA.ActivityManager.Email)
            {
                btn.IsEnabled = false;
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("a request was sent to the group manager");
            vm.sendRequest(vm.VM_UserConnected, apartA);
        }
    }
}
