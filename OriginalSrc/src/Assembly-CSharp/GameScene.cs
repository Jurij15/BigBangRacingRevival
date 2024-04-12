using System;
using UnityEngine;

// Token: 0x0200020A RID: 522
public class GameScene : IScene, IStatedObject
{
	// Token: 0x06000F21 RID: 3873 RVA: 0x000906ED File Offset: 0x0008EAED
	public GameScene(string _sceneName, Type _returnSceneType = null)
	{
		PsState.m_UIComponentsHidden = false;
		GameScene.m_returnSceneType = _returnSceneType;
		this.m_name = _sceneName;
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00090713 File Offset: 0x0008EB13
	// (set) Token: 0x06000F23 RID: 3875 RVA: 0x0009071B File Offset: 0x0008EB1B
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

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00090724 File Offset: 0x0008EB24
	// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0009072C File Offset: 0x0008EB2C
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

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00090735 File Offset: 0x0008EB35
	public bool m_initComplete
	{
		get
		{
			return this._initComplete;
		}
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x0009073D File Offset: 0x0008EB3D
	public void Load()
	{
		GameScene.m_perfTickReset = 0;
		GameScene.m_perfTicks = 0;
		GameScene.m_lowPerformance = false;
		this.Initialize();
		EveryplayManager.ClearRecording();
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x0009075C File Offset: 0x0008EB5C
	public static void InitGame()
	{
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0009075E File Offset: 0x0008EB5E
	public static void ResetGame()
	{
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x00090760 File Offset: 0x0008EB60
	public IState GetCurrentState()
	{
		if (this.m_stateMachine != null)
		{
			return this.m_stateMachine.GetCurrentState();
		}
		return null;
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x0009077A File Offset: 0x0008EB7A
	public void Reset()
	{
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x0009077C File Offset: 0x0008EB7C
	public void Initialize()
	{
		if (PsState.m_activeGameLoop != null)
		{
			PsState.m_activeGameLoop.EnteredMinigameScene();
		}
		this._initComplete = true;
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x0009079C File Offset: 0x0008EB9C
	public void Update()
	{
		GameLevelPreview.StepLevelPreview();
		if (!PlayerPrefsX.GetPerfMode())
		{
			if (Time.deltaTime > 0.02f)
			{
				if (GameScene.m_perfTickReset > 0)
				{
					GameScene.m_perfTickReset--;
				}
				GameScene.m_perfTicks++;
				if (GameScene.m_perfTicks >= 600)
				{
					GameScene.m_lowPerformance = true;
				}
			}
			else if (GameScene.m_perfTicks > 0)
			{
				GameScene.m_perfTickReset++;
				if (GameScene.m_perfTickReset >= 900)
				{
					GameScene.m_perfTicks = 0;
					GameScene.m_perfTickReset = 0;
				}
			}
			else
			{
				GameScene.m_perfTicks = 0;
				GameScene.m_perfTickReset = 0;
			}
		}
		this.mainTicker++;
		this.m_stateMachine.Update();
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x00090860 File Offset: 0x0008EC60
	public void Destroy()
	{
		PsState.m_activeGameLoop.DestroyMinigame();
		PsIngameMenu.CloseAll();
		PsMetagameManager.HideResources();
		this.m_stateMachine.Destroy();
		ResourcePool.UnloadResourceGroup(RESOURCE_GROUP.GAME);
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00090888 File Offset: 0x0008EC88
	~GameScene()
	{
	}

	// Token: 0x040011FF RID: 4607
	private string _name;

	// Token: 0x04001200 RID: 4608
	private StateMachine _stateMachine = new StateMachine();

	// Token: 0x04001201 RID: 4609
	private bool _initComplete;

	// Token: 0x04001202 RID: 4610
	public static int m_perfTickReset;

	// Token: 0x04001203 RID: 4611
	public static int m_perfTicks;

	// Token: 0x04001204 RID: 4612
	public static bool m_lowPerformance;

	// Token: 0x04001205 RID: 4613
	public static Type m_returnSceneType;

	// Token: 0x04001206 RID: 4614
	private int mainTicker;

	// Token: 0x04001207 RID: 4615
	public static bool m_resetting;
}
