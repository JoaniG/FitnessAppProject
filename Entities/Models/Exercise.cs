using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            RoutineExercises = new HashSet<RoutineExercise>();
            WorkoutExercises = new HashSet<WorkoutExercise>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int PrimaryTarget { get; set; }
        public int? SecondaryTarget { get; set; }

        public virtual ICollection<RoutineExercise> RoutineExercises { get; set; }
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
