using AutoMapper;
using FluentAssertions;
using Moq;
using MyApp.Application.Command;
using MyApp.Application.Command.Create_Student;
using MyApp.Application.Features.Students.DTOS;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tests.Application.Students
{

    public class CreateStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<IMapper> _mapper;

        public CreateStudentCommandHandlerTests()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_Should_Return_StudentDto()
        {
            // Arrange

            var command = new CreateStudentCommand
            {
                FName = "Mahmoud",
                LName = "Amer",
                Age = 25
            };

            var student = new Student
            {
                Id = 1,
                FName = "Mahmoud",
                LName = "Amer",
              
            };

            var studentDto = new StudentDto
            {
                Id = 1,
                FName = "Mahmoud",
                LName = "Amer",
                Age = 25
            };

            _mapper
                .Setup(x => x.Map<Student>(command))
                .Returns(student);

            _studentRepository
                .Setup(x => x.AddStudentAsync(student))
                .ReturnsAsync(student);

            _mapper
                .Setup(x => x.Map<StudentDto>(student))
                .Returns(studentDto);

            var handler =
                new CreateStudentCommandHandler(
                    _studentRepository.Object,
                    _mapper.Object);

            // Act

            var result =
                await handler.Handle(command, CancellationToken.None);

            // Assert

            result.Should().NotBeNull();

            result.FName.Should().Be("Mahmoud");

            result.LName.Should().Be("Amer");

            result.Age.Should().Be(25);

            _studentRepository.Verify(
                x => x.AddStudentAsync(student),
                Times.Once);
        }



    }
}
