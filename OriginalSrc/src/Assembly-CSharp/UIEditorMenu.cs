using System;
using UnityEngine;

// Token: 0x02000390 RID: 912
public class UIEditorMenu : UIHorizontalList
{
	// Token: 0x06001A40 RID: 6720 RVA: 0x00124FC4 File Offset: 0x001233C4
	public UIEditorMenu(UIComponent _parent, string _tag)
		: base(_parent, _tag)
	{
		this.SetAlign(1f, 0.9f);
		this.SetMargins(0.02f, RelativeTo.ScreenShortest);
		this.SetSpacing(0.03f, RelativeTo.ScreenShortest);
		this.RemoveDrawHandler();
		this.m_playButton = new UIRectSpriteButton(this, "Play", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_button_play", null), false, false);
		this.m_playButton.SetHeight(0.11f, RelativeTo.ScreenShortest);
		this.m_playButton.SetVerticalAlign(1f);
		this.m_fileMenuButton = new UIRectSpriteButton(this, "Menu", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_button_menu", null), false, false);
		this.m_fileMenuButton.SetHeight(0.11f, RelativeTo.ScreenShortest);
		this.m_fileMenuButton.SetVerticalAlign(1f);
		this.Update();
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x001250AC File Offset: 0x001234AC
	public void OpenFileMenu()
	{
		if (this.m_fileMenuButton == null)
		{
			return;
		}
		SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
		this.m_fileMenuButton.Destroy();
		this.m_fileMenuButton = null;
		this.m_fileMenu = new UIEditorFileMenu(this, "Menu");
		this.m_fileMenu.SetVerticalAlign(1f);
		this.Update();
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x00125114 File Offset: 0x00123514
	public void CloseFileMenu()
	{
		if (this.m_fileMenu == null)
		{
			return;
		}
		this.m_fileMenu.Destroy();
		this.m_fileMenu = null;
		this.m_fileMenuButton = new UIRectSpriteButton(this, "Menu", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_button_menu", null), false, false);
		this.m_fileMenuButton.SetHeight(0.11f, RelativeTo.ScreenShortest);
		this.m_fileMenuButton.SetVerticalAlign(1f);
		this.Update();
	}

	// Token: 0x04001CCB RID: 7371
	public UIRectSpriteButton m_playButton;

	// Token: 0x04001CCC RID: 7372
	public UIRectSpriteButton m_fileMenuButton;

	// Token: 0x04001CCD RID: 7373
	public UIEditorFileMenu m_fileMenu;
}
