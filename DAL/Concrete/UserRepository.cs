using DAL.Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {

        public UserRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }

        public User GetByUsername(string username)
        {
            var user = context.Where(a => a.Username == username).FirstOrDefault();
            return user;
        }

    }


}
