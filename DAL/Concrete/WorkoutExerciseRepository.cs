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
    internal class WorkoutExerciseRepository : BaseRepository<WorkoutExercise, Guid>, IWorkoutExerciseRepository
    {
        public WorkoutExerciseRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }

        public List<WorkoutExercise> GetExercisesByWorkoutId(Guid workoutId)
        {
            var exercises = context.Include(x => x.Sets).Where(a => a.WorkoutId == workoutId).ToList();
            return exercises;
        }
    }
}
