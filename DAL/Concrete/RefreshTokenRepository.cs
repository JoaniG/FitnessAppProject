using DAL.Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class RefreshTokenRepository : BaseRepository<RefreshToken, Guid>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {}
        public RefreshToken GetByValue(string tokenValue)
        {
            var user = context.Where(a => a.Token == tokenValue).FirstOrDefault();
            return user;
        }
    }
}
