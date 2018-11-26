using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JudRepository
{
    public class Description
    {
        #region Fields
        private static string strConnection;
        private Executor executor;

        private int id;
        private Project project;
        private Enterprise enterprise;
        private string text;

        private Project CPJ = new Project(strConnection);
        private Enterprise CEP = new Enterprise(strConnection);

        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Description(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = new Project(strConnection);
            this.enterprise = new Enterprise(strConnection);
            this.text = "";
        }

        public Description(string strCon, int project, int enterprise, string content)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = CPJ.GetProject(project);
            this.enterprise = CEP.GetEnterprise(enterprise);
            this.text = content;
        }

        public Description(string strCon, int id, int project, int enterprise, string content)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.project = CPJ.GetProject(project);
            this.enterprise = CEP.GetEnterprise(enterprise);
            this.text = content;
        }

        public Description(string strCon, Description description)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (description != null)
            {
                this.id = description.Id;
                this.project = description.Project;
                this.enterprise = description.Enterprise;
                this.text = description.Text;
            }
            else
            {
                this.id = 0;
                this.project = new Project(strConnection);
                this.enterprise = new Enterprise(strConnection);
                this.text = "";
            }

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
            string result = @"DELETE FROM dbo.DescriptionList WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creates a new IttLetterReceiver in Db
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns>int</returns>
        public bool CreateDescriptionInDb(Description description)
        {
            bool dbAnswer = false;
            //List<Description> tempDescriptionList = new List<Description>();

            //INSERT INTO [dbo].[DescriptionList]([Project], [Enterprise], [Text]) VALUES(<Project, int,>, <Enterprise, int,>, < Text, nvarchar(MAX),>)
            string strSql = @"INSERT INTO[dbo].[DescriptionList]([Project], [Enterprise], [Text]) VALUES(" + description.Project.Id + @", '" + description.Enterprise.Id + @", '" + description.Text + @"')";

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
        private string CreateUpdateDescriptionSqlQuery(Description description)
        {
            //UPDATE [dbo].[DescriptionList] SET [Project] = <Project, int),>, [Enterprise] = <Enterprise, int),>, [Text] = <Text, nvarchar(MAX),> WHERE [Id] = <Id, int>;
            return "UPDATE[dbo].[DescriptionList] SET [Project] = " + description.Project.Id + ", [Enterprise] = " + description.Enterprise.Id + ", [Text] = '" + description.Text + "' WHERE[Id] = " + description.Id;
        }

        /// <summary>
        /// Method, that deletes a IttLetterReceiver from Db
        /// </summary>
        /// <param name="id">int</param>
        public void DeleteFromDescriptionList(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Retrieves the IttLetter Receivers list from Db
        /// </summary>
        /// <returns>List<IttLetterReceiver></returns>
        public List<Description> GetDescriptionList()
        {
            List<string> results = executor.ReadListFromDataBase("DescriptionList");
            List<Description> result = new List<Description>();
            foreach (string line in results)
            {
                string[] lineArray = new string[3];
                lineArray = line.Split(';');
                Description description = new Description(strConnection, Convert.ToInt32(lineArray[0]), Convert.ToInt32(lineArray[1]), lineArray[2]);
                result.Add(description);
            }
            return result;
        }

        /// <summary>
        /// Retrieves an IttLetter Receivers List from Db
        /// </summary>
        /// <param name="projectId">int</param>
        /// <returns></returns>
        public List<Description> GetDescriptionList(int projectId)
        {
            List<Description> descriptionList = GetDescriptionList();
            List<Description> result = new List<Description>();
            foreach (Description description in descriptionList)
            {
                if (description.Project.Id == projectId)
                {
                    result.Add(description);
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
            return text;
        }

        /// <summary>
        /// Method, that updates sent status for an IttLetterReceiver in Db
        /// </summary>
        /// <param name="description">IttLetterReceiver</param>
        /// <returns>bool</returns>
        public bool UpdateDescription(Description description)
        {
            bool result;
            string strSql = CreateUpdateDescriptionSqlQuery(description);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get; }

        public Project Project { get; set; }
        public Enterprise Enterprise { get; set; }

        public string Text
        {
            get { return text; }
            set
            {
                try
                {
                    text = value.ToString();
                }
                catch (Exception)
                {
                    text = "";
                }
            }
        }

        #endregion

    }
}
