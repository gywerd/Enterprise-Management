using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public ContactInfo()
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            phone = "";
            fax = "";
            mobile = "";
            email = "";
        }

        /// <summary>
        /// Constructor for adding ContactPerson
        /// </summary>
        /// <param name="phone">string</param>
        /// <param name="fax">string</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        public ContactInfo(string phone, string fax, string mobile, string email)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.phone = phone;
            this.fax = fax;
            this.mobile = mobile;
            this.email = email;
        }

        /// <summary>
        /// Constructor for adding ContactPerson from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="phone">string</param>
        /// <param name="fax">string</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        public ContactInfo(int id, string phone, string fax, string mobile, string email)
        {
            strConnection = Bizz.StrConnection;
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
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

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
        /// <summary>
        /// Method, that creates a new Contact in Db
        /// </summary>
        /// <param name="tempContactInfo">ContactInfo</param>
        /// <returns>int</returns>
        public int CreateContactInfoInDb(ContactInfo tempContactInfo)
        {
            int result = 0;
            int count = 0;
            bool dbAnswer = false;
            List<ContactInfo> tempContactInfoList = new List<ContactInfo>();
            //INSERT INTO [dbo].[ContactInfoList]([Phone], [Fax], [Mobile], [Email]) VALUES(<Phone, nvarchar(10),>, <Fax, nvarchar(10),>, <Mobile, nvarchar(10),>, <Email, nvarchar(10),>)
            string strSql = "INSERT INTO[dbo].[Contacts]([Phone], [Fax], [Mobile], [Email]) VALUES(" + tempContactInfo.Phone + ", '" + tempContactInfo.Fax + "', '" + tempContactInfo.Mobile + "', '" + tempContactInfo.Email + "')";
            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl ved forsøg på at oprette ny kontaktinfo.", "Databasefejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            tempContactInfoList = GetContactInfoList();
            count = tempContactInfoList.Count;
            result = tempContactInfoList[count - 1].Id;
            return result;
        }

        /// <summary>
        /// Method, that fetches a ContactInfo list
        /// </summary>
        /// <returns></returns>
        public List<ContactInfo> GetContactInfoList()
        {
            List<string> results = executor.ReadListFromDataBase("ContactInfoList");
            List<ContactInfo> contacts = new List<ContactInfo>();
            foreach (string result in results)
            {
                string[] resultArray = new string[5];
                resultArray = result.Split(';');
                ContactInfo contactInfo = new ContactInfo(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], resultArray[3], resultArray[4]);
                contacts.Add(contactInfo);
            }
            return contacts;
        }

        /// <summary>
        /// Method, that sets id, if id == 0
        /// </summary>
        public void SetId(int id)
        {
            try
            {
                if (this.id == 0 && id >= 1)
                {
                    this.id = id;
                }
            }
            catch (Exception)
            {
                this.id = 0;
            }
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
            if (mobile != null && mobile != "")
            {
                tempName += "Mobil:" + mobile + "\n";
            }
            if (email != null && email != "")
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
