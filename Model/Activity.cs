using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.Model
{
    [Serializable]
    public abstract class Activity
    {
        #region Properties

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int activityNumber;

        public int ActivityNumber
        {
            get { return activityNumber; }
            set { activityNumber = value; }
        }


        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private int maxUsers;

        public int MaxUsers
        {
            get { return maxUsers; }
            set { maxUsers = value; }
        }

        private List<User> partners;

        public List<User> Partners
        {
            get { return partners; }
            set { partners = value; }
        }

        private string activityName;

        public string ActivityName
        {
            get { return activityName; }
            set { activityName = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private double payments;

        public double Payments
        {
            get { return payments; }
            set { payments = value; }
        }

        private List<User> pendingList;

        public List<User> PendingList
        {
            get { return pendingList; }
            set { pendingList = value; }
        }

        private User activityManager;

        public User ActivityManager
        {
            get { return activityManager; }
            set { activityManager = value; }
        }

        #endregion Properties

        public Activity(int maxUsers, string address, string city, List<User> partners, string activityName, string type, double payments, List<User> pendingList, string description, User activityManager)
        {
            this.activityManager = activityManager;
            this.description = description;
            this.maxUsers = maxUsers;
            this.address = address;
            this.city = city;
            this.partners = partners;
            this.activityName = activityName;
            this.type = type;
            this.payments = payments;
            this.pendingList = pendingList;
        }
    }
}