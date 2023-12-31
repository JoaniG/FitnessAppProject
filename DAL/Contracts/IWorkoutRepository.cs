﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IWorkoutRepository : IRepository<Workout, Guid>
    {
        List<Workout> GetWorkoutsByUserId(Guid userId);
        Workout GetWorkoutById(Guid id);
    }
}
