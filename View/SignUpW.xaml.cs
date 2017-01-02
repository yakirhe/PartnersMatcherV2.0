using PartnersMatcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PartnersMatcher.View
{
    /// <summary>
    /// Interaction logic for SignUpW.xaml
    /// </summary>
    public partial class SignUpW : Window
    {
        private MyViewModel vm;

        public SignUpW(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //check data if not null and send to the db
            if (fullNameTxt.Text == "" || dobTxt.Text == "" || passwordTxt.Text == "" || cityTxt.Text == "" || phoneTxt.Text == "" || emailTxt.Text == "")
            {
                MessageBox.Show("You need to fill all the required fields");
            }
            else
            {
                if (vm.ifUserExist(emailTxt.Text))
                {
                    MessageBox.Show("this user already exist ,try again");
                }
                else
                {
                    vm.addUser(fullNameTxt.Text, emailTxt.Text, dobTxt.Text, passwordTxt.Text, cityTxt.Text, phoneTxt.Text, "f");
                    
                    MessageBox.Show("Email was sent to " + emailTxt.Text);
                    Thread.Sleep(1000);
                    this.Close();
                }
            }
        }
    }
}