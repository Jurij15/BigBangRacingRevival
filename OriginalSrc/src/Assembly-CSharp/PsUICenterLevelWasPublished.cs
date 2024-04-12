using System;
using DeepLink;
using Facebook.Unity;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class PsUICenterLevelWasPublished : UICanvas
{
	// Token: 0x06001A0A RID: 6666 RVA: 0x0012013C File Offset: 0x0011E53C
	public PsUICenterLevelWasPublished(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupBackground));
		UIText uitext = new UIText(this, false, string.Empty, PsStrings.Get(StringID.LEVEL_PUBLISH_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07f, RelativeTo.ScreenHeight, "89FF2E", "313131");
		uitext.SetVerticalAlign(0.95f);
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.9f, RelativeTo.ScreenWidth);
		uicanvas.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas.SetVerticalAlign(0.85f);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.LEVEL_PUBLISH_INFO), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "FFFFFF", "313131");
		UIVerticalList uiverticalList = new UIVerticalList(this, "vlist");
		uiverticalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(0.25f);
		uiverticalList.RemoveDrawHandler();
		PsUICenterLevelWasPublished.PsUIScreenshot2 psUIScreenshot = new PsUICenterLevelWasPublished.PsUIScreenshot2(uiverticalList, EditorScene.GetSelectedScreenshot());
		psUIScreenshot.SetSize(0.6f, 0.375f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetVerticalAlign(0.3f);
		uihorizontalList.SetHeight(0.3f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.075f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.LEVEL_PUBLISH_INFO_MORE);
		PsUIInfoBox psUIInfoBox = new PsUIInfoBox(uihorizontalList, text, "InfoBox", false, "KGSecondChances_Font", 0.0225f, RelativeTo.ScreenHeight);
		psUIInfoBox.SetWidth(0.4f, RelativeTo.ScreenHeight);
		psUIInfoBox.SetHeight(0.3f, RelativeTo.ScreenHeight);
		psUIInfoBox.SetVerticalAlign(1f);
		UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.25f, RelativeTo.ScreenHeight);
		uicanvas2.SetHeight(0.15f, RelativeTo.ScreenHeight);
		uicanvas2.SetVerticalAlign(1f);
		uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.045f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetMargins(0.005f, RelativeTo.ScreenHeight);
		uicanvas3.SetVerticalAlign(1f);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.SHARE).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "89FF2E", "313131");
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uicanvas2, "shareArea");
		uihorizontalList2.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetVerticalAlign(0.25f);
		this.m_fbSharebutton = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_fbSharebutton.SetIcon("menu_icon_facebook", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_sharebutton = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
		string text2 = "menu_icon_share";
		this.m_sharebutton.SetIcon(text2, 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_continue = new PsUIGenericButton(this, 0.25f, 0.25f, 0.01f, "Button");
		this.m_continue.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_continue.SetGreenColors(true);
		this.m_continue.SetFittedText(PsStrings.Get(StringID.CONTINUE), 0.04f, 0.3f, RelativeTo.ScreenHeight, true);
		this.m_continue.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_continue.SetVerticalAlign(0.03f);
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x00120538 File Offset: 0x0011E938
	public override void Step()
	{
		if (this.m_fbSharebutton != null && this.m_fbSharebutton.m_hit)
		{
			this.ShareToFacebook();
			PsMetrics.LevelShared("publishScreenFb", PsState.m_activeGameLoop.m_minigameId, PsState.m_activeGameLoop.GetName(), PsState.m_activeGameLoop.GetCreatorId(), PsState.m_activeGameLoop.GetCreatorName());
		}
		else if (this.m_sharebutton != null && this.m_sharebutton.m_hit)
		{
			Share.ShareTextOnPlatform(PsUrlLaunch.GetLevelLinkUrl(PsState.m_activeGameLoop.m_minigameId));
			PsMetrics.LevelShared("publishScreen", PsState.m_activeGameLoop.m_minigameId, PsState.m_activeGameLoop.GetName(), PsState.m_activeGameLoop.GetCreatorId(), PsState.m_activeGameLoop.GetCreatorName());
		}
		else if (this.m_continue != null && this.m_continue.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		base.Step();
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x0012063C File Offset: 0x0011EA3C
	public void ShareToFacebook()
	{
		string text = PsStrings.Get(StringID.FB_LEVEL_SHARE_TEXT);
		text = text.Replace("%1", PsState.m_activeGameLoop.GetName());
		FB.ShareLink(new Uri(PsUrlLaunch.GetLevelLinkUrl(PsState.m_activeGameLoop.m_minigameId)), PsState.m_activeGameLoop.GetName(), text, null, new FacebookDelegate<IShareResult>(this.FBCallback));
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x0012069B File Offset: 0x0011EA9B
	private void FBCallback(IShareResult _result)
	{
		Debug.Log(string.Concat(new object[] { "Facebook ", _result.Error, ", cancelled: ", _result.Cancelled }), null);
	}

	// Token: 0x04001C6D RID: 7277
	private PsUIGenericButton m_fbSharebutton;

	// Token: 0x04001C6E RID: 7278
	private PsUIGenericButton m_sharebutton;

	// Token: 0x04001C6F RID: 7279
	private PsUIGenericButton m_continue;

	// Token: 0x02000388 RID: 904
	public class PsUIScreenshot2 : UICanvas
	{
		// Token: 0x06001A0E RID: 6670 RVA: 0x001206D8 File Offset: 0x0011EAD8
		public PsUIScreenshot2(UIComponent _parent, Screenshot _shot)
			: base(_parent, false, string.Empty, null, string.Empty)
		{
			this.m_cornerSize = 0.045f;
			this.m_material = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ScreenshotMat_Material));
			this.m_material.mainTexture = _shot.GetScreenshotTex2D();
			this.SetDrawHandler(new UIDrawDelegate(this.DrawHandler));
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0012073C File Offset: 0x0011EB3C
		public override void DrawHandler(UIComponent _c)
		{
			this.m_TC.transform.localScale = Vector3.one;
			_c.m_TC.transform.localScale = Vector3.one;
			PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, false);
			Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.3f, 10, Vector2.zero);
			Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth * 0.985f, _c.m_actualHeight * 0.985f, _c.m_actualHeight * 0.3f * 0.98f, 10, Vector2.zero);
			UVRect uvrect = new UVRect(0f, 0.55f - _c.m_actualHeight / _c.m_actualWidth / 2f, 1f, _c.m_actualHeight / _c.m_actualWidth);
			for (int i = 0; i < roundedRect.Length; i++)
			{
				roundedRect[i] = Quaternion.Euler(new Vector3(0f, 0f, -1.5f)) * roundedRect[i];
			}
			for (int j = 0; j < roundedRect2.Length; j++)
			{
				roundedRect2[j] = Quaternion.Euler(new Vector3(0f, 0f, -1.5f)) * roundedRect2[j];
			}
			uint num = DebugDraw.HexToUint("#0156b2");
			uint num2 = DebugDraw.HexToUint("#2e8599");
			uint num3 = DebugDraw.HexToUint("#1a64a8");
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.forward * 1f + Vector3.up * 2.5f, roundedRect, uint.MaxValue, uint.MaxValue, this.m_material, this.m_camera, string.Empty, uvrect);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f + Vector3.up * 2.5f, roundedRect, (float)Screen.height * 0.008f, DebugDraw.HexToColor("#41acee"), DebugDraw.HexToColor("#86d9f9"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f + Vector3.up * 2.5f, roundedRect, (float)Screen.height * 0.03f, new Color(1f, 1f, 1f, 0.3f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x001209F5 File Offset: 0x0011EDF5
		public override void Destroy()
		{
			base.Destroy();
			Object.DestroyImmediate(this.m_material);
		}

		// Token: 0x04001C73 RID: 7283
		private Material m_material;

		// Token: 0x04001C74 RID: 7284
		private float m_cornerSize;

		// Token: 0x04001C75 RID: 7285
		private Texture2D m_texture;
	}
}
