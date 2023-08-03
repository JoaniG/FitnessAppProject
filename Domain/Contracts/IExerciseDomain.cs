using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public  interface IExerciseDomain
    {
        List<RoutineExercise> GetExercisesInRoutine(Guid routineId);
        Exercise GetExerciseById(Guid id);
        List<Exercise> GetAllExercises();
        void AddExercise(Exercise exercise);
    }
}
