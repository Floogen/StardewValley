using Internal.Web;
using Microsoft.Extensions.Configuration;


const string ALTERNATIVE_TEXTURES_MOD_UID = "5596342395934";
const string FASHION_SENSE_MOD_UID = "5596342396657";


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
var nexusMods = new NexusModsIntegration(nexusModsApiKey, "stardewvalley");

// Set the output path for Alternative Textures
string alternativeTexturesOutputPath = Path.Combine("resources", "alternative-textures");
#if DEBUG
alternativeTexturesOutputPath = "..\\..\\..\\..\\Site\\wwwroot\\resources\\alternative-textures";
#endif

// Cache the content mod data for Alternative Textures
await nexusMods.GetAndCacheContentPacks(ALTERNATIVE_TEXTURES_MOD_UID, alternativeTexturesOutputPath);


// Set the output path for Fashion Sense
string fashionSenseOutputPath = Path.Combine("resources", "fashion-sense");
#if DEBUG
fashionSenseOutputPath = "..\\..\\..\\..\\Site\\wwwroot\\resources\\fashion-sense";
#endif

// Cache the content mod data for Fashion Sense
await nexusMods.GetAndCacheContentPacks(FASHION_SENSE_MOD_UID, fashionSenseOutputPath);