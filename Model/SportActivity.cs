using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.Model
{
    public class SportActivity : Activity
    {
        #region Properties

        private string sportType;

        public string SportType
        {
            get { return sportType; }
            set { sportType = value; }
        }

        #endregion Properties

        public SportActivity(string city, string address, string sportType, int maxUsers, List<User> partners, string activityName, string type, double payments, List<User> pendingList, string description, User activityManager) : base(maxUsers, address, city, partners, activityName, type, payments, pendingList, description, activityManager)
        {
            this.sportType = sportType;
        }
    }
}