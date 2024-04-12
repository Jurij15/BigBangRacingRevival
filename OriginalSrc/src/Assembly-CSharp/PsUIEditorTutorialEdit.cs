using System;

// Token: 0x0200034F RID: 847
public class PsUIEditorTutorialEdit : PsUIEditorEdit
{
	// Token: 0x060018C9 RID: 6345 RVA: 0x0010E6C4 File Offset: 0x0010CAC4
	public PsUIEditorTutorialEdit(Action _exitAction = null, Action _playAction = null)
		: base(_exitAction, _playAction)
	{
	}

	// Token: 0x060018CA RID: 6346 RVA: 0x0010E6D0 File Offset: 0x0010CAD0
	public override void CreateTopLeftArea(UIComponent _parent)
	{
		this.m_exitButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetIcon("menu_icon_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetOrangeColors(true);
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x0010E72D File Offset: 0x0010CB2D
	public override void CreateAdminInfo()
	{
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x0010E72F File Offset: 0x0010CB2F
	public override void CreateBottomRightArea(UIComponent _parent)
	{
	}
}
