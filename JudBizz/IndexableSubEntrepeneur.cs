using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class IndexableSubEntrepeneur : SubEntrepeneur
    {
        #region Fields
        int index;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IndexableSubEntrepeneur()
        {
        }

        /// <summary>
        /// Constructor, that adds an index to an existing SubEntrepeneur
        /// </summary>
        /// <param name="index"></param>
        /// <param name="subEntrepeneur"></param>
        public IndexableSubEntrepeneur(string strCon, List<LegalEntity> entrepeneurs, int index, SubEntrepeneur subEntrepeneur) : base(strCon, entrepeneurs, subEntrepeneur)
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
            string result = this.Name;
            return result;
        }

        #endregion

        #region Properties
        public int Index { get => index; }

        #endregion
    }
}
