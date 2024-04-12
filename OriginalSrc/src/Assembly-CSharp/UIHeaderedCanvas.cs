using System;

// Token: 0x02000587 RID: 1415
public class UIHeaderedCanvas : UICanvas
{
	// Token: 0x06002924 RID: 10532 RVA: 0x00076C4C File Offset: 0x0007504C
	public UIHeaderedCanvas(UIComponent _parent, string _tag = "", float _headerHeight = 0.125f, RelativeTo _headerHeightRelativeTo = RelativeTo.ScreenHeight, float _footerHeight = 0f, RelativeTo _footerHeightRelativeTo = RelativeTo.ScreenHeight)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.m_container = new UICanvas(_parent, false, _tag + "Container", null, string.Empty);
		this.m_container.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_container.RemoveDrawHandler();
		this.m_header = new UICanvas(this.m_container, false, string.Empty, null, string.Empty);
		this.m_header.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_header.SetHeight(_headerHeight, _headerHeightRelativeTo);
		this.m_header.SetVerticalAlign(1f);
		this.m_header.RemoveDrawHandler();
		if (_footerHeight > 0f)
		{
			this.m_footer = new UICanvas(this.m_container, false, string.Empty, null, string.Empty);
			this.m_footer.SetWidth(1f, RelativeTo.ParentWidth);
			this.m_footer.SetHeight(_footerHeight, _footerHeightRelativeTo);
			this.m_footer.SetVerticalAlign(0f);
			this.m_footer.RemoveDrawHandler();
		}
		base.Parent(this.m_container);
		base.SetWidth(1f, RelativeTo.ParentWidth);
		base.SetHeight(1f, RelativeTo.ParentHeight);
		base.SetVerticalAlign(0f);
		base.RemoveDrawHandler();
	}

	// Token: 0x06002925 RID: 10533 RVA: 0x00076D94 File Offset: 0x00075194
	public override void Update()
	{
		float num = this.m_container.m_actualHeight - this.m_container.m_actualMargins.t - this.m_container.m_actualMargins.b;
		float num2 = 0f;
		if (this.m_header != null)
		{
			num2 = this.m_header.m_actualHeight;
		}
		base.SetHeight(1f - num2 / num, RelativeTo.ParentHeight);
		base.Update();
	}

	// Token: 0x06002926 RID: 10534 RVA: 0x00076E08 File Offset: 0x00075208
	public new virtual void Parent(UIComponent _parent)
	{
		this.m_container.Parent(_parent);
	}

	// Token: 0x06002927 RID: 10535 RVA: 0x00076E16 File Offset: 0x00075216
	public new virtual void DetachFromParent()
	{
		this.m_container.DetachFromParent();
	}

	// Token: 0x06002928 RID: 10536 RVA: 0x00076E23 File Offset: 0x00075223
	public new virtual void DetachFromParent(bool _addToManager)
	{
		this.m_container.DetachFromParent(_addToManager);
	}

	// Token: 0x06002929 RID: 10537 RVA: 0x00076E34 File Offset: 0x00075234
	public override void Destroy()
	{
		if (this.m_container != null && this.m_container.m_parent == null)
		{
			UIComponent container = this.m_container;
			this.m_container = null;
			container.Destroy();
		}
		else
		{
			base.Destroy();
		}
	}

	// Token: 0x0600292A RID: 10538 RVA: 0x00076E7B File Offset: 0x0007527B
	public new virtual void SetSize(float _widthRatio, float _heightRatio, RelativeTo _relativeTo)
	{
		this.m_container.SetSize(_widthRatio, _heightRatio, _relativeTo);
	}

	// Token: 0x0600292B RID: 10539 RVA: 0x00076E8B File Offset: 0x0007528B
	public new virtual void SetWidth(float _widthRatio, RelativeTo _relativeTo)
	{
		this.m_container.SetWidth(_widthRatio, _relativeTo);
	}

	// Token: 0x0600292C RID: 10540 RVA: 0x00076E9A File Offset: 0x0007529A
	public new virtual void SetHeight(float _heightRatio, RelativeTo _relativeTo)
	{
		this.m_container.SetHeight(_heightRatio, _relativeTo);
	}

	// Token: 0x0600292D RID: 10541 RVA: 0x00076EA9 File Offset: 0x000752A9
	public new virtual void SetAlign(float _horizontal, float _vertical)
	{
		this.m_container.SetAlign(_horizontal, _vertical);
	}

	// Token: 0x0600292E RID: 10542 RVA: 0x00076EB8 File Offset: 0x000752B8
	public new virtual void SetHorizontalAlign(float _horizontal)
	{
		this.m_container.SetHorizontalAlign(_horizontal);
	}

	// Token: 0x0600292F RID: 10543 RVA: 0x00076EC6 File Offset: 0x000752C6
	public new virtual void SetVerticalAlign(float _vertical)
	{
		this.m_container.SetVerticalAlign(_vertical);
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x00076ED4 File Offset: 0x000752D4
	public new virtual void SetDrawHandler(UIDrawDelegate _handler)
	{
		this.m_container.SetDrawHandler(_handler);
	}

	// Token: 0x06002931 RID: 10545 RVA: 0x00076EE2 File Offset: 0x000752E2
	public new virtual void RemoveDrawHandler()
	{
		this.m_container.RemoveDrawHandler();
	}

	// Token: 0x06002932 RID: 10546 RVA: 0x00076EEF File Offset: 0x000752EF
	public new virtual void RemoveTouchAreas()
	{
		this.m_container.RemoveTouchAreas();
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x00076EFC File Offset: 0x000752FC
	public new virtual void SetTouchHandler(TouchEventDelegate _handler)
	{
		this.m_container.SetTouchHandler(_handler);
	}

	// Token: 0x04002E30 RID: 11824
	protected UICanvas m_container;

	// Token: 0x04002E31 RID: 11825
	protected UICanvas m_header;

	// Token: 0x04002E32 RID: 11826
	protected UICanvas m_footer;
}
