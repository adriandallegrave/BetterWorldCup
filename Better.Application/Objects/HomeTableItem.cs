namespace Better.Application.Objects
{
    public class HomeTableItem
    {
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string Result { get; set; }
        public string AwayTeam { get; set; }
        public float AmountWon { get; set; }
        public float AmountLost { get; set; }
        public List<string> OrderedBets { get; set; }
    }
}
