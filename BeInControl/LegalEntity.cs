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
        private static string strConnection;
        private int id;
        private string companyName;
        private int address;
        private int contact;
        private int craftGroup;

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
        /// <param name="contact">int</param>
        /// <param name="craftGroup">int</param>
        public LegalEntity(string companyName, int address, int contact, int craftGroup)
        {
            this.companyName = companyName;
            this.address = address;
            this.contact = contact;
            this.craftGroup = craftGroup;
        }

        /// <summary>
        /// Constructor for adding a legal entity from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="companyName">string</param>
        /// <param name="address">int</param>
        /// <param name="contact">int</param>
        /// <param name="craftGroup">int</param>
        public LegalEntity(int id, string companyName, int address, int contact, int craftGroup)
        {
            this.id = id;
            this.companyName = companyName;
            this.address = address;
            this.contact = contact;
            this.craftGroup = craftGroup;
        }
        #endregion

        #region Methods
        public List<LegalEntity> GetLegalEntityList()
        {
            List<string> results = executor.ReadListFromDataBase("LegalEntities");
            List<LegalEntity> entities = new List<LegalEntity>();
            foreach (string result in results)
            {
                string[] resultArray = new string[5];
                resultArray = result.Split(';');
                LegalEntity legalEntity = new LegalEntity(Convert.ToInt32(resultArray[0]), resultArray[1], Convert.ToInt32(resultArray[2]), Convert.ToInt32(resultArray[3]), Convert.ToInt32(resultArray[4]));
                entities.Add(legalEntity);
            }
            return entities;
        }

        #endregion

        #region Properties
        public int Id { get => id; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public int Address { get => address; set => address = value; }
        public int Contact { get => contact; set => contact = value; }
        public int CraftGroup
        {
            get => craftGroup;
            set => craftGroup = value;
        }
        #endregion

    }
}
