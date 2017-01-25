using System.Collections.Generic;

namespace PartnersMatcher.Model
{
    public class ApartmentActivity : Activity
    {
        #region Properties

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

        #endregion Properties

        public ApartmentActivity(User activityManager, string city, string address, int rentalFee, bool petFriendly, bool isKosher, bool isSmokingFriendly, int maxUsers, List<User> partners, string activityName, string type, int payments, string description, List<User> pendingList) : base(maxUsers, address, city, partners, activityName, activityName, rentalFee, pendingList, description, activityManager)
        {
            this.rentalFee = rentalFee;
            this.petFriendly = petFriendly;
            this.isKosher = isKosher;
            this.isSmokingFriendly = isSmokingFriendly;
        }
    }
}