using System;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class PsUITimedSpecialOfferPopup : PsUIHeaderedCanvas
{
	// Token: 0x06001886 RID: 6278 RVA: 0x0010ABBC File Offset: 0x00108FBC
	public PsUITimedSpecialOfferPopup(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.1f, RelativeTo.ScreenHeight, 0.1f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(1f, RelativeTo.ScreenHeight);
		this.SetHeight(0.8f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.06f, 0.06f, 0f, 0.09f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_exitArea = new UICanvas(_parent, true, string.Empty, null, string.Empty);
		this.m_exitArea.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_exitArea.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_exitArea.RemoveDrawHandler();
		this.m_exitArea.SetRogue();
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x0010ACAC File Offset: 0x001090AC
	public void CreateContent(PsTimedSpecialOffer _specialOffer)
	{
		this.m_specialOffer = _specialOffer;
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.05f, 0.05f, 0.02f, 0.01f, RelativeTo.ScreenHeight);
		UISprite uisprite = new UISprite(this.m_header, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_special_offer_icon", null), true);
		uisprite.SetHorizontalAlign(0f);
		uisprite.SetDepthOffset(-5f);
		uisprite.SetSize(uisprite.m_frame.width / uisprite.m_frame.height * 2f, 2f, RelativeTo.ParentHeight);
		new UIFittedText(this.m_header, false, string.Empty, PsStrings.Get(StringID.SPECIAL_OFFER_SINGULAR), PsFontManager.GetFont(PsFonts.KGSecondChances), false, "#BDFD43", null);
		new PsUITimedSpecialOfferBanner(this, this.m_specialOffer, new Action(this.ClosePopup), false);
		this.m_footer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIFooter));
		this.m_footer.SetMargins(0.12f, 0.12f, 0f, 0f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_footer, string.Empty);
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.SPECIAL_OFFER_ENDS);
		text = text.Replace("%1", string.Empty);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, text + ":", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, null, null);
		this.m_timerSeconds = Mathf.CeilToInt((float)this.m_specialOffer.m_timeLeft);
		string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timerSeconds, true, true);
		this.m_timer = new UIText(uihorizontalList, false, string.Empty, timeStringFromSeconds, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.05f, RelativeTo.ScreenHeight, null, null);
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x0010AEAC File Offset: 0x001092AC
	public override void Step()
	{
		int num = Mathf.CeilToInt((float)this.m_specialOffer.m_timeLeft);
		if (this.m_timer != null && num != this.m_timerSeconds)
		{
			this.m_timerSeconds = num;
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timerSeconds, true, true);
			this.m_timer.SetText(timeStringFromSeconds);
			this.m_timer.m_parent.Update();
		}
		if (this.m_exitArea != null && this.m_exitArea.m_hit)
		{
			this.ClosePopup();
		}
		base.Step();
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x0010AF3A File Offset: 0x0010933A
	private void ClosePopup()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		TouchAreaS.CancelAllTouches(null);
	}

	// Token: 0x0600188A RID: 6282 RVA: 0x0010AF58 File Offset: 0x00109358
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x04001B32 RID: 6962
	private UIText m_timer;

	// Token: 0x04001B33 RID: 6963
	private int m_timerSeconds;

	// Token: 0x04001B34 RID: 6964
	private PsTimedSpecialOffer m_specialOffer;

	// Token: 0x04001B35 RID: 6965
	private UICanvas m_exitArea;
}
