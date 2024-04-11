using System.Text.Json.Serialization;

namespace Internal.Models.NexusMods
{
    public class ModInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("mod_unique_downloads")]
        public int UniqueDownloads { get; set; }

        [JsonPropertyName("endorsement_count")]
        public int Endorsements { get; set; }

        [JsonPropertyName("contains_adult_content")]
        public bool HasAdultContent { get; set; }

        [JsonPropertyName("updated_time")]
        public DateTime LastUpdated { get; set; }

        [JsonPropertyName("created_time")]
        public DateTime CreatedTimestamp { get; set; }

        [JsonPropertyName("picture_url")]
        public string PictureUrl { get; set; }
    }
}
