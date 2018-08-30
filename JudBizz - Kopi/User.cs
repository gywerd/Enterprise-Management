using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JudBizz
{
    public class User
    {
        #region Fields
        private int id;
        private string initials;
        private string name;
        private string passWord;
        private int contactInfo;
        private int jobDescription;
        private bool administrator;

        private static string strConnection;
        private Executor executor;


        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constryctor
        /// </summary>
        public User()
        {
        }

        //
        public User(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
            this.name = "";
            this.passWord = "1234";
            this.contactInfo = 0;
            this.jobDescription = 0;
            this.administrator = false;
        }

        /// <summary>
        /// Constructor to add user
        /// </summary>
        /// <param name="initials">string</param>
        /// <param name="name">string</param>
        /// <param name="contactInfo">int</param>
        /// <param name="jobDescription">int</param>
        /// <param name="passWord">string</param>
        /// <param name="admin">bool</param>
        public User(string initials, string name, string password = "1234", int contactInfo = 0, int jobDescription = 0, bool admin = false)
        {
            this.initials = initials;
            this.name = name;
            this.passWord = password;
            this.contactInfo = contactInfo;
            this.jobDescription = jobDescription;
            this.administrator = admin;
        }

        /// <summary>
        /// Constructor to add user from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="initials">string</param>
        /// <param name="name">string</param>
        /// <param name="contactInfo">int</param>
        /// <param name="jobDescription">int</param>
        /// <param name="passWord">string</param>
        /// <param name="admin">bool</param>
        public User(int id, string initials, string name, string password, int contactInfo, int jobDescription, bool admin)
        {
            this.id = id;
            this.initials = initials;
            this.name = name;
            this.passWord = password;
            this.contactInfo = contactInfo;
            this.jobDescription = jobDescription;
            this.administrator = admin;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method returns user name as string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string result = name + " (" + initials + ")";
            return result;
        }

        public List<User> GetUsers()
        {
            List<string> results = executor.ReadListFromDataBase("Users");
            List<User> users = new List<User>();
            foreach (string result in results)
            {
                string[] resultArray = new string[7];
                resultArray = result.Split(';');
                User user = new User(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], resultArray[3], Convert.ToInt32(resultArray[4]), Convert.ToInt32(resultArray[5]), Convert.ToBoolean(resultArray[6]));
                users.Add(user);
            }
            return users;
        }

        public bool ChangePassword(string oldPassWord, string newPassWord)
        {
            if (passWord == oldPassWord)
            {
                passWord = newPassWord;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ToggleAdministrator()
        {
            if (administrator)
            {
                administrator = false;
            }
            else
            {
                administrator = true;
            }
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public string Initials
        {
            get => initials;
            set
            {
                try
                {
                    if (value != null && initials == null)
                    {
                        initials = value;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                try
                {
                    if (value != null)
                    {
                        name = value;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int ContactInfo
        {
            get => contactInfo;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        contactInfo = value;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int JobDescription
        {
            get => jobDescription;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        jobDescription = value;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string PassWord { get => passWord; }

        public bool Administrator { get => administrator; }
        #endregion
    }
}
