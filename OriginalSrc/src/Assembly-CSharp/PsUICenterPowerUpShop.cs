using System;

// Token: 0x020002EC RID: 748
public class PsUICenterPowerUpShop : PsUIHeaderedCanvas
{
	// Token: 0x0600161B RID: 5659 RVA: 0x000E6CEC File Offset: 0x000E50EC
	public PsUICenterPowerUpShop(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.9f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0.012f, 0.06f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x000E6DB0 File Offset: 0x000E51B0
	public void CreateHeaderContent(UIComponent _parent)
	{
		string text = "Buy a powerup!";
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetSpacing(0.001f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, text + " ", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, "#95e5ff", "#000000");
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x000E6E10 File Offset: 0x000E5210
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x000E6E2F File Offset: 0x000E522F
	public void BuyPowerUp(PsPowerUp _powerUp, int m_price)
	{
		Debug.LogError("setting power up: " + _powerUp.ToString());
		(PsState.m_activeGameLoop as PsGameLoopAdventureBattle).m_startPowerUp = _powerUp;
		(this.GetRoot() as PsUIBasePopup).CallAction("Bought");
	}
}
