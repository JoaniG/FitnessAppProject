using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IUserRepository: IRepository<User, Guid>
    {
        User GetByUsername(string username);
        //User CreateUser(User user);
        //bool DeleteUser(Guid id);
        //bool UpdateUser(User user);
    }
}
