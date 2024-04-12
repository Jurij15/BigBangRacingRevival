using System;
using UnityEngine;

// Token: 0x02000222 RID: 546
public class FadeLevelEndLoadingScene : ILoadingScene
{
	// Token: 0x06000FB0 RID: 4016 RVA: 0x00095759 File Offset: 0x00093B59
	public FadeLevelEndLoadingScene(Color _fadeColor, PsGameLoop _gameLoop, float _fadeDuration = 0.25f)
	{
		this.m_fadeColor = _fadeColor;
		this.m_gameLoop = _gameLoop;
		this.m_fadeDuration = _fadeDuration;
		this.m_queueCreated = false;
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0009577D File Offset: 0x00093B7D
	// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00095785 File Offset: 0x00093B85
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

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0009578E File Offset: 0x00093B8E
	// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00095796 File Offset: 0x00093B96
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

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0009579F File Offset: 0x00093B9F
	// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x000957A7 File Offset: 0x00093BA7
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

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x000957B0 File Offset: 0x00093BB0
	public bool InitComplete
	{
		get
		{
			return this.m_initComplete;
		}
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x000957B8 File Offset: 0x00093BB8
	private void StartIntro()
	{
		TweenC tweenC = TweenS.AddTransformTween(this.m_bg.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.zero, Vector3.one, this.m_fadeDuration, 0f, false);
		TweenS.SetTweenAlphaProperties(tweenC, false, true, false, null);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.IntroTweenEventHandler));
		this.m_introStarted = true;
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00095814 File Offset: 0x00093C14
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

	// Token: 0x06000FBA RID: 4026 RVA: 0x00095883 File Offset: 0x00093C83
	public void Load()
	{
		this.m_camera = CameraS.AddCamera("Loading Screen Camera", true, 3);
		this.Initialize();
		CameraS.BringToFront(this.m_camera, true);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x000958AC File Offset: 0x00093CAC
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

	// Token: 0x06000FBC RID: 4028 RVA: 0x0009594C File Offset: 0x00093D4C
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

	// Token: 0x06000FBD RID: 4029 RVA: 0x00095A0D File Offset: 0x00093E0D
	private void IntroTweenEventHandler(TweenC _c)
	{
		new PsUILoadingText(this.m_bg);
		this.m_bg.Update();
		this.m_fadeInComplete = true;
		TweenS.RemoveTweenEndEventListener(_c, new TweenEventDelegate(this.IntroTweenEventHandler));
		TweenS.RemoveComponent(_c);
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x00095A45 File Offset: 0x00093E45
	private void OutroTweenEventHandler(TweenC _c)
	{
		this.m_outroComplete = true;
		TweenS.RemoveTweenEndEventListener(_c, new TweenEventDelegate(this.OutroTweenEventHandler));
		TweenS.RemoveComponent(_c);
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x00095A68 File Offset: 0x00093E68
	public void Update()
	{
		if (this.m_fadeInComplete && !this.m_introComplete && (this.m_gameLoop == null || !this.m_gameLoop.m_gameMode.m_waitForHighscoreAndGhost) && !this.m_queueCreated)
		{
			new PsServerQueueFlow(delegate
			{
				this.m_introComplete = true;
			}, null, null);
			this.m_queueCreated = true;
		}
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00095AD1 File Offset: 0x00093ED1
	public void Destroy()
	{
		if (this.m_bg != null)
		{
			this.m_bg.Destroy();
			this.m_bg = null;
		}
		CameraS.RemoveCamera(this.m_camera);
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x00095AFB File Offset: 0x00093EFB
	public bool IntroComplete()
	{
		return this.m_introComplete;
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x00095B03 File Offset: 0x00093F03
	public bool OutroComplete()
	{
		return this.m_outroComplete;
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00095B0C File Offset: 0x00093F0C
	~FadeLevelEndLoadingScene()
	{
	}

	// Token: 0x0400129F RID: 4767
	private StateMachine m_stateMachine;

	// Token: 0x040012A0 RID: 4768
	private IScene m_fromScene;

	// Token: 0x040012A1 RID: 4769
	private IScene m_toScene;

	// Token: 0x040012A2 RID: 4770
	private bool m_initComplete;

	// Token: 0x040012A3 RID: 4771
	private bool m_introComplete;

	// Token: 0x040012A4 RID: 4772
	private bool m_outroComplete;

	// Token: 0x040012A5 RID: 4773
	private bool m_introStarted;

	// Token: 0x040012A6 RID: 4774
	private bool m_outroStarted;

	// Token: 0x040012A7 RID: 4775
	private bool m_fadeInComplete;

	// Token: 0x040012A8 RID: 4776
	public Camera m_camera;

	// Token: 0x040012A9 RID: 4777
	public Entity m_loadingScreenEntity;

	// Token: 0x040012AA RID: 4778
	public TransformC m_loadingScreenTC;

	// Token: 0x040012AB RID: 4779
	public Color m_fadeColor;

	// Token: 0x040012AC RID: 4780
	public float m_fadeDuration;

	// Token: 0x040012AD RID: 4781
	private PsGameLoop m_gameLoop;

	// Token: 0x040012AE RID: 4782
	private bool m_queueCreated;

	// Token: 0x040012AF RID: 4783
	private UICanvas m_bg;

	// Token: 0x040012B0 RID: 4784
	private PsUILoadingAnimation m_animation;
}
