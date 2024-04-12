using System;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class PsRatingLoadingScene : ILoadingScene, IStatedObject
{
	// Token: 0x06000FFF RID: 4095 RVA: 0x000964E5 File Offset: 0x000948E5
	public PsRatingLoadingScene(Type _centerType = null)
	{
		this.m_centerType = _centerType;
		if (this.m_centerType == null)
		{
			this.m_centerType = typeof(PsUICenterRatingLoading);
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06001000 RID: 4096 RVA: 0x0009650F File Offset: 0x0009490F
	// (set) Token: 0x06001001 RID: 4097 RVA: 0x00096517 File Offset: 0x00094917
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

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06001002 RID: 4098 RVA: 0x00096520 File Offset: 0x00094920
	// (set) Token: 0x06001003 RID: 4099 RVA: 0x00096528 File Offset: 0x00094928
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

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06001004 RID: 4100 RVA: 0x00096531 File Offset: 0x00094931
	// (set) Token: 0x06001005 RID: 4101 RVA: 0x00096539 File Offset: 0x00094939
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

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06001006 RID: 4102 RVA: 0x00096542 File Offset: 0x00094942
	public bool InitComplete
	{
		get
		{
			return this.m_initComplete;
		}
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x0009654A File Offset: 0x0009494A
	private void StartIntro()
	{
		this.m_introStarted = true;
		TouchAreaS.Enable();
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x00096558 File Offset: 0x00094958
	public void StartOutro()
	{
		this.m_outroStarted = true;
		CameraS.BringToFront(this.m_camera, true);
		this.m_state.BringToFront();
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x00096578 File Offset: 0x00094978
	public void Load()
	{
		this.Initialize();
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x00096580 File Offset: 0x00094980
	public void Initialize()
	{
		this.m_camera = CameraS.AddCamera("Rating Screen Camera", true, 3);
		this.m_entity = EntityManager.AddEntity("RatingScreenEntity");
		this.m_entity.m_persistent = true;
		this.m_TC = TransformS.AddComponent(this.m_entity, "RatingScreenTC", Vector3.forward * -240f);
		this.m_stateMachine = new StateMachine(this);
		this.m_state = new PsUIBaseState(this.m_centerType, null, null, null, false, InitialPage.Center);
		this.m_state.SetAction("Continue", new Action(this.ToMenuScene));
		this.m_state.SetAction("Rating", new Action(this.RatingSent));
		this.m_darkenSpriteSheet = SpriteS.AddSpriteSheet(this.m_camera, ResourceManager.GetMaterial(RESOURCE.LoadingScreenFadeMat_Material), 1f);
		SpriteC spriteC = SpriteS.AddComponent(this.m_TC, new Frame(0f, 0f, (float)Screen.width, (float)Screen.height), this.m_darkenSpriteSheet);
		SpriteS.SetColor(spriteC, new Color(0f, 0f, 0f, 0f));
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.zero, new Vector3(0.25f, 0.5f, 0.5f), 0.2f, 0f, true);
		this.m_stateMachine.ChangeState(this.m_state);
		this.m_state.SetCreateDelegate(delegate
		{
			EntityManager.RemoveAllTagsFromEntity(this.m_state.m_baseCanvas.m_TC.p_entity, false);
		});
		this.m_initComplete = true;
		this.m_introComplete = false;
		this.StartIntro();
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00096714 File Offset: 0x00094B14
	public void RatingSent()
	{
		this.m_introComplete = true;
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x00096720 File Offset: 0x00094B20
	private void ToMenuScene()
	{
		TouchAreaS.Disable();
		this.m_fadeOut = true;
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, new Vector3(0.5f, 0.5f, 0.5f), Vector3.zero, 0.2f, 0f, true);
		TimerS.AddComponent(EntityManager.AddEntity(), string.Empty, 0.25f, 0f, true, delegate(TimerC c)
		{
			if (!PsState.m_showStarterPackPopup)
			{
				TouchAreaS.Enable();
			}
			this.m_outroComplete = true;
		});
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00096792 File Offset: 0x00094B92
	public void Update()
	{
		this.m_stateMachine.Update();
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0009679F File Offset: 0x00094B9F
	public bool IntroComplete()
	{
		return this.m_introComplete;
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x000967A7 File Offset: 0x00094BA7
	public bool OutroComplete()
	{
		return this.m_outroComplete;
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x000967B0 File Offset: 0x00094BB0
	public void Destroy()
	{
		Debug.Log("RATING LOADING SCENE DESTROY", null);
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

	// Token: 0x040012D8 RID: 4824
	private StateMachine m_stateMachine;

	// Token: 0x040012D9 RID: 4825
	private IScene m_fromScene;

	// Token: 0x040012DA RID: 4826
	private IScene m_toScene;

	// Token: 0x040012DB RID: 4827
	private bool m_initComplete;

	// Token: 0x040012DC RID: 4828
	private bool m_introComplete;

	// Token: 0x040012DD RID: 4829
	private bool m_outroComplete;

	// Token: 0x040012DE RID: 4830
	private bool m_introStarted;

	// Token: 0x040012DF RID: 4831
	public bool m_outroStarted;

	// Token: 0x040012E0 RID: 4832
	public Camera m_camera;

	// Token: 0x040012E1 RID: 4833
	public Entity m_entity;

	// Token: 0x040012E2 RID: 4834
	public TransformC m_TC;

	// Token: 0x040012E3 RID: 4835
	private Texture2D m_texture;

	// Token: 0x040012E4 RID: 4836
	private Material m_material;

	// Token: 0x040012E5 RID: 4837
	private UISprite m_background;

	// Token: 0x040012E6 RID: 4838
	private SpriteSheet m_sheet;

	// Token: 0x040012E7 RID: 4839
	private SpriteSheet m_darkenSpriteSheet;

	// Token: 0x040012E8 RID: 4840
	private PsUIBaseState m_state;

	// Token: 0x040012E9 RID: 4841
	private bool m_fadeOut;

	// Token: 0x040012EA RID: 4842
	private Type m_centerType;
}
