using System;

// Token: 0x02000213 RID: 531
public class GameBeginHeatStateFresh : GameBeginHeatState
{
	// Token: 0x06000F63 RID: 3939 RVA: 0x000917E4 File Offset: 0x0008FBE4
	public override PsUIBasePopup GetPopup()
	{
		return new PsUIBasePopup(typeof(PsUICenterBeginRacingFresh), typeof(PsUITopBeginRacing), null, null, false, true, InitialPage.Center, false, false, false);
	}
}
