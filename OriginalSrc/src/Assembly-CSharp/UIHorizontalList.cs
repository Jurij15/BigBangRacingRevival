using System;
using UnityEngine;

// Token: 0x0200058E RID: 1422
public class UIHorizontalList : UICanvas
{
	// Token: 0x06002974 RID: 10612 RVA: 0x000A301A File Offset: 0x000A141A
	public UIHorizontalList(UIComponent _parent, string _tag)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(0f, RelativeTo.ParentHeight);
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x000A3043 File Offset: 0x000A1443
	public virtual void SetSpacing(float _value, RelativeTo _relativeTo)
	{
		this.m_spacing = _value;
		this.m_spacingRelativeTo = _relativeTo;
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x000A3054 File Offset: 0x000A1454
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

	// Token: 0x06002977 RID: 10615 RVA: 0x000A31D4 File Offset: 0x000A15D4
	public override void SetHeight(float _heightRatio, RelativeTo _relativeTo)
	{
		this.m_forcedHeight = _heightRatio;
		base.SetHeight(_heightRatio, _relativeTo);
	}

	// Token: 0x06002978 RID: 10616 RVA: 0x000A31E8 File Offset: 0x000A15E8
	public override void ArrangeContents()
	{
		float num = 99999f;
		float num2 = -99999f;
		float num3 = 99999f;
		float num4 = -99999f;
		this.m_contentX = this.m_actualWidth * -0.5f + this.m_actualMargins.l;
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
					this.m_contentX += this.m_actualSpacing;
				}
				float num7 = this.m_contentX + uicomponent.m_actualWidth * 0.5f;
				uicomponent.UpdateVerticalAlign(num7);
				float num8 = this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r;
				float num9 = num8 - uicomponent.m_actualWidth;
				if (num9 > 0f)
				{
					float num10 = (num7 + num9 * 0.5f) / num9;
					uicomponent.SetHorizontalAlign(num10);
				}
				this.m_contentX += uicomponent.m_actualWidth;
				if (this.m_forcedHeight == 0f)
				{
					if (uicomponent.m_actualHeight > num5)
					{
						num5 = uicomponent.m_actualHeight;
					}
				}
				else
				{
					float num11 = uicomponent.m_TC.transform.localPosition.y - uicomponent.m_actualHeight * 0.5f;
					float num12 = uicomponent.m_TC.transform.localPosition.y + uicomponent.m_actualHeight * 0.5f;
					if (num12 > num4)
					{
						num4 = num12;
					}
					if (num11 < num3)
					{
						num3 = num11;
					}
				}
				float num13 = uicomponent.m_TC.transform.localPosition.x - uicomponent.m_actualWidth * 0.5f;
				float num14 = uicomponent.m_TC.transform.localPosition.x + uicomponent.m_actualWidth * 0.5f;
				if (num14 > num2)
				{
					num2 = num14;
				}
				if (num13 < num)
				{
					num = num13;
				}
			}
		}
		if (this.m_forcedHeight == 0f)
		{
			this.m_contentHeight = num5;
			this.m_contentCenterY = 0f;
		}
		else
		{
			this.m_contentHeight = num4 - num3;
			this.m_contentCenterY = num3 + this.m_contentHeight * 0.5f;
		}
		this.m_contentWidth = num2 - num;
		this.m_contentCenterX = num + this.m_contentWidth * 0.5f;
		this.m_contentY = this.m_actualHeight * 0.5f - this.m_actualMargins.t - this.m_contentHeight - this.m_actualMargins.b;
		this.m_contentX += this.m_actualMargins.l;
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x000A34C8 File Offset: 0x000A18C8
	public override void Update()
	{
		this.SetWidth(1f, RelativeTo.ParentWidth);
		if (this.m_forcedHeight == 0f)
		{
			base.SetHeight(1f, RelativeTo.ParentHeight);
		}
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateMargins();
		this.UpdateSpacing();
		this.UpdateChildren();
		this.ArrangeContents();
		float num = this.m_contentWidth + this.m_actualMargins.l + this.m_actualMargins.r;
		float num2 = num / this.m_tempReferenceWidth;
		if (this.m_tempReferenceWidth == 0f)
		{
			num2 = (float)Screen.width;
		}
		this.SetWidth(num2, RelativeTo.ParentWidth);
		if (this.m_forcedHeight == 0f)
		{
			float num3 = this.m_contentHeight + this.m_actualMargins.t + this.m_actualMargins.b;
			float num4 = num3 / this.m_tempReferenceHeight;
			if (this.m_tempReferenceHeight == 0f)
			{
				num4 = (float)Screen.height;
			}
			base.SetHeight(num4, RelativeTo.ParentHeight);
		}
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

	// Token: 0x0600297A RID: 10618 RVA: 0x000A360A File Offset: 0x000A1A0A
	public override void RemoveTouchAreas()
	{
		Debug.LogWarning("unnecessary touch area removal");
		base.RemoveTouchAreas();
	}

	// Token: 0x04002E7E RID: 11902
	public float m_spacing;

	// Token: 0x04002E7F RID: 11903
	public float m_actualSpacing;

	// Token: 0x04002E80 RID: 11904
	public RelativeTo m_spacingRelativeTo;

	// Token: 0x04002E81 RID: 11905
	private float m_forcedHeight;
}
