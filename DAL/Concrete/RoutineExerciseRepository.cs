using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Entities.Models;

namespace DAL.Concrete
{
    internal class RoutineExerciseRepository : BaseRepository<RoutineExercise, Guid>, IRoutineExerciseRepository
    {
        public RoutineExerciseRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }

        public List<RoutineExercise> GetExerciseIdsInRoutine(Guid routineId)
        {
            var routineExercises = context.Where(a => a.RoutineId == routineId).ToList();
            return routineExercises;
        }
    }
}
