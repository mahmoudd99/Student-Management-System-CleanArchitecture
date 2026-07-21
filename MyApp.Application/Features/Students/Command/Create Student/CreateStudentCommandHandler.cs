using AutoMapper;
using MediatR;
using MyApp.Application.Features.Students.DTOS;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Command.Create_Student
{
    public class CreateStudentCommandHandler
        : IRequestHandler<CreateStudentCommand, StudentDto  >
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public CreateStudentCommandHandler(IStudentRepository studentRepository ,IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentDto> Handle(
            CreateStudentCommand request,
            CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);

            var created = await _studentRepository.AddStudentAsync(student);

            return _mapper.Map<StudentDto>(created);

        }
    }


}
