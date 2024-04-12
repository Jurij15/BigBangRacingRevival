using System;

// Token: 0x02000355 RID: 853
public abstract class PsUITopPlay : UICanvas
{
	// Token: 0x060018E8 RID: 6376 RVA: 0x0010D7FC File Offset: 0x0010BBFC
	public PsUITopPlay(Action _restartAction = null, Action _pauseAction = null)
		: base(null, false, "TopContent", null, string.Empty)
	{
		this.m_restartAction = _restartAction;
		this.m_pauseAction = _pauseAction;
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x0010D81F File Offset: 0x0010BC1F
	public override void Step()
	{
		base.Step();
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x0010D827 File Offset: 0x0010BC27
	public override void Update()
	{
		base.Update();
		this.HideUIButton();
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x0010D835 File Offset: 0x0010BC35
	private void HideUIButton()
	{
		if (this.m_hideUIButton != null)
		{
			EntityManager.SetVisibilityOfEntity(this.m_hideUIButton.m_TC.p_entity, false);
		}
	}

	// Token: 0x060018EC RID: 6380
	public abstract void CreateRestartArea(UIComponent _parent);

	// Token: 0x060018ED RID: 6381
	public abstract void CreateCoinArea();

	// Token: 0x060018EE RID: 6382
	public abstract void CreateLeftArea();

	// Token: 0x04001B7D RID: 7037
	protected Action m_restartAction;

	// Token: 0x04001B7E RID: 7038
	protected Action m_pauseAction;

	// Token: 0x04001B7F RID: 7039
	public UIHorizontalList m_rightArea;

	// Token: 0x04001B80 RID: 7040
	protected UIHorizontalList m_leftArea;

	// Token: 0x04001B81 RID: 7041
	protected PsUIGenericButton m_hideUIButton;
}
