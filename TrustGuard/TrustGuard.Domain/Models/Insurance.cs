using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustGuard.Domain.Models
{
    public class Insurance
    {
        public int Id { get; set; }
        public int InsuredProductId { get; set; }
        public int TypeId { get; set; }
        public int LevelId { get; set; }
        public int Price { get; set; }
    }
}
