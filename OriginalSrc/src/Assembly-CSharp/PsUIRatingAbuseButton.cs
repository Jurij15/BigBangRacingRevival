using System;
using UnityEngine;

// Token: 0x020003B8 RID: 952
public class PsUIRatingAbuseButton : PsUIRatingButton
{
	// Token: 0x06001B2B RID: 6955 RVA: 0x0012FDCE File Offset: 0x0012E1CE
	public PsUIRatingAbuseButton(UIComponent _parent, float _fillerwidth, float _fillerheight, string _spriteFrame = null)
		: base(_parent, _spriteFrame, _fillerwidth, _fillerheight)
	{
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x0012FDDB File Offset: 0x0012E1DB
	protected override void Constructor(string _spriteFrame, float _fillerwidth, float _fillerheight)
	{
		base.SetSound(null);
		base.SetRedColors();
		base.SetFittedText(PsStrings.Get(StringID.BUTTON_REPORT), 0.035f, _fillerwidth, RelativeTo.ScreenHeight, true);
		this.SetHeight(0.085f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x0012FE0E File Offset: 0x0012E20E
	public override void ButtonPressed()
	{
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BounceOut, Vector3.one, Vector3.one * 1.2f, 0.5f, 0f, true);
	}
}
