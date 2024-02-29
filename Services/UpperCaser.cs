using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UpperCaser
    {
        private MyContaxt _dbcontext;
        public UpperCaser(MyContaxt dbContext)
        {
            _dbcontext = dbContext;
        }
        public void Uppercaser(Friend frined)
        {
            frined.Name = frined.Name.ToUpper();
            _dbcontext.Friends.Update(frined);
            _dbcontext.SaveChanges();
        }
    }
}
