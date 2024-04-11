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



// Set the output path for Fashion Sense
string fashionSenseOutputPath = Path.Combine("resources", "fashion-sense");
#if DEBUG
fashionSenseOutputPath = "..\\..\\..\\..\\Site\\wwwroot\\resources\\fashion-sense";
#endif

// Cache the content mod data for Fashion Sense
await nexusMods.GetAndCacheContentPacks(9969, fashionSenseOutputPath);