﻿using JudDataAccess;
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
        private int offerId;
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
            this.price = Price;
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
            this.offerId = id;
            this.received = received;
            this.receivedDate = receivedDate;
            this.price = Price;
            this.chosen = chosen;
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

        public void DeleteFromOffers(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.Offers WHERE Id = " + id + ";";
            return result;
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
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (received)
            {
                string result = "Offer received: " + receivedDate.Value.ToString("d");
                return result;
            }
            else
            {
                string result = "Offer not received";
                return result;
            }
        }

        /// <summary>
        /// Retrieves a list of regions from Db
        /// </summary>
        /// <returns></returns>
        public List<Offer> GetOfferList()
        {
            List<string> results = executor.ReadListFromDataBase("Offers");
            List<Offer> offers = new List<Offer>();
            foreach (string result in results)
            {
                string[] resultArray = new string[4];
                resultArray = result.Split(';');
                if (resultArray[2] != null)
                {
                    Offer offer = new Offer(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToInt32(resultArray[3]), Convert.ToBoolean(resultArray[4]), Convert.ToDateTime(resultArray[2]));
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

        #endregion

        #region Properties
        public int OfferId { get => offerId; }
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