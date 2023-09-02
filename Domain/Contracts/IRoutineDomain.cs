using DTO;
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
        Routine GetRoutine(Guid id);
        Guid MakeNewRoutine(RoutineDTO routine);
        void DeleteRoutine(Guid id);
        void UpdateRoutine(Routine routine);

    }
}
