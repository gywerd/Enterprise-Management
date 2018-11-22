using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class RequestStatus
    {
        #region Fields
        private int id;
        private string description;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public RequestStatus(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.description = "";
        }

        /// <summary>
        /// Costructor used to add new RequestStatus
        /// </summary>
        /// <param name="description">string</param>
        public RequestStatus(string strCon, string description)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.description = description;
        }

        /// <summary>
        /// Costructor used to add RequestStatus from Db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        public RequestStatus(string strCon, int id, string description)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.description = description;
        }

        /// <summary>
        /// Costructor used to add RequestStatus from Db
        /// </summary>
        /// <param name="status">RequestStatus</param>
        public RequestStatus(string strCon, RequestStatus status)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (status != null)
            {
                this.id = status.Id;
                this.description = status.Description;
            }
            else
            {
                this.id = 0;
                this.description = "";
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Retrieves a list of project statuses from Db
        /// </summary>
        /// <returns></returns>
        public List<RequestStatus> GetRequestStatusList()
        {
            List<string> results = executor.ReadListFromDataBase("RequestStatusList");
            List<RequestStatus> statuses = new List<RequestStatus>();
            foreach (string result in results)
            {
                string[] resultArray = new string[2];
                resultArray = result.Split(';');
                RequestStatus status = new RequestStatus(strConnection, Convert.ToInt32(resultArray[0]), resultArray[1]);
                statuses.Add(status);
            }
            return statuses;
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return description;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public string Description
        {
            get => description;
            set
            {
                try
                {
                    if (value != null)
                    {
                        description = value;
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
