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
    public class IttLetterReceiver
    {
        #region Fields
        private int id = 0;
        private int shippingId = 0;
        private int project = 0;
        private string companyId = "";
        private string companyName = "";
        private string attention = "";
        private string street = "";
        private string place = "";
        private string zip = "";
        private string email = "";

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IttLetterReceiver(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.shippingId = 0;
            this.companyId = "";
            this.companyName = "";
            this.attention = "";
            this.street = "";
            this.place = "";
            this.zip = "";
        }

        /// <summary>
        /// Constructor for adding new ITT Letter Reciver
        /// </summary>
        /// <param name="shippingId">int</param>
        /// <param name="project">int</param>
        /// <param name="companyId">string</param>
        /// <param name="companyName">string</param>
        /// <param name="attention">string</param>
        /// <param name="street">string</param>
        /// <param name="zip">string</param>
        /// <param name="email">string</param>
        /// <param name="place">string</param>
        public IttLetterReceiver(string strCon, int shippingId, int project, string companyId, string companyName, string attention, string street, string zip, string email, string place = "")
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.shippingId = shippingId;
            this.project = project;
            this.companyId = companyId;
            this.companyName = companyName;
            this.attention = attention;
            this.street = street;
            this.place = place;
            this.zip = zip;
            this.email = email;
        }

        /// <summary>
        /// Constructor for adding ITT Letter Receiver from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="shippingId">int</param>
        /// <param name="project">int</param>
        /// <param name="companyId">string</param>
        /// <param name="companyName">string</param>
        /// <param name="attention">string</param>
        /// <param name="street">string</param>
        /// <param name="place">string</param>
        /// <param name="zip">string</param>
        /// <param name="email">string</param>
        public IttLetterReceiver(string strCon, int id, int shippingId, int project, string companyId, string companyName, string attention, string street, string place, string zip, string email)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.shippingId = shippingId;
            this.project = project;
            this.companyId = companyId;
            this.companyName = companyName;
            this.attention = attention;
            this.street = street;
            this.place = place;
            this.zip = zip;
            this.email = email;
        }

        /// <summary>
        /// Constructor that accepts data from existing Itt Letter Receiver
        /// </summary>
        /// <param name="receiver">IttLetterReceiver</param>
        public IttLetterReceiver(string strCon, IttLetterReceiver receiver)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (receiver != null)
            {
                this.id = receiver.Id;
                this.shippingId = receiver.shippingId;
                this.project = receiver.Project;
                this.companyId = receiver.companyId;
                this.companyName = receiver.companyName;
                this.attention = receiver.attention;
                this.street = receiver.Street;
                this.place = receiver.place;
                this.zip = receiver.zip;
                this.email = receiver.Email;
            }
            else
            {
                this.id = receiver.Id;
                this.shippingId = 0;
                this.companyId = "";
                this.companyName = "";
                this.attention = "";
                this.street = "";
                this.place = "";
                this.zip = "";
                this.email = "";
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates sqlQuery to delete a IttLetterReceiver entry in Db
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.IttLetterReceiverList WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creates a new IttLetterReceiver in Db
        /// </summary>
        /// <param name="receiver">IttLetterReceiver</param>
        /// <returns>int</returns>
        public bool CreateIttLetterReceiverInDb(IttLetterReceiver receiver)
        {
            bool dbAnswer = false;
            List<IttLetterReceiver> tempIttLetterReceivers = new List<IttLetterReceiver>();
            
            //INSERT INTO [dbo].[IttLetterReceivers]([ShippingId], [Project], [CompanyId], [CompanyName], [Attention], [Street], [Place], [Zip], [Email]) VALUES(<ShippingId, int,>, < Project, int,>, < CompanyId, nvarchar(255),>, < CompanyName, nvarchar(255),>, < Attention, nvarchar(50),>, < Street, nvarchar(50),>, < Place, nvarchar(50),>, < Zip, nvarchar(50),>, < Email, nvarchar(50),>)
            string strSql = "INSERT INTO[dbo].[IttLetterReceivers]([ShippingId], [Project], [CompanyId], [CompanyName], [Attention], [Street], [Place], [Zip], [Email]) VALUES(" + receiver.ShippingId + ", " + receiver.Project + ", '" + receiver.CompanyId + "', '" + receiver.CompanyName + "', '" + receiver.Attention + "', '" + receiver.Street + "', '" + receiver.Place + "', '" + receiver.Zip + "', '" + receiver.Email + "')";

            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl ved forsøg på at oprette et nyt tilbud.", "Databasefejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dbAnswer;
        }

        /// <summary>
        /// Method, that creates sqlQuery to update status of a IttLetterReceiver entry in Db
        /// </summary>
        /// <param name="receiver">IttLetterReceiver</param>
        /// <returns>string</returns>
        private string CreateUpdateIttLetterSentSqlQuery(IttLetterReceiver letter)
        {
            //UPDATE [dbo].[IttLetterReceiver] SET [CompanyId] = <CompanyId, nvarchar(10),>, [CompanyName] = <CompanyName, nvarchar(255),>, [Attention] = <Attention, nvarchar(50),>, [Street] = <Street, nvarchar(50),>, [Place] = <Place, nvarchar(50),>, [Zip] = <Zip, nvarchar(65),>, [Email] = <Email, nvarchar(255),> WHERE [Id] = <Id, int>;
            return "UPDATE[dbo].[IttLetterReceivers] SET[CompanyId] = '" + letter.CompanyId + "', [CompanyName] = '" + letter.CompanyName + "', [Attention] = '" + letter.Attention + "', [Street] = '" + letter.street + "', [Place] = '" + letter.Place + "', [Zip] = '" + letter.Zip + "', [Email] = '" + letter.Email + "' WHERE[Id] = " + letter.Id;
        }

        /// <summary>
        /// Method, that deletes a IttLetterReceiver from Db
        /// </summary>
        /// <param name="id">int</param>
        public void DeleteFromIttLetters(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Retrieves a list of IttLetterReceiver from Db
        /// </summary>
        /// <returns>List<IttLetterReceiver></returns>
        public List<IttLetterReceiver> GetIttLetterReceivers()
        {
            List<string> results = executor.ReadListFromDataBase("IttLetterReceivers");
            List<IttLetterReceiver> ittLetterReceivers = new List<IttLetterReceiver>();
            foreach (string result in results)
            {
                string[] resultArray = new string[10];
                resultArray = result.Split(';');
                IttLetterReceiver ittLetterReceiver = new IttLetterReceiver(strConnection, Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), Convert.ToInt32(resultArray[2]), resultArray[3], resultArray[4], resultArray[5], resultArray[6], resultArray[7], resultArray[8], resultArray[9]);
                ittLetterReceivers.Add(ittLetterReceiver);
            }
            return ittLetterReceivers;
        }

        /// <summary>
        /// Method, that sets id, if id == 0
        /// </summary>
        /// <param name="id">int</param>
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
        /// Returns main content as string with multiple rows
        /// </summary>
        /// <returns>string</returns>
        public string ToLongString()
        {
            string result = companyName + "\n" + attention + "\n" + street;
            if (place != "")
            {
                result += "\n" + place;
            }
            result += "\n" + zip;
            if (email != "")
            {
                result += "\n" + email;
            }
            return result;
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string result = companyName + ". " + attention + ". " + street;
            if (place != "")
            {
                result += ". " + place;
            }
            result += ". " + zip;
            if (email != "")
            {
                result += ". " + email;
            }
            result += ".";
            return result;
        }

        /// <summary>
        /// Method, that updates sent status for an IttLetterReceiver in Db
        /// </summary>
        /// <param name="receiver">IttLetterReceiver</param>
        /// <returns>bool</returns>
        public bool UpdateIttLetterReceiver(IttLetterReceiver receiver)
        {
            bool result;
            string strSql = CreateUpdateIttLetterSentSqlQuery(receiver);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public int ShippingId
        {
            get => shippingId;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        shippingId = value;
                    }
                    else
                        shippingId = 0;
                }
                catch (Exception)
                {
                    shippingId = 0;
                }
            }
        }

        public int Project
        {
            get => project;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        project = value;
                    }
                    else
                        project = 0;
                }
                catch (Exception)
                {
                    project = 0;
                }
            }
        }

        public string CompanyId
        {
            get => companyId;
            set
            {
                try
                {
                    if (value != null)
                    {
                        companyId = value;
                    }
                    else
                    {
                        companyId = "";
                    }
                }
                catch (Exception)
                {
                    companyId = "";
                }
            }
        }

        public string CompanyName
        {
            get => companyName;
            set
            {
                try
                {
                    if (value != null)
                    {
                        companyName = value;
                    }
                    else
                    {
                        companyName = "";
                    }
                }
                catch (Exception)
                {
                    companyName = "";
                }
            }
        }

        public string Attention
        {
            get => attention;
            set
            {
                try
                {
                    if (value != null)
                    {
                        attention = value;
                    }
                    else
                    {
                        attention = "";
                    }
                }
                catch (Exception)
                {
                    attention = "";
                }
            }
        }

        public string Street
        {
            get => street;
            set
            {
                try
                {
                    if (value != null)
                    {
                        street = value;
                    }
                    else
                    {
                        street = "";
                    }
                }
                catch (Exception)
                {
                    street = "";
                }
            }
        }

        public string Place
        {
            get => place;
            set
            {
                try
                {
                    if (value != null)
                    {
                        place = value;
                    }
                    else
                    {
                        place = "";
                    }
                }
                catch (Exception)
                {
                    place = "";
                }
            }
        }

        public string Zip
        {
            get => zip;
            set
            {
                try
                {
                    if (value != null)
                    {
                        zip = value;
                    }
                    else
                    {
                        zip = "";
                    }
                }
                catch (Exception)
                {
                    zip = "";
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                try
                {
                    if (value != null)
                    {
                        email = value;
                    }
                    else
                    {
                        email = "";
                    }
                }
                catch (Exception)
                {
                    email = "";
                }
            }
        }

        #endregion
    }
}
