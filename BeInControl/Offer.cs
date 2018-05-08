using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Offer
    {
        #region Fields
        private int fieldId;
        private bool request;
        private DateTime requestDate;
        private bool received;
        private DateTime receivedDate;
        private bool chosen;
        private double price;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Offer() { }

        /// <summary>
        /// Constructor used for new offer with all data
        /// </summary>
        /// <param name="request">bool</param>
        /// <param name="requestDate">DateTime</param>
        /// <param name="received">bool</param>
        /// <param name="receivedDate">DateTime</param>
        /// <param name="chosen">bool</param>
        /// <param name="price">double</param>
        public Offer(bool request, DateTime requestDate, bool received, DateTime receivedDate, bool chosen, double price)
        {
            this.request = request;
            this.requestDate = requestDate;
            this.received = received;
            this.receivedDate = receivedDate;
            this.chosen = chosen;
            this.price = price;
        }

        /// <summary>
        /// Constructor used to add record from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="request">bool</param>
        /// <param name="requestDate">DateTime</param>
        /// <param name="received">bool</param>
        /// <param name="receivedDate">DateTime</param>
        /// <param name="chosen">bool</param>
        /// <param name="price">double</param>
        public Offer(int id, bool request, DateTime requestDate, bool received, DateTime receivedDate, bool chosen, double price)
        {
            this.fieldId = id;
            this.request = request;
            this.requestDate = requestDate;
            this.received = received;
            this.receivedDate = receivedDate;
            this.chosen = chosen;
            this.price = price;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Methods that adds request info to record
        /// </summary>
        /// <param name="request">bool</param>
        /// <param name="requestDate">DateTime</param>
        public void AddRequest(bool request, DateTime requestDate)
        {
            this.request = request;
            this.requestDate = requestDate;
        }

        public void AddReceived(bool received, DateTime receivedDate)
        {
            this.received = received;
            this.receivedDate = receivedDate;
        }
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
        #endregion

        #region Properties
        public int FieldId { get => fieldId; }
        public bool Request { get => request; }
        public DateTime RequestDate { get => requestDate; }
        public bool Received { get => received; }
        public DateTime ReceivedDate { get => receivedDate; }
        public bool Chosen { get => chosen; }
        public double Price { get => price; set => price = value; }
        #endregion
    }
}