﻿using Common.Models;
using HtmlAgilityPack;
using Internal.Models.NexusMods;
using System.Diagnostics;
using System.Text.Json;

namespace Internal.Web
{
    internal class NexusMods
    {
        private const string BASE_WEB_ADDRESS = "https://www.nexusmods.com";
        private const string BASE_API_ADDRESS = "https://api.nexusmods.com/v1/games/";
        private const string CONTENT_PACKS_HEADER = "Mods requiring this file";

        private string _apiKey;
        private string _gameName;

        public NexusMods(string apiKey, string gameName)
        {
            _apiKey = apiKey;
            _gameName = gameName;
        }

        public string GetWebAddress(int modId)
        {
            return $"{BASE_WEB_ADDRESS}/{_gameName}/mods/{modId}";
        }

        public string GetApiAddress(int modId)
        {
            return $"{BASE_API_ADDRESS}/{_gameName}/mods/{modId}.json";
        }

        public List<int> GetContentPacksFromMod(string url)
        {
            HtmlDocument doc = new HtmlWeb().Load(url);
            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");

            // Iterate over the tables until CONTENT_PACKS_HEADER is found
            foreach (var table in tables)
            {
                HtmlNode? previousHeader = null;
                try
                {
                    previousHeader = table.PreviousSibling.PreviousSibling;
                }
                catch
                {
                    Debug.WriteLine("Failed to get previousHeader via PreviousSibling!");
                    continue;
                }

                if (previousHeader is not null && previousHeader.InnerText.Equals(CONTENT_PACKS_HEADER, StringComparison.OrdinalIgnoreCase))
                {
                    return ExtractContentPacksFromTable(table);
                }
            }

            return new List<int>();
        }

        public async Task CacheContentPackData(List<int> modIds, string targetFolder)
        {
            // Create a throwaway client
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("apiKey", _apiKey);
            client.DefaultRequestHeaders.Add("Application-Name", "Content Pack Check");
            client.DefaultRequestHeaders.Add("Application-Version", "1.0.0");
            client.DefaultRequestHeaders.Add("User-Agent", $"Content Pack Check/1.0.0");

            List<ContentPack> contentPacks = new List<ContentPack>();
            foreach (int modId in modIds)
            {
                var response = await client.GetAsync(new Uri(GetApiAddress(modId)));
                _ = response;
                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content is not null)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ModInfo? modInfo = JsonSerializer.Deserialize<ModInfo>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (modInfo is null)
                    {
                        Debug.WriteLine($"Failed to get ModInfo for {_gameName}/{modId}");
                        continue;
                    }
                    else if (string.IsNullOrEmpty(modInfo.PictureUrl))
                    {
                        Debug.WriteLine($"The mod {_gameName}/{modId} has no thumbnail!");
                        continue;
                    }

                    // Download the content pack data
                    if (Uri.TryCreate(modInfo.PictureUrl, new UriCreationOptions(), out var imageUrl))
                    {
                        //string imageName = $"{modId}{Path.GetExtension(imageUrl.Segments.Last())}";
                        //await DownloadImage(client, modInfo.PictureUrl, Path.Combine(targetFolder, imageName));

                        contentPacks.Add(new ContentPack()
                        {
                            Id = modId,
                            ModUrl = GetWebAddress(modId),
                            ImagePath = modInfo.PictureUrl,
                            ModName = modInfo.Name,
                            Author = modInfo.Author,
                            UniqueDownloads = modInfo.UniqueDownloads,
                            Endorsements = modInfo.Endorsements,
                            HasAdultContent = modInfo.HasAdultContent,
                            LastUpdated = modInfo.LastUpdated,
                            CreatedTimestamp = modInfo.CreatedTimestamp
                        });
                    }
                }
            }

            // Update the local data file so that the Site can determine the info to serve
            if (Path.Exists(targetFolder) is false)
            {
                Directory.CreateDirectory(targetFolder);
            }
            Console.WriteLine($"Saving to {Path.Combine(Directory.GetCurrentDirectory(), targetFolder, "content-packs.json")}");
            File.WriteAllText(Path.Combine(targetFolder, "content-packs.json"), JsonSerializer.Serialize(contentPacks, new JsonSerializerOptions() { WriteIndented = true }));

            client.Dispose();
        }

        private async Task DownloadImage(HttpClient client, string pictureUrl, string outputPath)
        {
            var imageBytes = await client.GetByteArrayAsync(pictureUrl);

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                using (var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
        }

        private List<int> ExtractContentPacksFromTable(HtmlNode table)
        {
            List<int> modIds = new List<int>();

            foreach (var modNode in table.Descendants("a").Where(n => n.Attributes.Contains("href")))
            {
                Uri uri = new Uri(modNode.Attributes["href"].Value);

                if (int.TryParse(uri.Segments.Last(), out int modId))
                {
                    modIds.Add(modId);
                }
            }

            return modIds;
        }
    }
}
