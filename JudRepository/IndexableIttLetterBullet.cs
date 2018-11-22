using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class IndexableIttLetterBullet : IttLetterBullet
    {
        int index;

        public IndexableIttLetterBullet(string strCon, int index, IttLetterBullet bullet) : base(strCon, bullet)
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
