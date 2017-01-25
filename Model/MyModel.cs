using PartnersMatcher.View.Controls;
using PartnersMatcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private static int activityNumber;

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

        #endregion Properties

        private OleDbConnection dbConnection;
        private MyViewModel vm;

        public MyModel()
        {
            usersDict = new Dictionary<string, User>();
            createDb();
            loadActivities();
        }

        internal void sendRequest(User userReq, Activity activity)
        {
            int activityNum = activity.ActivityNumber;
            try
            {
                dbConnection.Open();
                string findActivity = "select * from Activities where [ActivityNumber] =" + activityNum;
                OleDbCommand command = new OleDbCommand(findActivity, dbConnection);
                OleDbDataReader r = command.ExecuteReader();
                r.Read();
                string pendingStr = r.GetString(8);
                pendingStr = pendingStr + " " + userReq.Email;
                string updateActivity = "update Activities set pendingList = @pending where [ActivityNumber] =" + activityNum;
                OleDbCommand cmd = new OleDbCommand(updateActivity, dbConnection);
                cmd.Parameters.AddWithValue("@pending", pendingStr);
                Console.WriteLine(cmd.ExecuteNonQuery());

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

        public void acceptRequests(List<string> acceptRequests)
        {
            foreach (string str in acceptRequests)
            {
                //remove from pending list
                string[] acceptRequestStr = str.Split(' ');
                string removeFromPendingList = acceptRequestStr[0];
                string activityNum = acceptRequestStr[6];
                remove(removeFromPendingList, activityNum);
                //add to partners
                addToPartners(removeFromPendingList, activityNum);
            }
        }

        private void addToPartners(string acceptFromPendingList, string activityNum)
        {
            try
            {
                string readQuery = "select * from Activities where [ActivityNumber] = " + activityNum;
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand(readQuery, dbConnection);
                OleDbDataReader r = cmd.ExecuteReader();
                r.Read();
                string partnersList = r.GetString(4);
                int maxPartners = r.GetInt32(1);
                if (partnersList.Trim().Split(' ').Length >= maxPartners)
                {
                    MessageBox.Show("We are sorry but the activity is already full");
                    return;
                }
                partnersList += " " + acceptFromPendingList;
                //write new partners
                string upQuery = "update Activities set partners = @partners where[ActivityNumber] = " + activityNum;
                OleDbCommand upCom = new OleDbCommand(upQuery, dbConnection);
                upCom.Parameters.AddWithValue("@partners", partnersList);
                upCom.ExecuteNonQuery();
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
                    int maxUsers;
                    int actNumber = r.GetInt32(0);
                    string city, address, partners, activityName;
                    string activityType = r.GetString(6);
                    maxUsers = r.GetInt32(1);
                    city = r.GetString(2);
                    address = r.GetString(3);
                    partners = r.GetString(4);
                    activityName = r.GetString(5);
                    string pendingListStr = r.GetString(8);
                    bool smokingAllowed = r.GetBoolean(11);
                    string description = r.GetString(14);
                    int budget = r.GetInt32(12);
                    string emailOfManager = r.GetString(21);
                    User user = getUser(emailOfManager);
                    List<User> partnersList = getPendingList(partners);
                    List<User> pendingList = getPendingList(pendingListStr);
                    switch (activityType)
                    {
                        case "Apartment":
                            int ammountToPay = r.GetInt32(7);
                            bool petAllowed = r.GetBoolean(9);
                            bool isKosher = r.GetBoolean(10);
                            //convert partners from string to list
                            Activity activity = new ApartmentActivity(user, city, address, budget, petAllowed, isKosher, smokingAllowed, maxUsers, partnersList, activityName, activityType, 0, description, pendingList);
                            activity.ActivityNumber = actNumber;
                            activityList[activityType.ToLower()].Add(activity);
                            break;

                        case "sport":
                            string sportType = r.GetString(15);
                            Activity sportActivity = new SportActivity(city, address, sportType, maxUsers, partnersList, activityName, activityType, 0, pendingList, description, user);
                            sportActivity.ActivityNumber = actNumber;
                            activityList[activityType.ToLower()].Add(sportActivity);
                            break;

                        case "date":
                            bool alcohol = r.GetBoolean(13);
                            Activity dateActivity = new DateActivity(address, city, budget, smokingAllowed, alcohol, description, partnersList, activityName, activityType, budget, pendingList, user);
                            dateActivity.ActivityNumber = actNumber;
                            activityList[activityType.ToLower()].Add(dateActivity);
                            break;

                        case "trip":
                            string region = r.GetString(16);
                            string destination = r.GetString(17);
                            string startingDate = r.GetString(18);
                            string approximateDuration = r.GetString(19);
                            bool carNeeded = r.GetBoolean(20);
                            Activity tripActivity = new TripActivity(address, city, budget, region, destination, startingDate, approximateDuration, carNeeded, maxUsers, partnersList, activityName, activityType, budget, pendingList, description, user);
                            tripActivity.ActivityNumber = actNumber;
                            activityList[activityType.ToLower()].Add(tripActivity);
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

        public void ignoreFromPendingList(List<string> ignoreRequest)
        {
            foreach (string str in ignoreRequest)
            {
                string[] ignoreRequestStr = str.Split(' ');
                string removeFromPendingList = ignoreRequestStr[0];
                string activityNum = ignoreRequestStr[6];
                remove(removeFromPendingList, activityNum);

            }
        }

        private void remove(string removeFromPendingList, string activityNum)
        {
            try
            {
                string readQuery = "select * from Activities where [ActivityNumber] = " + activityNum;
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand(readQuery, dbConnection);
                OleDbDataReader r = cmd.ExecuteReader();
                r.Read();
                string newPendingList = "";
                string pendingList = r.GetString(8);
                string[] pendingListArr = pendingList.Split(' ');
                for (int i = 0; i < pendingListArr.Length; i++)
                {
                    if (pendingListArr[i] != removeFromPendingList)
                    {
                        newPendingList += pendingListArr[i];
                    }
                }
                //write the new pending list
                string upQuery = "update Activities set pendingList = @pending where[ActivityNumber] = " + activityNum;
                OleDbCommand upCom = new OleDbCommand(upQuery, dbConnection);
                upCom.Parameters.AddWithValue("@pending", newPendingList);
                upCom.ExecuteNonQuery();
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

        private List<User> getPendingList(string pendingListStr)
        {
            List<User> pendings = new List<User>();
            foreach (string email in pendingListStr.Split(' '))
            {
                pendings.Add(getUser(email));
            }
            return pendings;
        }

        private void initActivities()
        {
            activityList = new Dictionary<string, List<Activity>>();
            activityList["apartment"] = new List<Activity>();
            activityList["sport"] = new List<Activity>();
            activityList["date"] = new List<Activity>();
            activityList["trip"] = new List<Activity>();
        }

        public void addActivity(int numOfPartners, string city, string address, string partners, string activityName, string activityType, string rentalFee, string pendinglist, bool petFriendly, bool isKosher, bool smokingFriendly, int budget, bool alcoholIncluded, string description, string sportType, string region, string destination, string startingDate, string approximateDuration, bool carNeeded, User activityManager)
        {
            List<User> sendNull = new List<User>();
            activityType = activityType.ToLower();
            switch (activityType)
            {
                case "apartment":
                    Activity newApartment = new ApartmentActivity(activityManager, city, address, Int32.Parse(rentalFee), petFriendly, isKosher, smokingFriendly, Int32.Parse(partners), sendNull, activityName, activityType, Int32.Parse(rentalFee), description, sendNull);
                    sendQuery(newApartment, "apartment");
                    break;

                case "sport":
                    Activity newSport = new SportActivity(city, address, sportType, numOfPartners, sendNull, activityName, activityType, 0, null, description, activityManager);
                    sendQuery(newSport, "sport");
                    break;

                case "date":
                    Activity newDate = new DateActivity(address, city, budget, smokingFriendly, alcoholIncluded, description, sendNull, activityName, activityType, budget, sendNull, activityManager);
                    sendQuery(newDate, "date");
                    break;

                case "trip":
                    Activity newTrip = new TripActivity(address, city, budget, region, destination, startingDate, approximateDuration, carNeeded, numOfPartners, sendNull, activityName, activityType, budget, sendNull, description, activityManager);
                    sendQuery(newTrip, "trip");
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

        private User getUser(string email)
        {
            User user = new User();
            try
            {
                if (dbConnection.State.ToString().ToLower() != "open")
                    dbConnection.Open();
                string userQuery = "select * from Users where [Email] = '" + email + "'";
                OleDbCommand command = new OleDbCommand(userQuery, dbConnection);
                OleDbDataReader r = command.ExecuteReader();
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return user;
        }

        public bool signIn(string email, string password, MyViewModel vm)
        {
            this.vm = vm;
            if (validMail(email, false))
            {
                try
                {
                    if (dbConnection.State.ToString().ToLower() != "open")
                    {
                        dbConnection.Open();
                    }
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
                    checkRequest(email);
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

        private void checkRequest(string email)
        {
            bool thereAreReq = false;
            Dictionary<string, ObservableCollection<string>> requestsDict = new Dictionary<string, ObservableCollection<string>>();
            string findActivities = "select * from Activities where [ActivityManager] = '" + email + "'";
            OleDbCommand oleDbC = new OleDbCommand(findActivities, dbConnection);
            OleDbDataReader r = oleDbC.ExecuteReader();
            int counter = 0;
            while (r.Read())
            {
                ObservableCollection<string> requests = new ObservableCollection<string>();
                string pendingStr = r.GetString(8);
                if (pendingStr.Length != 0)
                {
                    thereAreReq = true;
                    foreach (string req in pendingStr.Trim().Split(' '))
                    {
                        requests.Add(req);
                    }
                    counter += requests.Count;
                    requestsDict["Activity number " + r.GetInt32(0).ToString() + " of the type " + r.GetString(6)] = requests;
                }
            }
            if (thereAreReq)
            {
                openReqWindow(requestsDict, counter);
            }
        }

        private void openReqWindow(Dictionary<string, ObservableCollection<string>> requestsDict, int counter)
        {
            JoinRequests jr = new JoinRequests(requestsDict, counter, vm);
            jr.Show();
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
                string sqlQuery = "INSERT INTO Activities ([ActivityNumber], [maxUsers], [City], [Address], [partners], [activityName], [activityType], [ammountToPay], [pendingList], [petAllowed], [isKosher], [smokingAllowed], [budget],[alcoholIncluded], [description], [sportType], [region], [destination], [startingDate], [approximateDuration], [carNeeded], [ActivityManager])" + " VALUES (@activityNumber,@maxUsers,@City,@Address,@partners,@activityName,@activityType,@ammountToPay,@pendingList,@petAllowed,@isKosher,@smokingAllowed,@budget,@alcoholIncluded,@description,@sportType,@region,@destination,@startingDate,@approximateDuration,@carNeeded,@activityManager)";
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
                        OleDbCommand commandA = new OleDbCommand(sqlQuery, dbConnection);
                        commandA.Parameters.AddWithValue("@activityNumber", activityNumber++);
                        commandA.Parameters.AddWithValue("@maxUsers", apartment.MaxUsers);
                        commandA.Parameters.AddWithValue("@City", apartment.City);
                        commandA.Parameters.AddWithValue("@Address", apartment.Address);
                        commandA.Parameters.AddWithValue("@partners", "");
                        commandA.Parameters.AddWithValue("@activityName", apartment.ActivityName);
                        commandA.Parameters.AddWithValue("@activityType", apartment.Type);
                        commandA.Parameters.AddWithValue("@ammountToPay", apartment.RentalFee);
                        commandA.Parameters.AddWithValue("@pendingList", "");
                        commandA.Parameters.AddWithValue("@petAllowed", apartment.PetFriendly);
                        commandA.Parameters.AddWithValue("@isKosher", apartment.IsKosher);
                        commandA.Parameters.AddWithValue("@smokingAllowed", apartment.IsSmokingFriendly);
                        commandA.Parameters.AddWithValue("@budget", apartment.RentalFee);
                        commandA.Parameters.AddWithValue("@alcoholIncluded", false);
                        commandA.Parameters.AddWithValue("@description", apartment.Description);
                        commandA.Parameters.AddWithValue("@sportType", "");
                        commandA.Parameters.AddWithValue("@region", "");
                        commandA.Parameters.AddWithValue("@destination", "");
                        commandA.Parameters.AddWithValue("@staringDate", "");
                        commandA.Parameters.AddWithValue("@approximateDuration", "");
                        commandA.Parameters.AddWithValue("@carNeeded", false);
                        commandA.Parameters.AddWithValue("@activityManager", apartment.ActivityManager.Email);
                        dbConnection.Open();
                        commandA.ExecuteNonQuery();
                        break;

                    case "sport":
                        SportActivity sport = (SportActivity)addToTables;
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
                        commandB.Parameters.AddWithValue("@petAllowed", false);
                        commandB.Parameters.AddWithValue("@isKosher", false);
                        commandB.Parameters.AddWithValue("@smokingAllowed", false);
                        commandB.Parameters.AddWithValue("@budget", 0);
                        commandB.Parameters.AddWithValue("@alcoholIncluded", true);
                        commandB.Parameters.AddWithValue("@description", sport.Description);
                        commandB.Parameters.AddWithValue("@sportType", sport.SportType);
                        commandB.Parameters.AddWithValue("@region", "");
                        commandB.Parameters.AddWithValue("@destination", "");
                        commandB.Parameters.AddWithValue("@staringDate", "");
                        commandB.Parameters.AddWithValue("@approximateDuration", "");
                        commandB.Parameters.AddWithValue("@carNeeded", false);
                        commandB.Parameters.AddWithValue("@activityManager", sport.ActivityManager.Email);
                        dbConnection.Open();
                        commandB.ExecuteNonQuery();
                        break;

                    case "date":
                        DateActivity date = (DateActivity)addToTables;
                        OleDbCommand commandD = new OleDbCommand(sqlQuery, dbConnection);
                        commandD.Parameters.AddWithValue("@activityNumber", activityNumber++);
                        commandD.Parameters.AddWithValue("@maxUsers", date.MaxUsers);
                        commandD.Parameters.AddWithValue("@City", date.City);
                        commandD.Parameters.AddWithValue("@Address", date.Address);
                        commandD.Parameters.AddWithValue("@partners", "");
                        commandD.Parameters.AddWithValue("@activityName", date.ActivityName);
                        commandD.Parameters.AddWithValue("@activityType", date.Type);
                        commandD.Parameters.AddWithValue("@ammountToPay", 0);
                        commandD.Parameters.AddWithValue("@pendingList", "");
                        commandD.Parameters.AddWithValue("@petAllowed", false);
                        commandD.Parameters.AddWithValue("@isKosher", false);
                        commandD.Parameters.AddWithValue("@smokingAllowed", date.IsSmokingFriendly);
                        commandD.Parameters.AddWithValue("@budget", date.Budget);
                        commandD.Parameters.AddWithValue("@alcoholIncluded", date.IncludeAlcohol);
                        commandD.Parameters.AddWithValue("@description", date.Description);
                        commandD.Parameters.AddWithValue("@sportType", "");
                        commandD.Parameters.AddWithValue("@region", "");
                        commandD.Parameters.AddWithValue("@destination", "");
                        commandD.Parameters.AddWithValue("@staringDate", "");
                        commandD.Parameters.AddWithValue("@approximateDuration", "");
                        commandD.Parameters.AddWithValue("@carNeeded", false);
                        commandD.Parameters.AddWithValue("@activityManager", date.ActivityManager.Email);
                        dbConnection.Open();
                        commandD.ExecuteNonQuery();
                        break;

                    case "trip":
                        TripActivity trip = (TripActivity)addToTables;
                        OleDbCommand commandT = new OleDbCommand(sqlQuery, dbConnection);
                        commandT.Parameters.AddWithValue("@activityNumber", activityNumber++);
                        commandT.Parameters.AddWithValue("@maxUsers", trip.MaxUsers);
                        commandT.Parameters.AddWithValue("@City", trip.City);
                        commandT.Parameters.AddWithValue("@Address", trip.Address);
                        commandT.Parameters.AddWithValue("@partners", "");
                        commandT.Parameters.AddWithValue("@activityName", trip.ActivityName);
                        commandT.Parameters.AddWithValue("@activityType", trip.Type);
                        commandT.Parameters.AddWithValue("@ammountToPay", trip.Budget);
                        commandT.Parameters.AddWithValue("@pendingList", "");
                        commandT.Parameters.AddWithValue("@petAllowed", false);
                        commandT.Parameters.AddWithValue("@isKosher", false);
                        commandT.Parameters.AddWithValue("@smokingAllowed", false);
                        commandT.Parameters.AddWithValue("@budget", trip.Budget);
                        commandT.Parameters.AddWithValue("@alcoholIncluded", false);
                        commandT.Parameters.AddWithValue("@description", trip.Description);
                        commandT.Parameters.AddWithValue("@sportType", "");
                        commandT.Parameters.AddWithValue("@region", trip.Region);
                        commandT.Parameters.AddWithValue("@destination", trip.Destination);
                        commandT.Parameters.AddWithValue("@staringtrip", "");
                        commandT.Parameters.AddWithValue("@approximateDuration", "");
                        commandT.Parameters.AddWithValue("@carNeeded", false);
                        commandT.Parameters.AddWithValue("@activityManager", trip.ActivityManager.Email);
                        dbConnection.Open();
                        commandT.ExecuteNonQuery();
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