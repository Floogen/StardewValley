using Internal.Web;
using Microsoft.Extensions.Configuration;


// Build the config
var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// Get the Nexus Mods API key
string[] commandLineArgs = Environment.GetCommandLineArgs();

string nexusModsApiKey = string.Empty;
if (commandLineArgs is not null && commandLineArgs.Length > 1)
{
    nexusModsApiKey = commandLineArgs[1];
}
#if DEBUG
nexusModsApiKey = config["nexusModsApiKey"];
#endif

// Establish the Nexus Mods link
var nexusMods = new NexusMods(nexusModsApiKey, "stardewvalley");

// Get the content packs associated to Fashion Sense
var fashionSenseContentPackIds = nexusMods.GetContentPacksFromMod(nexusMods.GetWebAddress(9969));

// Cache the data for the Fashion Sense content packs
#if DEBUG
await nexusMods.CacheContentPackData(fashionSenseContentPackIds.Take(10).ToList(), "..\\..\\..\\..\\Site\\wwwroot\\resources\\fashion-sense");
#else
await nexusMods.CacheContentPackData(fashionSenseContentPackIds.Take(10).ToList(), Path.Combine("resources", "fashion-sense"));
#endif