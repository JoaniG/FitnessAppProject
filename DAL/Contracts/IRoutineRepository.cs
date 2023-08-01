using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IRoutineRepository:IRepository<Routine, Guid>
    {
        IEnumerable<Routine> GetRoutinesByUserId(Guid userId);
        //Routine GetRoutineById(Guid id);
        //bool CreateNewRoutine(Routine routine);
        //bool DeleteRoutine(Guid id);
        //bool UpdateRoutine(Routine routine);
        
    }
}
