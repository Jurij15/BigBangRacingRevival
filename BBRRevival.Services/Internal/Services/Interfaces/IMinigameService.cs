using BBRRevival.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Internal.Services.Interfaces
{
    internal interface IMinigameService
    {
        public Task SaveMinigame(byte[] data, string fileSizes);

        public Task<MinigameMetadataModel> GetMinigameMetadata(string id);
        public Task GetMinigameLevelData(string id);
    }
}
