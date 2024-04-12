using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class EditorScene : IScene, IStatedObject
{
	// Token: 0x06000E25 RID: 3621 RVA: 0x00083CA0 File Offset: 0x000820A0
	public EditorScene(string _sceneName, bool _toTestState = false)
	{
		PsState.m_UIComponentsHidden = false;
		this.m_name = _sceneName;
		PsState.m_drawLayer = 0;
		PsState.m_editorLastSelectedItemCategory = string.Empty;
		PsState.m_freeWizardLastSelectedItemCategory = string.Empty;
		this.m_toTestState = _toTestState;
		GameObject gameObject = new GameObject("ScreenshotCamera");
		EditorScene.m_screenshotCam = gameObject.AddComponent<Camera>();
		EditorScene.m_screenshotCam.cullingMask = CameraS.m_mainCameraCullingMask;
		EditorScene.m_screenshotCam.gameObject.layer = CameraS.m_mainCameraLayer;
		EditorScene.m_screenshotCam.fieldOfView = CameraS.m_mainCameraFov;
		EditorScene.m_screenshotCam.farClipPlane = CameraS.m_mainCameraFarClip;
		EditorScene.m_screenshotCam.nearClipPlane = CameraS.m_mainCameraNearClip;
		EditorScene.m_screenshotCam.fieldOfView = CameraS.m_mainCameraFov;
		EditorScene.m_screenshotCam.backgroundColor = Color.black;
		EditorScene.m_screenshotCam.gameObject.SetActive(false);
		EditorScene.m_screenshotCam.enabled = false;
		EditorScene.m_screenshotCam.clearFlags = 2;
		EditorScene.m_screenshotHandler = new GameObject("ScreenshotHandler");
		EditorScene.m_storedScreenshots = new Screenshot[3];
		for (int i = 0; i < EditorScene.m_storedScreenshots.Length; i++)
		{
			EditorScene.m_storedScreenshots[i] = EditorScene.m_screenshotHandler.AddComponent(typeof(Screenshot)) as Screenshot;
			EditorScene.m_storedScreenshots[i].Initialize(512, 512);
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00083DFE File Offset: 0x000821FE
	// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00083E06 File Offset: 0x00082206
	public string m_name
	{
		get
		{
			return this._name;
		}
		set
		{
			this._name = value;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00083E0F File Offset: 0x0008220F
	// (set) Token: 0x06000E29 RID: 3625 RVA: 0x00083E17 File Offset: 0x00082217
	public StateMachine m_stateMachine
	{
		get
		{
			return this._stateMachine;
		}
		set
		{
			this._stateMachine = value;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00083E20 File Offset: 0x00082220
	public bool m_initComplete
	{
		get
		{
			return this._initComplete;
		}
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x00083E28 File Offset: 0x00082228
	public void Load()
	{
		this.Initialize();
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x00083E30 File Offset: 0x00082230
	public static void ClearScreenshots()
	{
		PsState.m_cameraManTakeShot = true;
		EditorScene.m_cameraManShotDelay = 0;
		EditorScene.m_nextScreenshotIndex = 0;
		EditorScene.m_manualScreenshotIndex = 0;
		for (int i = 0; i < EditorScene.m_storedScreenshots.Length; i++)
		{
			EditorScene.m_storedScreenshots[i].m_hasShot = false;
		}
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x00083E7C File Offset: 0x0008227C
	public static void TakeScreenshot()
	{
		if (!PsState.m_activeMinigame.m_editing && EditorScene.m_manualScreenshotIndex < 3)
		{
			Debug.LogWarning("SCREENSHOT TAKEN");
			Vector3 position = PsState.m_activeMinigame.m_playerTC.transform.position;
			int num = Random.Range(0, 2);
			if (num != 0)
			{
				position.z -= (float)Random.Range(1000, 1200);
			}
			else
			{
				position.z -= (float)Random.Range(500, 700);
			}
			float num2 = -position.z;
			float num3 = 2f * num2 * Mathf.Tan(EditorScene.m_screenshotCam.fieldOfView * 0.5f * 0.017453292f);
			position.y = Mathf.Max(position.y, (float)(-(float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight) * 0.5f + num3 * 0.2f);
			EditorScene.m_storedScreenshots[EditorScene.m_nextScreenshotIndex].TakeScreenshot(EditorScene.m_screenshotCam, position);
			EditorScene.m_nextScreenshotIndex = Math.Max(EditorScene.m_manualScreenshotIndex, (EditorScene.m_nextScreenshotIndex + 1) % 3);
		}
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x00083FA8 File Offset: 0x000823A8
	public static void TakeManualScreenshot(float _dist = 600f)
	{
		if (!PsState.m_activeMinigame.m_editing)
		{
			Vector3 position = PsState.m_activeMinigame.m_playerTC.transform.position;
			position.z -= _dist;
			float num = -position.z;
			float num2 = 2f * num * Mathf.Tan(EditorScene.m_screenshotCam.fieldOfView * 0.5f * 0.017453292f);
			position.y = Mathf.Max(position.y, (float)(-(float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight) * 0.5f + num2 * 0.2f);
			Debug.LogError("Taking manual screenshot: " + position.ToString());
			EditorScene.m_storedScreenshots[EditorScene.m_manualScreenshotIndex].TakeScreenshot(EditorScene.m_screenshotCam, position);
			EditorScene.m_manualScreenshotIndex = 1;
			EditorScene.m_nextScreenshotIndex = Math.Max(EditorScene.m_manualScreenshotIndex, EditorScene.m_nextScreenshotIndex % 3);
			EditorScene.m_cameraManShotDelay = Random.Range(120, 240);
		}
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x000840A8 File Offset: 0x000824A8
	public static Screenshot SwitchScreenshot(bool _next)
	{
		for (int i = 0; i < 3; i++)
		{
			int num;
			if (_next)
			{
				num = ToolBox.getRolledValue(EditorScene.m_nextScreenshotIndex + i + 1, 0, 2);
			}
			else
			{
				num = ToolBox.getRolledValue(EditorScene.m_nextScreenshotIndex - i - 1, 0, 2);
			}
			if (EditorScene.m_storedScreenshots[num].m_hasShot)
			{
				EditorScene.m_nextScreenshotIndex = num;
				return EditorScene.m_storedScreenshots[num];
			}
		}
		return null;
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x00084116 File Offset: 0x00082516
	public static void SetSelectedScreenshot(int _index)
	{
		EditorScene.m_nextScreenshotIndex = _index;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0008411E File Offset: 0x0008251E
	public static Screenshot GetSelectedScreenshot()
	{
		if (EditorScene.m_storedScreenshots[EditorScene.m_nextScreenshotIndex].m_hasShot)
		{
			return EditorScene.m_storedScreenshots[EditorScene.m_nextScreenshotIndex];
		}
		return null;
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x00084142 File Offset: 0x00082542
	public IState GetCurrentState()
	{
		if (this.m_stateMachine != null)
		{
			return this.m_stateMachine.GetCurrentState();
		}
		return null;
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0008415C File Offset: 0x0008255C
	public void Initialize()
	{
		if (PsState.m_activeGameLoop != null)
		{
			PsState.m_activeGameLoop.EnteredMinigameScene();
		}
		EditorScene.m_reservedResources = new Dictionary<string, ObscuredInt>();
		this._initComplete = true;
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x00084183 File Offset: 0x00082583
	public static int GetReservedResourceCount(string _itemIdentifier)
	{
		if (EditorScene.m_reservedResources != null && EditorScene.m_reservedResources.ContainsKey(_itemIdentifier))
		{
			return EditorScene.m_reservedResources[_itemIdentifier];
		}
		return 0;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x000841B4 File Offset: 0x000825B4
	public static void CumulateReservedResources(Dictionary<string, int> _resources)
	{
		if (_resources.Count > 0)
		{
			List<string> list = new List<string>(_resources.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				EditorScene.CumulateReservedResources(list[i], _resources[list[i]]);
			}
		}
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x0008420C File Offset: 0x0008260C
	public static void CumulateReservedResources(Dictionary<string, ObscuredInt> _resources)
	{
		if (_resources.Count > 0)
		{
			List<string> list = new List<string>(_resources.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				EditorScene.CumulateReservedResources(list[i], _resources[list[i]]);
			}
		}
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x00084268 File Offset: 0x00082668
	public static void CumulateReservedResources(string _itemIdentifier, int _count)
	{
		if (EditorScene.m_reservedResources.ContainsKey(_itemIdentifier))
		{
			Dictionary<string, ObscuredInt> reservedResources;
			(reservedResources = EditorScene.m_reservedResources)[_itemIdentifier] = reservedResources[_itemIdentifier] + _count;
		}
		else
		{
			EditorScene.m_reservedResources.Add(_itemIdentifier, _count);
		}
		if (EditorScene.m_cumulateEditorItemsDelegate != null)
		{
			EditorScene.m_cumulateEditorItemsDelegate(_itemIdentifier, _count);
		}
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x000842D3 File Offset: 0x000826D3
	public void Reset()
	{
		this.Destroy();
		this.Load();
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x000842E1 File Offset: 0x000826E1
	public void Update()
	{
		this.mainTicker++;
		this.m_stateMachine.Update();
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x000842FC File Offset: 0x000826FC
	public void Destroy()
	{
		PsUIEditorItemSelector.m_activeTabIndex = 0;
		PsUIEditorItemSelector.m_tabPositions = null;
		PsState.m_activeGameLoop.DestroyMinigame();
		if (EditorScene.m_screenshotHandler != null)
		{
			Object.Destroy(EditorScene.m_screenshotCam.gameObject);
			Object.Destroy(EditorScene.m_screenshotHandler);
			EditorScene.m_screenshotHandler = null;
			EditorScene.m_storedScreenshots = null;
			EditorScene.m_screenshotCam = null;
		}
		this.m_stateMachine.Destroy();
		ResourcePool.UnloadResourceGroup(RESOURCE_GROUP.GAME);
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0008436C File Offset: 0x0008276C
	~EditorScene()
	{
	}

	// Token: 0x04001125 RID: 4389
	public string _name;

	// Token: 0x04001126 RID: 4390
	private StateMachine _stateMachine = new StateMachine();

	// Token: 0x04001127 RID: 4391
	private bool _initComplete;

	// Token: 0x04001128 RID: 4392
	private bool m_toTestState;

	// Token: 0x04001129 RID: 4393
	private const int MAX_STORED_SCREENSHOTS = 3;

	// Token: 0x0400112A RID: 4394
	public static GameObject m_screenshotHandler;

	// Token: 0x0400112B RID: 4395
	public static Screenshot[] m_storedScreenshots;

	// Token: 0x0400112C RID: 4396
	public static int m_nextScreenshotIndex;

	// Token: 0x0400112D RID: 4397
	public static Camera m_screenshotCam;

	// Token: 0x0400112E RID: 4398
	public static int m_cameraManShotDelay;

	// Token: 0x0400112F RID: 4399
	public static int m_cameraManIdleTimer;

	// Token: 0x04001130 RID: 4400
	private int mainTicker;

	// Token: 0x04001131 RID: 4401
	private static int m_manualScreenshotIndex;

	// Token: 0x04001132 RID: 4402
	public static Dictionary<string, ObscuredInt> m_reservedResources;

	// Token: 0x04001133 RID: 4403
	public static EditorScene.CumulateEditorItemsDelegate m_cumulateEditorItemsDelegate;

	// Token: 0x020001DC RID: 476
	// (Invoke) Token: 0x06000E3E RID: 3646
	public delegate void CumulateEditorItemsDelegate(string _itemIdentifier, int _count);
}
