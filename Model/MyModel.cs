using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PartnersMatcher.Model
{
    public class MyModel : INotifyPropertyChanged
    {
        #region Properties
        private Dictionary<string, User> usersDict;
        private Dictionary<string, List<Activity>> activityList;
        //
        public Dictionary<string, List<Activity>> ActivityList
        {
            get { return activityList; }
            set
            {
                activityList = value;
                notifyPropertyChanged("ActivityList");
            }
        }

        private User userConnected;

        public User UserConnected
        {
            get { return userConnected; }
            set
            {
                userConnected = value;
                notifyPropertyChanged("UserConnected");
            }
        }
        #endregion


        public MyModel()
        {
            usersDict = new Dictionary<string, User>();
            createDb();
            EnterActivies();
        }

        private void EnterActivies()
        {
            activityList = new Dictionary<string, List<Activity>>();
            activityList["sport"] = new List<Activity>();
            activityList["date"] = new List<Activity>();
            activityList["trip"] = new List<Activity>();
            activityList["apartment"] = new List<Activity>();
            //create activities
            Activity sport1 = new SportActivity("Haifa", "football", 12, new List<User>(), "football at sportek", "sport", 0, new List<User>());
            Activity sport2 = new SportActivity("Beer sheva", "basketball", 8, new List<User>(), "basketball at the sport center", "sport", 0, new List<User>());
            Activity date1 = new DateActivity("Beer Sheva", 200, true, true, "Date at the benji", new List<User>(), "date at the benji", "date", 0, new List<User>());
            Activity date2 = new DateActivity("Jerusalem", 50, true, true, "date at the coffix", new List<User>(), "date at the coffix", "date", 0, new List<User>());
            Activity trip1 = new TripActivity("Madagascar", "12.12", "24 days", false, 4, new List<User>(), "Jungles trip", "trip", 11000, new List<User>());
            Activity trip2 = new TripActivity("Cayman islands & Jamaica", "7.4", "12 days", false, 7, new List<User>(), "Trip to the caribbean sea", "trip", 14000, new List<User>());
            Activity apartment1 = new ApartmentActivity("Tel Aviv", 2200, true, false, true, 3, new List<User>(), "4 rooms apartment", "apartment", 0, new List<User>());
            Activity apartment2 = new ApartmentActivity("Jerusalem", 1600, true, true, false, 3, new List<User>(), "4 rooms apartment + 20ft balcony", "apartment", 0, new List<User>());
            activityList["sport"].Add(sport1);
            activityList["sport"].Add(sport2);
            activityList["date"].Add(date1);
            activityList["date"].Add(date2);
            activityList["trip"].Add(trip1);
            activityList["trip"].Add(trip2);
            activityList["apartment"].Add(apartment1);
            activityList["apartment"].Add(apartment2);
        }

        private void createDb()
        {
            //check if db exist, if not create a new one
            if (File.Exists("users.txt"))
            {
                loadDb();
            }
        }

        private void loadDb()
        {
            using (Stream s = new FileStream("users.txt", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                usersDict = (Dictionary<string, User>)bin.Deserialize(s);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        internal bool signIn(string email, string password)
        {
            if (ifUserExist(email))
            {
                if (usersDict[email].Password != password)
                {
                    MessageBox.Show("Password is incorrect");
                    return false;
                }
                else
                {
                    //user is in the system
                    UserConnected = usersDict[email];
                    return true;
                }
            }
            else
            {
                MessageBox.Show("The email " + email + " is not registered to the system");
                return false;
            }
        }

        internal void addUser(string fullName, string email, string dob, string password, string city, string phone, string photo)
        {
            User user = new User(email, password, fullName, dob, phone, city, photo);
            usersDict[email] = user;
            sentMail(fullName, email);
        }

        private void sentMail(string fullName, string email)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient smptServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("partnermacher@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Register to  PartnerMacher";
                mail.Body = "welcome " + fullName + " hope you enjoy using PartnerMatcher";
                smptServer.Port = 587;
                smptServer.Credentials = new System.Net.NetworkCredential("partnermacher@gmail.com", "r12345678r");
                smptServer.EnableSsl = true;
                smptServer.Send(mail);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal void saveUsers()
        {
            using (Stream s = new FileStream("users.txt", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(s, usersDict);
            }
        }

        internal bool ifUserExist(string email)
        {
            email = email.ToLower();
            if (usersDict.ContainsKey(email))
            {
                return true;
            }
            return false;
        }
    }
}