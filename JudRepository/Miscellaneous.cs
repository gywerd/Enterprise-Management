using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JudRepository
{
    public class Miscellaneous
    {
        #region Fields
        private static string strConnection;
        private Executor executor;

        private int id;
        private Project project;
        private string text;

        private Project CPJ = new Project(strConnection);
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Miscellaneous(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = new Project(strConnection);
            this.text = "";
        }

        public Miscellaneous(string strCon, int enterprise, string text)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = CPJ.GetProject(enterprise);
            this.text = text;
        }

        public Miscellaneous(string strCon, int id, int enterprise, string text)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.project = CPJ.GetProject(enterprise);
            this.text = text;
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
            string result = @"DELETE FROM dbo.MiscellaneousList WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creates a new IttLetterReceiver in Db
        /// </summary>
        /// <param name="miscellaneus">Description</param>
        /// <returns>int</returns>
        public bool CreateDescriptionInDb(Miscellaneous miscellaneus)
        {
            bool dbAnswer = false;
            //List<Description> tempDescriptionList = new List<Description>();

            //INSERT INTO [dbo].[MiscellaneousList]([Project], [Text]) VALUES(<Enterprise, int,>, < Text, nvarchar(MAX),>)
            string strSql = @"INSERT INTO [dbo].[MiscellaneousList]([Project], [Text]) VALUES(" + miscellaneus.Project.Id + @", '" + miscellaneus.Text + @"')";

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
        private string CreateUpdateMiscellaneousSqlQuery(Miscellaneous miscellaneous)
        {
            //UPDATE [dbo].[MiscellaneousList] SET [Project] = <Project, int),>, [Text] = <Text, nvarchar(MAX),> WHERE [Id] = <Id, int>;
            return "UPDATE[dbo].[MiscellaneousList] SET[Project] = " + miscellaneous.Project.Id + ", [Text] = '" + miscellaneous.Text + "' WHERE[Id] = " + miscellaneous.Id;
        }

        /// <summary>
        /// Method, that deletes a IttLetterReceiver from Db
        /// </summary>
        /// <param name="id">int</param>
        public void DeleteFromMiscellaneousList(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Retrieves a list of IttLetterReceiver from Db
        /// </summary>
        /// <returns>List<IttLetterReceiver></returns>
        public List<Miscellaneous> GetMiscellaneusList()
        {
            List<string> results = executor.ReadListFromDataBase("MiscellaneusList");
            List<Miscellaneous> result = new List<Miscellaneous>();
            foreach (string line in results)
            {
                string[] lineArray = new string[3];
                lineArray = line.Split(';');
                Miscellaneous miscellaneous = new Miscellaneous(strConnection, Convert.ToInt32(lineArray[0]), Convert.ToInt32(lineArray[1]), lineArray[2]);
                result.Add(miscellaneous);
            }
            return result;
        }

        public List<Miscellaneous> GetMiscellaneusList(int projectId)
        {
            List<Miscellaneous> MiscellaneusList = GetMiscellaneusList();
            List<Miscellaneous> result = new List<Miscellaneous>();
            foreach (Miscellaneous miscellaneous in MiscellaneusList)
            {
                if (miscellaneous.Project.Id == projectId)
                {
                    result.Add(miscellaneous);
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
        public bool UpdateMiscellaneous(Miscellaneous miscellaneous)
        {
            bool result;
            string strSql = CreateUpdateMiscellaneousSqlQuery(miscellaneous);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get; }

        public Project Project { get; set; }
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