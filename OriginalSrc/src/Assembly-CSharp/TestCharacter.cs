using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class TestCharacter : Character
{
	// Token: 0x06000147 RID: 327 RVA: 0x0000F59C File Offset: 0x0000D99C
	public TestCharacter(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_group = (uint)(this.m_entity.m_index + 1000);
		this.m_scale = 0.8f;
		float num = 50f * this.m_scale;
		float num2 = 60f * this.m_scale;
		this.m_mainTC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_mainTC, _graphElement.m_position, _graphElement.m_rotation);
		this.m_torsoShape = new ucpPolyShape(num, num2, Vector2.zero, 17895696U, 16.666666f, 0f, 0f, (ucpCollisionType)3, false);
		this.m_torsoShape.group = this.m_group;
		float num3 = num * 0.5f;
		Vector2 vector;
		vector..ctor(0f, num2 * 0.5f);
		ucpCircleShape ucpCircleShape = new ucpCircleShape(num3, vector, 17895696U, 16.666666f, 0f, 0f, (ucpCollisionType)3, false);
		ucpCircleShape.group = this.m_group;
		this.m_feetShape = new ucpCircleShape(num3, -vector, 17895696U, 16.666666f, 0f, 0.9f, (ucpCollisionType)3, false);
		this.m_feetShape.group = this.m_group;
		this.m_mainBody = ChipmunkProS.AddDynamicBody(this.m_mainTC, new ucpShape[] { this.m_torsoShape, ucpCircleShape, this.m_feetShape }, null);
		this.m_mainBody.customComponent = this.m_unitC;
		DebugDraw.defaultColor = new Color(0f, 0f, 0f, 1f);
		DebugDraw.CreateBox(CameraS.m_mainCamera, this.m_mainTC, new Vector2(0f, 0f), num, num2, false);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, this.m_mainTC, vector, num3, false, 16);
		DebugDraw.CreateCircle(CameraS.m_mainCamera, this.m_mainTC, -vector, num3, false, 16);
		ChipmunkProWrapper.ucpBodySetMoment(this.m_mainBody.body, float.PositiveInfinity);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_mainBody, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, true, true);
			ChipmunkProS.AddCollisionHandler(this.m_mainBody, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, true, false);
			ChipmunkProS.AddCollisionHandler(this.m_mainBody, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)3, (ucpCollisionType)3, true, true, false);
			this.CreateCamera();
			this.m_minigame.SetPlayer(this, this.m_mainTC, typeof(TwoPlusOneButtonController));
		}
		this.CreateEditorTouchArea(num, num2 + num3, null, default(Vector2));
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000F857 File Offset: 0x0000DC57
	public void CreateCamera()
	{
		if (!this.m_minigame.m_editing)
		{
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000F86C File Offset: 0x0000DC6C
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing)
		{
			this.m_leftPressed = Controller.GetButtonState("LeftButton1") == ControllerButtonState.ON;
			this.m_rightPressed = Controller.GetButtonState("LeftButton2") == ControllerButtonState.ON;
			this.m_jumpPressed = Controller.GetButtonState("RightButton") == ControllerButtonState.ON;
			this.m_ticksSinceLastJumpPress++;
			if (this.m_jumpPressed)
			{
				this.m_ticksSinceLastJumpPress = 0;
			}
			this.m_isOnGround = this.m_contactState == ContactState.OnContact;
			this.m_feetNormal = this.m_feetNormal.normalized;
			this.m_torsoNormal = this.m_torsoNormal.normalized;
			if (Mathf.Abs(this.m_feetNormal.x) > 0.9f)
			{
				this.m_feetTouchingGround = false;
			}
			float num = Mathf.Lerp(1f, 1.5f, Mathf.Sqrt(Mathf.Abs(this.m_feetNormal.x))) * 8f;
			float num2 = 0f;
			if (this.m_leftPressed)
			{
				num2 = -0.5f;
			}
			else if (this.m_rightPressed)
			{
				num2 = 0.5f;
			}
			if (num2 == 0f)
			{
				ChipmunkProWrapper.ucpShapeSetFriction(this.m_feetShape.shapePtr, 0.9f);
			}
			else if (this.m_isOnGround && !this.m_isJumping)
			{
				if (Mathf.Abs(this.m_torsoNormal.x) < 0.8f)
				{
					this.m_feetNormal.x = ToolBox.limitBetween(this.m_feetNormal.x, -0.5f, 0.5f);
				}
				else
				{
					this.m_feetNormal.x = 0f;
				}
				Vector2 vector = -new Vector2(this.m_feetNormal.y, -this.m_feetNormal.x);
				Vector2 vector2 = vector * num2 * num;
				ChipmunkProWrapper.ucpShapeSetFriction(this.m_feetShape.shapePtr, 0f);
				if (this.m_feetTouchingGround && this.m_collisionTargetBody.Count > 0)
				{
					for (int i = 0; i < this.m_collisionTargetBody.Count; i++)
					{
						Vector2 vector3 = vector2 * ChipmunkProWrapper.ucpBodyGetMass(this.m_collisionTargetBody[i].body) * 0.9f;
					}
				}
			}
			else
			{
				ChipmunkProWrapper.ucpShapeSetFriction(this.m_feetShape.shapePtr, 0f);
			}
			bool flag = (this.m_jumpPressed && this.m_feetTouchingGround) || this.m_ticksSinceLastJumpPress < 5;
			if (flag && !this.m_isJumping && this.m_isOnGround)
			{
				Vector2 vector4 = ChipmunkProWrapper.ucpBodyGetVel(this.m_mainBody.body);
				ChipmunkProWrapper.ucpBodySetVel(this.m_mainBody.body, new Vector2(vector4.x, 0f));
				this.m_isJumping = true;
				this.m_isOnGround = false;
				this.m_jumpDelay = 10;
				this.m_jumpReleased = false;
				this.m_ticksSinceLastJumpPress = 999;
			}
			if (!this.m_jumpPressed)
			{
				this.m_jumpReleased = true;
			}
			if (this.m_jumpPressed && this.m_isJumping && !this.m_airJumpUsed && this.m_jumpReleased)
			{
				Vector2 vector5 = ChipmunkProWrapper.ucpBodyGetVel(this.m_mainBody.body);
				ChipmunkProWrapper.ucpBodySetVel(this.m_mainBody.body, new Vector2(vector5.x, 0f));
				this.m_airJumpUsed = true;
				this.m_isJumping = true;
				this.m_jumpDelay = 10;
				this.m_jumpReleased = false;
				this.m_ticksSinceLastJumpPress = 999;
			}
			if (this.m_isJumping)
			{
				if (this.m_jumpDelay > 0)
				{
					this.m_jumpDelay--;
				}
				if (this.m_jumpDelay == 0 && this.m_feetTouchingGround && this.m_isOnGround && !flag)
				{
					this.m_isJumping = false;
					this.m_airJumpUsed = false;
				}
			}
			this.m_feetTouchingGround = false;
			this.m_feetNormal = Vector2.zero;
			this.m_torsoNormal = Vector2.zero;
			this.m_collisionWorldPoint.Clear();
			this.m_collisionTargetBody.Clear();
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000FCB0 File Offset: 0x0000E0B0
	private void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (_pair.shapeA == this.m_feetShape.shapePtr)
		{
			if (_phase == ucpCollisionPhase.Begin || _phase == ucpCollisionPhase.Persist)
			{
				this.m_feetTouchingGround = true;
				this.m_feetNormal += _pair.normal;
				ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
				if (ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) != ucpBodyType.STATIC && !this.m_collisionTargetBody.Contains(chipmunkBodyC))
				{
					this.m_collisionWorldPoint.Add(_pair.point);
					this.m_collisionTargetBody.Add(chipmunkBodyC);
				}
			}
		}
		if (_pair.shapeA == this.m_torsoShape.shapePtr)
		{
			if (_phase == ucpCollisionPhase.Begin || _phase == ucpCollisionPhase.Persist)
			{
				this.m_torsoNormal += _pair.normal;
			}
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000FDAD File Offset: 0x0000E1AD
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x04000104 RID: 260
	private const float WEIGHT = 50f;

	// Token: 0x04000105 RID: 261
	private const float FRICTION = 0.9f;

	// Token: 0x04000106 RID: 262
	public TransformC m_mainTC;

	// Token: 0x04000107 RID: 263
	public ChipmunkBodyC m_mainBody;

	// Token: 0x04000108 RID: 264
	public ucpCircleShape m_feetShape;

	// Token: 0x04000109 RID: 265
	public ucpPolyShape m_torsoShape;

	// Token: 0x0400010A RID: 266
	public uint m_group;

	// Token: 0x0400010B RID: 267
	public float m_scale;

	// Token: 0x0400010C RID: 268
	public bool m_leftPressed;

	// Token: 0x0400010D RID: 269
	public bool m_rightPressed;

	// Token: 0x0400010E RID: 270
	public bool m_jumpPressed;

	// Token: 0x0400010F RID: 271
	public bool m_jumpReleased;

	// Token: 0x04000110 RID: 272
	public int m_ticksSinceLastJumpPress;

	// Token: 0x04000111 RID: 273
	public bool m_isOnGround;

	// Token: 0x04000112 RID: 274
	public Vector2 m_torsoNormal;

	// Token: 0x04000113 RID: 275
	public bool m_isJumping;

	// Token: 0x04000114 RID: 276
	public bool m_airJumpUsed;

	// Token: 0x04000115 RID: 277
	public int m_jumpDelay;

	// Token: 0x04000116 RID: 278
	public int m_airTimer;

	// Token: 0x04000117 RID: 279
	public int m_groundTimer;

	// Token: 0x04000118 RID: 280
	public bool m_feetTouchingGround;

	// Token: 0x04000119 RID: 281
	public Vector2 m_feetNormal;

	// Token: 0x0400011A RID: 282
	public List<Vector2> m_collisionWorldPoint = new List<Vector2>();

	// Token: 0x0400011B RID: 283
	public List<ChipmunkBodyC> m_collisionTargetBody = new List<ChipmunkBodyC>();
}
