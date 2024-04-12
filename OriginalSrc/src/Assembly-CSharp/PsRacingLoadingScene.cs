using System;
using UnityEngine;

// Token: 0x02000225 RID: 549
public class PsRacingLoadingScene : ILoadingScene, IStatedObject
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00096074 File Offset: 0x00094474
	// (set) Token: 0x06000FEB RID: 4075 RVA: 0x0009607C File Offset: 0x0009447C
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

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00096085 File Offset: 0x00094485
	// (set) Token: 0x06000FED RID: 4077 RVA: 0x0009608D File Offset: 0x0009448D
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

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00096096 File Offset: 0x00094496
	// (set) Token: 0x06000FEF RID: 4079 RVA: 0x0009609E File Offset: 0x0009449E
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

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x000960A7 File Offset: 0x000944A7
	public bool InitComplete
	{
		get
		{
			return this.m_initComplete;
		}
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x000960AF File Offset: 0x000944AF
	private void StartIntro()
	{
		this.m_introStarted = true;
		TouchAreaS.Enable();
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x000960BD File Offset: 0x000944BD
	public void StartOutro()
	{
		this.m_outroStarted = true;
		CameraS.BringToFront(this.m_camera, true);
		this.m_state.BringToFront();
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x000960DD File Offset: 0x000944DD
	public void Load()
	{
		this.Initialize();
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000960E8 File Offset: 0x000944E8
	public void Initialize()
	{
		this.m_camera = CameraS.AddCamera("Loading Screen Camera", true, 3);
		this.m_entity = EntityManager.AddEntity("LoadingScreenEntity");
		this.m_entity.m_persistent = true;
		this.m_TC = TransformS.AddComponent(this.m_entity, "LoadingScreenTC", Vector3.forward * -240f);
		this.m_stateMachine = new StateMachine(this);
		this.m_state = new PsUIBaseState(PsState.m_activeGameLoop.GetLoadingScreenComponent(), null, null, null, false, InitialPage.Center);
		this.m_state.SetAction("StartGame", new Action(this.StartGame));
		this.m_state.SetAction("Exit", new Action(this.Exit));
		this.m_darkenSpriteSheet = SpriteS.AddSpriteSheet(this.m_camera, ResourceManager.GetMaterial(RESOURCE.LoadingScreenFadeMat_Material), 1f);
		SpriteC spriteC = SpriteS.AddComponent(this.m_TC, new Frame(0f, 0f, (float)Screen.width, (float)Screen.height), this.m_darkenSpriteSheet);
		SpriteS.SetColor(spriteC, new Color(0f, 0f, 0f, 0f));
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.zero, new Vector3(0.5f, 0.5f, 0.5f), 0.2f, 0f, true);
		this.m_stateMachine.ChangeState(this.m_state);
		this.m_state.SetCreateDelegate(delegate
		{
			EntityManager.RemoveAllTagsFromEntity(this.m_state.m_baseCanvas.m_TC.p_entity, false);
		});
		this.m_initComplete = true;
		this.m_introComplete = PsState.m_activeGameLoop.m_minigameBytes != null;
		this.StartIntro();
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0009628F File Offset: 0x0009468F
	public void LevelLoaded()
	{
		if (this.m_screenShotTweened)
		{
			this.m_introComplete = true;
		}
		else
		{
			this.m_screenshotTweenAction = delegate
			{
				this.m_introComplete = true;
			};
		}
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x000962BC File Offset: 0x000946BC
	private void StartGame()
	{
		TouchAreaS.Disable();
		if (PsUrlLaunch.IsWatingLevelLoad())
		{
			PsUrlLaunch.CreatePopup(new Action(PsState.m_activeGameLoop.EnterMinigame));
		}
		else
		{
			PsState.m_activeGameLoop.EnterMinigame();
		}
		this.m_fadeOut = true;
		CameraS.RemoveBlur();
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, new Vector3(0.5f, 0.5f, 0.5f), Vector3.zero, 0.2f, 0f, true);
		TimerS.AddComponent(EntityManager.AddEntity(), string.Empty, 0.25f, 0f, true, delegate(TimerC c)
		{
			TouchAreaS.Enable();
			this.m_outroComplete = true;
		});
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x00096362 File Offset: 0x00094762
	private void Exit()
	{
		PsState.m_activeGameLoop.ExitLoadingScene();
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0009636E File Offset: 0x0009476E
	public void Update()
	{
		if (this.m_screenShotTweened && this.m_screenshotTweenAction != null)
		{
			this.m_screenshotTweenAction.Invoke();
			this.m_screenshotTweenAction = null;
		}
		this.m_stateMachine.Update();
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x000963A3 File Offset: 0x000947A3
	public bool IntroComplete()
	{
		return this.m_introComplete;
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x000963AB File Offset: 0x000947AB
	public bool OutroComplete()
	{
		return this.m_outroComplete;
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x000963B4 File Offset: 0x000947B4
	public void Destroy()
	{
		Debug.Log("LEVEL LOADING SCENE DESTROY", null);
		CameraS.RemoveCamera(this.m_camera);
		this.m_camera = null;
		if (this.m_darkenSpriteSheet != null)
		{
			SpriteS.RemoveSpriteSheet(this.m_darkenSpriteSheet);
			this.m_darkenSpriteSheet = null;
		}
		if (this.m_sheet != null)
		{
			SpriteS.RemoveSpriteSheet(this.m_sheet);
			this.m_sheet = null;
		}
		if (this.m_material != null)
		{
			Object.Destroy(this.m_material);
			this.m_material = null;
		}
		if (this.m_texture != null)
		{
			Object.Destroy(this.m_texture);
			this.m_texture = null;
		}
		if (this.m_background != null)
		{
			this.m_background.Destroy();
			this.m_background = null;
		}
		EntityManager.RemoveEntity(this.m_entity);
		this.m_entity = null;
		TransformS.RemoveComponent(this.m_TC);
		this.m_TC = null;
		this.m_stateMachine.Destroy();
		this.m_stateMachine = null;
	}

	// Token: 0x040012C4 RID: 4804
	private StateMachine m_stateMachine;

	// Token: 0x040012C5 RID: 4805
	private IScene m_fromScene;

	// Token: 0x040012C6 RID: 4806
	private IScene m_toScene;

	// Token: 0x040012C7 RID: 4807
	private bool m_initComplete;

	// Token: 0x040012C8 RID: 4808
	private bool m_introComplete;

	// Token: 0x040012C9 RID: 4809
	private bool m_outroComplete;

	// Token: 0x040012CA RID: 4810
	private bool m_introStarted;

	// Token: 0x040012CB RID: 4811
	public bool m_outroStarted;

	// Token: 0x040012CC RID: 4812
	public bool m_screenShotTweened;

	// Token: 0x040012CD RID: 4813
	public Camera m_camera;

	// Token: 0x040012CE RID: 4814
	public Entity m_entity;

	// Token: 0x040012CF RID: 4815
	public TransformC m_TC;

	// Token: 0x040012D0 RID: 4816
	private Texture2D m_texture;

	// Token: 0x040012D1 RID: 4817
	private Material m_material;

	// Token: 0x040012D2 RID: 4818
	private UISprite m_background;

	// Token: 0x040012D3 RID: 4819
	private SpriteSheet m_sheet;

	// Token: 0x040012D4 RID: 4820
	private SpriteSheet m_darkenSpriteSheet;

	// Token: 0x040012D5 RID: 4821
	private PsUIBaseState m_state;

	// Token: 0x040012D6 RID: 4822
	private bool m_fadeOut;

	// Token: 0x040012D7 RID: 4823
	private Action m_screenshotTweenAction;
}
