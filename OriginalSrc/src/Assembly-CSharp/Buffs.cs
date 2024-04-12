using System;
using System.Collections;

// Token: 0x02000098 RID: 152
public static class Buffs
{
	// Token: 0x0600032D RID: 813 RVA: 0x00030219 File Offset: 0x0002E619
	public static Buff GetBuff(string _identifier)
	{
		if (Buffs.m_buffs.ContainsKey(_identifier))
		{
			return Buffs.m_buffs[_identifier] as Buff;
		}
		return null;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00030240 File Offset: 0x0002E640
	public static void AddBuff(Buff _buff)
	{
		if (!Buffs.m_buffs.ContainsKey(_buff.m_identifier))
		{
			Buffs.m_buffs.Add(_buff.m_identifier, _buff);
		}
		else
		{
			Debug.LogError("trying to add dublicate buff " + _buff.m_identifier);
		}
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0003028D File Offset: 0x0002E68D
	public static void Initialize()
	{
	}

	// Token: 0x04000422 RID: 1058
	public static Hashtable m_buffs = new Hashtable();
}
