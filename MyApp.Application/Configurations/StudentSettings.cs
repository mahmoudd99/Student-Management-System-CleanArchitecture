using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Configurations
{
    public class StudentSettings
    {
        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public string SchoolName { get; set; } = string.Empty;
    }
}
