using System;
using UnityEngine;

// Token: 0x0200058C RID: 1420
public class UIScrollableSnappingCanvas : UIScrollableCanvas
{
	// Token: 0x0600296B RID: 10603 RVA: 0x001B4A09 File Offset: 0x001B2E09
	public UIScrollableSnappingCanvas(UIComponent _parent, string _tag, int _itemCount)
		: base(_parent, _tag)
	{
		this.m_itemCount = _itemCount;
		this.m_maxScrollInertialX = 200f / (1024f / (float)Screen.width);
		this.m_maxScrollInertialY = 0f;
		this.m_dragStartPos = Vector2.zero;
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x001B4A48 File Offset: 0x001B2E48
	public void SetPageChangeDelegate(Action<int> _action)
	{
		this.d_pageChangeDelegate = _action;
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x001B4A54 File Offset: 0x001B2E54
	public override void Step()
	{
		float num = (this.m_contentWidth - this.m_actualWidth + this.m_actualMargins.l + this.m_actualMargins.r) / (float)(this.m_itemCount - 1);
		this.m_currentSnapValueX = this.m_xPos / num;
		this.m_previousPageX = this.m_currentPageX;
		this.m_currentPageX = Mathf.RoundToInt(this.m_currentSnapValueX);
		if (this.m_previousPageX != this.m_currentPageX && this.d_pageChangeDelegate != null)
		{
			this.d_pageChangeDelegate.Invoke(this.m_currentPageX);
		}
		if (!this.m_draggingLeft && !this.m_draggingRight)
		{
			if (this.m_changePage == 0 && this.m_scrollInertiaMultiplerX > 0f && (Input.GetKeyDown(275) || this.m_dragX < (float)Screen.width * -0.2f))
			{
				if (this.m_currentPageX < this.m_itemCount - 1)
				{
					this.m_changePage = 1;
				}
				this.m_draggingLeft = false;
				this.m_dragX = 0f;
			}
			else if (this.m_changePage == 0 && this.m_scrollInertiaMultiplerX > 0f && (Input.GetKeyDown(276) || this.m_dragX > (float)Screen.width * 0.2f))
			{
				if (this.m_currentPageX > 0)
				{
					this.m_changePage = -1;
				}
				this.m_draggingRight = false;
				this.m_dragX = 0f;
			}
			if (this.m_prevSnapPos != (float)this.m_currentPageX)
			{
				this.m_changePage = 0;
			}
			this.m_prevSnapPos = (float)this.m_currentPageX;
			float num2 = num * (float)(this.m_currentPageX + this.m_changePage);
			float num3 = (num2 - this.m_xPos) * 0.175f;
			if (num3 != 0f)
			{
				this.m_scrollInertiaX = (this.m_scrollInertiaX + num3) / 2f;
			}
			if (this.m_changePage != 0)
			{
				this.m_allowScroll = true;
			}
		}
		base.Step();
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x001B4C54 File Offset: 0x001B3054
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchPhase == TouchAreaPhase.Began || _touchPhase == TouchAreaPhase.RollIn)
		{
			this.m_dragStartPos.x = _touches[0].m_currentPosition.x;
			this.m_dragStartPos.y = _touches[0].m_currentPosition.y;
			this.m_dragX = _touches[0].m_deltaPosition.x;
			this.m_dragY = _touches[0].m_deltaPosition.y;
		}
		else if (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut)
		{
			this.m_dragX = _touches[0].m_currentPosition.x - this.m_dragStartPos.x;
			this.m_dragY = _touches[0].m_currentPosition.y - this.m_dragStartPos.y;
			if (this.m_dragX > 0f)
			{
				this.m_draggingRight = true;
				this.m_draggingLeft = false;
			}
			else if (this.m_dragX < 0f)
			{
				this.m_draggingLeft = true;
				this.m_draggingRight = false;
			}
		}
		else if (_touchPhase == TouchAreaPhase.RollOut || _touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut)
		{
			this.m_draggingLeft = false;
			this.m_draggingRight = false;
		}
		base.TouchHandler(_touchArea, _touchPhase, _touchIsSecondary, _touchCount, _touches);
	}

	// Token: 0x0600296F RID: 10607 RVA: 0x001B4D91 File Offset: 0x001B3191
	public override void FreezeHorizontalScroll(bool _affectWholeHierarchy)
	{
		base.FreezeHorizontalScroll(_affectWholeHierarchy);
		this.m_draggingLeft = false;
		this.m_draggingRight = false;
	}

	// Token: 0x04002E6A RID: 11882
	public float m_prevSnapPos;

	// Token: 0x04002E6B RID: 11883
	public int m_changePage;

	// Token: 0x04002E6C RID: 11884
	public int m_itemCount;

	// Token: 0x04002E6D RID: 11885
	private bool m_draggingRight;

	// Token: 0x04002E6E RID: 11886
	private bool m_draggingLeft;

	// Token: 0x04002E6F RID: 11887
	public float m_currentSnapValueX;

	// Token: 0x04002E70 RID: 11888
	public int m_currentPageX;

	// Token: 0x04002E71 RID: 11889
	public int m_previousPageX;

	// Token: 0x04002E72 RID: 11890
	public Action<int> d_pageChangeDelegate;
}
