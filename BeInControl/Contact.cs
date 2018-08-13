using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Contact
    {
        #region Fields
        private int contactId;
        private string legalEntity;
        private int name;
        private string description;
        private string email;
        private string mobile;

        private static string strConnection;
        private Executor executor;

        public static Name CNA = new Name(strConnection);

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
        }

        /// <summary>
        /// Constructor for adding ContactPerson
        /// </summary>
        /// <param name="legalEntity">int</param>
        /// <param name="name">int</param>
        /// <param name="description">string</param>
        /// <param name="email">string</param>
        /// <param name="mobile">string</param>
        public Contact(string legalEntity, int name, string description = "", string email = "", string mobile = "")
        {
            this.legalEntity = legalEntity;
            this.name = name;
            this.description = description;
            this.email = email;
            this.mobile = mobile;
        }

        /// <summary>
        /// Constructor for adding ContactPerson
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="legalEntityId">int</param>
        /// <param name="name">int</param>
        /// <param name="description">string</param>
        /// <param name="email">string</param>
        /// <param name="mobile">string</param>
        public Contact(int id, string legalEntity, int name, string description, string email, string mobile)
        {
            this.contactId = id;
            this.legalEntity = legalEntity;
            this.name = name;
            this.description = description;
            this.email = email;
            this.mobile = mobile;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            string tempName = GetName(this.name);
            if (description != "")
            {
                tempName += " (" + description + ")";
            }
            return tempName;
        }

        public List<Contact> GetContactList()
        {
            List<string> results = executor.ReadListFromDataBase("Contacts");
            List<Contact> contacts = new List<Contact>();
            foreach (string result in results)
            {
                string[] resultArray = new string[4];
                resultArray = result.Split(';');
                Contact contact = new Contact(Convert.ToInt32(resultArray[0]), resultArray[1], Convert.ToInt32(resultArray[2]), resultArray[3], resultArray[4], resultArray[5]);
                contacts.Add(contact);
            }
            return contacts;
        }

        public string GetName(int id)
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

        #endregion

        #region Properties
        public int ContactId { get => contactId; }
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

        public int Name
        {
            get => name;
            set
            {
                try
                {
                    if (value >= 0)
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

        public string Description
        {
            get => description;
            set
            {
                try
                {
                    if (value != null)
                    {
                        description = value;
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
        #endregion

    }
}
