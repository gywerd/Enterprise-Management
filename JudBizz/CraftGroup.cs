using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JudBizz
{
    public class CraftGroup
    {
        #region Fields
        private int id;
        private int category;
        private string designation;
        private string description;
        private bool active;

        private static string strConnection;
        private Executor executor;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CraftGroup()
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.category = 0;
            this.designation = "";
            this.description = "";
            this.active = false;
        }

        /// <summary>
        /// Constructor to add craft new group
        /// </summary>
        /// <param name="active">bool</param>
        /// <param name="category">int</param>
        /// <param name="designation">string</param>
        /// <param name="description">string</param>
        public CraftGroup(int category, string designation, string description, bool active)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = 0;
            this.category = category;
            this.designation = designation;
            this.description = description;
            this.active = active;
        }

        /// <summary>
        /// Constructor to add craft group from Db
        /// </summary>
        /// <param name="id">string</param>
        /// <param name="active">bool</param>
        /// <param name="category">int</param>
        /// <param name="designation">string</param>
        /// <param name="description">string</param>
        public CraftGroup(int id, int category, string designation, string description, bool active)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            this.id = id;
            this.category = category;
            this.designation = designation;
            this.description = description;
            this.active = active;
        }

        /// <summary>
        /// Constructor to add craft group
        /// </summary>
        /// <param name="craftGroup">CraftGroup</param>
        public CraftGroup(CraftGroup craftGroup)
        {
            strConnection = Bizz.StrConnection;
            executor = new Executor(strConnection);

            if (craftGroup != null)
            {
                this.id = craftGroup.Id;
                this.category = craftGroup.Category;
                this.designation = craftGroup.Designation;
                this.description = craftGroup.Description;
                this.active = craftGroup.Active;
            }
            else
            {
                this.id = craftGroup.Id;
                this.category = 0;
                this.designation = "";
                this.description = "";
                this.active = false;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that reads a List of CraftGroups from Db
        /// </summary>
        /// <returns>List<CraftGroup></returns>
        public List<CraftGroup> GetCraftGroups()
        {
            List<string> results = executor.ReadListFromDataBase("CraftGroups");
            List<CraftGroup> craftGroups = new List<CraftGroup>();
            foreach (string result in results)
            {
                string[] resultArray = new string[5];
                resultArray = result.Split(';');
                CraftGroup craftGroup = new CraftGroup(Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), resultArray[2], resultArray[3], Convert.ToBoolean(resultArray[4]));
                craftGroups.Add(craftGroup);
            }
            return craftGroups;
        }

        /// <summary>
        /// Method, that returns main info as string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return designation;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public int Category
        {
            get => category;
            set
            {
                try
                {
                    if (value <= 0)
                    {
                        category = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string Designation
        {
            get => designation;
            set
            {
                try
                {
                    if (value != null)
                    {
                        designation = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

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

        public bool Active
        {
            get => active;
            set
            {
                try
                {
                    if (value.Equals(true) || value.Equals(false))
                    {
                        active = value;
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
