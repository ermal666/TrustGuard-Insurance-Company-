using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustGuard.Domain.Models
{
    public class Property
    {
        public int Id { get; set; }
        public int PropertyType { get; set; }
        public int OwnerId { get; set; }
    }
}
