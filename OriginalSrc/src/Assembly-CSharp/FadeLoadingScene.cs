using System;
using UnityEngine;

// Token: 0x02000223 RID: 547
public class FadeLoadingScene : ILoadingScene
{
	// Token: 0x06000FC5 RID: 4037 RVA: 0x00095B41 File Offset: 0x00093F41
	public FadeLoadingScene(Color _fadeColor, bool _fadeIn = true, float _fadeDuration = 0.25f)
	{
		this.m_fadeColor = _fadeColor;
		this.m_fadeIn = _fadeIn;
		this.m_fadeDuration = _fadeDuration;
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00095B5E File Offset: 0x00093F5E
	// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x00095B66 File Offset: 0x00093F66
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

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00095B6F File Offset: 0x00093F6F
	// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x00095B77 File Offset: 0x00093F77
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

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00095B80 File Offset: 0x00093F80
	// (set) Token: 0x06000FCB RID: 4043 RVA: 0x00095B88 File Offset: 0x00093F88
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

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00095B91 File Offset: 0x00093F91
	public bool InitComplete
	{
		get
		{
			return this.m_initComplete;
		}
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00095B9C File Offset: 0x00093F9C
	private void StartIntro()
	{
		if (this.m_fadeIn)
		{
			TweenC tweenC = TweenS.AddTransformTween(this.m_bg.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.zero, Vector3.one, this.m_fadeDuration, 0f, false);
			TweenS.SetTweenAlphaProperties(tweenC, false, true, false, null);
			TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.IntroTweenEventHandler));
		}
		else
		{
			new PsServerQueueFlow(delegate
			{
				this.m_introComplete = true;
			}, null, null);
			PsUILoadingText psUILoadingText = new PsUILoadingText(this.m_bg);
			EntityManager.RemoveAllTagsFromEntity(psUILoadingText.m_TC.p_entity, true);
			this.m_bg.Update();
		}
		this.m_introStarted = true;
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00095C44 File Offset: 0x00094044
	public void StartOutro()
	{
		if (this.m_bg != null)
		{
			this.m_bg.DestroyChildren();
		}
		TweenC tweenC = TweenS.AddTransformTween(this.m_bg.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.one, Vector3.zero, this.m_fadeDuration, 0f, false);
		TweenS.SetTweenAlphaProperties(tweenC, false, true, false, null);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.OutroTweenEventHandler));
		this.m_outroStarted = true;
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00095CB3 File Offset: 0x000940B3
	public void Load()
	{
		this.m_camera = CameraS.AddCamera("Loading Screen Camera", true, 3);
		CameraS.SetAsOverlayCamera(this.m_camera);
		this.Initialize();
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00095CD8 File Offset: 0x000940D8
	public void Initialize()
	{
		this.m_bg = new UICanvas(null, false, string.Empty, null, string.Empty);
		EntityManager.RemoveAllTagsFromEntity(this.m_bg.m_TC.p_entity, true);
		this.m_bg.SetCamera(this.m_camera, true, false);
		this.m_bg.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_bg.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_bg.SetDrawHandler(new UIDrawDelegate(this.BackgroundDrawhandler));
		this.m_bg.Update();
		this.m_initComplete = true;
		this.StartIntro();
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00095D78 File Offset: 0x00094178
	private void BackgroundDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		PrefabC prefabC = PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, DebugDraw.HexToUint("#000000"), DebugDraw.HexToUint("#000000"), ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), _c.m_camera, string.Empty, null);
		prefabC.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find("WOE/Unlit/ColorUnlitTransparentBg");
		Color color;
		color..ctor(0f, 0f, 0f, 1f);
		prefabC.p_gameObject.GetComponent<Renderer>().material.color = color;
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x00095E3C File Offset: 0x0009423C
	private void IntroTweenEventHandler(TweenC _c)
	{
		new PsServerQueueFlow(delegate
		{
			this.m_introComplete = true;
		}, null, null);
		TweenS.RemoveTweenEndEventListener(_c, new TweenEventDelegate(this.IntroTweenEventHandler));
		TweenS.RemoveComponent(_c);
		PsUILoadingText psUILoadingText = new PsUILoadingText(this.m_bg);
		EntityManager.RemoveAllTagsFromEntity(psUILoadingText.m_TC.p_entity, true);
		this.m_bg.Update();
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00095E9D File Offset: 0x0009429D
	private void OutroTweenEventHandler(TweenC _c)
	{
		this.m_outroComplete = true;
		TweenS.RemoveTweenEndEventListener(_c, new TweenEventDelegate(this.OutroTweenEventHandler));
		TweenS.RemoveComponent(_c);
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00095EBE File Offset: 0x000942BE
	public void Update()
	{
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x00095EC0 File Offset: 0x000942C0
	public void Destroy()
	{
		Debug.Log("FADE LOADING SCENE DESTROY", null);
		CameraS.RemoveCamera(this.m_camera);
		if (this.m_bg != null)
		{
			this.m_bg.Destroy();
			this.m_bg = null;
		}
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x00095EF5 File Offset: 0x000942F5
	public bool IntroComplete()
	{
		return this.m_introComplete;
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00095EFD File Offset: 0x000942FD
	public bool OutroComplete()
	{
		return this.m_outroComplete;
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00095F08 File Offset: 0x00094308
	~FadeLoadingScene()
	{
	}

	// Token: 0x040012B1 RID: 4785
	private StateMachine m_stateMachine;

	// Token: 0x040012B2 RID: 4786
	private IScene m_fromScene;

	// Token: 0x040012B3 RID: 4787
	private IScene m_toScene;

	// Token: 0x040012B4 RID: 4788
	private bool m_initComplete;

	// Token: 0x040012B5 RID: 4789
	private bool m_introComplete;

	// Token: 0x040012B6 RID: 4790
	private bool m_outroComplete;

	// Token: 0x040012B7 RID: 4791
	private bool m_introStarted;

	// Token: 0x040012B8 RID: 4792
	private bool m_outroStarted;

	// Token: 0x040012B9 RID: 4793
	public Camera m_camera;

	// Token: 0x040012BA RID: 4794
	public SpriteSheet m_loadingScreenSpriteSheet;

	// Token: 0x040012BB RID: 4795
	public TransformC m_loadingScreenTC;

	// Token: 0x040012BC RID: 4796
	public Color m_fadeColor;

	// Token: 0x040012BD RID: 4797
	public bool m_fadeIn;

	// Token: 0x040012BE RID: 4798
	public float m_fadeDuration;

	// Token: 0x040012BF RID: 4799
	public UICanvas m_bg;
}
