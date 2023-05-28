using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Domain.Enums;

namespace TrustGuard.Domain.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public int OwnerId { get; set; }
        public int? InsuranceLevel { get; set; }
    }
}
