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
        public CraftGroup() { }

        /// <summary>
        /// Constructor for access to db methods
        /// </summary>
        /// <param name="strCon"></param>
        public CraftGroup(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
            category = 0;
            designation = "";
            description = "";
            active = false;
        }

        /// <summary>
        /// Constructor to add craft group
        /// </summary>
        /// <param name="id">string</param>
        /// <param name="active">bool</param>
        /// <param name="category">int</param>
        /// <param name="designation">string</param>
        /// <param name="description">string</param>
        public CraftGroup(int id, int category, string designation, string description, bool active)
        {
            this.id = id;
            this.category = category;
            this.designation = designation;
            this.description = description;
            this.active = active;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            return designation;
        }

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
