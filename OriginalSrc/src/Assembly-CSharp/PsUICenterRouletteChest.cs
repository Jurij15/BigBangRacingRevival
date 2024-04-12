using System;
using System.Collections.Generic;
using AdMediation;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class PsUICenterRouletteChest : PsUICenterRoulette
{
	// Token: 0x06001BB0 RID: 7088 RVA: 0x00134DCD File Offset: 0x001331CD
	public PsUICenterRouletteChest(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x00134DD8 File Offset: 0x001331D8
	protected override PsUIGenericButton CreateSpinButton(UIComponent _parent)
	{
		if (PsAdMediation.adsAvailable())
		{
			PsMetrics.AdOffered("chestRoulette_popup");
		}
		PsUIAttentionButton psUIAttentionButton = new PsUIAttentionButton(_parent, default(Vector3), 0.25f, 0.25f, 0.005f);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_star_unlit", null);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_star_lit", null);
		psUIAttentionButton.SetText("<color=#333333>" + PsStrings.Get(StringID.NITRO_FILL_WATCH_AD).ToUpper() + "</color>", 0.022f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		UIHorizontalList uihorizontalList = new UIHorizontalList(psUIAttentionButton.m_textArea, "Spin text and stars");
		uihorizontalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, frame2, true, true);
		uifittedSprite.SetHeight(0.08f, RelativeTo.ParentHeight);
		uifittedSprite.SetHorizontalAlign(0.5f);
		new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.BUTTON_SPIN), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.08f, RelativeTo.ScreenHeight, null, null);
		uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, frame2, true, true);
		uifittedSprite.SetHeight(0.08f, RelativeTo.ParentHeight);
		uifittedSprite.SetHorizontalAlign(0.5f);
		psUIAttentionButton.SetIcon("menu_watch_ad_badge", 0.08f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		return psUIAttentionButton;
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x00134F34 File Offset: 0x00133334
	public override PsUI3DCanvasGacha GetSpinner(UIComponent _parent)
	{
		int num = 15;
		List<GachaType> visualChestTypes = PsSurpriseGacha.GetVisualChestTypes(PsUICenterRouletteChest.m_prize, num);
		PsUI3DCanvasGachaChest psUI3DCanvasGachaChest = new PsUI3DCanvasGachaChest(_parent, "Chest roulette canvas", new Vector3(0f, -0.55f, 0f));
		psUI3DCanvasGachaChest.AddSpinnerTargetGOs<GachaType>(num, PsUICenterRouletteChest.m_prize, visualChestTypes, 2.25f, new Vector3(20f, 190f, 0f), new string[0]);
		psUI3DCanvasGachaChest.SetHeight(0.5f, RelativeTo.ParentHeight);
		return psUI3DCanvasGachaChest;
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x00134FA9 File Offset: 0x001333A9
	protected override void StartRoulette()
	{
		this.WatchAd(delegate
		{
			this.m_exitButton.Destroy();
			this.GachaChestLogic();
			this.<StartRoulette>__BaseCallProxy0();
		});
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x00134FC0 File Offset: 0x001333C0
	private void GachaChestLogic()
	{
		int slotIndex = PsGachaManager.GetSlotIndex(PsGachaManager.SlotType.FREE);
		PsGacha psGacha = PsGachaManager.m_gachas[slotIndex];
		PsGachaManager.m_gachas[slotIndex] = null;
		PsGachaManager.m_lastOpenedGacha = psGacha.m_gachaType;
		PsGacha surpriceChest = PsSurpriseGacha.GetSurpriceChest();
		PsGachaManager.AddGacha(surpriceChest, PsGachaManager.SlotType.FREE, false);
		PsGachaManager.UnlockGacha(surpriceChest, false);
		PsGachaManager.m_lastGachaRewards = PsGachaManager.OpenGacha(psGacha, slotIndex, true);
		PsMetrics.ChestOpened("Surprise");
		FrbMetrics.ChestOpened("surprise");
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x00135028 File Offset: 0x00133428
	protected void WatchAd(Action _finishedAction)
	{
		if (PsAdMediation.adsAvailable())
		{
			TouchAreaS.Disable();
			PsAdMediation.ShowAd(delegate(AdResult c)
			{
				this.AdCallBack(c, _finishedAction);
			}, null);
		}
		else
		{
			PsMetrics.AdNotAvailable("roulette");
			_finishedAction.Invoke();
		}
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x00135084 File Offset: 0x00133484
	protected void AdCallBack(AdResult _result, Action _finishedAction)
	{
		TouchAreaS.Enable();
		SoundS.PauseMixer(false);
		Debug.Log("Ad display result: " + _result.ToString(), null);
		PsMetrics.AdWatched("chestRoulette", _result.ToString());
		if (_result == AdResult.Finished)
		{
			_finishedAction.Invoke();
		}
		else if (_result == AdResult.Failed || _result == AdResult.Skipped)
		{
			Debug.LogError("Ad skipped or failed");
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x0013510F File Offset: 0x0013350F
	public override string GetHeaderText()
	{
		return PsStrings.Get(StringID.GACHA_NAME_SURPRISE).ToUpper();
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x00135120 File Offset: 0x00133520
	public override string GetPrizeText()
	{
		return PsGachaManager.GetGachaNameWithChest(PsUICenterRouletteChest.m_prize).ToUpper();
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x00135131 File Offset: 0x00133531
	protected override string GetContinueButtonString()
	{
		return PsStrings.Get(StringID.OPEN);
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x0013513C File Offset: 0x0013353C
	public override void CreatePrizeInformation(UIComponent _parent)
	{
		this.m_chestContents = new List<UICanvas>();
		UIVerticalList uiverticalList = PsUICenterConfirmPurchase.CreateChestInformation(_parent, PsUICenterRouletteChest.m_prize, this.m_chestContents);
		_parent.Update();
		for (int i = 0; i < this.m_chestContents.Count; i++)
		{
			TweenC tweenC = TweenS.AddTransformTween(this.m_chestContents[i].m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, new Vector3(0f, 0f, 1f), Vector3.one, 0.6f, 0.1f * (float)i, true);
			TweenS.AddTweenStartEventListener(tweenC, delegate(TweenC _c)
			{
			});
			TransformS.SetScale(this.m_chestContents[i].m_TC, 0f);
		}
	}

	// Token: 0x04001E0F RID: 7695
	public static GachaType m_prize;

	// Token: 0x04001E10 RID: 7696
	private List<UICanvas> m_chestContents;
}
