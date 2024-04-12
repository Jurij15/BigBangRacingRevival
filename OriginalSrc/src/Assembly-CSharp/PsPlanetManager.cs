using System;
using System.Collections.Generic;

// Token: 0x0200012C RID: 300
public static class PsPlanetManager
{
	// Token: 0x06000930 RID: 2352 RVA: 0x00063888 File Offset: 0x00061C88
	public static PsPlanet GetCurrentPlanet()
	{
		return PsPlanetManager.m_currentPlanet;
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x00063890 File Offset: 0x00061C90
	public static void AddPlanet(PsPlanet _planet)
	{
		Debug.LogWarning("Planet Added");
		if (!PsPlanetManager.m_planets.Contains(_planet))
		{
			PsPlanetManager.m_planets.Add(_planet);
		}
		if (PsPlanetManager.m_currentPlanet != null)
		{
			PsPlanetManager.m_currentPlanet.DestroySpaceScene();
			PsPlanetManager.m_currentPlanet = null;
		}
		PsPlanetManager.m_currentPlanet = _planet;
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x000638E2 File Offset: 0x00061CE2
	public static void Update()
	{
		if (PsPlanetManager.m_currentPlanet != null)
		{
			PsPlanetManager.m_currentPlanet.Update();
		}
	}

	// Token: 0x04000894 RID: 2196
	public static List<PsPlanet> m_planets = new List<PsPlanet>();

	// Token: 0x04000895 RID: 2197
	private static PsPlanet m_currentPlanet;

	// Token: 0x04000896 RID: 2198
	public static PsTimedEvents m_timedEvents;
}
