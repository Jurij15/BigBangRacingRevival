using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class PsUIScrollableBase : PsUIHeaderedCanvas
{
	// Token: 0x060017B1 RID: 6065 RVA: 0x001004E4 File Offset: 0x000FE8E4
	public PsUIScrollableBase(UIComponent _parent)
		: base(_parent, "ScrollableContent", true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.8f, RelativeTo.ScreenWidth);
		this.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_scrollArea = new UIScrollableCanvas(this, string.Empty);
		this.m_scrollArea.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_scrollArea.m_maxScrollInertialY = 0f;
		this.m_scrollArea.m_maxScrollInertialX = 50f / (1024f / (float)Screen.width);
		this.m_scrollArea.RemoveDrawHandler();
		this.m_scrollArea.m_passTouchesToScrollableParents = true;
		this.CreateContent(this.m_scrollArea);
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x00100631 File Offset: 0x000FEA31
	public virtual void CreateHeaderContent(UIComponent _parent)
	{
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x00100633 File Offset: 0x000FEA33
	public virtual void CreateContent(UIComponent _parent)
	{
	}

	// Token: 0x04001A84 RID: 6788
	protected UIScrollableCanvas m_scrollArea;
}
