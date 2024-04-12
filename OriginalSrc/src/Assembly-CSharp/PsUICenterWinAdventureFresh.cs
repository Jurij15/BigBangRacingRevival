using System;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class PsUICenterWinAdventureFresh : PsUICenterWinAdventure
{
	// Token: 0x0600160A RID: 5642 RVA: 0x000E69B2 File Offset: 0x000E4DB2
	public PsUICenterWinAdventureFresh(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x000E69BC File Offset: 0x000E4DBC
	protected override void FlyingReward(UIComponent _uicomponent)
	{
		PsMetagameManager.m_menuResourceView.CreateFlyingResources(PsState.m_bigShardValue, this.m_camera.WorldToScreenPoint(_uicomponent.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f, ResourceType.Shards, 0f, null, null, null, null, default(Vector2));
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x000E6A30 File Offset: 0x000E4E30
	public override void Step()
	{
		PsGameModeAdventureFresh psGameModeAdventureFresh = PsState.m_activeGameLoop.m_gameMode as PsGameModeAdventureFresh;
		if (psGameModeAdventureFresh != null && PsMetagameManager.m_menuResourceView.m_cumulateResourceDidWrap && psGameModeAdventureFresh.m_diamondChange > 0 && !this.m_diamondAdded)
		{
			this.m_diamondAdded = true;
			PsMetagameManager.m_menuResourceView.CreateAddedResources(ResourceType.Diamonds, 1, 0f);
		}
		base.Step();
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x000E6A97 File Offset: 0x000E4E97
	protected override string GetCollectableName()
	{
		return PsStrings.Get(StringID.SHARDS_PLURAL);
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x000E6AA3 File Offset: 0x000E4EA3
	protected override string GetCollactableFrame(int _index)
	{
		return "menu_debrief_shard" + (_index + 1);
	}

	// Token: 0x040018DD RID: 6365
	private bool m_diamondAdded;
}
