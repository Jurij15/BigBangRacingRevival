using System;

// Token: 0x020003C7 RID: 967
public class PsUIPopupServerQueue : PsUIHeaderedCanvas
{
	// Token: 0x06001B78 RID: 7032 RVA: 0x0013248C File Offset: 0x0013088C
	public PsUIPopupServerQueue(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetWidth(0.4f, RelativeTo.ScreenWidth);
		this.SetHeight(0.35f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.Destroy();
		this.m_header = null;
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x00132558 File Offset: 0x00130958
	public void CreateContent(string[] _tags, string _textBox = null)
	{
		this.m_tags = _tags;
		new UITextbox(this, false, string.Empty, _textBox, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x0013258C File Offset: 0x0013098C
	public override void Step()
	{
		if (!HttpS.QueueTagsExist(this.m_tags))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Server");
			(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
			(this.GetRoot() as PsUIBasePopup).Destroy();
		}
		base.Step();
	}

	// Token: 0x04001DD8 RID: 7640
	private string[] m_tags;
}
