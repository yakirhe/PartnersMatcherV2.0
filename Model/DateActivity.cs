using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.Model
{
    public class DateActivity : Activity
    {
        #region Properties

        private double budget;

        public double Budget
        {
            get { return budget; }
            set { budget = value; }
        }

        private bool isSmokingFriendly;

        public bool IsSmokingFriendly
        {
            get { return isSmokingFriendly; }
            set { isSmokingFriendly = value; }
        }

        private bool includeAlcohol;

        public bool IncludeAlcohol
        {
            get { return includeAlcohol; }
            set { includeAlcohol = value; }
        }

        #endregion Properties

        public DateActivity(string address, string city, double budget, bool isSmokingFriendly, bool includeAlcohol, string description, List<User> partners, string activityName, string type, double payments, List<User> pendingList, User activityManager) : base(2, address, city, partners, activityName, type, payments, pendingList, description, activityManager)
        {
            this.budget = budget;
            this.isSmokingFriendly = isSmokingFriendly;
            this.includeAlcohol = includeAlcohol;
        }
    }
}