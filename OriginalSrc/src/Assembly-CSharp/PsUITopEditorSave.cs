using System;

// Token: 0x020002A7 RID: 679
public class PsUITopEditorSave : UICanvas
{
	// Token: 0x06001465 RID: 5221 RVA: 0x000D046C File Offset: 0x000CE86C
	public PsUITopEditorSave(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetOrangeColors(true);
		PsUIInfoBar psUIInfoBar = new PsUIInfoBar(this, string.Empty, false);
		psUIInfoBar.SetVerticalAlign(0f);
		psUIInfoBar.SetText("Finish your level with three stars to GO LIVE.");
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x000D0550 File Offset: 0x000CE950
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Back");
		}
		base.Step();
	}

	// Token: 0x0400172C RID: 5932
	private PsUIGenericButton m_exitButton;
}
