using API.Core.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public interface IGenericRepository <T> where T:BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}