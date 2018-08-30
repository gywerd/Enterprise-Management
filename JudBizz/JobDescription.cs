using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class JobDescription
    {
        #region Fields
        private int jobDescriptionId;
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
        public JobDescription() { }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public JobDescription(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor for adding a new job descripton
        /// </summary>
        /// <param name="jobTitle">string</param>
        /// <param name="expertise">string</param>
        /// <param name="procuration">bool</param>
        public JobDescription(string jobTitle, string expertise, bool procuration = false)
        {
            this.occupation = jobTitle;
            this.area = expertise;
            this.procuration = procuration;
        }
        /// <summary>
        /// Constructor for adding a job descripton from Db to List
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="jobTitle">string</param>
        /// <param name="expertise">string</param>
        /// <param name="procuration">bool</param>
        public JobDescription(int id, string jobTitle, string expertise, bool procuration = false)
        {
            this.jobDescriptionId = id;
            this.occupation = jobTitle;
            this.area = expertise;
            this.procuration = procuration;
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
                JobDescription jobDescription = new JobDescription(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], Convert.ToBoolean(resultArray[3]));
                jobDescriptions.Add(jobDescription);
            }
            return jobDescriptions;
        }

        #endregion

        #region Properties
        public int JobDescriptionId { get => jobDescriptionId; }
        public string Occupation { get => occupation; set => occupation = value; }
        public string Area { get => area; set => area = value; }
        public bool Procuration { get => procuration; }
        #endregion

    }
}
