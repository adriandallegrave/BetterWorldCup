using Better.Application.Objects;

namespace Better.Models
{
    public class BetsViewModel
    {
        public string UserMail { get; set; }
        public bool UserHaveBets { get; set; }
        public List<BetsTableItem> TableItems { get; set; }
        public List<BetSelection> Bets { get; set; }
        public string Error { get; set; }
    }
}
