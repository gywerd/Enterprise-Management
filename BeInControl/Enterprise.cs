using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Enterprise
    {
        #region Fields
        private int id;
        private int project;
        private string name;
        private string elaboration;
        private string offerList;
        private int craftGroup1;
        private int craftGroup2;
        private int craftGroup3;
        private int craftGroup4;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Enterprise() { }

        /// <summary>
        /// Executor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public Enterprise(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor for adding new Enterprise
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="project">int</param>
        /// <param name="craftGroup1">string</param>
        /// <param name="elaboration">string</param>
        /// <param name="offerList">string</param>
        /// <param name="craftGroup2">string</param>
        /// <param name="craftGroup3">string</param>
        /// <param name="craftGroup4">string</param>
        public Enterprise(int project, string name, int craftGroup1, string elaboration = "", string offerList = "", int craftGroup2 = 0, int craftGroup3 = 0, int craftGroup4 = 0)
        {
            this.project = project;
            this.name = name;
            this.elaboration = elaboration;
            this.offerList = offerList;
            this.craftGroup1 = craftGroup1;
            this.craftGroup2 = craftGroup2;
            this.craftGroup3 = craftGroup3;
            this.craftGroup4 = craftGroup4;
        }

        /// <summary>
        /// Constructor for adding Enterprise from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="name">string</param>
        /// <param name="project">int</param>
        /// <param name="elaboration">string</param>
        /// <param name="offerList">string</param>
        /// <param name="craftGroup1">string</param>
        /// <param name="craftGroup2">string</param>
        /// <param name="craftGroup3">string</param>
        /// <param name="craftGroup4">string</param>
        public Enterprise(int id, int project, string name, string elaboration, string offerList, int craftGroup1, int craftGroup2, int craftGroup3, int craftGroup4)
        {
            this.id = id;
            this.project = project;
            this.name = name;
            this.elaboration = elaboration;
            this.offerList = offerList;
            this.craftGroup1 = craftGroup1;
            this.craftGroup2 = craftGroup1;
            this.craftGroup3 = craftGroup3;
            this.craftGroup4 = craftGroup4;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return name;
        }

        public List<Enterprise> GetEnterpriseList()
        {
            List<string> results = executor.ReadListFromDataBase("EnterpriseList");
            List<Enterprise> enterprises = new List<Enterprise>();
            foreach (string result in results)
            {
                string[] resultArray = new string[9];
                resultArray = result.Split(';');
                Enterprise enterprise = new Enterprise(Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), resultArray[2], resultArray[3], resultArray[4], Convert.ToInt32(resultArray[5]), Convert.ToInt32(resultArray[6]), Convert.ToInt32(resultArray[7]), Convert.ToInt32(resultArray[8]));
                enterprises.Add(enterprise);
            }
            return enterprises;
        }

        #endregion

        #region Properties
        public int Id { get => id; }
        public int Project
        {
            get => project;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        project = value;
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
        public string Elaboration
        {
            get => elaboration;
            set
            {
                try
                {
                    if (value != null)
                    {
                        elaboration = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public string OfferList
        {
            get => offerList;
            set
            {
                try
                {
                    if (value != null)
                    {
                        offerList = value;
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
