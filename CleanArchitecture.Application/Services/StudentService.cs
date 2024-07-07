using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Professors.GetAll;
using CleanArchitecture.Application.Queries.Students.GetAll;
using CleanArchitecture.Application.Queries.Students.GetStudentById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Students.CreateStudent;
using CleanArchitecture.Domain.Commands.Students.DeleteStudent;
using CleanArchitecture.Domain.Commands.Students.UpdateStudent;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class StudentService : IStudentService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public StudentService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateStudentAsync(CreateStudentViewModel Student)
    {
        var StudentId = Guid.NewGuid();
        await _bus.SendCommandAsync(new CreateStudentCommand(
            StudentId,Student.UserId,Student.Code));

        return StudentId;
    }

    public async Task UpdateStudentAsync(UpdateStudentViewModel Student)
    {
        await _bus.SendCommandAsync(new UpdateStudentCommand(
            Student.Id,Student.UserId,
            Student.Code));
    }

    public async Task DeleteStudentAsync(Guid StudentId)
    {
        await _bus.SendCommandAsync(new DeleteStudentCommand(StudentId));
    }

    public async Task<StudentViewModel?> GetStudentByIdAsync(Guid StudentId)
    {
        var cachedStudent = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Student>(StudentId),
            async () => await _bus.QueryAsync(new GetStudentByIdQuery(StudentId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedStudent;
    }

    public async Task<PagedResult<StudentViewModel>> GetAllStudentsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new StudentsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}
