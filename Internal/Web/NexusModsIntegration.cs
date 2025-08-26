using Common.Models;
using HtmlAgilityPack;
using Internal.Models.NexusMods;
using Microsoft.Extensions.DependencyInjection;
using NexusMods.GraphQL;
using StrawberryShake;
using System.Text.Json;

namespace Internal.Web
{
    internal class NexusModsIntegration
    {
        private const string BASE_WEB_ADDRESS = "https://www.nexusmods.com";
        private const string BASE_GRAPHQL_API_ADDRESS = "https://api.nexusmods.com/v2/graphql";

        private const int STARDEW_VALLEY_GAME_ID = 1303;

        /// <summary>
        /// This seems to be arbitrarily limited to 80 on the Nexus Mods API. Using 50 in case it gets lowered further.
        /// </summary>
        private const int MAX_NODES_PER_REQUEST = 50;

        /// <summary>
        /// Not currently used but will likely need it again when the Nexus Mods API v2 is out of development.
        /// </summary>
        private string _apiKey;
        private string _gameName;

        private INexusClient _client;

        public NexusModsIntegration(string apiKey, string gameName)
        {
            _apiKey = apiKey;
            _gameName = gameName;

            // Establish GraphQL service
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddNexusClient()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(BASE_GRAPHQL_API_ADDRESS));

            IServiceProvider services = serviceCollection.BuildServiceProvider();

            // Create the GraphQL client
            _client = services.GetRequiredService<INexusClient>();
        }

        public string GetWebAddress(int modId)
        {
            return $"{BASE_WEB_ADDRESS}/{_gameName}/mods/{modId}";
        }

        public async Task GetAndCacheContentPacks(string frameworkId, string outputPath)
        {
            // Get the content pack IDs associated to the framework
            Console.WriteLine($"Grabbing content pack IDs associated with the framework ID {frameworkId}...");
            var contentPackIds = await ExtractContentPackIds(frameworkId);
            Console.WriteLine($"Framework ID {frameworkId} returned {contentPackIds.Count} content packs");


            // Get the content pack data associated to the framework
            Console.WriteLine($"Grabbing content pack data associated with the framework ID {frameworkId}...");
            var contentPackData = await ExtractContentPackData(contentPackIds, frameworkId);

            // Cache the data for the content packs
            Console.WriteLine($"Caching {contentPackData.Count} content packs associated with the framework ID {frameworkId}...");
            CacheContentPackData(contentPackData, outputPath, frameworkId);
        }

        private async Task<List<CompositeIdInput>> ExtractContentPackIds(string frameworkId, int offset = 0)
        {
            List<CompositeIdInput> modIds = new List<CompositeIdInput>();

            var results = await _client.GetFrameworkContentPacks.ExecuteAsync(frameworkId, MAX_NODES_PER_REQUEST, offset);
            results.EnsureNoErrors();

            // Verify data actually returned
            if (results.Data is null)
            {
                throw new Exception($"No data found for framework uID {frameworkId} with request offset of {offset}!");
            }

            // Cache the content packs ModId
            var frameworkData = results.Data.ModsByUid.Nodes.First();

            foreach (var contentPack in frameworkData.ModRequirements.ModsRequiringThisMod.Nodes)
            {
                modIds.Add(new CompositeIdInput() { GameId = STARDEW_VALLEY_GAME_ID , ModId = int.Parse(contentPack.ModId) });
            }

            // Check if we need to do a recursive grab due to max request constraints
            int totalContentPacks = frameworkData.ModRequirements.ModsRequiringThisMod.NodesCount;
            if (totalContentPacks > MAX_NODES_PER_REQUEST && offset < totalContentPacks - MAX_NODES_PER_REQUEST)
            {
                modIds.AddRange(await ExtractContentPackIds(frameworkId, offset + MAX_NODES_PER_REQUEST));
            }

            return modIds;
        }

        private async Task<List<ContentPack>> ExtractContentPackData(List<CompositeIdInput> modIds, string frameworkId, int offset = 0)
        {
            List<ContentPack> contentPacks = new List<ContentPack>();

            // Query the content packs
            var results = await _client.GetBulkModData.ExecuteAsync(modIds, MAX_NODES_PER_REQUEST, offset);
            results.EnsureNoErrors();

            // Verify data actually returned
            if (results.Data is null)
            {
                throw new Exception($"No data found when requesting individual content pack data for framework uID {frameworkId} with request offset of {offset}!");
            }

            // Cache the content packs ContentPack
            foreach (var contentPack in results.Data.LegacyMods.Nodes)
            {
                if (string.IsNullOrEmpty(contentPack.PictureUrl))
                {
                    Console.WriteLine($"The mod {GetWebAddress(contentPack.ModId)} has no thumbnail!");
                    continue;
                }

                contentPacks.Add(new ContentPack()
                {
                    Id = contentPack.ModId,
                    ModUrl = GetWebAddress(contentPack.ModId),
                    ImagePath = contentPack.PictureUrl,
                    ModName = contentPack.Name,
                    Author = contentPack.Author,
                    UniqueDownloads = contentPack.Downloads,
                    Endorsements = contentPack.Endorsements,
                    HasAdultContent = contentPack.Adult is true || contentPack.AdultContent is true,
                    LastUpdated = contentPack.UpdatedAt,
                    CreatedTimestamp = contentPack.CreatedAt
                });
            }

            // Check if we need to do a recursive grab due to max request constraints
            int totalContentPacks = modIds.Count;
            if (totalContentPacks > MAX_NODES_PER_REQUEST && offset < totalContentPacks - MAX_NODES_PER_REQUEST)
            {
                contentPacks.AddRange(await ExtractContentPackData(modIds, frameworkId, offset + MAX_NODES_PER_REQUEST));
            }

            return contentPacks;
        }

        public void CacheContentPackData(List<ContentPack> contentPacks, string targetFolder, string frameworkId, int offset = 0)
        {
            ContentPacksData contentPacksData = new ContentPacksData() { Timestamp = DateTime.UtcNow, ContentPacks = contentPacks };

            // Update the local data file so that the Site can determine the info to serve
            if (Path.Exists(targetFolder) is false)
            {
                Directory.CreateDirectory(targetFolder);
            }
            Console.WriteLine($"Saving cache to the following output path: {Path.Combine(Directory.GetCurrentDirectory(), targetFolder, "content-packs.json")}");

            if (contentPacksData.ContentPacks.Count >= 0)
            {
                File.WriteAllTextAsync(Path.Combine(targetFolder, "content-packs.json"), JsonSerializer.Serialize(contentPacksData, new JsonSerializerOptions() { WriteIndented = true }));

                Console.WriteLine($"Cached {contentPacksData.ContentPacks.Count} content packs!");
            }
            else
            {
                Console.WriteLine($"No content packs cached: List was empty!");
            }
        }
    }
}
