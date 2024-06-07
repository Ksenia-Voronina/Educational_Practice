using System;
using System.Collections.Generic;

#nullable disable

namespace testLab
{
    public partial class Question
    {
        public Question()
        {
            Results = new HashSet<Result>();
        }

        public int Idquestions { get; set; }
        public string Textquestions { get; set; }
        public string TrueAnswer { get; set; }
        public int Idtestversion { get; set; }

        public virtual Testversion IdtestversionNavigation { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
