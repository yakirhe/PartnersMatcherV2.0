using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.Model
{
    public class TripActivity : Activity
    {
        #region Properties

        private int budget;

        public int Budget
        {
            get { return budget; }
            set { budget = value; }
        }

        private string destination;

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        private string region;

        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        private string startingDate;

        public string StartingDate
        {
            get { return startingDate; }
            set { startingDate = value; }
        }

        private string approximateDuration;

        public string ApproximateDuration
        {
            get { return approximateDuration; }
            set { approximateDuration = value; }
        }

        private bool carNeeded;

        public bool CarNeeded
        {
            get { return carNeeded; }
            set { carNeeded = value; }
        }

        #endregion Properties

        public TripActivity(string address, string city, int budget, string region, string destination, string startingDate, string approximateDuration, bool carNeeded, int maxUsers, List<User> partners, string activityName, string type, double payments, List<User> pendingList, string description, User activityManager) : base(maxUsers, address, city, partners, activityName, type, payments, pendingList, description, activityManager)
        {
            this.budget = budget;
            this.destination = destination;
            this.startingDate = startingDate;
            this.approximateDuration = approximateDuration;
            this.carNeeded = carNeeded;
            this.region = region;
        }
    }
}