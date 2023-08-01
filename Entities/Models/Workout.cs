using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Workout
    {
        public Workout()
        {
            WorkoutExercises = new HashSet<WorkoutExercise>();
        }

        public Guid Id { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeEnded { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
