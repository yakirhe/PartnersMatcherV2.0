using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.Model
{
    class SportActivity : Activity
    {
        #region Properties
        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string sportType;

        public string SportType
        {
            get { return sportType; }
            set { sportType = value; }
        }
        #endregion

        public SportActivity(string location, string sportType, int maxUsers, List<User> partners, string activityName, string type, double payments, List<User> pendingList) : base(maxUsers, location, partners, activityName, type, payments, pendingList)
        {
            this.sportType = sportType;
        }
    }
}
