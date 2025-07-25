using System;
using System.Collections;
using System.IO;
using System.Text;
using BBRRevival.Server.Interfaces;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace BBRRevival.Server.Helpers
{
    public class FilePacker : IFilePacker
    {
        public MemoryStream ZipStream(MemoryStream inputStream, int compressLevel = 3)
        {
            inputStream.Position = 0L;
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write(inputStream.GetBuffer().Length);
            ZipOutputStream zipOutputStream = new ZipOutputStream(memoryStream);
            zipOutputStream.SetLevel(compressLevel);
            ZipEntry zipEntry = new ZipEntry("LevelData");
            zipOutputStream.PutNextEntry(zipEntry);
            StreamUtils.Copy(inputStream, zipOutputStream, new byte[4096]);
            zipOutputStream.CloseEntry();
            zipOutputStream.IsStreamOwner = false;
            zipOutputStream.Close();
            memoryStream.Position = 0L;
            return memoryStream;
        }

        public byte[] ZipBytes(byte[] inputBytes, int compressLevel = 3)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write(inputBytes.Length);
            ZipOutputStream zipOutputStream = new ZipOutputStream(memoryStream);
            zipOutputStream.SetLevel(compressLevel);
            ZipEntry zipEntry = new ZipEntry("LevelData");
            zipOutputStream.PutNextEntry(zipEntry);
            zipOutputStream.Write(inputBytes, 0, inputBytes.Length);
            zipOutputStream.CloseEntry();
            zipOutputStream.IsStreamOwner = false;
            zipOutputStream.Close();
            memoryStream.Position = 0L;
            return memoryStream.ToArray();
        }

        public byte[] UnZipBytes(byte[] inputBytes)
        {
            MemoryStream memoryStream = new MemoryStream(inputBytes, 0, inputBytes.Length, false, true);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            int num = binaryReader.ReadInt32();
            byte[] array = new byte[num];
            ZipInputStream zipInputStream = new ZipInputStream(memoryStream);
            zipInputStream.GetNextEntry();
            zipInputStream.Read(array, 0, num);
            zipInputStream.CloseEntry();
            zipInputStream.Close();
            return array;
        }

        public byte[] CombineByteArrays(byte[][] _arraysToCombine, Hashtable _header)
        {
            int num = 0;
            for (int i = 0; i < _arraysToCombine.Length; i++)
            {
                num += _arraysToCombine[i].Length;
            }
            byte[] array = new byte[num];
            string text = string.Empty;
            int num2 = 0;
            for (int j = 0; j < _arraysToCombine.Length; j++)
            {
                Buffer.BlockCopy(_arraysToCombine[j], 0, array, num2, _arraysToCombine[j].Length);
                num2 += _arraysToCombine[j].Length;
                text = text + _arraysToCombine[j].Length + ((j >= _arraysToCombine.Length - 1) ? string.Empty : ",");
            }
            _header.Add("FILE_SIZES", text);
            return array;
        }

        public byte[] AddByteArrayToExistingByteArray(byte[] _oldData, byte[] _new, Hashtable _header)
        {
            int num = _oldData.Length + _new.Length;
            byte[] array = new byte[num];
            string text = ((!_header.Contains("FILE_SIZES")) ? string.Empty : ((string)_header["FILE_SIZES"]));
            Array.Copy(_oldData, array, _oldData.Length);
            Buffer.BlockCopy(_new, 0, array, _oldData.Length, _new.Length);
            text = text + ((text.Length <= 0) ? string.Empty : ",") + _new.Length;
            _header["FILE_SIZES"] = text;
            return array;
        }

        public byte[] StringToByteArray(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public string ByteArrayToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public byte[][] UncombineByteArrays(byte[] combinedArray, string FileSizes)
        {
            if (FileSizes is null)
            {
                throw new ArgumentException("FileSizes is null");
            }

            string fileSizeString = FileSizes;
            string[] fileSizeStrings = fileSizeString.Split(',');
            int[] fileSizes = Array.ConvertAll(fileSizeStrings, int.Parse);

            byte[][] originalArrays = new byte[fileSizes.Length][];
            int offset = 0;

            for (int i = 0; i < fileSizes.Length; i++)
            {
                int length = fileSizes[i];
                originalArrays[i] = new byte[length];
                Buffer.BlockCopy(combinedArray, offset, originalArrays[i], 0, length);
                offset += length;
            }

            return originalArrays;
        }
    }
}
