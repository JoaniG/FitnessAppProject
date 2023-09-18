using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
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
    internal class RoutineDomain : DomainBase, IRoutineDomain
    {
        public RoutineDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private IRoutineRepository routineRepository => _unitOfWork.GetRepository<IRoutineRepository>();
        private IRoutineExerciseRepository routineExerciseRepository => _unitOfWork.GetRepository<IRoutineExerciseRepository>();
        

        public void DeleteRoutine(Guid id)
        {
            routineRepository.Remove(id);
            _unitOfWork.Save();
        }

        public Routine GetRoutine(Guid id)
        {
            var routine = routineRepository.GetById(id);
            return routine;
        }

        public void AddExercisesInRoutine(List<RoutineExerciseDTO> exercises)
        {
            List<RoutineExercise> routineExercises = new List<RoutineExercise>();
            foreach (var exercise in exercises)
            {
                RoutineExercise exerciseToAdd = new RoutineExercise
                {
                    RoutineId = exercise.RoutineId,
                    ExerciseId = exercise.ExerciseId,
                    Sets = exercise.Sets,
                    Description = exercise.Description
                };
                routineExercises.Add(exerciseToAdd);
            }
            routineExerciseRepository.AddRange(routineExercises);
            _unitOfWork.Save();
        }

        public Guid AddRoutine(RoutinePost routine)
        {
            Guid routineId = Guid.NewGuid();
            List<RoutineExercise> exercises = new List<RoutineExercise>();

            foreach(var exercise in routine.RoutineExercises)
            {
                RoutineExercise exerciseToAdd = new RoutineExercise
                {
                    RoutineId = routineId,
                    ExerciseId = exercise.ExerciseId,
                    Sets = exercise.Sets,
                    Description = exercise.Description
                };
                exercises.Add(exerciseToAdd);
            }

            Routine routineToAdd = new Routine
            {
                Id = routineId,
                UserId = routine.UserId,
                Name = routine.Name,
                Description = routine.Description,
                RoutineExercises = exercises
            };
            var id = routineRepository.Add(routineToAdd).Id;
            _unitOfWork.Save();
            return id;
        }

        public List<Routine> GetRoutinesByUserId(Guid userId)
        {
            var routines = routineRepository.GetRoutinesByUserId(userId);
            return routines;
        }

        public Guid MakeNewRoutine(RoutineDTO routine)
        {
            Routine routineToAdd = new Routine
            {
                Id = Guid.NewGuid(),
                Name = routine.Name,
                Description = routine.Description,
                UserId = routine.UserId
            };

            routineRepository.Add(routineToAdd);
            _unitOfWork.Save();
            return routineToAdd.Id;
            
        }

        public void UpdateRoutine(RoutinePut routine)
        {
            List<RoutineExercise> exercises = new List<RoutineExercise>();

            foreach(var exercise in routine.RoutineExercises)
            {
                RoutineExercise routineExercise = new RoutineExercise
                {
                    ExerciseId = exercise.ExerciseId,
                    RoutineId = exercise.RoutineId,
                    Sets = exercise.Sets,
                    Description = exercise.Description
                };
                routineExerciseRepository.Update(routineExercise);
                exercises.Add(routineExercise);
            }

            Routine updatedRoutine = new Routine
            {
                Id = routine.Id,
                UserId = routine.UserId,
                Name = routine.Name,
                Description = routine.Description,
                //RoutineExercises = exercises
            };
            routineRepository.Update(updatedRoutine);
            _unitOfWork.Save();
        }
    }
}
