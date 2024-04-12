using System;

// Token: 0x020002B0 RID: 688
public class PsUICenterLoseAdventureFresh : PsUICenterWinAdventure
{
	// Token: 0x060014AA RID: 5290 RVA: 0x000D66C5 File Offset: 0x000D4AC5
	public PsUICenterLoseAdventureFresh(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x000D66CE File Offset: 0x000D4ACE
	protected override string GetCollectableName()
	{
		return PsStrings.Get(StringID.SHARDS_PLURAL);
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000D66DA File Offset: 0x000D4ADA
	protected override string GetCollactableFrame(int _index)
	{
		return "menu_debrief_shard" + (_index + 1);
	}
}
