using System;
using UnityEngine;

// Token: 0x020005B4 RID: 1460
public class FrameworkTestGame : IGame
{
	// Token: 0x06002A93 RID: 10899 RVA: 0x001BA6D8 File Offset: 0x001B8AD8
	public FrameworkTestGame(string _projectCode)
	{
		this.m_sceneManager = new SceneManager();
		Application.targetFrameRate = 60;
		this.m_projectCode = _projectCode;
		Debug.Initialize(true, true, true, false);
		EntityManager.Initialize();
		TransformS.Initialize();
		PrefabS.Initialize();
		TouchAreaS.Initialize();
		TweenS.Initialize();
		SpriteS.Initialize();
		CameraS.Initialize();
		SoundS.Initialize(CameraS.m_mainCamera.gameObject);
		Array values = Enum.GetValues(typeof(ucpCollisionType));
		ChipmunkProS.Initialize((int)values.GetValue(values.Length - 1));
		DebugDraw.Initialize();
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06002A94 RID: 10900 RVA: 0x001BA76C File Offset: 0x001B8B6C
	// (set) Token: 0x06002A95 RID: 10901 RVA: 0x001BA774 File Offset: 0x001B8B74
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

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06002A96 RID: 10902 RVA: 0x001BA77D File Offset: 0x001B8B7D
	// (set) Token: 0x06002A97 RID: 10903 RVA: 0x001BA785 File Offset: 0x001B8B85
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

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06002A98 RID: 10904 RVA: 0x001BA78E File Offset: 0x001B8B8E
	// (set) Token: 0x06002A99 RID: 10905 RVA: 0x001BA796 File Offset: 0x001B8B96
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

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06002A9A RID: 10906 RVA: 0x001BA79F File Offset: 0x001B8B9F
	// (set) Token: 0x06002A9B RID: 10907 RVA: 0x001BA7A7 File Offset: 0x001B8BA7
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

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06002A9C RID: 10908 RVA: 0x001BA7B0 File Offset: 0x001B8BB0
	// (set) Token: 0x06002A9D RID: 10909 RVA: 0x001BA7B8 File Offset: 0x001B8BB8
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

	// Token: 0x06002A9E RID: 10910 RVA: 0x001BA7C1 File Offset: 0x001B8BC1
	public void Initialize(IScene _scene)
	{
		this.m_sceneManager.ChangeScene(_scene, new BasicLoadingScene());
	}

	// Token: 0x06002A9F RID: 10911 RVA: 0x001BA7D4 File Offset: 0x001B8BD4
	public void RemoveComponent(IComponent _c)
	{
	}

	// Token: 0x06002AA0 RID: 10912 RVA: 0x001BA7D6 File Offset: 0x001B8BD6
	public void OnApplicationPause(bool _pauseStatus)
	{
	}

	// Token: 0x06002AA1 RID: 10913 RVA: 0x001BA7D8 File Offset: 0x001B8BD8
	public void OnApplicationFocus(bool _focusStatus)
	{
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x001BA7DA File Offset: 0x001B8BDA
	public void LateUpdate()
	{
	}

	// Token: 0x06002AA3 RID: 10915 RVA: 0x001BA7DC File Offset: 0x001B8BDC
	public void Update()
	{
		this.m_currentTime = Time.time;
		Main.m_gameDeltaTime = FrameworkTestGame.m_dt;
		this.m_prevTime = this.m_currentTime;
		Main.m_gameTimeSinceAppStarted += (double)FrameworkTestGame.m_dt;
		TouchAreaS.Update();
		LevelManager.Update();
		this.m_sceneManager.UpdateLogic();
		ChipmunkProS.Update(FrameworkTestGame.m_dt);
		TweenS.Update();
		TransformS.Update();
		SpriteS.Update();
		PrefabS.Update();
		CameraS.Update();
		UIManager.Update();
		EntityManager.Update();
	}

	// Token: 0x04002FC6 RID: 12230
	private string _projectCode;

	// Token: 0x04002FC7 RID: 12231
	private IScene _currentScene;

	// Token: 0x04002FC8 RID: 12232
	private SceneManager _sceneManager;

	// Token: 0x04002FC9 RID: 12233
	private IState _currentState;

	// Token: 0x04002FCA RID: 12234
	private StateMachine _stateMachine;

	// Token: 0x04002FCB RID: 12235
	private float m_prevTime;

	// Token: 0x04002FCC RID: 12236
	private float m_currentTime;

	// Token: 0x04002FCD RID: 12237
	private float m_delta;

	// Token: 0x04002FCE RID: 12238
	private float m_cumulatedFrameTime;

	// Token: 0x04002FCF RID: 12239
	private static float m_dt = 0.016666668f;
}
