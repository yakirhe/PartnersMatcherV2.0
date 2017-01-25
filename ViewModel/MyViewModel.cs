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

        internal void sendRequest(User userReq, Activity activity)
        {
            model.sendRequest(userReq, activity);
        }

        internal void filterByLocation(string city, string type, double maxPrice = 0)
        {
            switch (type)
            {
                case "apartment":
                    filterCity(city, model.ActivityList[type], type, maxPrice);
                    break;

                case "sport":
                    filterCity(city, model.ActivityList[type], type, maxPrice);
                    break;

                case "date":
                    filterCity(city, model.ActivityList[type], type, maxPrice);
                    break;

                case "trip":
                    filterCity(city, model.ActivityList[type], type, maxPrice);
                    break;

                default:
                    break;
            }
        }

        public void accepsRequests(List<string> acceptRequests)
        {
            model.acceptRequests(acceptRequests);
        }

        public List<Activity> FilteredApts { get; set; }
        public List<Activity> FilteredTrips { get; set; }
        public List<Activity> FilteredDates { get; set; }
        public List<Activity> FilteredSports { get; set; }

        private void filterCity(string city, List<Activity> activities, string type, double maxPrice = 0)
        {
            switch (type)
            {
                case "apartment":
                    List<Activity> filteredApts = new List<Activity>();
                    foreach (ApartmentActivity activity in activities)
                    {
                        if (city.ToLower() == activity.City.ToLower())
                        {
                            if (maxPrice != 0 && activity.RentalFee <= maxPrice)
                                filteredApts.Add(activity);
                        }
                    }
                    FilteredApts = filteredApts;
                    break;

                case "sport":
                    List<Activity> filteredSports = new List<Activity>();
                    foreach (SportActivity activity in activities)
                    {
                        if (city.ToLower() == activity.City.ToLower())
                        {
                            filteredSports.Add(activity);
                        }
                    }
                    FilteredSports = filteredSports;
                    break;

                case "date":
                    List<Activity> filteredDate = new List<Activity>();
                    foreach (DateActivity activity in activities)
                    {
                        if (city.ToLower() == activity.City.ToLower())
                        {
                            filteredDate.Add(activity);
                        }
                    }
                    FilteredDates = filteredDate;
                    break;

                case "trip":
                    List<Activity> filteredTrips = new List<Activity>();
                    foreach (TripActivity activity in activities)
                    {
                        if (city.ToLower() == activity.Region.ToLower())
                        {
                            filteredTrips.Add(activity);
                        }
                    }
                    FilteredTrips = filteredTrips;
                    break;

                default:
                    break;
            }
        }

        public void ignoreFromPendingList(List<string> ignoreRequest)
        {
            model.ignoreFromPendingList(ignoreRequest);
        }

        public void addActivity(User activityManager, int numOfPartners, string city, string address, string partners, string activityName, string activityType, string rentalFee, string pendinglist, bool petFriendly, bool isKosher, bool smokingFriendly, int budget, bool alcoholIncluded, string description, string sportType, string region, string destination, string startingDate, string approximateDuration, bool carNeeded)
        {
            model.addActivity(numOfPartners, city, address, partners, activityName, activityType, rentalFee, pendinglist, petFriendly, isKosher, smokingFriendly, budget, alcoholIncluded, description, sportType, region, destination, startingDate, approximateDuration, carNeeded, activityManager);
        }

        public void logOut()
        {
            Connected = false;
            VM_UserConnected = null;
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

        private string selection;

        public string Selection
        {
            get { return selection; }
            set
            {
                selection = value;
                notifyPropertyChanged("Selection");
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

        public void addUser(string fullName, string email, string dob, string password, string city, string phone, bool smoking, bool pet)
        {
            model.addUser(fullName, email, dob, password, city, phone, smoking, pet);
        }

        public bool signIn(string email, string password, MyViewModel vm)
        {
            return model.signIn(email, password, vm);
        }
    }
}