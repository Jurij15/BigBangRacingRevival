using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class PsUICenterShopCoins : PsUIScrollableBase
{
	// Token: 0x06001850 RID: 6224 RVA: 0x00107A58 File Offset: 0x00105E58
	public PsUICenterShopCoins(UIComponent _parent)
		: base(_parent)
	{
		this.m_purchasing = false;
		PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x00107A88 File Offset: 0x00105E88
	public override void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, "Get more coins", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(-1.5f, 0f, -0.125f, -0.125f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
		uisprite.SetSize(1f, 1f * (frame.height / frame.width), RelativeTo.ParentHeight);
		uisprite.SetHorizontalAlign(0f);
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x00107B94 File Offset: 0x00105F94
	public override void CreateContent(UIComponent _parent)
	{
		this.m_buttons = new List<PsUIGenericButton>();
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenHeight);
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>99</color>", "3,000 COINS", "menu_shop_item_coins_tier1", true, false, false, false);
		psUIGenericButton.m_identifier = "3000coins";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		if (PsMetagameManager.m_playerStats.diamonds < 99)
		{
			psUIGenericButton.SetBlueColors(false);
			psUIGenericButton.RemoveTouchAreas();
		}
		this.m_buttons.Add(psUIGenericButton);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>249</color>", "10,000 COINS", "menu_shop_item_coins_tier2", true, false, false, false);
		psUIGenericButton.m_identifier = "10000coins";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		if (PsMetagameManager.m_playerStats.diamonds < 249)
		{
			psUIGenericButton.SetBlueColors(false);
			psUIGenericButton.RemoveTouchAreas();
		}
		this.m_buttons.Add(psUIGenericButton);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>690</color>", "50,000 COINS", "menu_shop_item_coins_tier3", true, false, false, false);
		psUIGenericButton.m_identifier = "50000coins";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		if (PsMetagameManager.m_playerStats.diamonds < 690)
		{
			psUIGenericButton.SetBlueColors(false);
			psUIGenericButton.RemoveTouchAreas();
		}
		this.m_buttons.Add(psUIGenericButton);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>1900</color>", "250,000 COINS", "menu_shop_item_coins_tier4", true, false, false, false);
		psUIGenericButton.m_identifier = "250000coins";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		if (PsMetagameManager.m_playerStats.diamonds < 1900)
		{
			psUIGenericButton.SetBlueColors(false);
			psUIGenericButton.RemoveTouchAreas();
		}
		this.m_buttons.Add(psUIGenericButton);
		psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetShopButton("<color=#749fc0>5900</color>", "1,000,000 COINS", "menu_shop_item_coins_tier5", true, false, false, false);
		psUIGenericButton.m_identifier = "1000000coins";
		psUIGenericButton.m_TAC.m_letTouchesThrough = true;
		if (PsMetagameManager.m_playerStats.diamonds < 5900)
		{
			psUIGenericButton.SetBlueColors(false);
			psUIGenericButton.RemoveTouchAreas();
		}
		this.m_buttons.Add(psUIGenericButton);
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x00107E38 File Offset: 0x00106238
	public override void Step()
	{
		if (this.m_purchasing)
		{
			base.Step();
			return;
		}
		for (int i = 0; i < this.m_buttons.Count; i++)
		{
			if (this.m_buttons[i].m_hit)
			{
				this.Purchase(this.m_buttons[i].m_identifier);
				this.m_purchasing = true;
				TouchAreaS.Disable();
				break;
			}
		}
		base.Step();
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x00107EB8 File Offset: 0x001062B8
	public void Purchase(string _identifier)
	{
		PsUICenterShopCoins.ShowLoadingPopup();
		if (_identifier == "3000coins")
		{
			PsMetagameManager.m_playerStats.CumulateCoins(3000);
			PsMetagameManager.m_playerStats.CumulateDiamonds(-99);
		}
		else if (_identifier == "10000coins")
		{
			PsMetagameManager.m_playerStats.CumulateCoins(10000);
			PsMetagameManager.m_playerStats.CumulateDiamonds(-249);
		}
		else if (_identifier == "50000coins")
		{
			PsMetagameManager.m_playerStats.CumulateCoins(50000);
			PsMetagameManager.m_playerStats.CumulateDiamonds(-690);
		}
		else if (_identifier == "250000coins")
		{
			PsMetagameManager.m_playerStats.CumulateCoins(250000);
			PsMetagameManager.m_playerStats.CumulateDiamonds(-1900);
		}
		else if (_identifier == "1000000coins")
		{
			PsMetagameManager.m_playerStats.CumulateCoins(1000000);
			PsMetagameManager.m_playerStats.CumulateDiamonds(-5900);
		}
		PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(this.SUCCEED), new Action<HttpC>(this.FAILED), null);
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x00107FE8 File Offset: 0x001063E8
	private void SUCCEED(HttpC _c)
	{
		PsUICenterShopCoins.HideLoadingPopup();
		PsMetagameManager.PlayerDataSetSUCCEED(_c);
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		TouchAreaS.Enable();
		SoundS.PlaySingleShot("/UI/Purchase", Vector2.zero, 1f);
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x00108034 File Offset: 0x00106434
	private void FAILED(HttpC _c)
	{
		PsUICenterShopCoins.HideLoadingPopup();
		Debug.Log("COIN PURCHASE FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		Hashtable setData = _c.objectData as Hashtable;
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, () => PsServerRequest.ServerPlayerSetData(setData, new Action<HttpC>(this.SUCCEED), new Action<HttpC>(this.FAILED), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x001080A2 File Offset: 0x001064A2
	private static void ShowLoadingPopup()
	{
		Debug.Log("Show loading popup", null);
		if (PsUICenterShopCoins.m_waitPopup != null)
		{
			PsUICenterShopCoins.m_waitPopup.Destroy();
			PsUICenterShopCoins.m_waitPopup = null;
		}
		PsUICenterShopCoins.m_waitPopup = new PsUIPurchaseWaitPopup(null);
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x001080D4 File Offset: 0x001064D4
	private static void HideLoadingPopup()
	{
		Debug.Log("Hide loading popup", null);
		if (PsUICenterShopCoins.m_waitPopup != null)
		{
			PsUICenterShopCoins.m_waitPopup.Destroy();
			PsUICenterShopCoins.m_waitPopup = null;
		}
	}

	// Token: 0x04001B10 RID: 6928
	private List<PsUIGenericButton> m_buttons;

	// Token: 0x04001B11 RID: 6929
	private bool m_purchasing;

	// Token: 0x04001B12 RID: 6930
	private static PsUIPurchaseWaitPopup m_waitPopup;
}
