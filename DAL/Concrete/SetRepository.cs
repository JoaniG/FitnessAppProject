using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Entities.Models;

namespace DAL.Concrete
{
    internal class SetRepository : BaseRepository<Set, Guid>, ISetRepository
    {
        public SetRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
