using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JudRepository
{
    public class TimeSchedule
    {
        #region Fields
        private static string strConnection;
        private Executor executor;

        private int id;
        private Project project;
        private string text;

        Project CPJ = new Project(strConnection);

        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public TimeSchedule(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = new Project(strConnection);
            this.text = "";
        }

        public TimeSchedule(string strCon, Project project, string text)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = project;
            this.text = text;
        }

        public TimeSchedule(string strCon, int id, int projectId, string text)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.project = CPJ.GetProject(projectId);
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
            string result = @"DELETE FROM dbo.DescriptionList WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creates a new IttLetterReceiver in Db
        /// </summary>
        /// <param name="timeSchedule">Description</param>
        /// <returns>int</returns>
        public bool CreateTimeScheduleInDb(TimeSchedule timeSchedule)
        {
            bool dbAnswer = false;
            //List<Description> tempDescriptionList = new List<Description>();

            //INSERT INTO [dbo].[TimeSchedules]([Project], [Text]) VALUES(<Enterprise, int,>, < Text, nvarchar(MAX),>)
            string strSql = @"INSERT INTO [dbo].[TimeSchedules]([Project], [Text]) VALUES(" + timeSchedule.Project.Id + @", '" + timeSchedule.Text + @"')";

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
        private string CreateUpdateTimeScheduleSqlQuery(TimeSchedule timeSchedule)
        {
            //UPDATE [dbo].[TimeSchedules] SET [Project] = <Project, int),>, [Text] = <Text, nvarchar(MAX),> WHERE [Id] = <Id, int>;
            return "UPDATE[dbo].[TimeSchedules] SET[Project] = " + timeSchedule.Project.Id + ", [Text] = '" + timeSchedule.Text + "' WHERE[Id] = " + timeSchedule.Id;
        }

        /// <summary>
        /// Method, that deletes a IttLetterReceiver from Db
        /// </summary>
        /// <param name="timeScheduleId">int</param>
        public void DeleteFromTimeSchedules(int timeScheduleId)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(timeScheduleId);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Retrieves a list of IttLetterReceiver from Db
        /// </summary>
        /// <returns>List<IttLetterReceiver></returns>
        public List<TimeSchedule> GetTimeSchedules()
        {
            List<String> timeSchedules = executor.ReadListFromDataBase("TimeSchedules");
            List<TimeSchedule> result = new List<TimeSchedule>();
            foreach (string line in timeSchedules)
            {
                string[] lineArray = new string[3];
                lineArray = line.Split(';');
                TimeSchedule timeSchedule = new TimeSchedule(strConnection, Convert.ToInt32(lineArray[0]), Convert.ToInt32(lineArray[1]), lineArray[2]);
                result.Add(timeSchedule);
            }
            return result;
        }

        public List<TimeSchedule> GetTimeSchedules(int projectId)
        {
            List<TimeSchedule> timeSchedules = new List<TimeSchedule>();
            List<TimeSchedule> result = new List<TimeSchedule>();

            foreach (TimeSchedule timeSchedule in timeSchedules)
            {
                if (timeSchedule.Project.Id == projectId)
                {
                    result.Add(timeSchedule);
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
        /// <param name="timeSchedule">IttLetterReceiver</param>
        /// <returns>bool</returns>
        public bool UpdateTimeSchedule(TimeSchedule timeSchedule)
        {
            bool result;
            string strSql = CreateUpdateTimeScheduleSqlQuery(timeSchedule);
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