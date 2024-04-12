using System;
using UnityEngine;

// Token: 0x020005BA RID: 1466
public class ChipmunkTestState : BasicState
{
	// Token: 0x06002AD3 RID: 10963 RVA: 0x001BB3F8 File Offset: 0x001B97F8
	public void createSprite(string name)
	{
		float num = (float)Random.Range(4, 10);
		Entity entity = EntityManager.AddEntity(name);
		TransformC transformC = TransformS.AddComponent(entity, name);
		SpriteC spriteC = SpriteS.AddComponent(transformC, new Frame(0f, 0f, 5f, 5f), this.m_spriteSheet);
		SpriteS.SetDimensions(spriteC, num * 1f, num * 1f);
		SpriteS.SetColor(spriteC, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f));
		TransformS.SetPosition(transformC, new Vector3((float)Random.Range(-50, 50), (float)Random.Range(100, 250), 0f));
		ucpPolyShape ucpPolyShape = new ucpPolyShape(num, num, Vector2.zero, uint.MaxValue, 1f, 0.5f, 0.5f, ucpCollisionType.None, false);
		ucpPolyShape.collisionType = ucpCollisionType.Any;
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x001BB4E4 File Offset: 0x001B98E4
	public override void Enter(IStatedObject _parent)
	{
		this.m_spriteSheet = FrameworkTestScene.m_spriteSheet;
		this.ticker = 0;
		Entity entity = EntityManager.AddEntity();
		TransformC transformC = TransformS.AddComponent(entity, "walls");
		ucpPolyShape ucpPolyShape = new ucpPolyShape(150f, 10f, Vector2.up * -75f, uint.MaxValue, 10f, 0.5f, 0.5f, ucpCollisionType.None, false);
		ucpPolyShape ucpPolyShape2 = new ucpPolyShape(10f, 150f, Vector2.right * -75f, uint.MaxValue, 10f, 0.5f, 0.5f, ucpCollisionType.None, false);
		ucpPolyShape ucpPolyShape3 = new ucpPolyShape(10f, 150f, Vector2.right * 75f, uint.MaxValue, 10f, 0.5f, 0.5f, ucpCollisionType.None, false);
		ucpShape[] array = new ucpShape[] { ucpPolyShape, ucpPolyShape2, ucpPolyShape3 };
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddStaticBody(transformC, array, null);
		DebugDraw.CreateBox(CameraS.m_mainCamera, transformC, Vector2.up * -75f, 150f, 10f, false);
		DebugDraw.CreateBox(CameraS.m_mainCamera, transformC, Vector2.right * -75f, 10f, 150f, false);
		DebugDraw.CreateBox(CameraS.m_mainCamera, transformC, Vector2.right * 75f, 10f, 150f, false);
		entity = EntityManager.AddEntity();
		transformC = TransformS.AddComponent(entity);
		TransformS.SetPosition(transformC, Vector3.up * -75f + Vector3.right * -75f);
		ucpCircleShape ucpCircleShape = new ucpCircleShape(20f, Vector2.zero, uint.MaxValue, 10f, 0f, 0f, (ucpCollisionType)2, false);
		chipmunkBodyC = ChipmunkProS.AddKinematicBody(transformC, ucpCircleShape, null);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, transformC, Vector2.zero, 20f, false, 16);
		TweenC tweenC = TweenS.AddTransformTween(transformC, TweenedProperty.Position, TweenStyle.CubicInOut, Vector3.up * -75f + Vector3.right * 75f, 2f, 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		entity = EntityManager.AddEntity();
		transformC = TransformS.AddComponent(entity);
		TransformS.SetPosition(transformC, Vector3.up * -20f);
		TransformS.SetRotation(transformC, Vector3.forward * -359f);
		ucpPolyShape ucpPolyShape4 = new ucpPolyShape(50f, 10f, Vector2.zero, uint.MaxValue, 10f, 0.75f, 1f, (ucpCollisionType)2, false);
		chipmunkBodyC = ChipmunkProS.AddKinematicBody(transformC, ucpPolyShape4, null);
		DebugDraw.CreateBox(CameraS.m_mainCamera, transformC, Vector2.zero, 50f, 10f, false);
		tweenC = TweenS.AddTransformTween(transformC, TweenedProperty.Position, TweenStyle.CubicInOut, Vector3.up * 20f, 1.5f, 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		tweenC = TweenS.AddTransformTween(transformC, TweenedProperty.Rotation, TweenStyle.QuadInOut, Vector3.forward * 359f, 5f, 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.QuadInOut);
		tweenC = TweenS.AddTransformTween(transformC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 2f, 0.5f, 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicOut);
		entity = EntityManager.AddEntity();
		transformC = TransformS.AddComponent(entity);
		TransformS.SetPosition(transformC, Vector3.up * 50f + Vector3.right * 5f);
		ucpCircleShape = new ucpCircleShape(10f, Vector2.zero, uint.MaxValue, 10f, 0.5f, 0.5f, (ucpCollisionType)3, false);
		chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, transformC, Vector2.zero, 10f, false, 16);
		entity = EntityManager.AddEntity("constraintTest");
		transformC = TransformS.AddComponent(entity, Vector3.right * 100f);
		ucpCircleShape = new ucpCircleShape(10f, Vector2.zero, uint.MaxValue, 10f, 0.5f, 0.5f, (ucpCollisionType)2, false);
		chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, transformC, Vector2.zero, 10f, false, 16);
		entity = EntityManager.AddEntity();
		TransformC transformC2 = TransformS.AddComponent(entity, "PinJointAnchor1", transformC.transform.position + Vector3.up * 100f);
		TransformC transformC3 = TransformS.AddComponent(entity, "PinJointAnchor2", transformC.transform.position + Vector3.up * 5f);
		ChipmunkProS.AddPinJoint(ChipmunkProS.m_staticBody, chipmunkBodyC, transformC2, transformC3);
		entity = EntityManager.AddEntity("constraintTest");
		transformC = TransformS.AddComponent(entity, Vector3.right * 125f);
		ucpCircleShape = new ucpCircleShape(10f, Vector2.zero, uint.MaxValue, 10f, 0.5f, 0.5f, (ucpCollisionType)2, false);
		chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, transformC, Vector2.zero, 10f, false, 16);
		entity = EntityManager.AddEntity();
		transformC2 = TransformS.AddComponent(entity, "PivotJointAnchor1", transformC.transform.position + Vector3.up * 5f);
		ChipmunkProS.AddPivotJoint(ChipmunkProS.m_staticBody, chipmunkBodyC, transformC2);
		entity = EntityManager.AddEntity("constraintTest");
		transformC = TransformS.AddComponent(entity, Vector3.right * 150f);
		ucpCircleShape = new ucpCircleShape(10f, Vector2.zero, uint.MaxValue, 10f, 0.5f, 0.5f, (ucpCollisionType)2, false);
		chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, transformC, Vector2.zero, 10f, false, 16);
		entity = EntityManager.AddEntity();
		transformC2 = TransformS.AddComponent(entity, "PivotJointAnchor1", transformC.transform.position + Vector3.up * 5f);
		transformC3 = TransformS.AddComponent(entity, "PivotJointAnchor2", transformC.transform.position + Vector3.up * 5f);
		ChipmunkProS.AddPivotJoint2(ChipmunkProS.m_staticBody, chipmunkBodyC, transformC2, transformC3);
		entity = EntityManager.AddEntity("constraintTest");
		transformC = TransformS.AddComponent(entity, Vector3.right * 175f + Vector3.up * 150f);
		ucpCircleShape = new ucpCircleShape(10f, Vector2.zero, uint.MaxValue, 10f, 0.5f, 0.5f, (ucpCollisionType)2, false);
		chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, transformC, Vector2.zero, 10f, false, 16);
		entity = EntityManager.AddEntity();
		transformC2 = TransformS.AddComponent(entity, "SlideJointAnchor1", transformC.transform.position + Vector3.up * -50f);
		transformC3 = TransformS.AddComponent(entity, "SlideJointAnchor2", transformC.transform.position + Vector3.up * -5f);
		ChipmunkProS.AddSlideJoint(ChipmunkProS.m_staticBody, chipmunkBodyC, transformC2, transformC3, 25f, 50f);
		entity = EntityManager.AddEntity("constraintTest");
		transformC = TransformS.AddComponent(entity, Vector3.right * 200f + Vector3.up * 100f);
		ucpCircleShape = new ucpCircleShape(10f, Vector2.zero, uint.MaxValue, 10f, 0.5f, 0.5f, (ucpCollisionType)2, false);
		chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, transformC, Vector2.zero, 10f, false, 16);
		entity = EntityManager.AddEntity();
		transformC2 = TransformS.AddComponent(entity, "GrooveJointGrooveA", transformC.transform.position);
		transformC3 = TransformS.AddComponent(entity, "GrooveJointGrooveB", transformC.transform.position + Vector3.up * -100f);
		TransformC transformC4 = TransformS.AddComponent(entity, "GrooveJointAnchor", transformC.transform.position);
		ChipmunkProS.AddGrooveJoint(ChipmunkProS.m_staticBody, chipmunkBodyC, transformC2, transformC3, transformC4);
		entity = EntityManager.AddEntity("constraintTest");
		transformC = TransformS.AddComponent(entity, Vector3.right * 225f + Vector3.up * 100f);
		ucpCircleShape = new ucpCircleShape(10f, Vector2.zero, uint.MaxValue, 10f, 0.5f, 0.5f, (ucpCollisionType)2, false);
		chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, ucpCircleShape, null);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, transformC, Vector2.zero, 10f, false, 16);
		entity = EntityManager.AddEntity();
		transformC2 = TransformS.AddComponent(entity, "DampedSpringAnchor1", transformC.transform.position);
		transformC3 = TransformS.AddComponent(entity, "DampedSpringAnchor2", transformC.transform.position + Vector3.up * 5f);
		ChipmunkProS.AddDampedSpring(ChipmunkProS.m_staticBody, chipmunkBodyC, transformC2, transformC3, 50f, 200f, 10f);
	}

	// Token: 0x06002AD5 RID: 10965 RVA: 0x001BBDDA File Offset: 0x001BA1DA
	public void handler(ucpCollisionPair _collisionPair, ucpCollisionPhase _collisionPhase)
	{
	}

	// Token: 0x06002AD6 RID: 10966 RVA: 0x001BBDDC File Offset: 0x001BA1DC
	public override void Execute()
	{
		this.ticker++;
		if (this.ticker == 40)
		{
			this.ticker = 0;
			Vector2 vector = Vector2.up * -400f;
			Vector2 vector2;
			vector2..ctor((float)Random.Range(-30, 30), (float)Random.Range(-30, 30));
			ChipmunkProWrapper.ucpSpaceSetGravity(vector + vector2);
		}
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x001BBE42 File Offset: 0x001BA242
	public override void Exit()
	{
		EntityManager.RemoveAllEntities();
	}

	// Token: 0x04002FF6 RID: 12278
	private SpriteSheet m_spriteSheet;

	// Token: 0x04002FF7 RID: 12279
	private int ticker;
}
