using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class MotorcycleController : Controller, IPowerUpController
{
	// Token: 0x06000346 RID: 838 RVA: 0x00030D38 File Offset: 0x0002F138
	public MotorcycleController(Camera _overlayCamera = null)
	{
		this.m_overlayCamera = _overlayCamera;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00030D47 File Offset: 0x0002F147
	public PsUIPowerUpButton GetPowerUp()
	{
		return this.m_powerup;
	}

	// Token: 0x06000348 RID: 840 RVA: 0x00030D50 File Offset: 0x0002F150
	public override void Open()
	{
		if (this.m_open)
		{
			this.Close();
		}
		base.Open();
		this.m_open = true;
		this.m_leftArea = new UIHorizontalList(null, "buttonArea");
		this.m_leftArea.SetAlign(0f, 0f);
		this.m_leftArea.SetMargins(0.04f, 0f, 0f, 0.04f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetSpacing(0.01f, RelativeTo.ScreenShortest);
		this.m_leftArea.RemoveDrawHandler();
		UIRectSpriteSensor uirectSpriteSensor = new UIRectSpriteSensor(this.m_leftArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_arrow_tilt_left", null), false);
		uirectSpriteSensor.SetHeight(0.125f, RelativeTo.ScreenLongest);
		UIRectSpriteSensor uirectSpriteSensor2 = new UIRectSpriteSensor(this.m_leftArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_arrow_tilt_right", null), false);
		uirectSpriteSensor2.SetHeight(0.125f, RelativeTo.ScreenLongest);
		base.AddButton("LeftButton1", uirectSpriteSensor, ControllerButtonType.BUTTON, 276);
		base.AddButton("LeftButton2", uirectSpriteSensor2, ControllerButtonType.BUTTON, 275);
		if (!PlayerPrefsX.GetOffroadRacing() && MotorcycleController.m_startCreate && !Booster.IsBoosterAllowed())
		{
			this.m_leanTutorial = new UICanvas(this.m_leftArea, false, "leanTut", null, string.Empty);
			this.m_leanTutorial.SetRogue();
			this.m_leanTutorial.SetSize(0.125f, 0.125f, RelativeTo.ScreenLongest);
			this.m_leanTutorial.SetAlign(0.5f, 1f);
			this.m_leanTutorial.SetMargins(0f, 0f, -0.13f, 0.13f, RelativeTo.ScreenLongest);
			this.m_leanTutorial.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_leanTutorial, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_tooltip_bg", null), true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_leanTutorial, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_tooltip_lean", null), true, true);
			uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
			uifittedSprite2.SetDepthOffset(-5f);
			UIText uitext = new UIText(this.m_leanTutorial, false, string.Empty, PsStrings.Get(StringID.TUTORIAL_LEAN), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0185f, RelativeTo.ScreenLongest, "#E4F3AE", null);
			uitext.SetAlign(0.5f, 0f);
		}
		this.m_leftArea.Update();
		Controller.GetButton("LeftButton1").Update();
		Controller.GetButton("LeftButton2").Update();
		if (this.m_leanTutorial != null)
		{
			TweenC tweenC = TweenS.AddTransformTween(this.m_leanTutorial.m_childs[1].m_TC, TweenedProperty.Rotation, TweenStyle.QuadInOut, Vector3.forward * -8f, Vector3.forward * 8f, 0.6f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.QuadInOut);
		}
		if (Booster.IsBoosterAllowed())
		{
			if (PsState.m_activeGameLoop is PsGameLoopTournament)
			{
				this.m_boostButton = new PsUIBoosterButtonTournament(null);
			}
			else
			{
				this.m_boostButton = new PsUIBoosterPowerUpButton(null);
			}
			this.m_boostButton.ShowRefillButton();
			this.m_boostButton.SetAlign((float)Screen.height / (float)Screen.width * 0.04f, this.m_leftArea.m_height * 1.3f);
			this.m_boostButton.Update();
			base.AddButton("BoostButton", this.m_boostButton.m_button, ControllerButtonType.BUTTON, 32);
		}
		if (this.m_boostButton != null && this.m_boostButton.IsUnavailable())
		{
			this.m_boostButton.GreyScaleOn();
		}
		this.m_rightArea = new UIHorizontalList(null, "buttonArea");
		this.m_rightArea.SetAlign(1f, 0f);
		this.m_rightArea.SetMargins(0f, 0.04f, 0f, 0.04f, RelativeTo.ScreenShortest);
		this.m_rightArea.SetSpacing(0.01f, RelativeTo.ScreenShortest);
		this.m_rightArea.RemoveDrawHandler();
		UIRectSpriteSensor uirectSpriteSensor3 = new UIRectSpriteSensor(this.m_rightArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_arrow_left", null), false);
		uirectSpriteSensor3.SetHeight(0.125f, RelativeTo.ScreenLongest);
		this.m_rightButton = new UIRectSpriteSensor(this.m_rightArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_arrow_right", null), false);
		this.m_rightButton.SetHeight(0.125f, RelativeTo.ScreenLongest);
		base.AddButton("Reverse", uirectSpriteSensor3, ControllerButtonType.BUTTON, 274);
		base.AddButton("Throttle", this.m_rightButton, ControllerButtonType.BUTTON, 273);
		if (!PlayerPrefsX.GetOffroadRacing() && MotorcycleController.m_startCreate && !Booster.IsBoosterAllowed())
		{
			this.m_driveTutorial = new UICanvas(this.m_rightArea, false, "leanTut", null, string.Empty);
			this.m_driveTutorial.SetRogue();
			this.m_driveTutorial.SetSize(0.125f, 0.125f, RelativeTo.ScreenLongest);
			this.m_driveTutorial.SetAlign(0.5f, 1f);
			this.m_driveTutorial.SetMargins(0f, 0f, -0.13f, 0.13f, RelativeTo.ScreenLongest);
			this.m_driveTutorial.RemoveDrawHandler();
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(this.m_driveTutorial, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_tooltip_bg", null), true, true);
			uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
			UIFittedSprite uifittedSprite4 = new UIFittedSprite(this.m_driveTutorial, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_tooltip_move", null), true, true);
			uifittedSprite4.SetHeight(1f, RelativeTo.ParentHeight);
			uifittedSprite4.SetDepthOffset(-5f);
			UIText uitext2 = new UIText(this.m_driveTutorial, false, string.Empty, PsStrings.Get(StringID.TUTORIAL_MOVE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0185f, RelativeTo.ScreenLongest, "#E4F3AE", null);
			uitext2.SetAlign(0.5f, 0f);
		}
		this.m_rightArea.Update();
		if (this.m_driveTutorial != null)
		{
			TweenC tweenC2 = TweenS.AddTransformTween(this.m_driveTutorial.m_childs[1].m_TC, TweenedProperty.Position, TweenStyle.QuadInOut, this.m_driveTutorial.m_childs[1].m_TC.transform.localPosition - Vector3.right * 0.015f * (float)Screen.width, this.m_driveTutorial.m_childs[1].m_TC.transform.localPosition + Vector3.right * 0.015f * (float)Screen.width, 0.6f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC2, -1, true, TweenStyle.QuadInOut);
		}
		if (this.m_overlayCamera != null)
		{
			this.m_leftArea.SetCamera(this.m_overlayCamera, true, false);
			this.m_rightArea.SetCamera(this.m_overlayCamera, true, false);
			if (this.m_boostButton != null)
			{
				this.m_boostButton.SetCamera(this.m_overlayCamera, true, false);
			}
		}
		if (MotorcycleController.m_startCreate && PsState.m_activeMinigame.m_gameStarted && !Booster.IsBoosterAllowed())
		{
			this.FadeTutorials();
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x000314BA File Offset: 0x0002F8BA
	public void FadeTutorials()
	{
		if (this.m_leanTutorial != null)
		{
			TimerS.AddComponent(this.m_leftArea.m_TC.p_entity, string.Empty, 6f, 0f, false, delegate(TimerC c)
			{
				TimerS.RemoveComponent(c);
				this.StartFade();
			});
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000314FC File Offset: 0x0002F8FC
	private void StartFade()
	{
		if (this.m_leanTutorial != null)
		{
			for (int i = 0; i < this.m_leanTutorial.m_childs.Count; i++)
			{
				TweenC tweenC = TweenS.AddTransformTween(this.m_leanTutorial.m_childs[i].m_TC, TweenedProperty.Alpha, TweenStyle.CubicOut, Vector3.one, Vector3.zero, 0.25f, 0f, true);
				if (i == this.m_leanTutorial.m_childs.Count - 1)
				{
					TweenS.SetTweenAlphaProperties(tweenC, false, false, true, Shader.Find("Framework/FontShader"));
				}
				else
				{
					TweenS.SetTweenAlphaProperties(tweenC, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
				}
			}
			for (int j = 0; j < this.m_driveTutorial.m_childs.Count; j++)
			{
				TweenC tweenC2 = TweenS.AddTransformTween(this.m_driveTutorial.m_childs[j].m_TC, TweenedProperty.Alpha, TweenStyle.CubicOut, Vector3.one, Vector3.zero, 0.5f, 0f, true);
				if (j == this.m_driveTutorial.m_childs.Count - 1)
				{
					TweenS.SetTweenAlphaProperties(tweenC2, false, false, true, Shader.Find("Framework/FontShader"));
				}
				else
				{
					TweenS.SetTweenAlphaProperties(tweenC2, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
				}
			}
		}
		MotorcycleController.m_startCreate = false;
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00031648 File Offset: 0x0002FA48
	public override void Close()
	{
		base.Close();
		if (this.m_open)
		{
			this.m_open = false;
			this.m_leftArea.Destroy();
			this.m_rightArea.Destroy();
			this.m_leanTutorial = null;
			this.m_driveTutorial = null;
			if (this.m_boostButton != null)
			{
				this.m_boostButton.Destroy();
			}
			if (this.m_powerup != null)
			{
				this.m_powerup.Destroy();
			}
			if (this.ghostpowerups != null)
			{
				this.ghostpowerups.Destroy();
			}
			if (this.powerups != null)
			{
				this.powerups.Destroy();
			}
			Controller.RemoveAllButtons();
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x000316F0 File Offset: 0x0002FAF0
	public override void EnableController()
	{
		foreach (UIComponent uicomponent in this.m_leftArea.m_childs)
		{
			UIRectSpriteSensor uirectSpriteSensor = (UIRectSpriteSensor)uicomponent;
			uirectSpriteSensor.EnableTouchAreas(true);
			uirectSpriteSensor.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Framework/VertexColorUnlitDouble");
		}
		foreach (UIComponent uicomponent2 in this.m_rightArea.m_childs)
		{
			UIRectSpriteSensor uirectSpriteSensor2 = (UIRectSpriteSensor)uicomponent2;
			uirectSpriteSensor2.EnableTouchAreas(true);
			uirectSpriteSensor2.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Framework/VertexColorUnlitDouble");
		}
		if (this.m_boostButton != null && PsMetagameManager.m_playerStats.boosters > 0)
		{
			this.m_boostButton.EnableTouchAreas(true);
			this.m_boostButton.GreyScaleOff();
		}
		base.EnableController();
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0003182C File Offset: 0x0002FC2C
	public override void DisableController()
	{
		foreach (UIComponent uicomponent in this.m_leftArea.m_childs)
		{
			UIRectSpriteSensor uirectSpriteSensor = (UIRectSpriteSensor)uicomponent;
			uirectSpriteSensor.DisableTouchAreas(true);
			uirectSpriteSensor.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find("WOE/Fx/GreyscaleUnlitAlpha");
		}
		foreach (UIComponent uicomponent2 in this.m_rightArea.m_childs)
		{
			UIRectSpriteSensor uirectSpriteSensor2 = (UIRectSpriteSensor)uicomponent2;
			uirectSpriteSensor2.DisableTouchAreas(true);
			uirectSpriteSensor2.m_rect.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find("WOE/Fx/GreyscaleUnlitAlpha");
		}
		if (this.m_boostButton != null)
		{
			this.m_boostButton.DisableTouchAreas(true);
			this.m_boostButton.GreyScaleOn();
		}
		base.DisableController();
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00031958 File Offset: 0x0002FD58
	public override void DisabletouchAreas()
	{
		base.DisabletouchAreas();
		foreach (UIComponent uicomponent in this.m_leftArea.m_childs)
		{
			UIRectSpriteSensor uirectSpriteSensor = (UIRectSpriteSensor)uicomponent;
			uirectSpriteSensor.DisableTouchAreas(true);
		}
		foreach (UIComponent uicomponent2 in this.m_rightArea.m_childs)
		{
			UIRectSpriteSensor uirectSpriteSensor2 = (UIRectSpriteSensor)uicomponent2;
			uirectSpriteSensor2.DisableTouchAreas(true);
		}
		if (this.m_boostButton != null)
		{
			this.m_boostButton.DisableTouchAreas(true);
		}
	}

	// Token: 0x04000436 RID: 1078
	private UIHorizontalList m_leftArea;

	// Token: 0x04000437 RID: 1079
	private UIHorizontalList m_rightArea;

	// Token: 0x04000438 RID: 1080
	public UIRectSpriteSensor m_rightButton;

	// Token: 0x04000439 RID: 1081
	public PsUIPowerUpButton m_powerup;

	// Token: 0x0400043A RID: 1082
	private UICanvas m_leanTutorial;

	// Token: 0x0400043B RID: 1083
	private UICanvas m_driveTutorial;

	// Token: 0x0400043C RID: 1084
	private UICanvas m_boostTutorial;

	// Token: 0x0400043D RID: 1085
	private Camera m_overlayCamera;

	// Token: 0x0400043E RID: 1086
	public static bool m_startCreate;

	// Token: 0x0400043F RID: 1087
	private UIHorizontalList powerups;

	// Token: 0x04000440 RID: 1088
	private UIHorizontalList ghostpowerups;
}
