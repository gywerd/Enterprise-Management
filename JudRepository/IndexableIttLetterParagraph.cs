using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class IndexableIttLetterParagraph : IttLetterParagraph
    {
        int index;

        public IndexableIttLetterParagraph(string strCon, int index, IttLetterParagraph paragraph) : base(strCon, paragraph)
        {
            this.index = index;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public int Index { get => index; }
    }
}
