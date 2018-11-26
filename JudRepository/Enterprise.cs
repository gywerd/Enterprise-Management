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
        private static string strConnection;
        private Executor executor;

        protected int id;
        protected Project project;
        protected string name;
        protected string elaboration;
        protected string offerList;
        protected CraftGroup craftGroup1;
        protected CraftGroup craftGroup2;
        protected CraftGroup craftGroup3;
        protected CraftGroup craftGroup4;

        private Project CPJ = new Project(strConnection);
        private CraftGroup CCG = new CraftGroup(strConnection);

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
            project = new Project(strConnection);
            name = "";
            elaboration = "";
            offerList = "";
            craftGroup1 = new CraftGroup(strConnection);
            craftGroup2 = new CraftGroup(strConnection);
            craftGroup3 = new CraftGroup(strConnection);
            craftGroup4 = new CraftGroup(strConnection);
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
        public Enterprise(string strCon, Project project, string name, CraftGroup craftGroup1, CraftGroup craftGroup2, CraftGroup craftGroup3, CraftGroup craftGroup4, string elaboration = "", string offerList = "")
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
        /// <param name="projectId">int</param>
        /// <param name="name">string</param>
        /// <param name="elaboration">string</param>
        /// <param name="offerList">string</param>
        /// <param name="craftGroup1Id">int</param>
        /// <param name="craftGroup2Id">int</param>
        /// <param name="craftGroup3Id">int</param>
        /// <param name="craftGroup4Id">int</param>
        public Enterprise(string strCon, int id, int projectId, string name, string elaboration, string offerList, int craftGroup1Id, int craftGroup2Id, int craftGroup3Id, int craftGroup4Id)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.project = CPJ.GetProject(projectId);
            this.name = name;
            this.elaboration = elaboration;
            this.offerList = offerList;
            this.craftGroup1 = CCG.GetCraftGroup(craftGroup1Id);
            this.craftGroup2 = CCG.GetCraftGroup(craftGroup2Id);
            this.craftGroup3 = CCG.GetCraftGroup(craftGroup3Id);
            this.craftGroup4 = CCG.GetCraftGroup(craftGroup4Id);
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
                project = new Project(strConnection);
                this.name = "";
                this.elaboration = "";
                this.offerList = "";
                craftGroup1 = new CraftGroup(strConnection);
                craftGroup2 = new CraftGroup(strConnection);
                craftGroup3 = new CraftGroup(strConnection);
                craftGroup4 = new CraftGroup(strConnection);
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
        /// Method, that reretrieves an Enterprise from Db
        /// </summary>
        /// <param name="enterpriseId">int</param>
        /// <returns></returns>
        public Enterprise GetEnterprise(int enterpriseId)
        {
            List<Enterprise> enterprises = new List<Enterprise>();
            Enterprise result = new Enterprise(strConnection);
            foreach (Enterprise enterprise in enterprises)
            {
                if (enterprise.Id == enterpriseId)
                {
                    result = enterprise ;
                }
            }
            return enterprises[0];
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
        /// Method, that reads a Enterprise List for a Project from Db
        /// </summary>
        /// <param name="projectId">int</param>
        /// <returns></returns>
        public List<Enterprise> GetEnterpriseList(int projectId)
        {
            List<Enterprise> enterprises = GetEnterpriseList();
            List<Enterprise> result = new List<Enterprise>();
            foreach (Enterprise enterprise in enterprises)
            {
                if (enterprise.Project.Id == projectId)
                {
                    result.Add(enterprise);
                }
            }
            return result;
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

        public Project Project { get; set; }

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

        public CraftGroup CraftGroup1 { get; set; }

        public CraftGroup CraftGroup2 { get; set; }

        public CraftGroup CraftGroup3 { get; set; }

        public CraftGroup CraftGroup4 { get; set; }

        #endregion
    }
}
