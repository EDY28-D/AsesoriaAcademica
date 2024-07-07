using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.IntegrationTests.Fixtures;

public sealed class ResearchGroupTestFixture : TestFixtureBase
{
    public Guid CreatedResearchGroupId { get; } = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);

        context.ResearchGroups.Add(new ResearchGroup(
            CreatedResearchGroupId,
            "Test ResearchGroup","sw123"));

       // context.Users.Add(new User(
          //  Guid.NewGuid(),
           // CreatedResearchGroupId,
            //"test@user.de",
            //"test",
           // "user",
           // "Test User",
           // UserRole.User));

        context.SaveChanges();
    }
}