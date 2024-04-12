using System;

// Token: 0x020003D8 RID: 984
public class UIPopupContentArea : UIVerticalList
{
	// Token: 0x06001BD0 RID: 7120 RVA: 0x0013595C File Offset: 0x00133D5C
	public UIPopupContentArea(UIComponent _parent, string _tag)
		: base(_parent, _tag)
	{
		this.SetMargins(0.075f, 0.075f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupContentArea));
	}
}
