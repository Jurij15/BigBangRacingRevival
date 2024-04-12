using System;
using UnityEngine;

// Token: 0x020004C5 RID: 1221
public class CameraTargetC : BasicComponent
{
	// Token: 0x060022BC RID: 8892 RVA: 0x00190B31 File Offset: 0x0018EF31
	public CameraTargetC()
		: base(ComponentType.CameraTarget)
	{
	}

	// Token: 0x060022BD RID: 8893 RVA: 0x00190B3C File Offset: 0x0018EF3C
	public override void Reset()
	{
		base.Reset();
		this.interpolateSpeed = 0.033333335f;
		this.frameGrowVelocityMultiplier = Vector2.zero;
		this.framePosVelocityMultiplier = Vector2.zero;
		this.frameSlopRadiusMinMax = Vector2.zero;
		this.lastFrameTCPos = Vector3.zero;
		this.frameScaleVelocityMultiplier = 0f;
		this.offset = Vector2.zero;
		this.framePrefab = null;
		this.frameTC = null;
		this.velAngleChangeMult = Vector2.zero;
		this.angleLimits = new Vector2(90f, 90f);
		this.activeRadius = float.MaxValue;
		this.secondaryTargetTransMult = 1f;
		this.frameWorldBounds = new cpBB(float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);
		this.framePeekShiftMax = Vector2.zero;
		this.framePeekShiftMultiplier = 0f;
		this.translatedCenter = Vector2.zero;
	}

	// Token: 0x040028CE RID: 10446
	public TransformC targetTC;

	// Token: 0x040028CF RID: 10447
	public Vector3 lastTargetTCPos;

	// Token: 0x040028D0 RID: 10448
	public TransformC frameTC;

	// Token: 0x040028D1 RID: 10449
	public Vector3 lastFrameTCPos;

	// Token: 0x040028D2 RID: 10450
	public cpBB targetFrame;

	// Token: 0x040028D3 RID: 10451
	public Vector2 offset;

	// Token: 0x040028D4 RID: 10452
	public float secondaryTargetTransMult;

	// Token: 0x040028D5 RID: 10453
	public Vector2 framePeek;

	// Token: 0x040028D6 RID: 10454
	public Vector2 translatedCenter;

	// Token: 0x040028D7 RID: 10455
	public float interpolateSpeed;

	// Token: 0x040028D8 RID: 10456
	public float frameScaleVelocityMultiplier;

	// Token: 0x040028D9 RID: 10457
	public Vector2 frameGrowVelocityMultiplier;

	// Token: 0x040028DA RID: 10458
	public Vector2 framePosVelocityMultiplier;

	// Token: 0x040028DB RID: 10459
	public Vector2 frameSlopRadiusMinMax;

	// Token: 0x040028DC RID: 10460
	public Vector2 velAngleChangeMult;

	// Token: 0x040028DD RID: 10461
	public Vector2 angleLimits;

	// Token: 0x040028DE RID: 10462
	public float activeRadius;

	// Token: 0x040028DF RID: 10463
	public Vector2 framePeekShiftMax;

	// Token: 0x040028E0 RID: 10464
	public float framePeekShiftMultiplier;

	// Token: 0x040028E1 RID: 10465
	public cpBB frameWorldBounds;

	// Token: 0x040028E2 RID: 10466
	public PrefabC framePrefab;
}
