using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class Ground : BasicAssembledClass
{
	// Token: 0x060003C4 RID: 964 RVA: 0x00036BB4 File Offset: 0x00034FB4
	public Ground(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_elasticity = 0.5f;
		this.m_friction = 0.5f;
		this.m_surfaceVelocity = Vector2.zero;
		this.m_smoothingAngle = 90f;
		this.m_depth = 300f;
		this.m_zOffset = -10f;
		this.m_segmentRadius = 10f;
		this.m_beltMaterialResourceName = string.Empty;
		this.m_frontMaterialResourceName = string.Empty;
		this.m_tireRollSound = null;
		this.m_drawSound = null;
		this.m_driveFX = null;
		this.m_drawFx = null;
		this.m_rectBrush = false;
		this.m_marchHard = false;
		this.m_destructible = false;
		this.m_skiddable = false;
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00036C63 File Offset: 0x00035063
	public virtual void Update()
	{
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00036C65 File Offset: 0x00035065
	public virtual void StartedContactWithUnit(UnitC _unitC, ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
	}

	// Token: 0x040004D6 RID: 1238
	public string m_groundItemIdentifier;

	// Token: 0x040004D7 RID: 1239
	public string m_editorWheelFrameName;

	// Token: 0x040004D8 RID: 1240
	public float m_elasticity;

	// Token: 0x040004D9 RID: 1241
	public float m_friction;

	// Token: 0x040004DA RID: 1242
	public Vector2 m_surfaceVelocity;

	// Token: 0x040004DB RID: 1243
	public float m_smoothingAngle;

	// Token: 0x040004DC RID: 1244
	public float m_depth;

	// Token: 0x040004DD RID: 1245
	public float m_zOffset;

	// Token: 0x040004DE RID: 1246
	public float m_segmentRadius;

	// Token: 0x040004DF RID: 1247
	public string m_beltMaterialResourceName;

	// Token: 0x040004E0 RID: 1248
	public string m_frontMaterialResourceName;

	// Token: 0x040004E1 RID: 1249
	public string m_groundPropStorageName;

	// Token: 0x040004E2 RID: 1250
	public bool m_isBurning;

	// Token: 0x040004E3 RID: 1251
	public bool m_isFrozen;

	// Token: 0x040004E4 RID: 1252
	public bool m_isElectrified;

	// Token: 0x040004E5 RID: 1253
	public string m_tireRollSound;

	// Token: 0x040004E6 RID: 1254
	public string m_drawSound;

	// Token: 0x040004E7 RID: 1255
	public string m_driveFX;

	// Token: 0x040004E8 RID: 1256
	public string m_drawFx;

	// Token: 0x040004E9 RID: 1257
	public bool m_rectBrush;

	// Token: 0x040004EA RID: 1258
	public bool m_marchHard;

	// Token: 0x040004EB RID: 1259
	public bool m_destructible;

	// Token: 0x040004EC RID: 1260
	public bool m_skiddable;
}
