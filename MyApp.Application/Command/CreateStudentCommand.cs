using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyApp.Application.Command.CreateStudentCommandHandler;

namespace MyApp.Application.Command
{


    public class CreateStudentCommandHandler
        : IRequestHandler<CreateStudentCommand, Student>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Student> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                FName = request.Name,
                LName=request.Name
            };

            return await _studentRepository.AddStudentAsync(student);
        }




        public class CreateStudentCommand : IRequest<Student>
        {

            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
        }
    }
}
