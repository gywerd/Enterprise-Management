﻿using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class SubEntrepeneur
    {
        #region Fields
        private int id;
        private int enterpriseList;
        private string entrepeneur;
        private int contact;
        private int request;
        private int ittLetter;
        private int offer;
        private bool reservations;
        private bool uphold;
        private bool agreementConcluded;
        private bool active;

        private static string strConnection;
        private Executor executor;

        public static LegalEntity CLE = new LegalEntity(strConnection);

        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public SubEntrepeneur()
        {
            this.enterpriseList = 0;
            this.entrepeneur = "";
            this.contact = 0;
            this.request = 0;
            this.ittLetter = 0;
            this.offer = 0;
            this.reservations = false;
            this.uphold = false;
            this.agreementConcluded = false;
            this.active = false;
        }

        /// <summary>
        /// Empty constructor, that activates Db-connection
        /// </summary>
        /// <param name="strCon">string</param>
        public SubEntrepeneur(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add subentrepeneur from Db to List
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="enterpriseList">int?</param>
        /// <param name="entrepeneur">string</param>
        /// <param name="contact">int?</param>
        /// <param name="request">int?</param>
        /// <param name="ittLetter">int?</param>
        /// <param name="offer">int?</param>
        /// <param name="reservations">bool</param>
        /// <param name="uphold">bool</param>
        /// <param name="agreementConcluded">bool</param>
        /// <param name="active">bool</param>
        public SubEntrepeneur(int id, int enterpriseList, string entrepeneur, int contact, int request, int ittLetter, int offer, bool reservations, bool uphold, bool agreementConcluded, bool active)
        {
            this.id = id;
            this.enterpriseList = enterpriseList;
            this.entrepeneur = entrepeneur;
            this.request = request;
            this.ittLetter = ittLetter;
            this.offer = offer;
            this.reservations = reservations;
            this.uphold = uphold;
            this.agreementConcluded = agreementConcluded;
            this.active = active;
        }

        /// <summary>
        /// Constructor for that accepts data from existing SubEntrepeneur
        /// </summary>
        /// <param name="subEntrepeneur">SubEntrepeneur</param>
        public SubEntrepeneur(SubEntrepeneur subEntrepeneur)
        {
            this.id = subEntrepeneur.Id;
            this.enterpriseList = subEntrepeneur.EnterpriseList;
            this.entrepeneur = subEntrepeneur.Entrepeneur;
            this.request = subEntrepeneur.Request;
            this.ittLetter = subEntrepeneur.IttLetter;
            this.offer = subEntrepeneur.Offer;
            this.reservations = subEntrepeneur.Reservations;
            this.uphold = subEntrepeneur.Uphold;
            this.agreementConcluded = subEntrepeneur.AgreementConcluded;
            this.active = subEntrepeneur.Active;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that creates an Delete SQL-Query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string CreateDeleteFromSqlQuery(int id)
        {
            //DELETE FROM table_name WHERE condition;
            string result = @"DELETE FROM dbo.SubEntrepeneurs WHERE Id = " + id + @";";
            return result;
        }

        /// <summary>
        /// Method, that creates an Insert Into SQL-Query
        /// </summary>
        /// <param name="subEntrepeneur">Enterprise</param>
        /// <returns>string</returns>
        private string CreateInsertIntoSqlQuery(SubEntrepeneur subEntrepeneur)
        {
            //INSERT INTO table_name (column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            string dataString = GetDataStringFromSubEntrepeneur(subEntrepeneur);
            string result = @"INSERT INTO dbo.SubEntrepeneurs(EnterpriseList, Entrepeneur, Contact, Request, IttLetter, Offer, Reservations, Uphold, AgreementConcluded, Active) VALUES(";
            result += dataString + @");";
            return result;
        }

        /// <summary>
        /// Method, that creates an Update SQL-Query
        /// </summary>
        /// <param name="subEntrepeneur">Enterprise</param>
        /// <returns>string</returns>
        private string CreateUpdateSqlQuery(SubEntrepeneur subEntrepeneur)
        {
            //UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE condition;
            string result = @"UPDATE dbo.SubEntrepeneurs SET EnterpriseList = " + subEntrepeneur.EnterpriseList.ToString() + @", Entrepeneur = '" + subEntrepeneur.Entrepeneur + @"', Contact = " + subEntrepeneur.Contact.ToString() + @", Request = " + subEntrepeneur.Request.ToString() + @", IttLetter = " + subEntrepeneur.IttLetter + @", Offer = " + subEntrepeneur.Offer.ToString() + @", Reservations = '" + subEntrepeneur.Reservations.ToString() + @"', Uphold = '" + subEntrepeneur.Uphold.ToString() + @"', AgreementConcluded = '" + subEntrepeneur.AgreementConcluded.ToString() + @"', Active = '" + subEntrepeneur.Active.ToString() + @" WHERE Id = " + subEntrepeneur.Id + @";";
            return result;
        }

        public void DeleteFromSubEntrepeneurs(int id)
        {
            bool result;
            string strSql = CreateDeleteFromSqlQuery(id);
            result = executor.WriteToDataBase(strSql);
        }

        /// <summary>
        /// Method, converts an SubEntrepeneur into a data string
        /// </summary>
        /// <param name="subEntrepeneur">SubEntrepeneur</param>
        /// <returns>string</returns>
        private string GetDataStringFromSubEntrepeneur(SubEntrepeneur subEntrepeneur)
        {
            string result = subEntrepeneur.EnterpriseList.ToString() + @", '" + subEntrepeneur.EnterpriseList.ToString() + @"', '" + subEntrepeneur.Entrepeneur + @"', " + subEntrepeneur.Contact.ToString() + @", " + subEntrepeneur.Request.ToString() + @", " + subEntrepeneur.IttLetter.ToString() + @", " + subEntrepeneur.Offer.ToString() + @", '" + subEntrepeneur.Reservations.ToString() + @", '" + subEntrepeneur.Uphold.ToString() + @", '" + subEntrepeneur.AgreementConcluded.ToString() + @", '" + subEntrepeneur.Active.ToString() + @"'";
            return result;
        }

        /// <summary>
        /// Method, that gets entrepeneur name from id
        /// </summary>
        /// <returns></returns>
        private string GetEntrepeneurName()
        {
            string result = "";
            List<LegalEntity> entrepeneurs = CLE.GetLegalEntities();
            foreach (LegalEntity entrepeneur in entrepeneurs)
            {
                if (entrepeneur.Id == this.entrepeneur)
                {
                    result = entrepeneur.Name;
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// Retrieves a list of subentrepeneurs from Db
        /// </summary>
        /// <returns></returns>
        public List<SubEntrepeneur> GetSubEntrepeneurs()
        {
            List<string> results = executor.ReadListFromDataBase("SubEntrepeneurs");
            List<SubEntrepeneur> subs = new List<SubEntrepeneur>();
            foreach (string sub in results)
            {
                string[] resultArray = new string[11];
                resultArray = sub.Split(';');
                SubEntrepeneur sub2 = new SubEntrepeneur(Convert.ToInt32(resultArray[0]), Convert.ToInt32(resultArray[1]), resultArray[2], Convert.ToInt32(resultArray[3]), Convert.ToInt32(resultArray[4]), Convert.ToInt32(resultArray[5]), Convert.ToInt32(resultArray[6]), Convert.ToBoolean(resultArray[7]), Convert.ToBoolean(resultArray[8]), Convert.ToBoolean(resultArray[9]), Convert.ToBoolean(resultArray[10]));
                subs.Add(sub2);
            }
            return subs;
        }

        /// <summary>
        /// Method, that adds an Enterprise to Db
        /// </summary>
        /// <param name="tempEnterprise">Enterprise</param>
        /// <returns>bool</returns>
        public bool InsertIntoSubEntrepeneurs(SubEntrepeneur tempSubEntrepeneur)
        {
            bool result;
            string strSql = CreateInsertIntoSqlQuery(tempSubEntrepeneur);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        /// <summary>
        /// <summary>
        /// Method that togle value of reservations
        /// </summary>
        public void ToggleReservations()
        {
            if (reservations)
            {
                reservations = false;
            }
            else
            {
                reservations = true;
            }
        }

        /// <summary>
        /// Method, that toggles value of uphold
        /// </summary>
        public void ToggleUphold()
        {
            if (uphold)
            {
                uphold = false;
            }
            else
            {
                uphold = true;
            }
        }

        /// <summary>
        /// Method, that toggles value of agreementConcluded
        /// </summary>
        public void ToggleAgreementConcluded()
        {
            if (agreementConcluded)
            {
                agreementConcluded = false;
            }
            else
            {
                agreementConcluded = true;
            }
        }

        /// <summary>
        /// Method, that toggles value of active
        /// </summary>
        public void ToggleActive()
        {
            if (active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
                string result = GetEntrepeneurName();
                return result;
        }

        /// <summary>
        /// Method, that updates an Enterprise in Db
        /// </summary>
        /// <param name="tempSubEntrepeneur"></param>
        /// <returns>bool</returns>
        public bool UpdateEnterpriseList(SubEntrepeneur tempSubEntrepeneur)
        {
            bool result;
            string strSql = CreateUpdateSqlQuery(tempSubEntrepeneur);
            result = executor.WriteToDataBase(strSql);
            return result;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

        public int EnterpriseList
        {
            get => enterpriseList;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        enterpriseList = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string Entrepeneur
        {
            get => entrepeneur;
            set
            {
                try
                {
                    if (value != null)
                    {
                        entrepeneur = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int Contact
        {
            get => contact;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        contact = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int Request
        {
            get => request;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        request = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int IttLetter
        {
            get => ittLetter;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        ittLetter = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int Offer
        {
            get => offer;
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        offer = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Reservations { get => reservations; }

        public bool Uphold { get => uphold; }

        public bool AgreementConcluded { get => agreementConcluded; }

        public bool Active { get => active; }
        #endregion
    }
}
