using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Enterprise
    {
        #region Fields
        private int enterpriseId;
        private string name;
        private int project;
        private string elaboration;
        private string tenderList;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Enterprise() { }

        /// <summary>
        /// Constructor for adding new Enterprise
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="project">int</param>
        /// <param name="elaboration">string</param>
        /// <param name="tenderList">string</param>
        public Enterprise(string name, int project, string elaboration = "", string tenderList = "")
        {
            this.name = name;
            this.project = project;
            this.elaboration = elaboration;
            this.tenderList = tenderList;
        }

        /// <summary>
        /// Constructor for adding new Enterprise
        /// </summary>
        /// <param name="EnterpriseId">int</param>
        /// <param name="name">string</param>
        /// <param name="project">int</param>
        /// <param name="elaboration">string</param>
        /// <param name="tenderList">string</param>
        public Enterprise(int id, string name, int project, string elaboration = "", string tenderList = "")
        {
            this.enterpriseId = id;
            this.name = name;
            this.project = project;
            this.elaboration = elaboration;
            this.tenderList = tenderList;
        }
        #endregion

        #region Methods

        #endregion

        #region Properties
        public int EnterpriseId { get => enterpriseId; }
        public string Name { get => name; set => name = value; }
        public int Project { get => project; set => project = value; }
        public string Elaboration { get => elaboration; set => elaboration = value; }
        public string TenderList { get => tenderList; set => tenderList = value; }
        #endregion
    }
}
