using System;
using System.Collections.Generic;
using BettingModel.Models;

namespace BettingService
{
    public class BettingService : IBetting
    {
        private SettledCustomers settledCustomerDetails;
        private readonly Lazy<List<CustomerResponse>> lazySettledCustomerResponse;
        private readonly Lazy<List<CustomerResponse>> lazyUnSettledCustomerResponse;
        private const string settledText = "SETTLED";  
        private const string unSettledText = "UNSETTLED";  

        public CustomerRequest customerRequest { get; set; }

        public BettingService(CustomerRequest request)
        {
            customerRequest = request;
            lazySettledCustomerResponse = new Lazy<List<CustomerResponse>>(
                    () =>
                    {
                        settledCustomerDetails = new SettledCustomers();
                        return settledCustomerDetails.SettledCustomerDetails();
                    })
                ;
            lazyUnSettledCustomerResponse = new Lazy<List<CustomerResponse>>(()
                =>
            {
                var customer = new UnSettledCustomer();
                settledCustomerDetails = new SettledCustomers();
                List<SettledBettingDetails> settledList = settledCustomerDetails.settleditemList;
                return customer.UnSettledCustomerDetails(settledList,
                    settledCustomerDetails.GetCustomersPercentageses());
            });
        }

        public List<CustomerResponse> GetBettingDetails()
        {
            if (customerRequest.Type.ToUpper() == unSettledText)
                if (!lazySettledCustomerResponse.IsValueCreated)
                {
                    IEnumerable<CustomerResponse> unSettledCustomerResponses = lazyUnSettledCustomerResponse.Value;

                    return new List<CustomerResponse>(unSettledCustomerResponses);
                }

            if (customerRequest.Type.ToUpper() == settledText)
                if (!lazyUnSettledCustomerResponse.IsValueCreated)
                {
                    IEnumerable<CustomerResponse> settledCustomerResponses = lazySettledCustomerResponse.Value;
                    return new List<CustomerResponse>(settledCustomerResponses);
                }
            return default(List<CustomerResponse>);
        }
    }
}
