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
        private int id;
        private int entrepeneur;
        private int enterpriseList;
        private int request;
        private int ittLetter;
        private int offer;
        private bool reservations;
        private bool uphold;
        private bool agreementConcluded;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public SubEntrepeneur() { }

        /// <summary>
        /// Constructor to add new subentrepeneur
        /// </summary>
        /// <param name="entrepeneur">int</param>
        /// <param name="enterpriseList">int</param>
        /// <param name="request">int</param>
        /// <param name="ittLetter">int</param>
        /// <param name="offer">int</param>
        /// <param name="reservations">bool</param>
        /// <param name="uphold">bool</param>
        /// <param name="agreementConcluded">bool</param>
        public SubEntrepeneur(int entrepeneur, int enterpriseList, int request = 0, int ittLetter = 0, int offer = 0, bool reservations = false, bool uphold = false, bool agreementConcluded = false)
        {
            this.enterpriseList = enterpriseList;
            this.entrepeneur = entrepeneur;
            this.request = request;
            this.ittLetter = ittLetter;
            this.offer = offer;
            this.reservations = reservations;
            this.uphold = uphold;
            this.agreementConcluded = agreementConcluded;
        }

        /// <summary>
        /// Constructor to add subentrepeneur from Db to List
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="entrepeneur">int</param>
        /// <param name="enterpriseList">int</param>
        /// <param name="request">int</param>
        /// <param name="ittLetter">int</param>
        /// <param name="offer">int</param>
        /// <param name="reservations">bool</param>
        /// <param name="uphold">bool</param>
        /// <param name="agreementConcluded">bool</param>
        public SubEntrepeneur(int id, int entrepeneur, int enterpriseList, int request, int ittLetter, int offer, bool reservations, bool uphold, bool agreementConcluded)
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
        /// Method, that toggles value of Uphold
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
        /// Method, that toggles value of Uphold
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
        #endregion

        #region Properties
        public int SubEntrepeneurId { get => id; }
        public int EnterpriseList { get => enterpriseList; set => enterpriseList = value; }
        public int Entrepeneur { get => entrepeneur; set => entrepeneur = value; }
        public int Request { get => request; set => request = value; }
        public int IttLetter { get => ittLetter; set => ittLetter = value; }
        public int Offer { get => offer; set => offer = value; }
        public bool Reservations { get => reservations; }
        public bool Uphold { get => uphold; }
        public bool AgreementConcluded { get => agreementConcluded; set => agreementConcluded = value; }
        #endregion
    }
}
