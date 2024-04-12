using System;
using System.Collections.Generic;
using AdMediation;
using UnityEngine;

// Token: 0x020003D6 RID: 982
public class PsUICenterRouletteCoin : PsUICenterRoulette
{
	// Token: 0x06001BBE RID: 7102 RVA: 0x00135246 File Offset: 0x00133646
	public PsUICenterRouletteCoin(UIComponent _parent)
		: base(_parent)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.BgDrawhandler));
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x00135278 File Offset: 0x00133678
	protected override PsUIGenericButton CreateSpinButton(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(0.75f);
		uiverticalList.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.SPIN_SUPERCHARGE);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, "Spinner Description", text, PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "#FFFFFF", true, null);
		uitextbox.SetMargins(0.08f, 0.08f, 0f, 0f, RelativeTo.ParentWidth);
		uitextbox.SetVerticalAlign(0.9f);
		PsUIAttentionButton psUIAttentionButton = new PsUIAttentionButton(uiverticalList, default(Vector3), 0.25f, 0.25f, 0.005f);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_star_unlit", null);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_star_lit", null);
		if (!PsUICenterRouletteCoin.IsTutorialSpin)
		{
			psUIAttentionButton.SetText("<color=#333333>" + PsStrings.Get(StringID.NITRO_FILL_WATCH_AD).ToUpper() + "</color>", 0.022f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			psUIAttentionButton.SetIcon("menu_watch_ad_badge", 0.08f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		}
		UIComponent uicomponent = ((!PsUICenterRouletteCoin.IsTutorialSpin) ? psUIAttentionButton.m_textArea : psUIAttentionButton);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicomponent, "Spin text and stars");
		uihorizontalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, frame2, true, true);
		uifittedSprite.SetHeight(0.08f, RelativeTo.ParentHeight);
		uifittedSprite.SetHorizontalAlign(0.5f);
		new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.BUTTON_SPIN), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.08f, RelativeTo.ScreenHeight, null, null);
		uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, frame2, true, true);
		uifittedSprite.SetHeight(0.08f, RelativeTo.ParentHeight);
		uifittedSprite.SetHorizontalAlign(0.5f);
		return psUIAttentionButton;
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x00135460 File Offset: 0x00133860
	public override PsUI3DCanvasGacha GetSpinner(UIComponent _parent)
	{
		int num = 15;
		List<CoinStreakStyle> visualCoinTypes = PsCoinRoulette.GetVisualCoinTypes(PsUICenterRouletteCoin.m_prize, num);
		PsUI3DCanvasGachaCoin psUI3DCanvasGachaCoin = new PsUI3DCanvasGachaCoin(_parent, "Coin roulette canvas", new Vector3(0f, -0.64f, 0f));
		psUI3DCanvasGachaCoin.AddSpinnerTargetGOs<CoinStreakStyle>(num, PsUICenterRouletteCoin.m_prize, visualCoinTypes, 2.25f, new Vector3(5.5f, 180f, 0f), new string[0]);
		psUI3DCanvasGachaCoin.SetHeight(0.5f, RelativeTo.ParentHeight);
		return psUI3DCanvasGachaCoin;
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x001354D5 File Offset: 0x001338D5
	public override string GetHeaderText()
	{
		return PsStrings.Get(StringID.GACHA_NAME_COINROULETTE).ToUpper();
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x001354E6 File Offset: 0x001338E6
	public override string GetPrizeText()
	{
		return PsCoinRoulette.GetName(PsUICenterRouletteCoin.m_prize).ToUpper();
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x001354F7 File Offset: 0x001338F7
	protected override string GetContinueButtonString()
	{
		return PsStrings.Get(StringID.PLAY);
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x00135500 File Offset: 0x00133900
	protected override void StartRoulette()
	{
		this.WatchAd(delegate
		{
			Debug.Log("Ad watched - ad callback", null);
			if (this.m_exitButton != null)
			{
				this.m_exitButton.Destroy();
				this.m_exitButton = null;
			}
			this.<StartRoulette>__BaseCallProxy0();
			PsCoinRoulette.WasSpinned(PsState.m_activeGameLoop);
			PsUICenterRouletteCoin.IsTutorialSpin = false;
		});
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x00135514 File Offset: 0x00133914
	protected void WatchAd(Action _finishedAction)
	{
		if (!PsUICenterRouletteCoin.IsTutorialSpin)
		{
			TouchAreaS.Disable();
			PsAdMediation.ShowAd(delegate(AdResult c)
			{
				this.AdCallBack(c, _finishedAction);
			}, null);
		}
		else
		{
			_finishedAction.Invoke();
		}
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x00135568 File Offset: 0x00133968
	protected void AdCallBack(AdResult _result, Action _finishedAction)
	{
		TouchAreaS.Enable();
		SoundS.PauseMixer(false);
		Debug.Log("Ad display result: " + _result.ToString(), null);
		PsMetrics.AdWatched("coinRoulette", _result.ToString());
		if (_result == AdResult.Finished)
		{
			_finishedAction.Invoke();
		}
		else if (_result == AdResult.Failed || _result == AdResult.Skipped)
		{
			Debug.LogError("Ad skpped or failed");
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x001355F4 File Offset: 0x001339F4
	public override void CreatePrizeInformation(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.6f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		string description = PsCoinRoulette.GetDescription(PsUICenterRouletteCoin.m_prize);
		UITextbox uitextbox = new UITextbox(uicanvas, false, string.Empty, description, PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "#FFFFFF", true, null);
		uitextbox.SetMargins(0.02f, 0.02f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		uitextbox.SetDrawHandler(new UIDrawDelegate(PsUICenterRouletteCoin.ResourceBackground));
		uitextbox.SetVerticalAlign(0.65f);
		uitextbox.SetWidth(0.65f, RelativeTo.ParentWidth);
		TweenC tweenC = TweenS.AddTransformTween(uitextbox.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, new Vector3(0f, 0f, 1f), Vector3.one, 0.6f, 0f, true);
		TweenS.AddTweenStartEventListener(tweenC, delegate(TweenC _c)
		{
		});
		_parent.Update();
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x00135706 File Offset: 0x00133B06
	protected override void SpinnerAnimationDone()
	{
		base.SpinnerAnimationDone();
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x00135710 File Offset: 0x00133B10
	public static void ResourceBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.015f * (float)Screen.height, 8, Vector2.zero);
		Color color = DebugDraw.HexToColor("#1B4461");
		uint num = DebugDraw.ColorToUInt(color);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, roundedRect, (float)Screen.height * 0.003f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x04001E12 RID: 7698
	public static CoinStreakStyle m_prize;

	// Token: 0x04001E13 RID: 7699
	public static bool IsTutorialSpin;
}
