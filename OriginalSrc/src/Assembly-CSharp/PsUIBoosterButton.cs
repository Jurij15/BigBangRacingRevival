using System;
using UnityEngine;

// Token: 0x02000218 RID: 536
public class PsUIBoosterButton : UICanvas
{
	// Token: 0x06000F7B RID: 3963 RVA: 0x00091930 File Offset: 0x0008FD30
	public PsUIBoosterButton(UIComponent _parent, bool _touchable = false)
		: base(_parent, _touchable, "Booster Button Container", null, string.Empty)
	{
		int boosters = PsMetagameManager.m_playerStats.boosters;
		this.m_maxBoosterCount = PsMetagameManager.m_playerStats.maxBoosters;
		this.SetSize(0.18f, 0.18f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		string text = "controller_boost_button";
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(text, null);
		this.m_button = new UISprite(this, true, "BoosterButton", PsState.m_uiSheet, frame, true);
		this.m_button.SetWidth(0.9f, RelativeTo.ParentWidth);
		this.m_button.SetHeight(frame.height / frame.width, RelativeTo.OwnWidth);
		this.m_button.SetAlign(1f, 0f);
		this.m_button.m_TAC.m_cancelOtherTouches = false;
		this.SetBoosterIcon(boosters, false);
		this.CreateBooster();
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x00091A27 File Offset: 0x0008FE27
	public virtual bool IsUnavailable()
	{
		return PsMetagameManager.m_playerStats.boosters < 1 || PsState.m_activeGameLoop.m_boosterUsed;
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x00091A48 File Offset: 0x0008FE48
	protected virtual void CreateBoosterCount(bool _update = false)
	{
		if (PsMetagameManager.IsTimedGiftActive(EventGiftTimedType.unlimitedNitros))
		{
			return;
		}
		if (this.m_boosterCount != null)
		{
			this.m_boosterCount.Destroy();
		}
		this.m_boosterCount = new UIText(this.m_button, false, "BoosterCount", string.Concat(new object[]
		{
			string.Empty,
			PsMetagameManager.m_playerStats.boosters,
			"/",
			this.m_maxBoosterCount
		}), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.275f, RelativeTo.ParentWidth, null, "#000000");
		this.m_boosterCount.SetVerticalAlign(0.25f);
		if (_update)
		{
			this.m_boosterCount.Update();
		}
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00091AFC File Offset: 0x0008FEFC
	protected void DestroyBoosterCount()
	{
		if (this.m_boosterCount != null)
		{
			this.m_boosterCount.Destroy();
			this.m_boosterCount = null;
		}
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x00091B1B File Offset: 0x0008FF1B
	protected virtual string GetIcon()
	{
		return "hud_boost_boost";
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x00091B24 File Offset: 0x0008FF24
	protected void SetBoosterIcon(int _count, bool _update = false)
	{
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(this.GetIcon(), null);
		if (this.m_icon != null)
		{
			this.m_icon.Destroy();
		}
		this.m_icon = new UISprite(this.m_button, false, "BoosterButtonIcon", PsState.m_uiSheet, frame, true);
		this.m_icon.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_icon.SetHeight(frame.height / frame.width, RelativeTo.OwnWidth);
		this.m_icon.SetDepthOffset(-2f);
		if (_count > 0)
		{
			this.CreateBoosterCount(_update);
		}
		if (_update)
		{
			this.m_icon.Update();
		}
		this.SetTween();
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x00091BDB File Offset: 0x0008FFDB
	protected virtual void SetTween()
	{
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x00091BDD File Offset: 0x0008FFDD
	public void ShowPowerUp()
	{
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x00091BDF File Offset: 0x0008FFDF
	protected virtual bool ShowRefillButtonNeeded()
	{
		return PsMetagameManager.m_playerStats.boosters < 1 && (!PsState.m_activeMinigame.m_gameStarted || PsState.m_activeMinigame.m_gameEnded) && !PsMetagameManager.IsTimedGiftActive(EventGiftTimedType.unlimitedNitros);
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x00091C1C File Offset: 0x0009001C
	public virtual void ShowRefillButton()
	{
		if (this.ShowRefillButtonNeeded())
		{
			this.m_refillButton = new PsUIGenericButton(this.m_button, 0.25f, 0.25f, 0.005f, "Button");
			string text = PsStrings.Get(StringID.NITRO_BUTTON_FILL_UP).Replace(" ", "\n");
			this.m_refillButton.SetText(text.ToUpper(), 0.028f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
			this.m_refillButton.SetDepthOffset(-5f);
			this.m_refillButton.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		}
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x00091CC4 File Offset: 0x000900C4
	public void RemoveRefillButton()
	{
		if (this.m_refillButton != null)
		{
			this.m_refillButton.Destroy();
			this.m_refillButton = null;
			if (PsMetagameManager.m_menuResourceView != null && PsMetagameManager.m_menuResourceView.m_showDiamonds)
			{
				LastResourceView lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
				lastResourceView.diamonds = false;
				PsMetagameManager.m_menuResourceView.ShowLastView(lastResourceView);
			}
		}
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x00091D24 File Offset: 0x00090124
	public virtual void CreateBooster()
	{
		Vehicle vehicle = PsState.m_activeMinigame.m_playerUnit as Vehicle;
		if (vehicle != null)
		{
			if (vehicle.m_booster == null)
			{
				bool flag = PsState.m_activeGameLoop.m_freeConsumableUnlock == "Booster";
				this.CreateBooster(vehicle, flag);
			}
			vehicle.m_booster.SetBoosterButton(this);
		}
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00091D7B File Offset: 0x0009017B
	public virtual void CreateBooster(Vehicle _vehicle, bool _freeBooster)
	{
		_vehicle.m_booster = new SpeedBooster(_freeBooster);
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x00091D89 File Offset: 0x00090189
	protected void SetResourceView()
	{
		if (this.m_lastResourceView != null)
		{
			PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastResourceView);
			this.m_lastResourceView = null;
		}
		else
		{
			PsMetagameManager.HideResources();
		}
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x00091DB8 File Offset: 0x000901B8
	public virtual void RefillButtonHit()
	{
		CameraS.CreateBlur(null);
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUINitroFillUpPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
		popup.SetAction("Purchased", delegate
		{
			this.RemoveRefillButton();
			this.CreateBoosterCount(false);
			this.Update();
			this.GreyScaleOff();
		});
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			CameraS.RemoveBlur();
			this.SetResourceView();
		});
		if (PsMetagameManager.m_menuResourceView != null)
		{
			this.m_lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
		}
		TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		PsMetagameManager.ShowResources(popup.m_mainContent.m_camera, true, false, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x00091EA8 File Offset: 0x000902A8
	public override void Step()
	{
		if (this.m_refillButton != null)
		{
			if (PsState.m_activeMinigame.m_gameStarted && !PsState.m_activeMinigame.m_gameEnded)
			{
				this.RemoveRefillButton();
			}
			else if (this.m_refillButton.m_hit)
			{
				this.RefillButtonHit();
			}
		}
		base.Step();
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x00091F08 File Offset: 0x00090308
	public virtual void GreyScaleOn()
	{
		if (this.m_button != null && this.m_button.m_rect != null)
		{
			this.m_button.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find(this.m_greyScaleShaderName);
		}
		if (this.m_icon != null && this.m_icon.m_rect != null)
		{
			this.m_icon.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find(this.m_greyScaleShaderName);
		}
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x00091FA0 File Offset: 0x000903A0
	public virtual void GreyScaleOff()
	{
		if (this.m_button != null && this.m_button.m_rect != null)
		{
			this.m_button.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find(this.m_defaultShaderName);
		}
		if (this.m_icon != null && this.m_icon.m_rect != null)
		{
			this.m_icon.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find(this.m_defaultShaderName);
		}
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x00092037 File Offset: 0x00090437
	public void UseBoost()
	{
		this.GreyScaleOn();
		this.RemoveBoost();
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x00092045 File Offset: 0x00090445
	public void RemoveBoost()
	{
		this.CreateBoosterCount(true);
		this.m_tweenedOff = true;
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x00092055 File Offset: 0x00090455
	public virtual void AddBoost()
	{
		this.m_tweenedOff = false;
	}

	// Token: 0x04001239 RID: 4665
	private UIFittedSprite m_boostIcon;

	// Token: 0x0400123A RID: 4666
	public UISprite m_button;

	// Token: 0x0400123B RID: 4667
	protected UISprite m_icon;

	// Token: 0x0400123C RID: 4668
	protected UIText m_boosterCount;

	// Token: 0x0400123D RID: 4669
	private UIText m_keyTimer;

	// Token: 0x0400123E RID: 4670
	private UISprite m_countBackground;

	// Token: 0x0400123F RID: 4671
	private PsUIGenericButton m_refillButton;

	// Token: 0x04001240 RID: 4672
	private bool m_createTutorialArrow;

	// Token: 0x04001241 RID: 4673
	private PsUITutorialArrowUI m_tutorialArrow;

	// Token: 0x04001242 RID: 4674
	private string m_defaultShaderName = "Framework/VertexColorUnlitDouble";

	// Token: 0x04001243 RID: 4675
	private string m_greyScaleShaderName = "WOE/Fx/GreyscaleUnlitAlpha";

	// Token: 0x04001244 RID: 4676
	private int m_maxBoosterCount;

	// Token: 0x04001245 RID: 4677
	private PsUIGenericButton m_fillUpButton;

	// Token: 0x04001246 RID: 4678
	private TweenC m_attentionTween;

	// Token: 0x04001247 RID: 4679
	protected LastResourceView m_lastResourceView;

	// Token: 0x04001248 RID: 4680
	protected bool m_tweenedOff;
}
