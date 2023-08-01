using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Set
    {
        public Guid Id { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
        public Guid WorkoutExerciseId { get; set; }

        public virtual WorkoutExercise WorkoutExercise { get; set; } = null!;
    }
}
