using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SetDTO
    {
        public int Reps { get; set; }
        public int Weight { get; set; }
        public Guid WorkoutExerciseId { get; set; }
    }
}
