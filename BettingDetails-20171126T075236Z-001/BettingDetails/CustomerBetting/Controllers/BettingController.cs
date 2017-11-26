using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Tracing;
using BettingModel.Models;

namespace CustomerBetting
{
    public class BettingController : ApiController
    {
        [Route("api/BettingDetails/GetCustomers")]
        [HttpPost]
        public List<CustomerResponse> AjaxMethod(CustomerRequest customer)
        {
            //Need to have the Logger that logs the traffic

            Configuration.Services.GetTraceWriter().Info(
                Request, "BettingController", "Get the Betting Details{0}.",customer.Type);
            var bettingService = new BettingService.BettingService(customer);
            List<CustomerResponse> response = bettingService.GetBettingDetails();

        
            return response;
        }
    }
}