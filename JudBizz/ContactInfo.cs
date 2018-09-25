using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class ContactInfo
    {
        #region Fields
        int id;
        string phone;
        string fax;
        string mobile;
        string email;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public ContactInfo() { }

        /// <summary>
        /// Constructor for access to db methods
        /// </summary>
        /// <param name="strCon"></param>
        public ContactInfo(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            phone = "";
            fax = "";
            mobile = "";
            email = "";
        }

        /// <summary>
        /// Constructor for adding ContactPerson
        /// </summary>
        /// <param name="phone">int</param>
        /// <param name="fax">int</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        public ContactInfo(string phone, string fax, string mobile, string email)
        {
            this.phone = phone;
            this.fax = fax;
            this.mobile = mobile;
            this.email = email;
        }

        /// <summary>
        /// Constructor for adding ContactPerson from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="phone">int</param>
        /// <param name="fax">int</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        public ContactInfo(int id, string phone, string fax, string mobile, string email)
        {
            this.id = id;
            this.phone = phone;
            this.fax = fax;
            this.mobile = mobile;
            this.email = email;
        }

        /// <summary>
        /// Constructor for adding ContactPerson from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="phone">int</param>
        /// <param name="fax">int</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        public ContactInfo(string strCon, int id, string phone, string fax, string mobile, string email)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.phone = phone;
            this.fax = fax;
            this.mobile = mobile;
            this.email = email;
        }

        /// <summary>
        /// Constructor that accepts data from existing Contact
        /// </summary>
        /// <param name="contactInfo">Contact</param>
        public ContactInfo(ContactInfo contactInfo)
        {
            if (contactInfo != null)
            {
                this.id = contactInfo.Id;
                this.phone = contactInfo.Phone;
                this.fax = contactInfo.Fax;
                this.mobile = contactInfo.Mobile;
                this.email = contactInfo.Email;
            }
            else
            {
                id = 0;
                phone = "";
                fax = "";
                mobile = "";
                email = "";
            }
        }

        #endregion

        #region Methods
        public List<ContactInfo> GetContactInfoList(string strCon)
        {
            List<string> results = executor.ReadListFromDataBase("ContactInfoList");
            List<ContactInfo> contacts = new List<ContactInfo>();
            foreach (string result in results)
            {
                string[] resultArray = new string[5];
                resultArray = result.Split(';');
                ContactInfo contactInfo = new ContactInfo(strCon, Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], resultArray[3], resultArray[4]);
                contacts.Add(contactInfo);
            }
            return contacts;
        }

        /// <summary>
        /// Method, that returns main info as string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string tempName = "Tlf: " + phone + " / Fax:" + fax + " / Mobil:" + mobile + " / Email:" + email;
            return tempName;
        }

        /// <summary>
        /// Method, that returns main info as string with multiple lines
        /// </summary>
        /// <returns>string</returns>
        public string ToLongString()
        {
            string tempName = "";
            if (phone != null && phone != "")
            {
                tempName += "Tlf: " + phone + "\n";
            }
            if (fax != null && fax != "")
            {
                tempName += "Fax:" + fax + "\n";
            }
            if (fax != null && fax != "")
            {
                tempName += "Mobil:" + mobile + "\n";
            }
            if (fax != null && fax != "")
            {
                tempName += "Email:" + email;
            }
            if (tempName == "")
            {
                tempName = "Ingen Kontaktinfo";
            }
            return tempName;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public string Phone
        {
            get => phone;
            set
            {
                try
                {
                    if (value != null)
                    {
                        phone = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string Fax
        {
            get => fax;
            set
            {
                try
                {
                    if (value != null)
                    {
                        fax = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
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
                catch (Exception ex)
                {

                    throw ex;
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
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        #endregion

    }
}
