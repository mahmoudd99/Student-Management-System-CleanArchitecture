using MediatR;
using MyApp.Application.Command;
using MyApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Students.Command.Delete_Student
{
    public class DeleteStudentCommandHandler
          : IRequestHandler<DeleteStudentCommand, Unit>
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Unit> Handle(
            DeleteStudentCommand request,
            CancellationToken cancellationToken)
        {
            await _studentRepository.DeleteStudentAsync(request.Id);

            return Unit.Value;
        }
    }
}
   
    

