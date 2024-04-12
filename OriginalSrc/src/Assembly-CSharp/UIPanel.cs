using System;

// Token: 0x02000589 RID: 1417
public class UIPanel : UIPagedCanvas
{
	// Token: 0x06002940 RID: 10560 RVA: 0x001B47D0 File Offset: 0x001B2BD0
	public UIPanel(UIComponent _parent, string _tag, UIComponent _header, UIComponent _footer)
		: base(null, _tag)
	{
		this.m_maxScrollInertialX = 50f;
		this.m_maxScrollInertialY = 0f;
		this.m_container = new UICanvas(_parent, false, _tag + "Container", null, string.Empty);
		this.m_container.SetWidth(1f, RelativeTo.ParentWidth);
		if (_header != null)
		{
			this.m_header = _header;
			this.m_header.Parent(this.m_container);
			this.m_header.SetWidth(1f, RelativeTo.ParentWidth);
			this.m_header.SetVerticalAlign(1f);
		}
		if (_footer != null)
		{
			this.m_footer = _footer;
			this.m_footer.Parent(this.m_container);
			this.m_footer.SetWidth(1f, RelativeTo.ParentWidth);
			this.m_footer.SetVerticalAlign(0f);
		}
		base.Parent(this.m_container);
		base.SetWidth(1f, RelativeTo.ParentWidth);
		base.SetHeight(1f, RelativeTo.ParentHeight);
	}

	// Token: 0x06002941 RID: 10561 RVA: 0x001B48D0 File Offset: 0x001B2CD0
	public override void Update()
	{
		float num = this.m_container.m_actualHeight - this.m_container.m_actualMargins.t - this.m_container.m_actualMargins.b;
		float num2 = 0f;
		float num3 = 0f;
		if (this.m_header != null)
		{
			num2 = this.m_header.m_actualHeight;
		}
		if (this.m_footer != null)
		{
			num3 = this.m_footer.m_actualHeight;
		}
		float num4 = num2 + num3;
		float num5 = num2 - num3;
		base.SetHeight(1f - num4 / num, RelativeTo.ParentHeight);
		base.SetVerticalAlign(0.5f - num5 * 0.5f / num4);
		base.Update();
	}

	// Token: 0x06002942 RID: 10562 RVA: 0x001B497A File Offset: 0x001B2D7A
	public new virtual void Parent(UIComponent _parent)
	{
		this.m_container.Parent(_parent);
	}

	// Token: 0x06002943 RID: 10563 RVA: 0x001B4988 File Offset: 0x001B2D88
	public new virtual void DetachFromParent()
	{
		this.m_container.DetachFromParent();
	}

	// Token: 0x06002944 RID: 10564 RVA: 0x001B4995 File Offset: 0x001B2D95
	public new virtual void DetachFromParent(bool _addToManager)
	{
		this.m_container.DetachFromParent(_addToManager);
	}

	// Token: 0x06002945 RID: 10565 RVA: 0x001B49A3 File Offset: 0x001B2DA3
	public new virtual void Destroy()
	{
		this.m_container.Destroy();
	}

	// Token: 0x06002946 RID: 10566 RVA: 0x001B49B0 File Offset: 0x001B2DB0
	public new virtual void SetSize(float _widthRatio, float _heightRatio, RelativeTo _relativeTo)
	{
		this.m_container.SetSize(_widthRatio, _heightRatio, _relativeTo);
	}

	// Token: 0x06002947 RID: 10567 RVA: 0x001B49C0 File Offset: 0x001B2DC0
	public new virtual void SetWidth(float _widthRatio, RelativeTo _relativeTo)
	{
		this.m_container.SetWidth(_widthRatio, _relativeTo);
	}

	// Token: 0x06002948 RID: 10568 RVA: 0x001B49CF File Offset: 0x001B2DCF
	public new virtual void SetHeight(float _heightRatio, RelativeTo _relativeTo)
	{
		this.m_container.SetHeight(_heightRatio, _relativeTo);
	}

	// Token: 0x06002949 RID: 10569 RVA: 0x001B49DE File Offset: 0x001B2DDE
	public new virtual void SetAlign(float _horizontal, float _vertical)
	{
		this.m_container.SetAlign(_horizontal, _vertical);
	}

	// Token: 0x0600294A RID: 10570 RVA: 0x001B49ED File Offset: 0x001B2DED
	public new virtual void SetHorizontalAlign(float _horizontal)
	{
		this.m_container.SetHorizontalAlign(_horizontal);
	}

	// Token: 0x0600294B RID: 10571 RVA: 0x001B49FB File Offset: 0x001B2DFB
	public new virtual void SetVerticalAlign(float _vertical)
	{
		this.m_container.SetVerticalAlign(_vertical);
	}

	// Token: 0x04002E36 RID: 11830
	public UICanvas m_container;

	// Token: 0x04002E37 RID: 11831
	public UIComponent m_header;

	// Token: 0x04002E38 RID: 11832
	public UIComponent m_footer;
}
