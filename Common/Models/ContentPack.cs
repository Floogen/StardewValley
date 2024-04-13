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

        public string? GetModNameForTitle()
        {
            if (ModName is null)
            {
                return null;
            }

            return ModName.Length >= 70 ? $"{ModName.Substring(0, 70)}..." : ModName;
        }

        public string? GetAuthorForTitle()
        {
            if (Author is null)
            {
                return null;
            }

            return Author.Length >= 25 ? $"{Author.Substring(0, 25)}..." : Author;
        }
    }
}
