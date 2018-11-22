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
        public Request(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            DateTime date = Convert.ToDateTime("1932-03-17");
            this.status = 0;
            this.sentDate = date;
            this.receivedDate = date;
        }

        /// <summary>
        /// Constructor to add new Request
        /// </summary>
        /// <param name="status">bool</param>
        /// <param name="sentDate">DateTime?</param>
        /// <param name="receivedDate">DateTime?</param>
        public Request(string strCon, int status, DateTime sentDate, DateTime receivedDate)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.status = status;
            CheckDates(status, sentDate, receivedDate);
        }

        /// <summary>
        /// Constructor to add Request from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="status">bool</param>
        /// <param name="sentDate">DateTime?</param>
        /// <param name="receivedDate">DateTime?</param>
        public Request(string strCon, int id, int status, DateTime sentDate, DateTime receivedDate)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.status = status;
            this.sentDate = sentDate;
            this.receivedDate = receivedDate;
        }

        /// <summary>
        /// Constructor, that accepts data from existing Request
        /// </summary>
        /// <param name="request">Request</param>
        public Request(string strCon, Request request)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (request != null)
            {
                this.id = request.Id;
                this.status = request.Status;
                this.sentDate = request.SentDate;
                this.receivedDate = request.ReceivedDate;
            }
            else
            {
                this.id = 0;
                this.status = 0;
                DateTime date = Convert.ToDateTime("1932-03-17");
                this.sentDate = date;
                this.receivedDate = date;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method that checks a date
        /// If date is receivedDate: status = {1, 2, 3}
        /// If date is sentDate: status = {2, 3}
        /// </summary>
        /// <param name="date">DateTime</param>
        public void CheckDate(DateTime date)
        {
            if (date.ToShortDateString().Substring(0, 10) != "17-03-1932")
            {
                this.receivedDate = date;
            }
            else
            {
                this.receivedDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Method, that checks sentDate and receivedDate
        /// </summary>
        /// <param name="status">int</param>
        /// <param name="sentDate">DateTime</param>
        /// <param name="receivedDate">DateTime</param>
        private void CheckDates(int status, DateTime sentDate, DateTime receivedDate)
        {
            switch (status)
            {
                case 0:
                    this.receivedDate = Convert.ToDateTime("1932-03-17");
                    this.sentDate = Convert.ToDateTime("1932-03-17");
                    break;
                case 1:
                    CheckDate(receivedDate);
                    this.sentDate = Convert.ToDateTime("1932-03-17");
                    break;
                case 2:
                    CheckDate(receivedDate);
                    CheckDate(sentDate);
                    break;
                case 3:
                    CheckDate(receivedDate);
                    CheckDate(sentDate);
                    break;
            }
        }

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
                        MessageBox.Show("Databasen returnerede en fejl ved forsøg på at oprette en ny forespørgsel.", "Databasefejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Request request = new Request(strConnection, Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), Convert.ToDateTime(resultArray[2]), Convert.ToDateTime(resultArray[3]));
                requests.Add(request);
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
