using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace DAL.Contracts
{
    public interface IRoutineExerciseRepository : IRepository<RoutineExercise, Guid>
    {
        List<RoutineExercise> GetExerciseIdsInRoutine(Guid routineId);
    }
}
