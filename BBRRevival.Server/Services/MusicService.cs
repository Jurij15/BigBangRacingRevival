using BBRRevival.Server.Interfaces;

namespace BBRRevival.Server.Services
{
    public class MusicService : IMusicService
    {
        public async Task<byte[]> GetMusicFile(string name)
        {
            return await File.ReadAllBytesAsync($"Assets/Music/{name}.bank");
        }
    }
}
