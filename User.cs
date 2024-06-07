using System;
using System.Collections.Generic;

#nullable disable

namespace testLab
{
    public partial class User
    {
        public User()
        {
            Results = new HashSet<Result>();
        }

        public int Idusers { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}
