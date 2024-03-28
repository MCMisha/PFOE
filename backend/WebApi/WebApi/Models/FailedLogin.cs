using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class FailedLogin
    {
        public int Id { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int UserId { get; set; }
    }
}
