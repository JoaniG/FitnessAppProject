using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Entities.Models;

namespace DAL.Concrete
{
    internal class RoutineRepository : BaseRepository<Routine, Guid>, IRoutineRepository
    {
        public RoutineRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }

        public List<Routine> GetRoutinesByUserId(Guid userId)
        {
            return context.Where(x => x.UserId == userId).ToList();
        }
    }
}
