using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Region
    {
        #region Fields
        private int id;
        private string region;
        private string zips;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Region()
        {
        }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public Region(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add new EnterpriseForm
        /// </summary>
        /// <param name="regionName">string</param>
        /// <param name="zips">string</param>
        public Region(string regionName, string zips)
        {
            this.region = regionName;
            this.zips = zips;
        }

        /// <summary>
        /// Constructor to add Enterprise Form from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="regionName">string</param>
        /// <param name="zips">string</param>
        public Region(int id, string regionName, string zips)
        {
            this.id = id;
            this.region = regionName;
            this.zips = zips;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return region;
        }

        /// <summary>
        /// Retrieves a list of regions from Db
        /// </summary>
        /// <returns></returns>
        public List<Region> GetGeography()
        {
            List<string> results = executor.ReadListFromDataBase("Regions");
            List<Region> geography = new List<Region>();
            foreach (string result in results)
            {
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                Region region = new Region(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2]);
                geography.Add(region);
            }
            return geography;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public string RegionName
        {
            get => region;
            set
            {
                try
                {
                    if (value != null)
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

        public string Zips
        {
            get => zips;
            set
            {
                try
                {
                    if (value != null)
                    {
                        zips = value;
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
