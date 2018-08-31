﻿using JudDataAccess;
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
        }

        public Category(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add craft group from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="name">string</param>
        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            return name;
        }

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