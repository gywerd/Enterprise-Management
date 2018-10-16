using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class IttLetterPdfData
    {
        #region Fields
        private List<Enterprise> enterprises;
        private List<string> completeSetDescriptions;
        private List<string> projectDocuments;
        private List<string> bluePrints;
        private List<string> timeSchedules;
        private List<string> miscellaneus;
        private string answerDate = "";
        private string questionDate = "";
        private string correctionSheetDate = "";
        private string conditionDate = "";
        private string builder = "";
        private string timeSpan = "";
        private string materialUrl = "";
        private string conditionUrl = "";
        private string passWord = "";

        #endregion

        #region Constructors
        public IttLetterPdfData() { }

        public IttLetterPdfData(List<Enterprise> enterprises, List<string> description, List<string> documents, List<string> bluePrints, List<string> schedules, List<string> miscellaneus, string answerDate, string questionDate, string correctionSheetDate, string builder, string timeSpan, string materialUrl, string conditionUrl, string passWord)
        {
            this.enterprises = enterprises;
            this.completeSetDescriptions = description;
            this.projectDocuments = documents;
            this.bluePrints = bluePrints;
            this.timeSchedules = schedules;
            this.miscellaneus = miscellaneus;
            this.answerDate = answerDate;
            this.questionDate = questionDate;
            this.correctionSheetDate = correctionSheetDate;
            this.builder = builder;
            this.timeSpan = timeSpan;
            this.materialUrl = materialUrl;
            this.conditionUrl = conditionUrl;
            this.passWord = passWord;
        }

        #endregion

        #region Methods

        #endregion

        #region Properties
        public List<Enterprise> Enterprises { get => enterprises; }

        public List<string> CompleteSetDescriptions { get => completeSetDescriptions; }

        public List<string> ProjectDocuments { get => projectDocuments; }

        public List<string> BluePrints { get => bluePrints; }

        public List<string> TimeSchedules { get => timeSchedules; }

        public string AnswerDate { get => answerDate; }

        public string QuestionDate { get => questionDate; }

        public string CorrectionSheetDate { get => correctionSheetDate; }

        public string Builder { get => builder; }

        public string TimeSpan { get => timeSpan; }

        public string MaterialUrl { get => materialUrl; }

        public string ConditionUrl { get => conditionUrl; }

        public string PassWord { get => passWord; }

        public string ConditionDate { get => conditionDate; }

        public List<string> Miscellaneus { get => miscellaneus; }

        #endregion

    }
}
