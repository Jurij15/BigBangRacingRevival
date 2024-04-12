using System;
using UnityEngine;

// Token: 0x020003B9 RID: 953
public class PsUISuperLikeButton : PsUIRatingButton
{
	// Token: 0x06001B2E RID: 6958 RVA: 0x0012FE3E File Offset: 0x0012E23E
	public PsUISuperLikeButton(UIComponent _parent, float _fillerwidth, float _fillerheight)
		: base(_parent, "menu_thumbs_mega", _fillerwidth, _fillerheight)
	{
		base.SetGreenColors(true);
		if (PsUISuperLikeButton.GetTimeLeft() > 0)
		{
			this.Disable();
		}
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x0012FE7C File Offset: 0x0012E27C
	public override void ButtonPressed()
	{
		base.ButtonPressedAnimation(true);
		PsMetagameManager.m_playerStats.SuperLikeSet();
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x0012FE90 File Offset: 0x0012E290
	public void Disable()
	{
		this.GreyScaleOn();
		this.DisableTouchAreas(true);
		this.m_thumbSprite.SetOverrideShader(Shader.Find(this.m_greyScaleShaderName));
		Color color;
		color..ctor(0.14f, 0.23f, 0.09f);
		this.m_thumbSprite.m_overrideMaterial.SetColor("_TintColor", color);
		this.ShowTimer();
		this.m_timerDesc = new UIVerticalList(this, string.Empty);
		this.m_timerDesc.SetVerticalAlign(0f);
		this.m_timerDesc.SetRogue();
		this.m_timerDesc.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.AVAILABLE_IN);
		UIText uitext = new UIText(this.m_timerDesc, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), 0.0175f, RelativeTo.ScreenHeight, "#ffffff", null);
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x0012FF58 File Offset: 0x0012E358
	public void Enable()
	{
		if (this.m_timerBackground != null)
		{
			this.GreyScaleOff();
			this.EnableTouchAreas(true);
			this.m_timerBackground.Destroy();
			this.m_timerDesc.Destroy();
			this.m_timerBackground = null;
			this.m_timer = null;
			this.Update();
		}
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x0012FFA8 File Offset: 0x0012E3A8
	public void ShowTimer()
	{
		int timeLeft = PsUISuperLikeButton.GetTimeLeft();
		if (timeLeft > 0)
		{
			float num = 0.0325f;
			UIComponent uicomponent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
			uicomponent.SetSize(0f, 0f, RelativeTo.ScreenHeight);
			uicomponent.SetVerticalAlign(0f);
			uicomponent.SetRogue();
			uicomponent.SetMargins(0f, 0f, num, -num, RelativeTo.ScreenShortest);
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_timer_background", null);
			this.m_timerBackground = new UISprite(uicomponent, false, "SuperLikeTimerBackground", PsState.m_uiSheet, frame, true);
			this.m_timerBackground.SetRogue();
			this.m_timerBackground.SetWidth(0.125f, RelativeTo.ScreenShortest);
			this.m_timerBackground.SetHeight(num, RelativeTo.ScreenShortest);
			this.m_timerBackground.SetDepthOffset(-15f);
			this.m_timerBackground.SetVerticalAlign(0f);
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(PsUISuperLikeButton.GetTimeLeft());
			this.m_timer = new UIText(this.m_timerBackground, false, string.Empty, timeStringFromSeconds, PsFontManager.GetFont(PsFonts.HurmeBold), 0.026f, RelativeTo.ScreenShortest, "#ffffff", null);
			this.m_timer.SetVerticalAlign(0f);
		}
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x001300D4 File Offset: 0x0012E4D4
	public static int GetTimeLeft()
	{
		return PsMetagameManager.m_playerStats.SuperLikeSecondsLeft();
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x001300F0 File Offset: 0x0012E4F0
	public override void Step()
	{
		if (PsUISuperLikeButton.GetTimeLeft() <= 0 && this.m_timer != null)
		{
			this.Enable();
		}
		else if (this.m_timer != null && Main.m_gameTicks % 30 == 0)
		{
			this.m_timer.SetText(PsMetagameManager.GetTimeStringFromSeconds(PsUISuperLikeButton.GetTimeLeft()));
		}
		base.Step();
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x00130151 File Offset: 0x0012E551
	public virtual void GreyScaleOn()
	{
		base.SetGrayColors();
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x00130159 File Offset: 0x0012E559
	public virtual void GreyScaleOff()
	{
		this.m_thumbSprite.SetOverrideShader(Shader.Find(this.m_defaultShaderName));
		base.SetGreenColors(true);
	}

	// Token: 0x04001D9E RID: 7582
	private UIText m_timer;

	// Token: 0x04001D9F RID: 7583
	private UISprite m_timerBackground;

	// Token: 0x04001DA0 RID: 7584
	private UIVerticalList m_timerDesc;

	// Token: 0x04001DA1 RID: 7585
	private string m_defaultShaderName = "Framework/VertexColorUnlitDouble";

	// Token: 0x04001DA2 RID: 7586
	private string m_greyScaleShaderName = "WOE/Fx/GreyscaleUnlitAlpha";
}
