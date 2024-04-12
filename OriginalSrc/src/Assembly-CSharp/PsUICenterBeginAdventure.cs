using System;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class PsUICenterBeginAdventure : UICanvas
{
	// Token: 0x060014E2 RID: 5346 RVA: 0x000DA1F0 File Offset: 0x000D85F0
	public PsUICenterBeginAdventure(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.RemoveDrawHandler();
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetMargins(0.025f, RelativeTo.ScreenHeight);
		this.CreateCenterContent();
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x000DA240 File Offset: 0x000D8640
	protected virtual void CreateCenterContent()
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "mapArea");
		uihorizontalList.SetAlign(0.5f, 0.8f);
		uihorizontalList.RemoveDrawHandler();
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.GetMotivationString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ScreenHeight, "#C2FF16", "313131");
		uitext.SetShadowShift(new Vector2(0.5f, -0.2f), 0.1f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.GetGameModeFrame(), null), true, true);
		uifittedSprite.SetHeight(0.25f, RelativeTo.ScreenHeight);
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000DA2E8 File Offset: 0x000D86E8
	protected virtual string GetMotivationString()
	{
		return PsStrings.Get(StringID.GET_MAP_PIECES);
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x000DA2F4 File Offset: 0x000D86F4
	protected virtual string GetGameModeFrame()
	{
		return "item_mode_adventure";
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000DA2FC File Offset: 0x000D86FC
	public override void Step()
	{
		if (this.m_back != null && this.m_back.m_TC.p_entity != null && this.m_back.m_TC.p_entity.m_active && (this.m_back.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Back");
		}
		base.Step();
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000DA389 File Offset: 0x000D8789
	public void RemoveBack()
	{
		this.m_back.Destroy();
		this.m_back = null;
		this.Update();
	}

	// Token: 0x040017A1 RID: 6049
	public PsUIGenericButton m_back;
}
