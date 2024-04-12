using System;

// Token: 0x02000570 RID: 1392
public class TextModel
{
	// Token: 0x06002897 RID: 10391 RVA: 0x001AFD8C File Offset: 0x001AE18C
	public TextModel(string _default, Action<string, object, object> _valueChangeDelegate = null)
	{
		this.m_text = _default;
		this.m_done = false;
		this.m_cancelled = false;
		this.m_uiModel = new UIModel(this, _valueChangeDelegate);
	}

	// Token: 0x04002DC7 RID: 11719
	public string m_text;

	// Token: 0x04002DC8 RID: 11720
	public bool m_done;

	// Token: 0x04002DC9 RID: 11721
	public bool m_cancelled;

	// Token: 0x04002DCA RID: 11722
	public UIModel m_uiModel;
}
