using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class Region
    {
        #region Fields
        private static string strConnection;
        private Executor executor;

        private int id;
        private string regionName;
        private string zips;

        Region CRG = new Region(strConnection);
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Region(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.regionName = "";
            this.zips = "";
        }

        /// <summary>
        /// Constructor to add new EnterpriseForm
        /// </summary>
        /// <param name="regionName">string</param>
        /// <param name="zips">string</param>
        public Region(string strCon, string regionName, string zips)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.regionName = regionName;
            this.zips = zips;
        }

        /// <summary>
        /// Constructor to add Enterprise Form from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="regionName">string</param>
        /// <param name="zips">string</param>
        public Region(string strCon, int id, string regionName, string zips)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.regionName = regionName;
            this.zips = zips;
        }

        /// <summary>
        /// Constructor to add Enterprise Form from Db
        /// </summary>
        /// <param name="region">Region</param>
        public Region(string strCon, Region region)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (region != null)
            {
                this.id = region.Id;
                this.regionName = region.RegionName;
                this.zips = region.Zips;
            }
            else
            {
                this.id = 0;
                this.regionName = "";
                this.zips = "";
            }
        }

        #endregion

        #region Methods
        public Region GetRegion(int regionId)
        {
            List<Region> regions = GetRegions();
            Region result = new Region(strConnection);

            foreach (Region region in regions)
            {
                if (region.Id == regionId)
                {
                    result = region;
                }
            }

            return result;
        }

        /// <summary>
        /// Retrieves a list of regions from Db
        /// </summary>
        /// <returns></returns>
        public List<Region> GetRegions()
        {
            List<string> results = executor.ReadListFromDataBase("Regions");
            List<Region> geography = new List<Region>();
            foreach (string result in results)
            {
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                Region region = new Region(strConnection, Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2]);
                geography.Add(region);
            }
            return geography;
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return regionName;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public string RegionName {
            get => regionName;
            set
            {
                try
                {
                    regionName = value;
                }
                catch (Exception)
                {
                    regionName = "";
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
                    zips = value;
                }
                catch (Exception)
                {
                    zips = "";
                }
            }
        }

        #endregion

    }
}
