using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.Model
{
    class DateActivity : Activity
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

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        public DateActivity(string location, double budget, bool isSmokingFriendly, bool includeAlcohol, string description, List<User> partners, string activityName, string type, double payments, List<User> pendingList) : base(2, location, partners, activityName, type, payments, pendingList)
        {
            this.budget = budget;
            this.isSmokingFriendly = isSmokingFriendly;
            this.includeAlcohol = includeAlcohol;
            this.description = description;
        }
    }
}
