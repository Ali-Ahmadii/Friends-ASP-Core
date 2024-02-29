using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IFreindsRepository
    {
        Task Create(Friend f);
        Task Delete(int id);
        Task<List<Friend>> Read();
        Task Update(Friend u);
    }
}
