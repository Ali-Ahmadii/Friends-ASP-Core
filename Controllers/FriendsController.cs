using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IDatabase _fakeDb;

        public FriendsController(IDatabase database)
        {
            _fakeDb = database;

        }

        static List<Friend> _friends = new List<Friend>();
        static List<FriendsViewModel> _friends_ = new List<FriendsViewModel>();
        static int flag = 0;

        public static async Task<int> GetTotalFriendsAsync()
        {
            if (flag == 0)
            {
                await CreateFriendsAsync();
                flag = 1;
            }

            return _friends.Count;
        }

        static async Task CreateFriendsAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                _friends.Add(new Friend { Name = "Friend:" + i, Phone = i, Profile = "/images/Friend" + i + ".jfif" });
            }
        }

        public async Task<IActionResult> Index()
        {
            if (flag == 0)
            {
                await CreateFriendsAsync();
                flag = 1;
            }
            return View();
        }

        public async Task<IActionResult> List()
        {
            if (flag == 0)
            {
                _friends_ = await _fakeDb.GetAllFriendsAsync();
            }
            return View(_friends_);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (flag == 0)
            {
                await CreateFriendsAsync();
                flag = 1;
            }
            return View(_friends.ElementAt(id));
        }

        [HttpGet]
        public IActionResult AddFriend()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFriend(Models.Friend friend)
        {
            if (flag == 0)
            {
                CreateFriendsAsync();
                flag = 1;
            }
            _friends.Add(new Friend { Name = friend.Name, Phone = friend.Phone, Profile = friend.Profile });
            return Redirect("List");
        }



    }
}
