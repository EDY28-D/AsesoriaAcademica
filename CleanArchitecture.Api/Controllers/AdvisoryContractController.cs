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
public sealed class AdvisoryContractController : ApiController
{
    private readonly IAdvisoryContractService _AdvisoryContractService;

    public AdvisoryContractController(
        INotificationHandler<DomainNotification> notifications,
        IAdvisoryContractService AdvisoryContractService) : base(notifications)
    {
        _AdvisoryContractService = AdvisoryContractService;
    }

    [HttpGet]
    [EnableCors("AllowAll")]
    [SwaggerOperation("Get a list of all AdvisoryContracts")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<AdvisoryContractViewModel>>))]
    public async Task<IActionResult> GetAllAdvisoryContractsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<AdvisoryContractViewModelSortProvider, AdvisoryContractViewModel, AdvisoryContract>]
        SortQuery? sortQuery = null)
    {
        var AdvisoryContracts = await _AdvisoryContractService.GetAllAdvisoryContractsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(AdvisoryContracts);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a AdvisoryContract by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<AdvisoryContractViewModel>))]
    public async Task<IActionResult> GetAdvisoryContractByIdAsync([FromRoute] Guid id)
    {
        var AdvisoryContract = await _AdvisoryContractService.GetAdvisoryContractByIdAsync(id);
        return Response(AdvisoryContract);
    }

    [HttpPost]
    [SwaggerOperation("Create a new AdvisoryContract")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateAdvisoryContractAsync([FromBody] CreateAdvisoryContractViewModel AdvisoryContract)
    {
        var AdvisoryContractId = await _AdvisoryContractService.CreateAdvisoryContractAsync(AdvisoryContract);
        return Response(AdvisoryContractId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing AdvisoryContract")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateAdvisoryContractViewModel>))]
    public async Task<IActionResult> UpdateAdvisoryContractAsync([FromBody] UpdateAdvisoryContractViewModel AdvisoryContract)
    {
        await _AdvisoryContractService.UpdateAdvisoryContractAsync(AdvisoryContract);
        return Response(AdvisoryContract);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing AdvisoryContract")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteAdvisoryContractAsync([FromRoute] Guid id)
    {
        await _AdvisoryContractService.DeleteAdvisoryContractAsync(id);
        return Response(id);
    }
}