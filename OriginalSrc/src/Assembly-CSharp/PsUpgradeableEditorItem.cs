using System;
using System.Collections;

// Token: 0x02000137 RID: 311
public class PsUpgradeableEditorItem : PsEditorItem
{
	// Token: 0x06000964 RID: 2404 RVA: 0x00064CE8 File Offset: 0x000630E8
	public PsUpgradeableEditorItem(string _name)
		: base(_name)
	{
	}

	// Token: 0x040008D4 RID: 2260
	public Hashtable m_upgradeValues;

	// Token: 0x040008D5 RID: 2261
	public int m_upgradeSteps;

	// Token: 0x040008D6 RID: 2262
	public string[] m_upgradePrices;

	// Token: 0x040008D7 RID: 2263
	public string m_rentName;

	// Token: 0x040008D8 RID: 2264
	public string m_rentButton;
}
