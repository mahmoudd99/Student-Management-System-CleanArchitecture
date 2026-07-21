using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Students.DTOS
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string FName { get; set; } = string.Empty;

        public string LName { get; set; } = string.Empty;

        public int Age { get; set; }
    }
}
