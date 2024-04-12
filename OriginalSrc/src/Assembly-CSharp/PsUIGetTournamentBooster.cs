using System;
using System.Collections;
using System.Collections.Generic;
using AdMediation;
using Server;
using UnityEngine;

// Token: 0x0200021C RID: 540
public class PsUIGetTournamentBooster : PsUIHeaderedCanvas
{
	// Token: 0x06000FA0 RID: 4000 RVA: 0x00092E4C File Offset: 0x0009124C
	public PsUIGetTournamentBooster(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.m_info = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament;
		this.SetWidth(0.6f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenWidth);
		this.SetVerticalAlign(0.45f);
		this.SetMargins(0.0125f, 0.0125f, 0.0225f, 0.0225f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.8f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.225f, RelativeTo.ParentHeight);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetMargins(0.1f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uicanvas, false, string.Empty, PsStrings.Get(StringID.TOURNAMENT_INFO_NITRO_POT) + PsStrings.Get(StringID.TOURNAMENT_INFO_NITROS_UNUSED), PsFontManager.GetFont(PsFonts.HurmeBold), 0.2f, RelativeTo.ParentHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetVerticalAlign(1f);
		uitextbox.SetMaxRows(4);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHeight(0.55f, RelativeTo.ParentHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0.5f);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		this.m_purchaseButton = new UIRectSpriteButton(uihorizontalList, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), true, false);
		this.m_purchaseButton.m_TAC.m_letTouchesThrough = true;
		this.m_purchaseButton.SetWidth(this.m_purchaseButton.m_frame.width / this.m_purchaseButton.m_frame.height, RelativeTo.ParentHeight);
		this.m_purchaseButton.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_purchaseButton.SetMargins(0.08f, 0.08f, 0.06f, 0.04f, RelativeTo.OwnWidth);
		UICanvas uicanvas2 = new UICanvas(this.m_purchaseButton, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetVerticalAlign(1f);
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, this.m_info.nitroPack1Nitros + PsStrings.Get(StringID.TOUR_X_NITROS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UISprite uisprite = new UISprite(this.m_purchaseButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_ shop_item_fill_boosters", null), true);
		uisprite.SetSize(0.9f, uisprite.m_frame.height / uisprite.m_frame.width * 0.9f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this.m_purchaseButton, string.Empty);
		uihorizontalList2.SetVerticalAlign(0f);
		uihorizontalList2.SetSpacing(0.05f, RelativeTo.ParentWidth);
		uihorizontalList2.RemoveDrawHandler();
		UIText uitext = new UIText(uihorizontalList2, false, string.Empty, this.m_info.nitroPack1Gems.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.15f, RelativeTo.ParentHeight, "#FFFFFF", "#000000");
		uitext.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UISprite uisprite2 = new UISprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null), true);
		uisprite2.SetSize(0.16f, uisprite2.m_frame.height / uisprite2.m_frame.width * 0.16f, RelativeTo.ParentHeight);
		this.m_purchaseButton2 = new UIRectSpriteButton(uihorizontalList, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), true, false);
		this.m_purchaseButton2.m_TAC.m_letTouchesThrough = true;
		this.m_purchaseButton2.SetWidth(this.m_purchaseButton2.m_frame.width / this.m_purchaseButton2.m_frame.height, RelativeTo.ParentHeight);
		this.m_purchaseButton2.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_purchaseButton2.SetMargins(0.08f, 0.08f, 0.06f, 0.04f, RelativeTo.OwnWidth);
		UICanvas uicanvas3 = new UICanvas(this.m_purchaseButton2, false, string.Empty, null, string.Empty);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas3.RemoveDrawHandler();
		uicanvas3.SetVerticalAlign(1f);
		UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, this.m_info.nitroPack2Nitros.ToString() + PsStrings.Get(StringID.TOUR_X_NITROS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText2.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UISprite uisprite3 = new UISprite(this.m_purchaseButton2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_ shop_item_fill_boosters", null), true);
		uisprite3.SetSize(0.9f, uisprite3.m_frame.height / uisprite3.m_frame.width * 0.9f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList3 = new UIHorizontalList(this.m_purchaseButton2, string.Empty);
		uihorizontalList3.SetVerticalAlign(0f);
		uihorizontalList3.SetSpacing(0.05f, RelativeTo.ParentWidth);
		uihorizontalList3.RemoveDrawHandler();
		UIText uitext2 = new UIText(uihorizontalList3, false, string.Empty, this.m_info.nitroPack2Gems.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.15f, RelativeTo.ParentHeight, "#FFFFFF", "#000000");
		uitext2.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UISprite uisprite4 = new UISprite(uihorizontalList3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null), true);
		uisprite4.SetSize(0.16f, uisprite4.m_frame.height / uisprite4.m_frame.width * 0.16f, RelativeTo.ParentHeight);
		if (PsAdMediation.adsAvailable())
		{
			this.m_watchAdButton = new UIRectSpriteButton(uihorizontalList, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), true, false);
			this.m_watchAdButton.m_TAC.m_letTouchesThrough = true;
			this.m_watchAdButton.SetWidth(this.m_watchAdButton.m_frame.width / this.m_watchAdButton.m_frame.height, RelativeTo.ParentHeight);
			this.m_watchAdButton.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_watchAdButton.SetMargins(0.08f, 0.08f, 0.06f, 0.06f, RelativeTo.OwnWidth);
			UICanvas uicanvas4 = new UICanvas(this.m_watchAdButton, false, string.Empty, null, string.Empty);
			uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas4.SetHeight(0.13f, RelativeTo.ParentHeight);
			uicanvas4.RemoveDrawHandler();
			uicanvas4.SetVerticalAlign(1f);
			UIFittedText uifittedText3 = new UIFittedText(uicanvas4, false, string.Empty, this.m_info.nitroAdPackNitros.ToString() + PsStrings.Get(StringID.TOUR_X_NITROS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
			uifittedText3.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
			UISprite uisprite5 = new UISprite(this.m_watchAdButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_ shop_item_fill_boosters", null), true);
			uisprite5.SetSize(0.9f, uisprite5.m_frame.height / uisprite5.m_frame.width * 0.9f, RelativeTo.ParentWidth);
			UISprite uisprite6 = new UISprite(uisprite5, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_watch_ad_badge", null), true);
			uisprite6.SetSize(0.5f, 0.5f, RelativeTo.ParentWidth);
			UICanvas uicanvas5 = new UICanvas(this.m_watchAdButton, false, string.Empty, null, string.Empty);
			uicanvas5.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas5.SetHeight(0.12f, RelativeTo.ParentHeight);
			uicanvas5.RemoveDrawHandler();
			uicanvas5.SetVerticalAlign(0f);
			UIFittedText uifittedText4 = new UIFittedText(uicanvas5, false, string.Empty, PsStrings.Get(StringID.NITRO_FILL_WATCH_AD), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
			uifittedText4.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
			PsMetrics.AdOffered("boosterReload");
		}
		else
		{
			PsMetrics.AdNotAvailable("boosterReload");
		}
		UICanvas uicanvas6 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas6.SetWidth(0.8f, RelativeTo.ParentWidth);
		uicanvas6.SetHeight(0.225f, RelativeTo.ParentHeight);
		uicanvas6.SetVerticalAlign(0f);
		uicanvas6.SetMargins(0.1f, RelativeTo.OwnHeight);
		uicanvas6.RemoveDrawHandler();
		UITextbox uitextbox2 = new UITextbox(uicanvas6, false, string.Empty, PsStrings.Get(StringID.TOURNAMENT_INFO_NITRO_UNLIMITED), PsFontManager.GetFont(PsFonts.HurmeBold), 0.2f, RelativeTo.ParentHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox2.SetMaxRows(4);
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x00093800 File Offset: 0x00091C00
	public void ItemHolderDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0009388C File Offset: 0x00091C8C
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.075f, 0.075f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.TOUR_TOURNAMENT_NITROS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x00093908 File Offset: 0x00091D08
	public void SaveToServer(Tournament.NitroPack _pack)
	{
		Tournament.GetTournamentNitroData data = new Tournament.GetTournamentNitroData();
		data.nitroPack = _pack;
		data.setData = new Hashtable();
		List<string> list = new List<string>();
		data.setData.Add("update", ClientTools.GeneratePlayerSetData(new Hashtable(), ref list));
		new PsServerQueueFlow(null, delegate
		{
			HttpC tournamentNitro = Tournament.GetTournamentNitro(data, new Action<HttpC>(this.SaveToServerSucceed), new Action<HttpC>(this.SaveToServerFailed), null);
			tournamentNitro.objectData = data;
		}, new string[] { "SetData" });
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x00093991 File Offset: 0x00091D91
	private void SaveToServerSucceed(HttpC _c)
	{
		Debug.Log("Tournament Nitro Purchase succeed!", null);
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x000939A0 File Offset: 0x00091DA0
	private void SaveToServerFailed(HttpC _c)
	{
		Debug.LogError("Tournament Nitro Purchase Failed!");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC tournamentNitro = Tournament.GetTournamentNitro(_c.objectData as Tournament.GetTournamentNitroData, new Action<HttpC>(this.SaveToServerSucceed), new Action<HttpC>(this.SaveToServerFailed), null);
			tournamentNitro.objectData = _c.objectData;
			return tournamentNitro;
		}, null);
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x000939F4 File Offset: 0x00091DF4
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit)
		{
			int nitroPack1Nitros = this.m_info.nitroPack1Nitros;
			int nitroPack1Gems = this.m_info.nitroPack1Gems;
			if (PsMetagameManager.m_playerStats.diamonds >= nitroPack1Gems)
			{
				PsMetagameManager.m_playerStats.CumulateTournamentBoosters(nitroPack1Nitros);
				PsMetagameManager.m_playerStats.CumulateDiamonds(-nitroPack1Gems);
				PsMetrics.TournamentNitrosFilled((PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.tournamentId, (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.room, nitroPack1Nitros, nitroPack1Gems, false);
				FrbMetrics.SpendVirtualCurrency("tournament_nitros_" + nitroPack1Nitros, "gems", (double)nitroPack1Gems);
				this.SaveToServer(Tournament.NitroPack.pack1);
				(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
			else
			{
				new PsGetDiamondsFlow(null, null, null);
			}
		}
		else if (this.m_purchaseButton2 != null && this.m_purchaseButton2.m_hit)
		{
			int nitroPack2Nitros = this.m_info.nitroPack2Nitros;
			int nitroPack2Gems = this.m_info.nitroPack2Gems;
			if (PsMetagameManager.m_playerStats.diamonds >= nitroPack2Gems)
			{
				PsMetagameManager.m_playerStats.CumulateTournamentBoosters(nitroPack2Nitros);
				PsMetagameManager.m_playerStats.CumulateDiamonds(-nitroPack2Gems);
				PsMetrics.TournamentNitrosFilled((PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.tournamentId, (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.room, nitroPack2Nitros, nitroPack2Gems, false);
				FrbMetrics.SpendVirtualCurrency("tournament_nitros_" + nitroPack2Nitros, "gems", (double)nitroPack2Gems);
				this.SaveToServer(Tournament.NitroPack.pack2);
				(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
			else
			{
				new PsGetDiamondsFlow(null, null, null);
			}
		}
		else if (this.m_watchAdButton != null && this.m_watchAdButton.m_hit)
		{
			TouchAreaS.Disable();
			PsAdMediation.ShowAd(new Action<AdResult>(this.adCallback), null);
		}
		base.Step();
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x00093C58 File Offset: 0x00092058
	private void adCallback(AdResult _result)
	{
		TouchAreaS.Enable();
		Debug.Log("Ad display result: " + _result.ToString(), null);
		if (_result == AdResult.Finished)
		{
			int nitroAdPackNitros = this.m_info.nitroAdPackNitros;
			PsMetagameManager.m_playerStats.CumulateTournamentBoosters(nitroAdPackNitros);
			PsMetrics.AdWatched("boosterReload", _result.ToString());
			PsMetrics.TournamentNitrosFilled((PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.tournamentId, (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.room, nitroAdPackNitros, 0, true);
			this.SaveToServer(Tournament.NitroPack.adPack);
			(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
		}
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
	}

	// Token: 0x0400124D RID: 4685
	private UIRectSpriteButton m_purchaseButton;

	// Token: 0x0400124E RID: 4686
	private UIRectSpriteButton m_purchaseButton2;

	// Token: 0x0400124F RID: 4687
	private UIRectSpriteButton m_watchAdButton;

	// Token: 0x04001250 RID: 4688
	private TournamentInfo m_info;
}
