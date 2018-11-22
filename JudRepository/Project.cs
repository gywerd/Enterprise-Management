﻿using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class Project
    {
        #region Fields
        private int id;
        private int caseId;
        private string name;
        private int builder;
        private int status;
        private int tenderForm;
        private int enterpriseForm;
        private int executive;
        private bool enterpriseList;
        private bool copy;

        private static string strConnection;
        private Executor executor;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Project(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.name = "";
            this.builder = 0;
            this.status = 1;
            this.tenderForm = 0;
            this.enterpriseForm = 0;
            this.executive = 0;
            enterpriseList = false;
            copy = false;
        }

        /// <summary>
        /// Constructor to add new project
        /// </summary>
        /// <param name="caseId">int</param>
        /// <param name="name">string</param>
        /// <param name="builder">string</param>
        /// <param name="status">int</param>
        /// <param name="tenderForm">int</param>
        /// <param name="enterpriseForm">int</param>
        /// <param name="executive">int</param>
        /// <param name="enterPriseList">bool</param>
        /// <param name="copy">bool</param>
        public Project(string strCon, int caseId, string name, int builder = 0, int status = 0, int tenderForm = 0, int enterpriseForm = 1, int executive = 0, bool enterpriseList = false,  bool copy = false)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.caseId = caseId;
            this.name = name;
            this.builder = builder;
            this.status = status;
            this.enterpriseList = enterpriseList;
            this.tenderForm = tenderForm;
            this.enterpriseForm = enterpriseForm;
            this.executive = executive;
            this.copy = copy;
        }

        /// <summary>
        /// Constructor to add project from Db to List
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="caseId">int</param>
        /// <param name="name">string</param>
        /// <param name="builder">string</param>
        /// <param name="status">int</param>
        /// <param name="tenderForm">int</param>
        /// <param name="enterpriseForm">int</param>
        /// <param name="executive">int</param>
        /// <param name="enterPriseList">bool</param>
        /// <param name="copy">bool</param>
        public Project(string strCon, int id, int caseId, string name, int builder, int status, int tenderForm, int enterpriseForm, int executive, bool enterpriseList, bool copy = false)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = id;
            this.caseId = caseId;
            this.name = name;
            this.builder = builder;
            this.status = status;
            this.tenderForm = tenderForm;
            this.enterpriseForm = enterpriseForm;
            this.executive = executive;
            this.enterpriseList = enterpriseList;
            this.copy = copy;
        }

        /// <summary>
        /// Constructor that receives data from an existing Project
        /// </summary>
        /// <param name="project"></param>
        public Project(string strCon, Project project)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            if (project != null)
            {
                this.id = project.Id;
                this.caseId = project.CaseId;
                this.name = project.Name;
                this.builder = project.Builder;
                this.status = project.Status;
                this.tenderForm = project.TenderForm;
                this.enterpriseForm = project.EnterpriseForm;
                this.executive = project.Executive;
                this.enterpriseList = project.EnterpriseList;
                this.copy = project.Copy;
            }
            else
            {
                this.id = 0;
                this.caseId = 0;
                this.name = "";
                this.builder = 0;
                this.status = 1;
                this.tenderForm = 0;
                this.enterpriseForm = 0;
                this.executive = 0;
                this.enterpriseList = false;
                this.copy = false;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that Create a Delete From SQL-Query
        /// </summary>
        /// <param name="project">Project</param>
        /// <returns>string</returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.Projects WHERE Id = " + id + ";";
            return result;
        }

        /// <summary>
        /// Method, that Create a Insert Into SQL-Query
        /// </summary>
        /// <param name="project">Project</param>
        /// <returns>string</returns>
        private string CreateInsertIntoSqlQuery(Project project)
        {
            //INSERT INTO table_name (column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            string dataString = GetDataStringFromProject(project);
            string result = @"INSERT INTO dbo.Projects(CaseId, Name, Builder, Status, TenderForm, EnterpriseForm, Executive, EnterpriseList, Copy) VALUES(";
            result += dataString + @");";
            return result;
        }

        /// <summary>
        /// Method, that Create an Update SQL-Query
        /// </summary>
        /// <param name="project">Project</param>
        /// <returns>string</returns>
        private string CreateUpdateSqlQuery(Project project)
        {
            //UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE condition;
            string result = @"UPDATE dbo.Projects SET CaseId = " + project.CaseId + ", Name = '" + project.Name + "', Builder = " + project.Builder + ", Status = " + project.Status + ", TenderForm = " + project.TenderForm + ", EnterpriseForm = " + project.EnterpriseForm + ", Executive = " + project.Executive + ", EnterpriseList = '" + project.EnterpriseList + "', Copy = '" + project.Copy + "' WHERE Id = " + project.Id + ";";
            return result;
        }

        /// <summary>
        /// Method, that deletes a Project from the Db
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool DeleteFromProject(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        /// <summary>
        /// Method, that converts a project into a data string for SQL-Query
        /// </summary>
        /// <param name="project">Project</param>
        /// <returns></returns>
        private string GetDataStringFromProject(Project project)
        {
            string result = project.CaseId + @", '" + project.Name + @"', " + project.Builder + @", " + project.Status + @", " + project.TenderForm + @", " + project.EnterpriseForm + @", " + project.Executive + @", 'false', 'false'";
            return result;
        }

        /// <summary>
        /// Method that reads a Project List from Db
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProjects()
        {
            List<string> results = executor.ReadListFromDataBase("Projects");
            List<Project> projects = new List<Project>();
            foreach (string result in results)
            {
                string[] resultArray = new string[10];
                resultArray = result.Split(';');
                Project project = new Project(strConnection, Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), resultArray[2], Convert.ToInt32(resultArray[3]), Convert.ToInt32(resultArray[4]), Convert.ToInt32(resultArray[5]), Convert.ToInt32(resultArray[6]), Convert.ToInt32(resultArray[7]), Convert.ToBoolean(resultArray[8]), Convert.ToBoolean(resultArray[9]));
                projects.Add(project);
            }
            return projects;
        }

        /// <summary>
        /// Method, that adds a project to Db
        /// </summary>
        /// <param name="tempProject">Project</param>
        /// <returns>bool</returns>
        public bool InsertIntoProject(Project tempProject)
        {
            bool result;
            string strSql = CreateInsertIntoSqlQuery(tempProject);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        /// <summary>
        /// Methods that toggles value of Copy field
        /// </summary>
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

        /// <summary>
        /// Methods that toggles value of EnterpriseList field
        /// </summary>
        public void ToggleEnterpriseList()
        {
            enterpriseList = true;
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string result = caseId + " " + name;
            return result;
        }

        /// <summary>
        /// Method, that updates a Project in Db
        /// </summary>
        /// <param name="tempProject">Project</param>
        /// <returns>bool</returns>
        public bool UpdateProject(Project tempProject)
        {
            bool result;
            string strSql = CreateUpdateSqlQuery(tempProject);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public int CaseId
        {
            get => caseId;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        caseId = value;
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

        public int Builder
        {
            get => builder;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        builder = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

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

        public int TenderForm
        {
            get => tenderForm;
            set
            {
                try
                {
                    if (value >= 0)
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

        public int EnterpriseForm
        {
            get => enterpriseForm;
            set
            {
                try
                {
                    if (value >= 0)
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

        public int Executive
        {
            get => executive;
            set
            {
                try
                {
                    if (value >= 0)
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

        public bool EnterpriseList { get => enterpriseList; }

        public bool Copy { get => copy; }

        #endregion
    }
}
