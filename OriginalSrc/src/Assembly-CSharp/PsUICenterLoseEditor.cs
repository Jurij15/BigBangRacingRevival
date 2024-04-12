using System;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class PsUICenterLoseEditor : UICanvas
{
	// Token: 0x060014AD RID: 5293 RVA: 0x000D66F0 File Offset: 0x000D4AF0
	public PsUICenterLoseEditor(UIComponent _parent)
		: base(_parent, true, string.Empty, null, string.Empty)
	{
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		UIText uitext = new UIText(this, false, string.Empty, "<color=#d1f0d8>Tap to</color> <i><color=#2EFE9A>retry</color></i>", PsFontManager.GetFont(PsFonts.HurmeBold), 0.08f, RelativeTo.ScreenHeight, null, null);
		uitext.SetVerticalAlign(0.95f);
		TweenC tweenC = TweenS.AddTransformTween(uitext.m_TC, TweenedProperty.Scale, TweenStyle.QuadInOut, new Vector3(1.05f, 1.05f, 1f), 0.6f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.QuadInOut);
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x000D678F File Offset: 0x000D4B8F
	public override void Step()
	{
		if (this.m_hit || Input.GetKeyDown(303))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Restart");
		}
		base.Step();
	}
}
