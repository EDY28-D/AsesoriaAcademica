using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class AdvisoryContractRepository : BaseRepository<AdvisoryContract>, IAdvisoryContractRepository
{
    public AdvisoryContractRepository(ApplicationDbContext context) : base(context)
    {
    }
}