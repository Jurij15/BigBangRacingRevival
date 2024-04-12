using System;
using UnityEngine;

// Token: 0x0200058A RID: 1418
public class UIScaleToContentCanvas : UICanvas
{
	// Token: 0x0600294C RID: 10572 RVA: 0x001252B4 File Offset: 0x001236B4
	public UIScaleToContentCanvas(UIComponent _parent, string _tag, bool _shrinkHorizontally = true, bool _shrinkVertically = true)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_shrinkHorizontally = _shrinkHorizontally;
		this.m_shrinkVertically = _shrinkVertically;
	}

	// Token: 0x0600294D RID: 10573 RVA: 0x001252EC File Offset: 0x001236EC
	public override void Update()
	{
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateMargins();
		this.UpdateChildren();
		this.ArrangeContents();
		float num = this.m_contentWidth + this.m_actualMargins.l + this.m_actualMargins.r;
		float num2 = num / (float)Screen.width;
		if (this.m_shrinkHorizontally || num > this.m_actualWidth)
		{
			this.SetWidth(num2, RelativeTo.ScreenWidth);
		}
		float num3 = this.m_contentHeight + this.m_actualMargins.t + this.m_actualMargins.b;
		float num4 = num3 / (float)Screen.height;
		if (this.m_shrinkVertically || num3 > this.m_actualHeight)
		{
			this.SetHeight(num4, RelativeTo.ScreenHeight);
		}
		this.UpdateSize();
		this.UpdateMargins();
		this.UpdateAlign();
		this.UpdateChildrenAlign();
		if (!this.m_hidden && this.d_Draw != null)
		{
			this.d_Draw(this);
		}
	}

	// Token: 0x04002E39 RID: 11833
	private bool m_shrinkHorizontally;

	// Token: 0x04002E3A RID: 11834
	private bool m_shrinkVertically;
}
