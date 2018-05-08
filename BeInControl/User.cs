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
        private string strConnection;
        private int userId;
        private string initials;
        private int name;
        private int contactInfo;
        private int jobDescription;
        private string passWord;
        private bool administrator;

        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constryctor
        /// </summary>
        public User() { }

        //
        public User(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add new user
        /// </summary>
        /// <param name="initials">string</param>
        /// <param name="name">int</param>
        /// <param name="contactInfo">int</param>
        /// <param name="jobDescription">int</param>
        /// <param name="passWord">string</param>
        /// <param name="admin">bool</param>
        public User(string initials, int name, int contactInfo, int jobDescription, string password, bool admin = false)
        {
            this.initials = initials;
            this.name = name;
            this.contactInfo = contactInfo;
            this.jobDescription = jobDescription;
            this.passWord = password;
            this.administrator = admin;
        }

        /// <summary>
        /// Constructor to add user from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="initials">string</param>
        /// <param name="name">int</param>
        /// <param name="contactInfo">int</param>
        /// <param name="jobDescription">int</param>
        /// <param name="passWord">string</param>
        /// <param name="admin">bool</param>
        public User(int id, string initials, int name, int contactInfo, int jobDescription, string passWord, bool admin)
        {
            this.userId = id;
            this.initials = initials;
            this.name = name;
            this.contactInfo = contactInfo;
            this.jobDescription = jobDescription;
            this.passWord = passWord;
            this.administrator = admin;
        }
        #endregion

        #region Methods
        public List<User> GetUserList()
        {
            List<string> results = executor.ReadListFromDataBase("Users");
            List<User> users = new List<User>();
            foreach (string result in results)
            {
                string[] resultArray = new string[7];
                resultArray = result.Split(';');
                User user = new User(Convert.ToInt32(resultArray[0]), resultArray[1], Convert.ToInt32(resultArray[2]), Convert.ToInt32(resultArray[3]), Convert.ToInt32(resultArray[4]), resultArray[5], Convert.ToBoolean(resultArray[6]));
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

        #endregion

        #region Fields
        public int UserId { get => userId;  }
        public string Initials { get => initials; set => initials = value; }
        public int Name { get => name; set => name = value;  }
        public int ContactInfo { get => contactInfo; set => contactInfo = value;  }
        public int JobDescription { get => jobDescription;  set => jobDescription = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public bool Administrator { get => administrator;  }
        #endregion
    }
}
