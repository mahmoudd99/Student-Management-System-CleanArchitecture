using AutoMapper;
using MediatR;
using MyApp.Application.Common.Pagination;
using MyApp.Application.Features.Students.DTOS;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsQueryHandler
  : IRequestHandler<GetAllStudentsQuery, PagedResult<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetAllStudentsQueryHandler(IStudentRepository studentRepository ,IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<StudentDto>> Handle(
    GetAllStudentsQuery request,
    CancellationToken cancellationToken)
        {
            var result = await _studentRepository.GetAllStudentsAsync(
                new PaginationParams
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    Search = request.Search,
                    SortBy = request.SortBy,
                    Descending = request.Descending,
                    MinAge = request.MinAge,
                    MaxAge = request.MaxAge
                });

            return new PagedResult<StudentDto>
            {
                Items = _mapper.Map<IEnumerable<StudentDto>>(result.Items),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }
    }
}
