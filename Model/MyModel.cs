using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.OleDb;
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
        public Dictionary<string, List<Activity>> ActivityList
        {
            get { return activityList; }
            set
            {
                activityList = value;
                notifyPropertyChanged("ActivityList");
            }
        }

        static int activityNumber;

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
        OleDbConnection dbConnection;

        public MyModel()
        {
            usersDict = new Dictionary<string, User>();
            createDb();
            loadActivities();
        }

        private void loadActivities()
        {
            initActivities();
            try
            {
                int count = 1;
                dbConnection.Open();
                string activityQuery = "select * from Activities";
                OleDbCommand command = new OleDbCommand(activityQuery, dbConnection);
                OleDbDataReader r = command.ExecuteReader();
                while (r.Read())
                {
                    string activityType = r.GetString(6);
                    switch (activityType)
                    {
                        case "Apartment":
                            int maxUsers = r.GetInt32(1);
                            string city = r.GetString(2);
                            string address = r.GetString(3);
                            string partners = r.GetString(4);
                            string activityName = r.GetString(5);
                            int ammountToPay = r.GetInt32(7);
                            string pendingList = r.GetString(8);
                            bool petAllowed = r.GetBoolean(9);
                            bool isKosher = r.GetBoolean(10);
                            bool smokingAllowed = r.GetBoolean(11);
                            int budget = r.GetInt32(12);
                            string description = r.GetString(14);
                            //convert partners from string to list
                            Activity activity = new ApartmentActivity(city, address, budget, petAllowed, isKosher, smokingAllowed, maxUsers, null, activityName, activityType, 0, description, null);
                            activityList[activityType.ToLower()].Add(activity);
                            break;
                        case "sport":
                            break;
                        case "date":
                            break;
                        case "trip":
                            break;
                        default:
                            break;
                    }
                    count++;
                }
                activityNumber = count;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void initActivities()
        {
            activityList = new Dictionary<string, List<Activity>>();
            activityList["apartment"] = new List<Activity>();
            activityList["sport"] = new List<Activity>();
            activityList["date"] = new List<Activity>();
            activityList["trip"] = new List<Activity>();
        }


        public void addActivity(int numOfPartners, string city, string address, string partners, string activityName, string activityType, string rentalFee, string pendinglist, bool petFriendly, bool isKosher, bool smokingFriendly, int budget, bool alcoholIncluded, string description, string sportType, string region, string destination, string startingDate, string approximateDuration, bool carNeeded)
        {
            List<User> sendNull = new List<User>();
            switch (activityType)
            {
                case "Apartment":
                    Activity newApartment = new ApartmentActivity(city, address, Int32.Parse(rentalFee), petFriendly, isKosher, smokingFriendly, Int32.Parse(partners), sendNull, activityName, activityType, Int32.Parse(rentalFee), description, sendNull);
                    sendQuery(newApartment, "apartment");
                    break;
                case "sport":
                    Activity newSport = new SportActivity(city, address, sportType, numOfPartners, sendNull, activityName, activityType, 0, null, description);
                    sendQuery(newSport, "sport");
                    break;
            }
        }

        private void createDb()
        {
            //connect to db
            string curPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string relPath = @"\Db\PMDB.accdb";
            string fullPath = curPath + relPath;
            //connect to db
            dbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullPath + "; Persist Security Info=False");
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
            if (validMail(email, false))
            {
                try
                {
                    dbConnection.Open();
                    string userQuery = "select * from Users where [Email] = '" + email + "'";
                    OleDbCommand command = new OleDbCommand(userQuery, dbConnection);
                    OleDbDataReader r = command.ExecuteReader();
                    User user = new User();
                    while (r.Read())
                    {
                        user.Email = r.GetString(0);
                        user.Password = r.GetString(1);
                        user.FullName = r.GetString(2);
                        user.Dob = r.GetString(3);
                        user.Phone = r.GetString(4);
                        user.City = r.GetString(5);
                        user.Smoking = r.GetBoolean(6);
                        user.Pet = r.GetBoolean(7);
                    }
                    if (user.Password != password)
                    {
                        MessageBox.Show("Incorrect password");
                        return false;
                    }
                    UserConnected = user;
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    dbConnection.Close();
                }
                return true;
            }
            else
            {
                MessageBox.Show("The email " + email + " is not registered to the system");
                return false;
            }
        }

        internal void addUser(string fullName, string email, string dob, string password, string city, string phone, bool smoking, bool pet)
        {
            if (!validMail(email, true))
            {
                MessageBox.Show("The email " + email + " already exist in the system");
                return;
            }
            User user = new User(email, password, fullName, dob, phone, city, smoking, pet);
            sendQuery(user, "user");
            sendMail(fullName, email);
        }

        private bool validMail(string email, bool addUser)
        {
            string mailQuery = "select [Email] from Users where [Email] = '" + email + "'";
            try
            {
                dbConnection.Open();
                OleDbCommand command = new OleDbCommand(mailQuery, dbConnection);
                OleDbDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    if (addUser)
                        return false;
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
            if (addUser)
                return true;
            return false;
        }

        private void sendQuery(object addToTables, string name)
        {
            name = name.ToLower();
            try
            {
                switch (name)
                {
                    case "user":
                        User user = (User)addToTables;
                        string sqlQueryU = "INSERT INTO Users ([Email], [Password], [Full name], [Dob], [Phone], [City], [Smoking], [Pet])" + " VALUES (@Email,@Password,@Fullname,@Dob,@Phone,@City,@Smoking,@Pet)";
                        OleDbCommand command = new OleDbCommand(sqlQueryU, dbConnection);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@Fullname", user.FullName);
                        command.Parameters.AddWithValue("@Dob", user.Dob);
                        command.Parameters.AddWithValue("@Phone", user.Phone);
                        command.Parameters.AddWithValue("@City", user.City);
                        command.Parameters.AddWithValue("@Smoking", user.Smoking);
                        command.Parameters.AddWithValue("@Pet", user.Pet);
                        dbConnection.Open();
                        command.ExecuteNonQuery();
                        break;
                    case "apartment":
                        ApartmentActivity apartment = (ApartmentActivity)addToTables;
                        string sqlQueryA = "INSERT INTO Activities ([ActivityNumber], [maxUsers], [City], [Address], [partners], [activityName], [activityType], [ammountToPay], [pendingList], [petAllowed], [isKosher], [smokingAllowed], [budget],[alcoholIncluded], [description], [sportType], [region], [destination], [startingDate],[approximateDuration], [carNeeded])" + " VALUES (@activityNumber,@maxUsers,@City,@Address,@partners,@activityName,@activityType,@ammountToPay,@pendingList,@petAllowed,@isKosher,@smokingAllowed,@budget,@alcoholIncluded,@description,@sportType,@region,@destination,@startingDate,@approximateDuration,@carNeeded)";
                        OleDbCommand commandA = new OleDbCommand(sqlQueryA, dbConnection);
                        commandA.Parameters.AddWithValue("@activityNumber", activityNumber++);
                        commandA.Parameters.AddWithValue("@maxUsers", apartment.MaxUsers);
                        commandA.Parameters.AddWithValue("@City", apartment.City);
                        commandA.Parameters.AddWithValue("@Address", apartment.Address);
                        commandA.Parameters.AddWithValue("@partners", apartment.MaxUsers);
                        commandA.Parameters.AddWithValue("@activityName", apartment.ActivityName);
                        commandA.Parameters.AddWithValue("@activityType", apartment.Type);
                        commandA.Parameters.AddWithValue("@ammountToPay", apartment.RentalFee);
                        commandA.Parameters.AddWithValue("@pendingList", "");
                        commandA.Parameters.AddWithValue("@petAllowed", apartment.PetFriendly);
                        commandA.Parameters.AddWithValue("@isKosher", apartment.IsKosher);
                        commandA.Parameters.AddWithValue("@smokingAllowed", apartment.IsSmokingFriendly);
                        commandA.Parameters.AddWithValue("@budget", apartment.RentalFee);
                        commandA.Parameters.AddWithValue("@alcoholIncluded", true);
                        commandA.Parameters.AddWithValue("@description", apartment.Description);
                        commandA.Parameters.AddWithValue("@sportType", "");
                        commandA.Parameters.AddWithValue("@region", "");
                        commandA.Parameters.AddWithValue("@destination", "");
                        commandA.Parameters.AddWithValue("@staringDate", "");
                        commandA.Parameters.AddWithValue("@approximateDuration", "");
                        commandA.Parameters.AddWithValue("@carNeeded", false);
                        dbConnection.Open();
                        commandA.ExecuteNonQuery();
                        break;
                    case "sport":
                        SportActivity sport = (SportActivity)addToTables;
                        string sqlQuery = "INSERT INTO Activities ([ActivityNumber], [maxUsers], [City], [Address], [partners], [activityName], [activityType], [ammountToPay], [pendingList], [petAllowed], [isKosher], [smokingAllowed], [budget],[alcoholIncluded], [description], [sportType], [region], [destination], [startingDate],[approximateDuration], [carNeeded])" + " VALUES (@activityNumber,@maxUsers,@City,@Address,@partners,@activityName,@activityType,@ammountToPay,@pendingList,@petAllowed,@isKosher,@smokingAllowed,@budget,@alcoholIncluded,@description,@sportType,@region,@destination,@startingDate,@approximateDuration,@carNeeded)";
                        OleDbCommand commandB = new OleDbCommand(sqlQuery, dbConnection);
                        commandB.Parameters.AddWithValue("@activityNumber", activityNumber++);
                        commandB.Parameters.AddWithValue("@maxUsers", sport.MaxUsers);
                        commandB.Parameters.AddWithValue("@City", sport.City);
                        commandB.Parameters.AddWithValue("@Address", sport.Address);
                        commandB.Parameters.AddWithValue("@partners", "");
                        commandB.Parameters.AddWithValue("@activityName", sport.ActivityName);
                        commandB.Parameters.AddWithValue("@activityType", sport.Type);
                        commandB.Parameters.AddWithValue("@ammountToPay", 0);
                        commandB.Parameters.AddWithValue("@pendingList", "");
                        commandB.Parameters.AddWithValue("@description", sport.Description);
                        commandB.Parameters.AddWithValue("@sportType", sport.SportType);
                        commandB.Parameters.AddWithValue("@region", "");
                        commandB.Parameters.AddWithValue("@destination", "");
                        commandB.Parameters.AddWithValue("@staringDate", "");
                        commandB.Parameters.AddWithValue("@approximateDuration", "");
                        dbConnection.Open();
                        commandB.ExecuteNonQuery();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void sendMail(string fullName, string email)
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
                MessageBox.Show("Email was sent to " + email);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}