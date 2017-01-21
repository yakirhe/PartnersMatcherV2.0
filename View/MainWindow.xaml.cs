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
        SideMenuC sm;
        loggedIn li;

        public MainWindow()
        {
            InitializeComponent();
            vm = new MyViewModel(new Model.MyModel());
            addSideMenu();
            this.DataContext = vm;
            connectC = new SignC(vm);
            connectGrid.Children.Add(connectC);
        }

        private void addSideMenu()
        {
            sm = new SideMenuC(vm);
            Grid.SetRow(sm, 1);
            connectGrid.Children.Add(sm);
            sm.Visibility = Visibility.Hidden;
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
                li = new loggedIn(vm.VM_UserConnected);
                connectGrid.Children.Add(li);
                sm.Visibility = Visibility.Visible;
            }
        }

        private void sideMenuWasPressed(object sender, TextChangedEventArgs e)
        {
            switch (sideMenuSelected.Text)
            {
                case "1":
                    SearchControl sc = new SearchControl(vm);
                    mainPanel.Children.Clear();
                    mainPanel.Children.Add(sc);
                    //search activity
                    break;
                case "2":
                    //add activity
                    mainPanel.Children.Clear();
                    AddActivityC aac = new AddActivityC(vm);
                    mainPanel.Children.Add(aac);
                    break;
                case "3":
                    mainPanel.Children.Clear();
                    vm.logOut();
                    sm.Visibility = Visibility.Hidden;
                    li.Visibility = Visibility.Hidden;
                    connectC.Visibility = Visibility.Visible;
                    //log out
                    break;
                default:
                    break;
            }
        }
    }
}