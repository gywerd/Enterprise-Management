using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class IndexableLegalEntity : LegalEntity
    {
        int index;

        public IndexableLegalEntity(string strCon, int index, LegalEntity legalEntity) : base(strCon, legalEntity)
        {
            this.index = index;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public int Index
        {
            get => index;
        }
    }
}
