using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Project
    {
        #region Fields
        private int projectId;
        private int status;
        private int tenderForm;
        private int enterpriseForm;
        private int offerExecutive;
        private bool copy;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Project() { }

        /// <summary>
        /// Constructor to add new project
        /// </summary>
        /// <param name="status">int</param>
        /// <param name="tenderForm">int</param>
        /// <param name="enterpriseForm">int</param>
        /// <param name="offerExecutive">int</param>
        /// <param name="copy">bool</param>
        public Project(int status, int tenderForm, int enterpriseForm, int offerExecutive, bool copy = false)
        {
            this.status = status;
            this.tenderForm = tenderForm;
            this.enterpriseForm = enterpriseForm;
            this.offerExecutive = offerExecutive;
            this.copy = copy;
        }

        /// <summary>
        /// Constructor to add project from Db to List
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="status">int</param>
        /// <param name="tenderForm">int</param>
        /// <param name="enterpriseForm">int</param>
        /// <param name="offerExecutive">int</param>
        /// <param name="copy">bool</param>
        public Project(int id, int status, int tenderForm, int enterpriseForm, int offerExecutive, bool copy = false)
        {
            this.projectId = id;
            this.status = status;
            this.tenderForm = tenderForm;
            this.enterpriseForm = enterpriseForm;
            this.offerExecutive = offerExecutive;
            this.copy = copy;
        }
        #endregion

        #region Methods
        public void ToggleCopy()
        {
            if (copy)
            {
                copy = false;
            }
            else
            {
                copy = true;
            }
        }
        #endregion

        #region Properties
        public int ProjectId { get => projectId; }
        public int Status { get => status; set => status = value; }
        public int TenderForm { get => tenderForm; set => tenderForm = value; }
        public int EnterpriseForm { get => enterpriseForm; set => enterpriseForm = value; }
        public int OfferExecutive { get => offerExecutive; set => offerExecutive = value; }
        public bool Copy { get => copy; }
        #endregion
    }
}
