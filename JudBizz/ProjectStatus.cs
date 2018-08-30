using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class ProjectStatus
    {
        #region Fields
        private int id;
        private string description;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public ProjectStatus() { }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public ProjectStatus(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        public ProjectStatus(int id, string description)
        {
            this.id = id;
            this.description = description;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return description;
        }

        /// <summary>
        /// Retrieves a list of project statuses from Db
        /// </summary>
        /// <returns></returns>
        public List<ProjectStatus> GetProjectStatusList()
        {
            List<string> results = executor.ReadListFromDataBase("ProjectStatusList");
            List<ProjectStatus> statuses = new List<ProjectStatus>();
            foreach (string result in results)
            {
                string[] resultArray = new string[2];
                resultArray = result.Split(';');
                ProjectStatus status = new ProjectStatus(Convert.ToInt32(resultArray[0]), resultArray[1]);
                statuses.Add(status);
            }
            return statuses;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public string Description
        {
            get => description;
            set
            {
                try
                {
                    if (value != null)
                    {
                        description = value;
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
