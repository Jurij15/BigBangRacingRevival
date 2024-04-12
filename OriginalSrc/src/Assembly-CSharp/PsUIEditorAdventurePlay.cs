using System;

// Token: 0x0200034E RID: 846
public class PsUIEditorAdventurePlay : PsUITopPlayAdventure
{
	// Token: 0x060018C6 RID: 6342 RVA: 0x0010E5E3 File Offset: 0x0010C9E3
	public PsUIEditorAdventurePlay(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
		this.CreateRestartArea(this.m_rightArea);
		this.Update();
	}

	// Token: 0x060018C7 RID: 6343 RVA: 0x0010E5FF File Offset: 0x0010C9FF
	public override void CreateCoinArea()
	{
	}

	// Token: 0x060018C8 RID: 6344 RVA: 0x0010E604 File Offset: 0x0010CA04
	public override void CreateRestartArea(UIComponent _parent)
	{
		this.m_pauseButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pauseButton.SetText(PsStrings.Get(StringID.EDITOR_BUTON_EDIT).ToUpper(), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_pauseButton.SetOrangeColors(true);
		this.m_pauseButton.SetSound("/UI/EditorMode_Edit");
		this.m_restartButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
	}
}
