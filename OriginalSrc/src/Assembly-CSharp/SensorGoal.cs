using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class SensorGoal : Unit
{
	// Token: 0x060001D7 RID: 471 RVA: 0x000147D0 File Offset: 0x00012BD0
	public SensorGoal(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		base.m_graphElement.m_name = "Goal";
		base.m_graphElement.m_isCopyable = false;
		base.m_graphElement.m_isRemovable = true;
		base.m_graphElement.m_isRotateable = false;
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position + new Vector3(0f, 0f, 50f), _graphElement.m_rotation);
		this.m_mainTransform = this.m_tc;
		float num = 45f;
		ucpShape ucpShape = this.CreateBodyShape();
		this.m_colShape = ucpShape;
		this.m_goalCmb = ChipmunkProS.AddStaticBody(this.m_tc, ucpShape, null);
		this.SetGoalPrefab();
		this.CreateEditorTouchArea(num, num, null, default(Vector2));
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x000148AB File Offset: 0x00012CAB
	public virtual ucpShape CreateBodyShape()
	{
		return new ucpCircleShape(45f, Vector2.zero, 17895696U, 500f, 0.5f, 0.9f, (ucpCollisionType)6, true);
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x000148D4 File Offset: 0x00012CD4
	public virtual void SetGoalPrefab()
	{
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_goalCmb, new CollisionDelegate(this.GoalCollisionHandler), (ucpCollisionType)6, (ucpCollisionType)3, true, false, false);
		}
		else
		{
			Vector2[] circle = DebugDraw.GetCircle(45f, 32, Vector2.zero);
			this.m_prefabC = PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_tc, Vector3.zero, circle, 4f, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), CameraS.m_mainCamera, Position.Center, true);
			this.m_minigame.SetGoalNode(this);
		}
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00014962 File Offset: 0x00012D62
	public override void CreateEditorTouchArea(float _width, float _height, TransformC _parentTC = null, Vector2 _offset = default(Vector2))
	{
		if (this.m_minigame.m_editing)
		{
			this.CreateGraphElementTouchArea(_width, _parentTC);
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0001497C File Offset: 0x00012D7C
	protected void GoalCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (this.m_minigame.m_playerBeamingOut)
		{
			return;
		}
		if (!this.m_minigame.m_gameEnded)
		{
			this.WinMiniGame();
			SoundS.PlaySingleShot("/InGame/GameEnd", Vector3.zero, 1f);
			this.m_playerGroup = ChipmunkProWrapper.ucpShapeGetFilter(ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB].shapes[0].shapePtr).group;
			ChipmunkProS.RemoveCollisionHandler(this.m_goalCmb, new CollisionDelegate(this.GoalCollisionHandler));
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00014A10 File Offset: 0x00012E10
	protected virtual void WinMiniGame()
	{
		PsState.m_activeGameLoop.WinMinigame();
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00014A1C File Offset: 0x00012E1C
	public override void Update()
	{
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameEnded)
		{
			this.m_colShapeRemovedTimer--;
			if (this.m_colShapeRemovedTimer == 0)
			{
				ChipmunkProWrapper.ucpShapeSetGroup(this.m_colShape.shapePtr, this.m_playerGroup);
			}
		}
	}

	// Token: 0x040001A3 RID: 419
	protected const int RADIUS = 45;

	// Token: 0x040001A4 RID: 420
	protected ChipmunkBodyC m_goalCmb;

	// Token: 0x040001A5 RID: 421
	protected PrefabC m_prefabC;

	// Token: 0x040001A6 RID: 422
	protected GameObject m_mainPrefab;

	// Token: 0x040001A7 RID: 423
	private TweenC m_tween;

	// Token: 0x040001A8 RID: 424
	private Animator m_animator;

	// Token: 0x040001A9 RID: 425
	private TransformC m_mainTransform;

	// Token: 0x040001AA RID: 426
	private ucpShape m_colShape;

	// Token: 0x040001AB RID: 427
	private int m_colShapeRemovedTimer;

	// Token: 0x040001AC RID: 428
	private GameObject m_confettiLocator;

	// Token: 0x040001AD RID: 429
	private uint m_playerGroup;

	// Token: 0x040001AE RID: 430
	private CameraTargetC m_camTarget;

	// Token: 0x040001AF RID: 431
	private ProjectorC m_projection;

	// Token: 0x040001B0 RID: 432
	protected TransformC m_tc;
}
