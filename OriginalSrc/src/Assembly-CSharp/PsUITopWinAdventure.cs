using System;
using System.Collections;
using AdMediation;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class PsUITopWinAdventure : UICanvas
{
	// Token: 0x06001647 RID: 5703 RVA: 0x000E8F88 File Offset: 0x000E7388
	public PsUITopWinAdventure(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.SetHeight(0.1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetAlign(0.5f, 1f);
		this.SetMargins(0f, 0f, 0.03f, 0f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(this.TopDrawhandler));
		if (PsState.m_activeGameLoop.m_gameMode.m_coinStreakStyle != CoinStreakStyle.BASIC && PsAdMediation.adsAvailable() && PsState.m_activeMinigame.m_collectedCoinsForDoubleUp > 0)
		{
			PsMetrics.AdOffered("coin_doubling_ad");
			this.m_doubleUpGoldButton = new PsUIAttentionButton(this, default(Vector3), 0.25f, 0.25f, 0.005f);
			this.m_doubleUpGoldButton.SetVerticalAlign(1f);
			this.m_doubleUpGoldButton.SetGreenColors(true);
			UIVerticalList uiverticalList = new UIVerticalList(this.m_doubleUpGoldButton, string.Empty);
			uiverticalList.RemoveDrawHandler();
			UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
			uihorizontalList.SetSpacing(0.008f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			new UISprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null), true).SetSize(0.032f, 0.032f, RelativeTo.ScreenHeight);
			new UIText(uihorizontalList, false, string.Empty, PsState.m_activeMinigame.m_collectedCoinsForDoubleUp + " " + PsStrings.Get(StringID.BUTTON_DOUBLE_COINS_COLLECTED), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.028f, RelativeTo.ScreenHeight, null, null);
			new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.BUTTON_DOUBLE_COINS).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.038f, RelativeTo.ScreenHeight, null, null);
			this.m_doubleUpGoldButton.SetIcon("menu_watch_ad_badge", 0.08f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		}
		else
		{
			PsMetrics.AdNotAvailable("coin_doubling_ad");
		}
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x000E9184 File Offset: 0x000E7584
	public void TopDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, 0.06f * (float)Screen.height, new Vector2(0f, 0.02f * (float)Screen.height), true);
		Color black = Color.black;
		Color black2 = Color.black;
		Color black3 = Color.black;
		black3.a = 0.5f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, rect, (float)Screen.height * 0.0075f, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, rect, (float)Screen.height * 0.015f, black3, black3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x000E9294 File Offset: 0x000E7694
	public override void Step()
	{
		if (this.m_doubleUpGoldButton != null && this.m_doubleUpGoldButton.m_hit)
		{
			this.m_doubleUpGoldButton.Destroy();
			this.m_doubleUpGoldButton = null;
			this.WatchAd();
		}
		base.Step();
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x000E92CF File Offset: 0x000E76CF
	private void WatchAd()
	{
		TouchAreaS.Disable();
		PsAdMediation.ShowAd(new Action<AdResult>(this.AdCallBack), null);
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x000E92E8 File Offset: 0x000E76E8
	private void AdCallBack(AdResult _result)
	{
		TouchAreaS.Enable();
		SoundS.PauseMixer(false);
		Debug.Log("Ad display result: " + _result.ToString(), null);
		PsMetrics.AdWatched("coin_doubling_ad", _result.ToString());
		if (_result == AdResult.Finished)
		{
			PsMetagameManager.m_playerStats.CumulateCoins(PsState.m_activeMinigame.m_collectedCoinsForDoubleUp);
			PsState.m_activeMinigame.m_collectedCoinsForDoubleUp = 0;
			PsMetagameManager.SetPlayerData(new Hashtable(), false, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
		}
		else if (_result == AdResult.Failed || _result == AdResult.Skipped)
		{
			Debug.LogError("Ad skpped or failed");
		}
	}

	// Token: 0x0400190A RID: 6410
	private PsUIGenericButton m_doubleUpGoldButton;
}
