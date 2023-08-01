using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Entities.Models;

namespace DAL.Concrete
{
    internal class WorkoutExerciseRepository : BaseRepository<WorkoutExercise, Guid>, IWorkoutExerciseRepository
    {
        public WorkoutExerciseRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
