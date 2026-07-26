using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using MyApp.Infrastructure.Data;
using MyApp.Application.Common.Pagination;
using Microsoft.EntityFrameworkCore;
public class StudentRepository(ApplicationDbContext dbContext)
    : IStudentRepository
{
    public async Task<PagedResult<Student>> GetAllStudentsAsync(PaginationParams paginationParams)
    {
        var query = dbContext.students.AsQueryable();
        //search
        if (!string.IsNullOrWhiteSpace(paginationParams.Search))
        {
            query = query.Where(s =>
                s.FName.Contains(paginationParams.Search) ||
                s.LName.Contains(paginationParams.Search));
        }
        // Filter


        if (paginationParams.MinAge.HasValue)
        {
            query = query.Where(s => s.Id >= paginationParams.MinAge.Value);
        }

        if (paginationParams.MaxAge.HasValue)
        {
            query = query.Where(s => s.Id <= paginationParams.MaxAge.Value);
        }

        //sorting
        var totalCount = await query.CountAsync();
        if (!string.IsNullOrWhiteSpace(paginationParams.SortBy))
        {
            query = paginationParams.SortBy.ToLower() switch
            {
                "fname" => paginationParams.Descending
                    ? query.OrderByDescending(s => s.FName)
                    : query.OrderBy(s => s.FName),

                "lname" => paginationParams.Descending
                    ? query.OrderByDescending(s => s.LName)
                    : query.OrderBy(s => s.LName),

                "id" => paginationParams.Descending
                    ? query.OrderByDescending(s => s.Id)
                    : query.OrderBy(s => s.Id),

                _ => query.OrderBy(s => s.Id)
            };
        }
       



        //pagination
        var students = await query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedResult<Student>
        {
            Items = students,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize,
            TotalCount = totalCount
        };
    }
    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await dbContext.students.FindAsync(id);
    }

    public async Task<Student> AddStudentAsync(Student student)
    {
        dbContext.students.Add(student);

        await dbContext.SaveChangesAsync();

        return student;
    }

    public async Task<Student> UpdateStudentAsync(Student student)
    {
        dbContext.students.Update(student);

        await dbContext.SaveChangesAsync();

        return student;
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await dbContext.students.FindAsync(id);

        if (student == null)
            throw new Exception("Student not found");

        dbContext.students.Remove(student);

        await dbContext.SaveChangesAsync();
    }

    Task<string> IStudentRepository.DeleteStudentAsync(int id)
    {
        throw new NotImplementedException();
    }
}