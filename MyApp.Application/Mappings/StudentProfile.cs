using AutoMapper;
using MyApp.Application.Command;
using MyApp.Application.Features.Students.DTOS;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mappings
{
    public class StudentProfile :Profile
    {

        public StudentProfile()
        {
            CreateMap<Student, StudentDto>();

            CreateMap<CreateStudentCommand, Student>();
            CreateMap<UpdateStudentCommand, Student>();

        }

    }
}
