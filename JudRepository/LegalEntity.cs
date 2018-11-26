using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class LegalEntity
    {
        #region Fields
        private static string strConnection;
        private Executor executor;

        private string id;
        private string name;
        private Address address;
        private ContactInfo contactInfo;
        private string url;
        private CraftGroup craftGroup1;
        private CraftGroup craftGroup2;
        private CraftGroup craftGroup3;
        private CraftGroup craftGroup4;
        private Region region;
        private bool countryWide;
        private bool cooperative;
        private bool active;

        Address CAD = new Address(strConnection);
        CraftGroup CCG = new CraftGroup(strConnection);
        ContactInfo CCI = new ContactInfo(strConnection);
        Region CRG = new Region(strConnection);
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public LegalEntity(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = "";
            name = "";
            address = new Address(strConnection);
            contactInfo = new ContactInfo(strConnection);
            url = "";
            craftGroup1 = new CraftGroup(strConnection);
            craftGroup2 = new CraftGroup(strConnection);
            craftGroup3 = new CraftGroup(strConnection);
            craftGroup4 = new CraftGroup(strConnection);
            region = new Region(strConnection);
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
        /// <param name="url">string</param>
        /// <param name="craftGroup1">int</param>
        /// <param name="craftGroup2">int</param>
        /// <param name="craftGroup3">int</param>
        /// <param name="craftGroup4">int</param>
        /// <param name="region">int</param>
        /// <param name="countryWide">bool</param>
        /// <param name="cooperative">bool</param>
        /// <param name="active"></param>
        public LegalEntity(string strCon, string name, Address address, ContactInfo contactInfo, string url, CraftGroup craftGroup1, CraftGroup craftGroup2, CraftGroup craftGroup3, CraftGroup craftGroup4, Region region, bool countryWide = false, bool cooperative = false, bool active=false)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = "";
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
        /// <param name="name">string</param>
        /// <param name="addressId">int</param>
        /// <param name="contactInfoId">int</param>
        /// <param name="url">string</param>
        /// <param name="craftGroup1">int</param>
        /// <param name="craftGroup2">int</param>
        /// <param name="craftGroup3">int</param>
        /// <param name="craftGroup4">int</param>
        /// <param name="region">int</param>
        /// <param name="countryWide">bool</param>
        /// <param name="cooperative">bool</param>
        /// <param name="active"></param>
        public LegalEntity(string strCon, string id, string name, int addressId, int contactInfoId, string url, int craftGroup1Id, int craftGroup2Id, int craftGroup3Id, int craftGroup4Id, int regionId, bool countryWide, bool cooperative, bool active)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.name = name;
            this.address = CAD.GetAddress(addressId);
            this.contactInfo = CCI.GetContactInfo(contactInfoId);
            this.url = url;
            this.craftGroup1 = CCG.GetCraftGroup(craftGroup1Id);
            this.craftGroup2 = CCG.GetCraftGroup(craftGroup2Id);
            this.craftGroup3 = CCG.GetCraftGroup(craftGroup3Id);
            this.craftGroup4 = CCG.GetCraftGroup(craftGroup4Id);
            this.region = CRG.GetRegion(regionId);
            this.countryWide = countryWide;
            this.cooperative = cooperative;
            this.active = active;
        }

        /// <summary>
        /// Constructor that accepts data from existing LegalEntity
        /// </summary>
        /// <param name="legalEntity">LegalEntity</param>
        public LegalEntity(string strCon, LegalEntity legalEntity)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (legalEntity != null)
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
            else
            {
                this.id = "";
                name = "";
                address = new Address(strConnection);
                contactInfo = new ContactInfo(strConnection);
                url = "";
                craftGroup1 = new CraftGroup(strConnection);
                craftGroup2 = new CraftGroup(strConnection);
                craftGroup3 = new CraftGroup(strConnection);
                craftGroup4 = new CraftGroup(strConnection);
                region = new Region(strConnection);
                countryWide = false;
                cooperative = false;
                active = false;
            }
        }
        
        #endregion

        #region Methods
        public List<LegalEntity> GetLegalEntities()
        {
            List<string> results = executor.ReadListFromDataBase("LegalEntities");
            List<LegalEntity> entities = new List<LegalEntity>();
            foreach (string result in results)
            {
                string[] resultArray = new string[13];
                resultArray = result.Split(';');
                LegalEntity legalEntity = new LegalEntity(strConnection, resultArray[0], resultArray[1], Convert.ToInt32(resultArray[2]), Convert.ToInt32(resultArray[3]), resultArray[4], Convert.ToInt32(resultArray[5]), Convert.ToInt32(resultArray[6]), Convert.ToInt32(resultArray[7]), Convert.ToInt32(resultArray[8]), Convert.ToInt32(resultArray[9]), Convert.ToBoolean(resultArray[10]), Convert.ToBoolean(resultArray[11]), Convert.ToBoolean(resultArray[12]));
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
            string result = name + ", (" + id + ")";
            return result;
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
                catch (Exception)
                {
                    url = "";
                }
            }
        }

        public CraftGroup CraftGroup1 { get; set; }

        public CraftGroup CraftGroup2 { get; set; }

        public CraftGroup CraftGroup3 { get; set; }

        public CraftGroup CraftGroup4 { get; set; }

        public Region Region { get; set; }
        public bool CountryWide
        {
            get => countryWide;
            set
            {
                try
                {
                    countryWide = value;
                }
                catch (Exception)
                {
                    countryWide = false;
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
                    cooperative = value;
                }
                catch (Exception)
                {
                    cooperative = false;
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
                    active = value;
                }
                catch (Exception)
                {
                    active = false;
                }
            }
        }

        #endregion

    }
}
