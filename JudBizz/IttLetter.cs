using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JudBizz
{
    public class IttLetter
    {
        #region Fields
        private int id;
        private bool sent;
        private DateTime sentDate;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public IttLetter()
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.sent = false;
            this.sentDate = Convert.ToDateTime("1932-03-17");
        }

        /// <summary>
        /// Constructor for adding new ITT Letter
        /// </summary>
        /// <param name="sent">bool</param>
        /// <param name="sentDate">DateTime</param>
        public IttLetter(bool sent, DateTime sentDate)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.sent = sent;
            if (sent)
            {
                if (sentDate.ToShortDateString().Substring(0, 10) == "1932-03-17")
                {
                    this.sentDate = DateTime.Now;
                }
                else
                {
                    this.sentDate = sentDate;
                }
            }
            else
            {
                this.sentDate = Convert.ToDateTime("1932-03-17");
            }
        }

        /// <summary>
        /// Constructor for adding ITT Letter from Db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sent"></param>
        /// <param name="sentDate"></param>
        public IttLetter(int id, bool sent, DateTime sentDate)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = id;
            this.sent = sent;
            this.sentDate = sentDate;
        }

        /// <summary>
        /// Constructor for accepts an existing Itt Letter
        /// </summary>
        /// <param name="ittLetter">IttLetter</param>
        public IttLetter(IttLetter ittLetter)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            if (ittLetter != null)
            {
                this.id = ittLetter.Id;
                this.sent = ittLetter.Sent;
                this.sentDate = ittLetter.SentDate;
            }
            else
            {
                this.id = 0;
                this.sent = false;
                this.sentDate = Convert.ToDateTime("1932-03-17");
            }
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
        /// Method, that creates a new IttLetter in Db
        /// </summary>
        /// <param name="tempIttLetter">IttLetter</param>
        /// <returns>int</returns>
        public int CreateIttLetterInDb(IttLetter tempIttLetter)
        {
            int result = 0;
            bool dbAnswer = false;
            List<IttLetter> tempIttLetters = new List<IttLetter>();
            //INSERT INTO [dbo].[IttLetters]([Sent], [SentDate]) VALUES(<Sent, bit,>, <SentDate, date,>)
            string tempSentDate = tempIttLetter.SentDate.Year + "-" + tempIttLetter.SentDate.Month + "-" + tempIttLetter.SentDate.Day;
            string strSql = "INSERT INTO[dbo].[IttLetters]([Sent], [SentDate]) VALUES('" + tempIttLetter.Sent + "', '" + tempSentDate + "')";
            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl ved forsøg på at oprette et nyt tilbud.", "Databasefejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                tempIttLetters = GetIttLetters();
                result = tempIttLetters[tempIttLetters.Count - 1].Id;
            }
            return result;
        }

        /// <summary>
        /// Method, that creates sqlQuery to update status of a IttLetter entry in Db
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
                query = @"UPDATE [dbo].[IttLetters] SET [Sent] = 'false', [SentDate] = '1932-03-17' WHERE [Id] = " + id.ToString();
            }
            return query;
        }

        /// <summary>
        /// Method, that deletes a IttLetter from Db
        /// </summary>
        /// <param name="id">int</param>
        public void DeleteFromIttLetters(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Retrieves a list of IttLetters from Db
        /// </summary>
        /// <returns>List<IttLetter></returns>
        public List<IttLetter> GetIttLetters()
        {
            List<string> results = executor.ReadListFromDataBase("IttLetters");
            List<IttLetter> ittLetters = new List<IttLetter>();
            foreach (string result in results)
            {
                IttLetter ittLetter;
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                ittLetter = new IttLetter(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToDateTime(resultArray[2]));
                ittLetters.Add(ittLetter);
            }
            return ittLetters;
        }

        /// <summary>
        /// Method, that sets id, if id == 0
        /// </summary>
        public void SetId(int id)
        {
            try
            {
                if (this.id == 0 && id >= 1)
                {
                    this.id = id;
                }
            }
            catch (Exception)
            {
                this.id = 0;
            }
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

        public DateTime SentDate
        {
            get => sentDate;
            set
            {
                DateTime tempValue = Convert.ToDateTime("1932-03-17");

                try
                {
                    if (value != tempValue)
                    {
                        sentDate = value;
                    }
                    else
                    {
                        sentDate = tempValue;
                    }
                }
                catch (Exception)
                {
                    sentDate = tempValue;
                }
            }
        }

        #endregion
    }
}
