using System;
using UnityEngine;

// Token: 0x020005B8 RID: 1464
public class AutoGeometryTestState : BasicState
{
	// Token: 0x06002ACB RID: 10955 RVA: 0x001BB3B5 File Offset: 0x001B97B5
	public override void Enter(IStatedObject _parent)
	{
		DebugDraw.m_lineWidth = 0.2f;
	}

	// Token: 0x06002ACC RID: 10956 RVA: 0x001BB3C1 File Offset: 0x001B97C1
	public override void Execute()
	{
		if (Input.GetMouseButton(0))
		{
		}
	}

	// Token: 0x06002ACD RID: 10957 RVA: 0x001BB3CE File Offset: 0x001B97CE
	public override void Exit()
	{
		AutoGeometryManager.DestroyAllLayers();
		EntityManager.RemoveAllEntities();
	}

	// Token: 0x04002FF5 RID: 12277
	private AutoGeometryLayer m_agLayer;
}
