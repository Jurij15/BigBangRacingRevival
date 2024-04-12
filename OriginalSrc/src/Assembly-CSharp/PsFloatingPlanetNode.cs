using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class PsFloatingPlanetNode : PsPlanetNode
{
	// Token: 0x0600088A RID: 2186 RVA: 0x0005DC4E File Offset: 0x0005C04E
	public PsFloatingPlanetNode(PsTimedEventLoop _loop, bool _tutorial = false)
		: base(_loop, _tutorial)
	{
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0005DC64 File Offset: 0x0005C064
	public override void CreateBase()
	{
		Entity entity = EntityManager.AddEntity(new string[] { "PlanetUI", "Tutorial" });
		float num = (float)this.m_loop.m_levelNumber * this.m_loop.m_planet.m_nodeRowAngleInterval;
		this.m_idTC = TransformS.AddComponent(entity, "FloaterIdTC");
		TransformS.ParentComponent(this.m_idTC, this.m_loop.m_planet.m_floatingNodesTC);
		TransformS.SetRotation(this.m_idTC, new Vector3(num, 0f, 0f));
		this.m_laneTC = TransformS.AddComponent(entity, "FloaterLaneTC");
		TransformS.ParentComponent(this.m_laneTC, this.m_idTC);
		TransformS.SetRotation(this.m_laneTC, new Vector3(0f, 0f, -1f + (float)this.m_loop.m_path.m_lane * -5.5f));
		Vector3 vector;
		vector..ctor(0f, this.m_floatingAltitude * this.m_loop.m_planet.m_planetRadius, 0f);
		this.m_rootTC = TransformS.AddComponent(entity, "FloaterRootTC", vector);
		TransformS.ParentComponent(this.m_rootTC, this.m_laneTC);
		this.m_rootTC.transform.localEulerAngles = Vector3.zero;
		this.m_tc = TransformS.AddComponent(entity, "FloatingNodeTween" + this.m_loop.m_levelNumber);
		TransformS.ParentComponent(this.m_tc, this.m_rootTC, Vector3.zero);
		this.m_tc.transform.localRotation = Quaternion.identity;
		this.Initialize();
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0005DE04 File Offset: 0x0005C204
	public override void Initialize()
	{
		this.m_domeTC = TransformS.AddComponent(this.m_tc.p_entity, this.GetTransformName());
		TransformS.ParentComponent(this.m_domeTC, this.m_tc, Vector3.zero);
		this.m_domeUITC = TransformS.AddComponent(this.m_tc.p_entity);
		TransformS.ParentComponent(this.m_domeUITC, this.m_domeTC, Vector3.zero);
		this.m_domeNumberTC = TransformS.AddComponent(this.m_tc.p_entity);
		TransformS.ParentComponent(this.m_domeNumberTC, this.m_domeUITC, new Vector3(0f, -5f, -5f));
		this.m_domeUITC.transform.LookAt(this.m_loop.m_planet.m_planetCamera.gameObject.transform, this.m_loop.m_planet.m_planetCamera.gameObject.transform.up);
		this.m_domeUITC.transform.localRotation *= Quaternion.Euler(Vector3.up * 180f);
		base.Initialize();
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0005DF28 File Offset: 0x0005C328
	public override string GetTransformName()
	{
		return "FloaterNode" + this.m_loop.m_levelNumber;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0005DF44 File Offset: 0x0005C344
	public override void CreateTouchArea()
	{
		this.m_tac = TouchAreaS.AddCircleArea(this.m_domeUITC, "FloatingPlanetNode" + this.m_loop.m_nodeId, 5f, this.m_loop.m_planet.m_planetCamera, null);
		TouchAreaS.AddTouchEventListener(this.m_tac, new TouchEventDelegate(this.TouchHandler));
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0005DFAC File Offset: 0x0005C3AC
	public override void CreatePrefab()
	{
		if (this.m_prefab != null)
		{
			PrefabS.RemoveComponent(this.m_prefab, true);
			this.m_prefab = null;
		}
		this.SetPrefabName();
		if (string.IsNullOrEmpty(this.m_prefabName))
		{
			this.m_prefabName = "FloatingPlatformPrefab";
		}
		GameObject gameObject = ResourceManager.GetGameObject(this.m_prefabName + "_GameObject");
		this.m_prefab = PrefabS.AddComponent(this.m_domeTC, Vector3.zero, gameObject);
		this.CustomizeGameobject();
		PrefabS.SetCamera(this.m_prefab.p_gameObject, this.m_loop.m_planet.m_planetCamera);
		TweenC tweenC = TweenS.AddTransformTween(this.m_domeTC, TweenedProperty.Position, TweenStyle.QuadInOut, Vector3.zero, Vector3.up * 5f, Random.Range(1.9f, 2.1f), 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.QuadInOut);
		TweenC tweenC2 = TweenS.AddTransformTween(this.m_domeTC, TweenedProperty.Rotation, TweenStyle.QuadInOut, Vector3.up * -90f, Vector3.up * 90f, Random.Range(8f, 10f), 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC2, -1, true, TweenStyle.QuadInOut);
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0005E0D5 File Offset: 0x0005C4D5
	public virtual void CustomizeGameobject()
	{
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0005E0D7 File Offset: 0x0005C4D7
	public virtual void SetPrefabName()
	{
		this.m_prefabName = string.Empty;
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0005E0E4 File Offset: 0x0005C4E4
	public override void CreateUI()
	{
		if (this.m_numberText == null)
		{
			Frame frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame("menu_timer_background", null);
			this.bgSpriteC = SpriteS.AddComponent(this.m_domeNumberTC, frame, this.m_planetNodeSpriteSheet);
			SpriteS.SetColor(this.bgSpriteC, new Color(1f, 1f, 1f, 0.6f));
			this.m_numberText = TextMeshS.AddComponent(this.m_domeNumberTC, Vector3.forward * -0.5f, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 5f, 2f, 30f, Align.Center, Align.Middle, this.m_loop.m_planet.m_planetCamera, this.m_loop.m_levelNumber.ToString());
			string bannerString = (this.m_loop as PsTimedEventLoop).GetBannerString();
			this.m_numberText.m_textMesh.characterSize = 0.6f;
			TextMeshS.SetText(this.m_numberText, bannerString, false);
			SpriteS.SetDimensions(this.bgSpriteC, this.m_numberText.m_renderer.bounds.size.x * 1.2f, this.m_numberText.m_renderer.bounds.size.y * 0.8f);
			this.m_numberText.m_renderer.material.shader = Shader.Find("Framework/FontShaderFg");
		}
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0005E258 File Offset: 0x0005C658
	public virtual void UpdateBanner()
	{
		if (this.m_numberText != null)
		{
			string bannerString = (this.m_loop as PsTimedEventLoop).GetBannerString();
			TextMeshS.SetText(this.m_numberText, bannerString, false);
			SpriteS.SetDimensions(this.bgSpriteC, this.m_numberText.m_renderer.bounds.size.x * 1.2f, this.m_numberText.m_renderer.bounds.size.y * 0.8f);
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0005E2E6 File Offset: 0x0005C6E6
	public override void CreateRoad()
	{
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0005E2E8 File Offset: 0x0005C6E8
	public override void Update()
	{
		this.m_domeUITC.transform.LookAt(this.m_loop.m_planet.m_planetCamera.gameObject.transform, this.m_loop.m_planet.m_planetCamera.gameObject.transform.up);
		this.m_domeUITC.transform.localRotation *= Quaternion.Euler(Vector3.up * 180f);
		if (Main.m_gameTicks % 30 == 0)
		{
			bool flag = false;
			string bannerString = (this.m_loop as PsTimedEventLoop).GetBannerString();
			if (this.m_numberText != null && this.m_numberText.m_textMesh.text != bannerString)
			{
				TextMeshS.SetTextOptimized(this.m_numberText, bannerString);
				flag = this.m_numberText.m_textMesh.text.Length != bannerString.Length;
			}
			if (flag)
			{
				SpriteS.SetDimensions(this.bgSpriteC, this.m_numberText.m_renderer.bounds.size.x * 1.25f, this.m_numberText.m_renderer.bounds.size.y * 0.65f);
			}
		}
		this.UpdateDestructionAnimation();
		if (this.m_newNode)
		{
			this.UpdateFloatingNodePosition();
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0005E458 File Offset: 0x0005C858
	protected virtual void UpdateFloatingNodePosition()
	{
		Quaternion quaternion = Quaternion.Euler(this.m_loop.m_planet.m_nodeRowAngleInterval * (float)this.m_loop.m_levelNumber, 0f, 0f);
		Quaternion quaternion2 = Quaternion.Lerp(this.m_idTC.transform.localRotation, quaternion, 0.025f);
		this.m_idTC.transform.localRotation = quaternion2;
		if (Quaternion.Angle(quaternion, this.m_idTC.transform.localRotation) < 0.05f)
		{
			this.m_newNode = false;
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0005E4E8 File Offset: 0x0005C8E8
	public virtual void SetNewNode()
	{
		this.m_newNode = true;
		float num = 90f;
		num += this.m_loop.m_planet.m_nodeRowAngleInterval * (float)this.m_loop.m_levelNumber;
		this.m_idTC.transform.localEulerAngles = new Vector3(num, 0f, 0f);
		SoundS.PlaySingleShot("/Metagame/FlyingDomeAppear", Vector2.zero, 1f);
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x0005E55B File Offset: 0x0005C95B
	public void RearrangeNodePosition()
	{
		this.m_newNode = true;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0005E564 File Offset: 0x0005C964
	private void UpdateDestructionAnimation()
	{
		if (this.m_shadowProjector != null && this.m_shadowTween != null)
		{
			this.m_shadowProjector.orthographicSize = this.m_shadowTween.currentValue.x;
			if (this.m_shadowTween.currentValue.x < 0.1f)
			{
				TweenS.RemoveComponent(this.m_shadowTween);
				this.m_shadowTween = null;
				this.m_shadowProjector = null;
			}
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0005E5DC File Offset: 0x0005C9DC
	public void DestroyNode()
	{
		if (!this.m_destroyStarted)
		{
			this.m_destroyStarted = true;
			this.m_shadowProjector = this.m_prefab.p_gameObject.GetComponentInChildren<Projector>();
			if (this.m_shadowProjector != null)
			{
				this.m_shadowTween = TweenS.AddTween(this.m_tc.p_entity, TweenStyle.ExpoIn, this.m_shadowProjector.orthographicSize, 0f, 0.4f, 0f);
			}
			TweenC scaleTween = TweenS.AddTransformTween(this.m_tc, TweenedProperty.Scale, TweenStyle.ExpoIn, new Vector3(0f, 0f, 0f), 0.5f, 0f, false);
			TweenS.AddTweenEndEventListener(scaleTween, delegate(TweenC _c)
			{
				TweenS.RemoveComponent(scaleTween);
				if (PsPlanetManager.GetCurrentPlanet().m_spaceEntity != null)
				{
					Entity entity = EntityManager.AddEntity();
					TransformC transformC = TransformS.AddComponent(entity, this.m_tc.transform.position);
					PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.FloatingDomeDeathEffect_GameObject));
					PrefabS.SetCamera(prefabC, this.m_loop.m_planet.m_planetCamera);
					PsPlanetManager.GetCurrentPlanet().RearrangeFloaters(this);
					PsPlanetManager.GetCurrentPlanet().m_floatingNodeList.Remove(this);
					TimerC timerC = TimerS.AddComponent(entity, "FloatingNodeRemoveTimer", 1f, 0f, true, null);
					this.Destroy();
					SoundS.PlaySingleShot("/Metagame/FlyingDomeDisappear", Vector2.zero, 1f);
				}
			});
		}
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0005E6A9 File Offset: 0x0005CAA9
	public void TimedDestructionOfNode(float _delay)
	{
		TimerS.AddComponent(this.m_tc.p_entity, "FloatingNodeDestroyTimer", 0f, _delay, false, delegate(TimerC _c)
		{
			TimerS.RemoveComponent(_c);
			this.DestroyNode();
		});
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0005E6D4 File Offset: 0x0005CAD4
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0005E6DC File Offset: 0x0005CADC
	public override void Highlight()
	{
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0005E6DE File Offset: 0x0005CADE
	public override void RemoveHighlight()
	{
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0005E6E0 File Offset: 0x0005CAE0
	public override void SetState()
	{
		this.SetActive();
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0005E6E8 File Offset: 0x0005CAE8
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchCount == 1 && _touchPhase == TouchAreaPhase.ReleaseIn && !_touches[0].m_dragged)
		{
			TouchAreaS.CancelAllTouches(null);
			SoundS.PlaySingleShot("/UI/DomeSelect", Vector3.zero, 1f);
			if (!PlayerPrefsX.GetNameChanged())
			{
				PsUserNameInputState psUserNameInputState = new PsUserNameInputState();
				psUserNameInputState.m_lastState = Main.m_currentGame.m_currentScene.GetCurrentState();
				psUserNameInputState.m_continueAction = new Action(this.m_loop.StartLoop);
				PsMenuScene.m_lastIState = psUserNameInputState;
				PsMenuScene.m_lastState = null;
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUserNameInputState);
			}
			else
			{
				this.m_loop.StartLoop();
			}
		}
	}

	// Token: 0x0400081B RID: 2075
	public TransformC m_rootTC;

	// Token: 0x0400081C RID: 2076
	public TransformC m_idTC;

	// Token: 0x0400081D RID: 2077
	public TransformC m_laneTC;

	// Token: 0x0400081E RID: 2078
	public TransformC m_domeTC;

	// Token: 0x0400081F RID: 2079
	protected TransformC m_domeUITC;

	// Token: 0x04000820 RID: 2080
	protected TransformC m_domeNumberTC;

	// Token: 0x04000821 RID: 2081
	protected string m_prefabName;

	// Token: 0x04000822 RID: 2082
	protected Projector m_shadowProjector;

	// Token: 0x04000823 RID: 2083
	protected TweenC m_shadowTween;

	// Token: 0x04000824 RID: 2084
	protected TweenC m_relationalTween;

	// Token: 0x04000825 RID: 2085
	private bool m_destroyStarted;

	// Token: 0x04000826 RID: 2086
	public bool m_newNode;

	// Token: 0x04000827 RID: 2087
	public SpriteC bgSpriteC;

	// Token: 0x04000828 RID: 2088
	protected float m_floatingAltitude = 1.1f;
}
