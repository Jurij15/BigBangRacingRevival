using System;

// Token: 0x0200024E RID: 590
public class PsUILoadingText : UICanvas
{
	// Token: 0x060011D8 RID: 4568 RVA: 0x000AF6B8 File Offset: 0x000ADAB8
	public PsUILoadingText(UIComponent _parent)
		: base(_parent, false, "PsUILoadingText", null, string.Empty)
	{
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetAlign(1f, 0f);
		uihorizontalList.SetMargins(0f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		new PsUILoadingAnimation(uihorizontalList, false).SetSize(0.15f, 0.15f, RelativeTo.ScreenHeight);
		new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.LOADING), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.06f, RelativeTo.ScreenHeight, null, null);
	}
}
