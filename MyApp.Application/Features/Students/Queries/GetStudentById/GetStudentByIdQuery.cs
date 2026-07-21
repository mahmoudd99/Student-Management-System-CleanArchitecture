using MediatR;
using MyApp.Application.Features.Students.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Students.Queries.GetStudentById
{
  
        public record GetStudentByIdQuery(int Id) : IRequest<StudentDto>;
   
}
