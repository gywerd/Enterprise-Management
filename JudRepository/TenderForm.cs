using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class TenderForm
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
        public TenderForm(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.description = "";
        }

        /// <summary>
        /// Method to add a new Tender Form
        /// </summary>
        /// <param name="description">string</param>
        public TenderForm(string strCon, string description)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.description = description;
        }

        /// <summary>
        /// Method to add a Tender Form from Db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        public TenderForm(string strCon, int id, string description)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.description = description;
        }

        /// <summary>
        /// Method to add a Tender Form
        /// </summary>
        /// <param name="form">TenderForm</param>
        public TenderForm(string strCon, TenderForm form)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (form != null)
            {
                this.id = form.Id;
                this.description = form.Description;
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
        /// Retrieves a list of TenderForms from Db
        /// </summary>
        /// <returns></returns>
        public List<TenderForm> GetTenderForms()
        {
            List<string> results = executor.ReadListFromDataBase("TenderForms");
            List<TenderForm> statuses = new List<TenderForm>();
            foreach (string result in results)
            {
                string[] resultArray = new string[2];
                resultArray = result.Split(';');
                TenderForm status = new TenderForm(strConnection, Convert.ToInt32(resultArray[0]), resultArray[1]);
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
    }
    #endregion
}