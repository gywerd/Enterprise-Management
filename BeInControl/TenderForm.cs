using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class TenderForm
    {
        #region Fields
        private int tenderFormId;
        private string form;
        private string abbreviation;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public TenderForm() { }

        /// <summary>
        /// Constructor for adding new tender forms
        /// </summary>
        /// <param name="form">string</param>
        /// <param name="abbreviation">string</param>
        public TenderForm(string form, string abbreviation)
        {
            this.form = form;
            this.abbreviation = abbreviation;
        }

        /// <summary>
        /// Constructor for adding tender forms from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="form">string</param>
        /// <param name="abbreviation">string</param>
        public TenderForm(int id, string form, string abbreviation)
        {
            this.tenderFormId = id;
            this.form = form;
            this.abbreviation = abbreviation;
        }
        #endregion

        #region Methods

        #endregion

        #region Properties
        public int TenderFormId { get => tenderFormId; }
        public string Form { get => form; set => form = value; }
        public string Abbreviation { get => abbreviation; set => abbreviation = value; }
        #endregion
    }
}
