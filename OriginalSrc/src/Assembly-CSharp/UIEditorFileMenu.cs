using System;

// Token: 0x0200038F RID: 911
public class UIEditorFileMenu : UIVerticalList
{
	// Token: 0x06001A3F RID: 6719 RVA: 0x00124C9C File Offset: 0x0012309C
	public UIEditorFileMenu(UIComponent _parent, string _tag)
		: base(_parent, _tag)
	{
		this.SetSpacing(-0.02f, RelativeTo.ScreenShortest);
		this.RemoveDrawHandler();
		this.m_menuButton = new UIRectSpriteButton(this, "MenuButton", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_button_menu", null), false, false);
		this.m_menuButton.SetHeight(0.11f, RelativeTo.ScreenShortest);
		this.m_menuButton.RemoveTouchAreas();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0f, 0f, 0.04f, 0.04f, RelativeTo.ScreenShortest);
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenShortest);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorFileMenuContentArea));
		this.m_newButton = new UICanvas(uiverticalList, true, string.Empty, null, string.Empty);
		this.m_newButton.RemoveDrawHandler();
		this.m_newButton.SetSize(0.11f, 0.11f, RelativeTo.ScreenShortest);
		this.m_newButton.SetMargins(0.01f, RelativeTo.ScreenShortest);
		UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_newButton, false, "NewButton", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_new", null), false, true);
		uifittedSprite.SetSize(0.06f, 0.06f, RelativeTo.ScreenShortest);
		uifittedSprite.SetVerticalAlign(1f);
		UITextbox uitextbox = new UITextbox(this.m_newButton, false, string.Empty, "NEW", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.02f, RelativeTo.ScreenShortest, true, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetVerticalAlign(0f);
		this.m_saveButton = new UICanvas(uiverticalList, true, string.Empty, null, string.Empty);
		this.m_saveButton.RemoveDrawHandler();
		this.m_saveButton.SetSize(0.11f, 0.11f, RelativeTo.ScreenShortest);
		this.m_saveButton.SetMargins(0.01f, RelativeTo.ScreenShortest);
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_saveButton, false, "SaveButton", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_save", null), false, true);
		uifittedSprite2.SetSize(0.06f, 0.06f, RelativeTo.ScreenShortest);
		uifittedSprite2.SetVerticalAlign(1f);
		UITextbox uitextbox2 = new UITextbox(this.m_saveButton, false, string.Empty, "SAVE", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.02f, RelativeTo.ScreenShortest, true, Align.Center, Align.Middle, null, true, null);
		uitextbox2.SetVerticalAlign(0f);
		this.m_exitButton = new UICanvas(uiverticalList, true, string.Empty, null, string.Empty);
		this.m_exitButton.RemoveDrawHandler();
		this.m_exitButton.SetSize(0.11f, 0.11f, RelativeTo.ScreenShortest);
		this.m_exitButton.SetMargins(0.01f, RelativeTo.ScreenShortest);
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(this.m_exitButton, false, "ExitButton", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_exit", null), false, true);
		uifittedSprite3.SetSize(0.06f, 0.06f, RelativeTo.ScreenShortest);
		uifittedSprite3.SetVerticalAlign(1f);
		UITextbox uitextbox3 = new UITextbox(this.m_exitButton, false, string.Empty, "EXIT", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.02f, RelativeTo.ScreenShortest, true, Align.Center, Align.Middle, null, true, null);
		uitextbox3.SetVerticalAlign(0f);
	}

	// Token: 0x04001CC3 RID: 7363
	public UIRectSpriteButton m_menuButton;

	// Token: 0x04001CC4 RID: 7364
	public UICanvas m_newButton;

	// Token: 0x04001CC5 RID: 7365
	public UICanvas m_saveButton;

	// Token: 0x04001CC6 RID: 7366
	public UICanvas m_publishButton;

	// Token: 0x04001CC7 RID: 7367
	public UICanvas m_exitButton;

	// Token: 0x04001CC8 RID: 7368
	public UICanvas m_openTutorialButton;

	// Token: 0x04001CC9 RID: 7369
	public UICanvas m_saveTutorialButton;
}
