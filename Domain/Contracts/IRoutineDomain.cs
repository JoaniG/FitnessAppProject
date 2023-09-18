using DTO;
using DTO.UserDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IRoutineDomain
    {
        List<Routine> GetRoutinesByUserId(Guid userId);
        void AddExercisesInRoutine(List<RoutineExerciseDTO> exercises);
        Routine GetRoutine(Guid id);
        Guid MakeNewRoutine(RoutineDTO routine);
        void DeleteRoutine(Guid id);
        void UpdateRoutine(RoutinePut routine);
        Guid AddRoutine(RoutinePost routine);

    }
}
