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
        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
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
        #endregion

        public Activity(int maxUsers, string location, List<User> partners, string activityName, string type, double payments, List<User> pendingList)
        {
            this.maxUsers = maxUsers;
            this.location = location;
            this.partners = partners;
            this.activityName = activityName;
            this.type = type;
            this.payments = payments;
            this.pendingList = pendingList;
        }
    }
}
