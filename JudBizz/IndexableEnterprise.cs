using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
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
        public IndexableEnterprise() { }

        /// <summary>
        /// Constructor, that adds an index to an existing Enterprise
        /// </summary>
        /// <param name="index"></param>
        /// <param name="enterprise"></param>
        public IndexableEnterprise(int index, Enterprise enterprise) : base(enterprise)
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
        public int Index
        {
            get => index;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        index = value;
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
