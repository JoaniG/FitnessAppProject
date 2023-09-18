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
        void AddExercisesInWorkout(List<WorkoutExerciseDTO> workoutExercises);
        List<WorkoutExercise> GetWorkoutExercises(Guid workoutId);
        void AddSets(List<SetDTO> sets);
        Guid AddWorkout(WorkoutPOST workout);
    }
}
