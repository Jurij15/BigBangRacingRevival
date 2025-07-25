using System.Collections;

namespace BBRRevival.Server.Interfaces
{
    public interface IFilePacker
    {
        public MemoryStream ZipStream(MemoryStream inputStream, int compressLevel = 3);
        public byte[] ZipBytes(byte[] inputBytes, int compressLevel = 3);
        public byte[] UnZipBytes(byte[] inputBytes);
        public byte[] CombineByteArrays(byte[][] _arraysToCombine, Hashtable _header);
        public byte[] AddByteArrayToExistingByteArray(byte[] _oldData, byte[] _new, Hashtable _header);
        public byte[] StringToByteArray(string str);
        public string ByteArrayToString(byte[] bytes);
        public byte[][] UncombineByteArrays(byte[] combinedArray, string FileSizes);
    }
}
