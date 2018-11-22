using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class IttLetterParagraph
    {
        #region Fields
        protected int id;
        protected int project;
        protected string name;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IttLetterParagraph(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            project = 0;
            name = "";
        }

        /// <summary>
        /// Constructor for adding new IttLetterParagraph
        /// </summary>
        /// <param name="project">int</param>
        /// <param name="name">string</param>
        public IttLetterParagraph(string strCon, int project, string name)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.project = project;
            this.name = name;
        }

        /// <summary>
        /// Constructor for adding IttLetterParagraph from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="project">int</param>
        /// <param name="name">string</param>
        public IttLetterParagraph(string strCon, int id, int project, string name)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.project = project;
            this.name = name;
        }

        /// <summary>
        /// Constructor for that accepts data from existing IttLetterParagraph
        /// </summary>
        /// <param name="paragraph">IttLetterParagraph</param>
        public IttLetterParagraph(string strCon, IttLetterParagraph paragraph)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (paragraph != null)
            {
                this.id = paragraph.id;
                this.project = paragraph.Project;
                this.name = paragraph.Name;
            }
            else
            {
                this.id = 0;
                this.project = 0;
                this.name = "";
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates a Delete SQL-Query
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.IttLetterParagraphList WHERE Id = " + id + @";";
            return result;
        }

        /// <summary>
        /// Method, that creates an Insert Into SQL-Query
        /// </summary>
        /// <param name="paragraph">IttLetterParagraph</param>
        /// <returns>string</returns>
        private string CreateInsertIntoSqlQuery(IttLetterParagraph paragraph)
        {
            //INSERT INTO table_name (column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            string dataString = GetDataStringFromParagraph(paragraph);
            string result = @"INSERT INTO dbo.IttLetterParagraphList(Project, Name) VALUES(";
            result += dataString + @");";
            return result;
        }

        /// <summary>
        /// Method, that creates an Update SQL-Query
        /// </summary>
        /// <param name="paragraph">IttLetterParagraph</param>
        /// <returns>string</returns>
        private string CreateUpdateSqlQuery(IttLetterParagraph paragraph)
        {
            //UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE condition;
            string result = @"UPDATE dbo.IttLetterParagraphList SET Project = " + paragraph.Project.ToString() + @", Name = '" + paragraph.Name + @" WHERE Id = " + paragraph.Id + @";";
            return result;
        }

        /// <summary>
        /// Method, that removes an IttLetterParagraph from Db
        /// </summary>
        /// <param name="id">int</param>
        public bool DeleteFromIttLetterParagraphList(int id)
        {
            try
            {
                bool result = false;
                string strSql = CreateDeleteFromSqlQuery(id);
                result = executor.WriteToDataBase(strSql);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Method, converts an IttLetterParagraph into a data string
        /// </summary>
        /// <param name="paragraph">IttLetterParagraph</param>
        /// <returns>string</returns>
        private string GetDataStringFromParagraph(IttLetterParagraph paragraph)
        {
            string result = paragraph.Project.ToString() + @", '" + paragraph.Name + @"'";
            return result;
        }

        /// <summary>
        /// Method, that reads the IttLetterParagraph List from Db
        /// </summary>
        /// <returns>List<IttLetterParagraph></returns>
        public List<IttLetterParagraph> GetIttLetterParagraphList()
        {
            List<string> results = executor.ReadListFromDataBase("IttLetterParagraphList");
            List<IttLetterParagraph> paragraphs = new List<IttLetterParagraph>();
            foreach (string result in results)
            {
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                IttLetterParagraph paragraph = new IttLetterParagraph(strConnection, Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), resultArray[2]);
                paragraphs.Add(paragraph);
            }
            return paragraphs;
        }

        /// <summary>
        /// Method, that adds an IttLetterParagraph to Db
        /// </summary>
        /// <param name="paragraph">IttLetterParagraph</param>
        /// <returns>bool</returns>
        public bool InsertIntoIttLetterParagraphList(IttLetterParagraph paragraph)
        {
            bool result;
            string strSql = CreateInsertIntoSqlQuery(paragraph);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        /// <summary>
        /// Method, that returns main info as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Method, that updates an IttLetterParagraph in Db
        /// </summary>
        /// <param name="paragraph">IttLetterParagraph</param>
        /// <returns>bool</returns>
        public bool UpdateIttLetterParagraphList(IttLetterParagraph paragraph)
        {
            bool result;
            string strSql = CreateUpdateSqlQuery(paragraph);
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
