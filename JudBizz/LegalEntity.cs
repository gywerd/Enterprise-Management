using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class LegalEntity
    {
        #region Fields
        private string id;
        private string name;
        private int address;
        private int contactInfo;
        private string url;
        private int craftGroup1;
        private int craftGroup2;
        private int craftGroup3;
        private int craftGroup4;
        private int region;
        private bool countryWide;
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
            name = "";
            address = 0;
            contactInfo = 0;
            url = "";
            craftGroup1 = 0;
            craftGroup2 = 0;
            craftGroup3 = 0;
            craftGroup4 = 0;
            region = 0;
            countryWide = false;
            cooperative = false;
            active = false;
        }

        /// <summary>
        /// Constructor for adding new legal entity
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="address">int</param>
        /// <param name="contactInfo">int</param>
        /// <param name="craftGroup1">int</param>
        public LegalEntity(string name, int address, int contactInfo, string url, int craftGroup1, int craftGroup2, int craftGroup3, int craftGroup4, int region, bool countryWide = false, bool cooperative = false, bool active=false)
        {
            this.name = name;
            this.address = address;
            this.contactInfo = contactInfo;
            this.url = url;
            this.craftGroup1 = craftGroup1;
            this.craftGroup2 = craftGroup2;
            this.craftGroup3 = craftGroup3;
            this.craftGroup4 = craftGroup4;
            this.region = region;
            this.countryWide = countryWide;
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
        public LegalEntity(string id, string name, int address, int contactInfo, string url, int craftGroup1, int craftGroup2, int craftGroup3, int craftGroup4, int region, bool countryWide, bool cooperative, bool active)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.contactInfo = contactInfo;
            this.url = url;
            this.craftGroup1 = craftGroup1;
            this.craftGroup2 = craftGroup2;
            this.craftGroup3 = craftGroup3;
            this.craftGroup4 = craftGroup4;
            this.region = region;
            this.countryWide = countryWide;
            this.cooperative = cooperative;
            this.active = active;
        }

        /// <summary>
        /// Constructor that accepts data from existing LegalEntity
        /// </summary>
        /// <param name="legalEntity">LegalEntity</param>
        public LegalEntity(LegalEntity legalEntity)
        {
            this.id = legalEntity.Id;
            this.name = legalEntity.Name;
            this.address = legalEntity.Address;
            this.contactInfo = legalEntity.ContactInfo;
            this.url = legalEntity.Url;
            this.craftGroup1 = legalEntity.CraftGroup1;
            this.craftGroup2 = legalEntity.CraftGroup2;
            this.craftGroup3 = legalEntity.CraftGroup3;
            this.craftGroup4 = legalEntity.CraftGroup4;
            this.region = legalEntity.Region;
            this.countryWide = legalEntity.CountryWide;
            this.cooperative = legalEntity.Cooperative;
            this.active = legalEntity.Active;
        }
        
        #endregion

        #region Methods
        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = name + ", (" + id + ")";
            return result;
        }

        public List<LegalEntity> GetLegalEntities()
        {
            List<string> results = executor.ReadListFromDataBase("LegalEntities");
            List<LegalEntity> entities = new List<LegalEntity>();
            foreach (string result in results)
            {
                string[] resultArray = new string[13];
                resultArray = result.Split(';');
                LegalEntity legalEntity = new LegalEntity(resultArray[0], resultArray[1], Convert.ToInt32(resultArray[2]), Convert.ToInt32(resultArray[3]), resultArray[4], Convert.ToInt32(resultArray[5]), Convert.ToInt32(resultArray[6]), Convert.ToInt32(resultArray[7]), Convert.ToInt32(resultArray[8]), Convert.ToInt32(resultArray[9]), Convert.ToBoolean(resultArray[10]), Convert.ToBoolean(resultArray[11]), Convert.ToBoolean(resultArray[12]));
                entities.Add(legalEntity);
            }
            return entities;
        }

        #endregion

        #region Properties
        public string Id { get => id; }

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

        public int CraftGroup1
        {
            get => craftGroup1;
            set
            {
                try
                {
                    if (value >= 0)
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

        public int CraftGroup2
        {
            get => craftGroup2;
            set
            {
                try
                {
                    if (value >= 0)
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

        public int CraftGroup3
        {
            get => craftGroup3;
            set
            {
                try
                {
                    if (value >= 0)
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

        public int CraftGroup4
        {
            get => craftGroup4;
            set
            {
                try
                {
                    if (value >= 0)
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

        public int Region
        {
            get => region;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        region = value;
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
