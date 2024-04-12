using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027C RID: 636
public class PsUICenterLanguageSelect : PsUIHeaderedCanvas
{
	// Token: 0x0600134E RID: 4942 RVA: 0x000BF840 File Offset: 0x000BDC40
	public PsUICenterLanguageSelect(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.75f, RelativeTo.ScreenWidth);
		this.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.m_selectedLanguage = PsStrings.GetLanguage();
		this.m_previousLanguage = this.m_selectedLanguage;
		this.CreateHeaderContent(this.m_header);
		this.CreateContent(this);
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x000BF978 File Offset: 0x000BDD78
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIText uitext = new UIText(_parent, false, string.Empty, PsStrings.Get(StringID.HEADER_LANGUAGE_SELECT).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.065f, RelativeTo.ScreenHeight, "#7AF2FF", null);
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x000BF9B4 File Offset: 0x000BDDB4
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0f, 0f, 0.035f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		int num = Mathf.CeilToInt(3.3333333f);
		for (int i = 0; i < num; i++)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			int num2 = Mathf.Min(10 - i * 3, 3);
			for (int j = 0; j < num2; j++)
			{
				PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
				psUIGenericButton.SetSpacing(0.015f, RelativeTo.ScreenHeight);
				Language language = (Language)(i * 3 + j);
				psUIGenericButton.SetFittedText(PsStrings.Get("LANGUAGE_" + language.ToString().ToUpper()), 0.03f, 0.2f, RelativeTo.ScreenHeight, false);
				psUIGenericButton.SetHeight(0.08f, RelativeTo.ScreenHeight);
				if (language == this.m_selectedLanguage)
				{
					psUIGenericButton.SetBlueColors(false);
					psUIGenericButton.DisableTouchAreas(true);
				}
				else if (!PsState.m_languageSelectEnabled)
				{
					psUIGenericButton.SetGrayColors();
					psUIGenericButton.DisableTouchAreas(true);
				}
				this.m_buttons.Add(psUIGenericButton);
			}
		}
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uicanvas.SetVerticalAlign(0f);
		uicanvas.SetWidth(0.5f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0f, 0f, 0.06f, -0.06f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		this.m_ok = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetGreenColors(true);
		this.m_ok.SetText("OK", 0.04f, 0.175f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x000BFBC8 File Offset: 0x000BDFC8
	public override void Step()
	{
		bool flag = false;
		for (int i = 0; i < this.m_buttons.Count; i++)
		{
			if (this.m_buttons[i].m_hit)
			{
				flag = true;
				this.m_selectedLanguage = (Language)i;
				break;
			}
		}
		if (flag)
		{
			for (int j = 0; j < this.m_buttons.Count; j++)
			{
				Language language = (Language)j;
				if (language == this.m_selectedLanguage)
				{
					this.m_buttons[j].SetBlueColors(false);
					this.m_buttons[j].DisableTouchAreas(true);
				}
				else
				{
					this.m_buttons[j].SetBlueColors(true);
					this.m_buttons[j].EnableTouchAreas(true);
				}
				this.m_buttons[j].Update();
			}
		}
		if (this.m_ok.m_hit)
		{
			PsState.SetLanguage(this.m_selectedLanguage);
			PlayerPrefsX.SetLanguage(this.m_selectedLanguage);
			if (this.m_previousLanguage == this.m_selectedLanguage)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
			else
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Changed");
			}
		}
		base.Step();
	}

	// Token: 0x0400162F RID: 5679
	private List<PsUIGenericButton> m_buttons = new List<PsUIGenericButton>();

	// Token: 0x04001630 RID: 5680
	private PsUIGenericButton m_ok;

	// Token: 0x04001631 RID: 5681
	private Language m_selectedLanguage;

	// Token: 0x04001632 RID: 5682
	private Language m_previousLanguage;
}
