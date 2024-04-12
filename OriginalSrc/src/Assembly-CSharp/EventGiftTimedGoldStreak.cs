using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class EventGiftTimedGoldStreak : EventGiftTimed
{
	// Token: 0x0600059B RID: 1435 RVA: 0x00047A71 File Offset: 0x00045E71
	public EventGiftTimedGoldStreak(Dictionary<string, object> _dict)
	{
		this.m_timedType = EventGiftTimedType.goldCoinStreak;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00047A80 File Offset: 0x00045E80
	public override string GetName()
	{
		return PsStrings.Get(StringID.GIFT_TYPE_GOLD_DAY);
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00047A8C File Offset: 0x00045E8C
	public override void CreateUI(UIComponent _parent)
	{
		UI3DRenderTextureCanvas ui3DRenderTextureCanvas = new UI3DRenderTextureCanvas(_parent, string.Empty, null, false);
		ui3DRenderTextureCanvas.AddGameObject(this.GetStreakGO(CoinStreakStyle.ALL_GOLD), new Vector3(0f, -0.49f, 0f), new Vector3(17f, 180f, 0f));
		ui3DRenderTextureCanvas.m_3DCamera.fieldOfView = 22f;
		ui3DRenderTextureCanvas.m_3DCameraOffset = -4.31f;
		ui3DRenderTextureCanvas.m_3DCamera.nearClipPlane = 1f;
		ui3DRenderTextureCanvas.m_3DCamera.farClipPlane = 10f;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00047B18 File Offset: 0x00045F18
	private GameObject GetStreakGO(CoinStreakStyle _coinstreak)
	{
		ResourcePool.Asset asset = RESOURCE.RewardChest_GameObject;
		switch (_coinstreak)
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
}
