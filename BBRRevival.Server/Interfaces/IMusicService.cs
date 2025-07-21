namespace BBRRevival.Server.Interfaces
{
    public interface IMusicService
    {
        public Task<byte[]> GetMusicFile(string name);
    }
}
