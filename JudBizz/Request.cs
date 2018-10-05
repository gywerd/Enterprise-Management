using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JudBizz
{
    public class Request
    {
        #region Fields
        private int id;
        private int status;
        private DateTime sentDate;
        private DateTime receivedDate;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Request()
        {
            DateTime date = Convert.ToDateTime("1932-03-17");
            this.status = 0;
            this.sentDate = date;
            this.receivedDate = date;
        }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public Request(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            DateTime date = Convert.ToDateTime("1932-03-17");
            this.status = 0;
            this.sentDate = date;
            this.receivedDate = date;
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
        /// Constructor to add new Request
        /// </summary>
        /// <param name="status">bool</param>
        /// <param name="sentDate">DateTime?</param>
        /// <param name="receivedDate">DateTime?</param>
        public Request(int status, DateTime? sentDate = null, DateTime? receivedDate = null)
        {
            this.status = status;
            if (sentDate == null)
            {
                if (status <= 1)
                {
                    sentDate = Convert.ToDateTime("1932-03-17");
                }
                else
                {
                    sentDate = DateTime.Now;
                }
            }
            this.sentDate = Convert.ToDateTime(sentDate);
            if (receivedDate == null)
            {
                if (status <= 1)
                {
                    receivedDate = Convert.ToDateTime("1932-03-17");
                }
                else
                {
                    receivedDate = DateTime.Now;
                }
            }
            this.receivedDate = Convert.ToDateTime(receivedDate);
        }

        /// <summary>
        /// Constructor to add Request from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="regionName">string</param>
        /// <param name="zips">string</param>
        public Request(int id, int status, DateTime? sentDate = null, DateTime? receivedDate = null)
        {
            this.id = id;
            this.status = status;
            if (sentDate == null)
            {
                if (status <= 1)
                {
                    sentDate = Convert.ToDateTime("1932-03-17");
                }
                else
                {
                    sentDate = DateTime.Now;
                }
            }
            this.sentDate = Convert.ToDateTime(sentDate);
            if (receivedDate == null)
            {
                if (status <= 1)
                {
                    receivedDate = Convert.ToDateTime("1932-03-17");
                }
                else
                {
                    receivedDate = DateTime.Now;
                }
            }
            this.receivedDate = Convert.ToDateTime(receivedDate);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates a new Request in Db
        /// </summary>
        /// <param name="tempRequest">Request</param>
        /// <returns>int</returns>
        public int CreateRequestInDb(Request tempRequest)
        {
            int result = 0;
            int count = 0;
            bool dbAnswer = false;
            List<Request> tempRequests = new List<Request>();
            //INSERT INTO [dbo].[Requests]([Status], [SentDate], [ReceivedDate]) VALUES(< Status, int,>, < SentDate, date,>, < ReceivedDate, date,>)
            string strSql = "INSERT INTO[dbo].[Requests]([Status], [SentDate], [ReceivedDate]) VALUES(0, '1932-03-17', '1932-03-17')";
            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl ved forsøg på at oprette en ny forespørgsel.", "Databasefejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            tempRequests = GetRequests();
            count = tempRequests.Count;
            result = tempRequests[count - 1].Id;
            return result;
        }

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
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 0, [SentDate] = '1932-03-17', [ReceivedDate] = '1932-03-17' WHERE [Id] = " + id.ToString();
                    break;
                case 1:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 1, [SentDate] = '" + date + @"', [ReceivedDate] = '1932-03-17' WHERE [Id] = " + id.ToString();
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
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 5, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
                case 6:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 6, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
                case 7:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 7, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
                case 8:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 8, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
                case 9:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 9, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
                case 10:
                    query = @"UPDATE [dbo].[Requests] SET [Status] = 10, [ReceivedDate] = '" + date + @"' WHERE [Id] = " + id.ToString();
                    break;
            }
            return query;
        }

        /// <summary>
        /// Method, that deletes row from Requests in Db
        /// </summary>
        /// <param name="id">int</param>
        public void DeleteFromRequests(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
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
                    return "Forespørgsel sendt: " + sentDate.ToShortDateString();
                case 2:
                    return "Forespørgsel bekræftet: " + receivedDate.ToShortDateString();
                case 3:
                    return "Forespørgsel annulleret: " + receivedDate.ToShortDateString();
            }
            return "";
        }

        /// <summary>
        /// Method, that updates a status of a Request entry in Db
        /// </summary>
        /// <param name="status">int</param>
        /// <param name="id">int</param>
        /// <param name="date">string</param>
        /// <returns>bool</returns>
        public bool UpdateRequestStatus(int status, int id, DateTime date)
        {
            bool result;
            string tempDay = date.Day.ToString();
            if (Convert.ToInt32(tempDay) < 10)
            {
                tempDay = "0" + tempDay;
            }
            string tempMonth = date.Month.ToString();
            if (Convert.ToInt32(tempMonth) < 10)
            {
                tempMonth = "0" + tempMonth;
            }
            string strDate = date.Year.ToString() + "-" + tempMonth + "-" + tempDay;
            string strSql = CreateUpdateStatusSqlQuery(status, id, strDate);
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

        public DateTime SentDate
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
                }
            }
        }

        public DateTime ReceivedDate
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
                }
            }
        }

        #endregion

    }
}
