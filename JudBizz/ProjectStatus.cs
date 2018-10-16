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
        public ProjectStatus()
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.description = "";
        }

        /// <summary>
        /// Constructor add a new ProjectStatus
        /// </summary>
        /// <param name="description">string</param>
        public ProjectStatus(string description)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.description = description;
        }

        /// <summary>
        /// Constructor add a ProjectStatus from Db
        /// </summary>
        /// <param name="description"></param>
        public ProjectStatus(int id, string description)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = id;
            this.description = description;
        }

        /// <summary>
        /// Constructor add a ProjectStatus
        /// </summary>
        /// <param name="status">ProjectStatus</param>
        public ProjectStatus(ProjectStatus status)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            if (status != null)
            {
                this.id = status.Id;
                this.description = status.Description;
            }
            else
            {
                this.id = 0;
                this.description = "";
            }
        }

        #endregion

        #region Methods
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

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return description;
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
