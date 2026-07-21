using AutoMapper;
using MediatR;
using MyApp.Application.Features.Students.DTOS;
using MyApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQueryHandler
        : IRequestHandler<GetStudentByIdQuery, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentDto> Handle(
            GetStudentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var student =
                await _studentRepository.GetStudentByIdAsync(request.Id);

            if (student == null)
                throw new Exception("Student not found");

            return _mapper.Map<StudentDto>(student);
        }
    }



}
