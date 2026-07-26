using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Command;
using MyApp.Application.Features.Students.Queries.GetAllStudents;
using MyApp.Application.Features.Students.Queries.GetStudentById;
using MyApp.Core.Entities;


namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMediator mediat) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllStudents([FromQuery] GetAllStudentsQuery query)
        {
            var result = await mediat.Send(query);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await mediat.Send(new GetStudentByIdQuery(id));

            return Ok(result);
        }

        [HttpPut("UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent( int id,[FromBody] UpdateStudentCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var result = await mediat.Send(command);

            return Ok(result);
        }

        [HttpPost("AddStudent")]
        public async Task<ActionResult> AddStudent([FromBody] CreateStudentCommand command)
        {

            var result= await mediat.Send(command);
            return Ok(result);

        }
        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent( [FromBody]int id)
        {
            var result = await mediat.Send(new DeleteStudentCommand(id));
            return Ok(result);
        }





    }
}
