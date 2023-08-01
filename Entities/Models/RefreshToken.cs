using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public DateTime IssuedDate { get; set; }
        public bool Revoked { get; set; }

        public virtual User? User { get; set; }
    }
}
