using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Command
{

    public  record AddStudentCommand(Student Student):IRequest<Student>;
    public class AddStudentCommandHandler(IStudentRepository studentRepository) : IRequestHandler<AddStudentCommand, Student>
    {

        public async Task<Student> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            return await studentRepository.AddStudentAsync(request.Student);
        }
    }
}

