using System;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class PsUIAttentionArrow : UICanvas
{
	// Token: 0x06001154 RID: 4436 RVA: 0x000A789C File Offset: 0x000A5C9C
	public PsUIAttentionArrow(UIComponent _parent, bool _onRight = true)
		: base(_parent, false, "AttentionArrow", null, string.Empty)
	{
		this.SetSize(0.1f, 0.1f, RelativeTo.ScreenShortest);
		this.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_attention_arrow", null);
		if (!_onRight)
		{
			frame.flipX = true;
		}
		this.m_sprite = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		this.m_sprite.m_TC.transform.position += new Vector3(0f, 0f, -10f);
		TweenC tweenC = TweenS.AddTransformTween(this.m_sprite.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_sprite.m_TC.transform.localPosition, this.m_sprite.m_TC.transform.localPosition - new Vector3(30f * ((!_onRight) ? (-1f) : 1f), 0f, 0f), 0.5f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x000A79C4 File Offset: 0x000A5DC4
	public override void UpdateSpecial()
	{
		this.UpdateAlign();
		base.UpdateSpecial();
	}

	// Token: 0x04001446 RID: 5190
	private UIFittedSprite m_sprite;
}
