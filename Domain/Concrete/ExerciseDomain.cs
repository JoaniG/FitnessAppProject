using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class ExerciseDomain : DomainBase, IExerciseDomain
    {
        public ExerciseDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private IExerciseRepository exerciseRepository => _unitOfWork.GetRepository<IExerciseRepository>();
        private IRoutineExerciseRepository routineExerciseRepository => _unitOfWork.GetRepository<IRoutineExerciseRepository>();

        public void AddExercise(Exercise exercise)
        {
            exerciseRepository.Add(exercise);
            _unitOfWork.Save();
        }

        public List<Exercise> GetAllExercises()
        {
            var exercises = exerciseRepository.GetAll().ToList();
            return exercises;
        }

        public Exercise GetExerciseById(Guid id)
        {
            var exercise = exerciseRepository.GetById(id);
            return exercise;
        }

        public List<RoutineExercise> GetExercisesInRoutine(Guid routineId)
        {
            var routineExercises = routineExerciseRepository.GetExerciseIdsInRoutine(routineId);
            return routineExercises;
        }
    }
}
