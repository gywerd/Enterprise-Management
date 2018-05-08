using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class ContactInformation
    {
        #region Fields
        private int contactInformationId;
        private string phone;
        private string mobile;
        private string fax;
        private string email;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public ContactInformation() { }

        /// <summary>
        /// Constructor for adding new record
        /// </summary>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        /// <param name="phone">string</param>
        /// <param name="fax">string</param>
        public ContactInformation(string mobile, string email, string phone ="", string fax = "")
        {
            this.phone = phone;
            this.mobile = mobile;
            this.fax = fax;
            this.email = email;
        }

        /// <summary>
        /// Constructor for adding Contact Information from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        /// <param name="phone">string</param>
        /// <param name="fax">string</param>
        public ContactInformation(int id, string mobile, string email, string phone = "", string fax = "")
        {
            this.contactInformationId = id;
            this.phone = phone;
            this.mobile = mobile;
            this.fax = fax;
            this.email = email;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string tempcontact;
            tempcontact = "Phone: " + phone + "\nMobile: " + mobile + "\nFax: " + fax + "\nEmail: " + email;
            return tempcontact;
        }
        #endregion

        #region Properties
        public int ContactInformationId { get => contactInformationId; }
        public string Phone { get => phone; set => phone = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Fax { get => fax; set => fax = value; }
        public string Email { get => email; set => email = value; }
        #endregion
    }
}
