namespace Better.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool HaveBets { get; set; }
        public float Balance { get; set; }
    }
}
