using System;
using UnityEngine;

// Token: 0x0200058D RID: 1421
public class UIIconLabel : UIComponent
{
	// Token: 0x06002970 RID: 10608 RVA: 0x001B4DA8 File Offset: 0x001B31A8
	public UIIconLabel(UIComponent _parent, string _tag, SpriteSheet _spriteSheet, Frame _frame, Align _horizontalAlign, Align _verticalAlign)
		: base(_parent, false, _tag, null, null, string.Empty)
	{
		this.m_horizontalLabelAlign = _horizontalAlign;
		this.m_verticalLabelAlign = _verticalAlign;
		this.SetWidth(UIIconLabel.m_defaultWidth, UIIconLabel.m_defaultWidthRelativeTo);
		this.SetHeight(UIIconLabel.m_defaultHeight, UIIconLabel.m_defaultHeightRelativeTo);
		this.SetMargins(UIIconLabel.m_defaultMargins.l, UIIconLabel.m_defaultMargins.r, UIIconLabel.m_defaultMargins.t, UIIconLabel.m_defaultMargins.b, UIIconLabel.m_defaultMarginsRelativeTo);
		this.m_spriteSheet = _spriteSheet;
		this.m_frame = _frame;
		TransformC transformC = TransformS.AddComponent(this.m_TC.p_entity, "IconLabel");
		TransformS.ParentComponent(transformC, this.m_TC);
		transformC.transform.gameObject.layer = this.m_TC.transform.gameObject.layer;
		this.m_sprite = SpriteS.AddComponent(transformC, this.m_frame, this.m_spriteSheet);
		this.SetDrawHandler(new UIDrawDelegate(this.DrawHandler));
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x001B4EAC File Offset: 0x001B32AC
	protected void ArrangeSprite()
	{
		float num = this.m_actualHeight / this.m_sprite.height * 0.618f;
		SpriteS.SetDimensionScale(this.m_sprite, num);
		float num2 = 0f;
		if (this.m_horizontalLabelAlign == Align.Left)
		{
			num2 = (this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r - this.m_sprite.width * num) * -0.5f + (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
		}
		else if (this.m_horizontalLabelAlign == Align.Right)
		{
			num2 = (this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r - this.m_sprite.width * num) * 0.5f + (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
		}
		float num3 = 0f;
		if (this.m_verticalLabelAlign == Align.Bottom)
		{
			num3 = (this.m_actualHeight - this.m_actualMargins.b - this.m_actualMargins.t - this.m_sprite.height * num) * -0.5f + (this.m_actualMargins.b - this.m_actualMargins.t) * 0.5f;
		}
		else if (this.m_verticalLabelAlign == Align.Top)
		{
			num3 = (this.m_actualHeight - this.m_actualMargins.b - this.m_actualMargins.t - this.m_sprite.height * num) * 0.5f + (this.m_actualMargins.b - this.m_actualMargins.t) * 0.5f;
		}
		Vector3 vector;
		vector..ctor(num2, num3, 0f);
		TransformS.SetPosition(this.m_sprite.p_TC, vector);
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x001B5086 File Offset: 0x001B3486
	public override void DrawHandler(UIComponent _c)
	{
		this.ArrangeSprite();
	}

	// Token: 0x04002E73 RID: 11891
	public static float m_defaultWidth = 1f;

	// Token: 0x04002E74 RID: 11892
	public static float m_defaultHeight = 1f;

	// Token: 0x04002E75 RID: 11893
	public static cpBB m_defaultMargins = new cpBB(0.02f);

	// Token: 0x04002E76 RID: 11894
	public static RelativeTo m_defaultWidthRelativeTo = RelativeTo.ParentWidth;

	// Token: 0x04002E77 RID: 11895
	public static RelativeTo m_defaultHeightRelativeTo = RelativeTo.ParentHeight;

	// Token: 0x04002E78 RID: 11896
	public static RelativeTo m_defaultMarginsRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002E79 RID: 11897
	public SpriteSheet m_spriteSheet;

	// Token: 0x04002E7A RID: 11898
	public Frame m_frame;

	// Token: 0x04002E7B RID: 11899
	public SpriteC m_sprite;

	// Token: 0x04002E7C RID: 11900
	public Align m_horizontalLabelAlign;

	// Token: 0x04002E7D RID: 11901
	public Align m_verticalLabelAlign;
}
