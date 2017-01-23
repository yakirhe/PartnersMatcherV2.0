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

        private string sportType;

        public string SportType
        {
            get { return sportType; }
            set { sportType = value; }
        }
        #endregion

        public SportActivity(string city, string address, string sportType, int maxUsers, List<User> partners, string activityName, string type, double payments, List<User> pendingList, string description) : base(maxUsers, city, partners, activityName, type, payments, pendingList, description)
        {
            this.city = city;
            this.address = address;
            this.sportType = sportType;
        }
    }
}
