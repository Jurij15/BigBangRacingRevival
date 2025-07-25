namespace BBRRevival.Server.Interfaces
{
    public interface IPlanetService
    {
        public Task<string> GetPlanetByName(string planetName);
        public Task<byte[]> GetPlanetBytes(string planetName);
    }
}
