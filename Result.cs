using System;
using System.Collections.Generic;

#nullable disable

namespace testLab
{
    public partial class Result
    {
        public int Idquestions { get; set; }
        public int Idusers { get; set; }
        public string Answeruser { get; set; }
        public int Mark { get; set; }
        public string Idtestversion { get; set; }

        public virtual Question IdquestionsNavigation { get; set; }
        public virtual User IdusersNavigation { get; set; }
    }
}
