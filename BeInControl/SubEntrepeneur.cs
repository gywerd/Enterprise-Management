using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class SubEntrepeneur
    {
        #region Fields
        private int subEntrepeneurId;
        private int? enterpriseList;
        private string entrepeneur;
        private int? contact;
        private int? request;
        private int? ittLetter;
        private int? offer;
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
            this.enterpriseList = null;
            this.entrepeneur = "";
            this.contact = null;
            this.request = null;
            this.ittLetter = null;
            this.offer = null;
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
        public SubEntrepeneur(int id, int? enterpriseList, string entrepeneur, int? contact, int? request, int? ittLetter, int? offer, bool reservations, bool uphold, bool agreementConcluded, bool active)
        {
            this.subEntrepeneurId = id;
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
        #endregion

        #region Methods
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
        /// Retrieves a list of subentrepeneurs from Db
        /// </summary>
        /// <returns></returns>
        public List<SubEntrepeneur> GetSubEntrepeneurList()
        {
            List<string> results = executor.ReadListFromDataBase("Requests");
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
        /// Method, that gets entrepeneur name from id
        /// </summary>
        /// <returns></returns>
        private string GetEntrepeneurName()
        {
            string result = "";
            List<LegalEntity> entrepeneurs = CLE.GetLegalEntities();
            foreach (LegalEntity entrepeneur in entrepeneurs)
            {
                if (entrepeneur.EntityId == this.entrepeneur)
                {
                    result = entrepeneur.CompanyName;
                    return result;
                }
            }
            return result;
        }

        #endregion

        #region Properties
        public int SubEntrepeneurId { get => subEntrepeneurId; }

        public int? EnterpriseList
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

        public int? Contact
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

        public int? Request
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

        public int? IttLetter
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

        public int? Offer
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
