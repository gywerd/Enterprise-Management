using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class Category
    {
        #region Fields
        private int id;
        private string name;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Category()
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.name = "";
        }

        /// <summary>
        /// Constructor to add new Category 
        /// </summary>
        /// <param name="name">string</param>
        public Category(string name)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.name = name;
        }

        /// <summary>
        /// Constructor to add craft group from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="name">string</param>
        public Category(int id, string name)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Constructor, that accepts an existing Category
        /// </summary>
        /// <param name="category">Category</param>
        public Category(Category category)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            if (category != null)
            {
                this.id = category.Id;
                this.name = category.Name;
            }
            else
            {
                this.id = 0;
                this.name = "";
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that reads list of Categories from Db
        /// </summary>
        /// <returns>List<Category></returns>
        public List<Category> GetCategories()
        {
            List<string> results = executor.ReadListFromDataBase("Categories");
            List<Category> cats = new List<Category>();
            foreach (string result in results)
            {
                string[] resultArray = new string[2];
                resultArray = result.Split(';');
                Category cat = new Category(Convert.ToInt32(resultArray[0]), resultArray[1]);
                cats.Add(cat);
            }
            return cats;
        }

        /// <summary>
        /// Method, that returns main info as string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return name;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

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
