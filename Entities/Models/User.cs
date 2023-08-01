using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class User
    {
        public User()
        {
            Measurements = new HashSet<Measurement>();
            RefreshTokens = new HashSet<RefreshToken>();
            Routines = new HashSet<Routine>();
            Workouts = new HashSet<Workout>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public bool WeightSetting { get; set; }
        public bool HeightSetting { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
