namespace Better.Domain.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public Account Account { get; set; }
        public Guid AccountId { get; set; }
    }
}
