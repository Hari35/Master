using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BettingModel.Models;

namespace BettingService
{
    interface IUnSettledCustomers
    {
        List<CustomerResponse> UnSettledCustomerDetails(List<SettledBettingDetails> settledCustomers, IEnumerable<SettledCustomersPercentages> settledCustomerPercentages);
    }
}
