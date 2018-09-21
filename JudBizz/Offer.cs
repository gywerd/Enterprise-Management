using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class Offer
    {
        #region Fields
        private int id;
        private bool received;
        private DateTime? receivedDate;
        private double price;
        private bool chosen;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Offer()
        {
            this.received = false;
            this.receivedDate = null;
            this.price = 0;
            this.chosen = false;
        }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public Offer(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor used to add new offer with all data
        /// </summary>
        /// <param name="received">bool</param>
        /// <param name="receivedDate">DateTime?</param>
        /// <param name="price">double</param>
        /// <param name="chosen">bool</param>
        public Offer(bool received, DateTime? receivedDate, double price, bool chosen)
        {
            this.received = received;
            this.receivedDate = receivedDate;
            this.price = price;
            this.chosen = chosen;
        }

        /// <summary>
        /// Constructor used to add offer from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="received">bool</param>
        /// <param name="receivedDate">DateTime?</param>
        /// <param name="price">double</param>
        /// <param name="chosen">bool</param>
        public Offer(int id, bool received, double price, bool chosen, DateTime? receivedDate = null)
        {
            this.id = id;
            this.received = received;
            this.receivedDate = receivedDate;
            this.price = price;
            this.chosen = chosen;
        }

        /// <summary>
        /// Constructor used to add offer from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="received">bool</param>
        /// <param name="receivedDate">DateTime?</param>
        /// <param name="price">double</param>
        /// <param name="chosen">bool</param>
        public Offer(Offer offer)
        {
            this.id = offer.Id;
            this.received = offer.Received;
            this.receivedDate = offer.ReceivedDate;
            this.price = offer.Price;
            this.chosen = offer.Chosen;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to add received and received date to 
        /// </summary>
        /// <param name="receivedDate">DateTime?</param>
        public void AddReceived(DateTime? receivedDate = null)
        {
            if (receivedDate == null)
            {
                receivedDate = DateTime.Now;
            }
            this.received = true;
            this.receivedDate = receivedDate;
        }

        /// <summary>
        /// Method, that creste a Delete SQL-query
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.Offers WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creste a Update Offer ReceivedSQL-query
        /// </summary>
        /// <param name="sent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string CreateUpdateOfferReceivedSqlQuery(bool sent, int id, string date)
        {
            string query = "";

            //UPDATE [dbo].[Offers] SET [Status] = <Status, int,>,[ReceivedDate] = <ReceivedDate, date,> WHERE [Id] = <Id, int>;
            if (sent)
            {
                query = @"UPDATE [dbo].[Offers] SET [Received] = 'true', [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
            }
            else
            {
                query = @"UPDATE [dbo].[Offers] SET [Received] = 'false', [ReceivedDate] = '1899-12-31' WHERE [Id] = " + id.ToString();
            }
            return query;
        }

        /// <summary>
        /// Method, that deletes a row from Db
        /// </summary>
        /// <param name="id">int</param>
        public void DeleteFromOffers(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Retrieves a list of regions from Db
        /// </summary>
        /// <returns></returns>
        public List<Offer> GetOffers()
        {
            List<string> results = executor.ReadListFromDataBase("Offers");
            List<Offer> offers = new List<Offer>();
            foreach (string result in results)
            {
                string[] resultArray = new string[5];
                resultArray = result.Split(';');
                if (resultArray[2] != null)
                {
                    Offer offer = new Offer(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToDouble(resultArray[3]), Convert.ToBoolean(resultArray[4]), Convert.ToDateTime(resultArray[2]));
                    offers.Add(offer);
                }
                else
                {
                    Offer offer = new Offer(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToInt32(resultArray[3]), Convert.ToBoolean(resultArray[4]));
                    offers.Add(offer);
                }
            }
            return offers;
        }

        /// <summary>
        /// Method to add received and received date to 
        /// </summary>
        /// <param name="receivedDate">DateTime?</param>
        public void ResetReceived()
        {
            this.received = false;
            this.receivedDate = Convert.ToDateTime("1899-12-31");
        }

        /// <summary>
        /// Method to add received and received date to 
        /// </summary>
        /// <param name="receivedDate">DateTime?</param>
        public void SetReceivedDate(DateTime date)
        {
            this.receivedDate = date;
        }

        /// <summary>
        /// Method that toggles chosen state
        /// </summary>
        public void ToggleChosen()
        {
            if (chosen)
            {
                chosen = false;
            }
            else
            {
                chosen = true;
            }
        }

        /// <summary>
        /// Methods that toggles value of Received field
        /// </summary>
        public void ToggleReceived()
        {
            if (received)
            {
                received = false;
            }
            else
            {
                received = true;
            }
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (received)
            {
                string result = "Offer received: " + receivedDate.Value.ToShortDateString();
                return result;
            }
            else
            {
                string result = "Offer not received";
                return result;
            }
        }

        /// <summary>
        /// Method, that updates received status for an Offer in Db
        /// </summary>
        /// <param name="sent">bool</param>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool UpdateOfferReceived(int id, bool sent, string date)
        {
            bool result;
            string strSql = CreateUpdateOfferReceivedSqlQuery(sent, id, date);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }
        public bool Received { get => received; }
        public DateTime? ReceivedDate { get => receivedDate; }
        public double Price
        {
            get => price;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        price = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public bool Chosen { get => chosen; }
        #endregion
    }
}