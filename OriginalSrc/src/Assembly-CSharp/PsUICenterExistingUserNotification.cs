using System;
using UnityEngine;

// Token: 0x02000345 RID: 837
public class PsUICenterExistingUserNotification : PsUIHeaderedCanvas
{
	// Token: 0x0600188B RID: 6283 RVA: 0x0010AFD8 File Offset: 0x001093D8
	public PsUICenterExistingUserNotification(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.75f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x0010B0D4 File Offset: 0x001094D4
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, " New Update", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x0010B14C File Offset: 0x0010954C
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x0010B1CC File Offset: 0x001095CC
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		string text = "Welcome to updated Big Bang Racing!\n\nWe have fine-tuned the user interface and merged the treasure chest slots of both vehicles to make a more coherent gameplay experience. Every player who might have lost an unopened chest in the process has been given a free golden chest, ready to be opened immediately. Gems can now also be purchased from the shop, though the game is (and always will be) fully playable without spending real money.\n\nEnjoy the new version!\n- Big Bang Racing team";
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetText("Alright!", 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_continue.SetGreenColors(true);
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x0010B2A0 File Offset: 0x001096A0
	public override void Step()
	{
		if (this.m_continue.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		base.Step();
	}

	// Token: 0x04001B39 RID: 6969
	private PsUIGenericButton m_continue;
}
