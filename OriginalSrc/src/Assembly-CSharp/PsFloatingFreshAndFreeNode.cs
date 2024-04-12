using System;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class PsFloatingFreshAndFreeNode : PsFloatingPlanetNode
{
	// Token: 0x0600087D RID: 2173 RVA: 0x0005E88C File Offset: 0x0005CC8C
	public PsFloatingFreshAndFreeNode(PsTimedEventLoop _loop, bool _tutorial = false)
		: base(_loop, _tutorial)
	{
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0005E896 File Offset: 0x0005CC96
	public override void SetPrefabName()
	{
		this.m_prefabName = "FloatingPlatformDailyDiamondsPrefab";
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0005E8A4 File Offset: 0x0005CCA4
	public override void CreateUI()
	{
		base.CreateUI();
		Frame frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame("menu_hovertext_new", null);
		if (PsMetagameManager.m_doubleValueGoodOrBadEvent != null && PsMetagameManager.m_doubleValueGoodOrBadEvent.timeToStart <= 0 && PsMetagameManager.m_doubleValueGoodOrBadEvent.timeLeft > 0)
		{
			frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame("menu_hovertext_2x", null);
		}
		SpriteC spriteC = SpriteS.AddComponent(this.m_domeNumberTC, frame, this.m_planetNodeSpriteSheet);
		SpriteS.SetDimensions(spriteC, 3f * (frame.width / frame.height), 3f);
		SpriteS.SetColor(spriteC, new Color(1f, 1f, 1f));
		SpriteS.SetOffset(spriteC, new Vector3(-2.5f, 7.5f, 0f), 20f);
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0005E979 File Offset: 0x0005CD79
	public override string GetTransformName()
	{
		return "FloaterFreshAndFree";
	}
}
