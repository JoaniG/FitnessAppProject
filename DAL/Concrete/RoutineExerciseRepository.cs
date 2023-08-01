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
    }
}
