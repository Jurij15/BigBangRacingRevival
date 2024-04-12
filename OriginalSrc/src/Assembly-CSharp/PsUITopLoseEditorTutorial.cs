using System;

// Token: 0x020002B4 RID: 692
public class PsUITopLoseEditorTutorial : UICanvas
{
	// Token: 0x060014B4 RID: 5300 RVA: 0x000D847C File Offset: 0x000D687C
	public PsUITopLoseEditorTutorial(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		PsMetagameManager.HideResources();
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperRight");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(1f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_pauseButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pauseButton.SetText("EDIT", 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_pauseButton.SetOrangeColors(true);
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x000D852A File Offset: 0x000D692A
	public override void Step()
	{
		if (this.m_pauseButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001783 RID: 6019
	private PsUIGenericButton m_pauseButton;
}
