using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using BettingModel.Models;

namespace BettingService
{
    public class SettledCustomers : ISettledCustomers
    {
        private readonly string settledCSVFileLocation;
        private readonly string[] settledFileDetails;
        public List<SettledBettingDetails> settleditemList;

        internal SettledCustomers()
        {
            settledCSVFileLocation = ConfigurationSettings.AppSettings["CSVPath"] + "//Settled.csv";
            settledFileDetails = File.ReadAllLines(settledCSVFileLocation);
            settleditemList = GetSettledItems();
        }

        public IEnumerable<SettledCustomersPercentages> GetCustomersPercentageses()
        {
            return settleditemList.GroupBy(c => c.Customer)
                .Select(g => new SettledCustomersPercentages
                {
                    Id = Convert.ToInt32(g.Key),
                    TotalStake = g.Sum(xx => xx.Stake),
                    AvgWin = g.Average(xx => xx.Win),
                    AvgerageBet = g.Average(xx => xx.Stake),
                    WinningPercentage = 100 * g.Count(c => c.Win > 0) / (double) g.Count()
                });
        }

        public List<CustomerResponse> SettledCustomerDetails()
        {
            var settledCustomersPercentages = settleditemList.GroupBy(c => c.Customer)
                .Select(g => new
                {
                    Id = Convert.ToInt32(g.Key),
                    TotalStake = g.Sum(xx => xx.Stake),
                    AvgWin = g.Average(xx => xx.Win),
                    AvgerageBet = g.Average(xx => xx.Stake),
                    WinningPercentage = 100 * g.Count(c => c.Win > 0) / (double) g.Count()
                });


            var settledFilteredCustomersPercentages = settleditemList.GroupBy(c => c.Customer)
                .Select(g => new
                {
                    Id = Convert.ToInt32(g.Key),
                    TotalStake = g.Sum(xx => xx.Stake),
                    AvgWin = g.Average(xx => xx.Win),
                    AvgerageBet = g.Average(xx => xx.Stake),
                    WinningPercentage = 100 * g.Count(c => c.Win > 0) / (double) g.Count()
                })
                .Where(c => c.WinningPercentage >= 60);

            List<CustomerResponse> settledUnUsualCustomers = settledFilteredCustomersPercentages
                .Select(unUsalCustomers => new CustomerResponse
                {
                    Id = unUsalCustomers.Id,
                    TotalStake = unUsalCustomers.TotalStake,
                    AvgerageBet = unUsalCustomers.AvgerageBet,
                    TypeofCustomer = "UnUsual Winning Rate"
                })
                .ToList();


            List<CustomerResponse> settledUsualCustomers = settledCustomersPercentages
                .Select(usalCustomers => new CustomerResponse
                {
                    Id = usalCustomers.Id,
                    TotalStake = usalCustomers.TotalStake,
                    AvgerageBet = usalCustomers.AvgerageBet,
                    TypeofCustomer = "Usual Winning Rate"
                })
                .ToList();
            settledUsualCustomers.RemoveAll(x => settledUnUsualCustomers.Exists(y => y.Id == x.Id));
            settledUnUsualCustomers.AddRange(settledUsualCustomers);
            return settledUnUsualCustomers;
        }

        public List<SettledBettingDetails> GetSettledItems()
        {
            IEnumerable<string[]> settledData = settledFileDetails.Skip(1).Select(l => l.Split(',').ToArray());
            settleditemList = settledData.Select(values => new SettledBettingDetails
                {
                    Customer = Convert.ToInt32(values[0]),
                    Event = values[1],
                    Participant = values[2],
                    Stake = Convert.ToInt32(values[3]),
                    Win = Convert.ToInt32(values[4])
                })
                .ToList();

            return settleditemList;
        }
    }
}
