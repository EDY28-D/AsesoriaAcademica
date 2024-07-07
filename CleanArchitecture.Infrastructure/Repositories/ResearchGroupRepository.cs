using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class ResearchGroupRepository : BaseRepository<ResearchGroup>, IResearchGroupRepository
{
    public ResearchGroupRepository(ApplicationDbContext context) : base(context)
    {
    }
}