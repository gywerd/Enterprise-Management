using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class LegalEntity
    {
        #region Fields
        private string entityid;
        private string companyName;
        private int address;
        private string phone;
        private string fax;
        private string url;
        private string craftGroup1;
        private string craftGroup2;
        private string craftGroup3;
        private string craftGroup4;
        private bool countryWide;
        private int area;
        private bool cooperative;
        private bool active;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public LegalEntity() { }

        /// <summary>
        /// Constructor for access to db methods
        /// </summary>
        public LegalEntity(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor for adding new legal entity
        /// </summary>
        /// <param name="companyName">string</param>
        /// <param name="address">int</param>
        /// <param name="phone">int</param>
        /// <param name="craftGroup1">int</param>
        public LegalEntity(string companyName, int address, string phone, string fax, string url, string craftGroup1, int area, bool countryWide = false, bool cooperative = false, bool active=true, string craftGroup2 = "", string craftGroup3 = "", string craftGroup4 = "")
        {
            this.companyName = companyName;
            this.address = address;
            this.phone = phone;
            this.fax = fax;
            this.url = url;
            this.craftGroup1 = craftGroup1;
            this.craftGroup2 = craftGroup2;
            this.craftGroup3 = craftGroup3;
            this.craftGroup4 = craftGroup4;
            this.countryWide = countryWide;
            this.area = area;
            this.cooperative = cooperative;
            this.active = active;
        }

        /// <summary>
        /// Constructor for adding a legal entity from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="companyName">string</param>
        /// <param name="address">int</param>
        /// <param name="contact">int</param>
        /// <param name="craftGroup">int</param>
        public LegalEntity(string id, string companyName, int address, string phone, string fax, string url, string craftGroup1, string craftGroup2, string craftGroup3, string craftGroup4, bool countryWide, int area, bool cooperative, bool active)
        {
            this.entityid = id;
            this.companyName = companyName;
            this.address = address;
            this.phone = phone;
            this.fax = fax;
            this.url = url;
            this.craftGroup1 = craftGroup1;
            this.craftGroup2 = craftGroup2;
            this.craftGroup3 = craftGroup3;
            this.craftGroup4 = craftGroup4;
            this.countryWide = countryWide;
            this.area = area;
            this.cooperative = cooperative;
            this.active = active;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return companyName;
        }

        public List<LegalEntity> GetLegalEntities()
        {
            List<string> results = executor.ReadListFromDataBase("LegalEntities");
            List<LegalEntity> entities = new List<LegalEntity>();
            foreach (string result in results)
            {
                string[] resultArray = new string[14];
                resultArray = result.Split(';');
                LegalEntity legalEntity = new LegalEntity(resultArray[0], resultArray[1], Convert.ToInt32(resultArray[2]), resultArray[3], resultArray[4], resultArray[5], resultArray[6], resultArray[7], resultArray[8], resultArray[9], Convert.ToBoolean(resultArray[10]), Convert.ToInt32(resultArray[11]), Convert.ToBoolean(resultArray[12]), Convert.ToBoolean(resultArray[13]));
                entities.Add(legalEntity);
            }
            return entities;
        }

        #endregion

        #region Properties
        public string EntityId { get => entityid; }

        public string CompanyName
        {
            get => companyName;
            set
            {
                try
                {
                    if (value != null)
                    {
                        companyName = value;
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

        public string CraftGroup1
        {
            get => craftGroup1;
            set
            {
                try
                {
                    if (value != null)
                    {
                        craftGroup1 = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string CraftGroup2
        {
            get => craftGroup2;
            set
            {
                try
                {
                    if (value != null)
                    {
                        craftGroup2 = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string CraftGroup3
        {
            get => craftGroup3;
            set
            {
                try
                {
                    if (value != null)
                    {
                        craftGroup3 = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string CraftGroup4
        {
            get => craftGroup4;
            set
            {
                try
                {
                    if (value != null)
                    {
                        craftGroup4 = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public bool CountryWide
        {
            get => countryWide;
            set
            {
                try
                {
                    if (value.Equals(true) || value.Equals(false))
                    {
                        countryWide = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public int Area
        {
            get => area;
            set
            {
                try
                {
                    if (value >= 0)
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

        public bool Cooperative
        {
            get => cooperative;
            set
            {
                try
                {
                    if (value.Equals(true) || value.Equals(false))
                    {
                        cooperative = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public bool Active
        {
            get => active;
            set
            {
                try
                {
                    if (value.Equals(true) || value.Equals(false))
                    {
                        active = value;
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
