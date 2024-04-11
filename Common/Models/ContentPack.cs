namespace Common.Models
{
    public class ContentPack
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? ModName { get; set; }
        public int UniqueDownloads { get; set; }
        public int Endorsements { get; set; }
        public bool HasAdultContent { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string? ImagePath { get; set; }
        public string? ModUrl { get; set; }
    }
}
