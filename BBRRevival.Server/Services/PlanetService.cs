using BBRRevival.Server.Interfaces;
using System.Numerics;
using System.Text;

namespace BBRRevival.Server.Services
{
    public class PlanetService : IPlanetService
    {
        public PlanetService() { }

        public async Task<string> GetPlanetByName(string planetName)
        {
            string data = null;

            string filePath = $"Assets\\InitialData\\{planetName}LocalInitialData.txt";

            try
            {
                data = await File.ReadAllTextAsync(filePath);
            }
            catch (Exception)
            {
                throw new ArgumentException("Planet not found");
            }

            if (data is null)
            {
                throw new ArgumentException("Planet not found");
            }

            return data;
        }

        public async Task<byte[]> GetPlanetBytes(string planetName)
        {
            return Encoding.UTF8.GetBytes(await GetPlanetByName(planetName));
        }
    }
}
