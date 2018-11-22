using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class IttLetterBullet
    {
        #region Fields
        protected int id;
        protected int paragraph;
        protected string name;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IttLetterBullet(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            paragraph = 0;
            name = "";
        }

        /// <summary>
        /// Constructor for adding new IttLetterBullet
        /// </summary>
        /// <param name="paragraph">int</param>
        /// <param name="name">string</param>
        public IttLetterBullet(string strCon, int paragraph, string name)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.paragraph = paragraph;
            this.name = name;
        }

        /// <summary>
        /// Constructor for adding IttLetterBullet from Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="paragraph">int</param>
        /// <param name="name">string</param>
        public IttLetterBullet(string strCon, int id, int paragraph, string name)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.paragraph = paragraph;
            this.name = name;
        }

        /// <summary>
        /// Constructor for that accepts data from existing IttLetterBullet
        /// </summary>
        /// <param name="bullet">IttLetterBullet</param>
        public IttLetterBullet(string strCon, IttLetterBullet bullet)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (bullet != null)
            {
                this.id = bullet.id;
                this.paragraph = bullet.Paragraph;
                this.name = bullet.Name;
            }
            else
            {
                this.id = 0;
                this.paragraph = 0;
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
            string result = @"DELETE FROM dbo.IttLetterBulletList WHERE Id = " + id + @";";
            return result;
        }

        /// <summary>
        /// Method, that creates an Insert Into SQL-Query
        /// </summary>
        /// <param name="bullet">IttLetterBullet</param>
        /// <returns>string</returns>
        private string CreateInsertIntoSqlQuery(IttLetterBullet bullet)
        {
            //INSERT INTO table_name (column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            string dataString = GetDataStringFromIttLetterBullet(bullet);
            string result = @"INSERT INTO dbo.IttLetterBulletList(Project, Name) VALUES(";
            result += dataString + @");";
            return result;
        }

        /// <summary>
        /// Method, that creates an Update SQL-Query
        /// </summary>
        /// <param name="bullet">IttLetterBullet</param>
        /// <returns>string</returns>
        private string CreateUpdateSqlQuery(IttLetterBullet bullet)
        {
            //UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE condition;
            string result = @"UPDATE dbo.IttLetterBulletList SET Paragraph = " + bullet.Paragraph.ToString() + @", Name = '" + bullet.Name + @" WHERE Id = " + bullet.Id + @";";
            return result;
        }

        /// <summary>
        /// Method, that removes an IttLetterBullet from Db
        /// </summary>
        /// <param name="id">int</param>
        public bool DeleteFromIttLetterBulletList(int id)
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
        /// Method, converts an IttLetterBullet into a data string
        /// </summary>
        /// <param name="bullet">IttLetterBullet</param>
        /// <returns>string</returns>
        private string GetDataStringFromIttLetterBullet(IttLetterBullet bullet)
        {
            string result = bullet.Paragraph.ToString() + @", '" + bullet.Name + @"'";
            return result;
        }

        /// <summary>
        /// Method, that reads the IttLetterBullet List from Db
        /// </summary>
        /// <returns>List<IttLetterBullet></returns>
        public List<IttLetterBullet> GetIttLetterBulletList()
        {
            List<string> results = executor.ReadListFromDataBase("IttLetterBulletList");
            List<IttLetterBullet> bullets = new List<IttLetterBullet>();
            foreach (string result in results)
            {
                string[] resultArray = new string[3];
                resultArray = result.Split(';');
                IttLetterBullet bullet = new IttLetterBullet(strConnection, Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), resultArray[2]);
                bullets.Add(bullet);
            }
            return bullets;
        }

        /// <summary>
        /// Method, that adds a IttLetterBullet to Db
        /// </summary>
        /// <param name="bullet">IttLetterBullet</param>
        /// <returns>bool</returns>
        public bool InsertIntoIttLetterBulletList(IttLetterBullet bullet)
        {
            bool result;
            string strSql = CreateInsertIntoSqlQuery(bullet);
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
        /// Method, that updates an IttLetterBullet in Db
        /// </summary>
        /// <param name="bullet">IttLetterBullet</param>
        /// <returns>bool</returns>
        public bool UpdateIttLetterBulletList(IttLetterBullet bullet)
        {
            bool result;
            string strSql = CreateUpdateSqlQuery(bullet);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public int Paragraph
        {
            get => paragraph;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        paragraph = value;
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
