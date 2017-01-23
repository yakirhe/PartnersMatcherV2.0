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
        }




        public void addActivity(int numOfPartners, string city, string address, string partners, string activityName, string activityType, string rentalFee, string pendinglist, bool petFriendly, bool isKosher, bool smokingFriendly, int budget, bool alcoholIncluded, string description, string sportType, string region, string destination, string startingDate, string approximateDuration, bool carNeeded)
        {
            switch (activityType)
            {
                case "Apartment":
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
            sendQuery(user);
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

        private void sendQuery(User user)
        {
            try
            {
                string sqlQuery = "INSERT INTO Users ([Email], [Password], [Full name], [Dob], [Phone], [City], [Smoking], [Pet])" + " VALUES (@Email,@Password,@Fullname,@Dob,@Phone,@City,@Smoking,@Pet)";
                OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection);
                command.Parameters.AddWithValue("@Username", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Fullname", user.FullName);
                command.Parameters.AddWithValue("@Dob", user.Dob);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@City", user.City);
                command.Parameters.AddWithValue("@Smoking", user.Smoking);
                command.Parameters.AddWithValue("@Pet", user.Pet);
                dbConnection.Open();
                command.ExecuteNonQuery();
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