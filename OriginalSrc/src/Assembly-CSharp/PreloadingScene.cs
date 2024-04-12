using System;

// Token: 0x02000224 RID: 548
public class PreloadingScene : IScene, IStatedObject
{
	// Token: 0x06000FDB RID: 4059 RVA: 0x00095F46 File Offset: 0x00094346
	public PreloadingScene(IScene _toScene)
	{
		this.m_toScene = _toScene;
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00095F60 File Offset: 0x00094360
	// (set) Token: 0x06000FDD RID: 4061 RVA: 0x00095F68 File Offset: 0x00094368
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

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00095F71 File Offset: 0x00094371
	// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00095F79 File Offset: 0x00094379
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

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00095F82 File Offset: 0x00094382
	public bool m_initComplete
	{
		get
		{
			return this._initComplete;
		}
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x00095F8A File Offset: 0x0009438A
	public IState GetCurrentState()
	{
		if (this.m_stateMachine != null)
		{
			return this.m_stateMachine.GetCurrentState();
		}
		return null;
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x00095FA4 File Offset: 0x000943A4
	public void Load()
	{
		PsState.m_UIComponentsHidden = false;
		this.Initialize();
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x00095FB2 File Offset: 0x000943B2
	public void Initialize()
	{
		PsMetagameManager.CreateSplashBG();
		this.m_stateMachine = new StateMachine(this);
		this.m_stateMachine.ChangeState(new PsUIBaseState(typeof(PsUIPreloaderCenter), null, null, null, false, InitialPage.Center));
		this._initComplete = true;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00095FEB File Offset: 0x000943EB
	public void NextScene()
	{
		SoundS.Initialize(CameraS.m_mainCamera.gameObject);
		Main.m_currentGame.m_sceneManager.ChangeScene(this.m_toScene, null);
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00096012 File Offset: 0x00094412
	public void Reset()
	{
		this.Destroy();
		this.Load();
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x00096020 File Offset: 0x00094420
	public void Update()
	{
		this.m_stateMachine.Update();
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0009602D File Offset: 0x0009442D
	public void Destroy()
	{
		this.m_stateMachine.Destroy();
		EntityManager.RemoveAllEntities();
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x00096040 File Offset: 0x00094440
	~PreloadingScene()
	{
	}

	// Token: 0x040012C0 RID: 4800
	private string _name = "PreloadingScene";

	// Token: 0x040012C1 RID: 4801
	private StateMachine _stateMachine;

	// Token: 0x040012C2 RID: 4802
	private bool _initComplete;

	// Token: 0x040012C3 RID: 4803
	private IScene m_toScene;
}
