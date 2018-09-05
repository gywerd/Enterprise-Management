using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
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

        /// <summary>
        /// Constructor that accepts an existing ZipTown
        /// </summary>
        /// <param name="zipTown">ZipTown</param>
        public ZipTown(ZipTown zipTown)
        {
            if (zipTown != null)
            {
                this.zip = zipTown.zip;
                this.town = zipTown.town;
            }
            else
            {
                this.zip = "";
                this.town = "";
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that Create a Delete From SQL-Query
        /// </summary>
        /// <param name="zip">string</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(string zip)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.ZipTown WHERE Id = '" + zip + "';";
            return result;
        }

        /// <summary>
        /// Method, that Create a Insert Into SQL-Query
        /// </summary>
        /// <param name="zipTown">ZipTown</param>
        /// <returns>string</returns>
        private string CreateInsertIntoSqlQuery(ZipTown zipTown)
        {
            //INSERT INTO table_name (column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            string dataString = GetDataStringFromProject(zipTown);
            string result = @"INSERT INTO dbo.ZipTown(Zip, Town) VALUES(";
            result += dataString + @");";
            return result;
        }

        /// <summary>
        /// Method, that Create an Update SQL-Query
        /// </summary>
        /// <param name="zipTown">ZipTown</param>
        /// <returns>string</returns>
        private string CreateUpdateSqlQuery(ZipTown zipTown)
        {
            //UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE condition;
            string result = @"UPDATE dbo.Projects SET Town = '" + zipTown.Town + "' WHERE Zip = '" + zipTown.Zip + "';";
            return result;
        }

        /// <summary>
        /// Method, that deletes a Project from the Db
        /// </summary>
        /// <param name="zip">string</param>
        /// <returns>bool</returns>
        public bool DeleteFromZipTown(string zip)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(zip);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        /// <summary>
        /// Method, that converts a project into a data string for SQL-Query
        /// </summary>
        /// <param name="zipTown">ZipTown</param>
        /// <returns>string</returns>
        private string GetDataStringFromProject(ZipTown zipTown)
        {
            string result = "'" + zipTown.Zip + @"', '" + zipTown.Town + "'";
            return result;
        }

        /// <summary>
        /// Method, that load a ZipTownList from Db
        /// </summary>
        /// <returns>List<ZipTown></returns>
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

        /// <summary>
        /// Method, that adds a project to Db
        /// </summary>
        /// <param name="zipTown">Project</param>
        /// <returns>bool</returns>
        public bool InsertIntoZipTown(ZipTown zipTown)
        {
            bool result;
            string strSql = CreateInsertIntoSqlQuery(zipTown);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        /// <summary>
        /// Method, that converts main info to string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return zip + " " + town;
        }

        /// <summary>
        /// Method, that updates a ZipTown in Db
        /// </summary>
        /// <param name="zipTown">ZipTown</param>
        /// <returns>bool</returns>
        public bool UpdateZipTown(ZipTown zipTown)
        {
            bool result;
            string strSql = CreateUpdateSqlQuery(zipTown);
            result = executor.WriteToDataBase(strSql);
            return result;
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