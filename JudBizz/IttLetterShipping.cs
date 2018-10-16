using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JudBizz
{
    public class IttLetterShipping
    {
        #region Fields
        int id = 0;
        int project = 0;
        string commonPdfPath = "";
        string pdfPath = "";


        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IttLetterShipping()
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.commonPdfPath = @"PDF_Documents\";
            this.pdfPath = "";
        }

        /// <summary>
        /// Constructor for adding new ITT Letter Shipping
        /// </summary>
        /// <param name="commmonPdfPath">string</param>
        /// <param name="pdfPath">string</param>
        public IttLetterShipping(string commmonPdfPath, string pdfPath)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.commonPdfPath = commmonPdfPath;
            this.pdfPath = pdfPath;
        }

        /// <summary>
        /// Constructor for adding ITT Letter Shipping from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="commmonPdfPath">string</param>
        /// <param name="pdfPath">string</param>
        public IttLetterShipping(int id, string commmonPdfPath, string pdfPath)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = id;
            this.commonPdfPath = commmonPdfPath;
            this.pdfPath = pdfPath;
        }

        /// <summary>
        /// Constructor for accepts an existing Itt Letter Shipping
        /// </summary>
        /// <param name="shipping">IttLetterShipping</param>
        public IttLetterShipping(IttLetterShipping shipping)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            if (shipping != null)
            {
                this.id = shipping.Id;
                this.commonPdfPath = shipping.CommonPdfPath;
                this.pdfPath = shipping.PdfPath;
            }
            else
            {
                this.id = 0;
                this.commonPdfPath = @"PDF_Documents\";
                this.pdfPath = "";
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates sqlQuery to delete a IttLetterShipping entry in Db
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.IttLetterShippingList WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that creates a new IttLetterShipping in Db
        /// </summary>
        /// <param name="shipping">IttLetterShipping</param>
        /// <returns>int</returns>
        public int CreateIttLetterShippingInDb(Bizz bizz, IttLetterShipping shipping)
        {
            int result = 0;
            bool dbAnswer = false;
            List<IttLetterShipping> tempIttLetterShippingList = new List<IttLetterShipping>();
            //INSERT INTO [dbo].[IttLetterShippingList]([CommonPdfPath], [PdfPath]) VALUES(<CommonPdfPath, nvarchar(50),>, <PdfPath, nvarchar(50)>)
            string strSql = "INSERT INTO[dbo].[IttLetterShippingList]([CommonPdfPath], [PdfPath]) VALUES('" + shipping.CommonPdfPath + "', '" + shipping.PdfPath + "')";
            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl. Forsendelsen blev ikke oprettet.", "Opret Forsendelse", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bizz.IttLetterShippingList.Clear();
                bizz.IttLetterShippingList = Bizz.CIS.GetIttLetterShippingList();
                tempIttLetterShippingList = GetIttLetterShippingList();
                result = GetIttLetterShippingId(bizz.IttLetterShippingList);
            }
            return result;
        }

        /// <summary>
        /// Method, that creates sqlQuery to update status of a IttLetterShipping entry in Db
        /// </summary>
        /// <param name="shipping">IttLetterShipping</param>
        /// <returns>string</returns>
        private string CreateUpdateIttLetterSentSqlQuery(IttLetterShipping shipping)
        {
            //UPDATE [dbo].[IttLetterShipping] SET [CommonPdfPath] = <CommonPdfPath, nvarchar(50),>,[PdfPath] = <PdfPath, nvarchar(50),> WHERE [Id] = <Id, int>;
            return "UPDATE[dbo].[IttLetterShipping] SET[CommonPdfPath] = '" + shipping.CommonPdfPath + "',[PdfPath] = '" + shipping.PdfPath + "' WHERE[Id] = " + shipping.Id;
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
        /// Method, that retrieves a shipping id 
        /// </summary>
        /// <returns>int</returns>
        private int GetIttLetterShippingId(List<IttLetterShipping> list)
        {
            int result = 0;
            foreach (IttLetterShipping temp in list)
            {
                if (temp.PdfPath == Bizz.MacAdresss)
                {
                    result = temp.Id;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Retrieves a list of IttLetterReceiver from Db
        /// </summary>
        /// <returns>List<IttLetterShipping></returns>
        public List<IttLetterShipping> GetIttLetterShippingList()
        {
            List<string> results = executor.ReadListFromDataBase("IttLetterShippingList");
            List<IttLetterShipping> ittLetterShippingList = new List<IttLetterShipping>();
            foreach (string result in results)
            {
                IttLetterShipping ittLetterShipping;
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                ittLetterShipping = new IttLetterShipping(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2]);
            }
            return ittLetterShippingList;
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
            return commonPdfPath + "\n" + pdfPath;
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return commonPdfPath + ", " + pdfPath;
        }

        /// <summary>
        /// Method, that updates sent status for an IttLetterShipping in Db
        /// </summary>
        /// <param name="shipping">IttLetterShipping</param>
        /// <returns>bool</returns>
        public bool UpdateIttLetterShipping(IttLetterShipping shipping)
        {
            bool result;
            string strSql = CreateUpdateIttLetterSentSqlQuery(shipping);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

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

        public string CommonPdfPath
        {
            get => commonPdfPath;
            set
            {
                try
                {
                    if (value != null)
                    {
                        commonPdfPath = value;
                    }
                    else
                    {
                        commonPdfPath = "";
                    }
                }
                catch (Exception)
                {
                    commonPdfPath = "";
                }
            }
        }

        public string PdfPath
        {
            get => pdfPath;
            set
            {
                try
                {
                    if (value != null)
                    {
                        pdfPath = value;
                    }
                    else
                    {
                        pdfPath = "";
                    }
                }
                catch (Exception)
                {
                    pdfPath = "";
                }
            }
        }

        #endregion
    }
}
