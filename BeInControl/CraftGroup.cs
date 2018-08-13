using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BicBizz
{
    public class CraftGroup
    {
        #region Fields
        private string abbreviation;
        private bool active;
        private int category;
        private string description;
        private string designation;

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
        }

        /// <summary>
        /// Constructor to add craft group
        /// </summary>
        /// <param name="abbreviation">string</param>
        /// <param name="active">bool</param>
        /// <param name="category">int</param>
        /// <param name="designation">string</param>
        /// <param name="description">string</param>
        public CraftGroup(string abbreviation, bool active, int category, string description, string designation)
        {
            this.active = active;
            this.category = category;
            this.abbreviation = abbreviation;
            this.description = description;
            this.designation = designation;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            return designation;
        }

        public List<CraftGroup> GetCraftGroupList()
        {
            List<string> results = executor.ReadListFromDataBase("CraftGroups");
            List<CraftGroup> craftGroups = new List<CraftGroup>();
            foreach (string result in results)
            {
                string[] resultArray = new string[5];
                resultArray = result.Split(';');
                CraftGroup craftGroup = new CraftGroup(resultArray[0], Convert.ToBoolean(resultArray[1]), Convert.ToInt32(resultArray[2]), resultArray[3], resultArray[4]);
                craftGroups.Add(craftGroup);
            }
            return craftGroups;
        }

        #endregion

        #region Properties
        public string Abbreviation { get => abbreviation; }

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
        #endregion
    }
}
