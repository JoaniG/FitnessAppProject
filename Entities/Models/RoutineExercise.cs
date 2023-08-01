using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class RoutineExercise
    {
        public Guid RoutineId { get; set; }
        public Guid ExerciseId { get; set; }
        public int Sets { get; set; }
        public string? Description { get; set; }

        public virtual Exercise Exercise { get; set; } = null!;
        public virtual Routine Routine { get; set; } = null!;
    }
}
