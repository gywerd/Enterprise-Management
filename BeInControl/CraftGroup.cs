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
        private static string strConnection;
        private int id;
        private bool active;
        private int sort;
        private int field;
        private string abbreviation;
        private string designation;
        private string description;

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
        /// Constructor to add new field group
        /// </summary>
        /// <param name="active">bool</param>
        /// <param name="sort">int</param>
        /// <param name="field">int</param>
        /// <param name="abbreviation">string</param>
        /// <param name="designation">string</param>
        /// <param name="description">string</param>
        public CraftGroup(bool active, int sort, int field, string abbreviation, string designation, string description)
        {
            this.active = active;
            this.sort = sort;
            this.field = field;
            this.abbreviation = abbreviation;
            this.designation = designation;
            this.description = description;
        }
        /// <summary>
        /// Constructor to add field group from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="active">bool</param>
        /// <param name="sort">int</param>
        /// <param name="field">int</param>
        /// <param name="abbreviation">string</param>
        /// <param name="designation">string</param>
        /// <param name="description">string</param>
        public CraftGroup(int id, bool active, int sort, int field, string abbreviation, string designation, string description)
        {
            this.id = id;
            this.active = active;
            this.sort = sort;
            this.field = field;
            this.abbreviation = abbreviation;
            this.designation = designation;
            this.description = description;
        }
        #endregion

        #region Methods
        public List<CraftGroup> GetCraftGroupList()
        {
            List<string> results = executor.ReadListFromDataBase("CraftGroups");
            List<CraftGroup> craftGroups = new List<CraftGroup>();
            foreach (string result in results)
            {
                string[] resultArray = new string[7];
                resultArray = result.Split(';');
                CraftGroup craftGroup = new CraftGroup(Convert.ToInt32(resultArray[0]), Convert.ToBoolean(resultArray[1]), Convert.ToInt32(resultArray[2]), Convert.ToInt32(resultArray[3]), resultArray[4], resultArray[5], resultArray[6]);
                craftGroups.Add(craftGroup);
            }
            return craftGroups;
        }


        #endregion

        #region Properties
        public int Id { get => id;  }
        public int Sort { get => sort; set => sort = value; }
        public int Field { get => field; set => field = value; }
        public string Abbreviation { get => abbreviation; set => abbreviation = value; }
        public string Designation { get => designation; set => designation = value; }
        public string Description { get => description; set => description = value; }
        public bool Active
        {
            get => active;
            set => active = value;
        }
        #endregion
    }
}
