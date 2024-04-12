using System;
using System.Collections;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

// Token: 0x02000565 RID: 1381
public static class FilePacker
{
	// Token: 0x06002859 RID: 10329 RVA: 0x001AD484 File Offset: 0x001AB884
	public static MemoryStream ZipStream(MemoryStream inputStream, int compressLevel = 3)
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

	// Token: 0x0600285A RID: 10330 RVA: 0x001AD504 File Offset: 0x001AB904
	public static byte[] ZipBytes(byte[] inputBytes, int compressLevel = 3)
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

	// Token: 0x0600285B RID: 10331 RVA: 0x001AD574 File Offset: 0x001AB974
	public static byte[] UnZipBytes(byte[] inputBytes)
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

	// Token: 0x0600285C RID: 10332 RVA: 0x001AD5D0 File Offset: 0x001AB9D0
	public static byte[] CombineByteArrays(byte[][] _arraysToCombine, Hashtable _header)
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

	// Token: 0x0600285D RID: 10333 RVA: 0x001AD67C File Offset: 0x001ABA7C
	public static byte[] AddByteArrayToExistingByteArray(byte[] _oldData, byte[] _new, Hashtable _header)
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

	// Token: 0x0600285E RID: 10334 RVA: 0x001AD712 File Offset: 0x001ABB12
	public static byte[] StringToByteArray(string str)
	{
		return Encoding.UTF8.GetBytes(str);
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x001AD71F File Offset: 0x001ABB1F
	public static string ByteArrayToString(byte[] bytes)
	{
		return Encoding.UTF8.GetString(bytes);
	}
}
