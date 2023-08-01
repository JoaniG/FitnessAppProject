using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Entities.Models;

namespace DAL.Concrete
{
    internal class WorkoutRepository : BaseRepository<Workout, Guid>, IWorkoutRepository
    {
        public WorkoutRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Workout> GetWorkoutsByUserId(Guid userId)
        {
            return context.Where(x => x.UserId == userId);
        }
    }
}
