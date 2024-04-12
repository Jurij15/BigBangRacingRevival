using System;

// Token: 0x020003D9 RID: 985
public class UIPopupHeader : UIVerticalList
{
	// Token: 0x06001BD1 RID: 7121 RVA: 0x001359BC File Offset: 0x00133DBC
	public UIPopupHeader(UIComponent _parent, string _tag, string _header, string _caption)
		: base(_parent, _tag)
	{
		this.SetHeight(0.2f, RelativeTo.ScreenHeight);
		this.SetMargins(0.05f, 0.05f, 0f, 0.025f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		new UITextbox(this, false, "SavePanelHeaderHeader", "<color=#bfff3f>" + _header + "</color>", PsFontManager.GetFont(PsFonts.KGLetHerGo), 0.07f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		if (_caption != string.Empty)
		{
			new UITextbox(this, false, "SavePanelHeaderCaption", _caption, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		}
	}
}
