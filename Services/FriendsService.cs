using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class FriendsService : IFreindsRepository
    {
        private readonly IFreindsRepository _f;

        public FriendsService(IFreindsRepository ff)
        {
            _f = ff;
        }

        public async Task Create(Friend f)
        {
            await _f.Create(f);
        }

        public async Task Delete(int id)
        {
            await _f.Delete(id);
        }

        public async Task<List<Friend>> Read()
        {
            return await _f.Read();
        }

        public async Task Update(Friend u)
        {
            await _f.Update(u);
        }
    }
}
