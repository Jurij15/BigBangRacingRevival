using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class Buff
{
	// Token: 0x06000328 RID: 808 RVA: 0x00030124 File Offset: 0x0002E524
	public Buff()
	{
		this.m_identifier = string.Empty;
		this.m_isDebuff = true;
		this.m_isContagious = false;
		this.m_isCancellable = false;
		this.m_interval = -1f;
		this.m_duration = -1f;
		this.m_beganEffect = null;
		this.m_tickEffect = null;
		this.m_endEffect = null;
		this.m_shieldModifier = null;
		this.m_armorModifier = null;
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00030190 File Offset: 0x0002E590
	public virtual void PlayEffects(Unit _unit, BuffState _state, Vector2 _pos)
	{
	}

	// Token: 0x0400040A RID: 1034
	public string m_identifier;

	// Token: 0x0400040B RID: 1035
	public bool m_isDebuff;

	// Token: 0x0400040C RID: 1036
	public bool m_isContagious;

	// Token: 0x0400040D RID: 1037
	public bool m_isCancellable;

	// Token: 0x0400040E RID: 1038
	public float m_interval;

	// Token: 0x0400040F RID: 1039
	public float m_duration;

	// Token: 0x04000410 RID: 1040
	public Damage m_beganEffect;

	// Token: 0x04000411 RID: 1041
	public Damage m_tickEffect;

	// Token: 0x04000412 RID: 1042
	public Damage m_endEffect;

	// Token: 0x04000413 RID: 1043
	public StatModifier m_shieldModifier;

	// Token: 0x04000414 RID: 1044
	public StatModifier m_armorModifier;
}
