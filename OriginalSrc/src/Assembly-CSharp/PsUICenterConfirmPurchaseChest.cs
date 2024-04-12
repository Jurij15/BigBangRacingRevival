using System;

// Token: 0x02000337 RID: 823
public class PsUICenterConfirmPurchaseChest : PsUICenterConfirmPurchase
{
	// Token: 0x060017FB RID: 6139 RVA: 0x00104775 File Offset: 0x00102B75
	public PsUICenterConfirmPurchaseChest(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x0010477E File Offset: 0x00102B7E
	protected override void SetSize()
	{
		this.SetWidth(0.78f, RelativeTo.ScreenHeight);
		this.SetHeight(0.8f, RelativeTo.ScreenHeight);
	}
}
