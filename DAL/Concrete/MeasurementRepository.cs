﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Entities.Models;

namespace DAL.Concrete
{
    internal class MeasurementRepository : BaseRepository<Measurement, Guid>, IMeasurementRepository
    {
        public MeasurementRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
