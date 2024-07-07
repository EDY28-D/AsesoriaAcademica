using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
//using Google.Apis.Calendar.v3.Data;
//using CleanArchitecture.Api.Services;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class StudentController : ApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(
            INotificationHandler<DomainNotification> notifications,
            IStudentService studentService) : base(notifications)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all Students")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<StudentViewModel>>))]
        public async Task<IActionResult> GetAllStudentsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<StudentViewModelSortProvider, StudentViewModel, Student>]
            SortQuery? sortQuery = null)
        {
            var students = await _studentService.GetAllStudentsAsync(query, includeDeleted, searchTerm, sortQuery);
            return Response(students);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a Student by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<StudentViewModel>))]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            return Response(student);
        }

        [HttpPost]
        [SwaggerOperation("Create a new Student")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateStudentAsync([FromBody] CreateStudentViewModel student)
        {
            var studentId = await _studentService.CreateStudentAsync(student);
            return Response(studentId);
        }

        [HttpPut]
        [SwaggerOperation("Update an existing Student")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateStudentViewModel>))]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudentViewModel student)
        {
            await _studentService.UpdateStudentAsync(student);
            return Response(student);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete an existing Student")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid id)
        {
            await _studentService.DeleteStudentAsync(id);
            return Response(id);
        }


    }

}