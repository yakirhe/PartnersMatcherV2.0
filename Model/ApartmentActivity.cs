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
        private string city;

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


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

        public ApartmentActivity(string city,string address, int rentalFee, bool petFriendly, bool isKosher, bool isSmokingFriendly, int maxUsers, List<User> partners, string activityName, string type, int payments,string description, List<User> pendingList) : base(maxUsers,address, partners, activityName,activityName, rentalFee, pendingList)
        {
            this.description = description;
            this.city = city;
            this.address = address;
            this.rentalFee = rentalFee;
            this.petFriendly = petFriendly;
            this.isKosher = isKosher;
            this.isSmokingFriendly = isSmokingFriendly;
        }
    }
}
