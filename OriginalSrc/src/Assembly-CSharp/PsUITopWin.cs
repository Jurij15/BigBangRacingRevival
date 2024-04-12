using System;

// Token: 0x020002F4 RID: 756
public class PsUITopWin : UICanvas
{
	// Token: 0x06001646 RID: 5702 RVA: 0x000E8F50 File Offset: 0x000E7350
	public PsUITopWin(UIComponent _parent)
		: base(_parent, false, "TopWin", null, string.Empty)
	{
		this.RemoveDrawHandler();
		PsUILevelHeader psUILevelHeader = new PsUILevelHeader(this);
		psUILevelHeader.SetVerticalAlign(0.98f);
	}

	// Token: 0x04001908 RID: 6408
	private PsUIGenericButton m_everyplayButton;

	// Token: 0x04001909 RID: 6409
	private PsUIGenericButton m_restartButton;
}
