using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class IndexableEnterprise : Enterprise
    {
        #region Fields
        int index;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IndexableEnterprise(string strCon) : base(strCon) { }

        /// <summary>
        /// Constructor, that adds an index to an existing Enterprise
        /// </summary>
        /// <param name="index"></param>
        /// <param name="enterprise"></param>
        public IndexableEnterprise(string strCon, int index, Enterprise enterprise) : base(strCon, enterprise)
        {
            this.index = index;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that return main info as string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region Properties
        public int Index { get => index; }

        #endregion
    }
}
