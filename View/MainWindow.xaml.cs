using PartnersMatcher.View.Controls;
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

namespace PartnersMatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyViewModel vm;
        SignC connectC;

        public MainWindow()
        {
            InitializeComponent();
            vm = new MyViewModel(new Model.MyModel());
            this.DataContext = vm;
            connectC = new SignC(vm);
            connectGrid.Children.Add(connectC);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //
        }

        private void trigger_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (trigger.Text.ToLower() == "true")
            {
                connectC.Visibility = Visibility.Hidden;
                loggedIn li = new loggedIn(vm.VM_UserConnected);
                SearchControl sc = new SearchControl(vm);
                mainGrid.Children.Add(sc);
                connectGrid.Children.Add(li);
            }
        }
    }
}