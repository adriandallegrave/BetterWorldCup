using Better.Application.Objects;

namespace Better.Models
{
    public class HomeViewModel
    {
        public List<HomeTableItem> TableItems { get; set; }
        public List<string> Users { get; set; }
        public string Version { get; set; }
    }
}
