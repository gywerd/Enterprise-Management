using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class JobDescription
    {
        #region Fields
        private int jobDescriptionId;
        private string occupation;
        private string expertise;
        private bool procuration;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public JobDescription() { }

        /// <summary>
        /// Constructor for adding a new job descripton
        /// </summary>
        /// <param name="jobTitle">string</param>
        /// <param name="expertise">string</param>
        /// <param name="procuration">bool</param>
        public JobDescription(string jobTitle, string expertise, bool procuration = false)
        {
            this.occupation = jobTitle;
            this.expertise = expertise;
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
            this.expertise = expertise;
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
        #endregion

        #region Properties
        public int JobDescriptionId { get => jobDescriptionId; }
        public string Occupation { get => occupation; set => occupation = value; }
        public string Expertise { get => expertise; set => expertise = value; }
        public bool Procuration { get => procuration; }
        #endregion

    }
}
