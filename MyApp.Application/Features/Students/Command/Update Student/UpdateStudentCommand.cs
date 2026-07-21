using MediatR;
using MyApp.Application.Features.Students.DTOS;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Command
{  
    
    public record UpdateStudentCommand(
        int Id,
        string FName,
        string LName,
        int Age
    ) : IRequest<StudentDto>;
        
     
    
    
}

