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

namespace MyApp.Application.Command.Update_Student
{
    public class UpdateStudentCommandHandler
        : IRequestHandler<UpdateStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public UpdateStudentCommandHandler(
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentDto> Handle(
     UpdateStudentCommand request,
     CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.Id);

            if (student == null)
                throw new Exception("Student not found");

            student.FName = request.FName;
            student.LName = request.LName;
           

            var updatedStudent = await _studentRepository.UpdateStudentAsync(student);

            return _mapper.Map<StudentDto>(updatedStudent);
        }



    }


}


