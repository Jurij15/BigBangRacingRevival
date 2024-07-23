using BBRRevival.Services.API.Models;
using BBRRevival.Services.Internal.Services.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Internal.Services
{
    public class MinigameService : IMinigameService
    {
        public Task GetMinigameLevelData(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<MinigameMetadataModel> GetMinigameMetadata(string id)
        {
            Log.Information($"Loading Metadata for {id}");

            MinigameMetadataModel model = null;

            if (Directory.Exists(Path.Combine(CommonPaths.MinigamesRootPath, id)))
            {
                if (File.Exists(Path.Combine(CommonPaths.MinigamesRootPath, id, "metadata")))
                {
                    model = JsonConvert.DeserializeObject<MinigameMetadataModel>(await File.ReadAllTextAsync(Path.Combine(CommonPaths.MinigamesRootPath, id, "metadata")));
                }
            }

            Log.Information($"Loaded Metadata for {id}");

            return model;
        }

        public async Task SaveMinigame(byte[] data, string fileSizes)
        {
            Log.Information("Saving Minigame");

            ///More info on arrays
            ///The first array is some minigame data in bytes (idk what atm)
            ///the second is the actuall level data
            ///the third is screenshot data, only one or maybe more?
            ///the forth one is some other json, probably minigame data
            ///
            ///Not all arrays have to be present, i believe only the first and the second one at least
            ///the third and the fourth one can be swapped
            byte[][] arrays = FilePacker.UncombineByteArrays(data, fileSizes);

            var minigameInfoData = Encoding.UTF8.GetString(arrays[0]);
            var minigameLevelData = arrays[1];

            var serializedMetadata = JsonConvert.DeserializeObject<MinigameMetadataModel>(minigameInfoData);

            serializedMetadata.id = Guid.NewGuid().ToString().Replace("-", "");

            var jsonData = JsonConvert.SerializeObject(serializedMetadata, Formatting.Indented); //just to pretty-print
            

            serializedMetadata.DebugPrint(); //remove later on

            //TODO: other data like screenshots and the other json
            if (arrays.Count() > 2)
            {
                var json = Encoding.UTF8.GetString(arrays[2]); //this might not be json but screenshot data, depends on chances
                Console.WriteLine(json);
            }

            if (!Directory.Exists(Path.Combine(CommonPaths.MinigamesRootPath, serializedMetadata.id)))
            {
                Directory.CreateDirectory(Path.Combine(CommonPaths.MinigamesRootPath, serializedMetadata.id));
            }

            await File.WriteAllTextAsync(Path.Combine(CommonPaths.MinigamesRootPath, serializedMetadata.id, "metadata"), jsonData);
            await File.WriteAllBytesAsync(Path.Combine(CommonPaths.MinigamesRootPath, serializedMetadata.id, "level") , minigameLevelData);

            Log.Information($"Saved Minigame with id {serializedMetadata.id}");
        }
    }
}
