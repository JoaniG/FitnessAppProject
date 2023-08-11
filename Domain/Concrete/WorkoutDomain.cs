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
        public WorkoutDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private IWorkoutRepository workoutRepository => _unitOfWork.GetRepository<IWorkoutRepository>();
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
            return workoutToAdd.Id;
        }

        public Workout GetWorkout(Guid id)
        {
            var workout = workoutRepository.GetById(id);
            return workout;
        }

        public List<Workout> GetWorkoutsByUser(Guid userId)
        {
            var workouts = workoutRepository.GetWorkoutsByUserId(userId);
            return workouts;
        }
    }
}
