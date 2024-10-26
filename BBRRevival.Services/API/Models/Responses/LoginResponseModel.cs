using Newtonsoft.Json;

namespace BBRRevival.Services.API.Models.Responses;

public class LoginResponseModel : JsonModel
{
    public PlayerData PlayerData { get; set; } = new();
    public ClientConfig ClientConfig { get; set; }

    public List<PlanetVersionModel> planetVersions { get; set; } = new();

    [JsonIgnore]
    public Tournament Tournament { get; set; }

    [JsonIgnore]
    public Event Event;

    public override string ToJson()
    {
        PlayerData.clientConfig = ClientConfig;

        return base.ToJson();
    }
}