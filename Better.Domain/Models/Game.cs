namespace Better.Domain.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public int HomeScored { get; set; }
        public int AwayScored { get; set; }
        public DateTime StartTime { get; set; }
        public Guid HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }
        public Guid AwayTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }
    }
}
