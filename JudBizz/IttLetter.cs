using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class IttLetter
    {
        #region Fields
        private int id;
        private bool sent;
        private DateTime? sentDate;

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
        public IttLetter(bool sent, DateTime? sentDate)
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
        public IttLetter(int id, bool sent, DateTime? sentDate)
        {
            this.id = id;
            this.sent = sent;
            this.sentDate = sentDate;
        }

        /// <summary>
        /// Constructor for adding ITT letter from Db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sent"></param>
        /// <param name="sentDate"></param>
        public IttLetter(IttLetter ittLetter)
        {
            this.id = ittLetter.Id;
            this.sent = ittLetter.Sent;
            this.sentDate = ittLetter.SentDate;
        }
        
        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates sqlQuery to delete a Request entry in Db
        /// </summary>
        /// <param name="sent">int</param>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.IttLetters WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creates sqlQuery to update status of a Request entry in Db
        /// </summary>
        /// <param name="sent">int</param>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateUpdateIttLetterSentSqlQuery(bool sent, int id, string date)
        {
            //UPDATE [dbo].[IttLetters] SET [Status] = <Status, int,>,[SentDate] = <ReceivedDate, date,> WHERE [Id] = <Id, int>;
            string query = "";
            if (sent)
            {
                query = @"UPDATE [dbo].[IttLetters] SET [Sent] = 'true', [SentDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
            }
            else
            {
                query = @"UPDATE [dbo].[IttLetters] SET [Sent] = 'false', [SentDate] = '1899-12-31' WHERE [Id] = " + id.ToString();
            }
            return query;
        }

        /// <summary>
        /// Method, that deletes a Request from Db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFromIttLetters(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
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
                IttLetter ittLetter;
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                if (resultArray[2] != "")
                {
                    ittLetter = new IttLetter(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToDateTime(resultArray[2]));
                }
                else
                {
                    ittLetter = new IttLetter(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), null);
                }
                ittLetters.Add(ittLetter);
            }
            return ittLetters;
        }

        /// <summary>
        /// Methods that toggles value of Sent field
        /// </summary>
        public void ToggleSent()
        {
            if (sent)
            {
                sent = false;
            }
            else
            {
                sent = true;
            }
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (sent)
            {
                string result = "Tilbudsbrev sendt: " + sentDate.Value.ToShortDateString();
                return result;
            }
            else
            {
                string result = "Tilbudsbrev ikke sendt";
                return result;
            }
        }

        /// <summary>
        /// Method, that updates sent status for an IttLetter in Db
        /// </summary>
        /// <param name="sent">bool</param>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool UpdateIttLetterSent(int id, bool sent, string date)
        {
            bool result;
            string strSql = CreateUpdateIttLetterSentSqlQuery(sent, id, date);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }
        public bool Sent { get => sent; }
        public DateTime? SentDate { get => sentDate; set => sentDate = value; }

        #endregion
    }
}
