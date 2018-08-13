using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class EnterpriseForm
    {
        #region Fields
        private int formId;
        private string abbreviation;
        private string name;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public EnterpriseForm() { }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public EnterpriseForm(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add new EnterpriseForm
        /// </summary>
        /// <param name="abbreviation">string</param>
        /// <param name="name">string</param>
        public EnterpriseForm(string abbreviation, string name)
        {
            this.abbreviation = abbreviation;
            this.name = name;
        }

        /// <summary>
        /// Constructor to add Enterprise Form from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="abbreviation">string</param>
        /// <param name="name">string</param>
        public EnterpriseForm(int id, string abbreviation, string name)
        {
            this.formId = id;
            this.abbreviation = abbreviation;
            this.name = name;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Retrieves a list of Enterprise Forms from Db
        /// </summary>
        /// <returns></returns>
        public List<EnterpriseForm> GetEnterpriseFormList()
        {
            List<string> results = executor.ReadListFromDataBase("EnterpriseForm");
            List<EnterpriseForm> enterpriseForms = new List<EnterpriseForm>();
            foreach (string result in results)
            {
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                EnterpriseForm address = new EnterpriseForm(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2]);
                enterpriseForms.Add(address);
            }
            return enterpriseForms;
        }

        #endregion

        #region Properties
        public int FormId { get => formId; }

        public string Abbreviation
        {
            get => abbreviation;
            set
            {
                try
                {
                    if (value != null)
                    {
                        abbreviation = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                try
                {
                    if (value != null)
                    {
                        name = value;
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
