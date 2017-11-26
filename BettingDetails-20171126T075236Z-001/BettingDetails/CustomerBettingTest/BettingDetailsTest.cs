using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BettingModel.Models;
using BettingService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject1
{
    [TestClass]
    public class BettingDetailsTest
    {
        [TestMethod]
        public void CheckSettledBettingDetails()
        {
            CustomerRequest request = new CustomerRequest()
            {
                Type = "Settled"
            };
            IBetting _service = new BettingService.BettingService(request);
            var settled = _service.GetBettingDetails();           
            Assert.IsNotEmpty(settled.Select(x=>x.Id));
            Assert.IsNotEmpty(settled.Select(x => x.TotalStake));
            Assert.IsNotEmpty(settled.Select(x => x.TypeofCustomer));

        }
        [TestMethod]
        public void CheckUnSettledBettingDetails()
        {
            CustomerRequest request = new CustomerRequest()
            {
                Type = "UnSettled"
            };
            IBetting _service = new BettingService.BettingService(request);
            var settled = _service.GetBettingDetails();            
            Assert.IsNotEmpty(settled.Select(x => x.Id));
            Assert.IsNotEmpty(settled.Select(x => x.TotalStake));
            Assert.IsNotEmpty(settled.Select(x => x.TypeofCustomer));

        }


    }
}
