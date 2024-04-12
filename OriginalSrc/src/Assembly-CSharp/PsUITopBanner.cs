using System;

// Token: 0x020002AD RID: 685
public class PsUITopBanner : UICanvas
{
	// Token: 0x0600149A RID: 5274 RVA: 0x000D37B8 File Offset: 0x000D1BB8
	public PsUITopBanner(UIComponent _parent)
		: base(_parent, false, "TopLevelBanner", null, string.Empty)
	{
		PsUILevelHeader psUILevelHeader = new PsUILevelHeader(this);
		psUILevelHeader.SetVerticalAlign(0.98f);
		this.RemoveDrawHandler();
	}
}
