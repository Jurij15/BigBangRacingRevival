using System;

// Token: 0x0200025C RID: 604
public class PsUISubMenuContentHeaders : UIVerticalList
{
	// Token: 0x0600122E RID: 4654 RVA: 0x000B3A04 File Offset: 0x000B1E04
	public PsUISubMenuContentHeaders(UIComponent _parent, string _header, string _caption)
		: base(_parent, "SubMenuContentHeaders")
	{
		this.RemoveDrawHandler();
		this.header = new UIText(this, false, string.Empty, _header.ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenShortest, "#ffd800", "#231f34");
		new UIText(this, false, string.Empty, _caption, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenShortest, "#fcffe8", null);
	}

	// Token: 0x0400155D RID: 5469
	public UIText header;
}
