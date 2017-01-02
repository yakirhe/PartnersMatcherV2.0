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
    /// Interaction logic for SignC.xaml
    /// </summary>
    public partial class SignC : UserControl
    {
        private MyViewModel vm;

        public SignC(MyViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
        }

        /// <summary>
        /// on sign up click open the sign up form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SignUpW suw = new SignUpW(vm);
            suw.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (emailTxt.Text == "" || passwordTxt.Password == "")
            {
                MessageBox.Show("Please fill in all the required fields");
            }
            else
            {
                if (vm.signIn(emailTxt.Text, passwordTxt.Password))
                {
                    MessageBox.Show("Connected");
                }
            }
        }
    }
}