using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class WorkoutExercise
    {
        public WorkoutExercise()
        {
            Sets = new HashSet<Set>();
        }

        public Guid Id { get; set; }
        public string? Description { get; set; } = null!;
        public Guid WorkoutId { get; set; }
        public Guid ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; } = null!;
        public virtual Workout Workout { get; set; } = null!;
        public virtual ICollection<Set> Sets { get; set; }
    }
}
