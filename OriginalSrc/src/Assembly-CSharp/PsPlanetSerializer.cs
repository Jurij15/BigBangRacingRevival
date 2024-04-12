using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MiniJSON;

// Token: 0x02000130 RID: 304
public static class PsPlanetSerializer
{
	// Token: 0x06000945 RID: 2373 RVA: 0x00063E88 File Offset: 0x00062288
	public static void SaveProgressionLocally(PsPlanet _planet)
	{
		PsPlanetSerializer.SaveProgressionLocally(_planet.GetIdentifier());
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00063E98 File Offset: 0x00062298
	public static void SaveProgressionLocally(string _planetIdentifier)
	{
		if (PlanetTools.m_planetProgressionInfos.ContainsKey(_planetIdentifier))
		{
			PlanetProgressionInfo planetProgressionInfo = PlanetTools.m_planetProgressionInfos[_planetIdentifier];
			string text = "_" + planetProgressionInfo.m_planetIdentifier;
			Debug.Log("Saving PLANET: " + text, null);
			List<PsPlanetPath> list = new List<PsPlanetPath>();
			list.Add(planetProgressionInfo.m_floatingPath);
			string text2 = Json.Serialize(ClientTools.GenerateProgressionPathJson(planetProgressionInfo.m_mainPath.m_nodeInfos, planetProgressionInfo.m_mainPath.m_currentNodeId, planetProgressionInfo.m_planetIdentifier, true, true, true, list));
			string text3 = string.Concat(new string[]
			{
				PsPlanetSerializer.GetPathString(),
				"/",
				PlayerPrefsX.GetUserId(),
				text,
				".txt"
			});
			StreamWriter streamWriter = File.CreateText(text3);
			streamWriter.WriteLine(text2);
			streamWriter.Close();
			return;
		}
		Debug.LogError("ERROR: not found: " + _planetIdentifier);
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00063F84 File Offset: 0x00062384
	public static PsPlanetPath LoadProgression(string _planetIdentifier, PsPlanetPathType _planetPathType, bool _fullPath = false)
	{
		string text = "_" + _planetIdentifier;
		string text2 = string.Concat(new string[]
		{
			PsPlanetSerializer.GetPathString(),
			"/",
			PlayerPrefsX.GetUserId(),
			text,
			".txt"
		});
		if (_fullPath)
		{
			text2 = _planetIdentifier;
		}
		if (File.Exists(text2))
		{
			StreamReader streamReader = File.OpenText(text2);
			string text3 = string.Empty;
			for (string text4 = streamReader.ReadLine(); text4 != null; text4 = streamReader.ReadLine())
			{
				text3 += text4;
			}
			streamReader.Close();
			Dictionary<string, object> dictionary = Json.Deserialize(text3) as Dictionary<string, object>;
			List<PsPlanetPath> list = ClientTools.ParseProgressionPathData(dictionary["paths"] as List<object>, true);
			Debug.Log(string.Concat(new object[] { "Searching for: ", _planetPathType, " in: ", _planetIdentifier }), null);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetPathType() == _planetPathType)
				{
					Debug.LogWarning(_planetPathType.ToString() + "(" + _planetIdentifier + ") was found from local progression file.");
					return list[i];
				}
			}
			Debug.Log(string.Concat(new object[] { _planetPathType, " not found from local progression file.(", _planetIdentifier, ")" }), null);
			return null;
		}
		Debug.LogWarning("NO LOCAL PROGRESSION FILE FOUND FROM: " + text2);
		return null;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0006410C File Offset: 0x0006250C
	public static PsPlanetPath LoadProgression(PsPlanet _planet, PsPlanetPathType _planetPathType)
	{
		string identifier = _planet.GetIdentifier();
		return PsPlanetSerializer.LoadProgression(identifier, _planetPathType, false);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00064128 File Offset: 0x00062528
	public static List<PsPlanetPath> LoadProgressionsOfPlanet(string _planetIdentifier)
	{
		string text = "_" + _planetIdentifier;
		string text2 = string.Concat(new string[]
		{
			PsPlanetSerializer.GetPathString(),
			"/",
			PlayerPrefsX.GetUserId(),
			text,
			".txt"
		});
		if (File.Exists(text2))
		{
			StreamReader streamReader = File.OpenText(text2);
			string text3 = string.Empty;
			for (string text4 = streamReader.ReadLine(); text4 != null; text4 = streamReader.ReadLine())
			{
				text3 += text4;
			}
			streamReader.Close();
			Dictionary<string, object> dictionary = Json.Deserialize(text3) as Dictionary<string, object>;
			return ClientTools.ParseProgressionPathData(dictionary["paths"] as List<object>, true);
		}
		Debug.LogWarning("NO LOCAL PROGRESSION FILE FOUND FROM: " + text2);
		return null;
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000641F0 File Offset: 0x000625F0
	public static void SaveLocalPlanetInitialData(string _json, string _planetIdentifier)
	{
		string text = Main.GetPersistentDataPath() + "/" + _planetIdentifier + "LocalInitialData.txt";
		StreamWriter streamWriter = File.CreateText(text);
		streamWriter.WriteLine(_json);
		streamWriter.Close();
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00064228 File Offset: 0x00062628
	public static Dictionary<string, object> LoadLocalPlanetInitialData(string _planetIdentifier)
	{
		Debug.LogWarning("Loading local initial data of: " + _planetIdentifier);
		string text = Main.GetPersistentDataPath() + "/" + _planetIdentifier + "LocalInitialData.txt";
		if (!File.Exists(text))
		{
			Debug.LogWarning("NO LOCAL INITIAL PATH FILE: " + text);
			return null;
		}
		Debug.Log("LOADING LOCAL INITIAL PATH FILE: " + text, null);
		StreamReader streamReader = File.OpenText(text);
		string text2 = string.Empty;
		for (string text3 = streamReader.ReadLine(); text3 != null; text3 = streamReader.ReadLine())
		{
			text2 += text3;
		}
		streamReader.Close();
		if (string.IsNullOrEmpty(text2))
		{
			Debug.LogWarning("LOCAL INITIAL PATH FILE EXISTS, BUT WAS EMPTY: " + text);
			return null;
		}
		Debug.Log(string.Concat(new object[] { "DEVICES LOCAL PATH DATA (length:", text2.Length, "): ", text2 }), null);
		return Json.Deserialize(text2) as Dictionary<string, object>;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x0006431C File Offset: 0x0006271C
	public static string GetPathString()
	{
		string text = Main.GetPersistentDataPath() + "/ProgressionData";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0006434C File Offset: 0x0006274C
	public static List<string> GetLocalPlanets()
	{
		string pathString = PsPlanetSerializer.GetPathString();
		string userId = PlayerPrefsX.GetUserId();
		if (string.IsNullOrEmpty(userId))
		{
			return null;
		}
		List<string> list = new List<string>();
		DirectoryInfo directoryInfo = new DirectoryInfo(pathString);
		FileInfo[] files = directoryInfo.GetFiles(userId + "*.*");
		for (int i = 0; i < files.Length; i++)
		{
			string text = files[i].Name;
			if (string.IsNullOrEmpty(text) || !text.Contains(userId))
			{
				break;
			}
			text = text.Substring(text.LastIndexOf(userId) + userId.Length + 1);
			text = text.Substring(0, text.Length - 4);
			list.Add(text);
		}
		return list;
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0006440B File Offset: 0x0006280B
	public static bool FileExists(string _path)
	{
		return File.Exists(_path);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00064414 File Offset: 0x00062814
	public static void SerializePlanetPathToFile(string _file, PsPlanetPath _pathData)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Stream stream = File.Open(_file, 2);
		if (stream != null)
		{
			binaryFormatter.Serialize(stream, _pathData);
			stream.Close();
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00064444 File Offset: 0x00062844
	public static PsPlanetPath DeSerializeLevelFromFile(string _path)
	{
		Debug.Log("Deserializing path...", null);
		if (PsPlanetSerializer.FileExists(_path))
		{
			PsPlanetPath psPlanetPath = null;
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				Stream stream = File.Open(_path, 3);
				psPlanetPath = (PsPlanetPath)binaryFormatter.Deserialize(stream);
				stream.Close();
			}
			catch
			{
			}
			return psPlanetPath;
		}
		Debug.LogWarning("File not found: " + _path);
		return null;
	}
}
