namespace BettingModel.Models
{
  
    public class CustomerResponse
    {
        
        public int Id { get; set; }
        public int TotalStake { get; set; }
        public int Avg { get; set; }
        public double AvgerageBet { get; set; }
        public string TypeofCustomer { get; set; }
    }
}