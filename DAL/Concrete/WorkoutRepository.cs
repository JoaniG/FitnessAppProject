using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    internal class WorkoutRepository : BaseRepository<Workout, Guid>, IWorkoutRepository
    {
        public WorkoutRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }

        public List<Workout> GetWorkoutsByUserId(Guid userId)
        {
            return context.Include(x => x.WorkoutExercises).ThenInclude(x => x.Sets).Include(x => x.WorkoutExercises).ThenInclude(x => x.Exercise).Where(x => x.UserId == userId).ToList();
        }

        public Workout GetWorkoutById(Guid id)
        {
            var workout = context.Include(x => x.WorkoutExercises).ThenInclude(x => x.Sets).Include(x => x.WorkoutExercises).ThenInclude(x => x.Exercise).Where(a => a.Id == id).SingleOrDefault();
            return workout;
        }
    }
}
