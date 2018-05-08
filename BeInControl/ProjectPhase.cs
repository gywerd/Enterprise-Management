using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class ProjectPhase
    {
        #region Fields
        private int projectPhaseId;
        private string phase;
        private string description;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public ProjectPhase() { }

        /// <summary>
        /// Constructor to add new project phase
        /// </summary>
        /// <param name="phase"></param>
        /// <param name="description"></param>
        public ProjectPhase(string phase, string description)
        {
            this.phase = phase;
            this.description = description;
        }

        /// <summary>
        /// Constructor to add project phase from Db to List
        /// </summary>
        /// <param name="phase"></param>
        /// <param name="description"></param>
        public ProjectPhase(int id, string phase, string description)
        {
            this.projectPhaseId = id;
            this.phase = phase;
            this.description = description;
        }
        #endregion

        #region Methods

        #endregion

        #region Properties
        public int ProjectPhaseId { get => projectPhaseId; }
        public string Phase { get => phase; set => phase = value; }
        public string Description { get => description; set => description = value; }
        #endregion
    }
}
