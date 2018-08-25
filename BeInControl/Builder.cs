using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Builder
    {
        #region Fields
        private int id;
        private string cvr;
        private string name;
        private int address;
        private int contactInfo;
        private string url;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Builder()
        {
        }

        /// <summary>
        /// Constructor for access to db methods
        /// </summary>
        public Builder(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
            cvr = "0";
            name = "";
            address = 0;
            contactInfo = 0;
            url = "";
        }

        /// <summary>
        /// Constructor for adding new legal entity
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="address">int</param>
        /// <param name="contactInfo">int</param>
        /// <param name="craftGroup1">int</param>
        public Builder(string cvr, string name, int address = 0, int contactInfo = 0, string url = "")
        {
            this.cvr = cvr;
            this.name = name;
            this.address = address;
            this.contactInfo = contactInfo;
            this.url = url;
        }

        /// <summary>
        /// Constructor for adding a legal entity from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="companyName">string</param>
        /// <param name="address">int</param>
        /// <param name="contact">int</param>
        /// <param name="craftGroup">int</param>
        public Builder(int id, string cvr, string name, int address, int contactInfo, string url)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.contactInfo = contactInfo;
            this.url = url;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = name;
            return result;
        }

        public List<Builder> GetBuilders()
        {
            List<string> results = executor.ReadListFromDataBase("Builders");
            List<Builder> entities = new List<Builder>();
            foreach (string result in results)
            {
                string[] resultArray = new string[6];
                resultArray = result.Split(';');
                Builder legalEntity = new Builder(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], Convert.ToInt32(resultArray[3]), Convert.ToInt32(resultArray[4]), resultArray[5]);
                entities.Add(legalEntity);
            }
            return entities;
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
                    if (value != null)
                    {
                        cvr = value;
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

        public int Address
        {
            get => address;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        address = value;
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

        public string Url
        {
            get => url;
            set
            {
                try
                {
                    if (value != null)
                    {
                        url = value;
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

