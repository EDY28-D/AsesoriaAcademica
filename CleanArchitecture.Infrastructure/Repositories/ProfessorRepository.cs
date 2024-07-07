using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class ProfessorRepository : BaseRepository<Professor>, IProfessorRepository
{
    public ProfessorRepository(ApplicationDbContext context) : base(context)
    {
    }
}