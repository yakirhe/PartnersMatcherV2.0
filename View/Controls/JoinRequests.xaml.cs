using PartnersMatcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for JoinRequests.xaml
    /// </summary>
    public partial class JoinRequests : Window
    {
        Dictionary<string, ObservableCollection<string>> requestsDict;
        List<string> emails = new List<string>();
        private MyViewModel vm;
        ObservableCollection<string> allReq;

        public JoinRequests(Dictionary<string, ObservableCollection<string>> requestsDict, int counter, MyViewModel vm)
        {
            this.vm = vm;
            InitializeComponent();
            this.requestsDict = requestsDict;
            title.Text = "You have " + counter + " new requests";
            allReq = new ObservableCollection<string>();
            foreach (string request in requestsDict.Keys)
            {
                for (int i = 0; i < requestsDict[request].Count; i++)
                {
                    string newReq = requestsDict[request][i] + " requested to join " + request;
                    allReq.Add(newReq);
                }
            }
            requestsLB.ItemsSource = allReq;
        }

        /// <summary>
        /// accept
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //get the selected items
            List<string> acceptRequests = new List<string>();
            //get the selected items
            foreach (string str in requestsLB.SelectedItems)
            {
                acceptRequests.Add(str);
            }
            //remove from list view
            foreach (string toRemove in acceptRequests)
            {
                allReq.Remove(toRemove);
            }
            //send to vm
            vm.accepsRequests(acceptRequests);
        }

        /// <summary>
        /// ignore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> ignoreRequest = new List<string>();
            //get the selected items
            foreach (string str in requestsLB.SelectedItems)
            {
                ignoreRequest.Add(str);
            }
            //remove
            foreach (string toRemove in ignoreRequest)
            {
                allReq.Remove(toRemove);
            }
            //getMails(acceptRequest);
            vm.ignoreFromPendingList(ignoreRequest);
        }
    }
}
