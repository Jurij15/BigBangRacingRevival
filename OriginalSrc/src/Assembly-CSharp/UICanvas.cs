using System;

// Token: 0x02000586 RID: 1414
public class UICanvas : UIComponent
{
	// Token: 0x06002922 RID: 10530 RVA: 0x0003C99E File Offset: 0x0003AD9E
	public UICanvas(UIComponent _parent, bool _touchable, string _tag, UIModel _model = null, string _fieldName = "")
		: base(_parent, _touchable, _tag, null, _model, _fieldName)
	{
	}

	// Token: 0x06002923 RID: 10531 RVA: 0x0003C9B0 File Offset: 0x0003ADB0
	public override void ArrangeContents()
	{
		float num = 99999f;
		float num2 = -99999f;
		float num3 = 99999f;
		float num4 = -99999f;
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			UIComponent uicomponent = this.m_childs[i];
			if (!uicomponent.m_rogue)
			{
				float num5 = uicomponent.m_TC.transform.localPosition.x - uicomponent.m_actualWidth * 0.5f;
				float num6 = uicomponent.m_TC.transform.localPosition.x + uicomponent.m_actualWidth * 0.5f;
				float num7 = uicomponent.m_TC.transform.localPosition.y - uicomponent.m_actualHeight * 0.5f;
				float num8 = uicomponent.m_TC.transform.localPosition.y + uicomponent.m_actualHeight * 0.5f;
				if (num6 > num2)
				{
					num2 = num6;
				}
				if (num5 < num)
				{
					num = num5;
				}
				if (num8 > num4)
				{
					num4 = num8;
				}
				if (num7 < num3)
				{
					num3 = num7;
				}
			}
		}
		this.m_contentWidth = num2 - num;
		this.m_contentHeight = num4 - num3;
		this.m_contentCenterX = num + this.m_contentWidth * 0.5f;
		this.m_contentCenterY = num3 + this.m_contentHeight * 0.5f;
		this.m_contentX = this.m_actualWidth * -0.5f + this.m_actualMargins.l + this.m_contentWidth + this.m_actualMargins.r;
		this.m_contentY = this.m_actualHeight * 0.5f - this.m_actualMargins.t - this.m_contentHeight - this.m_actualMargins.b;
	}

	// Token: 0x04002E2A RID: 11818
	public float m_contentX;

	// Token: 0x04002E2B RID: 11819
	public float m_contentY;

	// Token: 0x04002E2C RID: 11820
	public float m_contentWidth;

	// Token: 0x04002E2D RID: 11821
	public float m_contentHeight;

	// Token: 0x04002E2E RID: 11822
	public float m_contentCenterX;

	// Token: 0x04002E2F RID: 11823
	public float m_contentCenterY;
}
