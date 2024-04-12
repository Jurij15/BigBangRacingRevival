using System;

// Token: 0x02000581 RID: 1409
public class UIFittedTextButton : UIFittedText
{
	// Token: 0x06002909 RID: 10505 RVA: 0x001B392C File Offset: 0x001B1D2C
	public UIFittedTextButton(UIComponent _parent, string _tag, string _text)
		: base(_parent, true, _tag, _text, "HurmeRegular_Font", true, null, null)
	{
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(0.1f, RelativeTo.ScreenHeight);
	}
}
