using System;
using System.Collections.Generic;
using System.Linq;


namespace BettingService
{
    public class UnSettledBettingItems
    {
        public int Customer { get; set; }

        public string Event { get; set; }

        public string Participant { get; set; }

        public int Stake { get; set; }

        public int ToWin { get; set; }
    }
}
