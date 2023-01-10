namespace Better.Domain.Models
{
    public class Bet
    {
        public Guid Id { get; set; }
        public int HomeGuess { get; set; }
        public int AwayGuess { get; set; }
        public Game Game { get; set; }
        public Guid GameId { get; set; }
        public Account Account { get; set; }
        public Guid AccountId { get; set; }
    }
}
