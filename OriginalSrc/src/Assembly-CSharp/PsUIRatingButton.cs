using System;
using UnityEngine;

// Token: 0x020003B3 RID: 947
public class PsUIRatingButton : PsUIGenericButton
{
	// Token: 0x06001B21 RID: 6945 RVA: 0x0012FC33 File Offset: 0x0012E033
	public PsUIRatingButton(UIComponent _parent, string _spriteFrame, float _fillerwidth, float _fillerheight)
		: base(_parent, 0.25f, 0.25f, 0.01f, "Button")
	{
		this.Constructor(_spriteFrame, _fillerwidth, _fillerheight);
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x0012FC5C File Offset: 0x0012E05C
	protected virtual void Constructor(string _spriteFrame, float _fillerwidth, float _fillerheight)
	{
		base.SetSound(null);
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(_fillerwidth, _fillerheight, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0f, 0f, -0.0225f, 0.0225f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.0075f, RelativeTo.ScreenHeight);
		uiverticalList.SetRogue();
		uiverticalList.SetDepthOffset(-10f);
		uiverticalList.RemoveDrawHandler();
		this.m_thumbSprite = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_spriteFrame, null), true, true);
		this.m_thumbSprite.SetHeight(0.175f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x0012FD18 File Offset: 0x0012E118
	protected void ButtonPressedAnimation(bool _thumbsUp)
	{
		Vector3 localPosition = this.m_thumbSprite.m_TC.transform.localPosition;
		Vector3 vector = localPosition + new Vector3(0f, (!_thumbsUp) ? (-20f) : 20f, 0f);
		TweenS.AddTransformTween(this.m_thumbSprite.m_TC, TweenedProperty.Position, TweenStyle.BounceOut, vector, 0.5f, 0f, true);
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x0012FD86 File Offset: 0x0012E186
	public virtual void ButtonPressed()
	{
	}

	// Token: 0x04001D9D RID: 7581
	protected UIFittedSprite m_thumbSprite;
}
