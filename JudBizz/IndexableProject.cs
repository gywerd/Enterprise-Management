using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudBizz
{
    public class IndexableProject : Project
    {
        int index;

        public IndexableProject(int index, Project project) : base(project)
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
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        index = value;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
