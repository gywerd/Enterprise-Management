using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Request
    {
        #region Fields
        private int requestId;
        private bool sent;
        private DateTime? sentDate;
        private bool received;
        private DateTime? receivedDate;
        private bool cancellation;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Request()
        {
            this.sent = false;
            this.sentDate = null;
            this.received = false;
            this.receivedDate = null;
            this.cancellation = false;
        }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public Request(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add request from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="regionName">string</param>
        /// <param name="zips">string</param>
        public Request(int id, bool sent, bool received, bool cancellation, DateTime? sentDate = null, DateTime? receivedDate = null)
        {
            this.requestId = id;
            this.sent = sent;
            this.sentDate = sentDate;
            this.received = received;
            this.receivedDate = receivedDate;
            this.cancellation = cancellation;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that toggles value of sent
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
        /// Method, that toggles value of Active
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
        /// Method, that toggles value of Active
        /// </summary>
        public void ToggleCancellation()
        {
            if (cancellation)
            {
                cancellation = false;
            }
            else
            {
                cancellation = true;
            }
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (sent.Equals(true) && received.Equals(false))
            {
                string result = "Forespørgsel sendt: " + sentDate.Value.ToShortDateString();
                return result;
            }
            else if (sent.Equals(true) && received.Equals(true) && cancellation.Equals(true))
            {
                string result = "Forespørgsel annulleret: " + receivedDate.Value.ToShortDateString();
                return result;
            }
            else if (sent.Equals(true) && received.Equals(true) && cancellation.Equals(false))
            {
                string result = "Forespørgsel bekræftet: " + receivedDate.Value.ToShortDateString();
                return result;
            }
            else
            {
                string result = "Forespørgsel ikke sendt.";
                return result;
            }
        }

        /// <summary>
        /// Retrieves a list of regions from Db
        /// </summary>
        /// <returns></returns>
        public List<Request> GetRequests()
        {
            List<string> results = executor.ReadListFromDataBase("Requests");
            List<Request> requests = new List<Request>();
            foreach (string result in results)
            {
                string[] resultArray = new string[6];
                resultArray = result.Split(';');
                if (resultArray[2] == null && resultArray[4] == null)
                {
                    Request request = new Request(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToBoolean(resultArray[3]), Convert.ToBoolean(resultArray[5]));
                    requests.Add(request);
                }
                else if (resultArray[2] != null)
                {
                    Request request = new Request(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToBoolean(resultArray[3]), Convert.ToBoolean(resultArray[5]), Convert.ToDateTime(resultArray[2]));
                    requests.Add(request);
                }
                else
                {
                    Request request = new Request(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToBoolean(resultArray[3]), Convert.ToBoolean(resultArray[5]), Convert.ToDateTime(resultArray[2]), Convert.ToDateTime(resultArray[4]));
                    requests.Add(request);
                }
            }
            return requests;
        }

        #endregion

        #region Properties
        public int RequestId { get => requestId; }

        public bool Sent
        {
            get => sent;
            set
            {
                try
                {
                    if (value.Equals(true) || value.Equals(false))
                    {
                        sent = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DateTime? SentDate
        {
            get => sentDate;
            set
            {
                try
                {
                    if (value != null)
                    {
                        sentDate = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Received
        {
            get => received;
            set
            {
                try
                {
                    if (value.Equals(true) || value.Equals(false))
                    {
                        received = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DateTime? ReceivedDate
        {
            get => receivedDate;
            set
            {
                try
                {
                    if (value != null)
                    {
                        receivedDate = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Cancellation
        {
            get => cancellation;
            set
            {
                try
                {
                    if (value.Equals(true) || value.Equals(false))
                    {
                        cancellation = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

    }
}
