namespace Common.Models
{
    public class ContentPacksData
    {
        public DateTime Timestamp { get; set; }
        public List<ContentPack> ContentPacks { get; set; } = new List<ContentPack>();
    }
}
