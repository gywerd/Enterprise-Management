using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Project
    {
        #region Fields
        private int projectId;
        private string name;
        private int builder;
        private bool enterpriseList;
        private int status;
        private string tenderForm;
        private string enterpriseForm;
        private string executive;
        private bool copy;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Project() { }

        public Project(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add new project
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="status">int</param>
        /// <param name="enterpriseForm">string</param>
        /// <param name="executive">int</param>
        /// <param name="builder">int</param>
        /// <param name="enterPriseList">int</param>
        /// <param name="tenderForm">string</param>
        /// <param name="copy">bool</param>
        public Project(string name, int status, string enterpriseForm, string executive, int builder = 0, bool enterPriseList = false, string tenderForm = "", bool copy = false)
        {
            this.name = name;
            this.builder = builder;
            this.status = status;
            this.enterpriseList = enterPriseList;
            this.tenderForm = tenderForm;
            this.enterpriseForm = enterpriseForm;
            this.executive = executive;
            this.copy = copy;
        }

        /// <summary>
        /// Constructor to add project from Db to List
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="name">string</param>
        /// <param name="enterPriseList">int</param>
        /// <param name="status">int</param>
        /// <param name="enterpriseForm">string</param>
        /// <param name="executive">int</param>
        /// <param name="tenderForm">int</param>
        /// <param name="copy">bool</param>
        public Project(int id, string name, int builder, bool enterPriseList, int status, string tenderForm, string enterpriseForm, string executive, bool copy = false)
        {
            this.projectId = id;
            this.name = name;
            this.builder = builder;
            this.enterpriseList = enterPriseList;
            this.status = status;
            this.tenderForm = tenderForm;
            this.enterpriseForm = enterpriseForm;
            this.executive = executive;
            this.copy = copy;
        }
        #endregion

        #region Methods
        public void ToggleCopy()
        {
            if (copy)
            {
                copy = false;
            }
            else
            {
                copy = true;
            }
        }

        public void AddEnterpriseList()
        {
            enterpriseList = true;
        }

        public List<Project> GetProjectList()
        {
            List<string> results = executor.ReadListFromDataBase("Projects");
            List<Project> projects = new List<Project>();
            foreach (string result in results)
            {
                string[] resultArray = new string[9];
                resultArray = result.Split(';');
                Project project = new Project(Convert.ToInt32(resultArray[0]), resultArray[1], Convert.ToInt32(resultArray[2]), Convert.ToBoolean(resultArray[3]), Convert.ToInt32(resultArray[4]), resultArray[5], resultArray[6], resultArray[7], Convert.ToBoolean(resultArray[8]));
                projects.Add(project);
            }
            return projects;
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = Convert.ToString(projectId) + " " + name;
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => projectId; }

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

        public bool EnterpriseList { get => enterpriseList; }

        public int Status
        {
            get => status;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        status = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string TenderForm
        {
            get => tenderForm;
            set
            {
                try
                {
                    if (value != null)
                    {
                        tenderForm = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string EnterpriseForm
        {
            get => enterpriseForm;
            set
            {
                try
                {
                    if (value != null)
                    {
                        enterpriseForm = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string Executive
        {
            get => executive;
            set
            {
                try
                {
                    if (value != null)
                    {
                        executive = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public bool Copy { get => copy; }
        #endregion
    }
}
