using PartnersMatcher.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.ViewModel
{
    public class MyViewModel : INotifyPropertyChanged
    {
        private MyModel model;

        public MyViewModel(MyModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                Connected = true;
                if (e.PropertyName == "ActivityList")
                {
                    updateProperties();
                }
            };
        }

        private void updateProperties()
        {
            ApartmentActivity = model.ActivityList["apartment"];
            DateActivity = model.ActivityList["date"];
            SportActivity = model.ActivityList["sport"];
            TripActivity = model.ActivityList["trip"];
        }

        private List<Activity> apartmentActivity;

        public List<Activity> ApartmentActivity
        {
            get { return model.ActivityList["apartment"]; }
            set
            {
                apartmentActivity = value;
                notifyPropertyChanged("ApartmentActivity");
            }
        }

        internal void filterByLocation(string city, string type)
        {
            switch (type)
            {
                case "apartment":
                    filterCity(city,model.ActivityList[type]);
                    break;
                case "sport":
                    filterCity(city, model.ActivityList[type]);
                    break;
                case "date":
                    filterCity(city, model.ActivityList[type]);
                    break;
                case "trip":
                    filterCity(city, model.ActivityList[type]);
                    break;
                default:
                    break;
            }
        }

        private void filterCity(string city, List<Activity> activities)
        {
            List<Activity> dupAct = activities;
            foreach (Activity activity in dupAct)
            {
                if (city.ToLower() != activity.Location.ToLower())
                {
                    activities.Remove(activity);
                }
            }
        }

        private List<Activity> dateActivity;

        public List<Activity> DateActivity
        {
            get { return model.ActivityList["date"]; }
            set
            {
                dateActivity = value;
                notifyPropertyChanged("DateActivity");
            }
        }

        private List<Activity> sportActivity;

        public List<Activity> SportActivity
        {
            get { return model.ActivityList["sport"]; }
            set
            {
                sportActivity = value;
                notifyPropertyChanged("SportActivity");
            }
        }

        private List<Activity> tripActivity;

        public List<Activity> TripActivity
        {
            get { return model.ActivityList["trip"]; }
            set
            {
                tripActivity = value;
                notifyPropertyChanged("TripActivity");
            }
        }


        private User userConnected;

        public User VM_UserConnected
        {
            get { return model.UserConnected; }
            set { userConnected = value; }
        }

        private bool connected;

        public bool Connected
        {
            get { return connected; }
            set
            {
                connected = value;
                notifyPropertyChanged("Connected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        internal void addUser(string fullName, string email, string dob, string password, string city, string phone, string photo)
        {
            model.addUser(fullName, email, dob, password, city, phone, photo);
        }

        internal bool ifUserExist(string email)
        {
            return model.ifUserExist(email);
        }

        internal void saveUsers()
        {
            model.saveUsers();
        }

        internal bool signIn(string email, string password)
        {
            return model.signIn(email, password);
        }
    }
}