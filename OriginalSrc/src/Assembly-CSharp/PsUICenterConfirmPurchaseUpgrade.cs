using System;

// Token: 0x02000336 RID: 822
public class PsUICenterConfirmPurchaseUpgrade : PsUICenterConfirmPurchase
{
	// Token: 0x060017F9 RID: 6137 RVA: 0x00104752 File Offset: 0x00102B52
	public PsUICenterConfirmPurchaseUpgrade(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x0010475B File Offset: 0x00102B5B
	protected override void SetSize()
	{
		this.SetWidth(0.5f, RelativeTo.ScreenHeight);
		this.SetHeight(0.6f, RelativeTo.ScreenHeight);
	}
}
