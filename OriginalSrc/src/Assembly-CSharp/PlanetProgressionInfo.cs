using System;

// Token: 0x0200042E RID: 1070
public class PlanetProgressionInfo
{
	// Token: 0x06001DAC RID: 7596 RVA: 0x00152566 File Offset: 0x00150966
	public PsPlanetPath GetMainPath()
	{
		return this.m_mainPath;
	}

	// Token: 0x06001DAD RID: 7597 RVA: 0x0015256E File Offset: 0x0015096E
	public PsPlanetPath GetFloatingPath()
	{
		return this.m_floatingPath;
	}

	// Token: 0x06001DAE RID: 7598 RVA: 0x00152576 File Offset: 0x00150976
	public string GetIdentifier()
	{
		return this.m_planetIdentifier;
	}

	// Token: 0x04002082 RID: 8322
	public string m_planetIdentifier;

	// Token: 0x04002083 RID: 8323
	public PsPlanetPath m_mainPath;

	// Token: 0x04002084 RID: 8324
	public PsPlanetPath m_floatingPath;
}
