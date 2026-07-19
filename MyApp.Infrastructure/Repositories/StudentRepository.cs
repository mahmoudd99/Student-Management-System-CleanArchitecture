using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class StudentRepository(ApplicationDbContext dbContext): IStudentRepository
    {
        public async Task<IEnumerable<Student>> GetStudentAsync()
        {
            return await dbContext.students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await dbContext.students.FirstOrDefaultAsync(X => X.Id == id);

        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            dbContext.students.Add(student);
            await dbContext.SaveChangesAsync();
            return student;

        }

        public async Task<Student> UpdateStudentAsync(int id, Student student)
        {
            var Data = await dbContext.students.FirstOrDefaultAsync(x => x.Id == id);

            if (Data != null)
            {
                Data.FName = student.FName;
                Data.LName = student.LName;
                Data.Email = student.Email;
                dbContext.students.Update(student);
            }

            return student;

        }



        public async Task<string> DeleteStudentAsync(int id)
        {
            var data = await dbContext.students.FirstOrDefaultAsync(x=>x.Id == id);
            dbContext.students.Remove(data);
            return "student is delleted succeffuly!";
        }
    }

}
       
