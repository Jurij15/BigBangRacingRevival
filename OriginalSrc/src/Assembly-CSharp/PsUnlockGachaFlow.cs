using System;

// Token: 0x02000208 RID: 520
public class PsUnlockGachaFlow : Flow
{
	// Token: 0x06000F1A RID: 3866 RVA: 0x0008FE90 File Offset: 0x0008E290
	public PsUnlockGachaFlow(Action _proceed, Action _cancel, int _diamondAmount)
		: base(_proceed, _cancel, null)
	{
		if (PsMetagameManager.m_playerStats.diamonds >= _diamondAmount)
		{
			this.Proceed.Invoke();
		}
		else
		{
			new PsGetDiamondsFlow(null, _cancel, null);
		}
	}
}
