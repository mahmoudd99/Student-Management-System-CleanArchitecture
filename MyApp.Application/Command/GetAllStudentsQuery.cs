using MediatR;
using MediatR.Pipeline;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Command
{
    public record GetAllStudentsQuery():IRequest<IEnumerable<Student>>;
    public class GetAllStudentsQueryHandler(IStudentRepository studentRepository) : 
                    IRequestHandler<GetAllStudentsQuery, IEnumerable<Student>>
    {
        public Task<IEnumerable<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return studentRepository.GetStudentAsync();
        }
    }
}
  