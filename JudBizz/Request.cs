using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class Request
    {
        #region Fields
        private int id;
        private int status;
        private DateTime? sentDate;
        private DateTime? receivedDate;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Request()
        {
            this.status = 0;
            this.sentDate = null;
            this.receivedDate = null;
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
        /// Constructor, that accepts data from existing Request
        /// </summary>
        /// <param name="strCon">string</param>
        public Request(Request request)
        {
            this.id = request.Id;
            this.status = request.Status;
            this.sentDate = request.SentDate;
            this.receivedDate = request.ReceivedDate;
        }

        /// <summary>
        /// Constructor to add request from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="regionName">string</param>
        /// <param name="zips">string</param>
        public Request(int id, int status, DateTime? sentDate = null, DateTime? receivedDate = null)
        {
            this.id = id;
            this.status = status;
            this.sentDate = sentDate;
            this.receivedDate = receivedDate;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that create Delete SQL-query
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.Requests WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creates sqlQuery to update status of a Request entry in Db
        /// </summary>
        /// <param name="status">int</param>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateUpdateStatusSqlQuery(int status, int id, string date)
        {
            //UPDATE [dbo].[Requests] SET [Status] = <Status, int,>,[SentDate] = <SentDate, date,>,[ReceivedDate] = <ReceivedDate, date,> WHERE [Id] = <Id, int>
            string query = "";

            switch (status)
            {
                case 0:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 0, [SentDate] = '1899-12-31, [ReceivedDate] = '1899-12-31' WHERE [Id] = " + id.ToString();
                    break;
                case 1:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 1, [SentDate] = '" + date + @"', [ReceivedDate] = '1899-12-31' WHERE [Id] = " + id.ToString();
                    break;
                case 2:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 2, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
                case 3:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 3, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
                case 4:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 4, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
                case 5:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 5, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString() + @";";
                    break;
                case 6:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 6, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString() + @";";
                    break;
                case 7:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 7, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString() + @";";
                    break;
                case 8:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 8, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString() + @";";
                    break;
                case 9:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 9, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString() + @";";
                    break;
                case 10:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 10, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString() + @";";
                    break;
            }
            return query;
        }

        public void DeleteFromRequests(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (status)
            {
                case 0:
                    return "Forespørgsel ikke sendt.";
                case 1:
                    return "Forespørgsel sendt: " + sentDate.Value.ToShortDateString();
                case 2:
                    return "Forespørgsel bekræftet: " + receivedDate.Value.ToShortDateString();
                case 3:
                    return "Forespørgsel annulleret: " + receivedDate.Value.ToShortDateString();
            }
            return "";
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
                if (resultArray[2] == null && resultArray[3] == null)
                {
                    Request request = new Request(Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]));
                    requests.Add(request);
                }
                else if (resultArray[2] != null && resultArray[3] == null)
                {
                    Request request = new Request(Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), Convert.ToDateTime(resultArray[2]));
                    requests.Add(request);
                }
                else
                {
                    Request request = new Request(Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), Convert.ToDateTime(resultArray[2]), Convert.ToDateTime(resultArray[3]));
                    requests.Add(request);
                }
            }
            return requests;
        }

        /// <summary>
        /// Method, that updates a status of a Request entry in Db
        /// </summary>
        /// <param name="status">int</param>
        /// <param name="id">int</param>
        /// <param name="date">string</param>
        /// <returns>bool</returns>
        public bool UpdateRequestStatus(int status, int id, string date)
        {
            bool result;
            string strSql = CreateUpdateStatusSqlQuery(status, id, date);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public int Status
        {
            get => status;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        status = value;
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
                catch (Exception)
                {
                    receivedDate = null;
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
                catch (Exception)
                {
                    receivedDate = null;
                }
            }
        }

        #endregion

    }
}
