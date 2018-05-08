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
        private static string strConnection;
        private int id;
        private string zip;
        private string town;
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
        /// Constructor for adding new ZipTown to db
        /// </summary>
        /// <param name="zip">string</param>
        /// <param name="town">string</param>
        public ZipTown(string zip, string town)
        {
            this.zip = zip;
            this.town = town;
        }

        /// <summary>
        /// Constructor for adding ZipTown fron db to list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="zip"></param>
        /// <param name="town"></param>
        public ZipTown(int id, string zip, string town)
        {
            this.id = id;
            this.zip = zip;
            this.town = town;
        }
        #endregion

        #region Methods
        public List<ZipTown> GetZipTownList()
        {
            List<string> results = executor.ReadListFromDataBase("ZipTown");
            List<ZipTown> zips = new List<ZipTown>();
            foreach (string result in results)
            {
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                ZipTown zipTown = new ZipTown(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2]);
                zips.Add(zipTown);
            }
            return zips;
        }

        public override string ToString()
        {
            return zip + " " + town;
        }
        #endregion

        #region Properties
        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Zip
        {
            get => zip;
            set => zip = value;
        }

        public string Town
        {
            get => town;
            set => town = value;
        }
        #endregion
    }
}