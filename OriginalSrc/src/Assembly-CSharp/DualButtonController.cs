using System;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class DualButtonController : Controller
{
	// Token: 0x06000340 RID: 832 RVA: 0x000306FE File Offset: 0x0002EAFE
	public DualButtonController(Camera _overlayCamera = null)
	{
		this.m_overlayCamera = _overlayCamera;
	}

	// Token: 0x06000341 RID: 833 RVA: 0x00030710 File Offset: 0x0002EB10
	public override void Open()
	{
		if (this.m_open)
		{
			this.Close();
		}
		this.m_open = true;
		this.m_leftArea = new UIHorizontalList(null, "buttonArea");
		this.m_leftArea.SetAlign(0f, 0f);
		this.m_leftArea.SetMargins(0.04f, 0f, 0f, 0.04f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetSpacing(0.02f, RelativeTo.ScreenShortest);
		this.m_leftArea.RemoveDrawHandler();
		UIRectSpriteSensor uirectSpriteSensor = new UIRectSpriteSensor(this.m_leftArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_arrow_left", null), false);
		uirectSpriteSensor.SetHeight(0.125f, RelativeTo.ScreenLongest);
		base.AddButton("Reverse", uirectSpriteSensor, ControllerButtonType.BUTTON, 276);
		this.m_leftArea.Update();
		if (Booster.IsBoosterAllowed())
		{
			this.m_boostButton = new PsUIBoosterButton(null, false);
			this.m_boostButton.SetAlign((float)Screen.height / (float)Screen.width * 0.04f, this.m_leftArea.m_height * 1.3f);
			this.m_boostButton.Update();
			base.AddButton("BoostButton", this.m_boostButton.m_button, ControllerButtonType.BUTTON, 32);
		}
		if (this.m_boostButton != null && PsMetagameManager.m_playerStats.boosters <= 0)
		{
			this.m_boostButton.GreyScaleOn();
		}
		this.m_rightArea = new UIHorizontalList(null, "buttonArea");
		this.m_rightArea.SetAlign(1f, 0f);
		this.m_rightArea.SetMargins(0f, 0.04f, 0f, 0.04f, RelativeTo.ScreenShortest);
		this.m_rightArea.SetSpacing(0.02f, RelativeTo.ScreenShortest);
		this.m_rightArea.RemoveDrawHandler();
		this.m_rightButton = new UIRectSpriteSensor(this.m_rightArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_arrow_right", null), false);
		this.m_rightButton.SetHeight(0.125f, RelativeTo.ScreenLongest);
		base.AddButton("Throttle", this.m_rightButton, ControllerButtonType.BUTTON, 275);
		this.m_rightArea.Update();
		if (this.m_overlayCamera != null)
		{
			this.m_leftArea.SetCamera(this.m_overlayCamera, true, false);
			this.m_rightArea.SetCamera(this.m_overlayCamera, true, false);
			if (this.m_boostButton != null)
			{
				this.m_boostButton.SetCamera(this.m_overlayCamera, true, false);
			}
		}
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00030998 File Offset: 0x0002ED98
	public override void Close()
	{
		base.Close();
		if (this.m_open)
		{
			this.m_open = false;
			this.m_leftArea.Destroy();
			this.m_rightArea.Destroy();
			if (this.m_boostButton != null)
			{
				this.m_boostButton.Destroy();
				this.m_boostButton = null;
			}
			Controller.RemoveAllButtons();
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x000309F8 File Offset: 0x0002EDF8
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

	// Token: 0x06000344 RID: 836 RVA: 0x00030B34 File Offset: 0x0002EF34
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

	// Token: 0x06000345 RID: 837 RVA: 0x00030C60 File Offset: 0x0002F060
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

	// Token: 0x04000432 RID: 1074
	private UIHorizontalList m_leftArea;

	// Token: 0x04000433 RID: 1075
	private UIHorizontalList m_rightArea;

	// Token: 0x04000434 RID: 1076
	public UIRectSpriteSensor m_rightButton;

	// Token: 0x04000435 RID: 1077
	private Camera m_overlayCamera;
}
