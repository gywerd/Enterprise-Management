using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JudRepository
{
    public class BluePrint
    {
        #region Fields
        private static string strConnection;
        private Executor executor;

        int id;
        Project project;
        string name;
        string description;
        string url;

        Project CPJ = new Project(strConnection);

        #endregion

        #region Constructors
        public BluePrint(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = new Project(strConnection);
            this.name = "";
            this.description = "";
            this.url = "";
        }

        public BluePrint(string strCon, Project project, string name, string description, string url)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = project;
            this.name = name;
            this.description = description;
            this.url = url;
        }

        public BluePrint(string strCon, int id, int projectId, string name, string description, string url)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.project = CPJ.GetProject(projectId);
            this.name = name;
            this.description = description;
            this.url = url;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates sqlQuery to delete a IttLetterReceiver entry in Db
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.BluePrints WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creates a new IttLetterReceiver in Db
        /// </summary>
        /// <param name="bluePrint">Description</param>
        /// <returns>int</returns>
        public bool CreateBluePrintInDb(BluePrint bluePrint)
        {
            bool dbAnswer = false;
            //List<Description> tempDescriptionList = new List<Description>();

            //INSERT INTO [dbo].[BluePrints]([Project], [Name], [Description], [Url]) VALUES(<PdfData, int,>, <Name, nvarchar(50),>, <Description, nvarchar(255),>, <Url, nvarchar(50),>)
            string strSql = @"INSERT INTO[dbo].[BluePrints]([Project], [Name], [Description], [Url]) VALUES(" + bluePrint.Project.Id + @", '" + bluePrint.Name + @"', '" + bluePrint.Description + @"', '" + bluePrint.Url + @"')";

            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl ved forsøg på at oprette et nyt tilbud.", "Databasefejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dbAnswer;
        }

        /// <summary>
        /// Method, that creates sqlQuery to update status of a IttLetterReceiver entry in Db
        /// </summary>
        /// <param name="receiver">IttLetterReceiver</param>
        /// <returns>string</returns>
        private string CreateUpdateBluePrintSqlQuery(BluePrint bluePrint)
        {
            //UPDATE [dbo].[BluePrints] SET [Project] = <Project, int),>, [Name] = <Name, nvarchar(50),>, [Description] = <Description, nvarchar(255),>, [Url] = <Url, nvarchar(50),> WHERE [Id] = <Id, int>;
            return @"UPDATE[dbo].[BluePrints] SET [Project] = " + bluePrint.Project.Id + @", [Name] = '" + bluePrint.Name + @"', [Description] = '" + bluePrint.Description + @"', [Url] = '" + bluePrint.Url + @"' WHERE[Id] = " + bluePrint.Id;
        }

        /// <summary>
        /// Method, that deletes a IttLetterReceiver from Db
        /// </summary>
        /// <param name="id">int</param>
        public void DeleteFromBluePrints(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Retrieves a list of IttLetterReceiver from Db
        /// </summary>
        /// <returns>List<IttLetterReceiver></returns>
        public List<BluePrint> GetBluePrints()
        {
            List<string> results = executor.ReadListFromDataBase("DescriptionList");
            List<BluePrint> result = new List<BluePrint>();
            foreach (string line in results)
            {
                string[] lineArray = new string[5];
                lineArray = line.Split(';');
                BluePrint blueprint = new BluePrint(strConnection, Convert.ToInt32(lineArray[0]), Convert.ToInt32(lineArray[1]), lineArray[2], lineArray[3], lineArray[4]);
                result.Add(blueprint);
            }
            return result;
        }

        public List<BluePrint> GetBluePrints(int projectId)
        {
            List<BluePrint> bluePrints = GetBluePrints();
            List<BluePrint> result = new List<BluePrint>();
            foreach (BluePrint bluePrint in bluePrints)
            {
                if (bluePrint.Project.Id == projectId)
                {
                    result.Add(bluePrint);
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that sets id, if id == 0
        /// </summary>
        /// <param name="id">int</param>
        public void SetId(int id)
        {
            if (int.TryParse(id.ToString(), out int parsedId) && this.id == 0 && parsedId >= 1)
            {
                this.id = parsedId;
            }
            else
            {
                this.id = 0;
            }
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Method, that updates sent status for an IttLetterReceiver in Db
        /// </summary>
        /// <param name="bluePrint">IttLetterReceiver</param>
        /// <returns>bool</returns>
        public bool UpdateBluePrint(BluePrint bluePrint)
        {
            bool result;
            string strSql = CreateUpdateBluePrintSqlQuery(bluePrint);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public Project Project { get; set; }

        public string Name
        {
            get { return name; }
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

        public string Description
        {
            get { return description; }
            set
            {
                try
                {
                    description = value;
                }
                catch (Exception)
                {
                    description = "";
                }
            }
        }

        public string Url
        {
            get { return url; }
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

        #endregion

    }
}