using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class ZipTown
    {
        #region Fields
        private string zip;
        private string town;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public ZipTown() { }

        /// <summary>
        /// Constructor for access to db methods
        /// </summary>
        /// <param name="stringConnection"></param>
        public ZipTown(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor for adding ZipTown to Db or List
        /// </summary>
        /// <param name="zip">string</param>
        /// <param name="town">string</param>
        public ZipTown(string zip, string town)
        {
            this.zip = zip;
            this.town = town;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method that return class content as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return zip + " " + town;
        }

        public List<ZipTown> GetZipTownList()
        {
            List<string> results = executor.ReadListFromDataBase("ZipTown");
            List<ZipTown> zips = new List<ZipTown>();
            foreach (string result in results)
            {
                string[] resultArray = new string[2];
                resultArray = result.Split(';');
                ZipTown zipTown = new ZipTown(resultArray[0], resultArray[1]);
                zips.Add(zipTown);
            }
            return zips;
        }

        #endregion

        #region Properties
        public string Zip
        {
            get => zip;
            set
            {
                try
                {
                    if (value != null)
                    {
                        zip = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string Town
        {
            get => town;
            set
            {
                try
                {
                    if (value != null)
                    {
                        town = value;
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