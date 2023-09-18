using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.Concrete
{
    internal class WorkoutDomain : DomainBase, IWorkoutDomain
    {
        private readonly IUserDomain _userDomain;
        public WorkoutDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserDomain userDomain) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _userDomain = userDomain;
        }
        private IWorkoutRepository workoutRepository => _unitOfWork.GetRepository<IWorkoutRepository>();
        private IWorkoutExerciseRepository workoutExerciseRepository => _unitOfWork.GetRepository<IWorkoutExerciseRepository>();
        private ISetRepository setRepository => _unitOfWork.GetRepository<ISetRepository>();
        public Guid CreateWorkout(WorkoutDTO workout)
        {
            Workout workoutToAdd = new Workout
            {
                Id = Guid.NewGuid(),
                TimeEnded = workout.TimeEnded,
                TimeStarted = workout.TimeStarted,
                UserId = workout.UserId
            };

            workoutRepository.Add(workoutToAdd);
            _unitOfWork.Save();
            return workoutToAdd.Id;
        }

        public Guid AddWorkout(WorkoutPOST workout)
        {
            Guid workoutId = Guid.NewGuid();
            List<WorkoutExercise> workoutExercises = new List<WorkoutExercise>();

            foreach(var exercise in workout.workoutExercises)
            {
                var exerciseId = Guid.NewGuid();
                List<Set> sets = new List<Set>();
                foreach (var set in exercise.Sets)
                {
                    bool isMetric = _userDomain.GetUserSettings(workout.UserId);
                    int weight = set.Weight;
                    if (!isMetric)
                    {
                        weight = (int)(set.Weight / 2.205);
                    }
                    Set setToAdd = new Set
                    {
                        Id = Guid.NewGuid(),
                        WorkoutExerciseId = exerciseId,
                        Weight = weight,
                        Reps = set.Reps
                    };
                    sets.Add(setToAdd);
                }
                WorkoutExercise exerciseToAdd = new WorkoutExercise
                {
                    Id = exerciseId,
                    ExerciseId = exercise.ExerciseId,
                    Description = exercise.Description ?? "",
                    Sets = sets,
                    WorkoutId = workoutId
                };
                workoutExercises.Add(exerciseToAdd);
            }
            Workout workoutToAdd = new Workout
            {
                Id = workoutId,
                TimeEnded = workout.TimeEnded,
                TimeStarted = workout.TimeStarted,
                UserId = workout.UserId,
                WorkoutExercises = workoutExercises
            };
            var addedWorkout = workoutRepository.Add(workoutToAdd);
            _unitOfWork.Save();
            return addedWorkout.Id;
        }

        public Workout GetWorkout(Guid id)
        {
            var workout = workoutRepository.GetWorkoutById(id);
            bool isMetric = _userDomain.GetUserSettings(workout.UserId);
            if (!isMetric)
            {
                foreach(var workoutExercise in workout.WorkoutExercises)
                {
                    foreach(var set in workoutExercise.Sets)
                    {
                        set.Weight = (int)(set.Weight * 2.205);
                    }
                }
            }
            workout.User = null;
            return workout;
        }

        public void AddExercisesInWorkout(List<WorkoutExerciseDTO> workoutExercises)
        {
            List<WorkoutExercise> workoutExercisesToAdd = new List<WorkoutExercise>();
            foreach(WorkoutExerciseDTO workoutExercise in workoutExercises)
            {
                WorkoutExercise exercise = new WorkoutExercise
                {
                    Id = Guid.NewGuid(),
                    Description = workoutExercise.Description,
                    WorkoutId = workoutExercise.WorkoutId,
                    ExerciseId = workoutExercise.ExerciseId
                };
                workoutExercisesToAdd.Add(exercise);
            }
            workoutExerciseRepository.AddRange(workoutExercisesToAdd);
            _unitOfWork.Save();
        }

        public List<Workout> GetWorkoutsByUser(Guid userId)
        {
            var workouts = workoutRepository.GetWorkoutsByUserId(userId);
            var sortedWorkouts = workouts.OrderByDescending(workout => workout.TimeEnded).ToList();
            bool isMetric = _userDomain.GetUserSettings(userId);
            if (!isMetric)
            {
                foreach (var workout in sortedWorkouts)
                {
                    foreach (var workoutExercise in workout.WorkoutExercises)
                    {
                        foreach (var set in workoutExercise.Sets)
                        {
                            set.Weight = (int)(set.Weight * 2.205);
                        }
                    }
                    workout.User = null;
                }
            }
            return sortedWorkouts;
        }

        public List<WorkoutExercise> GetWorkoutExercises(Guid workoutId)
        {
            var exercises = workoutExerciseRepository.GetExercisesByWorkoutId(workoutId);
            return exercises;
        }

        public void AddSets(List<SetDTO> sets)
        {
            List<Set> setList = new List<Set>();

            foreach(SetDTO set in sets)
            {
                Set setToAdd = new Set
                {
                    Id = Guid.NewGuid(),
                    Reps = set.Reps,
                    Weight = set.Weight,
                    WorkoutExerciseId = set.WorkoutExerciseId
                };
                setList.Add(setToAdd);
            }
            setRepository.AddRange(setList);
            _unitOfWork.Save();
        }

    }
}
