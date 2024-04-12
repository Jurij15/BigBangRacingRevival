using System;

// Token: 0x02000396 RID: 918
public class UIEditorTestMenu : UIVerticalList
{
	// Token: 0x06001A5A RID: 6746 RVA: 0x00126518 File Offset: 0x00124918
	public UIEditorTestMenu(UIComponent _parent, string _tag)
		: base(_parent, _tag)
	{
		this.SetAlign(1f, 1f);
		this.SetMargins(0.02f, RelativeTo.ScreenShortest);
		this.SetSpacing(0.03f, RelativeTo.ScreenShortest);
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_restartButton = new UIRectSpriteButton(uihorizontalList, "Restart", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_button_retry", null), false, false);
		this.m_restartButton.SetHeight(0.12f, RelativeTo.ScreenShortest);
		this.m_restartButton.SetVerticalAlign(1f);
		this.m_restartButton.SetTouchAreaSizeMultipler(1.5f);
		this.m_editButton = new UIRectSpriteButton(uihorizontalList, "Edit", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_button_edit", null), false, false);
		this.m_editButton.SetHeight(0.12f, RelativeTo.ScreenShortest);
		this.m_editButton.SetVerticalAlign(1f);
		this.m_editButton.SetTouchAreaSizeMultipler(1.2f);
		this.Update();
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x0012663C File Offset: 0x00124A3C
	public override void Step()
	{
		if (this.m_editButton.m_hit)
		{
			PsState.m_activeGameLoop.ExitMinigame();
		}
		else if (this.m_restartButton.m_hit)
		{
			PsState.m_activeGameLoop.RestartMinigame();
		}
		base.Step();
	}

	// Token: 0x04001CE4 RID: 7396
	public UIRectSpriteButton m_restartButton;

	// Token: 0x04001CE5 RID: 7397
	public UIRectSpriteButton m_editButton;
}
