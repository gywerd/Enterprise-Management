using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class IttLetter
    {
        #region Fields
        private int ittLetterId;
        private bool letter;
        private DateTime letterSent;
        private DateTime answerReceivedDate;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public IttLetter() { }

        /// <summary>
        /// Constructor for adding new ITT letter
        /// </summary>
        /// <param name="letter">bool</param>
        /// <param name="letterSent">DateTime</param>
        public IttLetter(bool letter, DateTime letterSent)
        {
            this.letter = letter;
            this.letterSent = letterSent;
        }

        
        public IttLetter(int id, bool letter, DateTime letterSent, DateTime answerReceivedDate)
        {
            this.ittLetterId = id;
            this.letter = letter;
            this.letterSent = letterSent;
            if (answerReceivedDate != null)
            {
                this.answerReceivedDate = answerReceivedDate;
            }
        }
        #endregion

        #region Methods

        #endregion

        #region Properties
        public int IttLetterId { get => ittLetterId; }
        public bool Letter { get => letter; }
        public DateTime LetterSent { get => letterSent; set => letterSent = value; }
        public DateTime AnswerReceivedDate { get => answerReceivedDate; set => answerReceivedDate = value; }
        #endregion
    }
}
