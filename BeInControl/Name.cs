using BicDataAccess;
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
        private string strConnection;
        private int nameId;
        private string givenName;
        private string surName;
        private Executor executor;
        #endregion

        #region constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Name() { }

        public Name(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

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

        public List<Name> GetNameList()
        {
            List<string> results = executor.ReadListFromDataBase("Names");
            List<Name> names = new List<Name>();
            foreach (string result in results)
            {
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                Name name = new Name(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2]);
                names.Add(name);
            }
            return names;
        }

        #endregion

        #region Fields
        public int NameId { get => nameId; }
        public string GivenName { get => givenName; set => givenName = value; }
        public string SurName { get => surName; set => surName = value; }
        #endregion
    }
}
