using System;
using System.Collections;
using System.Collections.Generic;
using Server;

// Token: 0x0200042F RID: 1071
public static class PlanetTools
{
	// Token: 0x06001DAF RID: 7599 RVA: 0x00152580 File Offset: 0x00150980
	public static void PlayNextLevelOfPlanet(string _planetIdentifier, bool _skipCheckpoints = true)
	{
		if (!PlanetTools.m_planetProgressionInfos.ContainsKey(_planetIdentifier))
		{
			Debug.LogError("Invalid planetidentifier: " + _planetIdentifier);
			return;
		}
		PlanetProgressionInfo planetProgressionInfo = PlanetTools.m_planetProgressionInfos[_planetIdentifier];
		PlanetTools.CreateInitialLevelsIfNeeded(planetProgressionInfo);
		Debug.Log("Starting current level of: " + planetProgressionInfo.GetIdentifier(), null);
		PsGameLoop psGameLoop = planetProgressionInfo.GetMainPath().GetCurrentNodeInfo();
		if (psGameLoop.m_context != PsMinigameContext.Level)
		{
			psGameLoop.SaveProgression(true);
			if (psGameLoop.m_nodeId == psGameLoop.m_path.GetLastBlockId())
			{
			}
			psGameLoop = planetProgressionInfo.GetMainPath().GetCurrentNodeInfo();
		}
		psGameLoop.StartLoop();
	}

	// Token: 0x06001DB0 RID: 7600 RVA: 0x00152620 File Offset: 0x00150A20
	private static void FixBrokenPath(PlanetProgressionInfo _planet)
	{
		List<PsGameLoop> list = new List<PsGameLoop>();
		int num = 1;
		PsPlanetPath mainPath = _planet.GetMainPath();
		for (int i = 0; i < mainPath.m_nodeInfos.Count; i++)
		{
			if (mainPath.m_nodeInfos[i].m_nodeId != i + num)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Node id: ",
					i + num,
					", is MISSING!! (nodes id: ",
					mainPath.m_nodeInfos[i].m_nodeId,
					" ), Fixing path with new node"
				}));
				PsGameLoopBlockLevel psGameLoopBlockLevel = new PsGameLoopBlockLevel(PsMinigameContext.Block, string.Empty, PsPathBlockType.Chest, GachaType.SILVER, 0, "chest_2", string.Empty, mainPath, i + num, 0, 0, 0, 0, 0, false, string.Empty);
				list.Add(psGameLoopBlockLevel);
				mainPath.m_nodeInfos.Insert(i, psGameLoopBlockLevel);
			}
		}
		if (list.Count > 0)
		{
			Debug.LogError("Path was broken, saving fixed path to server");
			Hashtable hashtable = ClientTools.GenerateProgressionPathJson(list, mainPath.m_currentNodeId, _planet.m_planetIdentifier, true, true, true, null);
			PsMetagameManager.SaveProgression(hashtable, _planet.m_planetIdentifier, false);
		}
	}

	// Token: 0x06001DB1 RID: 7601 RVA: 0x00152738 File Offset: 0x00150B38
	public static void ChangePlanet(string _planetIdentifier)
	{
		PsPlanet currentPlanet = PsPlanetManager.GetCurrentPlanet();
		currentPlanet.ResetPlanet();
		PlanetProgressionInfo planetProgressionInfo = PlanetTools.m_planetProgressionInfos[_planetIdentifier];
		currentPlanet.SetPlanetData(planetProgressionInfo);
		currentPlanet.InitializePathData();
		foreach (string text in PlanetTools.m_planetProgressionInfos.Keys)
		{
			if (text != "Metadata")
			{
				PlanetTools.CreateInitialLevelsIfNeeded(PlanetTools.m_planetProgressionInfos[text]);
			}
		}
		PsFloaters.AddFloaters();
		PsMainMenuState.CreateEventNode();
		currentPlanet.InitializePlanetNodes(null);
		currentPlanet.Update();
		currentPlanet.CreateAndShowBanners();
		Debug.LogWarning("Planet changed.");
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x00152800 File Offset: 0x00150C00
	public static void CreateInitialLevelsIfNeeded(PlanetProgressionInfo _planet)
	{
		string identifier = _planet.GetIdentifier();
		PsUnlock nextUnlock = PsMetagameData.GetNextUnlock(identifier, false, null);
		Debug.LogWarning("Planet: " + identifier);
		Debug.LogWarning("INITIALIZING METAGAME, NEXT UNLOCK IS: " + nextUnlock.m_name);
		if (nextUnlock.m_name == "unlock_start")
		{
			List<PsGameLoop> list = PlanetTools.GeneratePersistentPathStrip(_planet, null);
			Hashtable hashtable = ClientTools.GenerateProgressionPathJson(list, 1, identifier, true, true, true, null);
			PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable, identifier, false);
		}
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x0015287C File Offset: 0x00150C7C
	public static void SetServerVersions(List<object> _list)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		for (int i = 0; i < _list.Count; i++)
		{
			Dictionary<string, object> dictionary2 = _list[i] as Dictionary<string, object>;
			if (dictionary2.ContainsKey("planet") && dictionary2.ContainsKey("version"))
			{
				dictionary.Add((string)dictionary2["planet"], Convert.ToInt32(dictionary2["version"]));
				Debug.LogWarning(string.Concat(new object[]
				{
					"SERVER INITIALDATA: ",
					(string)dictionary2["planet"],
					", version: ",
					Convert.ToInt32(dictionary2["version"])
				}));
			}
		}
		PlanetTools.m_serverInitialVersions = dictionary;
	}

	// Token: 0x06001DB4 RID: 7604 RVA: 0x0015294C File Offset: 0x00150D4C
	public static void LoadLocalPlanets()
	{
		Debug.LogWarning("Loading local planets (initial data)");
		PlanetTools.m_localPlanets = new Dictionary<string, Dictionary<string, object>>();
		Dictionary<string, object> localPlanetVersions = PlayerPrefsX.GetLocalPlanetVersions();
		if (localPlanetVersions == null || localPlanetVersions.Count < 1)
		{
			Debug.Log("No local planets (planetversions)", null);
			return;
		}
		foreach (KeyValuePair<string, object> keyValuePair in localPlanetVersions)
		{
			Dictionary<string, object> dictionary = PsPlanetSerializer.LoadLocalPlanetInitialData(keyValuePair.Key);
			if (dictionary != null)
			{
				dictionary["version"] = keyValuePair.Value;
				PlanetTools.m_localPlanets.Add(keyValuePair.Key, dictionary);
				Debug.LogWarning(string.Concat(new object[] { "Planet loaded: ", keyValuePair.Key, ", version: ", keyValuePair.Value }));
			}
		}
	}

	// Token: 0x06001DB5 RID: 7605 RVA: 0x00152A40 File Offset: 0x00150E40
	public static void LoadAll(Action _callback)
	{
		PlanetTools.planets = new List<string>();
		foreach (KeyValuePair<string, int> keyValuePair in PlanetTools.m_serverInitialVersions)
		{
			if (!(keyValuePair.Key == "Obsolete") && !(keyValuePair.Key == "Adventure"))
			{
				PlanetTools.planets.Add(keyValuePair.Key);
			}
		}
		PlanetTools.planetsUptoDate = 0;
		for (int i = 0; i < PlanetTools.planets.Count; i++)
		{
			string name = PlanetTools.planets[i];
			PlanetTools.LoadPlanet(PlanetTools.planets[i], delegate
			{
				PlanetTools.LoadAllCallback(_callback, name);
			}, false);
		}
	}

	// Token: 0x06001DB6 RID: 7606 RVA: 0x00152B50 File Offset: 0x00150F50
	public static void LoadAllCallback(Action _callBack, string _name)
	{
		PlanetTools.planetsUptoDate++;
		Debug.LogWarning(string.Concat(new object[]
		{
			"Loaded initialmetadata: ",
			PlanetTools.planetsUptoDate,
			" / ",
			PlanetTools.planets.Count,
			" ( ",
			_name,
			" )"
		}));
		if (PlanetTools.planets.Count == PlanetTools.planetsUptoDate)
		{
			_callBack.Invoke();
		}
	}

	// Token: 0x06001DB7 RID: 7607 RVA: 0x00152BD8 File Offset: 0x00150FD8
	public static void LoadPlanet(string _planetIdentifier, Action _callback, bool _preferLocal = false)
	{
		Debug.LogWarning("LoadPlanet");
		if (!PlanetTools.m_planetProgressionInfos.ContainsKey(_planetIdentifier))
		{
			PlanetProgressionInfo planetProgressionInfo = new PlanetProgressionInfo();
			planetProgressionInfo.m_mainPath = new PsPlanetPath("MainPath", _planetIdentifier, PsPlanetPathType.main);
			planetProgressionInfo.m_floatingPath = new PsPlanetPath("FloatingPath", _planetIdentifier, PsPlanetPathType.floating);
			planetProgressionInfo.m_floatingPath.m_lane = 1;
			planetProgressionInfo.m_planetIdentifier = _planetIdentifier;
			PlanetTools.m_planetProgressionInfos.Add(_planetIdentifier, planetProgressionInfo);
		}
		bool flag = true;
		bool flag2 = PlanetTools.m_localPlanets.ContainsKey(_planetIdentifier);
		int serverVersion = 0;
		if (PlanetTools.m_serverInitialVersions != null && PlanetTools.m_serverInitialVersions.ContainsKey(_planetIdentifier))
		{
			serverVersion = PlanetTools.m_serverInitialVersions[_planetIdentifier];
		}
		if (flag2)
		{
			int num = Convert.ToInt32(PlanetTools.m_localPlanets[_planetIdentifier]["version"]);
			Debug.LogWarning(string.Concat(new object[] { "Planet initial data: ", _planetIdentifier, "localVersion: ", num, ", serverversion: ", serverVersion }));
			if (num == serverVersion || _preferLocal)
			{
				ClientTools.ParseInitialGraphData(PlanetTools.m_localPlanets[_planetIdentifier], num, _planetIdentifier);
				flag = false;
				_callback.Invoke();
			}
		}
		else
		{
			Debug.LogWarning("No planet locally: " + _planetIdentifier);
		}
		if (flag)
		{
			Action<HttpC> action = delegate(HttpC c)
			{
				PlanetTools.PlanetLoadOk(c, _callback, serverVersion, _planetIdentifier);
			};
			Action<HttpC> action2 = delegate(HttpC c)
			{
				PlanetTools.PlanetLoadFailed(c, _callback, serverVersion, _planetIdentifier);
			};
			string planetIdentifier = _planetIdentifier;
			Path.GetInitial(action, action2, null, 0, planetIdentifier);
		}
	}

	// Token: 0x06001DB8 RID: 7608 RVA: 0x00152DC3 File Offset: 0x001511C3
	public static void PlanetLoadOk(HttpC _c, Action _callback, int _planetVersion, string _planetIdentifier)
	{
		ClientTools.ParseInitialGraphData(_c, _planetVersion, _planetIdentifier);
		_callback.Invoke();
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x00152DD4 File Offset: 0x001511D4
	public static void PlanetLoadFailed(HttpC _c, Action _callback, int _planetVersion, string _planetIdentifier)
	{
		Path.GetInitial(delegate(HttpC c)
		{
			PlanetTools.PlanetLoadOk(c, _callback, _planetVersion, _planetIdentifier);
		}, delegate(HttpC c)
		{
			PlanetTools.PlanetLoadFailed(c, _callback, _planetVersion, _planetIdentifier);
		}, null, 0, "Adventure");
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x00152E24 File Offset: 0x00151224
	public static void LoadPersistentPlanetData()
	{
		Debug.LogWarning("Loading persistent planet data");
		PlanetTools.m_planetProgressionInfos = new Dictionary<string, PlanetProgressionInfo>();
		List<string> localPlanets = PsPlanetSerializer.GetLocalPlanets();
		if (localPlanets == null || localPlanets.Count < 1)
		{
			return;
		}
		string text = string.Join(", ", localPlanets.ToArray());
		Debug.LogWarning("Loading local progressions, planetnames: " + text);
		bool flag = !localPlanets.Contains("RacingMotorcycle");
		for (int i = 0; i < localPlanets.Count; i++)
		{
			if (!(localPlanets[i] == "Planet") || flag)
			{
				PlanetProgressionInfo planetProgressionInfo = new PlanetProgressionInfo();
				List<PsPlanetPath> list;
				try
				{
					list = PsPlanetSerializer.LoadProgressionsOfPlanet(localPlanets[i]);
				}
				catch
				{
					list = null;
				}
				if (list != null && list.Count > 0)
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j].GetPathType() == PsPlanetPathType.main)
						{
							planetProgressionInfo.m_mainPath = list[j];
						}
						else if (list[j].GetPathType() == PsPlanetPathType.floating)
						{
							planetProgressionInfo.m_floatingPath = list[j];
						}
						else
						{
							Debug.LogError(string.Concat(new string[]
							{
								"Not suitable planet: ",
								localPlanets[i],
								": pathname: ",
								list[j].m_name,
								", type: ",
								list[j].GetPathType().ToString()
							}));
						}
					}
				}
				if (planetProgressionInfo.m_mainPath != null)
				{
					string text2 = localPlanets[i];
					if (text2 == "Planet")
					{
						text2 = "RacingMotorcycle";
					}
					Debug.LogWarning("Adding " + text2 + " to planetProgressionInfos");
					planetProgressionInfo.m_planetIdentifier = text2;
					PlanetTools.m_planetProgressionInfos.Add(text2, planetProgressionInfo);
				}
			}
		}
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x00153038 File Offset: 0x00151438
	public static void UpdateLocalPlanetPaths(List<PsPlanetPath> _serverPaths)
	{
		string text = string.Empty;
		for (int i = 0; i < _serverPaths.Count; i++)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				_serverPaths[i].m_planet,
				"(",
				_serverPaths[i].GetPathType(),
				"), "
			});
		}
		Debug.LogWarning(string.Concat(new object[] { "Names of paths from server (", _serverPaths.Count, "): ", text }));
		for (int j = 0; j < _serverPaths.Count; j++)
		{
			if (!PlanetTools.m_planetProgressionInfos.ContainsKey(_serverPaths[j].m_planet))
			{
				PlanetProgressionInfo planetProgressionInfo = new PlanetProgressionInfo();
				planetProgressionInfo.m_mainPath = new PsPlanetPath("MainPath", _serverPaths[j].m_planet, PsPlanetPathType.main);
				planetProgressionInfo.m_floatingPath = new PsPlanetPath("FloatingPath", _serverPaths[j].m_planet, PsPlanetPathType.floating);
				planetProgressionInfo.m_planetIdentifier = _serverPaths[j].m_planet;
				PlanetTools.m_planetProgressionInfos[_serverPaths[j].m_planet] = planetProgressionInfo;
			}
		}
		foreach (KeyValuePair<string, PlanetProgressionInfo> keyValuePair in PlanetTools.m_planetProgressionInfos)
		{
			PsPlanetPath mainPath = keyValuePair.Value.m_mainPath;
			PsPlanetPath psPlanetPath = null;
			PsPlanetPath psPlanetPath2 = null;
			List<PsPlanetPath> list = new List<PsPlanetPath>();
			for (int k = 0; k < _serverPaths.Count; k++)
			{
				if (_serverPaths[k].m_planet == keyValuePair.Key)
				{
					if (_serverPaths[k].GetPathType() == PsPlanetPathType.main)
					{
						psPlanetPath = _serverPaths[k];
					}
					else if (_serverPaths[k].GetPathType() == PsPlanetPathType.floating)
					{
						psPlanetPath2 = _serverPaths[k];
					}
					else if (_serverPaths[k].GetPathType() == PsPlanetPathType.side)
					{
						list.Add(_serverPaths[k]);
					}
				}
			}
			bool flag = false;
			if (psPlanetPath != null && psPlanetPath.m_nodeInfos.Count > 0)
			{
				flag = true;
				Debug.Log("UPDATE PERSISTENT PATH: " + mainPath.m_planet, null);
				Debug.Log("CURRENT NODE ID: " + mainPath.m_currentNodeId, null);
				Debug.Log("CURRENT NODE COUNT: " + mainPath.m_nodeInfos.Count, null);
				List<PsGameLoop> nodeInfos = psPlanetPath.m_nodeInfos;
				List<PsGameLoop> nodeInfos2 = mainPath.m_nodeInfos;
				nodeInfos.Sort((PsGameLoop s1, PsGameLoop s2) => s1.m_nodeId.CompareTo(s2.m_nodeId));
				for (int l = 0; l < nodeInfos.Count; l++)
				{
					int num = PlanetTools.ContainsNodeId(nodeInfos2, nodeInfos[l].m_nodeId);
					if (num >= 0)
					{
						Debug.Log("UPDATE NODE ID: " + nodeInfos[l].m_nodeId, null);
						PsGameLoop psGameLoop = nodeInfos2[num];
						if (psGameLoop != PsState.m_activeGameLoop)
						{
							nodeInfos[l].m_path = psGameLoop.m_path;
							nodeInfos2[num] = nodeInfos[l];
						}
					}
					else if (nodeInfos[l].m_nodeId <= nodeInfos2.Count)
					{
						Debug.Log("INSERT NODE ID: " + nodeInfos[l].m_nodeId, null);
						nodeInfos[l].m_path = mainPath;
						nodeInfos2.Insert(nodeInfos[l].m_nodeId - 1, nodeInfos[l]);
					}
					else
					{
						Debug.Log("ADD NODE ID: " + nodeInfos[l].m_nodeId, null);
						nodeInfos[l].m_path = mainPath;
						nodeInfos2.Add(nodeInfos[l]);
					}
				}
				mainPath.m_currentNodeId = psPlanetPath.m_currentNodeId;
			}
			if (psPlanetPath2 != null && psPlanetPath2.m_nodeInfos.Count > 0)
			{
				bool flag2 = true;
				if (PsPlanetManager.GetCurrentPlanet() != null && PsPlanetManager.GetCurrentPlanet().m_floatingNodeList != null && PsPlanetManager.GetCurrentPlanet().m_floatingNodeList.Count > 0 && PsPlanetManager.GetCurrentPlanet().GetPlanetInfo() != null && PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().m_planetIdentifier == psPlanetPath2.m_planet)
				{
					flag2 = false;
				}
				if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_path == keyValuePair.Value.m_floatingPath)
				{
					flag2 = false;
				}
				if (flag2)
				{
					keyValuePair.Value.m_floatingPath = psPlanetPath2;
				}
			}
			for (int m = 0; m < list.Count; m++)
			{
				flag = true;
				List<PsGameLoop> nodeInfos3 = list[m].m_nodeInfos;
				nodeInfos3.Sort((PsGameLoop s1, PsGameLoop s2) => s1.m_nodeId.CompareTo(s2.m_nodeId));
				PsGameLoop nodeInfo = mainPath.GetNodeInfo(list[m].m_startNodeId);
				PsPlanetPath psPlanetPath3 = nodeInfo.m_sidePath;
				if (psPlanetPath3 != null)
				{
					if (psPlanetPath3.m_nodeInfos != null)
					{
						psPlanetPath3.m_nodeInfos = new List<PsGameLoop>(nodeInfo.m_sidePath.m_nodeInfos);
					}
					else
					{
						psPlanetPath3.m_nodeInfos = new List<PsGameLoop>();
					}
				}
				if (psPlanetPath3 != null && psPlanetPath3 != list[m])
				{
					for (int n = 0; n < nodeInfos3.Count; n++)
					{
						if (nodeInfos3[n].m_nodeId <= psPlanetPath3.m_nodeInfos.Count)
						{
							int num2 = nodeInfos3[n].m_nodeId - 1;
							Debug.Log("UPDATE SIDE NODE ID: " + nodeInfos3[n].m_nodeId, null);
							psPlanetPath3.m_nodeInfos[num2] = nodeInfos3[n];
							psPlanetPath3.m_nodeInfos[num2].m_path = psPlanetPath3;
						}
						else
						{
							Debug.Log("ADD SIDE NODE ID: " + nodeInfos3[n].m_nodeId, null);
							psPlanetPath3.m_nodeInfos.Add(nodeInfos3[n]);
							psPlanetPath3.m_nodeInfos[nodeInfos3[n].m_nodeId - 1].m_path = psPlanetPath3;
						}
					}
				}
				else
				{
					psPlanetPath3 = list[m];
				}
				psPlanetPath3.m_currentNodeId = list[m].m_currentNodeId;
				nodeInfo.m_sidePath = psPlanetPath3;
				psPlanetPath3.m_parentPath = nodeInfo.m_path;
			}
			if (PsPlanetManager.GetCurrentPlanet() != null && PsPlanetManager.GetCurrentPlanet().GetIdentifier() != null && PsPlanetManager.GetCurrentPlanet().GetIdentifier() == keyValuePair.Key && Main.m_currentGame.m_currentScene.m_name == "MenuScene" && Main.m_currentGame.m_currentScene.m_initComplete)
			{
				PsPlanetManager.GetCurrentPlanet().ClearRows();
				PsPlanetManager.GetCurrentPlanet().Update();
			}
			PlanetTools.FixBrokenPath(keyValuePair.Value);
			if (flag)
			{
				PsPlanetSerializer.SaveProgressionLocally(keyValuePair.Key);
			}
		}
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x001537F4 File Offset: 0x00151BF4
	private static int ContainsNodeId(List<PsGameLoop> _list, int _nodeId)
	{
		for (int i = _list.Count - 1; i >= 0; i--)
		{
			if (_list[i].m_nodeId == _nodeId)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x00153830 File Offset: 0x00151C30
	public static void OpenAllUnlocks()
	{
		foreach (KeyValuePair<string, PlanetProgressionInfo> keyValuePair in PlanetTools.m_planetProgressionInfos)
		{
			PlanetTools.OpenUnlocks(keyValuePair.Value);
		}
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x00153890 File Offset: 0x00151C90
	public static void OpenUnlocks(PlanetProgressionInfo _planet)
	{
		Debug.LogWarning("UNLOCKING: " + _planet.GetIdentifier());
		Debug.LogWarning("node count :" + _planet.m_mainPath.m_nodeInfos.Count);
		Debug.LogWarning("current node id :" + _planet.m_mainPath.m_currentNodeId);
		int num = _planet.m_mainPath.GetCurrentBlockId();
		if (num == -1)
		{
			num = _planet.m_mainPath.m_nodeInfos.Count - 1;
		}
		Debug.Log(_planet.GetIdentifier() + ": check until id :" + num, null);
		if (PsState.m_activeGameLoop != null && !string.IsNullOrEmpty(PsState.m_activeGameLoop.m_temporaryUnlock))
		{
			PsMetagameData.TemporarilyUnlockUnlockable(PsState.m_activeGameLoop.m_temporaryUnlock);
		}
		for (int i = 0; i < _planet.m_mainPath.m_nodeInfos.Count; i++)
		{
			if (num < _planet.m_mainPath.m_nodeInfos[i].m_nodeId)
			{
				return;
			}
			string unlockedProgressionName = _planet.m_mainPath.m_nodeInfos[i].GetUnlockedProgressionName();
			if (unlockedProgressionName != string.Empty)
			{
				PsMetagameData.Unlock(unlockedProgressionName, _planet.GetIdentifier());
			}
		}
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x001539D8 File Offset: 0x00151DD8
	public static List<PsGameLoop> GeneratePersistentPathStrip(PlanetProgressionInfo _planet, string _lastIdentifier = null)
	{
		List<PsGameLoop> list = new List<PsGameLoop>();
		List<PsUnlock> list2 = new List<PsUnlock>();
		int num = 0;
		PsUnlock psUnlock;
		if (num == 0)
		{
			psUnlock = PsMetagameData.GetNextUnlock(_planet.GetIdentifier(), true, _lastIdentifier);
		}
		else
		{
			psUnlock = PsMetagameData.GetNextUnlockFromIdentifier(_lastIdentifier, _planet.GetIdentifier());
		}
		if (psUnlock == null)
		{
			Debug.LogError("WHAT!?");
			return list;
		}
		string name = psUnlock.m_name;
		list2.Add(psUnlock);
		num += psUnlock.m_levels.Count;
		int num2 = 0;
		int num3 = 0;
		if (_planet.GetMainPath().m_nodeInfos.Count > 0)
		{
			num2 = _planet.GetMainPath().GetLastBlockId();
			PsGameLoop nodeInfo = _planet.GetMainPath().GetNodeInfo(_planet.GetMainPath().GetLastLevelId());
			if (nodeInfo != null)
			{
				num3 = nodeInfo.m_levelNumber;
			}
		}
		Debug.LogWarning("START ID: " + num2);
		Debug.LogWarning("START LEVEL NUMBER: " + num2);
		for (int i = 0; i < list2.Count; i++)
		{
			PsUnlock psUnlock2 = list2[i];
			Debug.LogWarning("LEVEL COUNT: " + psUnlock2.m_levels.Count);
			for (int j = 0; j < psUnlock2.m_levels.Count; j++)
			{
				MetagameNodeData metagameNodeData = psUnlock2.m_levels[j];
				num2++;
				PsGameLoop psGameLoop = PsPlanet.CreateLoop(_planet.GetMainPath(), metagameNodeData, num2, ref num3, psUnlock2.m_name);
				if (metagameNodeData.m_sidePathLevels != null)
				{
					List<PsGameLoop> row = PlanetTools.GetRow(num2 + 1, _planet);
					bool flag = false;
					bool flag2 = false;
					for (int k = 0; k < row.Count; k++)
					{
						if (row[k].m_path.m_lane == -1)
						{
							flag = true;
						}
						else if (row[k].m_path.m_lane == 1)
						{
							flag2 = true;
						}
					}
					if (!flag || !flag2)
					{
						PsPlanetPath psPlanetPath = new PsPlanetPath(num2 + "_SidePath", _planet.GetIdentifier(), PsPlanetPathType.side);
						float xposition = PsPlanetNode.GetXPosition(psGameLoop, 1);
						float xposition2 = PsPlanetNode.GetXPosition(psGameLoop, 2);
						if (xposition2 < 0f)
						{
							psPlanetPath.m_lane = 1;
						}
						else
						{
							psPlanetPath.m_lane = -1;
						}
						if (psPlanetPath.m_lane == -1 && flag)
						{
							psPlanetPath.m_lane = 1;
						}
						if (psPlanetPath.m_lane == 1 && flag2)
						{
							psPlanetPath.m_lane = -1;
						}
						psPlanetPath.m_startNodeId = psGameLoop.m_nodeId;
						psPlanetPath.m_parentPath = psGameLoop.m_path;
						psPlanetPath.m_currentNodeId = 0;
						psGameLoop.m_sidePath = psPlanetPath;
						int num4 = 0;
						int num5 = 0;
						for (int l = 0; l < metagameNodeData.m_sidePathLevels.Count; l++)
						{
							MetagameNodeData metagameNodeData2 = metagameNodeData.m_sidePathLevels[l];
							num4++;
							PsGameLoop psGameLoop2 = PsPlanet.CreateLoop(psPlanetPath, metagameNodeData2, num4, ref num5, string.Empty);
							psPlanetPath.m_nodeInfos.Add(psGameLoop2);
							if (metagameNodeData2.m_dialogues != null)
							{
								psGameLoop2.m_dialogues = new Hashtable();
								for (int m = 0; m < metagameNodeData2.m_dialogues.Count; m++)
								{
									string text = metagameNodeData2.m_dialogues[m].m_trigger.ToString();
									if (!psGameLoop2.m_dialogues.Contains(text))
									{
										psGameLoop2.m_dialogues.Add(text, metagameNodeData2.m_dialogues[m].m_identifier);
									}
									else
									{
										Debug.LogError("DUPLICATE DIALOGUE IDENTIFIER " + text + " AT " + psUnlock2.m_name);
									}
								}
							}
						}
					}
					else
					{
						Debug.LogError("Tried to add sidepath, but all lanes were already in use: " + num2);
					}
				}
				if (metagameNodeData.m_dialogues != null)
				{
					psGameLoop.m_dialogues = new Hashtable();
					for (int n = 0; n < metagameNodeData.m_dialogues.Count; n++)
					{
						string text2 = metagameNodeData.m_dialogues[n].m_trigger.ToString();
						if (!psGameLoop.m_dialogues.Contains(text2))
						{
							psGameLoop.m_dialogues.Add(text2, metagameNodeData.m_dialogues[n].m_identifier);
						}
						else
						{
							Debug.LogError("DUPLICATE DIALOGUE IDENTIFIER " + text2 + " AT " + psUnlock2.m_name);
						}
					}
				}
				list.Add(psGameLoop);
				_planet.GetMainPath().m_nodeInfos.Add(psGameLoop);
				string unlockedProgressionName = psGameLoop.GetUnlockedProgressionName();
				if (unlockedProgressionName != string.Empty)
				{
					psGameLoop.SetUnlockedProgressionName(psUnlock2.m_name);
				}
			}
		}
		return list;
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x00153EC4 File Offset: 0x001522C4
	public static List<PsGameLoop> GetRow(int _rowId, PlanetProgressionInfo _planet)
	{
		List<PsGameLoop> list = new List<PsGameLoop>();
		PsGameLoop nodeInfo = _planet.GetMainPath().GetNodeInfo(_rowId);
		if (nodeInfo != null)
		{
			list.Add(nodeInfo);
		}
		int num = 0;
		int num2 = _planet.GetMainPath().GetPrevBlockId(_rowId) - 5;
		for (int i = _rowId - 1; i > num2 - 1; i--)
		{
			num++;
			PsGameLoop nodeInfo2 = _planet.GetMainPath().GetNodeInfo(i);
			if (nodeInfo2 != null && nodeInfo2.m_sidePath != null)
			{
				PsGameLoop nodeInfo3 = nodeInfo2.m_sidePath.GetNodeInfo(num);
				if (nodeInfo3 != null)
				{
					list.Add(nodeInfo3);
				}
			}
		}
		return list;
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x00153F68 File Offset: 0x00152368
	public static List<PsGameLoop> GiveNextStrip(PsGameLoop _loop)
	{
		string unlockedProgressionName = _loop.GetUnlockedProgressionName();
		List<PsGameLoop> list = PlanetTools.GeneratePersistentPathStrip(_loop.m_path.GetPlanetInfo(), unlockedProgressionName);
		for (int i = 0; i < list.Count; i++)
		{
			list[i].m_keepNodeHidden = true;
			if (list[i].m_sidePath != null)
			{
				List<PsGameLoop> nodeInfos = list[i].m_sidePath.m_nodeInfos;
				for (int j = 0; j < nodeInfos.Count; j++)
				{
					nodeInfos[j].m_keepNodeHidden = true;
				}
			}
		}
		return list;
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x00154000 File Offset: 0x00152400
	public static PsGameLoop FixWithBlock(PsGameLoop _loop)
	{
		PlanetProgressionInfo planetInfo = _loop.m_path.GetPlanetInfo();
		PsUnlock currentUnlock = PsMetagameData.GetCurrentUnlock(planetInfo.m_planetIdentifier);
		PsGameLoop psGameLoop = null;
		int levelNumber = _loop.m_levelNumber;
		for (int i = currentUnlock.m_levels.Count - 1; i >= 0; i--)
		{
			MetagameNodeData metagameNodeData = currentUnlock.m_levels[i];
			if (metagameNodeData.m_blockType == PsPathBlockType.Boss || metagameNodeData.m_blockType == PsPathBlockType.Chest || metagameNodeData.m_blockType == PsPathBlockType.Antenna)
			{
				psGameLoop = PsPlanet.CreateLoop(planetInfo.GetMainPath(), metagameNodeData, _loop.m_nodeId + 1, ref levelNumber, currentUnlock.m_name);
				if (metagameNodeData.m_sidePathLevels != null)
				{
					List<PsGameLoop> row = PlanetTools.GetRow(_loop.m_nodeId + 2, planetInfo);
					bool flag = false;
					bool flag2 = false;
					for (int j = 0; j < row.Count; j++)
					{
						if (row[j].m_path.m_lane == -1)
						{
							flag = true;
						}
						else if (row[j].m_path.m_lane == 1)
						{
							flag2 = true;
						}
					}
					if (!flag || !flag2)
					{
						PsPlanetPath psPlanetPath = new PsPlanetPath(_loop.m_nodeId + 1 + "_SidePath", planetInfo.GetIdentifier(), PsPlanetPathType.side);
						float xposition = PsPlanetNode.GetXPosition(psGameLoop, 1);
						float xposition2 = PsPlanetNode.GetXPosition(psGameLoop, 2);
						if (xposition2 < 0f)
						{
							psPlanetPath.m_lane = 1;
						}
						else
						{
							psPlanetPath.m_lane = -1;
						}
						if (psPlanetPath.m_lane == -1 && flag)
						{
							psPlanetPath.m_lane = 1;
						}
						if (psPlanetPath.m_lane == 1 && flag2)
						{
							psPlanetPath.m_lane = -1;
						}
						psPlanetPath.m_startNodeId = psGameLoop.m_nodeId;
						psPlanetPath.m_parentPath = psGameLoop.m_path;
						psPlanetPath.m_currentNodeId = 0;
						psGameLoop.m_sidePath = psPlanetPath;
						int num = 0;
						int num2 = 0;
						for (int k = 0; k < metagameNodeData.m_sidePathLevels.Count; k++)
						{
							MetagameNodeData metagameNodeData2 = metagameNodeData.m_sidePathLevels[k];
							num++;
							PsGameLoop psGameLoop2 = PsPlanet.CreateLoop(psPlanetPath, metagameNodeData2, num, ref num2, string.Empty);
							psPlanetPath.m_nodeInfos.Add(psGameLoop2);
							if (metagameNodeData2.m_dialogues != null)
							{
								psGameLoop2.m_dialogues = new Hashtable();
								for (int l = 0; l < metagameNodeData2.m_dialogues.Count; l++)
								{
									string text = metagameNodeData2.m_dialogues[l].m_trigger.ToString();
									if (!psGameLoop2.m_dialogues.Contains(text))
									{
										psGameLoop2.m_dialogues.Add(text, metagameNodeData2.m_dialogues[l].m_identifier);
									}
									else
									{
										Debug.LogError("DUPLICATE DIALOGUE IDENTIFIER " + text + " AT " + currentUnlock.m_name);
									}
								}
							}
						}
					}
					else
					{
						Debug.LogError("Tried to add sidepath, but all lanes were already in use: " + (_loop.m_nodeId + 1));
					}
				}
				if (metagameNodeData.m_dialogues != null)
				{
					psGameLoop.m_dialogues = new Hashtable();
					for (int m = 0; m < metagameNodeData.m_dialogues.Count; m++)
					{
						string text2 = metagameNodeData.m_dialogues[m].m_trigger.ToString();
						if (!psGameLoop.m_dialogues.Contains(text2))
						{
							psGameLoop.m_dialogues.Add(text2, metagameNodeData.m_dialogues[m].m_identifier);
						}
						else
						{
							Debug.LogError("DUPLICATE DIALOGUE IDENTIFIER " + text2 + " AT " + currentUnlock.m_name);
						}
					}
				}
				planetInfo.GetMainPath().m_nodeInfos.Add(psGameLoop);
				string unlockedProgressionName = psGameLoop.GetUnlockedProgressionName();
				if (unlockedProgressionName != string.Empty)
				{
					psGameLoop.SetUnlockedProgressionName(currentUnlock.m_name);
				}
				break;
			}
		}
		return psGameLoop;
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x001543FC File Offset: 0x001527FC
	public static string GetVehiclePlanetIdentifier()
	{
		int vehicleIndex = PsState.GetVehicleIndex();
		if (vehicleIndex == 0)
		{
			return "AdventureOffroadCar";
		}
		if (vehicleIndex != 1)
		{
			Debug.LogError("Wrong index");
			return "AdventureOffroadCar";
		}
		return "AdventureMotorcycle";
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x0015443C File Offset: 0x0015283C
	public static string GetVehicleRacingPlanetIdentifier()
	{
		int vehicleIndex = PsState.GetVehicleIndex();
		if (vehicleIndex == 0)
		{
			return "RacingOffroadCar";
		}
		if (vehicleIndex != 1)
		{
			Debug.LogError("Wrong index");
			return "RacingOffroadCar";
		}
		return "RacingMotorcycle";
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x0015447C File Offset: 0x0015287C
	public static PlanetProgressionInfo GetCurrentRacingPlanetInfo()
	{
		string vehicleRacingPlanetIdentifier = PlanetTools.GetVehicleRacingPlanetIdentifier();
		if (!PlanetTools.m_planetProgressionInfos.ContainsKey(vehicleRacingPlanetIdentifier))
		{
			Debug.LogError("No planet on identifier: " + vehicleRacingPlanetIdentifier);
			return null;
		}
		return PlanetTools.m_planetProgressionInfos[vehicleRacingPlanetIdentifier];
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x001544BC File Offset: 0x001528BC
	public static void ResetLocalData()
	{
		PlanetTools.m_planetProgressionInfos.Clear();
		Main.DeleteLocalDataFiles();
	}

	// Token: 0x04002085 RID: 8325
	public static Dictionary<string, PlanetProgressionInfo> m_planetProgressionInfos;

	// Token: 0x04002086 RID: 8326
	public static Dictionary<string, Dictionary<string, object>> m_localPlanets;

	// Token: 0x04002087 RID: 8327
	public static Dictionary<string, int> m_serverInitialVersions;

	// Token: 0x04002088 RID: 8328
	public static List<string> planets;

	// Token: 0x04002089 RID: 8329
	public static int planetsUptoDate;

	// Token: 0x02000430 RID: 1072
	public enum PlanetTypes
	{
		// Token: 0x0400208D RID: 8333
		AdventureOffroadCar,
		// Token: 0x0400208E RID: 8334
		AdventureMotorcycle,
		// Token: 0x0400208F RID: 8335
		RacingOffroadCar,
		// Token: 0x04002090 RID: 8336
		RacingMotorcycle
	}
}
