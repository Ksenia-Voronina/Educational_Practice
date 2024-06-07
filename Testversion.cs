using System;
using System.Collections.Generic;

#nullable disable

namespace testLab
{
    public partial class Testversion
    {
        public Testversion()
        {
            Questions = new HashSet<Question>();
        }

        public int Idtestversion { get; set; }
        public string Titleversion { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
