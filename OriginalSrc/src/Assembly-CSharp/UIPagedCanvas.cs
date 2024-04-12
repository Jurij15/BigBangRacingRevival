using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000588 RID: 1416
public class UIPagedCanvas : UIScrollableCanvas
{
	// Token: 0x06002934 RID: 10548 RVA: 0x001B4271 File Offset: 0x001B2671
	public UIPagedCanvas(UIComponent _parent, string _tag)
		: base(_parent, _tag)
	{
		this.m_currentPage = 0;
		this.m_maxScrollInertialX = 50f / (1024f / (float)Screen.width);
		this.RemoveTouchAreas();
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x001B42A0 File Offset: 0x001B26A0
	public int GetCurrentPage()
	{
		return this.m_currentPage;
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x001B42A8 File Offset: 0x001B26A8
	public override void Step()
	{
		if (this.m_changingPage)
		{
			float num = 0f;
			if (this.m_contentWidth > this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r)
			{
				num = this.m_contentCenterX - this.m_contentWidth * 0.5f + this.m_actualWidth * 0.5f - this.m_actualMargins.l;
			}
			this.m_scrollInertiaX = Mathf.Min(this.m_maxScrollInertialX, Mathf.Max(-this.m_maxScrollInertialX, this.m_scrollInertiaX));
			float num2 = (float)this.m_currentPage * this.m_actualWidth;
			float num3 = this.m_scrollTC.transform.localPosition.x - num;
			float num4 = Mathf.Max(this.m_actualWidth, 0f);
			if (this.m_overrideScrollPosition)
			{
				num3 = num4 * this.m_currentScrollX + num;
				TransformS.SetPosition(this.m_scrollTC, new Vector3(num3, 0f, 0f));
				this.m_overrideScrollPosition = false;
				return;
			}
			float num5 = this.m_scrollInertiaX;
			if (num3 <= num2)
			{
				this.m_scrollInertiaX = (num3 - num2) * -(1f - this.m_scrollFallOff * 0.618f);
				this.m_scrollInertiaX = Mathf.Max(this.m_scrollInertiaX, (num3 - num2) * -(1f - this.m_scrollFallOff * 0.618f));
				if (Math.Abs(this.m_scrollInertiaX) < 0.1f)
				{
					this.m_scrollInertiaX = num3 - num2;
					this.m_changingPage = false;
					if (this.m_childs.Count > this.m_currentPage)
					{
						this.m_childs[this.m_currentPage].Focus();
					}
					this.EnableTouchAreas(true);
				}
				num5 = this.m_scrollInertiaX;
			}
			else if (num3 > num2)
			{
				this.m_scrollInertiaX = -(num2 - num3) * -(1f - this.m_scrollFallOff * 0.618f);
				this.m_scrollInertiaX = Mathf.Min(this.m_scrollInertiaX, -(num2 - num3) * -(1f - this.m_scrollFallOff * 0.618f));
				if (Math.Abs(this.m_scrollInertiaX) < 0.1f)
				{
					this.m_scrollInertiaX = -(num2 - num3);
					this.m_changingPage = false;
					if (this.m_childs.Count > this.m_currentPage)
					{
						this.m_childs[this.m_currentPage].Focus();
					}
					this.EnableTouchAreas(true);
				}
				num5 = this.m_scrollInertiaX;
			}
			else
			{
				if (Mathf.Abs(this.m_scrollInertiaX) > 0.1f)
				{
					this.m_scrollInertiaX *= this.m_scrollFallOff;
				}
				else
				{
					this.m_scrollInertiaX = 0f;
				}
				num5 = this.m_scrollInertiaX;
			}
			if (num4 > 0f)
			{
				this.m_currentScrollX = num3 / num4;
			}
			else
			{
				this.m_currentScrollX = 0f;
			}
			if (num5 != 0f)
			{
				TransformS.Move(this.m_scrollTC, new Vector3(num5, 0f, 0f));
			}
			this.ModifyChildCameras(this.m_scrollTC.transform.localPosition.x);
		}
		base.Step();
	}

	// Token: 0x06002937 RID: 10551 RVA: 0x001B45D5 File Offset: 0x001B29D5
	public virtual void NextPage()
	{
		this.m_currentPage++;
		this.m_changingPage = true;
	}

	// Token: 0x06002938 RID: 10552 RVA: 0x001B45EC File Offset: 0x001B29EC
	public virtual void PreviousPage()
	{
		this.m_currentPage--;
		this.m_changingPage = true;
	}

	// Token: 0x06002939 RID: 10553 RVA: 0x001B4604 File Offset: 0x001B2A04
	public virtual void GoToPage(int _pageIndex, bool _immediate)
	{
		this.m_currentPage = _pageIndex;
		this.m_changingPage = true;
		if (_immediate)
		{
			float num = (float)_pageIndex * this.m_actualWidth;
			float num2 = (float)this.m_childs.Count * this.m_actualWidth;
			if (this.m_childs.Count == 0)
			{
				this.m_currentScrollX = 0f;
			}
			else
			{
				this.m_currentScrollX = num / num2;
			}
			this.m_overrideScrollPosition = true;
			this.Step();
		}
	}

	// Token: 0x0600293A RID: 10554 RVA: 0x001B4679 File Offset: 0x001B2A79
	public virtual bool IsChangingPage()
	{
		return this.m_changingPage;
	}

	// Token: 0x0600293B RID: 10555 RVA: 0x001B468C File Offset: 0x001B2A8C
	public override void Update()
	{
		base.Update();
		this.m_childsWithUniqueCameras = this.GetChildsWithUniqueCamera(this);
		this.ModifyChildCameras(this.m_scrollTC.transform.localPosition.x);
	}

	// Token: 0x0600293C RID: 10556 RVA: 0x001B46CA File Offset: 0x001B2ACA
	public override void UpdateSize()
	{
		base.UpdateSize();
	}

	// Token: 0x0600293D RID: 10557 RVA: 0x001B46D2 File Offset: 0x001B2AD2
	public override void DestroyChildren()
	{
		base.DestroyChildren();
		this.m_currentPage = 0;
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x001B46E4 File Offset: 0x001B2AE4
	protected void ModifyChildCameras(float _posX)
	{
		for (int i = 0; i < this.m_childsWithUniqueCameras.Count; i++)
		{
			Vector3 localPosition = this.m_childsWithUniqueCameras[i].m_camera.transform.localPosition;
			localPosition.x = _posX - this.m_actualWidth * (float)i;
			this.m_childsWithUniqueCameras[i].m_camera.transform.localPosition = localPosition;
		}
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x001B4758 File Offset: 0x001B2B58
	protected List<UIComponent> GetChildsWithUniqueCamera(UIComponent _parent)
	{
		List<UIComponent> list = new List<UIComponent>();
		for (int i = 0; i < _parent.m_childs.Count; i++)
		{
			if (_parent.m_childs[i].m_uniqueCamera)
			{
				list.Add(_parent.m_childs[i]);
			}
			else
			{
				list.AddRange(this.GetChildsWithUniqueCamera(_parent.m_childs[i]));
			}
		}
		return list;
	}

	// Token: 0x04002E33 RID: 11827
	private List<UIComponent> m_childsWithUniqueCameras;

	// Token: 0x04002E34 RID: 11828
	protected int m_currentPage;

	// Token: 0x04002E35 RID: 11829
	protected bool m_changingPage;
}
