using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JudRepository
{
    public class IttLetterPdfData
    {
        #region Fields
        //Fields for DataAccess
        private static string strConnection;
        private Executor executor;

        //Fields for Db Content
        private int id;
        private Project project;
        private Builder builder;
        private string answerDate = "";
        private string questionDate = "";
        private string correctionSheetDate = "";
        private string conditionDate = "";
        private string timeSpan = "";
        private string materialUrl = "";
        private string conditionUrl = "";
        private string passWord = "";

        //Lists
        private List<Enterprise> enterprises;
        private List<Description> descriptionList;
        private List<IttLetterReceiver> ittLetterReceivers;
        private List<IttLetterShipping> ittLetterShippingList;
        private List<BluePrint> bluePrints;
        private List<TimeSchedule> timeSchedules;
        private List<Miscellaneous> miscellaneusList;

        //Entities for methods
        private Project CPJ = new Project(strConnection);
        private Builder CBL = new Builder(strConnection);
        private Enterprise CEP = new Enterprise(strConnection);
        private Description CDS = new Description(strConnection);
        private IttLetterReceiver CIR = new IttLetterReceiver(strConnection);
        private IttLetterShipping CIS = new IttLetterShipping(strConnection);
        private BluePrint CBP = new BluePrint(strConnection);
        private TimeSchedule CTS = new TimeSchedule(strConnection);
        private Miscellaneous CMS = new Miscellaneous(strConnection);

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IttLetterPdfData(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = new Project(strConnection);
            this.builder = new Builder(strConnection);
            this.answerDate = "";
            this.questionDate = "";
            this.correctionSheetDate = "";
            this.timeSpan = "";
            this.materialUrl = "";
            this.conditionUrl = "";
            this.passWord = "";
            this.enterprises = new List<Enterprise>();
            this.descriptionList = new List<Description>();
            this.ittLetterReceivers = new List<IttLetterReceiver>();
            this.ittLetterShippingList = new List<IttLetterShipping>();
            this.bluePrints = new List<BluePrint>();
            this.timeSchedules = new List<TimeSchedule>();
            this.miscellaneusList = new List<Miscellaneous>();
        }

        /// <summary>
        /// Constructor to adding new Description
        /// </summary>
        /// <param name="project">int</param>
        /// <param name="builder">int</param>
        /// <param name="answerDate">string</param>
        /// <param name="questionDate">string</param>
        /// <param name="correctionSheetDate">string</param>
        /// <param name="timeSpan">string</param>
        /// <param name="materialUrl">string</param>
        /// <param name="conditionUrl">string</param>
        /// <param name="passWord">string</param>
        /// <param name="enterprises">List<Enterprise></param>
        /// <param name="descriptionList">List<Description></param>
        /// <param name="receivers">List<IttLetterReceiver></param>
        /// <param name="shippingList">List<IttLetterShipping></param>
        /// <param name="bluePrints">List<string> bluePrints</param>
        /// <param name="schedules">List<string> schedules</param>
        /// <param name="miscellaneusList">List<Miscellaneus></param>
        public IttLetterPdfData(string strCon, Project project, Builder builder, string answerDate, string questionDate, string correctionSheetDate, string timeSpan, string materialUrl, string conditionUrl, string passWord, List<Enterprise> enterprises, List<Description> descriptionList, List<IttLetterReceiver> receivers, List<IttLetterShipping> shippingList, List<BluePrint> bluePrints, List<TimeSchedule> schedules, List<Miscellaneous> miscellaneus)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = project;
            this.builder = builder;
            this.answerDate = answerDate;
            this.questionDate = questionDate;
            this.correctionSheetDate = correctionSheetDate;
            this.timeSpan = timeSpan;
            this.materialUrl = materialUrl;
            this.conditionUrl = conditionUrl;
            this.passWord = passWord;
            this.enterprises = enterprises;
            this.descriptionList = descriptionList;
            this.ittLetterReceivers = receivers;
            this.ittLetterShippingList = shippingList;
            this.bluePrints = bluePrints;
            this.timeSchedules = schedules;
            this.miscellaneusList = miscellaneus;
        }

        /// <summary>
        /// Constructor to adding Description from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="project">int</param>
        /// <param name="builder">int</param>
        /// <param name="answerDate">string</param>
        /// <param name="questionDate">string</param>
        /// <param name="correctionSheetDate">string</param>
        /// <param name="timeSpan">string</param>
        /// <param name="materialUrl">string</param>
        /// <param name="conditionUrl">string</param>
        /// <param name="passWord">string</param>
        /// <param name="enterprises">List<Enterprise></param>
        /// <param name="descriptionList">List<Description></param>
        /// <param name="receivers">List<IttLetterReceiver></param>
        /// <param name="shippingList">List<IttLetterShipping></param>
        /// <param name="bluePrints">List<string> bluePrints</param>
        /// <param name="schedules">List<string> schedules</param>
        /// <param name="miscellaneus">List<string></param>
        public IttLetterPdfData(string strCon, int id, int project, int builder, string answerDate, string questionDate, string correctionSheetDate, string timeSpan, string materialUrl, string conditionUrl, string passWord)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = CPJ.GetProject(project);
            this.builder = CBL.GetBuilder(builder);
            this.answerDate = answerDate;
            this.questionDate = questionDate;
            this.correctionSheetDate = correctionSheetDate;
            this.timeSpan = timeSpan;
            this.materialUrl = materialUrl;
            this.conditionUrl = conditionUrl;
            this.passWord = passWord;
            this.enterprises = CEP.GetEnterpriseList(project);
            this.descriptionList = CDS.GetDescriptionList(project);
            this.ittLetterReceivers = CIR.GetIttLetterReceivers(project);
            this.ittLetterShippingList = CIS.GetIttLetterShippingList(project);
            this.bluePrints = CBP.GetBluePrints(project);
            this.timeSchedules = CTS.GetTimeSchedules(project);
            this.miscellaneusList = CMS.GetMiscellaneusList(project);
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
            //List<IttLetterReceiver> tempIttLetterReceivers = new List<IttLetterReceiver>();

            //INSERT INTO [dbo].[IttLetterReceivers]([ShippingId], [Project], [CompanyId], [CompanyName], [Attention], [Street], [Place], [Zip], [Email]) VALUES(<ShippingId, int,>, < Project, int,>, < CompanyId, nvarchar(255),>, < CompanyName, nvarchar(255),>, < Attention, nvarchar(50),>, < Street, nvarchar(50),>, < Place, nvarchar(50),>, < Zip, nvarchar(50),>, < Email, nvarchar(50),>)
            string strSql = "INSERT INTO[dbo].[IttLetterReceivers]([ShippingId], [Project], [CompanyId], [CompanyName], [Attention], [Street], [Place], [Zip], [Email]) VALUES(" + receiver.Shipping.Id + ", " + receiver.Project.Id + ", '" + receiver.CompanyId + "', '" + receiver.CompanyName + "', '" + receiver.Attention + "', '" + receiver.Street + "', '" + receiver.Place + "', '" + receiver.Zip + "', '" + receiver.Email + "')";

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
        private string CreateUpdateIttLetterReceiverSqlQuery(IttLetterReceiver letter)
        {
            //UPDATE [dbo].[IttLetterReceiver] SET [CompanyId] = <CompanyId, nvarchar(10),>, [CompanyName] = <CompanyName, nvarchar(255),>, [Attention] = <Attention, nvarchar(50),>, [Street] = <Street, nvarchar(50),>, [Place] = <Place, nvarchar(50),>, [Zip] = <Zip, nvarchar(65),>, [Email] = <Email, nvarchar(255),> WHERE [Id] = <Id, int>;
            return "UPDATE[dbo].[IttLetterReceivers] SET[CompanyId] = '" + letter.CompanyId + "', [CompanyName] = '" + letter.CompanyName + "', [Attention] = '" + letter.Attention + "', [Street] = '" + letter.Street + "', [Place] = '" + letter.Place + "', [Zip] = '" + letter.Zip + "', [Email] = '" + letter.Email + "' WHERE[Id] = " + letter.Id;
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
            if (int.TryParse(id.ToString(), out int parsedId) && this.id == 0 && parsedId >= 1)
            {
                this.id = parsedId;
            }
            else
            {
                this.id = 0;
            }
        }

        /// <summary>
        /// Returns main content as string with multiple rows
        /// </summary>
        /// <returns>string</returns>
        //public string ToLongString()
        //{
        //    string result = companyName + "\n" + attention + "\n" + street;
        //    if (place != "")
        //    {
        //        result += "\n" + place;
        //    }
        //    result += "\n" + zip;
        //    if (email != "")
        //    {
        //        result += "\n" + email;
        //    }
        //    return result;
        //}

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string result = "PDF-data til projekt: " + project.Name;
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
            string strSql = CreateUpdateIttLetterReceiverSqlQuery(receiver);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public Project Project { get; set; }

        public Builder Builder { get; set; }

        public string AnswerDate
        {
            get => answerDate;
            set
            {
                try
                {
                    answerDate = value;
                }
                catch (Exception)
                {

                    answerDate = "";
                }
            }
        }

        public string QuestionDate
        {
            get => questionDate;
            set
            {
                try
                {
                    questionDate = value;
                }
                catch (Exception)
                {
                    questionDate = "";
                }
            }
        }

        public string CorrectionSheetDate
        {
            get => correctionSheetDate;
            set
            {
                try
                {
                    correctionSheetDate = value;
                }
                catch (Exception)
                {
                    correctionSheetDate = "";
                }
            }
        }

        public string TimeSpan
        {
            get => timeSpan;
            set
            {
                try
                {
                    timeSpan = value;
                }
                catch (Exception)
                {
                    timeSpan = "";
                }
            }
        }

        public string MaterialUrl
        {
            get => materialUrl;
            set
            {
                try
                {
                    materialUrl = value;
                }
                catch (Exception)
                {
                    materialUrl = "";
                }
            }
        }

        public string ConditionUrl
        {
            get => conditionUrl;
            set
            {
                try
                {
                    conditionUrl = value;
                }
                catch (Exception)
                {
                    conditionUrl = "";
                }
            }
        }

        public string PassWord
        {
            get => passWord;
            set
            {
                try
                {
                    passWord = value;
                }
                catch (Exception)
                {
                    passWord = "";
                }
            }
        }

        public string ConditionDate
        {
            get => conditionDate;
            set
            {
                try
                {
                    conditionDate = value;
                }
                catch (Exception)
                {
                    conditionDate = "";
                }
            }
        }

        public List<Miscellaneous> Miscellaneous { get; set; }

        public List<Enterprise> Enterprises { get; set; }

        public List<Description> DescriptionList { get; set; }

        public List<IttLetterReceiver> IttLetterReceivers { get; set; }

        public List<IttLetterShipping> IttLetterShippingList { get; set; }

        public List<BluePrint> BluePrints { get; set; }

        public List<TimeSchedule> TimeSchedules { get; set; }

        #endregion

    }
}
