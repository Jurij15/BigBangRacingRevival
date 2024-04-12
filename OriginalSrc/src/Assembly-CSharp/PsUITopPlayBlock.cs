using System;

// Token: 0x0200035A RID: 858
public class PsUITopPlayBlock : UICanvas
{
	// Token: 0x06001900 RID: 6400 RVA: 0x0010F900 File Offset: 0x0010DD00
	public PsUITopPlayBlock(Action _restartAction = null, Action _pauseAction = null)
		: base(null, false, "TopContent", null, string.Empty)
	{
		this.m_restartAction = _restartAction;
		this.m_pauseAction = _pauseAction;
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(1f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_pauseButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pauseButton.SetIcon("hud_icon_pause", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_pauseButton.SetOrangeColors(true);
		this.m_restartButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
		this.Update();
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x0010FA14 File Offset: 0x0010DE14
	public override void Step()
	{
		if (this.m_restartButton.m_hit)
		{
			if (this.m_restartAction != null)
			{
				this.m_restartAction.Invoke();
			}
		}
		else if (this.m_pauseButton.m_hit && this.m_pauseAction != null)
		{
			this.m_pauseAction.Invoke();
		}
		base.Step();
	}

	// Token: 0x04001B86 RID: 7046
	private PsUIGenericButton m_restartButton;

	// Token: 0x04001B87 RID: 7047
	private PsUIGenericButton m_pauseButton;

	// Token: 0x04001B88 RID: 7048
	private Action m_restartAction;

	// Token: 0x04001B89 RID: 7049
	private Action m_pauseAction;
}
