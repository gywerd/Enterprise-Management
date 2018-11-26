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
    public class IttLetterShipping
    {
        #region Fields
        private static string strConnection;
        private Executor executor;

        int id = 0;
        Project project;
        string commonPdfPath = "";
        string pdfPath = "";

        Project CPJ = new Project(strConnection);

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IttLetterShipping(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = new Project(strConnection);
            this.commonPdfPath = @"PDF_Documents\";
            this.pdfPath = "";
        }

        /// <summary>
        /// Constructor for adding new ITT Letter Shipping
        /// </summary>
        /// <param name="commmonPdfPath">string</param>
        /// <param name="pdfPath">string</param>
        public IttLetterShipping(string strCon, Project project, string commmonPdfPath, string pdfPath)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = project;
            this.commonPdfPath = commmonPdfPath;
            this.pdfPath = pdfPath;
        }

        /// <summary>
        /// Constructor for adding ITT Letter Shipping from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="commmonPdfPath">string</param>
        /// <param name="pdfPath">string</param>
        public IttLetterShipping(string strCon, int id, int project, string commmonPdfPath, string pdfPath)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.project = CPJ.GetProject(project);
            this.commonPdfPath = commmonPdfPath;
            this.pdfPath = pdfPath;
        }

        /// <summary>
        /// Constructor for accepts an existing Itt Letter Shipping
        /// </summary>
        /// <param name="shipping">IttLetterShipping</param>
        public IttLetterShipping(string strCon, IttLetterShipping shipping)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (shipping != null)
            {
                this.id = shipping.Id;
                this.project = shipping.Project;
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
        public int CreateIttLetterShippingInDb(string macAddress, List<IttLetterShipping> ittLetterShippingList, IttLetterShipping shipping)
        {
            int result = 0;
            bool dbAnswer = false;
            List<IttLetterShipping> tempIttLetterShippingList = new List<IttLetterShipping>();
            //INSERT INTO [dbo].[IttLetterShippingList]([CommonPdfPath], [PdfPath]) VALUES(<CommonPdfPath, nvarchar(50),>, <PdfPath, nvarchar(50)>)
            string strSql = @"INSERT INTO [dbo].[IttLetterShippingList]([Project], [CommonPdfPath], [PdfPath]) VALUES(" + shipping.Project.Id + ", '" + shipping.CommonPdfPath + "', '" + shipping.PdfPath + "')";
            dbAnswer = executor.WriteToDataBase(strSql);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl. Forsendelsen blev ikke oprettet.", "Opret Forsendelse", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ittLetterShippingList.Clear();
                ittLetterShippingList = GetIttLetterShippingList();
                result = GetIttLetterShippingId(macAddress, ittLetterShippingList);
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
            //UPDATE [dbo].[IttLetterShippingList] SET [CommonPdfPath] = <CommonPdfPath, nvarchar(50),>,[PdfPath] = <PdfPath, nvarchar(50),> WHERE [Id] = <Id, int>;
            return "UPDATE [dbo].[IttLetterShippingList] SET[CommonPdfPath] = '" + shipping.CommonPdfPath + "',[PdfPath] = '" + shipping.PdfPath + "' WHERE[Id] = " + shipping.Id;
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
        /// Method, that retrieves an IttLetter Shipping
        /// </summary>
        /// <param name="shippingId"></param>
        /// <returns></returns>
        public IttLetterShipping GetIttLetterShipping(int shippingId)
        {
            List<IttLetterShipping> shippingList = new List<IttLetterShipping>();
            IttLetterShipping result = new IttLetterShipping(strConnection);

            foreach (IttLetterShipping shipping in shippingList)
            {
                if (shipping.Id == shippingId)
                {
                    result = shipping;
                }

            }

            return result;
        }

        /// <summary>
        /// Method, that retrieves a shipping id 
        /// </summary>
        /// <returns>int</returns>
        private int GetIttLetterShippingId(string macAddress, List<IttLetterShipping> list)
        {
            int result = 0;
            foreach (IttLetterShipping temp in list)
            {
                if (temp.PdfPath == macAddress)
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
                string[] resultArray = new string[4];
                resultArray = result.Split(';');
                ittLetterShipping = new IttLetterShipping(strConnection, Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), resultArray[2], resultArray[3]);
                ittLetterShippingList.Add(ittLetterShipping);
            }
            return ittLetterShippingList;
        }

        public List<IttLetterShipping> GetIttLetterShippingList(int projectId)
        {
            List<IttLetterShipping> ittLetterShippingList = new List<IttLetterShipping>();
            List<IttLetterShipping> result = new List<IttLetterShipping>();

            foreach (IttLetterShipping ittLetterShipping in ittLetterShippingList)
            {
                if (ittLetterShipping.Project.Id == projectId)
                {
                    result.Add(ittLetterShipping);
                }
            }
            return result;
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

        public Project Project { get; set; }

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
