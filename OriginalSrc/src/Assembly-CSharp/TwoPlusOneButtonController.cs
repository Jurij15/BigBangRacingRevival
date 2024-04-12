using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class TwoPlusOneButtonController : Controller
{
	// Token: 0x06000350 RID: 848 RVA: 0x00031A3E File Offset: 0x0002FE3E
	public TwoPlusOneButtonController(Camera _overlayCamera = null)
	{
		this.m_overlayCamera = _overlayCamera;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00031A50 File Offset: 0x0002FE50
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
		this.m_leftArea.SetSpacing(0.01f, RelativeTo.ScreenShortest);
		this.m_leftArea.RemoveDrawHandler();
		UIRectSpriteSensor uirectSpriteSensor = new UIRectSpriteSensor(this.m_leftArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_arrow_left", null), false);
		uirectSpriteSensor.SetHeight(0.175f, RelativeTo.ScreenShortest);
		uirectSpriteSensor.SetTouchAreaSizeMultipler(1.5f);
		base.AddButton("LeftButton1", uirectSpriteSensor, ControllerButtonType.BUTTON, 276);
		UIRectSpriteSensor uirectSpriteSensor2 = new UIRectSpriteSensor(this.m_leftArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_arrow_right", null), false);
		uirectSpriteSensor2.SetHeight(0.175f, RelativeTo.ScreenShortest);
		uirectSpriteSensor2.SetTouchAreaSizeMultipler(1.5f);
		base.AddButton("LeftButton2", uirectSpriteSensor2, ControllerButtonType.BUTTON, 275);
		this.m_leftArea.Update();
		this.m_rightArea = new UIHorizontalList(null, "buttonArea");
		this.m_rightArea.SetAlign(1f, 0f);
		this.m_rightArea.SetMargins(0f, 0.04f, 0f, 0.04f, RelativeTo.ScreenShortest);
		this.m_rightArea.SetSpacing(0.01f, RelativeTo.ScreenShortest);
		this.m_rightArea.RemoveDrawHandler();
		UIRectSpriteSensor uirectSpriteSensor3 = new UIRectSpriteSensor(this.m_rightArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("controller_button_green", null), false);
		uirectSpriteSensor3.SetHeight(0.175f, RelativeTo.ScreenShortest);
		uirectSpriteSensor3.SetTouchAreaSizeMultipler(1.5f);
		base.AddButton("RightButton", uirectSpriteSensor3, ControllerButtonType.BUTTON, 273);
		this.m_rightArea.Update();
		if (this.m_overlayCamera != null)
		{
			this.m_leftArea.SetCamera(this.m_overlayCamera, true, false);
			this.m_rightArea.SetCamera(this.m_overlayCamera, true, false);
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00031C85 File Offset: 0x00030085
	public override void Close()
	{
		base.Close();
		if (this.m_open)
		{
			this.m_open = false;
			this.m_leftArea.Destroy();
			this.m_rightArea.Destroy();
			Controller.RemoveAllButtons();
		}
	}

	// Token: 0x04000441 RID: 1089
	private UIHorizontalList m_leftArea;

	// Token: 0x04000442 RID: 1090
	private UIHorizontalList m_rightArea;

	// Token: 0x04000443 RID: 1091
	private Camera m_overlayCamera;
}
