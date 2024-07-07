using System;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.IntegrationTests.Fixtures;

public sealed class AdvisoryContractTestFixture : TestFixtureBase
{
    public Guid CreatedAdvisoryContractId { get;} = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
       base.SeedTestData(context);
        context.ResearchGroups.Add(new ResearchGroup(Ids.Seed.ResearchGroupId,"test","test"));
        context.ResearchLines.Add(new ResearchLine(Ids.Seed.ResearchLineId, "test", Ids.Seed.ResearchGroupId,"test"));
        context.Professors.Add(new Professor(Ids.Seed.ProfessorId,Ids.Seed.UserId,Ids.Seed.ResearchGroupId,false));
        context.Students.Add(new Student(Ids.Seed.StudentId, Ids.Seed.UserId, "test"));
       
        context.AdvisoryContracts.Add(new AdvisoryContract(
            CreatedAdvisoryContractId, Ids.Seed.ProfessorId, Ids.Seed.StudentId, Ids.Seed.ResearchLineId, "test","test","test"));
       // context.Tenants.Add(new Tenant(
       //    Ids.Seed.TenantId,
       //    "Test Tenant"));
       // context.Users.Add(new User(
       //  Guid.NewGuid(),
       // Ids.Seed.TenantId,
       //"test@user.de",
       //"test",
       // "user",
       // "Test User",
       // UserRole.User));

        context.SaveChanges();
    }
}