using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.Model
{
    public class ApartmentActivity : Activity
    {
        #region Properties
        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private double rentalFee;

        public double RentalFee
        {
            get { return rentalFee; }
            set { rentalFee = value; }
        }

        private bool petFriendly;

        public bool PetFriendly
        {
            get { return petFriendly; }
            set { petFriendly = value; }
        }

        private bool isKosher;

        public bool IsKosher
        {
            get { return isKosher; }
            set { isKosher = value; }
        }

        private bool isSmokingFriendly;

        public bool IsSmokingFriendly
        {
            get { return isSmokingFriendly; }
            set { isSmokingFriendly = value; }
        }
        #endregion

        public ApartmentActivity(string location, double rentalFee, bool petFriendly, bool isKosher, bool isSmokingFriendly, int maxUsers, List<User> partners, string activityName, string type, double payments, List<User> pendingList) : base(maxUsers, location, partners, activityName, type, payments, pendingList)
        {
            this.rentalFee = rentalFee;
            this.petFriendly = petFriendly;
            this.isKosher = isKosher;
            this.isSmokingFriendly = isSmokingFriendly;
        }
    }
}
