using System;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class PsUICenterLocalSavePrompt : PsUIHeaderedCanvas
{
	// Token: 0x0600189C RID: 6300 RVA: 0x0010B9B8 File Offset: 0x00109DB8
	public PsUICenterLocalSavePrompt(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.6f, RelativeTo.ScreenWidth);
		this.SetHeight(0.6f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.05f, 0.05f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x0010BAB4 File Offset: 0x00109EB4
	public void CreateHeaderContent(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.BACKUP_PROMPT), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x0010BB34 File Offset: 0x00109F34
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x0010BBB4 File Offset: 0x00109FB4
	public void CreateContent(UIComponent _parent)
	{
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, PsStrings.Get(StringID.BACKUP_INFO_POPUP), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0325f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "ffffff", true, "313131");
		uitextbox.SetMargins(0.05f, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetVerticalAlign(0f);
		uicanvas.SetMargins(0f, 0f, 0.0175f, -0.0175f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		this.m_ok = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetGreenColors(true);
		this.m_ok.SetText(PsStrings.Get(StringID.OK), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_ok.SetVerticalAlign(1f);
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x0010BCB0 File Offset: 0x0010A0B0
	public override void Step()
	{
		if (this.m_ok.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		base.Step();
	}

	// Token: 0x04001B42 RID: 6978
	private PsUIGenericButton m_ok;
}
