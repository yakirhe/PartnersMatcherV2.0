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
            #region enter Activities
            //sport activity
            Activity sport1 = new SportActivity("Haifa", "football", 12, new List<User>(), "football at sportek", "sport", 0, new List<User>());
            Activity sport2 = new SportActivity("Beer sheva", "basketball", 8, new List<User>(), "basketball at the sport center", "sport", 0, new List<User>());
            Activity sport3 = new SportActivity("Tel aviv", "tennis", 2, new List<User>(), "tennis at the sport center", "sport", 0, new List<User>());
            Activity sport4 = new SportActivity("jerusalem", "swimming", 6, new List<User>(), "swimming at the sport center", "sport", 0, new List<User>());
            Activity sport5 = new SportActivity("Tel aviv", "running", 5, new List<User>(), "running at the stadium", "sport", 0, new List<User>());
            //date activity
            Activity date1 = new DateActivity("Beer Sheva", 200, true, true, "Date at the benji", new List<User>(), "date at the benji", "date", 0, new List<User>());
            Activity date2 = new DateActivity("Jerusalem", 50, true, true, "date at the coffix", new List<User>(), "date at the coffix", "date", 0, new List<User>());
            Activity date3 = new DateActivity("Tel aviv", 100, true, true, "date at the cinema", new List<User>(), "date at the cinema", "date", 0, new List<User>());
            Activity date4 = new DateActivity("Haifa", 150, true, true, "date at the nargila bar", new List<User>(), "date at the nargila bar", "date", 0, new List<User>());
            Activity date5 = new DateActivity("Tel aviv", 250, true, true, "date at the dude resturant", new List<User>(), "date at the dude resturant", "date", 0, new List<User>());
            //trip activity
            Activity trip1 = new TripActivity("Africa", "Madagascar", "12.12", "24 days", false, 4, new List<User>(), "Jungles trip", "trip", 11000, new List<User>());
            Activity trip2 = new TripActivity("Caribbean", "Cayman islands & Jamaica", "7.4", "12 days", false, 7, new List<User>(), "Trip to the caribbean sea", "trip", 14000, new List<User>());
            Activity trip3 = new TripActivity("Australia", "beach surfing in australia", "7.4", "12 days", false, 7, new List<User>(), "beach surfing in australia", "trip", 14000, new List<User>());
            Activity trip4 = new TripActivity("Far east", "meditation in india", "7.4", "12 days", false, 7, new List<User>(), "meditation in india", "trip", 14000, new List<User>());
            Activity trip5 = new TripActivity("South america", "travel to iguasu falls in argentina and brazil", "7.4", "12 days", false, 7, new List<User>(), "travel to iguasu falls in argentina and brazil", "trip", 14000, new List<User>());
            Activity trip6 = new TripActivity("North america", "travel to niagra falls in U.S.A", "7.4", "12 days", false, 7, new List<User>(), "travel to niagra falls in U.S.A", "trip", 14000, new List<User>());
            Activity trip7 = new TripActivity("Scandinavia", "travel to the north glow", "7.4", "12 days", false, 7, new List<User>(), "travel to the north glow", "trip", 14000, new List<User>());
            Activity trip8 = new TripActivity("Middle east", "travel to jorden", "7.4", "12 days", false, 7, new List<User>(), "travel to jorden", "trip", 14000, new List<User>());
            //rent apatment activity
            Activity apartment1 = new ApartmentActivity("Tel Aviv", 2200, true, false, true, 3, new List<User>(), "4 rooms apartment", "apartment", 0, new List<User>());
            Activity apartment2 = new ApartmentActivity("Jerusalem", 1600, true, true, false, 3, new List<User>(), "4 rooms apartment + 20ft balcony", "apartment", 0, new List<User>());
            Activity apartment3 = new ApartmentActivity("Haifa", 1600, true, true, false, 3, new List<User>(), "4 rooms apartment + 20ft balcony", "apartment", 0, new List<User>());
            Activity apartment4 = new ApartmentActivity("Beer sheva", 1600, true, true, false, 3, new List<User>(), "4 rooms apartment + 20ft balcony", "apartment", 0, new List<User>());
            Activity apartment5 = new ApartmentActivity("Tel aviv", 3000, true, true, false, 3, new List<User>(), "4 rooms apartment + 20ft balcony", "apartment", 0, new List<User>());
            activityList["sport"].Add(sport1);
            activityList["sport"].Add(sport2);
            activityList["sport"].Add(sport3);
            activityList["sport"].Add(sport4);
            activityList["sport"].Add(sport5);
            activityList["date"].Add(date1);
            activityList["date"].Add(date2);
            activityList["date"].Add(date3);
            activityList["date"].Add(date4);
            activityList["date"].Add(date5);
            activityList["trip"].Add(trip1);
            activityList["trip"].Add(trip2);
            activityList["trip"].Add(trip3);
            activityList["trip"].Add(trip4);
            activityList["trip"].Add(trip5);
            activityList["trip"].Add(trip6);
            activityList["trip"].Add(trip7);
            activityList["trip"].Add(trip8);
            activityList["apartment"].Add(apartment1);
            activityList["apartment"].Add(apartment2);
            activityList["apartment"].Add(apartment3);
            activityList["apartment"].Add(apartment4);
            activityList["apartment"].Add(apartment5);

            #endregion
        }

        private void createDb()
        {
            //connect to db
            string curPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string relPath = @"\Db\PMDB.accdb";
            string fullPath = curPath + relPath;
            //connect to db
            dbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullPath + "; Persist Security Info=False");
            //checkRecords();
            //insertRecord();
        }

        private void insertRecord()
        {
            dbConnection.Open();
            string sqlQuery = "INSERT INTO Users ([Email], [Password], [Full name], [Dob], [Phone], [City], [Smoking], [Pet])" + " VALUES (@Email,@Password,@Fullname,@Dob,@Phone,@City,@Smoking,@Pet)";
            OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection);
            command.Parameters.AddWithValue("@Username", "dsf");
            command.Parameters.AddWithValue("@Password", "fgd");
            command.Parameters.AddWithValue("@Fullname", "dfg");
            command.Parameters.AddWithValue("@Dob", "dfg");
            command.Parameters.AddWithValue("@Phone", "sgs");
            command.Parameters.AddWithValue("@City", "fgd");
            command.Parameters.AddWithValue("@Smoking", true);
            command.Parameters.AddWithValue("@Pet", false);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        private void checkRecords()
        {
            dbConnection.Open();
            string query = "SELECT * FROM Users";
            OleDbCommand cmd = new OleDbCommand(query, dbConnection);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MessageBox.Show(reader.GetString(0));
            }
            dbConnection.Close();
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