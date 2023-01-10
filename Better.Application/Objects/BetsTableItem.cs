namespace Better.Application.Objects
{
    public class BetsTableItem
    {
        public Guid MatchId { get; set; }
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }
}
