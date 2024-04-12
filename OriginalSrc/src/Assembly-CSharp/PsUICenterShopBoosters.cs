using System;
using System.Collections;
using System.Collections.Generic;
using AdMediation;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class PsUICenterShopBoosters : PsUIScrollableBase
{
	// Token: 0x06001842 RID: 6210 RVA: 0x00107278 File Offset: 0x00105678
	public PsUICenterShopBoosters(UIComponent _parent)
		: base(_parent)
	{
		this.m_purchasing = false;
		PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x001072C0 File Offset: 0x001056C0
	public override void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, "Get more boosters", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		UICanvas uicanvas = new UICanvas(uitext, false, string.Empty, null, string.Empty);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(-1.5f, 0f, -0.125f, -0.125f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("hud_boost_boost", null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
		uisprite.SetSize(1f, 1f * (frame.height / frame.width), RelativeTo.ParentHeight);
		uisprite.SetHorizontalAlign(0f);
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x001073CC File Offset: 0x001057CC
	public override void CreateContent(UIComponent _parent)
	{
		this.m_buttons = new List<PsUIGenericButton>();
		this.m_freeBooster = PsState.m_activeGameLoop.m_freeConsumableUnlock == "Booster" && PsMetagameManager.m_playerStats.boosters == 0;
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenHeight);
		if (PsAdMediation.adsAvailable() || this.m_freeBooster)
		{
			this.m_adButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			if (this.m_freeBooster)
			{
				this.m_adButton.SetShopButton("FREE ", "1 BOOSTER", "hud_boost_boost", false, false, false, false);
			}
			else
			{
				this.m_adButton.SetShopButton("VIDEO", "FREE BOOSTER", "hud_boost_boost", false, false, false, false);
			}
			this.m_adButton.m_TAC.m_letTouchesThrough = true;
			this.m_adButton.m_identifier = "1BoosterAd";
		}
		for (int i = 0; i < this.m_boostPrices.GetLength(0); i++)
		{
			PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			string text = this.m_boostPrices[i, 1].ToString();
			int num = this.m_boostPrices[i, 0];
			string text2 = ((num <= 1) ? " BOOSTER" : " BOOSTERS");
			psUIGenericButton.SetShopButton(text, num + text2, "hud_boost_boost", false, true, false, false);
			psUIGenericButton.m_TAC.m_letTouchesThrough = true;
			psUIGenericButton.m_identifier = num + "Booster";
			this.m_buttons.Add(psUIGenericButton);
		}
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x001075B0 File Offset: 0x001059B0
	public override void Step()
	{
		if (this.m_purchasing)
		{
			base.Step();
			return;
		}
		if (this.m_createTutorialArrow)
		{
			if (this.m_tutorialArrow == null && this.m_adButton != null)
			{
				this.m_tutorialArrow = new PsUITutorialArrowUI(this.m_adButton, true, null, 2f, default(Vector3), false);
			}
			this.m_createTutorialArrow = false;
		}
		for (int i = 0; i < this.m_boostPrices.GetLength(0); i++)
		{
			if (this.m_buttons[i].m_hit)
			{
				TouchAreaS.Disable();
				this.Purchase(this.m_boostPrices[i, 0], this.m_boostPrices[i, 1]);
				this.m_purchasing = true;
				break;
			}
		}
		if (!this.m_purchasing && this.m_adButton != null && this.m_adButton.m_hit)
		{
			if (this.m_freeBooster)
			{
				TouchAreaS.Disable();
				this.Purchase(1, 0);
				this.m_purchasing = true;
			}
			else
			{
				TouchAreaS.Disable();
				PsAdMediation.ShowAd(new Action<AdResult>(this.AdViewCallback), null);
			}
		}
		base.Step();
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x001076E4 File Offset: 0x00105AE4
	public void Purchase(int _boostCount, int _coinPrice)
	{
		if (_coinPrice <= PsMetagameManager.m_playerStats.coins)
		{
			PsUICenterShopBoosters.ShowLoadingPopup();
			this.SetBoosters(_boostCount, -_coinPrice, 0);
		}
		else
		{
			this.OpenCoinPopup(_boostCount, _coinPrice);
		}
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x00107712 File Offset: 0x00105B12
	private void AdViewCallback(AdResult _result)
	{
		Debug.Log("Ad result: " + _result.ToString(), null);
		if (_result == AdResult.Finished)
		{
			PsUICenterShopBoosters.ShowLoadingPopup();
			this.SetBoosters(1, 0, 0);
		}
		else
		{
			TouchAreaS.Enable();
		}
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x00107750 File Offset: 0x00105B50
	private void OpenCoinPopup(int _boostAmount, int _coinAmount)
	{
		TouchAreaS.Enable();
		PsMetagameManager.m_coinsToDiamondsConvertAmount = _coinAmount - PsMetagameManager.m_playerStats.coins;
		this.m_popup = new PsUIBasePopup(typeof(PsUICenterNotEnoughCoinsConversion), null, null, null, true, true, InitialPage.Center, false, false, false);
		this.m_popup.SetAction("Exit", new Action(this.DestroyPopup));
		this.m_popup.SetAction("Upgrade", delegate
		{
			this.BuyWithDiamonds(_boostAmount, _boostAmount);
		});
		TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x0010782C File Offset: 0x00105C2C
	public void DestroyPopup()
	{
		this.m_purchasing = false;
		this.m_popup.Destroy();
		this.m_popup = null;
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x00107848 File Offset: 0x00105C48
	public void BuyWithDiamonds(int _boostAmount, int _coinAmount)
	{
		TouchAreaS.Disable();
		this.DestroyPopup();
		int num = PsMetagameManager.CoinsToDiamonds(PsMetagameManager.m_coinsToDiamondsConvertAmount, true);
		this.SetBoosters(_boostAmount, -PsMetagameManager.m_playerStats.coins, num);
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x00107880 File Offset: 0x00105C80
	public void SetBoosters(int _boughtBoosterCount, int _cumulateCoins, int _cumulateDiamonds = 0)
	{
		PsMetagameManager.m_playerStats.CumulateBoosters(_boughtBoosterCount);
		if (_cumulateCoins != 0)
		{
			PsMetagameManager.m_playerStats.CumulateCoins(Math.Abs(_cumulateCoins) * -1);
		}
		if (_cumulateDiamonds != 0)
		{
			PsMetagameManager.m_playerStats.CumulateDiamonds(Math.Abs(_cumulateDiamonds) * -1);
		}
		PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(this.SUCCEED), new Action<HttpC>(this.FAILED), null);
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x001078EC File Offset: 0x00105CEC
	private void SUCCEED(HttpC _c)
	{
		PsUICenterShopBoosters.HideLoadingPopup();
		PsMetagameManager.PlayerDataSetSUCCEED(_c);
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		TouchAreaS.Enable();
		SoundS.PlaySingleShot("/UI/Purchase", Vector2.zero, 1f);
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x00107938 File Offset: 0x00105D38
	private void FAILED(HttpC _c)
	{
		PsUICenterShopBoosters.HideLoadingPopup();
		Debug.Log("COIN PURCHASE FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		Hashtable setData = _c.objectData as Hashtable;
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, () => PsServerRequest.ServerPlayerSetData(setData, new Action<HttpC>(this.SUCCEED), new Action<HttpC>(this.FAILED), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x001079A6 File Offset: 0x00105DA6
	private static void ShowLoadingPopup()
	{
		Debug.Log("Show loading popup", null);
		if (PsUICenterShopBoosters.m_waitPopup != null)
		{
			PsUICenterShopBoosters.m_waitPopup.Destroy();
			PsUICenterShopBoosters.m_waitPopup = null;
		}
		PsUICenterShopBoosters.m_waitPopup = new PsUIPurchaseWaitPopup(null);
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x001079D8 File Offset: 0x00105DD8
	private static void HideLoadingPopup()
	{
		Debug.Log("Hide loading popup", null);
		if (PsUICenterShopBoosters.m_waitPopup != null)
		{
			PsUICenterShopBoosters.m_waitPopup.Destroy();
			PsUICenterShopBoosters.m_waitPopup = null;
		}
	}

	// Token: 0x04001B07 RID: 6919
	private List<PsUIGenericButton> m_buttons;

	// Token: 0x04001B08 RID: 6920
	private PsUIGenericButton m_adButton;

	// Token: 0x04001B09 RID: 6921
	private bool m_purchasing;

	// Token: 0x04001B0A RID: 6922
	private PsUIBasePopup m_popup;

	// Token: 0x04001B0B RID: 6923
	private bool m_freeBooster;

	// Token: 0x04001B0C RID: 6924
	public bool m_createTutorialArrow;

	// Token: 0x04001B0D RID: 6925
	private PsUITutorialArrowUI m_tutorialArrow;

	// Token: 0x04001B0E RID: 6926
	private int[,] m_boostPrices = new int[,]
	{
		{ 3, 780 },
		{ 10, 2500 },
		{ 50, 12000 }
	};

	// Token: 0x04001B0F RID: 6927
	private static PsUIPurchaseWaitPopup m_waitPopup;
}
