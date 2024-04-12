using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
public class TestJello : Unit
{
	// Token: 0x060002F2 RID: 754 RVA: 0x0002EE78 File Offset: 0x0002D278
	public TestJello(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name, _graphElement.m_position);
		float num = 150f;
		float num2 = 1f;
		float num3 = 100f;
		float num4 = 100f;
		int num5 = 20;
		float num6 = -(num3 - (float)num5) / 2f;
		float num7 = -(num4 - (float)num5) / 2f;
		int num8 = Mathf.RoundToInt(num3 / (float)num5);
		int num9 = Mathf.RoundToInt(num4 / (float)num5);
		this.m_bodies = new ChipmunkBodyC[num8, num9];
		for (int i = 0; i < num9; i++)
		{
			for (int j = 0; j < num8; j++)
			{
				Vector3 vector;
				vector..ctor(num6 + (float)(j * num5), num7 + (float)(i * num5), 0f);
				TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name, _graphElement.m_position + vector);
				ucpShape ucpShape = new ucpCircleShape(5f, Vector2.zero, 17895696U, 0.1f, 0.5f, 0.5f, (ucpCollisionType)4, false);
				ucpShape.group = base.GetGroup();
				this.m_bodies[j, i] = ChipmunkProS.AddDynamicBody(transformC, ucpShape, null);
				if (j > 0)
				{
					ChipmunkBodyC chipmunkBodyC = this.m_bodies[j - 1, i];
					ChipmunkBodyC chipmunkBodyC2 = this.m_bodies[j, i];
					IntPtr intPtr = ChipmunkProWrapper.ucpDampedSpringNew(chipmunkBodyC.body, chipmunkBodyC2.body, Vector2.zero, Vector2.zero, (float)num5, num, num2);
					ChipmunkProWrapper.ucpAddConstraint(intPtr, -1);
				}
				if (i > 0)
				{
					ChipmunkBodyC chipmunkBodyC3 = this.m_bodies[j, i - 1];
					ChipmunkBodyC chipmunkBodyC4 = this.m_bodies[j, i];
					IntPtr intPtr2 = ChipmunkProWrapper.ucpDampedSpringNew(chipmunkBodyC3.body, chipmunkBodyC4.body, Vector2.zero, Vector2.zero, (float)num5, num, num2);
					ChipmunkProWrapper.ucpAddConstraint(intPtr2, -1);
				}
				if (j > 0 && i > 0)
				{
					ChipmunkBodyC chipmunkBodyC5 = this.m_bodies[j - 1, i];
					ChipmunkBodyC chipmunkBodyC6 = this.m_bodies[j, i - 1];
					IntPtr intPtr3 = ChipmunkProWrapper.ucpDampedSpringNew(chipmunkBodyC5.body, chipmunkBodyC6.body, Vector2.zero, Vector2.zero, Mathf.Sqrt((float)(num5 * num5 * 2)), Mathf.Sqrt(num * num * 2f), num2);
					ChipmunkProWrapper.ucpAddConstraint(intPtr3, -1);
					chipmunkBodyC5 = this.m_bodies[j - 1, i - 1];
					chipmunkBodyC6 = this.m_bodies[j, i];
					intPtr3 = ChipmunkProWrapper.ucpDampedSpringNew(chipmunkBodyC5.body, chipmunkBodyC6.body, Vector2.zero, Vector2.zero, Mathf.Sqrt((float)(num5 * num5 * 2)), Mathf.Sqrt(num * num * 2f), num2);
					ChipmunkProWrapper.ucpAddConstraint(intPtr3, -1);
				}
			}
		}
		if (!this.m_minigame.m_editing)
		{
		}
		this.CreateEditorTouchArea(100f, 100f, null, default(Vector2));
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0002F183 File Offset: 0x0002D583
	private void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		Debug.Log("Goal Reached", null);
		this.m_minigame.m_gameEnded = true;
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0002F19C File Offset: 0x0002D59C
	public override void Update()
	{
		int num = 4;
		Vector3 vector = this.m_bodies[0, 0].TC.transform.position + this.m_bodies[num, 0].TC.transform.position + this.m_bodies[num, num].TC.transform.position + this.m_bodies[0, num].TC.transform.position;
		vector /= 4f;
		DebugDraw.Clear(CameraS.m_mainCamera, this.m_tc);
		for (int i = 1; i < 5; i++)
		{
			DebugDraw.CreateLine(CameraS.m_mainCamera, this.m_tc, this.m_bodies[i - 1, 0].TC.transform.position - vector, this.m_bodies[i, 0].TC.transform.position - vector);
		}
		for (int j = 1; j < 5; j++)
		{
			DebugDraw.CreateLine(CameraS.m_mainCamera, this.m_tc, this.m_bodies[num, j - 1].TC.transform.position - vector, this.m_bodies[num, j].TC.transform.position - vector);
		}
		for (int k = 1; k < 5; k++)
		{
			DebugDraw.CreateLine(CameraS.m_mainCamera, this.m_tc, this.m_bodies[num - (k - 1), num].TC.transform.position - vector, this.m_bodies[num - k, num].TC.transform.position - vector);
		}
		for (int l = 1; l < 5; l++)
		{
			DebugDraw.CreateLine(CameraS.m_mainCamera, this.m_tc, this.m_bodies[0, num - (l - 1)].TC.transform.position - vector, this.m_bodies[0, num - l].TC.transform.position - vector);
		}
		TransformS.SetPosition(this.m_tc, vector);
	}

	// Token: 0x040003D9 RID: 985
	private TransformC m_tc;

	// Token: 0x040003DA RID: 986
	private ChipmunkBodyC[,] m_bodies;
}
