using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class ContactInfo
{
	// Token: 0x06000400 RID: 1024 RVA: 0x00038F24 File Offset: 0x00037324
	public ContactInfo(ChipmunkBodyC _contactBody, ChipmunkBodyC _cmb, Ground _ground, Vector2 _contactPoint)
	{
		this.m_contactBody = _contactBody;
		this.m_cmb = _cmb;
		this.m_contactPoint = _contactPoint;
		this.m_ground = _ground;
		this.m_unit = null;
		this.m_beganTime = Main.m_resettingGameTime;
		this.m_endTime = -1f;
		this.m_contactCount = 1;
		this.m_began = true;
		this.m_end = false;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00038F88 File Offset: 0x00037388
	public ContactInfo(ChipmunkBodyC _contactBody, ChipmunkBodyC _cmb, Unit _unit, Vector2 _contactPoint)
	{
		this.m_contactBody = _contactBody;
		this.m_cmb = _cmb;
		this.m_contactPoint = _contactPoint;
		this.m_ground = null;
		this.m_unit = _unit;
		this.m_beganTime = Main.m_resettingGameTime;
		this.m_endTime = -1f;
		this.m_contactCount = 1;
		this.m_began = true;
		this.m_end = false;
	}

	// Token: 0x04000540 RID: 1344
	public ChipmunkBodyC m_contactBody;

	// Token: 0x04000541 RID: 1345
	public ChipmunkBodyC m_cmb;

	// Token: 0x04000542 RID: 1346
	public Vector2 m_contactPoint;

	// Token: 0x04000543 RID: 1347
	public Ground m_ground;

	// Token: 0x04000544 RID: 1348
	public Unit m_unit;

	// Token: 0x04000545 RID: 1349
	public int m_contactCount;

	// Token: 0x04000546 RID: 1350
	public bool m_began;

	// Token: 0x04000547 RID: 1351
	public float m_beganTime;

	// Token: 0x04000548 RID: 1352
	public bool m_end;

	// Token: 0x04000549 RID: 1353
	public float m_endTime;
}
