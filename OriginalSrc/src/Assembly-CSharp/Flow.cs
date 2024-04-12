using System;

// Token: 0x02000201 RID: 513
public abstract class Flow
{
	// Token: 0x06000F04 RID: 3844 RVA: 0x0006D9A9 File Offset: 0x0006BDA9
	public Flow(Action _proceed, Action _cancel, Flow _rootFlow = null)
	{
		this.Proceed = _proceed;
		this.Cancel = _cancel;
		this.m_rootFlow = _rootFlow;
	}

	// Token: 0x040011DF RID: 4575
	public Action Proceed;

	// Token: 0x040011E0 RID: 4576
	public Action Cancel;

	// Token: 0x040011E1 RID: 4577
	public Flow m_rootFlow;
}
