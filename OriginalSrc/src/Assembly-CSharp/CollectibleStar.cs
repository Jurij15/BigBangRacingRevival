using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class CollectibleStar : Unit
{
	// Token: 0x06000156 RID: 342 RVA: 0x0001066C File Offset: 0x0000EA6C
	public CollectibleStar(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		CollectibleStar.SHOULD_FADE = false;
		base.m_graphElement.m_name = "CollectibleStar";
		base.m_graphElement.m_isCopyable = false;
		base.m_graphElement.m_isRemovable = false;
		base.m_graphElement.m_isRotateable = false;
		ResourcePool.Asset asset = RESOURCE.TreasureMapIngameA_GameObject;
		if (CollectibleStar.m_lastCreatedMapPiece < 1 || CollectibleStar.m_lastCreatedMapPiece > 2)
		{
			asset = RESOURCE.TreasureMapIngameA_GameObject;
			CollectibleStar.m_lastCreatedMapPiece = 1;
		}
		else if (CollectibleStar.m_lastCreatedMapPiece == 1)
		{
			asset = RESOURCE.TreasureMapIngameB_GameObject;
			CollectibleStar.m_lastCreatedMapPiece = 2;
		}
		else
		{
			asset = RESOURCE.TreasureMapIngameC_GameObject;
			CollectibleStar.m_lastCreatedMapPiece = 3;
		}
		if (PsState.m_activeGameLoop.m_context == PsMinigameContext.Fresh)
		{
			asset = RESOURCE.CollectibleShardLargePrefab_GameObject;
		}
		if (PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
		{
			asset = RESOURCE.BossCheckpointPrefab_GameObject;
		}
		GameObject gameObject = ResourceManager.GetGameObject(asset);
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position, _graphElement.m_rotation);
		this.m_pos = _graphElement.m_position;
		float num = 30f;
		ucpShape ucpShape = new ucpCircleShape(num, Vector2.zero, 16777216U, 50f, 0.5f, 0.9f, (ucpCollisionType)4, true);
		this.m_starCmb = ChipmunkProS.AddKinematicBody(this.m_tc, ucpShape, null);
		this.m_prefabC = PrefabS.AddComponent(this.m_tc, Vector3.zero, gameObject);
		if (PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
		{
			this.m_bossScript = this.m_prefabC.p_gameObject.GetComponent<EffectBossCheckpoint>();
			if (this.m_bossScript != null)
			{
				this.m_bossScript.StartScrolling();
			}
			this.m_prefabC.p_gameObject.transform.position = new Vector3(this.m_prefabC.p_gameObject.transform.position.x, this.m_prefabC.p_gameObject.transform.position.y, 148.7f);
		}
		this.m_materialInstance = null;
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_starCmb, new CollisionDelegate(this.StarCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, false, false);
			this.CreateCamTarget();
		}
		if (this.m_minigame.m_editing && CollectibleStar.SHOW_TUTORIAL && string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameId))
		{
			this.m_editorTutorial = new UIText(null, false, "playerTutorial", PsStrings.Get(StringID.TUTORIAL_TAP_TO_MOVE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, null, "313131");
			this.m_editorTutorial.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = Color.white;
			this.m_editorTutorial.m_shadowtmc.m_textMesh.GetComponent<Renderer>().material.color = DebugDraw.HexToColor("313131");
			this.m_editorTutorial.Update();
		}
		this.CreateEditorTouchArea(num, num, null, default(Vector2));
		this.m_reactToBlastWaves = false;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00010974 File Offset: 0x0000ED74
	public void CreateCamTarget()
	{
		CameraTargetC cameraTargetC = CameraS.AddTargetComponent(this.m_starCmb.TC, 150f, 150f, new Vector2(0f, 0f));
		cameraTargetC.activeRadius = 400f;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x000109B6 File Offset: 0x0000EDB6
	public override void CreateEditorTouchArea(float _width, float _height, TransformC _parentTC = null, Vector2 _offset = default(Vector2))
	{
		if (this.m_minigame.m_editing)
		{
			this.CreateGraphElementTouchArea(_width, _parentTC);
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000109D0 File Offset: 0x0000EDD0
	public void Collect()
	{
		PsState.m_activeGameLoop.CollectibleCollected();
		Vector2 vector = this.m_tc.transform.position;
		if (this.m_bossScript != null && PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
		{
			BossCheckpointSymbol lastPrize = ((PsState.m_activeGameLoop as PsGameLoopAdventureBattle).m_gameMode as PsGameModeAdventureBattle).GetLastPrize();
			this.m_bossScript.StopScrolling(lastPrize);
			this.m_bossScript.Wobble();
			SoundS.PlaySingleShot("/Ingame/Units/Checkpoint_Player", new Vector3(vector.x, vector.y, 0f), 1f);
		}
		else
		{
			Entity entity = EntityManager.AddEntity(new string[] { "GTAG_AUTODESTROY" });
			TransformC transformC = TransformS.AddComponent(entity, this.m_tc.transform.position);
			PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.PickUpStarSparks_GameObject), string.Empty, false, true);
			PrefabS.SetCamera(prefabC, CameraS.m_mainCamera);
			TimerC timerC = TimerS.AddComponent(entity, "RemoveTimer", 1f, 0f, true, null);
			if (PsState.m_activeGameLoop.m_context == PsMinigameContext.Fresh)
			{
				SoundS.PlaySingleShotWithParameter("/Ingame/Units/GotDiamond", new Vector3(vector.x, vector.y, 0f), "CollectableNumber", (float)PsState.m_activeMinigame.m_collectedStars, 1f);
			}
			else
			{
				SoundS.PlaySingleShotWithParameter("/Ingame/Units/GotCollectable", new Vector3(vector.x, vector.y, 0f), "CollectableNumber", (float)PsState.m_activeMinigame.m_collectedStars, 1f);
			}
			this.Destroy();
		}
		this.m_isDead = true;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00010B78 File Offset: 0x0000EF78
	public override void Destroy()
	{
		if (this.m_editorTutorial != null)
		{
			this.m_editorTutorial.Destroy();
		}
		this.m_editorTutorial = null;
		if (this.m_materialInstance != null)
		{
			Object.Destroy(this.m_materialInstance);
			this.m_materialInstance = null;
		}
		base.Destroy();
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00010BCB File Offset: 0x0000EFCB
	private void StarCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (!this.m_isDead)
		{
			this.Collect();
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00010BE0 File Offset: 0x0000EFE0
	public override void Update()
	{
		if (this.m_minigame.m_editing && this.m_editorTutorial != null)
		{
			Vector3 vector = CameraS.m_mainCamera.WorldToScreenPoint(base.m_graphElement.m_TC.transform.position + Vector3.up * 80f);
			Vector3 vector2 = CameraS.m_uiCamera.ScreenToWorldPoint(vector);
			Vector3 vector3 = vector2;
			this.m_editorTutorial.m_TC.transform.position = new Vector3(vector3.x, vector3.y, 0f);
			if (base.m_graphElement.m_selected)
			{
				CollectibleStar.SHOULD_FADE = true;
			}
		}
		if (this.m_minigame.m_editing && CollectibleStar.SHOULD_FADE && !this.m_tutorialFade)
		{
			this.m_tutorialFade = true;
			TweenC tweenC = TweenS.AddTransformTween(this.m_editorTutorial.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.zero, 2f, 2f, true);
			TweenS.SetTweenAlphaProperties(tweenC, false, false, true, Shader.Find("Framework/FontShader"));
			TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
			{
				this.m_editorTutorial.Destroy();
				this.m_editorTutorial = null;
				CollectibleStar.SHOW_TUTORIAL = false;
			});
		}
		base.Update();
	}

	// Token: 0x0400013A RID: 314
	private const int RADIUS = 30;

	// Token: 0x0400013B RID: 315
	public ChipmunkBodyC m_starCmb;

	// Token: 0x0400013C RID: 316
	private PrefabC m_prefabC;

	// Token: 0x0400013D RID: 317
	public Vector3 m_pos;

	// Token: 0x0400013E RID: 318
	public TransformC m_tc;

	// Token: 0x0400013F RID: 319
	private Material m_materialInstance;

	// Token: 0x04000140 RID: 320
	private static int m_lastCreatedMapPiece;

	// Token: 0x04000141 RID: 321
	private UIText m_editorTutorial;

	// Token: 0x04000142 RID: 322
	private bool m_tutorialFade;

	// Token: 0x04000143 RID: 323
	public static bool SHOW_TUTORIAL = true;

	// Token: 0x04000144 RID: 324
	public static bool SHOULD_FADE;

	// Token: 0x04000145 RID: 325
	public EffectBossCheckpoint m_bossScript;
}
