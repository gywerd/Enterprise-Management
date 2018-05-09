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
        private string name;
        private int builder;
        private int status;
        private int enterpriseForm;
        private int executive;
        private string tenderForm;
        private bool copy;
        private bool active;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Project() { }

        /// <summary>
        /// Constructor to add new project
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="builder">int</param>
        /// <param name="status">int</param>
        /// <param name="enterpriseForm">int</param>
        /// <param name="executive">int</param>
        /// <param name="tenderForm">string</param>
        /// <param name="copy">bool</param>
        /// <param name="active">bool</param>
        public Project(string name, int builder, int status, int enterpriseForm, int executive, string tenderForm = "", bool copy = false, bool active = true)
        {
            this.status = status;
            this.name = name;
            this.builder = builder;
            this.tenderForm = tenderForm;
            this.enterpriseForm = enterpriseForm;
            this.executive = executive;
            this.copy = copy;
            this.active = active;
        }

        /// <summary>
        /// Constructor to add project from Db to List
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="name">string</param>
        /// <param name="builder">int</param>
        /// <param name="status">int</param>
        /// <param name="enterpriseForm">int</param>
        /// <param name="executive">int</param>
        /// <param name="tenderForm">int</param>
        /// <param name="copy">bool</param>
        /// <param name="active">bool</param>
        public Project(int id, string name, int builder, int status, int enterpriseForm, int executive, string tenderForm = "", bool copy = false, bool active = true)
        {
            this.projectId = id;
            this.name = name;
            this.builder = builder;
            this.status = status;
            this.enterpriseForm = enterpriseForm;
            this.executive = executive;
            this.tenderForm = tenderForm;
            this.copy = copy;
            this.active = active;
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
        public string TenderForm { get => tenderForm; set => tenderForm = value; }
        public int EnterpriseForm { get => enterpriseForm; set => enterpriseForm = value; }
        public int Executive { get => executive; set => executive = value; }
        public bool Copy { get => copy; }
        public string Name { get => name; set => name = value; }
        public int Builder { get => builder; set => builder = value; }
        public bool Active {  get => active; set => active = value; }
        #endregion
    }
}
