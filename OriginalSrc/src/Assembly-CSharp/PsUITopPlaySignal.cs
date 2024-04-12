using System;

// Token: 0x02000360 RID: 864
public class PsUITopPlaySignal : UICanvas, IPlayMenu
{
	// Token: 0x06001925 RID: 6437 RVA: 0x001109F4 File Offset: 0x0010EDF4
	public PsUITopPlaySignal(Action _exitAction = null)
		: base(null, false, "TopContent", null, string.Empty)
	{
		this.m_exitAction = _exitAction;
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetText("Skip", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_exitButton.SetOrangeColors(true);
		this.Update();
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x00110AAA File Offset: 0x0010EEAA
	public void ApplyLeftySettings()
	{
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x00110AAC File Offset: 0x0010EEAC
	public override void Step()
	{
		if (this.m_exitButton.m_hit && this.m_exitAction != null)
		{
			this.m_exitAction.Invoke();
		}
		base.Step();
	}

	// Token: 0x04001BB0 RID: 7088
	private PsUIGenericButton m_exitButton;

	// Token: 0x04001BB1 RID: 7089
	private Action m_exitAction;
}
