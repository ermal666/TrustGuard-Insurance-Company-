using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Service.Services
{
    public class InsuranceService
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceService(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }

        public async Task CreateInsurance()
        {
            
        }
    }
}
