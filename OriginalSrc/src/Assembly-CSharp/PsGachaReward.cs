using System;
using System.Collections.Generic;

// Token: 0x020000AA RID: 170
public class PsGachaReward
{
	// Token: 0x06000388 RID: 904 RVA: 0x00035AD0 File Offset: 0x00033ED0
	public PsGachaReward(Dictionary<string, int> _upgradeItems, Dictionary<string, int> _editorItems, int _bossHandicap, int _coins, int _gems, int _nitros, string _hat)
	{
		this.m_upgradeItems = _upgradeItems;
		this.m_editorItems = _editorItems;
		this.m_boss = _bossHandicap;
		this.m_coins = _coins;
		this.m_gems = _gems;
		this.m_nitros = _nitros;
		this.m_hat = _hat;
	}

	// Token: 0x04000492 RID: 1170
	public Dictionary<string, int> m_upgradeItems;

	// Token: 0x04000493 RID: 1171
	public Dictionary<string, int> m_editorItems;

	// Token: 0x04000494 RID: 1172
	public int m_boss;

	// Token: 0x04000495 RID: 1173
	public int m_coins;

	// Token: 0x04000496 RID: 1174
	public int m_gems;

	// Token: 0x04000497 RID: 1175
	public int m_nitros;

	// Token: 0x04000498 RID: 1176
	public string m_hat;

	// Token: 0x04000499 RID: 1177
	public string m_bonusHat;

	// Token: 0x0400049A RID: 1178
	public int m_bonusMoney;
}
