using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustGuard.Domain.Enums
{
    public enum InsuranceType
    {
        Casco = 1,
        Tpl = 2,
        Appartment = 3,
        House = 4,
        Health = 5,
        Accident = 6,
    }

    public enum InsuranceLevel
    {
        Minimal = 0,
        Normal = 1,
        Full = 2,
    }
}
