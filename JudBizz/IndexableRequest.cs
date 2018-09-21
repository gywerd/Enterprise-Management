using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class IndexableRequest : Request
    {
        int index;

        public IndexableRequest(int index, Request request) : base(request)
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
