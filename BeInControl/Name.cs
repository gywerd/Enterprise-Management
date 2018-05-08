using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Name
    {
        #region Fields
        private int nameId;
        private string givenName;
        private string surName;
        #endregion

        #region constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Name() { }

        /// <summary>
        /// Constructor to add new name
        /// </summary>
        /// <param name="givenName">string</param>
        /// <param name="surName">string</param>
        public Name(string givenName, string surName)
        {
            this.givenName = givenName;
            this.surName = surName;
        }

        /// <summary>
        /// Constructor to add name from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="givenName">string</param>
        /// <param name="surName">string</param>
        public Name(int id, string givenName, string surName)
        {
            this.nameId = id;
            this.givenName = givenName;
            this.surName = surName;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string tempName;
            tempName = givenName + " " + surName;
            return tempName;
        }
        #endregion

        #region Fields
        public int NameId { get => nameId; }
        public string GivenName { get => givenName; set => givenName = value; }
        public string SurName { get => surName; set => surName = value; }
        #endregion
    }
}
