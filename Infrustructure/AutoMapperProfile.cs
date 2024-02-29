using WebApplication1.Models;

namespace WebApplication1.Infrustructure
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Friend,FriendsViewModel>();
        }
    }
}
