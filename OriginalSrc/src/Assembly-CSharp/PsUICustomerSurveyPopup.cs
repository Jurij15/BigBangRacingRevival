using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020002FC RID: 764
public class PsUICustomerSurveyPopup : PsUIEventMessagePopup
{
	// Token: 0x06001673 RID: 5747 RVA: 0x000EC408 File Offset: 0x000EA808
	public PsUICustomerSurveyPopup(UIComponent _parent)
		: base(_parent, "CustomerSurvey", false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(base.BGDrawhandler));
		this.SetWidth(0.85f, RelativeTo.ScreenHeight);
		this.SetHeight(0.875f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.04f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.07f, 0.07f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		if (PsMetagameManager.m_playerStats.completedSurvey)
		{
			this.m_popupIndex = 7;
		}
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x000EC51A File Offset: 0x000EA91A
	public override void SetEventMessage(EventMessage _msg, bool _newsPage = false)
	{
		this.m_eventMessage = _msg;
		(this.GetRoot() as PsUIBasePopup).SetAction("Exit", delegate
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		});
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x000EC558 File Offset: 0x000EA958
	public override void CreateHeaderContent(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, "Customer survey", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "95e5ff", null);
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x000EC5B8 File Offset: 0x000EA9B8
	public override void CreateContent(UIComponent _parent)
	{
		_parent.DestroyChildren();
		switch (this.m_popupIndex)
		{
		case 0:
			this.SurveyStart(_parent);
			break;
		case 1:
			this.SurveyGender(_parent);
			break;
		case 2:
			this.SurveyAge(_parent);
			break;
		case 3:
			this.SurveyCreation(_parent, 1);
			break;
		case 4:
			this.SurveyCreation(_parent, 2);
			break;
		case 5:
			this.SurveyNotCreation(_parent, 1);
			break;
		case 6:
			this.SurveyNotCreation(_parent, 2);
			break;
		case 7:
			this.SurveyEnd(_parent);
			break;
		default:
			this.SurveyEnd(_parent);
			break;
		}
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x000EC670 File Offset: 0x000EAA70
	private void SurveyStart(UIComponent _parent)
	{
		this.SetWidth(0.65f, RelativeTo.ScreenHeight);
		this.SetHeight(0.55f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, "Would you like to answer a few questions?\n\nYou get 20 GEMS for answering!", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, "000000");
		uitextbox.SetMargins(0f, 0f, 0f, 0.015f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		this.m_no = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_no.SetAlign(1f, 1f);
		this.m_no.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_no.SetRedColors();
		this.m_no.SetText("No", 0.029f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetAlign(1f, 1f);
		this.m_ok.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_ok.SetGreenColors(true);
		this.m_ok.SetText("Yes", 0.029f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		this.GetRoot().Update();
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x000EC86C File Offset: 0x000EAC6C
	private void SurveyGender(UIComponent _parent)
	{
		this.m_no = null;
		this.m_ok = null;
		this.CreateCloseButton();
		UIText uitext = new UIText(_parent, false, string.Empty, this.m_popupIndex + "/6", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, null, null);
		uitext.SetAlign(0.985f, 0.985f);
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, "What is your gender?", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, "000000");
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uitextbox.SetMargins(0f, 0f, 0f, 0.015f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList2.SetHorizontalAlign(0f);
		uiverticalList2.SetMargins(0.15f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		PsUISurveyQuestion psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "Female");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "Male");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "Other");
		this.m_questions.Add(psUISurveyQuestion);
		this.GetRoot().Update();
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000EC9F4 File Offset: 0x000EADF4
	private void SurveyAge(UIComponent _parent)
	{
		this.m_no = null;
		this.m_ok = null;
		this.SetHeight(0.85f, RelativeTo.ScreenHeight);
		UIText uitext = new UIText(_parent, false, string.Empty, this.m_popupIndex + "/6", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, null, null);
		uitext.SetAlign(0.985f, 0.985f);
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, "How old are you?", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, "000000");
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uitextbox.SetMargins(0f, 0f, 0f, 0.015f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList2.SetHorizontalAlign(0f);
		uiverticalList2.SetMargins(0.15f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		PsUISurveyQuestion psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "Under 10");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "11-15");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "16-20");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "21-30");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "31-40");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList2, "Over 41");
		this.m_questions.Add(psUISurveyQuestion);
		this.GetRoot().Update();
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x000ECBD0 File Offset: 0x000EAFD0
	private void SurveyCreation(UIComponent _parent, int _order)
	{
		this.m_no = null;
		this.m_ok = null;
		this.SetWidth(0.875f, RelativeTo.ScreenHeight);
		this.SetHeight(0.875f, RelativeTo.ScreenHeight);
		UIText uitext = new UIText(_parent, false, string.Empty, this.m_popupIndex + "/6", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, null, null);
		uitext.SetAlign(0.985f, 0.985f);
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		string text = "If you create levels, what is your " + ((_order != 2) ? string.Empty : "2. ") + "biggest reason for doing so?";
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, "000000");
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uitextbox.SetMargins(0f, 0f, 0f, 0.015f, RelativeTo.ScreenHeight);
		PsUISurveyQuestion psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "It’s just fun!");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I want to get likes on my levels.");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I want to get coins.");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I want to create as good levels as possible for others to enjoy.");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I want to connect with other players.");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I don’t create levels.");
		this.m_questions.Add(psUISurveyQuestion);
		if (_order == 2 && this.m_answers[this.m_popupIndex - 2] != 5)
		{
			this.m_questions[this.m_answers[this.m_popupIndex - 2]].DisableButton();
		}
		this.GetRoot().Update();
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x000ECDD8 File Offset: 0x000EB1D8
	private void SurveyNotCreation(UIComponent _parent, int _order)
	{
		this.m_no = null;
		this.m_ok = null;
		UIText uitext = new UIText(_parent, false, string.Empty, this.m_popupIndex + "/6", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, null, null);
		uitext.SetAlign(0.985f, 0.985f);
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		string text = "If you don’t create levels, what is your biggest " + ((_order != 2) ? string.Empty : "2. ") + "reason for that? (You can also choose \"I create levels.\")";
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, "000000");
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uitextbox.SetMargins(0f, 0f, 0f, 0.015f, RelativeTo.ScreenHeight);
		PsUISurveyQuestion psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "It’s not as fun as just playing the levels");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I tried, but I didn’t get enough likes, so I quit.");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I don’t think I can create an interesting or fun level.");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I don’t know where to begin, or the Editor is too hard to use.");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I don’t gain anything from it.");
		this.m_questions.Add(psUISurveyQuestion);
		psUISurveyQuestion = new PsUISurveyQuestion(uiverticalList, "I create levels.");
		this.m_questions.Add(psUISurveyQuestion);
		if (_order == 2 && this.m_answers[this.m_popupIndex - 2] != 5)
		{
			this.m_questions[this.m_answers[this.m_popupIndex - 2]].DisableButton();
		}
		this.GetRoot().Update();
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x000ECFC8 File Offset: 0x000EB3C8
	private void SurveyEnd(UIComponent _parent)
	{
		if (this.m_exitButton != null)
		{
			this.m_exitButton.Destroy();
		}
		this.m_exitButton = null;
		this.SetWidth(0.65f, RelativeTo.ScreenHeight);
		this.SetHeight(0.55f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, "Thank you for your answers! Have an alien-tastic day!", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, "000000");
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uitextbox.SetMargins(0f, 0f, 0f, 0.015f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		if (!PsMetagameManager.m_playerStats.completedSurvey)
		{
			this.m_claim = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_claim.SetAlign(1f, 1f);
			this.m_claim.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
			this.m_claim.SetPurpleColors();
			this.m_claim.SetText("Claim", 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
			this.m_claim.SetDiamondPrice(20, 0.03f);
		}
		else
		{
			this.m_no = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_no.SetAlign(1f, 1f);
			this.m_no.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
			this.m_no.SetGreenColors(true);
			this.m_no.SetText("Ok!", 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		}
		this.GetRoot().Update();
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x000ED1FC File Offset: 0x000EB5FC
	public override void Step()
	{
		bool flag = false;
		if (this.m_no != null && this.m_no.m_hit)
		{
			this.m_questions.Clear();
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		else if (this.m_claim != null && this.m_claim.m_hit)
		{
			this.m_questions.Clear();
			PsMetagameManager.m_playerStats.ageGroup = this.m_age;
			PsMetagameManager.m_playerStats.gender = this.m_gender;
			PsMetagameManager.m_playerStats.completedSurvey = true;
			PsMetagameManager.m_playerStats.CumulateDiamonds(20);
			PsMetagameManager.SetPlayerData(new Hashtable(), true, new Action<HttpC>(PsMetagameManager.PlayerDataSetSUCCEED), new Action<HttpC>(PsMetagameManager.PlayerDataSetFAILED), null);
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		else if (this.m_ok != null && this.m_ok.m_hit)
		{
			this.m_popupIndex++;
			flag = true;
		}
		for (int i = 0; i < this.m_questions.Count; i++)
		{
			if (this.m_questions[i].m_button.m_hit)
			{
				this.m_answers.Add(i);
				this.SetAge();
				this.SetGender();
				this.m_popupIndex++;
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.m_questions.Clear();
			this.CreateContent(this);
		}
		base.Step();
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x000ED3B8 File Offset: 0x000EB7B8
	public void SetAge()
	{
		if (this.m_popupIndex == 2)
		{
			switch (this.m_answers[this.m_answers.Count - 1])
			{
			case 0:
				this.m_age = "<10";
				break;
			case 1:
				this.m_age = "11-15";
				break;
			case 2:
				this.m_age = "16-20";
				break;
			case 3:
				this.m_age = "21-30";
				break;
			case 4:
				this.m_age = "30-40";
				break;
			case 5:
				this.m_age = ">40";
				break;
			}
		}
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x000ED470 File Offset: 0x000EB870
	public void SetGender()
	{
		if (this.m_popupIndex == 1)
		{
			int num = this.m_answers[this.m_answers.Count - 1];
			if (num != 0)
			{
				if (num != 1)
				{
					if (num == 2)
					{
						this.m_gender = "Other";
					}
				}
				else
				{
					this.m_gender = "Male";
				}
			}
			else
			{
				this.m_gender = "Female";
			}
		}
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x000ED4EB File Offset: 0x000EB8EB
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x04001933 RID: 6451
	private int m_popupIndex;

	// Token: 0x04001934 RID: 6452
	private PsUIGenericButton m_ok;

	// Token: 0x04001935 RID: 6453
	private PsUIGenericButton m_no;

	// Token: 0x04001936 RID: 6454
	private PsUIGenericButton m_claim;

	// Token: 0x04001937 RID: 6455
	private List<PsUISurveyQuestion> m_questions = new List<PsUISurveyQuestion>();

	// Token: 0x04001938 RID: 6456
	private List<int> m_answers = new List<int>();

	// Token: 0x04001939 RID: 6457
	private string m_age;

	// Token: 0x0400193A RID: 6458
	private string m_gender;
}
