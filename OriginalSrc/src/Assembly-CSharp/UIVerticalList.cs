using System;
using UnityEngine;

// Token: 0x0200058F RID: 1423
public class UIVerticalList : UICanvas
{
	// Token: 0x0600297B RID: 10619 RVA: 0x0003FAF7 File Offset: 0x0003DEF7
	public UIVerticalList(UIComponent _parent, string _tag)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.SetWidth(0f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x0003FB20 File Offset: 0x0003DF20
	public virtual void SetSpacing(float _value, RelativeTo _relativeTo)
	{
		this.m_spacing = _value;
		this.m_spacingRelativeTo = _relativeTo;
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x0003FB30 File Offset: 0x0003DF30
	public virtual void UpdateSpacing()
	{
		RelativeTo relativeTo = this.m_spacingRelativeTo;
		if (this.m_spacingRelativeTo == RelativeTo.ParentShortest)
		{
			relativeTo = ((this.m_parent.m_actualWidth <= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
		}
		else if (this.m_spacingRelativeTo == RelativeTo.ParentLongest)
		{
			relativeTo = ((this.m_parent.m_actualWidth >= this.m_parent.m_actualHeight) ? RelativeTo.ParentWidth : RelativeTo.ParentHeight);
		}
		else if (this.m_spacingRelativeTo == RelativeTo.ScreenShortest)
		{
			relativeTo = ((Screen.width <= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
		}
		else if (this.m_spacingRelativeTo == RelativeTo.ScreenLongest)
		{
			relativeTo = ((Screen.width >= Screen.height) ? RelativeTo.ScreenWidth : RelativeTo.ScreenHeight);
		}
		float num = 0f;
		if (relativeTo == RelativeTo.ParentWidth)
		{
			if (this.m_parent != null)
			{
				num = this.m_parent.m_actualWidth;
			}
			else
			{
				num = (float)Screen.width;
			}
		}
		else if (relativeTo == RelativeTo.ParentHeight)
		{
			if (this.m_parent != null)
			{
				num = this.m_parent.m_actualHeight;
			}
			else
			{
				num = (float)Screen.height;
			}
		}
		else if (relativeTo == RelativeTo.ScreenHeight)
		{
			num = (float)Screen.height;
		}
		else if (relativeTo == RelativeTo.ScreenWidth)
		{
			num = (float)Screen.width;
		}
		else if (relativeTo == RelativeTo.OwnHeight)
		{
			num = this.m_actualHeight;
		}
		else if (relativeTo == RelativeTo.OwnWidth)
		{
			num = this.m_actualWidth;
		}
		this.m_actualSpacing = this.m_spacing * num;
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x0003FCB0 File Offset: 0x0003E0B0
	public override void SetWidth(float _widthRatio, RelativeTo _relativeTo)
	{
		this.m_forcedWidth = _widthRatio;
		base.SetWidth(_widthRatio, _relativeTo);
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x0003FCC4 File Offset: 0x0003E0C4
	public override void ArrangeContents()
	{
		float num = 99999f;
		float num2 = -99999f;
		float num3 = 99999f;
		float num4 = -99999f;
		this.m_contentY = this.m_actualHeight * 0.5f - this.m_actualMargins.t;
		float num5 = 0f;
		int num6 = 0;
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			UIComponent uicomponent = this.m_childs[i];
			if (uicomponent.m_rogue)
			{
				num6++;
			}
			else
			{
				if (i - num6 > 0)
				{
					this.m_contentY -= this.m_actualSpacing;
				}
				float num7 = this.m_contentY - uicomponent.m_actualHeight * 0.5f;
				uicomponent.UpdateHorizontalAlign(num7);
				float num8 = this.m_actualHeight - this.m_actualMargins.b - this.m_actualMargins.t;
				float num9 = num8 - uicomponent.m_actualHeight;
				if (num9 > 0f)
				{
					float num10 = (num7 + num9 * 0.5f) / num9;
					uicomponent.SetVerticalAlign(num10);
				}
				this.m_contentY -= uicomponent.m_actualHeight;
				if (this.m_forcedWidth == 0f)
				{
					if (uicomponent.m_actualWidth > num5)
					{
						num5 = uicomponent.m_actualWidth;
					}
				}
				else
				{
					float num11 = uicomponent.m_TC.transform.localPosition.x - uicomponent.m_actualWidth * 0.5f;
					float num12 = uicomponent.m_TC.transform.localPosition.x + uicomponent.m_actualWidth * 0.5f;
					if (num12 > num2)
					{
						num2 = num12;
					}
					if (num11 < num)
					{
						num = num11;
					}
				}
				float num13 = uicomponent.m_TC.transform.localPosition.y - uicomponent.m_actualHeight * 0.5f;
				float num14 = uicomponent.m_TC.transform.localPosition.y + uicomponent.m_actualHeight * 0.5f;
				if (num14 > num4)
				{
					num4 = num14;
				}
				if (num13 < num3)
				{
					num3 = num13;
				}
			}
		}
		if (this.m_forcedWidth == 0f)
		{
			this.m_contentWidth = num5;
			this.m_contentCenterX = 0f;
		}
		else
		{
			this.m_contentWidth = num2 - num;
			this.m_contentCenterX = num + this.m_contentWidth * 0.5f;
		}
		this.m_contentHeight = num4 - num3;
		this.m_contentCenterY = num3 + this.m_contentHeight * 0.5f;
		this.m_contentX = this.m_actualWidth * -0.5f + this.m_actualMargins.l + this.m_contentWidth + this.m_actualMargins.r;
		this.m_contentY -= this.m_actualMargins.t;
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x0003FFA4 File Offset: 0x0003E3A4
	public override void Update()
	{
		if (this.m_forcedWidth == 0f)
		{
			base.SetWidth(1f, RelativeTo.ParentWidth);
		}
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateMargins();
		this.UpdateSpacing();
		this.UpdateChildren();
		this.ArrangeContents();
		this.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateChildrenAlign();
		this.ArrangeContents();
		if (!this.m_hidden && this.d_Draw != null)
		{
			this.d_Draw(this);
		}
		if (this.m_parent == null)
		{
			this.UpdateSpecial();
		}
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x00040050 File Offset: 0x0003E450
	public void UpdateDimensions()
	{
		if (this.m_forcedWidth == 0f)
		{
			float num = this.m_contentWidth + this.m_actualMargins.l + this.m_actualMargins.r;
			float num2 = num / this.m_tempReferenceWidth;
			if (this.m_tempReferenceWidth == 0f)
			{
				num2 = (float)Screen.width;
			}
			base.SetWidth(num2, RelativeTo.ParentWidth);
		}
		float num3 = this.m_contentHeight + this.m_actualMargins.t + this.m_actualMargins.b;
		float num4 = num3 / this.m_tempReferenceHeight;
		if (this.m_tempReferenceHeight == 0f)
		{
			num4 = (float)Screen.height;
		}
		this.SetHeight(num4, RelativeTo.ParentHeight);
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x000400FB File Offset: 0x0003E4FB
	public override void RemoveTouchAreas()
	{
		Debug.LogWarning("unnecessary touch area removal");
		base.RemoveTouchAreas();
	}

	// Token: 0x04002E82 RID: 11906
	public float m_spacing;

	// Token: 0x04002E83 RID: 11907
	public float m_actualSpacing;

	// Token: 0x04002E84 RID: 11908
	public RelativeTo m_spacingRelativeTo;

	// Token: 0x04002E85 RID: 11909
	private float m_forcedWidth;
}
