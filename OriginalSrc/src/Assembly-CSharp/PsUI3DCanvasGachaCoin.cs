using System;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class PsUI3DCanvasGachaCoin : PsUI3DCanvasGacha
{
	// Token: 0x06001419 RID: 5145 RVA: 0x000CA905 File Offset: 0x000C8D05
	public PsUI3DCanvasGachaCoin(UIComponent _parent, string _tag, Vector3 _offset)
		: base(_parent, _tag, _offset)
	{
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x000CA910 File Offset: 0x000C8D10
	protected override void WinEffect()
	{
		base.WinEffect();
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x000CA918 File Offset: 0x000C8D18
	public override void SetPrizeGameObject(GameObject _go)
	{
		base.SetPrizeGameObject(_go);
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x000CA921 File Offset: 0x000C8D21
	public override bool IsSame<T>(T _first, T _second)
	{
		return (CoinStreakStyle)((object)_first) == (CoinStreakStyle)((object)_second);
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x000CA93C File Offset: 0x000C8D3C
	public override GameObject GetOneGO<T>(T _item)
	{
		CoinStreakStyle coinStreakStyle = (CoinStreakStyle)((object)_item);
		ResourcePool.Asset asset = RESOURCE.RewardChest_GameObject;
		switch (coinStreakStyle)
		{
		case CoinStreakStyle.COPPER_AND_GOLD:
			asset = RESOURCE.StreakGoldCopper_GameObject;
			break;
		case CoinStreakStyle.ALL_GOLD:
			asset = RESOURCE.StreakAllGold_GameObject;
			break;
		case CoinStreakStyle.GOLD_HOARDER:
			asset = RESOURCE.StreakGoldHoarder_GameObject;
			break;
		case CoinStreakStyle.GOLD_MANIAC:
			asset = RESOURCE.StreakGoldManiac_GameObject;
			break;
		case CoinStreakStyle.ROYAL:
			asset = RESOURCE.StreakRoyal_GameObject;
			break;
		case CoinStreakStyle.ALL_DIAMONDS:
			asset = RESOURCE.StreakAllDiamonds_GameObject;
			break;
		}
		return Object.Instantiate<GameObject>(ResourceManager.GetGameObject(asset));
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x000CA9D0 File Offset: 0x000C8DD0
	public override bool IsBigWin<T>(T _item)
	{
		CoinStreakStyle coinStreakStyle = (CoinStreakStyle)((object)_item);
		return coinStreakStyle != CoinStreakStyle.ALL_GOLD && coinStreakStyle != CoinStreakStyle.COPPER_AND_GOLD;
	}
}
