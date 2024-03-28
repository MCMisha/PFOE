using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Setting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Style { get; set; } = null!;
        public int FontSize { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
