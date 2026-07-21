using MyApp.Application.Common.Pagination;
using MyApp.Core.Entities;

namespace MyApp.Application.Interfaces
{
    public interface IStudentRepository
    {

        Task<PagedResult<Student>> GetAllStudentsAsync(PaginationParams paginationParams);

        Task<Student?> GetStudentByIdAsync(int id);

        Task<Student> AddStudentAsync(Student student);

        Task<Student?> UpdateStudentAsync(Student student);

        Task<string> DeleteStudentAsync(int id);
    }
}