using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JudBizz
{
    public class Contact
    {
        #region Fields
        private int id;
        protected string legalEntity;
        protected string name;
        protected string area;
        protected int contactInfo;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Contact() { }

        /// <summary>
        /// Constructor for access to db methods
        /// </summary>
        /// <param name="strCon"></param>
        public Contact(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
            legalEntity = "";
            name = "";
            area = "";
            contactInfo = 0;
        }

        /// <summary>
        /// Constructor for adding ContactPerson
        /// </summary>
        /// <param name="legalEntity">int</param>
        /// <param name="name">int</param>
        /// <param name="description">string</param>
        /// <param name="email">string</param>
        /// <param name="mobile">string</param>
        public Contact(string legalEntity, string name, string description = "", int contactInfo = 0)
        {
            this.legalEntity = legalEntity;
            this.name = name;
            this.area = description;
            this.contactInfo = contactInfo;
        }

        /// <summary>
        /// Constructor for adding ContactPerson from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="legalEntityId">int</param>
        /// <param name="name">int</param>
        /// <param name="description">string</param>
        /// <param name="email">string</param>
        /// <param name="mobile">string</param>
        public Contact(int id, string legalEntity, string name, string description, int contactInfo)
        {
            this.id = id;
            this.legalEntity = legalEntity;
            this.name = name;
            this.area = description;
            this.contactInfo = contactInfo;
        }

        /// <summary>
        /// Constructor that accepts data from existing Contact
        /// </summary>
        /// <param name="contact">Contact</param>
        public Contact(Contact contact)
        {
            if (contact != null)
            {
                this.id = contact.Id;
                this.legalEntity = contact.LegalEntity;
                this.name = contact.Name;
                this.area = contact.Area;
                this.contactInfo = contact.ContactInfo;
            }
            else
            {
                id = 0;
                legalEntity = "";
                name = "";
                area = "";
                contactInfo = 0;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates a new Contact in Db
        /// </summary>
        /// <param name="tempContact">Contact</param>
        /// <returns>int</returns>
        public int CreateContactInDb(Contact tempContact)
        {
            int result = 0;
            int count = 0;
            bool dbAnswer = false;
            List<Contact> tempContacts = new List<Contact>();
            //INSERT INTO [dbo].[Contacts]([LegalEntity], [Name], [Area], [ContactInfo]) VALUES(<LegalEntity, nvarchar(10),>, <Name, nvarchar(10),>, <Area, nvarchar(10),>, <ContactInfo, int,>)
            string strSql = "INSERT INTO[dbo].[Contacts]([Status], [SentDate], [ReceivedDate]) VALUES(" + tempContact.LegalEntity + ", '" + tempContact.Name + "', '" + tempContact.Area + "', '" + tempContact.ContactInfo + "')";
            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl ved forsøg på at oprette en ny kontakt.", "Databasefejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            tempContacts = GetContacts();
            count = tempContacts.Count;
            result = tempContacts[count - 1].Id;
            return result;
        }

        /// <summary>
        /// Method, that fetches a list of Contacts
        /// </summary>
        /// <returns></returns>
        public List<Contact> GetContacts()
        {
            List<string> results = executor.ReadListFromDataBase("Contacts");
            List<Contact> contacts = new List<Contact>();
            foreach (string result in results)
            {
                string[] resultArray = new string[5];
                resultArray = result.Split(';');
                Contact contact = new Contact(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], resultArray[3], Convert.ToInt32(resultArray[4]));
                contacts.Add(contact);
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
            string tempName = name;
            if (area != "")
            {
                tempName += " (" + area + ")";
            }
            return tempName;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public string LegalEntity
        {
            get => legalEntity;
            set
            {
                try
                {
                    if (value != null)
                    {
                        legalEntity = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
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
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string Area
        {
            get => area;
            set
            {
                try
                {
                    if (value != null)
                    {
                        area = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
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
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        #endregion

    }
}
