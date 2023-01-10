namespace Better.Application.Dtos
{
    public class GameDto
    {
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
