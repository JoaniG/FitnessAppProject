using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
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
            return routineToAdd.Id;
            
        }

        public void UpdateRoutine(Routine routine)
        {
            routineRepository.Update(routine);
            _unitOfWork.Save();
        }
    }
}
