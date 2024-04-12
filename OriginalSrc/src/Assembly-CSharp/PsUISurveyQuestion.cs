using System;

// Token: 0x020002FE RID: 766
public class PsUISurveyQuestion : UIHorizontalList
{
	// Token: 0x0600168E RID: 5774 RVA: 0x000EDFD8 File Offset: 0x000EC3D8
	public PsUISurveyQuestion(UIComponent _parent, string _question)
		: base(_parent, "Question")
	{
		this.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		this.SetHorizontalAlign(0f);
		this.RemoveDrawHandler();
		this.m_button = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_button.SetIcon("menu_scoreboard_prize_check", 0.0325f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		UITextbox uitextbox = new UITextbox(this, false, string.Empty, _question, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0225f, RelativeTo.ScreenHeight, true, Align.Left, Align.Top, null, true, null);
		uitextbox.SetWidth(0.65f, RelativeTo.ScreenHeight);
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x000EE07E File Offset: 0x000EC47E
	public void DisableButton()
	{
		this.m_button.RemoveTouchAreas();
		this.m_button.SetDarkGrayColors();
	}

	// Token: 0x0400194C RID: 6476
	public PsUIGenericButton m_button;
}
