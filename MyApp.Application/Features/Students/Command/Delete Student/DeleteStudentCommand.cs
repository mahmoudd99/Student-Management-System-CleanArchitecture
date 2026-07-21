using MediatR;
using MyApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Command
{
    public record DeleteStudentCommand(int Id) : IRequest<Unit>;

}
