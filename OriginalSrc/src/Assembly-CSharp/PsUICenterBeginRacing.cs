using System;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class PsUICenterBeginRacing : UICanvas
{
	// Token: 0x06001500 RID: 5376 RVA: 0x000DB548 File Offset: 0x000D9948
	public PsUICenterBeginRacing(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.RemoveDrawHandler();
		this.CreateCountDownNumber(3, false);
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x000DB56C File Offset: 0x000D996C
	protected virtual void SetHeatNumber(UIComponent _parent)
	{
		int heatNumber = (PsState.m_activeGameLoop as PsGameLoopRacing).m_heatNumber;
		if (heatNumber < 6 && (PsState.m_activeGameLoop as PsGameLoopRacing).m_secondarysWon < 4)
		{
			UIText uitext = new UIText(_parent, false, string.Empty, PsStrings.Get(StringID.HEAT).ToUpper() + " " + heatNumber, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ScreenHeight, this.GetHeatTextColor(heatNumber), "000000");
			uitext.SetShadowShift(new Vector2(0.5f, 0.25f), 0.1f);
			TransformS.SetRotation(uitext.m_TC, new Vector3(0f, 0f, 3f));
			uitext.SetAlign(0.5f, 0.75f);
		}
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000DB630 File Offset: 0x000D9A30
	public void CreateCountDownNumber(int _number, bool _update = true)
	{
		string text = "hud_countdown_" + _number;
		UIFittedSprite uifittedSprite = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		uifittedSprite.SetHeight(0.2f, RelativeTo.ScreenHeight);
		uifittedSprite.SetAlign(0.5f, 0.75f);
		float num = 0.8f;
		TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 0.925f, num * 0.1f, 0f, true);
		TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Scale, TweenStyle.CubicIn, Vector3.one * 0.925f, Vector3.one * 1.5f, num * 0.9f, num * 0.1f, true);
		TweenC tweenC = TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.one, Vector3.zero, num, 0f, true);
		TweenS.SetTweenAlphaProperties(tweenC, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		if (_update)
		{
			uifittedSprite.Update();
		}
		SoundS.PlaySingleShotWithParameter("/InGame/Events/RaceCountdown", Vector3.zero, "Counter", (float)_number, 1f);
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x000DB753 File Offset: 0x000D9B53
	private string GetHeatTextColor(int _heat)
	{
		return "9df465";
	}
}
