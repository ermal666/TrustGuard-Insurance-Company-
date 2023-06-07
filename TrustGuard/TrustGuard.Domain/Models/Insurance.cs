using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustGuard.Domain.Models
{
    public class Insurance
    {
        public int InsuranceId { get; set; }
        public string? InsuranceType { get; set; }
        
        public ICollection<Offer> Offers { get; set; }
    }
}
