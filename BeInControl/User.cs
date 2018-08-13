using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BicBizz
{
    public class User
    {
        #region Fields
        private string initials;
        private int? name;
        private string mobile;
        private string email;
        private int? jobDescription;
        private string passWord;
        private bool administrator;

        private static string strConnection;
        private Executor executor;

        public static Name CNA = new Name(strConnection);

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constryctor
        /// </summary>
        public User()
        {
            this.name = null;
            this.mobile = "";
            this.email = "";
            this.jobDescription = null;
            this.passWord = "1234";
            this.administrator = false;
        }

        //
        public User(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add user
        /// </summary>
        /// <param name="initials">string</param>
        /// <param name="name">int</param>
        /// <param name="contactInfo">int</param>
        /// <param name="jobDescription">int</param>
        /// <param name="passWord">string</param>
        /// <param name="admin">bool</param>
        public User(string initials, int? name, int? jobDescription, string password = "1234", string mobile="", string email="", bool admin = false)
        {
            this.initials = initials;
            this.name = name;
            this.mobile = mobile;
            this.email = email;
            this.jobDescription = jobDescription;
            this.passWord = password;
            this.administrator = admin;
        }

        /// <summary>
        /// Constructor to add user from Db to list
        /// </summary>
        /// <param name="initials">string</param>
        /// <param name="name">int</param>
        /// <param name="contactInfo">int</param>
        /// <param name="jobDescription">int</param>
        /// <param name="passWord">string</param>
        /// <param name="admin">bool</param>
        public User(string initials, int name, string mobile, string email, int jobDescription, string passWord, bool admin)
        {
            this.initials = initials;
            this.name = name;
            this.mobile = mobile;
            this.email = email;
            this.jobDescription = jobDescription;
            this.passWord = passWord;
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
            string result = GetName(name);
            return result;
        }

        public string GetName(int? id)
        {
            string result = "";
            List<Name> names = CNA.GetNameList();
            foreach (Name name2 in names)
            {
                if (name2.NameId.Equals(id))
                {
                    result = name2.ToString();
                    return result;
                }
            }
            return result;
        }

        public List<User> GetUserList()
        {
            List<string> results = executor.ReadListFromDataBase("Users");
            List<User> users = new List<User>();
            foreach (string result in results)
            {
                string[] resultArray = new string[7];
                resultArray = result.Split(';');
                User user = new User(resultArray[0], Convert.ToInt32(resultArray[1]), resultArray[2], resultArray[3], Convert.ToInt32(resultArray[4]), resultArray[5], Convert.ToBoolean(resultArray[6]));
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

        public string Mobile
        {
            get => mobile;
            set
            {
                try
                {
                    if (value != null)
                    {
                        mobile = value;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                try
                {
                    if (value != null)
                    {
                        email = value;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int? Name
        {
            get => name;
            set
            {
                try
                {
                    if (value != null && value >= 0)
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

        public int? JobDescription
        {
            get => jobDescription;
            set
            {
                try
                {
                    if (value != null)
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
