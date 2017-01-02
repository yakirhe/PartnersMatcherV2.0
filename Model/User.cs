using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersMatcher.Model
{
    [Serializable]
    public class User
    {
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string photo;

        public string Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        private string dob;

        public string Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public User(string email, string password, string fullName, string dob, string phone, string city, string photo)
        {
            this.email = email;
            this.password = password;
            this.fullName = fullName;
            this.dob = dob;
            this.phone = phone;
            this.city = city;
            this.photo = photo;
        }
    }
}
