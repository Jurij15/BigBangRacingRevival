using System;

// Token: 0x020002CB RID: 715
public class PsUICenterStartAdventureFresh : PsUICenterStartAdventure
{
	// Token: 0x06001525 RID: 5413 RVA: 0x000DB765 File Offset: 0x000D9B65
	public PsUICenterStartAdventureFresh(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000DB76E File Offset: 0x000D9B6E
	protected override string GetMotivationString()
	{
		return PsStrings.Get(StringID.GET_SHARDS);
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x000DB77A File Offset: 0x000D9B7A
	protected override string GetGameModeFrame()
	{
		return "hud_shard1";
	}
}
