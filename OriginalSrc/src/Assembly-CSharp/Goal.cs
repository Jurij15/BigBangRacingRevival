using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class Goal : Unit
{
	// Token: 0x060001AD RID: 429 RVA: 0x00014BD8 File Offset: 0x00012FD8
	public Goal(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		base.m_graphElement.m_name = "Goal";
		base.m_graphElement.m_isCopyable = false;
		base.m_graphElement.m_isRemovable = false;
		base.m_graphElement.m_isRotateable = false;
		base.m_graphElement.m_isFlippable = true;
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, 0f, 50f), _graphElement.m_rotation);
		this.m_mainTransform = transformC;
		this.m_representations = new GoalRepresentation[]
		{
			new DefaultGoal(this),
			new BouncyBallGoal(this),
			new MetalBallGoal(this)
		};
		if (base.m_graphElement.m_flipped)
		{
			base.m_graphElement.m_flipped = false;
			int num = this.CurrentIndex + 1;
			num %= this.m_representations.Length;
			this.CurrentIndex = num;
		}
		this.m_representation = this.m_representations[this.CurrentIndex];
		this.m_representation.CreateBody(out this.m_colShape, out this.m_goalCmb);
		this.m_prefabC = this.m_representation.CreatePrefab();
		this.m_animator = this.m_prefabC.p_gameObject.GetComponent("Animator") as Animator;
		this.m_materialInstances = null;
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_goalCmb, new CollisionDelegate(this.GoalCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, false, false);
			ChipmunkProS.AddCollisionHandler(this.m_goalCmb, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, false, false);
			ChipmunkProS.AddCollisionHandler(this.m_goalCmb, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)4, (ucpCollisionType)2, true, false, false);
			this.CreateCamTarget();
		}
		else
		{
			this.m_minigame.SetGoalNode(this);
		}
		this.m_colShapeRemovedTimer = 5;
		int num2 = 1280;
		this.m_projection = ProjectorS.AddComponent(this.m_mainTransform, ResourceManager.GetMaterial(RESOURCE.ShadowMaterial_Material), num2, new Vector3(0f, -20f));
		this.CreateEditorTouchArea(0f, 0f, null, default(Vector2));
		if (this.m_minigame.m_editing && Goal.SHOW_TUTORIAL && string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameId))
		{
			this.m_editorTutorial = new UIText(null, false, "playerTutorial", PsStrings.Get(StringID.TUTORIAL_TAP_TO_MOVE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, null, "313131");
			this.m_editorTutorial.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = Color.white;
			this.m_editorTutorial.m_shadowtmc.m_textMesh.GetComponent<Renderer>().material.color = DebugDraw.HexToColor("313131");
			this.m_editorTutorial.Update();
		}
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x060001AE RID: 430 RVA: 0x00014EB2 File Offset: 0x000132B2
	// (set) Token: 0x060001AF RID: 431 RVA: 0x00014EC5 File Offset: 0x000132C5
	private int CurrentIndex
	{
		get
		{
			return (int)base.m_graphElement.m_storedRotation.x;
		}
		set
		{
			base.m_graphElement.m_storedRotation.x = (float)value;
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00014ED9 File Offset: 0x000132D9
	public override void SetGravity(Vector2 _gravity)
	{
		if (this.m_representation.m_affectedByGravity)
		{
			base.SetGravity(_gravity);
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00014EF2 File Offset: 0x000132F2
	public override void CreateEditorTouchArea(float _width = 0f, float _height = 0f, TransformC _parentTC = null, Vector2 _offset = default(Vector2))
	{
		if (this.m_minigame.m_editing)
		{
			this.CreateGraphElementTouchArea(this.m_representation.m_radius, _parentTC);
		}
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00014F16 File Offset: 0x00013316
	public void CreateCamTarget()
	{
		this.m_camTarget = CameraS.AddTargetComponent(this.m_mainTransform, 200f, 200f, new Vector2(0f, 0f));
		this.m_camTarget.activeRadius = 500f;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00014F54 File Offset: 0x00013354
	private void GoalCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (this.m_minigame.m_playerBeamingOut)
		{
			return;
		}
		if (!this.m_minigame.m_playerReachedGoal && !this.m_minigame.m_gameEnded && !this.m_minigame.m_playerUnit.m_isDead)
		{
			this.m_minigame.m_playerReachedGoal = true;
			this.m_minigame.m_playerReachedGoalCount++;
			this.m_camTarget.activeRadius = float.MaxValue;
			CameraS.SetTargetBB(this.m_camTarget, 400f, 400f);
			Vehicle vehicle = this.m_minigame.m_playerUnit as Vehicle;
			if (vehicle != null)
			{
				vehicle.ReachedGoal();
			}
			this.m_minigame.m_playerUnit.SetAsTeleporting(15, this.m_mainTransform.transform, this.m_mainTransform.transform, true, false, true, true);
			this.m_animator.SetTrigger("GameEnd");
			this.m_representation.OnCollision();
			PsState.m_activeGameLoop.m_gameMode.RecordGhostGoal(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks));
			PsState.m_activeGameLoop.WinMinigame();
			SoundS.PlaySingleShot("/InGame/GameEnd", Vector3.zero, 1f);
			ChipmunkProS.RemoveCollisionHandler(this.m_goalCmb, new CollisionDelegate(this.GoalCollisionHandler));
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000150A4 File Offset: 0x000134A4
	private void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (_phase == ucpCollisionPhase.Begin && Mathf.Abs(_pair.impulse.magnitude) > 0.5f)
		{
			float positionBetween = ToolBox.getPositionBetween(_pair.impulse.magnitude, 1600f, (float)this.m_representation.m_maxForce);
			if (positionBetween > 0f && !string.IsNullOrEmpty(this.m_representation.m_impactSound))
			{
				SoundS.PlaySingleShotWithParameter(this.m_representation.m_impactSound, this.m_goalCmb.TC.transform.position, "Force", positionBetween, 1f);
			}
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00015148 File Offset: 0x00013548
	public override void Update()
	{
		if (this.m_minigame.m_editing && this.m_editorTutorial != null)
		{
			Vector3 vector = CameraS.m_mainCamera.WorldToScreenPoint(base.m_graphElement.m_TC.transform.position + Vector3.up * 80f);
			Vector3 vector2 = CameraS.m_uiCamera.ScreenToWorldPoint(vector);
			Vector3 vector3 = vector2;
			this.m_editorTutorial.m_TC.transform.position = new Vector3(vector3.x, vector3.y, 0f);
			if (base.m_graphElement.m_selected && !this.m_tutorialFade)
			{
				this.m_tutorialFade = true;
				TweenC tweenC = TweenS.AddTransformTween(this.m_editorTutorial.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.zero, 2f, 2f, true);
				TweenS.SetTweenAlphaProperties(tweenC, false, false, true, Shader.Find("Framework/FontShader"));
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
				{
					this.m_editorTutorial.Destroy();
					this.m_editorTutorial = null;
					Goal.SHOW_TUTORIAL = false;
				});
			}
		}
		this.m_representation.Update();
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00015254 File Offset: 0x00013654
	public override void Destroy()
	{
		if (this.m_editorTutorial != null)
		{
			this.m_editorTutorial.Destroy();
		}
		this.m_editorTutorial = null;
		if (this.m_materialInstances != null)
		{
			for (int i = 0; i < this.m_materialInstances.Length; i++)
			{
				Object.Destroy(this.m_materialInstances[i]);
				this.m_materialInstances[i] = null;
			}
			this.m_materialInstances = null;
		}
		base.Destroy();
	}

	// Token: 0x04000184 RID: 388
	private ChipmunkBodyC m_goalCmb;

	// Token: 0x04000185 RID: 389
	private PrefabC m_prefabC;

	// Token: 0x04000186 RID: 390
	private Animator m_animator;

	// Token: 0x04000187 RID: 391
	private ucpShape m_colShape;

	// Token: 0x04000188 RID: 392
	public TransformC m_mainTransform;

	// Token: 0x04000189 RID: 393
	private int m_colShapeRemovedTimer;

	// Token: 0x0400018A RID: 394
	private GameObject m_confettiLocator;

	// Token: 0x0400018B RID: 395
	private uint m_playerGroup;

	// Token: 0x0400018C RID: 396
	public CameraTargetC m_camTarget;

	// Token: 0x0400018D RID: 397
	public ProjectorC m_projection;

	// Token: 0x0400018E RID: 398
	private UIText m_editorTutorial;

	// Token: 0x0400018F RID: 399
	private bool m_tutorialFade;

	// Token: 0x04000190 RID: 400
	public static bool SHOW_TUTORIAL = true;

	// Token: 0x04000191 RID: 401
	private Material[] m_materialInstances;

	// Token: 0x04000192 RID: 402
	private GoalRepresentation[] m_representations;

	// Token: 0x04000193 RID: 403
	private GoalRepresentation m_representation;
}
