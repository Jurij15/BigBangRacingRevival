using System;

// Token: 0x020002C2 RID: 706
public class PsUICenterBeginAdventureFresh : PsUICenterBeginAdventure
{
	// Token: 0x060014E8 RID: 5352 RVA: 0x000DA3A3 File Offset: 0x000D87A3
	public PsUICenterBeginAdventureFresh(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000DA3AC File Offset: 0x000D87AC
	protected override string GetMotivationString()
	{
		return PsStrings.Get(StringID.GET_SHARDS);
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x000DA3B8 File Offset: 0x000D87B8
	protected override string GetGameModeFrame()
	{
		return "hud_shard1";
	}
}
