using System;
using UnityEngine;

// Token: 0x02000229 RID: 553
public class PsMenuScene : IScene, IStatedObject
{
	// Token: 0x06001025 RID: 4133 RVA: 0x00096BBC File Offset: 0x00094FBC
	public PsMenuScene(string _sceneName, bool _restart = false)
	{
		PsState.m_UIComponentsHidden = false;
		this.m_name = _sceneName;
		if (_restart)
		{
			PsMenuScene.m_lastState = null;
		}
		if (PsMenuScene.m_lastState == null)
		{
			PsMenuScene.m_menuMusicState = 0;
		}
		else
		{
			PsMenuScene.m_menuMusicState = Random.Range(1, 3);
		}
		this.m_utilityEntity = EntityManager.AddEntity("MenuSceneUtilityEntity");
		this.m_utilityEntity.m_persistent = true;
		this.m_utilityTC = TransformS.AddComponent(this.m_utilityEntity, "MenuSceneUtilityTC");
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06001026 RID: 4134 RVA: 0x00096C3B File Offset: 0x0009503B
	// (set) Token: 0x06001027 RID: 4135 RVA: 0x00096C43 File Offset: 0x00095043
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

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06001028 RID: 4136 RVA: 0x00096C4C File Offset: 0x0009504C
	// (set) Token: 0x06001029 RID: 4137 RVA: 0x00096C54 File Offset: 0x00095054
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

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x0600102A RID: 4138 RVA: 0x00096C5D File Offset: 0x0009505D
	public bool m_initComplete
	{
		get
		{
			return this._initComplete;
		}
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x00096C65 File Offset: 0x00095065
	public void Load()
	{
		NotificationManager.Resume();
		this.Initialize();
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x00096C72 File Offset: 0x00095072
	public IState GetCurrentState()
	{
		if (this.m_stateMachine != null)
		{
			return this.m_stateMachine.GetCurrentState();
		}
		return null;
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x00096C8C File Offset: 0x0009508C
	public void Reset()
	{
		this.Destroy();
		this.Load();
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x00096C9A File Offset: 0x0009509A
	public void Update()
	{
		this.m_stateMachine.Update();
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x00096CA8 File Offset: 0x000950A8
	public void Destroy()
	{
		EntityManager.RemoveEntity(this.m_utilityEntity);
		this.m_utilityEntity = null;
		this.m_utilityTC = null;
		this.m_stateMachine.Destroy();
		PsPlanetManager.GetCurrentPlanet().DestroySpaceScene();
		CameraS.m_mainCamera.enabled = true;
		ResourcePool.UnloadResourceGroup(RESOURCE_GROUP.MENU);
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x00096CF4 File Offset: 0x000950F4
	public void Initialize()
	{
		if (PlayerPrefsX.GetTeamId() != null && !PsMetagameManager.m_loadingTeam)
		{
			PsMetagameManager.GetOwnTeam(delegate(TeamData c)
			{
				Debug.Log("Own team loaded", null);
			}, true);
		}
		SoundS.RemoveAllSounds();
		RenderSettings.ambientLight = DebugDraw.GetColor(255f, 255f, 255f);
		PsPlanetManager.GetCurrentPlanet().CreateSpaceScene();
		this.UpdateMusic();
		this.m_stateMachine = new StateMachine(this);
		IState state;
		if (PsMenuScene.m_lastState != null)
		{
			object[] array = new object[0];
			state = Activator.CreateInstance(Type.GetType(PsMenuScene.m_lastState), array) as IState;
		}
		else if (PsMenuScene.m_lastIState != null)
		{
			state = PsMenuScene.m_lastIState;
		}
		else
		{
			state = new PsMainMenuState();
		}
		this._initComplete = true;
		this.m_stateMachine.ChangeState(state);
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x00096DCC File Offset: 0x000951CC
	public void UpdateMusic()
	{
		if (!PsState.m_muteMusic)
		{
			if (this.m_music == null)
			{
				this.m_music = SoundS.AddComponent(this.m_utilityTC, "/Music/PlanetMusic", 1f, true);
				SoundS.PlaySound(this.m_music, true);
				SoundS.SetSoundParameter(this.m_music, "MusicState", (float)PsMenuScene.m_menuMusicState);
			}
			else
			{
				SoundS.ResumeSound(this.m_music);
			}
		}
		else if (this.m_music != null)
		{
			SoundS.PauseSound(this.m_music);
		}
	}

	// Token: 0x040012F1 RID: 4849
	private string _name;

	// Token: 0x040012F2 RID: 4850
	private StateMachine _stateMachine;

	// Token: 0x040012F3 RID: 4851
	private bool _initComplete;

	// Token: 0x040012F4 RID: 4852
	private Entity m_utilityEntity;

	// Token: 0x040012F5 RID: 4853
	private TransformC m_utilityTC;

	// Token: 0x040012F6 RID: 4854
	private SoundC m_music;

	// Token: 0x040012F7 RID: 4855
	private static int m_menuMusicState;

	// Token: 0x040012F8 RID: 4856
	public static string m_lastState;

	// Token: 0x040012F9 RID: 4857
	public static IState m_lastIState;
}
