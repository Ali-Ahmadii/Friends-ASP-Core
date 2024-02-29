using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface IDatabase
    {
        Task<List<FriendsViewModel>> GetAllFriendsAsync();
    }

    public class Database : IDatabase
    {
        private readonly IMapper _mapper;

        public Database(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<FriendsViewModel>> GetAllFriendsAsync()
        {
            // Simulate asynchronous operation using Task.Run
            return await Task.Run(() =>
            {
                var friends = new List<Friend>();
                for (int i = 0; i < 10; i++)
                {
                    friends.Add(new Friend { Name = "Friend:" + i, Phone = i, Profile = "/images/Friend" + i + ".jfif" });
                }
                return friends.Select(a => _mapper.Map<Friend, FriendsViewModel>(a)).ToList();
            });
        }
    }
}
