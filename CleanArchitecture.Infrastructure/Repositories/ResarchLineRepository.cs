using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class ResearchLineRepository : BaseRepository<ResearchLine>, IResearchLineRepository
{
    public ResearchLineRepository(ApplicationDbContext context) : base(context)
    {
    }
}