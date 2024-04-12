using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000543 RID: 1347
public static class LevelSerializer
{
	// Token: 0x0600279F RID: 10143 RVA: 0x001AA2BC File Offset: 0x001A86BC
	public static void SerializeLevelToFile(string _file, Level _levelData)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Stream stream = File.Open(_file, 2);
		if (stream != null)
		{
			binaryFormatter.Serialize(stream, _levelData);
			stream.Close();
		}
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x001AA2EB File Offset: 0x001A86EB
	public static byte[] SerializeLevelToBytes(Level _levelData)
	{
		return LevelSerializer.SerializeLevelToStream(_levelData).ToArray();
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x001AA2F8 File Offset: 0x001A86F8
	public static MemoryStream SerializeLevelToStream(Level _levelData)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, _levelData);
		return memoryStream;
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x001AA31C File Offset: 0x001A871C
	public static void SerializeGraph(string _file, GraphNode _graph)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Stream stream = File.Open(_file, 2);
		binaryFormatter.Serialize(stream, _graph);
		stream.Close();
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x001AA348 File Offset: 0x001A8748
	public static Level DeSerializeLevelFromFile(string _path)
	{
		Debug.Log("Deserializing level...", null);
		if (Main.FileExists(_path))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			Stream stream = File.Open(_path, 3);
			Level level = (Level)binaryFormatter.Deserialize(stream);
			stream.Close();
			return level;
		}
		Debug.LogWarning("File not found: " + _path);
		return null;
	}

	// Token: 0x060027A4 RID: 10148 RVA: 0x001AA3A4 File Offset: 0x001A87A4
	public static Level DeSerializeLevelFromBytes(byte[] _bytes)
	{
		Debug.Log("Deserializing level...", null);
		MemoryStream memoryStream = new MemoryStream(_bytes, 0, _bytes.Length, false, true);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Level level = (Level)binaryFormatter.Deserialize(memoryStream);
		memoryStream.Close();
		return level;
	}

	// Token: 0x060027A5 RID: 10149 RVA: 0x001AA3E8 File Offset: 0x001A87E8
	public static GraphNode DeSerializeGraph(string _path)
	{
		if (Main.FileExists(_path))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			Stream stream = File.Open(_path, 3);
			GraphNode graphNode = (GraphNode)binaryFormatter.Deserialize(stream);
			stream.Close();
			return graphNode;
		}
		Debug.LogWarning("File not found: " + _path);
		return null;
	}

	// Token: 0x060027A6 RID: 10150 RVA: 0x001AA434 File Offset: 0x001A8834
	public static Level DeSerializeUnityLevel(string _path)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		TextAsset textAsset = Resources.Load(_path) as TextAsset;
		if (textAsset != null)
		{
			Stream stream = new MemoryStream(textAsset.bytes);
			return (Level)binaryFormatter.Deserialize(stream);
		}
		Resources.UnloadAsset(textAsset);
		return null;
	}

	// Token: 0x060027A7 RID: 10151 RVA: 0x001AA484 File Offset: 0x001A8884
	public static GraphNode DeSerializeUnityGraph(string _path)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		TextAsset textAsset = Resources.Load(_path) as TextAsset;
		if (textAsset != null)
		{
			Stream stream = new MemoryStream(textAsset.bytes);
			return (GraphNode)binaryFormatter.Deserialize(stream);
		}
		Resources.UnloadAsset(textAsset);
		return null;
	}
}
