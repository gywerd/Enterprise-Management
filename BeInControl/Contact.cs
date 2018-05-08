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
        private static string strConnection;
        private int contactPersonId;
        private int legalEntity;
        private int name;
        private int contactInfo;

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
        }

        /// <summary>
        /// Constructor for adding ContactPerson
        /// </summary>
        /// <param name="legalEntity">int</param>
        /// <param name="name">int</param>
        /// <param name="contactInfo">int</param>
        public Contact(int legalEntity, int name, int contactInfo)
        {
            this.legalEntity = legalEntity;
            this.name = name;
            this.contactInfo = contactInfo;
        }

        /// <summary>
        /// Constructor for adding ContactPerson
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="legalEntityId">int</param>
        /// <param name="name">int</param>
        /// <param name="contactInfo">int</param>
        public Contact(int id, int legalEntity, int name, int contactInfo)
        {
            this.contactPersonId = id;
            this.legalEntity = legalEntity;
            this.name = name;
            this.contactInfo = contactInfo;
        }

        #endregion

        #region Methods
        public List<Contact> GetContactPersonList()
        {
            List<string> results = executor.ReadListFromDataBase("Contacts");
            List<Contact> contacts = new List<Contact>();
            foreach (string result in results)
            {
                string[] resultArray = new string[4];
                resultArray = result.Split(';');
                Contact contact = new Contact(Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), Convert.ToInt32(resultArray[2]), Convert.ToInt32(resultArray[3]));
                contacts.Add(contact);
            }
            return contacts;
        }

        #endregion

        #region Properties
        public int ContactPersonId { get => contactPersonId; }
        public int LegalEntity { get => legalEntity; set => legalEntity = value; }
        public int Name { get => name; set => name = value; }
        public int ContactInfo { get => contactInfo; set => contactInfo = value; }
        #endregion

    }
}
