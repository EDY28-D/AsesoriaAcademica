using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Cors;

namespace CleanArchitecture.Api.Controllers;
[EnableCors("AllowAll")]
[ApiController]
//[Authorize]
[Route("/api/v1/[controller]")]
/// <summary>
/// By Perez
/// </summary>


    public sealed class MyController : ControllerBase
    {
        private readonly IMyService _service;

        public MyController(IMyService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _service.UseSettings();
            return Ok("Settings used successfully");
        }
    }
