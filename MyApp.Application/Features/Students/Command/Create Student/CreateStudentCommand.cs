using MediatR;
using MyApp.Application.Features.Students.DTOS;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Command
{
       public class CreateStudentCommand : IRequest<StudentDto>
        {

        public string FName { get; set; }

        public string LName { get; set; }

        public int Age { get; set; }



    }

}

