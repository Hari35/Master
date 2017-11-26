using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BettingModel.Models;

namespace BettingService
{
    internal interface ISettledCustomers
    {
        List<CustomerResponse> SettledCustomerDetails();
    }
}
