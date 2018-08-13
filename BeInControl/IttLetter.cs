using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class IttLetter
    {
        #region Fields
        private int ittLetterId;
        private bool sent;
        private DateTime sentDate;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public IttLetter() { }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public IttLetter(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor for adding new ITT letter
        /// </summary>
        /// <param name="sent">bool</param>
        /// <param name="sentDate">DateTime</param>
        public IttLetter(bool sent, DateTime sentDate)
        {
            this.sent = sent;
            this.sentDate = sentDate;
        }

        /// <summary>
        /// Constructor for adding ITT letter from Db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sent"></param>
        /// <param name="sentDate"></param>
        public IttLetter(int id, bool sent, DateTime sentDate)
        {
            this.ittLetterId = id;
            this.sent = sent;
            this.sentDate = sentDate;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (sent)
            {
                string result = "Tilbudsbrev sendt: " + sentDate.ToShortDateString();
                return result;
            }
            else
            {
                string result = "Tilbudsbrev ikke sendt";
                return result;
            }
        }

        /// <summary>
        /// Retrieves a list of regions from Db
        /// </summary>
        /// <returns></returns>
        public List<IttLetter> GetIttLetters()
        {
            List<string> results = executor.ReadListFromDataBase("IttLetters");
            List<IttLetter> ittLetters = new List<IttLetter>();
            foreach (string result in results)
            {
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                IttLetter ittLetter = new IttLetter(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToDateTime(resultArray[2]));
                ittLetters.Add(ittLetter);
            }
            return ittLetters;
        }

        #endregion

        #region Properties
        public int IttLetterId { get => ittLetterId; }
        public bool Sent { get => sent; }
        public DateTime SentDate { get => sentDate; set => sentDate = value; }
        #endregion
    }
}
