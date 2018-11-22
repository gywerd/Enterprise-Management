using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace JudRepository
{
    public class Offer
    {
        #region Fields
        private int id;
        private bool received;
        private DateTime receivedDate;
        private double price;
        private bool chosen;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Offer(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.received = false;
            this.receivedDate = Convert.ToDateTime("1932-03-17");
            this.price = 0;
            this.chosen = false;
        }

        /// <summary>
        /// Constructor used to add new offer
        /// </summary>
        /// <param name="received">bool</param>
        /// <param name="receivedDate">DateTime?</param>
        /// <param name="price">double</param>
        /// <param name="chosen">bool</param>
        public Offer(string strCon, bool received, double price, bool chosen, DateTime receivedDate)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.received = received;
            if (received)
            {
                if (receivedDate.ToShortDateString().Substring(0, 10) != "17-03-1932")
                {
                    this.receivedDate = DateTime.Now;
                }
                else
                {
                    this.receivedDate = Convert.ToDateTime(receivedDate);
                }
            }
            else
            {
                    this.receivedDate = Convert.ToDateTime("1932-03-17");
            }
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
        public Offer(string strCon, int id, bool received, double price, bool chosen, DateTime receivedDate)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.received = received;
            this.receivedDate = Convert.ToDateTime(receivedDate);
            this.price = price;
            this.chosen = chosen;
        }

        /// <summary>
        /// Constructor, thats accepts an existing Offer
        /// </summary>
        /// <param name="offer">Offer</param>
        public Offer(string strCon, Offer offer)
        {
            if (offer != null)
            {
                strConnection = strCon;
                executor = new Executor(strConnection);

                this.id = offer.Id;
                this.received = offer.Received;
                this.receivedDate = offer.ReceivedDate;
                this.price = offer.Price;
                this.chosen = offer.Chosen;
            }
            else
        	{
                this.id = 0;
                this.received = false;
                this.receivedDate = Convert.ToDateTime("1932-03-17");
                this.price = 0;
                this.chosen = false;
            }
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
            if (receivedDate.Value.ToShortDateString().Substring(0, 10) != "17-03-1932")
            {
                this.received = true;
            }
            else
            {
                this.received = false;
            }
            if (this.receivedDate.ToShortDateString().Substring(0, 10) != receivedDate.Value.ToShortDateString().Substring(0, 10))
            {
                this.receivedDate = Convert.ToDateTime(receivedDate);
            }
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
        /// Method, that creates a new Offer in Db
        /// </summary>
        /// <param name="tempOffer">Offer</param>
        /// <returns>int</returns>
        public int CreateOfferInDb(Offer tempOffer)
        {
            int result = 0;
            int count = 0;
            bool dbAnswer = false;
            List<Offer> tempOffers = new List<Offer>();
            //INSERT INTO [dbo].[Offers]([Received], [ReceivedDate], [Price], [Chosen]) VALUES(<Received, bit,>, <ReceivedDate, date,>, <Price, money,>, <Chosen, bit,>)
            string tempReceivedDate = tempOffer.ReceivedDate.Year + "-" + tempOffer.ReceivedDate.Month + "-" + tempOffer.ReceivedDate.Day;
            string strSql = "INSERT INTO[dbo].[Offers]([Received], [ReceivedDate], [Price], [Chosen]) VALUES('" + tempOffer.Received + "', '" + tempReceivedDate + "', " + tempOffer.Price + ", '" + tempOffer.Chosen + "')";
            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl ved forsøg på at oprette et nyt tilbud.", "Databasefejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tempOffers = GetOffers();
            count = tempOffers.Count;
            result = tempOffers[count - 1].Id;
            return result;
        }

        /// <summary>
        /// Method, that create a Update Offer ReceivedSQL-query
        /// </summary>
        /// <param name="sent">bool</param>
        /// <param name="offer">Offer</param>
        /// <returns></returns>
        private string CreateUpdateOfferReceivedSqlQuery(Offer offer)
        {
            string query = "";

            //UPDATE [dbo].[Offers] SET [Status] = <Status, int,>,[ReceivedDate] = <ReceivedDate, date,>, [Price] = <Price, money,>, [Chosen] = <Chosen, bit,> WHERE [Id] = <Id, int>;
            if (offer.Received)
            {
                query = @"UPDATE [dbo].[Offers] SET [Received] = 'true', [ReceivedDate] = '" + offer.ReceivedDate + @"', [Price] = " + offer.Price.ToString() + ", [Chosen] = '" + offer.Chosen + "' WHERE [Id] = " + offer.Id.ToString();
            }
            else
            {
                query = @"UPDATE [dbo].[Offers] SET [Received] = 'false', [ReceivedDate] = '1932-03-17', [Price] = 0, [Chosen] = 'false' WHERE [Id] = " + id.ToString();
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
        /// Method, that fetches a list of Offers from Db
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
                Offer offer = new Offer(strConnection, Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToDouble(resultArray[3]), Convert.ToBoolean(resultArray[4]), Convert.ToDateTime(resultArray[2]));
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
            this.receivedDate = Convert.ToDateTime("1932-03-17");
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
                string result = "Offer received: " + receivedDate.ToShortDateString();
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
        /// <param name="offer">Offer</param>
        /// <returns>bool</returns>
        public bool UpdateOfferReceived(Offer offer)
        {
            bool result;
            string strSql = CreateUpdateOfferReceivedSqlQuery(offer);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }
        public bool Received { get => received; }
        public DateTime ReceivedDate { get => receivedDate; }
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