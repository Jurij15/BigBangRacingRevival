using Newtonsoft.Json;

namespace BBRRevival.Services.API.Models.Responses;

public class LoginResponseModel : DictionaryModel
{
    public PlayerData PlayerData { get; set; }

    [JsonIgnore]
    public ClientConfig ClientConfig { get; set; }

    public List<PlanetVersionModel> planetVersions { get; set; }

    public Tournament Tournament { get; set; }

    [JsonIgnore]
    public Event Event { get; set; }

    public override Dictionary<string, object> ToDictionary(bool recursive = true)
    {
        var dict = base.ToDictionary();

        return dict;
    }
}