using System;
using UnityEngine;

// Token: 0x020005B6 RID: 1462
public class BasicLoadingScene : ILoadingScene
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06002AAC RID: 10924 RVA: 0x001BAB88 File Offset: 0x001B8F88
	// (set) Token: 0x06002AAD RID: 10925 RVA: 0x001BAB90 File Offset: 0x001B8F90
	public StateMachine StateMachine
	{
		get
		{
			return this.m_stateMachine;
		}
		set
		{
			this.m_stateMachine = value;
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06002AAE RID: 10926 RVA: 0x001BAB99 File Offset: 0x001B8F99
	// (set) Token: 0x06002AAF RID: 10927 RVA: 0x001BABA1 File Offset: 0x001B8FA1
	public IScene FromScene
	{
		get
		{
			return this.m_fromScene;
		}
		set
		{
			this.m_fromScene = value;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x001BABAA File Offset: 0x001B8FAA
	// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x001BABB2 File Offset: 0x001B8FB2
	public IScene ToScene
	{
		get
		{
			return this.m_toScene;
		}
		set
		{
			this.m_toScene = value;
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x001BABBB File Offset: 0x001B8FBB
	public bool InitComplete
	{
		get
		{
			return this.m_initComplete;
		}
	}

	// Token: 0x06002AB3 RID: 10931 RVA: 0x001BABC3 File Offset: 0x001B8FC3
	private void StartIntro()
	{
		Debug.LogInfo("LOADING SCREEN: Intro started");
		this.m_initComplete = true;
		this.m_introStarted = true;
		this.sticker = this.ticker;
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x001BABE9 File Offset: 0x001B8FE9
	public void StartOutro()
	{
		this.m_outroStarted = true;
		this.sticker = this.ticker;
		Debug.LogInfo("LOADING SCREEN: Outro started");
	}

	// Token: 0x06002AB5 RID: 10933 RVA: 0x001BAC08 File Offset: 0x001B9008
	public void Load()
	{
		Debug.LogInfo("LOADING SCREEN: Loading assets");
		this.Initialize();
	}

	// Token: 0x06002AB6 RID: 10934 RVA: 0x001BAC1C File Offset: 0x001B901C
	public void Initialize()
	{
		this.m_camera = CameraS.AddCamera("Loading Screen Camera", true, 3);
		this.m_prefab = Object.Instantiate<GameObject>(Resources.Load("LoadScreenPrefab") as GameObject);
		this.m_prefab.layer = this.m_camera.gameObject.layer;
		this.m_prefab.transform.localScale = new Vector3(1f, 1f, 1f) * (float)Screen.height * 0.02f;
		this.m_prefab.transform.position = this.m_camera.transform.position + new Vector3(500f, 0f, 100f);
		this.StartIntro();
	}

	// Token: 0x06002AB7 RID: 10935 RVA: 0x001BACE8 File Offset: 0x001B90E8
	public void Update()
	{
		this.ticker++;
		if (!this.m_introComplete && this.m_introStarted)
		{
			float num = 1f - ToolBox.getPositionBetween((float)this.ticker, (float)this.sticker, (float)(this.sticker + 30));
			this.m_prefab.transform.position = this.m_camera.transform.position + new Vector3(1000f * num, 0f, 100f);
			if (num == 0f)
			{
				this.m_introComplete = true;
			}
		}
		if (!this.m_outroComplete && this.m_outroStarted)
		{
			float positionBetween = ToolBox.getPositionBetween((float)this.ticker, (float)this.sticker, (float)(this.sticker + 30));
			this.m_prefab.transform.position = this.m_camera.transform.position + new Vector3(1000f * positionBetween, 0f, 100f);
			if (positionBetween == 1f)
			{
				this.m_outroComplete = true;
			}
		}
		this.m_prefab.transform.Rotate(new Vector3(0f, 10f, 0f));
	}

	// Token: 0x06002AB8 RID: 10936 RVA: 0x001BAE2F File Offset: 0x001B922F
	public void Destroy()
	{
		CameraS.RemoveCamera(this.m_camera);
		Object.DestroyImmediate(this.m_prefab);
		Resources.UnloadAsset(this.m_prefab);
		Debug.LogInfo("LOADING SCREEN: Destroy");
	}

	// Token: 0x06002AB9 RID: 10937 RVA: 0x001BAE5C File Offset: 0x001B925C
	public bool IntroComplete()
	{
		return this.m_introComplete;
	}

	// Token: 0x06002ABA RID: 10938 RVA: 0x001BAE64 File Offset: 0x001B9264
	public bool OutroComplete()
	{
		return this.m_outroComplete;
	}

	// Token: 0x06002ABB RID: 10939 RVA: 0x001BAE6C File Offset: 0x001B926C
	~BasicLoadingScene()
	{
		Debug.Log(this + ": Memory Freed", null);
	}

	// Token: 0x04002FDA RID: 12250
	private StateMachine m_stateMachine;

	// Token: 0x04002FDB RID: 12251
	private IScene m_fromScene;

	// Token: 0x04002FDC RID: 12252
	private IScene m_toScene;

	// Token: 0x04002FDD RID: 12253
	private bool m_initComplete;

	// Token: 0x04002FDE RID: 12254
	private bool m_introComplete;

	// Token: 0x04002FDF RID: 12255
	private bool m_outroComplete;

	// Token: 0x04002FE0 RID: 12256
	private bool m_introStarted;

	// Token: 0x04002FE1 RID: 12257
	private bool m_outroStarted;

	// Token: 0x04002FE2 RID: 12258
	public Camera m_camera;

	// Token: 0x04002FE3 RID: 12259
	public GameObject m_prefab;

	// Token: 0x04002FE4 RID: 12260
	private int sticker;

	// Token: 0x04002FE5 RID: 12261
	private int ticker;
}
