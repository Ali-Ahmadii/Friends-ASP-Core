using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class FreindsRepository : IFreindsRepository
    {
        private readonly MyContaxt _dbcontaxt;

        public FreindsRepository(MyContaxt dbcontext)
        {
            _dbcontaxt = dbcontext;
        }

        public async Task<List<Friend>> Read()
        {
            return await _dbcontaxt.Friends.ToListAsync();
        }

        public async Task Create(Friend f)
        {
            _dbcontaxt.Friends.Add(f);
            await Save();
        }

        public async Task Update(Friend u)
        {
            _dbcontaxt.Friends.Update(u);
            await Save();
        }

        public async Task Delete(int id)
        {
            Friend x = await _dbcontaxt.Friends.FindAsync(id);
            _dbcontaxt.Friends.Remove(x);
            await Save();
        }

        private async Task Save()
        {
            await _dbcontaxt.SaveChangesAsync();
        }
    }
}
