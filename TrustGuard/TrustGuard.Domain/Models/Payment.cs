using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustGuard.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public double Price { get; set; }
        public int InsuranceType { get; set; }
    }
}
