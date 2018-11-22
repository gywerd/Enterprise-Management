using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JudRepository
{
    public class Enterprise
    {
        #region Fields
        protected int id;
        protected int project;
        protected string name;
        protected string elaboration;
        protected string offerList;
        protected int craftGroup1;
        protected int craftGroup2;
        protected int craftGroup3;
        protected int craftGroup4;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Enterprise(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            project = 0;
            name = "";
            elaboration = "";
            offerList = "";
            craftGroup1 = 0;
            craftGroup2 = 0;
            craftGroup3 = 0;
            craftGroup4 = 0;
        }

        /// <summary>
        /// Constructor for adding new Enterprise
        /// </summary>
        /// <param name="project">int</param>
        /// <param name="name">string</param>
        /// <param name="craftGroup1">int</param>
        /// <param name="elaboration">string</param>
        /// <param name="offerList">string</param>
        /// <param name="craftGroup2">int</param>
        /// <param name="craftGroup3">int</param>
        /// <param name="craftGroup4">int</param>
        public Enterprise(string strCon, int project, string name, int craftGroup1, string elaboration = "", string offerList = "", int craftGroup2 = 0, int craftGroup3 = 0, int craftGroup4 = 0)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
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
        /// <param name="project">int</param>
        /// <param name="name">string</param>
        /// <param name="elaboration">string</param>
        /// <param name="offerList">string</param>
        /// <param name="craftGroup1">int</param>
        /// <param name="craftGroup2">int</param>
        /// <param name="craftGroup3">int</param>
        /// <param name="craftGroup4">int</param>
        public Enterprise(string strCon, int id, int project, string name, string elaboration, string offerList, int craftGroup1, int craftGroup2, int craftGroup3, int craftGroup4)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
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
        /// Constructor for that accepts data from existing Enterprise
        /// </summary>
        /// <param name="enterprise">Enterprise</param>
        public Enterprise(string strCon, Enterprise enterprise)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (enterprise != null)
            {
                this.id = enterprise.id;
                this.project = enterprise.Project;
                this.name = enterprise.Name;
                this.elaboration = enterprise.Elaboration;
                this.offerList = enterprise.OfferList;
                this.craftGroup1 = enterprise.CraftGroup1;
                this.craftGroup2 = enterprise.CraftGroup2;
                this.craftGroup3 = enterprise.CraftGroup3;
                this.craftGroup4 = enterprise.CraftGroup4;
            }
            else
            {
                this.id = 0;
                this.project = 0;
                this.name = "";
                this.elaboration = "";
                this.offerList = "";
                this.craftGroup1 = 0;
                this.craftGroup2 = 0;
                this.craftGroup3 = 0;
                this.craftGroup4 = 0;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates a Delete SQL-Query
        /// </summary>
        /// <param name="enterprise">int</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int enterprise)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.EnterpriseList WHERE Id = " + enterprise + @";";
            return result;
        }

        /// <summary>
        /// Method, that creates an Insert Into SQL-Query
        /// </summary>
        /// <param name="enterprise">Enterprise</param>
        /// <returns>string</returns>
        private string CreateInsertIntoSqlQuery(Enterprise enterprise)
        {
            //INSERT INTO table_name (column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            string dataString = GetDataStringFromEnterprise(enterprise);
            string result = @"INSERT INTO dbo.EnterpriseList(Project, Name, Elaboration, OfferList, CraftGroup1, CraftGroup2, CraftGroup3, CraftGroup4) VALUES(";
            result += dataString + @");";
            return result;
        }

        /// <summary>
        /// Method, that creates an Update SQL-Query
        /// </summary>
        /// <param name="enterprise">Enterprise</param>
        /// <returns>string</returns>
        private string CreateUpdateSqlQuery(Enterprise enterprise)
        {
            //UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE condition;
            string result = @"UPDATE dbo.EnterpriseList SET Project = " + enterprise.Project.ToString() + @", Name = '" + enterprise.Name + @"', Elaboration = '" + enterprise.Elaboration + @"', OfferList = '" + enterprise.OfferList + @"', CraftGroup1 = " + enterprise.CraftGroup1.ToString() + @", CraftGroup2 = " + enterprise.CraftGroup2.ToString() + @", CraftGroup3 = " + enterprise.CraftGroup3.ToString() + @", CraftGroup4 = " + enterprise.CraftGroup4.ToString() + @" WHERE Id = " + enterprise.Id + @";";
            return result;
        }

        /// <summary>
        /// Method, that removes an Enterprise from Db
        /// </summary>
        /// <param name="enterprise">int</param>
        public bool DeleteFromEnterpriseList(int enterprise)
        {
            try
            {
                bool result = false;
                string strSql = CreateDeleteFromSqlQuery(enterprise);
                result = executor.WriteToDataBase(strSql);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Method, converts an Enterprise into a data string
        /// </summary>
        /// <param name="enterprise">Enterprise</param>
        /// <returns>string</returns>
        private string GetDataStringFromEnterprise(Enterprise enterprise)
        {
            string result = enterprise.Project.ToString() + @", '" + enterprise.Name + @"', '" + enterprise.Elaboration + @"', '" + enterprise.OfferList + @"', " + enterprise.CraftGroup1.ToString() + @", " + enterprise.CraftGroup2.ToString() + @", " + enterprise.CraftGroup3.ToString() + @", " + enterprise.CraftGroup4.ToString();
            return result;
        }

        /// <summary>
        /// Method, that reads the Enterprise List from Db
        /// </summary>
        /// <returns>List<Enterprise></returns>
        public List<Enterprise> GetEnterpriseList()
        {
            List<string> results = executor.ReadListFromDataBase("EnterpriseList");
            List<Enterprise> enterprises = new List<Enterprise>();
            foreach (string result in results)
            {
                string[] resultArray = new string[9];
                resultArray = result.Split(';');
                Enterprise enterprise = new Enterprise(strConnection, Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), resultArray[2], resultArray[3], resultArray[4], Convert.ToInt32(resultArray[5]), Convert.ToInt32(resultArray[6]), Convert.ToInt32(resultArray[7]), Convert.ToInt32(resultArray[8]));
                enterprises.Add(enterprise);
            }
            return enterprises;
        }

        /// <summary>
        /// Method, that adds an Enterprise to Db
        /// </summary>
        /// <param name="tempEnterprise">Enterprise</param>
        /// <returns>bool</returns>
        public bool InsertIntoEnterpriseList(Enterprise tempEnterprise)
        {
            bool result;
            string strSql = CreateInsertIntoSqlQuery(tempEnterprise);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        /// <summary>
        /// Method, that returns main info as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Method, that updates an Enterprise in Db
        /// </summary>
        /// <param name="tempEnterprise"></param>
        /// <returns>bool</returns>
        public bool UpdateEnterpriseList(Enterprise tempEnterprise)
        {
            bool result;
            string strSql = CreateUpdateSqlQuery(tempEnterprise);
            result = executor.WriteToDataBase(strSql);
            return result;
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

        #endregion
    }
}
