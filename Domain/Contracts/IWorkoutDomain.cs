using DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IWorkoutDomain
    {
        Workout GetWorkout(Guid id);
        Guid CreateWorkout(WorkoutDTO workout);
        List<Workout> GetWorkoutsByUser(Guid userId);
    }
}
