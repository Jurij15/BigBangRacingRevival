using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class PsBlackScene : IScene, IStatedObject
{
	// Token: 0x06001013 RID: 4115 RVA: 0x000968E2 File Offset: 0x00094CE2
	public PsBlackScene(string _sceneName, bool _restart = false)
	{
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06001014 RID: 4116 RVA: 0x000968EA File Offset: 0x00094CEA
	// (set) Token: 0x06001015 RID: 4117 RVA: 0x000968F2 File Offset: 0x00094CF2
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

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06001016 RID: 4118 RVA: 0x000968FB File Offset: 0x00094CFB
	// (set) Token: 0x06001017 RID: 4119 RVA: 0x00096903 File Offset: 0x00094D03
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

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06001018 RID: 4120 RVA: 0x0009690C File Offset: 0x00094D0C
	public bool m_initComplete
	{
		get
		{
			return this._initComplete;
		}
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x00096914 File Offset: 0x00094D14
	public void Load()
	{
		this.Initialize();
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x0009691C File Offset: 0x00094D1C
	public IState GetCurrentState()
	{
		if (this.m_stateMachine != null)
		{
			return this.m_stateMachine.GetCurrentState();
		}
		return null;
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x00096936 File Offset: 0x00094D36
	public void Reset()
	{
		this.Destroy();
		this.Load();
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x00096944 File Offset: 0x00094D44
	public void Update()
	{
		if (this.m_counter == 60)
		{
			return;
		}
		this.m_counter++;
		if (this.m_counter == 60)
		{
			Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLoadingScene(Color.black, true, 0.25f));
		}
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x000969A4 File Offset: 0x00094DA4
	public void Destroy()
	{
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x000969A6 File Offset: 0x00094DA6
	public void Initialize()
	{
		this._initComplete = true;
	}

	// Token: 0x040012EB RID: 4843
	private string _name;

	// Token: 0x040012EC RID: 4844
	private StateMachine _stateMachine;

	// Token: 0x040012ED RID: 4845
	private bool _initComplete;

	// Token: 0x040012EE RID: 4846
	private int m_counter;
}
