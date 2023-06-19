using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustGuard.Application.Dtos
{
    public class CreateCascoDto
    {
        public string VinNumber { get; set; }
        public string CarModel { get; set; }
        public string Plate { get; set; }
        public string Producer { get; set; }
        public string? Color { get; set; }
        public int EngineCapacity { get; set; }
        public int? SeatingCapacity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string UserId { get; set; }
        public int OfferId { get; set; }
    }
}
