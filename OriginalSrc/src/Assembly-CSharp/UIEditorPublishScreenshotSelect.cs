using System;
using UnityEngine;

// Token: 0x02000393 RID: 915
public class UIEditorPublishScreenshotSelect : UIVerticalList
{
	// Token: 0x06001A4A RID: 6730 RVA: 0x00125770 File Offset: 0x00123B70
	public UIEditorPublishScreenshotSelect(UIComponent _parent, string _tag)
		: base(_parent, _tag)
	{
		this.RemoveDrawHandler();
		this.m_screenshotMaterial = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ScreenshotMat_Material));
		UIText uitext = new UIText(this, false, string.Empty, PsStrings.Get(StringID.EDITOR_PUBLISH_SCREENSHOT), PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, null, null);
		uitext.SetColor("#a8e2ff", null);
		uitext.SetMargins(0.03f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uitext.SetHorizontalAlign(0f);
		UICanvas uicanvas = new UICanvas(this, false, "Screenshot", null, string.Empty);
		uicanvas.SetWidth(0.9f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.65f, RelativeTo.OwnWidth);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.ScreenshotDrawHandler));
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "swapButtons");
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		int num = 0;
		for (int i = 0; i < EditorScene.m_storedScreenshots.Length; i++)
		{
			if (EditorScene.m_storedScreenshots[i].m_hasShot)
			{
				num++;
			}
		}
		this.m_screenShotButtons = new UIEditorPublishScreenshotSelect.PsUIScreenshotButton[Mathf.Max(1, num)];
		for (int j = 0; j < this.m_screenShotButtons.Length; j++)
		{
			this.m_screenShotButtons[j] = new UIEditorPublishScreenshotSelect.PsUIScreenshotButton(uihorizontalList, EditorScene.m_storedScreenshots[j]);
			this.m_screenShotButtons[j].SetWidth(0.12f, RelativeTo.ScreenShortest);
			this.m_screenShotButtons[j].SetHeight(0.65f, RelativeTo.OwnWidth);
		}
		if (this.m_screenShotButtons[0] != null)
		{
			this.SetScreenshot(0);
			this.m_selectedScreenshotButton = this.m_screenShotButtons[0];
			this.m_selectedScreenshotButton.SetHightlighted(true);
		}
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x00125922 File Offset: 0x00123D22
	public void SetScreenshot(Screenshot _screenshot)
	{
		if (_screenshot != null)
		{
			this.m_screenshotMaterial.mainTexture = _screenshot.GetScreenshotTex2D();
		}
		else
		{
			Debug.Log("TRYING TO SET NULL SCREENSHOT!", null);
		}
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x00125954 File Offset: 0x00123D54
	public void SetScreenshot(int _index)
	{
		if (this.m_screenShotButtons[_index].m_screenshot != null)
		{
			EditorScene.SetSelectedScreenshot(_index);
			this.m_screenshotMaterial.mainTexture = this.m_screenShotButtons[_index].m_screenshot.GetScreenshotTex2D();
		}
		else
		{
			Debug.Log("TRYING TO SET NULL SCREENSHOT!", null);
		}
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x001259AC File Offset: 0x00123DAC
	public void ScreenshotDrawHandler(UIComponent _c)
	{
		float num = 0.02f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] bezierRect = DebugDraw.GetBezierRect(_c.m_actualWidth - num * 3f * (float)Screen.height, _c.m_actualHeight - num * 3f * (float)Screen.height, num * (float)Screen.height, 10, Vector2.zero);
		UVRect uvrect = new UVRect(0f, 0.55f - _c.m_actualHeight / _c.m_actualWidth / 2f, 1f, _c.m_actualHeight / _c.m_actualWidth);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 1f, bezierRect, uint.MaxValue, uint.MaxValue, this.m_screenshotMaterial, this.m_camera, string.Empty, uvrect);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f, bezierRect, (float)Screen.height * 0.006f, DebugDraw.HexToColor("#41acee"), DebugDraw.HexToColor("#86d9f9"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -3f, bezierRect, (float)Screen.height * 0.025f, new Color(1f, 1f, 1f, 0.3f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x00125B18 File Offset: 0x00123F18
	public override void Step()
	{
		for (int i = 0; i < this.m_screenShotButtons.Length; i++)
		{
			if (this.m_screenShotButtons[i] != this.m_selectedScreenshotButton && this.m_screenShotButtons[i].m_hit)
			{
				this.SetScreenshot(i);
				this.m_selectedScreenshotButton.SetHightlighted(false);
				this.m_selectedScreenshotButton = this.m_screenShotButtons[i];
				this.m_selectedScreenshotButton.SetHightlighted(true);
			}
		}
		base.Step();
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x00125B96 File Offset: 0x00123F96
	public override void Destroy()
	{
		base.Destroy();
		Object.DestroyImmediate(this.m_screenshotMaterial);
	}

	// Token: 0x04001CD4 RID: 7380
	private Material m_screenshotMaterial;

	// Token: 0x04001CD5 RID: 7381
	private UIEditorPublishScreenshotSelect.PsUIScreenshotButton m_selectedScreenshotButton;

	// Token: 0x04001CD6 RID: 7382
	private UIEditorPublishScreenshotSelect.PsUIScreenshotButton[] m_screenShotButtons;

	// Token: 0x02000394 RID: 916
	public class PsUIScreenshotButton : UICanvas
	{
		// Token: 0x06001A50 RID: 6736 RVA: 0x00125BAC File Offset: 0x00123FAC
		public PsUIScreenshotButton(UIComponent _parent, Screenshot _screenshot)
			: base(_parent, true, string.Empty, null, string.Empty)
		{
			this.m_screenshot = _screenshot;
			this.SetDrawHandler(new UIDrawDelegate(this.DrawHandler));
			this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ScreenshotMat_Material));
			if (this.m_screenshot != null)
			{
				this.m_material.mainTexture = this.m_screenshot.GetScreenshotTex2D();
			}
			this.SetHightlighted(false);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00125C2C File Offset: 0x0012402C
		public override void DrawHandler(UIComponent _c)
		{
			float num = 0.007f;
			PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
			float num2 = (float)Screen.height * 0.005f;
			Vector2[] bezierRect = DebugDraw.GetBezierRect(_c.m_actualWidth - num * 3f * (float)Screen.height + num2, _c.m_actualHeight - num * 3f * (float)Screen.height + num2, num * (float)Screen.height + num2 / 2f, 10, Vector2.zero);
			this.m_ps = PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 3f, bezierRect, uint.MaxValue, uint.MaxValue, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera, string.Empty, null);
			this.m_p = PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 2f, bezierRect, (float)Screen.height * 0.004f, Color.white, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
			PrefabS.SetVisibility(this.m_ps, this.m_highlighted, true);
			PrefabS.SetVisibility(this.m_p, this.m_highlighted, true);
			Vector2[] bezierRect2 = DebugDraw.GetBezierRect(_c.m_actualWidth - num * 3f * (float)Screen.height, _c.m_actualHeight - num * 3f * (float)Screen.height, num * (float)Screen.height, 10, Vector2.zero);
			UVRect uvrect = new UVRect(0f, 0.55f - _c.m_actualHeight / _c.m_actualWidth / 2f, 1f, _c.m_actualHeight / _c.m_actualWidth);
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 1f, bezierRect2, uint.MaxValue, uint.MaxValue, this.m_material, this.m_camera, string.Empty, uvrect);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f, bezierRect2, (float)Screen.height * 0.004f, DebugDraw.HexToColor("#41acee"), DebugDraw.HexToColor("#86d9f9"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00125E43 File Offset: 0x00124243
		public override void Destroy()
		{
			base.Destroy();
			Object.DestroyImmediate(this.m_material);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00125E58 File Offset: 0x00124258
		public void SetHightlighted(bool flag)
		{
			this.m_highlighted = flag;
			if (this.m_p != null)
			{
				PrefabS.SetVisibility(this.m_p, this.m_highlighted, true);
			}
			if (this.m_ps != null)
			{
				PrefabS.SetVisibility(this.m_ps, this.m_highlighted, true);
			}
			if (this.m_highlighted)
			{
				this.m_material.color = Color.white;
			}
			else
			{
				this.m_material.color = Color.gray;
			}
		}

		// Token: 0x04001CD7 RID: 7383
		private Material m_material;

		// Token: 0x04001CD8 RID: 7384
		public Screenshot m_screenshot;

		// Token: 0x04001CD9 RID: 7385
		private bool m_highlighted;

		// Token: 0x04001CDA RID: 7386
		private PrefabC m_ps;

		// Token: 0x04001CDB RID: 7387
		private PrefabC m_p;
	}
}
