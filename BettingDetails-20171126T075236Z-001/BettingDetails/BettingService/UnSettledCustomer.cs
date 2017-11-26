using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using BettingModel.Models;

namespace BettingService
{
    public class UnSettledCustomer : IUnSettledCustomers
    {
        private readonly string unSettledCSVFileLocation;
        private readonly string[] unSettledFileData;

        public UnSettledCustomer()
        {
            unSettledCSVFileLocation = ConfigurationSettings.AppSettings["CSVPath"] + "//Unsettled.csv";
            unSettledFileData = File.ReadAllLines(unSettledCSVFileLocation);
        }

        public List<CustomerResponse> UnSettledCustomerDetails(List<SettledBettingDetails> settledCustomers,
            IEnumerable<SettledCustomersPercentages> settledCustomerPercentages)
        {
            IEnumerable<string[]> unSettledData = unSettledFileData.Skip(1).Select(l => l.Split(',').ToArray());

            List<UnSettledBettingItems> unsettleditemList = unSettledData.Select(values => new UnSettledBettingItems
                {
                    Customer = Convert.ToInt32(values[0]),
                    Event = values[1],
                    Participant = values[2],
                    Stake = Convert.ToInt32(values[3]),
                    ToWin = Convert.ToInt32(values[4])
                })
                .ToList();

            var unsettledCustomers = unsettleditemList.GroupBy(g => g.Customer)
                .Select(g => new
                {
                    Id = Convert.ToInt32(g.Key),
                    TotalStake = g.Sum(xx => xx.Stake),
                    Avg = g.Average(ss => ss.ToWin),
                    AvgerageBet = g.Average(ss => ss.Stake)
                });

            var unusalCustomers = unsettledCustomers.Where(gh => gh.TotalStake >
                                                                 10 * settledCustomerPercentages
                                                                     .Select(bn => bn.AvgerageBet)
                                                                     .FirstOrDefault());


            var highlyUnusalCustomers = unsettledCustomers.Where(gh => gh.TotalStake >
                                                                       30 * settledCustomerPercentages
                                                                           .Select(bn => bn.AvgerageBet)
                                                                           .FirstOrDefault());

            List<CustomerResponse> unUsualCustomers = unusalCustomers.Select(unUsalCustomers => new CustomerResponse
                {
                    Id = unUsalCustomers.Id,
                    TotalStake = unUsalCustomers.TotalStake,
                    AvgerageBet = unUsalCustomers.AvgerageBet,
                    TypeofCustomer = "UnUsal Betting Rate"
                })
                .ToList();


            List<CustomerResponse> highlyUnusalCustomerItems = highlyUnusalCustomers.Select(
                    highlyUsalCustomers => new CustomerResponse
                    {
                        Id = highlyUsalCustomers.Id,
                        TotalStake = highlyUsalCustomers.TotalStake,
                        AvgerageBet = highlyUsalCustomers.AvgerageBet,
                        TypeofCustomer = "Highly UnSual Betting Rate"
                    })
                .ToList();
            unUsualCustomers.RemoveAll(x => highlyUnusalCustomerItems.Exists(y => y.Id == x.Id));
            highlyUnusalCustomerItems.AddRange(unUsualCustomers);
            return highlyUnusalCustomerItems;
        }
    }
}
