using System;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.IntegrationTests.Fixtures;

public sealed class ResearchLineTestFixture : TestFixtureBase
{
    public Guid CreatedResearchLineId { get; } = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
        context.ResearchGroups.Add(new ResearchGroup(Ids.Seed.ResearchGroupId,
           "testname","tescode"));
        base.SeedTestData(context);
        context.ResearchLines.Add(new ResearchLine(
            CreatedResearchLineId,
            "Test ResearchLine", Ids.Seed.ResearchGroupId, "sw123"));

       // context.Users.Add(new User(
          //  Guid.NewGuid(),
           // CreatedResearchLineId,
            //"test@user.de",
            //"test",
           // "user",
           // "Test User",
           // UserRole.User));

        context.SaveChanges();
    }
}