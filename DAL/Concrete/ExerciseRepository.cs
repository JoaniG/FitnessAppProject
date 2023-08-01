using DAL.Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class ExerciseRepository : BaseRepository<Exercise, Guid>, IExerciseRepository
    {
        public ExerciseRepository(FitnessAppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
