using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class PsGame : IGame
{
	// Token: 0x06000C5A RID: 3162 RVA: 0x000753B0 File Offset: 0x000737B0
	public PsGame(string _projectCode)
	{
		if (Main.ShouldSetLowPerf() && string.IsNullOrEmpty(PlayerPrefsX.GetUserId()))
		{
			Debug.LogError("Lowend device: setting automated perf mode");
			PsState.m_perfMode = true;
			Main.SetPerfMode(true);
			PlayerPrefsX.SetLowEndPrompt(true);
		}
		FontInfoManager.AddInfo("OpenSans_Font", 10, 0.8f);
		Main.SetPerfMode(PsState.m_perfMode);
		QualitySettings.vSyncCount = 0;
		this.m_lastOutOfFocusTime = 0.0;
		this.m_sceneManager = new SceneManager();
		this.m_projectCode = _projectCode;
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0007543A File Offset: 0x0007383A
	// (set) Token: 0x06000C5C RID: 3164 RVA: 0x00075442 File Offset: 0x00073842
	public string m_projectCode
	{
		get
		{
			return this._projectCode;
		}
		set
		{
			this._projectCode = value;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0007544B File Offset: 0x0007384B
	// (set) Token: 0x06000C5E RID: 3166 RVA: 0x00075453 File Offset: 0x00073853
	public IScene m_currentScene
	{
		get
		{
			return this._currentScene;
		}
		set
		{
			this._currentScene = value;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0007545C File Offset: 0x0007385C
	// (set) Token: 0x06000C60 RID: 3168 RVA: 0x00075464 File Offset: 0x00073864
	public SceneManager m_sceneManager
	{
		get
		{
			return this._sceneManager;
		}
		set
		{
			this._sceneManager = value;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0007546D File Offset: 0x0007386D
	// (set) Token: 0x06000C62 RID: 3170 RVA: 0x00075475 File Offset: 0x00073875
	public IState m_currentState
	{
		get
		{
			return this._currentState;
		}
		set
		{
			this._currentState = value;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0007547E File Offset: 0x0007387E
	// (set) Token: 0x06000C64 RID: 3172 RVA: 0x00075486 File Offset: 0x00073886
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

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0007548F File Offset: 0x0007388F
	public bool PreloaderIsActive
	{
		get
		{
			return this._currentScene is PreloadingScene;
		}
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x000754A0 File Offset: 0x000738A0
	public void Initialize(IScene _scene)
	{
		PsState.SetLanguage(PlayerPrefsX.GetLanguage());
		Language language = PlayerPrefsX.GetLanguage();
		Debug.Initialize(false, true, false, false);
		FacebookManager.Initialize();
		EveryplayManager.SetEnabled(PsState.m_everyplayEnabled);
		EveryplayManager.Initialize();
		PsAdMediation.Initialize();
		PsMetrics.Initialize();
		PsMetrics.GameStart();
		this.m_editorReplaceShader = Shader.Find("WOE/Editor/UnitMaskReplaceShader");
		TextAsset textAsset = ResourceManager.GetTextAsset(RESOURCE.RandomLevelNamesBase_TextAsset);
		PsResources.LevelNameGeneratorBaseStrings = textAsset.text.Split(new string[] { "\r\n", "\n" }, 0);
		TextMeshS.Initialize();
		CacheManager.Initialize();
		PsCaches.Initialize();
		EntityManager.Initialize();
		TransformS.Initialize();
		PrefabS.Initialize();
		TouchAreaS.Initialize();
		TouchAreaS.Disable();
		TweenS.Initialize();
		TimerS.Initialize();
		SpriteS.Initialize();
		ProjectorS.Initialize();
		HttpS.Initialize();
		MonoBehaviourS.Initialize();
		CameraS.Initialize();
		SoundS.m_canPlaySounds = !PsState.m_muteSoundFX;
		NotificationManager.Initialize();
		PsGame.InitPhysics();
		PsS.Initialize();
		PsMetagameManager.Initialize();
		PsAchievementManager.Initialize();
		PsNonConsumableIAPs.Initialize();
		AutoGeometryLayer.ENABLE_AUTODECOS = PsState.m_enableAutodecos;
		DebugDraw.m_lineWidth = 3f;
		DebugDraw.Initialize();
		PsState.m_uiSheet = SpriteS.AddSpriteSheet(CameraS.m_uiCamera, ResourceManager.GetMaterial(RESOURCE.UiAtlasMat_Material), ResourceManager.GetTextAsset(RESOURCE.UiAtlas_TextAsset), 1f);
		this.m_sceneManager.ChangeScene(new PreloadingScene(_scene), null);
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x000755F4 File Offset: 0x000739F4
	public static void CreateSystemInfoBox()
	{
		PsGame.m_debugInfoCanvas = new UICanvas(null, false, "SystemInfoCanvas", null, string.Empty);
		PsGame.m_debugInfoCanvas.SetHeight(0.3f, RelativeTo.ScreenHeight);
		PsGame.m_debugInfoCanvas.SetWidth(0.2f, RelativeTo.ScreenWidth);
		PsGame.m_debugInfoCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupContentArea));
		PsGame.m_debugInfoCanvas.SetMargins(0.1f, 0.1f, 0.1f, 0.1f, RelativeTo.OwnHeight);
		PsGame.m_debugInfoCanvas.SetVerticalAlign(0f);
		PsGame.m_debugInfoCanvas.SetHorizontalAlign(1f);
		PsGame.m_debugInfoCanvas.SetDepthOffset(-100f);
		PsGame.m_debugInfoTextbox = new UITextbox(PsGame.m_debugInfoCanvas, false, "SystemInfo", string.Empty, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.012f, RelativeTo.ScreenShortest, true, Align.Center, Align.Top, null, true, null);
		PsGame.m_debugInfoTextbox.SetVerticalAlign(1f);
		PsGame.m_debugInfoCanvas.Update();
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x000756F1 File Offset: 0x00073AF1
	public static void UpdateSystemInfo()
	{
		if (Main.m_gameTicks % 60 == 0 && PsGame.m_debugInfoTextbox != null)
		{
			PsGame.m_debugInfoTextbox.SetText(Debug.GetSystemStats());
		}
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0007571C File Offset: 0x00073B1C
	public static void InitPhysics()
	{
		Array values = Enum.GetValues(typeof(PsCollisionType));
		ChipmunkProS.Initialize((int)values.GetValue(values.Length - 1));
		ChipmunkProWrapper.ucpSpaceSetCollisionSlop(2f);
		ChipmunkProWrapper.ucpSpaceSetSleepTimeThreshold(0.5f);
		ChipmunkProWrapper.ucpSpaceSetIdleSpeedThreshold(5f);
		ChipmunkProWrapper.ucpSpaceSetIterations(8);
		ChipmunkProWrapper.ucpSpaceSetGravity(PsState.m_defaultGravity);
		PsGlobalCollisionHandlers.Initialize();
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00075784 File Offset: 0x00073B84
	public void OnApplicationPause(bool _pauseStatus)
	{
		Debug.Log("OnApplicationPause: " + _pauseStatus, null);
		if (this.m_currentScene != null && !this.PreloaderIsActive && !PsState.m_inStartupSequence)
		{
			if (_pauseStatus)
			{
				this.m_lastOutOfFocusTime = Main.m_EPOCHSeconds;
				if (PsState.m_canAutoPause)
				{
					PsState.m_activeGameLoop.PauseMinigame();
					PsState.m_activeGameLoop.PauseElapsedTimer();
					if (this.m_currentScene.m_stateMachine != null)
					{
						this.m_currentScene.m_stateMachine.Update();
					}
					Debug.LogError("Game autopaused.");
				}
				CameraS.StoreTexture();
			}
			else
			{
				Teleport.m_forceRefresh = true;
				for (int i = 0; i < PsState.m_activeRenderTextures.Count; i++)
				{
					PsState.m_activeRenderTextures[i].ReleaseAndDestroyRenderTexture();
					PsState.m_activeRenderTextures[i].RecreateRenderTexture();
				}
				CameraS.ApplyTexture();
				this.m_lastOutOfFocusTime = Main.m_EPOCHSeconds - this.m_lastOutOfFocusTime;
				Debug.LogWarning("Check for need to do a new login...");
				bool flag = false;
				if (!ServerManager.m_hasAlertViewOnScreen && !PsState.m_inLoginFlow && !PsState.m_inIapFlow)
				{
					if (!ServerManager.m_playerAuthenticated)
					{
						flag = true;
					}
					else if (!PsState.m_adWatched)
					{
						flag = true;
					}
				}
				if (flag)
				{
					Debug.Log("back to app: RELOGGING", null);
					ServerManager.Login();
				}
				else
				{
					Debug.Log("back to app: NO NEED TO RELOG", null);
				}
				PsState.m_adWatched = false;
			}
		}
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x000758F6 File Offset: 0x00073CF6
	public void OnApplicationFocus(bool _focusStatus)
	{
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x000758F8 File Offset: 0x00073CF8
	public void LateUpdate()
	{
		if (PsState.m_activeMinigame != null)
		{
			if (PsState.m_activeMinigame.m_editing)
			{
				CameraS.RenderMainCameraToTexture(this.m_editorReplaceShader);
			}
			else
			{
				CameraS.ClearMainCameraRenderTexture();
			}
		}
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x00075928 File Offset: 0x00073D28
	public void RemoveComponent(IComponent _c)
	{
		ComponentType componentType = _c.m_componentType;
		if (componentType != (ComponentType)30)
		{
			if (componentType != (ComponentType)31)
			{
				if (componentType == (ComponentType)32)
				{
					PsS.RemoveGround(_c as GroundC);
				}
			}
			else
			{
				PsS.RemoveCustomObject(_c as CustomObjectC);
			}
		}
		else
		{
			PsS.RemoveUnit(_c as UnitC);
		}
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0007598C File Offset: 0x00073D8C
	public void Update()
	{
		float num = Main.m_dt * Main.m_timeScale;
		double num2 = Main.m_dtD * Main.m_timeScaleD;
		CameraS.ClearMainCameraRenderTexture();
		TouchAreaS.Update();
		for (int i = 0; i < Main.m_ticksPerFrame; i++)
		{
			Main.m_gameDeltaTime = num;
			Main.m_gameTicks++;
			Main.m_gameTimeSinceAppStarted += num2;
			Main.m_resettingGameTime += num;
			ScreenCapture.Update();
			TweenS.Update();
			this.m_sceneManager.UpdateLogic();
			UIManager.Update();
			PsMetagameManager.Update();
			PsBackgroundDownloader.Update();
			NotificationManager.Update();
			ChipmunkProS.Update((!PsState.m_physicsPaused) ? num : 0f);
			LevelManager.Update();
			PsS.Update();
			MonoBehaviourS.Update();
			EntityManager.UpdateLogic();
			AutoGeometryManager.Update();
			TransformS.Update();
			SpriteS.Update();
			PrefabS.Update();
			TextMeshS.Update();
			ProjectorS.Update();
			CameraS.Update();
			SoundS.Update();
			HttpS.Update();
			TimerS.Update();
			TweenS.LateUpdate();
			CacheManager.Update();
			EntityManager.Update();
			GizmoManager.UpdatePosition();
			EveryplayManager.Update();
			PsGame.UpdateSystemInfo();
			CameraS.UpdateBlur();
		}
	}

	// Token: 0x04000B73 RID: 2931
	private string _projectCode;

	// Token: 0x04000B74 RID: 2932
	private IScene _currentScene;

	// Token: 0x04000B75 RID: 2933
	private SceneManager _sceneManager;

	// Token: 0x04000B76 RID: 2934
	private IState _currentState;

	// Token: 0x04000B77 RID: 2935
	private StateMachine _stateMachine;

	// Token: 0x04000B78 RID: 2936
	private float m_delta;

	// Token: 0x04000B79 RID: 2937
	private float m_cumulatedFrameTime;

	// Token: 0x04000B7A RID: 2938
	private double m_lastOutOfFocusTime;

	// Token: 0x04000B7B RID: 2939
	private Shader m_editorReplaceShader;

	// Token: 0x04000B7C RID: 2940
	private static UICanvas m_debugInfoCanvas;

	// Token: 0x04000B7D RID: 2941
	private static UITextbox m_debugInfoTextbox;
}
