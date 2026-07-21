using MediatR;
using MediatR.Pipeline;
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
    public record GetAllStudentsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? Search = null,
    string? SortBy = null,
    bool Descending = false,
        int? MinAge = null,
    int? MaxAge = null
) : IRequest<PagedResult<StudentDto>>;
}
  