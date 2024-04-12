using System;

// Token: 0x0200034D RID: 845
public class PsUIEditorRacePlay : PsUITopPlayRacing
{
	// Token: 0x060018C3 RID: 6339 RVA: 0x0010E03F File Offset: 0x0010C43F
	public PsUIEditorRacePlay(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
		this.CreateRestartArea(this.m_rightArea);
		this.Update();
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x0010E05B File Offset: 0x0010C45B
	public override void CreateCoinArea()
	{
	}

	// Token: 0x060018C5 RID: 6341 RVA: 0x0010E060 File Offset: 0x0010C460
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
