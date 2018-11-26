using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class Builder
    {
        #region Fields
        private static string strConnection;
        private Executor executor;

        private int id;
        private string cvr;
        private string name;
        private Address address;
        private ContactInfo contactInfo;
        private string url;

        private Address CAD = new Address(strConnection);
        private ContactInfo CCI = new ContactInfo(strConnection);
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Builder(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            id = 0;
            cvr = "0";
            name = "";
            address = new Address(strConnection);
            contactInfo = new ContactInfo(strConnection);
            url = "";
        }

        /// <summary>
        /// Constructor for adding new builder
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="address">int</param>
        /// <param name="contactInfo">int</param>
        /// <param name="craftGroup1">int</param>
        public Builder(string strCon, string cvr, string name, int address = 0, int contactInfo = 0, string url = "")
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.cvr = cvr;
            this.name = name;
            this.address = CAD.GetAddress(address);
            this.contactInfo = CCI.GetContactInfo(contactInfo);
            this.url = url;
        }

        /// <summary>
        /// Constructor for adding a builder from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="companyName">string</param>
        /// <param name="addressId">int</param>
        /// <param name="contactInfoId">int</param>
        /// <param name="craftGroup">int</param>
        public Builder(string strCon, int id, string cvr, string name, int addressId, int contactInfoId, string url)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.name = name;
            this.address = CAD.GetAddress(addressId);
            this.contactInfo = CCI.GetContactInfo(contactInfoId);
            this.url = url;
        }

        /// <summary>
        /// Constructor, that accepts an existing builder
        /// </summary>
        /// <param name="builder">Builder</param>
        public Builder(string strCon, Builder builder)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (builder != null)
            {
                this.id = builder.Id;
                this.name = builder.Name;
                this.address = builder.Address;
                this.contactInfo = builder.ContactInfo;
                this.url = builder.Url;
            }
            else
            {
                this.id = 0;
                this.name = "";
                this.address = new Address(strConnection);
                this.contactInfo = new ContactInfo(strConnection);
                this.url = "";
            }
        }


        #endregion

        #region Methods
        public Builder GetBuilder(int builderId)
        {
            List<Builder> entities = GetBuilders();
            Builder result = new Builder(strConnection);
            foreach (Builder builder in entities)
            {
                if (builder.Id == builderId)
                {
                    result = builder;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that reads Builders list from Db
        /// </summary>
        /// <returns></returns>
        public List<Builder> GetBuilders()
        {
            List<string> results = executor.ReadListFromDataBase("Builders");
            List<Builder> entities = new List<Builder>();
            foreach (string result in results)
            {
                string[] resultArray = new string[6];
                resultArray = result.Split(';');
                Builder legalEntity = new Builder(strConnection, Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], Convert.ToInt32(resultArray[3]), Convert.ToInt32(resultArray[4]), resultArray[5]);
                entities.Add(legalEntity);
            }
            return entities;
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = name;
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public string Cvr
        {
            get => cvr;
            set
            {
                try
                {
                    cvr = value;
                }
                catch (Exception)
                {
                    cvr = "";
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
                    name = value;
                }
                catch (Exception)
                {
                    name = "";
                }
            }
        }

        public Address Address { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public string Url
        {
            get => url;
            set
            {
                try
                {
                   url = value;
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

