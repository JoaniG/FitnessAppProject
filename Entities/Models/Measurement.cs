using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Measurement
    {
        public Guid Id { get; set; }
        public int? Chest { get; set; }
        public int? Waist { get; set; }
        public int? Hips { get; set; }
        public int? Biceps { get; set; }
        public int? Thighs { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
