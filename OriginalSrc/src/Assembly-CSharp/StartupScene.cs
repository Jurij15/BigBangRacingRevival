using System;

// Token: 0x02000289 RID: 649
public class StartupScene : IScene, IStatedObject
{
	// Token: 0x06001376 RID: 4982 RVA: 0x000C2C3C File Offset: 0x000C103C
	public StartupScene(string _sceneName)
	{
		this.m_name = _sceneName;
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06001377 RID: 4983 RVA: 0x000C2C4B File Offset: 0x000C104B
	// (set) Token: 0x06001378 RID: 4984 RVA: 0x000C2C53 File Offset: 0x000C1053
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

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06001379 RID: 4985 RVA: 0x000C2C5C File Offset: 0x000C105C
	// (set) Token: 0x0600137A RID: 4986 RVA: 0x000C2C64 File Offset: 0x000C1064
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

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x0600137B RID: 4987 RVA: 0x000C2C6D File Offset: 0x000C106D
	public bool m_initComplete
	{
		get
		{
			return this._initComplete;
		}
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x000C2C75 File Offset: 0x000C1075
	public IState GetCurrentState()
	{
		if (this.m_stateMachine != null)
		{
			return this.m_stateMachine.GetCurrentState();
		}
		return null;
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x000C2C90 File Offset: 0x000C1090
	public void Load()
	{
		if (PsState.m_activeGameLoop != null)
		{
			PsState.m_activeGameLoop.CleanLoop();
		}
		PsState.m_activeGameLoop = null;
		PsState.m_activeMinigame = null;
		PsState.m_UIComponentsHidden = false;
		for (int i = PsState.m_openPopups.Count - 1; i >= 0; i--)
		{
			PsState.m_openPopups[i].Destroy();
		}
		for (int j = PsState.m_dialogues.Count - 1; j >= 0; j--)
		{
			PsState.m_dialogues[j].Destroy();
		}
		PsState.m_activeRenderTextures.Clear();
		ServerManager.m_connectingPopup = null;
		CameraS.RemoveBlur();
		this.Initialize();
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x000C2D38 File Offset: 0x000C1138
	public void Initialize()
	{
		PsState.m_restarting = false;
		if (PlayerPrefsX.GetUserId() != null && PlayerPrefsX.GetUserName() != null)
		{
			this.m_loadingText = new PsUILoadingText(null);
			this.m_loadingText.Update();
		}
		this.m_stateMachine = new StateMachine(this);
		this.m_stateMachine.ChangeState(new UserLoginState());
		this._initComplete = true;
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x000C2D99 File Offset: 0x000C1199
	public void Reset()
	{
		this.Destroy();
		this.Load();
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x000C2DA7 File Offset: 0x000C11A7
	public void Update()
	{
		this.m_stateMachine.Update();
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x000C2DB4 File Offset: 0x000C11B4
	public void Destroy()
	{
		PsMetagameManager.DestroySplashBG();
		if (this.m_loadingText != null)
		{
			this.m_loadingText.Destroy();
			this.m_loadingText = null;
		}
		this.m_stateMachine.Destroy();
		EntityManager.RemoveAllEntities();
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000C2DE8 File Offset: 0x000C11E8
	~StartupScene()
	{
	}

	// Token: 0x04001663 RID: 5731
	private string _name;

	// Token: 0x04001664 RID: 5732
	private StateMachine _stateMachine;

	// Token: 0x04001665 RID: 5733
	private bool _initComplete;

	// Token: 0x04001666 RID: 5734
	private PsUILoadingText m_loadingText;
}
