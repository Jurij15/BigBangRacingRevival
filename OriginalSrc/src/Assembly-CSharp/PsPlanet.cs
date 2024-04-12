using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class PsPlanet
{
	// Token: 0x06000902 RID: 2306 RVA: 0x00060E74 File Offset: 0x0005F274
	public PsPlanet()
	{
		PsPlanetManager.m_planets.Add(this);
		this.m_floaterManager = new PsFloaterManager(this);
		this.m_floatingNodeList = new List<PsFloatingPlanetNode>();
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00060EE4 File Offset: 0x0005F2E4
	public void SetPlanetData(PlanetProgressionInfo _data)
	{
		this.m_planetData = _data;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00060EED File Offset: 0x0005F2ED
	public string GetIdentifier()
	{
		if (this.m_planetData == null)
		{
			return null;
		}
		return this.m_planetData.m_planetIdentifier;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00060F07 File Offset: 0x0005F307
	public PlanetProgressionInfo GetPlanetInfo()
	{
		return this.m_planetData;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00060F0F File Offset: 0x0005F30F
	public PsPlanetPath GetMainPath()
	{
		if (this.m_planetData == null)
		{
			return null;
		}
		return this.m_planetData.m_mainPath;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00060F29 File Offset: 0x0005F329
	public PsPlanetPath GetFloatingPath()
	{
		if (this.m_planetData == null)
		{
			return null;
		}
		return this.m_planetData.m_floatingPath;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00060F43 File Offset: 0x0005F343
	public int GetMainPathCurrentNodeId()
	{
		if (this.GetMainPath() == null)
		{
			return -1;
		}
		return this.GetMainPath().m_currentNodeId;
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00060F5D File Offset: 0x0005F35D
	public int GetLastNodeId()
	{
		if (this.GetMainPath() != null)
		{
			return this.GetMainPath().m_nodeInfos.Count;
		}
		return 0;
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00060F7C File Offset: 0x0005F37C
	public void RearrangeFloaters(PsFloatingPlanetNode _deleteNode)
	{
		Debug.LogWarning("Arranging floatingpath");
		int num = this.m_floatingNodeList.IndexOf(_deleteNode) + 1;
		for (int i = num; i < this.m_floatingNodeList.Count; i++)
		{
			this.m_floatingNodeList[i].m_loop.m_levelNumber--;
			this.m_floatingNodeList[i].RearrangeNodePosition();
		}
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00060FF0 File Offset: 0x0005F3F0
	public void ChangePlanetSequence()
	{
		Debug.LogWarning("start planet change");
		TimerC timerC = TimerS.AddComponent(this.m_spaceEntity, "PlanetChanger", 0.5f, 0f, false, new TimerComponentDelegate(this.ChangePlanetSequenceMidpoint));
		timerC = TimerS.AddComponent(this.m_spaceEntity, "PlanetChanger", 1f, 0f, false, new TimerComponentDelegate(this.ChangePlanetSequenceFinished));
		PsPlanet.m_changePlanetTicks = 0;
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0006105D File Offset: 0x0005F45D
	private void ChangePlanetSequenceMidpoint(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		PlanetTools.ChangePlanet(PsMainMenuState.GetCurrentPlanetIdentifier());
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0006106F File Offset: 0x0005F46F
	private void ChangePlanetSequenceFinished(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		PsMainMenuState.ShowUI(true, null);
		TouchAreaS.Enable();
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00061084 File Offset: 0x0005F484
	public void ResetPlanet()
	{
		this.ClearRows();
		this.ClearFloatingNodes();
		if (this.m_planetEntity != null)
		{
			EntityManager.RemoveEntity(this.m_planetEntity);
		}
		this.m_planetEntity = null;
		this.m_alienPlanetTC = null;
		this.m_rollTC = null;
		this.m_backgroundTC = null;
		this.m_cloudTC = null;
		this.m_atmosphereTC = null;
		this.m_floatingNodesTC = null;
		this.CreatePlanet();
		this.RemoveRollTouchArea();
		this.CreateRollTouchArea();
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x000610F8 File Offset: 0x0005F4F8
	public void CreatePlanet()
	{
		this.m_planetEntity = EntityManager.AddEntity("PlanetUI");
		this.m_alienPlanetTC = TransformS.AddComponent(this.m_planetEntity);
		this.m_rollTC = TransformS.AddComponent(this.m_planetEntity, new Vector3(0f, 0f, 200f));
		this.m_backgroundTC = TransformS.AddComponent(this.m_planetEntity);
		this.m_backgroundTC.transform.Rotate(Vector3.up * 180f);
		GameObject gameObject;
		if (PsState.GetVehicleIndex() == 0)
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.PlanetSliceJungleAPrefab_GameObject);
		}
		else
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.PlanetSliceDesertAPrefab_GameObject);
		}
		PrefabC prefabC = PrefabS.AddComponent(this.m_alienPlanetTC, Vector3.zero, gameObject);
		prefabC.p_gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
		PrefabC prefabC2 = PrefabS.AddComponent(this.m_alienPlanetTC, Vector3.zero, gameObject);
		prefabC2.p_gameObject.transform.rotation = Quaternion.Euler(90f, 0f, -90f);
		PrefabC prefabC3 = PrefabS.AddComponent(this.m_alienPlanetTC, Vector3.zero, gameObject);
		prefabC3.p_gameObject.transform.rotation = Quaternion.Euler(180f, 0f, -90f);
		PrefabC prefabC4 = PrefabS.AddComponent(this.m_alienPlanetTC, Vector3.zero, gameObject);
		prefabC4.p_gameObject.transform.rotation = Quaternion.Euler(270f, 0f, -90f);
		if (PsState.GetVehicleIndex() == 0)
		{
			PrefabC prefabC5 = PrefabS.AddComponent(this.m_backgroundTC, ResourceManager.GetGameObject(RESOURCE.PlanetBackroundJunglePrefab_GameObject), string.Empty);
			PrefabS.SetCamera(prefabC5, this.m_planetCamera);
		}
		else
		{
			PrefabC prefabC6 = PrefabS.AddComponent(this.m_backgroundTC, ResourceManager.GetGameObject(RESOURCE.PlanetBackroundDesertPrefab_GameObject), string.Empty);
			PrefabS.SetCamera(prefabC6, this.m_planetCamera);
		}
		PrefabC prefabC7 = PrefabS.AddComponent(this.m_backgroundTC, ResourceManager.GetGameObject(RESOURCE.PlanetStarskyPrefab_GameObject), string.Empty);
		PrefabS.SetCamera(prefabC7, this.m_planetCamera);
		this.m_cloudTC = TransformS.AddComponent(this.m_planetEntity, Vector3.zero);
		PrefabC prefabC8 = PrefabS.AddComponent(this.m_cloudTC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CloudsA_GameObject));
		prefabC8.p_gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		PrefabC prefabC9 = PrefabS.AddComponent(this.m_cloudTC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CloudsB_GameObject));
		prefabC9.p_gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
		PrefabC prefabC10 = PrefabS.AddComponent(this.m_cloudTC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CloudsC_GameObject));
		prefabC10.p_gameObject.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
		PrefabC prefabC11 = PrefabS.AddComponent(this.m_cloudTC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CloudsB_GameObject));
		prefabC11.p_gameObject.transform.rotation = Quaternion.Euler(270f, 0f, 0f);
		PrefabS.SetCamera(prefabC8, this.m_planetCamera);
		PrefabS.SetCamera(prefabC9, this.m_planetCamera);
		PrefabS.SetCamera(prefabC10, this.m_planetCamera);
		PrefabS.SetCamera(prefabC11, this.m_planetCamera);
		this.m_cloudSets = new List<Transform>();
		this.m_cloudParticles = new List<Transform>();
		this.ParseClouds(prefabC8.p_gameObject);
		this.ParseClouds(prefabC9.p_gameObject);
		this.ParseClouds(prefabC10.p_gameObject);
		this.ParseClouds(prefabC11.p_gameObject);
		this.m_atmosphereTC = TransformS.AddComponent(this.m_planetEntity, Vector3.zero);
		PrefabC prefabC12 = PrefabS.AddComponent(this.m_atmosphereTC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.PlanetAtmosphere_GameObject));
		PrefabS.SetCamera(prefabC12, this.m_planetCamera);
		this.m_floatingNodesTC = TransformS.AddComponent(this.m_planetEntity, Vector3.zero);
		TransformS.SetRotation(this.m_cloudTC, Vector3.right * this.m_currentAngle);
		TransformS.SetRotation(this.m_atmosphereTC, Vector3.right * this.m_currentAngle);
		TransformS.SetRotation(this.m_floatingNodesTC, Vector3.right * (this.m_pathStartOffset + this.m_floatingOffset));
		PsPlanet.m_released = true;
		PsPlanet.m_dragging = false;
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00061558 File Offset: 0x0005F958
	public void AlienPlanetTouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (PsPlanet.m_locked)
		{
			return;
		}
		if (_touchPhase == TouchAreaPhase.DragStart)
		{
			PsPlanet.m_released = false;
			PsPlanet.m_dragging = true;
			this.m_rollStartAngle = _touches[0].m_currentPosition.y * 90f / (float)Screen.height;
			this.m_prevRollAngle = this.m_rollStartAngle;
		}
		else if (PsPlanet.m_dragging && (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut || _touchPhase == TouchAreaPhase.StationaryIn || _touchPhase == TouchAreaPhase.StationaryOut))
		{
			this.m_rollAngle = _touches[0].m_currentPosition.y * 90f / (float)Screen.height;
			PsPlanet.m_rollInertia = (PsPlanet.m_rollInertia + this.m_rollAngle - this.m_prevRollAngle) * 0.5f;
			this.m_prevRollAngle = this.m_rollAngle;
		}
		else if (_touchPhase == TouchAreaPhase.DragEnd)
		{
			PsPlanet.m_released = true;
			PsPlanet.m_dragging = false;
			if (_touchArea.m_wasDragged)
			{
				this.m_rollAngle = _touches[0].m_currentPosition.y * 90f / (float)Screen.height;
				PsPlanet.m_rollInertia = (PsPlanet.m_rollInertia + this.m_rollAngle - this.m_prevRollAngle) * 0.5f * (float)Main.m_ticksPerFrame;
				this.m_prevRollAngle = this.m_rollAngle;
			}
		}
		if (_touchPhase == TouchAreaPhase.ReleaseIn && !_touchArea.m_wasDragged)
		{
			PsPlanet currentPlanet = PsPlanetManager.GetCurrentPlanet();
			float num = 2f;
			Ray ray = currentPlanet.m_planetCamera.ScreenPointToRay(_touches[0].m_currentPosition);
			RaycastHit[] array = Physics.SphereCastAll(ray.origin, num, ray.direction, 1000f, 1 << currentPlanet.m_planetCamera.gameObject.layer);
			TouchAreaC touchAreaC = null;
			float num2 = 999999f;
			foreach (RaycastHit raycastHit in array)
			{
				TouchAreaBootstrap touchAreaBootstrap = raycastHit.transform.GetComponent("TouchAreaBootstrap") as TouchAreaBootstrap;
				if (touchAreaBootstrap != null)
				{
					Vector2 vector = currentPlanet.m_planetCamera.WorldToScreenPoint(touchAreaBootstrap.m_TAC.m_TC.transform.position);
					float num3 = Vector2.Distance(vector, _touches[0].m_currentPosition);
					if (num3 < num2)
					{
						num2 = num3;
						touchAreaC = touchAreaBootstrap.m_TAC;
					}
				}
			}
			if (touchAreaC != null)
			{
				touchAreaC.m_touchCount = 1;
				_touches[0].m_primaryArea = touchAreaC;
				if (touchAreaC.d_TouchEventDelegate != null)
				{
					touchAreaC.d_TouchEventDelegate(touchAreaC, _touchPhase, _touchIsSecondary, 1, _touches);
				}
				_touchArea.m_touchCount = 0;
				return;
			}
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x000617F4 File Offset: 0x0005FBF4
	public void CreateSpaceScene()
	{
		Debug.LogWarning("CREATING SPACE SCENE");
		this.m_spaceEntity = EntityManager.AddEntity("PlanetUI");
		CameraS.m_mainCamera.enabled = false;
		this.m_planetCameraTC = TransformS.AddComponent(this.m_spaceEntity, "PlanetCameraTC");
		this.m_planetCameraTC2 = TransformS.AddComponent(this.m_spaceEntity, "PlanetCameraTC");
		TransformS.SetPosition(this.m_planetCameraTC2, new Vector3(-6.35f, 26.7f, -250f));
		TransformS.ParentComponent(this.m_planetCameraTC2, this.m_planetCameraTC);
		this.m_planetCamera = CameraS.AddCamera("PlanetCamera", false, 3);
		this.m_planetCamera.transform.parent = this.m_planetCameraTC2.transform;
		this.m_planetCamera.transform.localPosition = Vector3.zero;
		this.m_planetCamera.transform.localRotation = Quaternion.identity;
		this.m_planetCamera.depth = -1f;
		this.m_planetCamera.clearFlags = 2;
		this.m_planetCamera.fieldOfView = 26f;
		this.m_planetCamera.backgroundColor = Color.black;
		this.m_planetCamera.transform.eulerAngles = new Vector3(349f, 357f, 12f);
		this.m_planetCamera.transform.localPosition = new Vector3(-4.83f, 0f, -15f);
		this.m_planetCamera.transform.eulerAngles = new Vector3(348.56f, 351.75f, 13.1f);
		this.m_planetCamera.cullingMask = this.m_planetCamera.cullingMask | 512;
		this.m_planetSpriteSheet = SpriteS.AddSpriteSheet(this.m_planetCamera, ResourceManager.GetMaterial(RESOURCE.UIPlanetAtlasMat_Material), ResourceManager.GetTextAsset(RESOURCE.UiAtlas_TextAsset), 1f);
		this.m_planetSpriteSheetTutorial = SpriteS.AddSpriteSheet(this.m_planetCamera, ResourceManager.GetMaterial(RESOURCE.UIPlanetAtlasMat_Material), ResourceManager.GetTextAsset(RESOURCE.UiAtlas_TextAsset), 1f);
		PlanetTools.ChangePlanet(PlanetTools.GetVehiclePlanetIdentifier());
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x000619FC File Offset: 0x0005FDFC
	public void CreateRollTouchArea()
	{
		if (this.m_rollTAC != null)
		{
			return;
		}
		this.m_rollTAC = TouchAreaS.AddRectArea(this.m_rollTC, "Roll", (float)Screen.width, (float)Screen.height, CameraS.m_uiCamera, null, default(Vector2));
		TouchAreaS.AddTouchEventListener(this.m_rollTAC, new TouchEventDelegate(this.AlienPlanetTouchHandler));
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x00061A5D File Offset: 0x0005FE5D
	public void RemoveRollTouchArea()
	{
		if (this.m_rollTAC != null)
		{
			TouchAreaS.RemoveArea(this.m_rollTAC);
			this.m_rollTAC = null;
		}
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00061A7C File Offset: 0x0005FE7C
	public void CreateAndShowBanners()
	{
		this.CreateBanners();
		if (this.m_banners != null && this.m_banners.Count > 0)
		{
			this.m_banners[0].Show();
		}
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x00061AB4 File Offset: 0x0005FEB4
	private void ParseClouds(GameObject _cloudSlice)
	{
		for (int i = 0; i < _cloudSlice.transform.childCount; i++)
		{
			Transform child = _cloudSlice.transform.GetChild(i);
			this.m_cloudSets.Add(child);
			for (int j = 0; j < child.childCount; j++)
			{
				Transform child2 = child.GetChild(j);
				this.m_cloudParticles.Add(child2);
			}
		}
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00061B21 File Offset: 0x0005FF21
	public void HideSpaceScene()
	{
		EntityManager.SetVisibilityOfEntity(this.m_spaceEntity, false);
		EntityManager.SetVisibilityOfEntity(this.m_planetEntity, false);
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x00061B3B File Offset: 0x0005FF3B
	public void ShowSpaceScene()
	{
		EntityManager.SetVisibilityOfEntity(this.m_spaceEntity, true);
		EntityManager.SetVisibilityOfEntity(this.m_planetEntity, true);
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00061B58 File Offset: 0x0005FF58
	public void DestroySpaceScene()
	{
		Debug.LogWarning("DESTROYING SPACE SCENE");
		CameraS.RemoveCamera(this.m_planetCamera);
		SpriteS.RemoveSpriteSheet(this.m_planetSpriteSheet);
		SpriteS.RemoveSpriteSheet(this.m_planetSpriteSheetTutorial);
		if (this.m_spaceEntity != null)
		{
			EntityManager.RemoveEntity(this.m_spaceEntity);
		}
		this.m_spaceEntity = null;
		if (this.m_planetEntity != null)
		{
			EntityManager.RemoveEntity(this.m_planetEntity);
		}
		this.m_planetEntity = null;
		this.m_rollTAC = null;
		this.ClearRows();
		this.ClearFloatingNodes();
		this.DestroyBanners();
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00061BE3 File Offset: 0x0005FFE3
	public void InitializePathData()
	{
		this.m_nodeRows = new List<PlanetRow>();
		if (this.GetMainPath() == null)
		{
			this.m_planetData.m_mainPath = new PsPlanetPath("MainPath", this.m_planetData.m_planetIdentifier, PsPlanetPathType.main);
		}
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00061C1C File Offset: 0x0006001C
	public void ResetLocalProgression()
	{
		if (this.m_nodeRows != null)
		{
			while (this.m_nodeRows.Count > 0)
			{
				int num = this.m_nodeRows.Count - 1;
				PlanetRow planetRow = this.m_nodeRows[num];
				while (planetRow.m_nodes.Count > 0)
				{
					int num2 = planetRow.m_nodes.Count - 1;
					planetRow.m_nodes[num2].Destroy();
					planetRow.m_nodes.RemoveAt(num2);
				}
				this.m_nodeRows.RemoveAt(num);
			}
		}
		this.m_floaterManager.ClearFloatingNodes();
		this.m_planetData.m_mainPath = new PsPlanetPath("MainPath", this.m_planetData.m_planetIdentifier, PsPlanetPathType.main);
		PsMetagameData.LockAll();
		PsPlanetSerializer.SaveProgressionLocally(this);
		PlayerPrefsX.SetLastSync("0");
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00061CF4 File Offset: 0x000600F4
	public void InitializePlanetNodes(PsGameLoop _targetNode = null)
	{
		this.SetRowLimits(0, this.GetLastNodeId());
		int num = this.GetMainPath().m_currentNodeId;
		if (PsState.m_activeGameLoop != null && PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_path.m_planet == this.GetIdentifier() && (PsState.m_activeGameLoop.m_context == PsMinigameContext.Level || PsState.m_activeGameLoop.m_context == PsMinigameContext.Block))
		{
			num = PsState.m_activeGameLoop.m_nodeId + PsState.m_activeGameLoop.m_path.m_startNodeId + 1;
		}
		this.m_currentAngle = (float)(num - 2) * -this.m_nodeRowAngleInterval;
		PsMainMenuState.m_currentScroll = this.m_currentAngle;
		this.Update();
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00061DB4 File Offset: 0x000601B4
	public void SetRowLimits(int _min, int _max)
	{
		this.m_minRow = _min - 1;
		this.m_maxRow = _max - 2;
		this.m_minAngle = (float)this.m_minRow * this.m_nodeRowAngleInterval;
		this.m_maxAngle = (float)this.m_maxRow * this.m_nodeRowAngleInterval;
		Debug.LogWarning(string.Concat(new object[] { "SetRowLimits: ", this.m_minRow, " - ", this.m_maxAngle }));
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00061E38 File Offset: 0x00060238
	public List<PsGameLoop> GetRow(int _rowId)
	{
		List<PsGameLoop> list = new List<PsGameLoop>();
		PsGameLoop nodeInfo = this.GetMainPath().GetNodeInfo(_rowId);
		if (nodeInfo != null)
		{
			list.Add(nodeInfo);
		}
		int num = 0;
		int num2 = this.GetMainPath().GetPrevBlockId(_rowId) - 5;
		for (int i = _rowId - 1; i > num2 - 1; i--)
		{
			num++;
			PsGameLoop nodeInfo2 = this.GetMainPath().GetNodeInfo(i);
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

	// Token: 0x0600091E RID: 2334 RVA: 0x00061EDC File Offset: 0x000602DC
	public static PsGameLoop CreateLoop(PsPlanetPath _path, MetagameNodeData _data, int _id, ref int _levelNumber, string _unlockName)
	{
		PsGameLoop psGameLoop = null;
		if (_data.m_nodeDataType == MetagameNodeDataType.Level)
		{
			_levelNumber++;
			MinigameSearchParametres minigameSearchParametres = new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any);
			minigameSearchParametres.m_difficulty = _data.m_levelDifficulty;
			MinigameSearchParametres minigameSearchParametres2 = minigameSearchParametres;
			object obj;
			if (_data.m_levelSubgenre == PsSubgenre.Any)
			{
				obj = null;
			}
			else
			{
				(obj = new string[1])[0] = _data.m_levelSubgenre.ToString();
			}
			minigameSearchParametres2.m_features = obj;
			minigameSearchParametres.m_gameMode = _data.m_levelGameMode;
			minigameSearchParametres.m_items = _data.m_levelItems.ToArray();
			minigameSearchParametres.m_playerUnitFilter = _data.m_levelPlayerUnit;
			if (_path.m_planet.Contains("Racing"))
			{
				psGameLoop = new PsGameLoopRacing(_data.m_levelMinigameId, minigameSearchParametres, _path, _id, _levelNumber, 0, false, 1, false, false, 0, null, 0, false, _data.m_levelMedalTimes, null, 0);
			}
			else
			{
				psGameLoop = new PsGameLoopAdventure(_data.m_levelMinigameId, minigameSearchParametres, _path, _id, _levelNumber, 0, false, _data.m_levelMedalTimes);
			}
		}
		else if (_data.m_nodeDataType == MetagameNodeDataType.Signal || _data.m_nodeDataType == MetagameNodeDataType.Block)
		{
			if (_data.m_blockType == PsPathBlockType.Boss)
			{
				psGameLoop = new PsGameLoopAdventureBattle(PsMinigameContext.Block, _data.m_levelMinigameId, null, _path, _id, -1, 0, false, _unlockName, 0, 0, 0, string.Empty, 0.0);
			}
			else
			{
				GachaType gachaType = GachaType.SILVER;
				if (_data.m_blockType == PsPathBlockType.Chest)
				{
					switch (_data.m_blockChestType)
					{
					case PsPathBlockChestType.Bronze:
						gachaType = GachaType.BRONZE;
						break;
					case PsPathBlockChestType.Silver:
						gachaType = GachaType.SILVER;
						break;
					case PsPathBlockChestType.Random:
						if (Random.value > 0.3f)
						{
							gachaType = GachaType.SILVER;
						}
						else
						{
							gachaType = GachaType.GOLD;
						}
						break;
					case PsPathBlockChestType.Gold:
						gachaType = GachaType.GOLD;
						break;
					}
				}
				psGameLoop = new PsGameLoopBlockLevel(PsMinigameContext.Block, _data.m_levelMinigameId, _data.m_blockType, gachaType, _data.m_blockRequiredStars, _unlockName, _data.m_blockWillUnlock, _path, _id, 0, _data.m_blockCoins, _data.m_blockDiamonds, _data.m_blockBolts, _data.m_blockKeys, false, _data.m_dataIdentifier);
			}
		}
		return psGameLoop;
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x000620C0 File Offset: 0x000604C0
	public void UpdatePlanetUI()
	{
		if (this.m_banners != null)
		{
			for (int i = 0; i < this.m_banners.Count; i++)
			{
				this.m_banners[i].UpdateBannerText();
			}
		}
		if (this.m_floatingNodeList != null)
		{
			for (int j = 0; j < this.m_floatingNodeList.Count; j++)
			{
				this.m_floatingNodeList[j].UpdateBanner();
			}
		}
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00062140 File Offset: 0x00060540
	public void RollToRowId(int _rowId)
	{
		this.m_forcedRoll = true;
		this.m_startAngle = this.m_currentAngle;
		this.m_targetRow = _rowId - 1;
		this.m_targetAngle = (float)this.m_targetRow * -this.m_nodeRowAngleInterval;
		this.m_rollStartTime = Main.m_resettingGameTime;
		this.m_rollDifference = this.m_targetAngle - this.m_startAngle;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x0006219C File Offset: 0x0006059C
	public void Update()
	{
		if (PsPlanet.m_changePlanetTicks > -1)
		{
			PsPlanet.m_changePlanetTicks++;
			float num = ((float)PsPlanet.m_changePlanetTicks - 30f) / 60f;
			float num2 = (float)PsPlanet.m_changePlanetTicks / 30f;
			if (PsPlanet.m_changePlanetTicks == 1)
			{
				if (this.m_starEntity != null)
				{
					EntityManager.RemoveEntity(this.m_starEntity);
				}
				this.m_starEntity = EntityManager.AddEntity();
				this.m_starEntity.m_persistent = true;
				this.m_starTC = TransformS.AddComponent(this.m_starEntity);
				this.m_stars = new List<PsPlanet.PsStar>();
			}
			else if (PsPlanet.m_changePlanetTicks < 30)
			{
				this.m_planetCameraTC2.transform.localRotation = Quaternion.Euler(new Vector3(-45f, 0f, 0f) * num2);
				this.m_planetCameraTC.transform.localPosition = Vector3.forward * -1000f * num2;
				for (int i = 0; i < 10; i++)
				{
					PsPlanet.PsStar psStar = new PsPlanet.PsStar();
					PsPlanet.PsStar psStar2 = psStar;
					Vector2 vector;
					vector..ctor(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
					psStar2.m_dir = vector.normalized;
					psStar.m_age = Random.Range(num2 * 0.25f, 0.5f);
					this.m_stars.Add(psStar);
				}
			}
			else if (PsPlanet.m_changePlanetTicks == 90)
			{
				this.m_planetCameraTC.transform.localPosition = Vector3.zero;
				PsPlanet.m_changePlanetTicks = -1;
				this.m_stars = null;
				EntityManager.RemoveEntity(this.m_starEntity);
				this.m_starEntity = null;
				this.m_starTC = null;
			}
			else if (PsPlanet.m_changePlanetTicks > 30)
			{
				this.m_planetCameraTC2.transform.localRotation = Quaternion.identity;
				this.m_planetCameraTC.transform.localPosition = new Vector3(1000f, -2000f, -10000f) * Mathf.Pow(TweenS.tween(TweenStyle.ExpoOut, num, 1f, 1f, -1f), 2f);
			}
		}
		if (this.m_stars != null)
		{
			DebugDraw.Clear(CameraS.m_uiCamera, this.m_starTC);
			for (int j = this.m_stars.Count - 1; j > -1; j--)
			{
				PsPlanet.PsStar psStar3 = this.m_stars[j];
				psStar3.m_age += Main.m_gameDeltaTime;
				if (psStar3.m_age > 1f)
				{
					this.m_stars.RemoveAt(j);
				}
				Vector2 vector2 = psStar3.m_dir * (float)Screen.width * psStar3.m_age * psStar3.m_age + new Vector2(0.13f * (float)Screen.width, -0.1f * (float)Screen.height);
				float num3 = (float)Screen.width * 0.2f * psStar3.m_age * psStar3.m_age;
				DebugDraw.CreateLine(CameraS.m_uiCamera, this.m_starTC, num3, vector2, Mathf.Atan2(psStar3.m_dir.y, psStar3.m_dir.x) * 57.29578f);
			}
		}
		this.m_currentZeroIndex = Mathf.FloorToInt(-this.m_currentAngle / this.m_nodeRowAngleInterval);
		if (this.m_forcedRoll)
		{
			float num4 = Main.m_resettingGameTime - this.m_rollStartTime;
			if (num4 < 1.5f)
			{
				float num5 = TweenS.tween(TweenStyle.CubicInOut, num4, 1.5f, this.m_startAngle, this.m_rollDifference);
				this.m_currentAngle = num5;
				this.m_forcedRollActive = true;
			}
			else if (this.m_forcedRollActive)
			{
				this.m_forcedRoll = false;
				this.m_forcedRollActive = false;
			}
		}
		for (int k = 0; k < this.m_nodeRowCount; k++)
		{
			if (this.m_nodeRows.Count > k)
			{
				int rowId = this.m_nodeRows[0].m_rowId;
				if (rowId < this.m_currentZeroIndex)
				{
					int num6 = this.m_currentZeroIndex - rowId;
					for (int l = 0; l < num6; l++)
					{
						int rowId2 = this.m_nodeRows[this.m_nodeRows.Count - 1].m_rowId;
						PlanetRow planetRow = new PlanetRow(rowId2 + 1);
						this.m_nodeRows.Add(planetRow);
						List<PsGameLoop> row = this.GetRow(rowId2 + 1);
						for (int m = 0; m < row.Count; m++)
						{
							PsPlanetNode psPlanetNode = this.CreatePlanetNode(row[m], false);
							planetRow.m_nodes.Add(psPlanetNode);
						}
					}
					while (this.m_nodeRows.Count > this.m_nodeRowCount)
					{
						int num7 = 0;
						PlanetRow planetRow2 = this.m_nodeRows[num7];
						while (planetRow2.m_nodes.Count > 0)
						{
							int num8 = planetRow2.m_nodes.Count - 1;
							if (planetRow2.m_nodes[num8] != null)
							{
								planetRow2.m_nodes[num8].Destroy();
							}
							planetRow2.m_nodes.RemoveAt(num8);
						}
						this.m_nodeRows.RemoveAt(num7);
					}
				}
				else if (rowId > this.m_currentZeroIndex)
				{
					int num9 = rowId - this.m_currentZeroIndex;
					for (int n = num9 - 1; n > -1; n--)
					{
						PlanetRow planetRow3 = new PlanetRow(this.m_currentZeroIndex + n);
						this.m_nodeRows.Insert(0, planetRow3);
						List<PsGameLoop> row2 = this.GetRow(this.m_currentZeroIndex + n);
						for (int num10 = 0; num10 < row2.Count; num10++)
						{
							PsPlanetNode psPlanetNode2 = this.CreatePlanetNode(row2[num10], false);
							planetRow3.m_nodes.Add(psPlanetNode2);
						}
					}
					while (this.m_nodeRows.Count > this.m_nodeRowCount)
					{
						int num11 = this.m_nodeRows.Count - 1;
						PlanetRow planetRow4 = this.m_nodeRows[num11];
						while (planetRow4.m_nodes.Count > 0)
						{
							int num12 = planetRow4.m_nodes.Count - 1;
							planetRow4.m_nodes[num12].Destroy();
							planetRow4.m_nodes.RemoveAt(num12);
						}
						this.m_nodeRows.RemoveAt(num11);
					}
				}
			}
			else
			{
				PlanetRow planetRow5 = new PlanetRow(this.m_currentZeroIndex + k);
				this.m_nodeRows.Add(planetRow5);
				List<PsGameLoop> row3 = this.GetRow(this.m_currentZeroIndex + k);
				for (int num13 = 0; num13 < row3.Count; num13++)
				{
					PsPlanetNode psPlanetNode3 = this.CreatePlanetNode(row3[num13], false);
					planetRow5.m_nodes.Add(psPlanetNode3);
				}
			}
		}
		TransformS.SetRotation(this.m_alienPlanetTC, Vector3.right * this.m_currentAngle);
		float num14 = (float)Math.Max(0, this.m_floatingNodeList.Count - 3);
		float num15 = num14 * this.m_nodeRowAngleInterval;
		if (num15 > 0f)
		{
			this.m_floaterOffset = Mathf.Max(0f, Mathf.Min(1f, (this.m_floaterOffset * num15 - PsPlanet.m_rollInertia) / num15));
		}
		this.m_floatingNodesTC.transform.Rotate(Vector3.right * PsPlanet.m_rollInertia);
		Quaternion quaternion = Quaternion.Lerp(this.m_floatingNodesTC.transform.rotation, Quaternion.Euler(Vector3.right * (this.m_pathStartOffset + this.m_floatingOffset - this.m_floaterOffset * num15)), 0.025f);
		this.m_floatingNodesTC.transform.localRotation = quaternion;
		Quaternion quaternion2 = Quaternion.Lerp(this.m_cloudTC.transform.localRotation, this.m_alienPlanetTC.transform.localRotation, 0.1f);
		this.m_cloudTC.transform.localRotation = quaternion2;
		Quaternion quaternion3 = Quaternion.Lerp(this.m_atmosphereTC.transform.localRotation, this.m_alienPlanetTC.transform.localRotation, 0.035f);
		this.m_atmosphereTC.transform.localRotation = quaternion3;
		for (int num16 = 0; num16 < this.m_cloudSets.Count; num16++)
		{
			this.m_cloudSets[num16].RotateAround(Vector3.zero, Vector3.right, (Mathf.Sin(Main.m_resettingGameTime * 0.25f + (float)(num16 * 10)) * 0.75f + 0.75f) * -0.025f - 0.025f);
		}
		for (int num17 = 0; num17 < this.m_cloudParticles.Count; num17++)
		{
			this.m_cloudParticles[num17].LookAt(this.m_planetCamera.transform.position);
			this.m_cloudParticles[num17].Rotate(Vector3.right * -90f);
		}
		for (int num18 = 0; num18 < this.m_nodeRows.Count; num18++)
		{
			PlanetRow planetRow6 = this.m_nodeRows[num18];
			for (int num19 = 0; num19 < planetRow6.m_nodes.Count; num19++)
			{
				planetRow6.m_nodes[num19].Update();
			}
		}
		for (int num20 = 0; num20 < this.m_floatingNodeList.Count; num20++)
		{
			this.m_floatingNodeList[num20].Update();
		}
		if (this.m_banners != null && this.m_banners.Count > 0)
		{
			for (int num21 = this.m_banners.Count - 1; num21 >= 0; num21--)
			{
				PsUICheckpointBanner banner = this.m_banners[num21];
				if (PsGachaManager.m_gachas[banner.m_gachaSlotIndex] == null && banner != null)
				{
					this.m_banners.Remove(banner);
					banner.Destroy();
					banner = null;
				}
				else if (PsGachaManager.m_gachas[banner.m_gachaSlotIndex].m_unlocked)
				{
					banner.Hide(delegate
					{
						this.m_banners.Remove(banner);
						banner.Destroy();
						banner = null;
					});
				}
				else if (banner != null)
				{
					banner.Step();
				}
				if (banner != null)
				{
					PsGameLoop gameLoop = banner.m_gameLoop;
					float num22 = (float)(gameLoop.m_nodeId - 2) * this.m_nodeRowAngleInterval + this.m_currentAngle;
					if (num22 < 38f && (banner.m_state == PsUICheckpointBanner.State.STAND || banner.m_state == PsUICheckpointBanner.State.UPDATE))
					{
						banner.m_state = PsUICheckpointBanner.State.FOLLOW;
						banner.SetArrowShape(0f);
						int num23 = gameLoop.m_nodeId + gameLoop.m_path.m_startNodeId;
						float xposition = PsPlanetNode.GetXPosition(gameLoop, 0);
						float num24 = 0f;
						float num25 = (float)num23 + num24;
						float num26 = Mathf.Cos((this.m_pathStartOffset + num25 * this.m_nodeRowAngleInterval) * 0.017453292f) * this.m_planetRadius;
						float num27 = Mathf.Sin((this.m_pathStartOffset + num25 * this.m_nodeRowAngleInterval) * 0.017453292f) * this.m_planetRadius;
						Vector3 vector3;
						vector3..ctor(xposition, num26, num27);
						Vector3 vector4 = vector3.normalized * this.m_planetRadius;
						float num28 = Mathf.Atan2(-num26, num27) * 57.29578f + 180f;
						float num29 = Mathf.Atan2(this.m_planetRadius, xposition) * 57.29578f + 90f;
						TransformS.ParentComponent(banner.m_checkpointTC, this.m_alienPlanetTC, vector4);
						TransformS.SetRotation(banner.m_checkpointTC, new Vector3(num28, 0f, 0f));
						banner.m_checkpointTC.transform.Rotate(new Vector3(90f, num29, 0f), 1);
					}
					else if (num22 >= 38f && (banner.m_state == PsUICheckpointBanner.State.FOLLOW || banner.m_state == PsUICheckpointBanner.State.UPDATE))
					{
						banner.m_state = PsUICheckpointBanner.State.STAND;
						banner.SetArrowShape((num22 - 38f) / 10f);
						float xposition2 = PsPlanetNode.GetXPosition(gameLoop, 0);
						float num30 = Mathf.Cos((-this.m_currentAngle - 22f) * 0.017453292f) * this.m_planetRadius;
						float num31 = Mathf.Sin((-this.m_currentAngle - 22f) * 0.017453292f) * this.m_planetRadius;
						Vector3 vector5;
						vector5..ctor(xposition2, num30, num31);
						Vector3 vector6 = vector5.normalized * this.m_planetRadius;
						float num32 = Mathf.Atan2(-num30, num31) * 57.29578f + 180f;
						float num33 = Mathf.Atan2(this.m_planetRadius, xposition2) * 57.29578f + 90f;
						TransformS.ParentComponent(banner.m_checkpointTC, this.m_alienPlanetTC, vector6);
						TransformS.SetRotation(banner.m_checkpointTC, new Vector3(num32, 0f, 0f));
						banner.m_checkpointTC.transform.Rotate(new Vector3(90f, num33, 0f), 1);
						banner.m_checkpointTC.transform.SetParent(null, true);
					}
					else if (num22 >= 38f && num22 < 48f)
					{
						banner.SetArrowShape((num22 - 38f) / 10f);
					}
					if (num21 > 0)
					{
						float num34 = (float)(this.m_banners[num21 - 1].m_gameLoop.m_nodeId - 2) * this.m_nodeRowAngleInterval + this.m_currentAngle;
						if (num34 <= 7f && this.m_banners[num21].m_hidden)
						{
							this.m_banners[num21].Show();
						}
						else if (num34 > 7f && !this.m_banners[num21].m_hidden)
						{
							this.m_banners[num21].Hide(null);
						}
					}
					TransformS.SetGlobalPosition(banner.m_bannerTC, banner.m_checkpointTC.transform.position + banner.m_checkpointTC.transform.up * 7f);
				}
			}
		}
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x000630AC File Offset: 0x000614AC
	public void DestroyBanners()
	{
		if (this.m_banners == null || this.m_banners.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.m_banners.Count; i++)
		{
			this.m_banners[i].Destroy();
			this.m_banners[i] = null;
		}
		this.m_banners.Clear();
		this.m_banners = null;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00063124 File Offset: 0x00061524
	public void CreateBanners()
	{
		this.DestroyBanners();
		if (this.m_banners == null)
		{
			this.m_banners = new List<PsUICheckpointBanner>();
		}
		int num = -1;
		if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
		{
			num = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.MOTO_PATH);
		}
		else if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar))
		{
			num = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.CAR_PATH);
		}
		for (int i = 0; i < this.GetMainPath().m_nodeInfos.Count; i++)
		{
			PsGameLoop psGameLoop = this.GetMainPath().m_nodeInfos[i];
			if (!(psGameLoop is PsGameLoopAdventureBattle) && this.GetMainPath().m_currentNodeId == psGameLoop.m_nodeId && psGameLoop.m_context == PsMinigameContext.Block && num >= 0 && (PsGachaManager.m_gachas[num] == null || !PsGachaManager.m_gachas[num].m_unlocked))
			{
				int num2 = psGameLoop.m_nodeId + psGameLoop.m_path.m_startNodeId;
				float xposition = PsPlanetNode.GetXPosition(psGameLoop, 0);
				float num3 = 0f;
				float num4 = (float)num2 + num3;
				float num5 = Mathf.Cos((this.m_pathStartOffset + num4 * this.m_nodeRowAngleInterval) * 0.017453292f) * this.m_planetRadius;
				float num6 = Mathf.Sin((this.m_pathStartOffset + num4 * this.m_nodeRowAngleInterval) * 0.017453292f) * this.m_planetRadius;
				Vector3 vector;
				vector..ctor(xposition, num5, num6);
				Vector3 vector2 = vector.normalized * this.m_planetRadius;
				float num7 = Mathf.Atan2(-num5, num6) * 57.29578f + 180f;
				float num8 = Mathf.Atan2(this.m_planetRadius, xposition) * 57.29578f + 90f;
				TransformC transformC = TransformS.AddComponent(this.m_alienPlanetTC.p_entity, "Checkpoint TC");
				TransformS.ParentComponent(transformC, this.m_alienPlanetTC, vector2);
				TransformS.SetRotation(transformC, new Vector3(num7, 0f, 0f));
				transformC.transform.Rotate(new Vector3(90f, num8, 0f), 1);
				TransformC transformC2 = TransformS.AddComponent(this.m_alienPlanetTC.p_entity, "Banner TC");
				this.m_banners.Add(new PsUICheckpointBanner(psGameLoop, transformC2, transformC, num));
				TransformS.SetRotation(transformC2, this.m_planetCamera.transform.rotation.eulerAngles);
			}
		}
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00063380 File Offset: 0x00061780
	public void ClearFloatingNodes()
	{
		if (this.m_floatingNodeList != null)
		{
			for (int i = this.m_floatingNodeList.Count - 1; i >= 0; i--)
			{
				this.m_floatingNodeList[i].Destroy();
			}
			this.m_floatingNodeList.Clear();
		}
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x000633D4 File Offset: 0x000617D4
	public void ClearRows()
	{
		if (this.m_nodeRows == null)
		{
			return;
		}
		while (this.m_nodeRows.Count > 0)
		{
			int num = this.m_nodeRows.Count - 1;
			PlanetRow planetRow = this.m_nodeRows[num];
			while (planetRow.m_nodes.Count > 0)
			{
				int num2 = planetRow.m_nodes.Count - 1;
				planetRow.m_nodes[num2].Destroy();
				planetRow.m_nodes.RemoveAt(num2);
			}
			this.m_nodeRows.RemoveAt(num);
		}
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0006346C File Offset: 0x0006186C
	private PsPlanetNode CreatePlanetNode(PsGameLoop _info, bool _tutorial = false)
	{
		PsPlanetNode psPlanetNode = null;
		if (_info.m_context == PsMinigameContext.Block)
		{
			psPlanetNode = new PsPlanetBlockNode(_info, _tutorial);
		}
		else if (_info.m_context == PsMinigameContext.Level)
		{
			psPlanetNode = new PsPlanetLevelNode(_info, _tutorial);
		}
		return psPlanetNode;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x000634A8 File Offset: 0x000618A8
	public PsFloatingPlanetNode CreateFloatingNode(PsTimedEventLoop _timedEvent)
	{
		PsFloatingPlanetNode psFloatingPlanetNode = new PsFloatingPlanetNode(_timedEvent, false);
		psFloatingPlanetNode.SetState();
		return psFloatingPlanetNode;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x000634C4 File Offset: 0x000618C4
	public PsGameLoop FixBrokenPathWithBlock(PsGameLoop _loop)
	{
		if (this.m_nodeRows != null && _loop.m_path.m_planet == this.GetIdentifier())
		{
			int count = this.m_nodeRows.Count;
			for (int i = count - 1; i > -1; i--)
			{
				if (this.m_nodeRows[i].m_rowId == _loop.m_nodeId)
				{
					break;
				}
				if (this.m_nodeRows[i].m_nodes.Count == 0)
				{
					this.m_nodeRows.RemoveAt(i);
				}
			}
		}
		return PlanetTools.FixWithBlock(_loop);
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x00063568 File Offset: 0x00061968
	public List<PsGameLoop> GiveNextStrip(PsGameLoop _loop)
	{
		if (this.m_nodeRows != null && _loop.m_path.m_planet == this.GetIdentifier())
		{
			int count = this.m_nodeRows.Count;
			for (int i = count - 1; i > -1; i--)
			{
				if (this.m_nodeRows[i].m_rowId == _loop.m_nodeId)
				{
					break;
				}
				if (this.m_nodeRows[i].m_nodes.Count == 0)
				{
					this.m_nodeRows.RemoveAt(i);
				}
			}
		}
		return PlanetTools.GiveNextStrip(_loop);
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0006360C File Offset: 0x00061A0C
	public void StartRevealSquence(List<PsGameLoop> _addedNodes)
	{
		this.Update();
		if (PsState.m_activeGameLoop.m_node != null)
		{
			PsState.m_activeGameLoop.m_node.Deactivate();
		}
		float num = 0.125f;
		for (int i = 0; i < _addedNodes.Count; i++)
		{
			TimerC timerC = TimerS.AddComponent(this.m_spaceEntity, string.Empty, (float)(i + 1) * num + 1f, 0f, false, new TimerComponentDelegate(this.RevealPlanetNode));
			timerC.customObject = _addedNodes[i];
			if (_addedNodes[i].m_sidePath != null)
			{
				List<PsGameLoop> nodeInfos = _addedNodes[i].m_sidePath.m_nodeInfos;
				for (int j = 0; j < nodeInfos.Count; j++)
				{
					timerC = TimerS.AddComponent(this.m_spaceEntity, string.Empty, (float)(i + j + 2) * num + 1f, 0f, false, new TimerComponentDelegate(this.RevealPlanetNode));
					timerC.customObject = nodeInfos[j];
				}
			}
		}
		TimerC timerC2 = TimerS.AddComponent(this.m_spaceEntity, string.Empty, (float)(_addedNodes.Count + 1) * num + 2f, 0f, false, new TimerComponentDelegate(this.StartRevealUnlockSequence));
		timerC2.customObject = _addedNodes[0];
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00063758 File Offset: 0x00061B58
	private void RevealPlanetNode(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		PsGameLoop psGameLoop = _c.customObject as PsGameLoop;
		if (psGameLoop.m_node != null)
		{
			psGameLoop.m_node.Reveal();
		}
		else
		{
			psGameLoop.m_keepNodeHidden = false;
		}
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00063799 File Offset: 0x00061B99
	private void StartRevealUnlockSequence(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		this.SetRowLimits(0, this.GetLastNodeId());
		PsState.m_activeGameLoop.ReleaseLoop();
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x000637B8 File Offset: 0x00061BB8
	public void FastForward()
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Timer, this.m_spaceEntity);
		Debug.LogWarning("fastforwarding");
		Debug.LogWarning("planet timers: " + componentsByEntity.Count);
		for (int i = 0; i < componentsByEntity.Count; i++)
		{
			TimerC timerC = componentsByEntity[i] as TimerC;
			if (timerC.timeoutHandler != null)
			{
				timerC.timeoutHandler(timerC);
			}
		}
		if (PsState.m_activeGameLoop != null)
		{
			PsState.m_activeGameLoop.ReleaseLoop();
		}
	}

	// Token: 0x0400085E RID: 2142
	public PlanetProgressionInfo m_planetData;

	// Token: 0x0400085F RID: 2143
	public Entity m_planetEntity;

	// Token: 0x04000860 RID: 2144
	public Entity m_spaceEntity;

	// Token: 0x04000861 RID: 2145
	public TouchAreaC m_rollTAC;

	// Token: 0x04000862 RID: 2146
	public TransformC m_alienPlanetTC;

	// Token: 0x04000863 RID: 2147
	public TransformC m_cloudTC;

	// Token: 0x04000864 RID: 2148
	public TransformC m_atmosphereTC;

	// Token: 0x04000865 RID: 2149
	public TransformC m_floatingNodesTC;

	// Token: 0x04000866 RID: 2150
	public TransformC m_rollTC;

	// Token: 0x04000867 RID: 2151
	public TransformC m_backgroundTC;

	// Token: 0x04000868 RID: 2152
	public TransformC m_planetCameraTC;

	// Token: 0x04000869 RID: 2153
	public TransformC m_planetCameraTC2;

	// Token: 0x0400086A RID: 2154
	public Camera m_planetCamera;

	// Token: 0x0400086B RID: 2155
	public float m_currentAngle;

	// Token: 0x0400086C RID: 2156
	public float m_minAngle;

	// Token: 0x0400086D RID: 2157
	public float m_maxAngle;

	// Token: 0x0400086E RID: 2158
	public int m_currentZeroIndex = -1;

	// Token: 0x0400086F RID: 2159
	public int m_minRow;

	// Token: 0x04000870 RID: 2160
	public int m_maxRow;

	// Token: 0x04000871 RID: 2161
	public List<PlanetRow> m_nodeRows;

	// Token: 0x04000872 RID: 2162
	public int m_nodeRowCount = 10;

	// Token: 0x04000873 RID: 2163
	public float m_nodeRowAngleInterval = 10f;

	// Token: 0x04000874 RID: 2164
	public float m_planetRadius = 101.1f;

	// Token: 0x04000875 RID: 2165
	public float m_pathStartOffset = -80f;

	// Token: 0x04000876 RID: 2166
	public float m_floatingOffset = 7f;

	// Token: 0x04000877 RID: 2167
	public float m_floaterOffset;

	// Token: 0x04000878 RID: 2168
	public List<PsPlanetPath> m_sidePaths;

	// Token: 0x04000879 RID: 2169
	public PsFloaterManager m_floaterManager;

	// Token: 0x0400087A RID: 2170
	public SpriteSheet m_planetSpriteSheet;

	// Token: 0x0400087B RID: 2171
	public SpriteSheet m_planetSpriteSheetTutorial;

	// Token: 0x0400087C RID: 2172
	private List<Transform> m_cloudSets;

	// Token: 0x0400087D RID: 2173
	private List<Transform> m_cloudParticles;

	// Token: 0x0400087E RID: 2174
	public List<PsFloatingPlanetNode> m_floatingNodeList;

	// Token: 0x0400087F RID: 2175
	public static int m_changePlanetTicks = -1;

	// Token: 0x04000880 RID: 2176
	private static bool m_dragging;

	// Token: 0x04000881 RID: 2177
	public static bool m_locked;

	// Token: 0x04000882 RID: 2178
	public static bool m_released;

	// Token: 0x04000883 RID: 2179
	private float m_prevRollAngle;

	// Token: 0x04000884 RID: 2180
	public static float m_rollInertia;

	// Token: 0x04000885 RID: 2181
	private float m_rollStartAngle;

	// Token: 0x04000886 RID: 2182
	private float m_rollAngle;

	// Token: 0x04000887 RID: 2183
	public bool m_forcedRoll;

	// Token: 0x04000888 RID: 2184
	public float m_targetAngle;

	// Token: 0x04000889 RID: 2185
	public int m_targetRow;

	// Token: 0x0400088A RID: 2186
	private float m_rollStartTime;

	// Token: 0x0400088B RID: 2187
	private float m_startAngle;

	// Token: 0x0400088C RID: 2188
	private float m_rollDifference;

	// Token: 0x0400088D RID: 2189
	private List<PsPlanet.PsStar> m_stars;

	// Token: 0x0400088E RID: 2190
	private Entity m_starEntity;

	// Token: 0x0400088F RID: 2191
	private TransformC m_starTC;

	// Token: 0x04000890 RID: 2192
	private bool m_forcedRollActive;

	// Token: 0x04000891 RID: 2193
	public List<PsUICheckpointBanner> m_banners;

	// Token: 0x0200012B RID: 299
	private class PsStar
	{
		// Token: 0x04000892 RID: 2194
		public Vector2 m_dir;

		// Token: 0x04000893 RID: 2195
		public float m_age;
	}
}
