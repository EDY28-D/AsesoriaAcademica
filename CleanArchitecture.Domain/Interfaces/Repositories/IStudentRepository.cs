using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        // Add any additional methods specific to Student repository if needed
    }
}
