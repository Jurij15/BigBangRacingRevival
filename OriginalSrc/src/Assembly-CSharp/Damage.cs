using System;

// Token: 0x020000A2 RID: 162
[Serializable]
public class Damage
{
	// Token: 0x06000356 RID: 854 RVA: 0x00031D04 File Offset: 0x00030104
	public Damage()
	{
		this.m_amount = new float[6];
		this.m_amount[(int)((UIntPtr)0)] = 0f;
		this.m_amount[(int)((UIntPtr)1)] = 0f;
		this.m_amount[(int)((UIntPtr)2)] = 0f;
		this.m_amount[(int)((UIntPtr)3)] = 0f;
		this.m_amount[(int)((UIntPtr)4)] = 0f;
		this.m_procDamage = null;
		this.m_procDamageChance = 0f;
		this.m_procBuff = null;
		this.m_procBuffChance = 0f;
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00031D90 File Offset: 0x00030190
	public Damage(DamageType _damageType, float _damageAmount)
	{
		this.m_amount = new float[6];
		this.m_amount[(int)((UIntPtr)1)] = 0f;
		this.m_amount[(int)((UIntPtr)0)] = 0f;
		this.m_amount[(int)((UIntPtr)2)] = 0f;
		this.m_amount[(int)((UIntPtr)3)] = 0f;
		this.m_amount[(int)((UIntPtr)4)] = 0f;
		this.m_amount[(int)((UIntPtr)_damageType)] = _damageAmount;
		this.m_procDamage = null;
		this.m_procDamageChance = 0f;
		this.m_procBuff = null;
		this.m_procBuffChance = 0f;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00031E23 File Offset: 0x00030223
	public void SetDamage(DamageType _damageType, int _amount)
	{
		this.m_amount[(int)_damageType] = (float)_amount;
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00031E2F File Offset: 0x0003022F
	public void SetProcDamage(Damage _damage, float _chance)
	{
		this.m_procDamage = _damage;
		this.m_procDamageChance = _chance;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00031E3F File Offset: 0x0003023F
	public void SetProcBuff(Buff _buff, float _chance)
	{
		this.m_procBuff = _buff;
		this.m_procBuffChance = _chance;
	}

	// Token: 0x0400044B RID: 1099
	public float[] m_amount;

	// Token: 0x0400044C RID: 1100
	public string m_visualFx;

	// Token: 0x0400044D RID: 1101
	public float m_visualFxDuration;

	// Token: 0x0400044E RID: 1102
	public string m_soundFx;

	// Token: 0x0400044F RID: 1103
	public Damage m_procDamage;

	// Token: 0x04000450 RID: 1104
	public float m_procDamageChance;

	// Token: 0x04000451 RID: 1105
	public Buff m_procBuff;

	// Token: 0x04000452 RID: 1106
	public float m_procBuffChance;
}
