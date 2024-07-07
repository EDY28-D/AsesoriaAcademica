using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
//[Authorize]
[Route("/api/v1/[controller]")]
public sealed class ResearchLineController : ApiController
{
    private readonly IResearchLineService _ResearchLineService;

    public ResearchLineController(
        INotificationHandler<DomainNotification> notifications,
        IResearchLineService ResearchLineService) : base(notifications)
    {
        _ResearchLineService = ResearchLineService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all ResearchLines")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ResearchLineViewModel>>))]
    public async Task<IActionResult> GetAllResearchLinesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<ResearchLineViewModelSortProvider, ResearchLineViewModel, ResearchLine>]
        SortQuery? sortQuery = null)
    {
        var ResearchLines = await _ResearchLineService.GetAllResearchLinesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(ResearchLines);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a ResearchLine by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ResearchLineViewModel>))]
    public async Task<IActionResult> GetResearchLineByIdAsync([FromRoute] Guid id)
    {
        var ResearchLine = await _ResearchLineService.GetResearchLineByIdAsync(id);
        return Response(ResearchLine);
    }

    [HttpPost]
    [SwaggerOperation("Create a new ResearchLine")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateResearchLineAsync([FromBody] CreateResearchLineViewModel ResearchLine)
    {
        var ResearchLineId = await _ResearchLineService.CreateResearchLineAsync(ResearchLine);
        return Response(ResearchLineId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing ResearchLine")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateResearchLineViewModel>))]
    public async Task<IActionResult> UpdateResearchLineAsync([FromBody] UpdateResearchLineViewModel ResearchLine)
    {
        await _ResearchLineService.UpdateResearchLineAsync(ResearchLine);
        return Response(ResearchLine);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing ResearchLine")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteResearchLineAsync([FromRoute] Guid id)
    {
        await _ResearchLineService.DeleteResearchLineAsync(id);
        return Response(id);
    }
}