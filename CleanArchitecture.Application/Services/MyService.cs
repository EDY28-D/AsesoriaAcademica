using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Professors.GetAll;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using CleanArchitecture.Domain.Settings;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Application.Services;
/// <summary>
/// By Perez
/// </summary>
public sealed class MyService : IMyService
{
    private readonly NewSettings _settings;

    public MyService(IOptions<NewSettings> settings)
    {
        _settings = settings.Value;
    }

    public void UseSettings()
    {
        Console.WriteLine($"Param1: {_settings.Param1}");
        Console.WriteLine($"Param2: {_settings.Param2}");
    }
}