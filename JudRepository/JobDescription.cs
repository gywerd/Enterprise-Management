using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class JobDescription
    {
        #region Fields
        private int id;
        private string occupation;
        private string area;
        private bool procuration;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public JobDescription(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.occupation = "";
            this.area = "";
            this.procuration = false;
        }

        /// <summary>
        /// Constructor for adding a new job descripton
        /// </summary>
        /// <param name="occupation">string</param>
        /// <param name="area">string</param>
        /// <param name="procuration">bool</param>
        public JobDescription(string strCon, string occupation, string area, bool procuration = false)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.occupation = occupation;
            this.area = area;
            this.procuration = procuration;
        }

        /// <summary>
        /// Constructor for adding a job descripton from Db to List
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="occupation">string</param>
        /// <param name="area">string</param>
        /// <param name="procuration">bool</param>
        public JobDescription(string strCon, int id, string occupation, string area, bool procuration = false)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.occupation = occupation;
            this.area = area;
            this.procuration = procuration;
        }

        /// <summary>
        /// Constructor for adding a job descripton from Db to List
        /// </summary>
        /// <param name="JobDescription">description</param>
        public JobDescription(string strCon, JobDescription description)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (description != null)
            {
                this.id = description.Id;
                this.occupation = description.Occupation;
                this.area = description.Area;
                this.procuration = description.Procuration;
            }
            else
            {
                this.id = 0;
                this.occupation = "";
                this.area = "";
                this.procuration = false;
            }
        }
        
        #endregion

        #region Methods
        public void ToggleProcuration()
        {
            if (procuration)
            {
                procuration = false;
            }
            else
            {
                procuration = true;
            }
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return occupation;
        }

        /// <summary>
        /// Retrieves a list of regions from Db
        /// </summary>
        /// <returns></returns>
        public List<JobDescription> GetJobDescriptions()
        {
            List<string> results = executor.ReadListFromDataBase("JobDescriptions");
            List<JobDescription> jobDescriptions = new List<JobDescription>();
            foreach (string result in results)
            {
                string[] resultArray = new string[4];
                resultArray = result.Split(';');
                JobDescription jobDescription = new JobDescription(strConnection, Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], Convert.ToBoolean(resultArray[3]));
                jobDescriptions.Add(jobDescription);
            }
            return jobDescriptions;
        }

        #endregion

        #region Properties
        public int Id { get => id; }
        public string Occupation { get => occupation; set => occupation = value; }
        public string Area { get => area; set => area = value; }
        public bool Procuration { get => procuration; }
        #endregion

    }
}
