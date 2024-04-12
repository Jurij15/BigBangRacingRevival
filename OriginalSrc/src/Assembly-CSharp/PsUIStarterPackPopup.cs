using System;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class PsUIStarterPackPopup : PsUIHeaderedCanvas
{
	// Token: 0x0600186C RID: 6252 RVA: 0x00108F9C File Offset: 0x0010739C
	public PsUIStarterPackPopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetHeight(0.82f, RelativeTo.ScreenHeight);
		this.SetWidth(0.9675999f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0625f, 0.0625f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.m_price = "NaN";
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x00109090 File Offset: 0x00107490
	public override void CreateCloseButton()
	{
		UICanvas uicanvas = new UICanvas(this.m_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.125f, 0.125f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(1f, 1f);
		uicanvas.SetMargins(0.4f, -0.4f, -0.4f, 0.4f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetDepthOffset(-20f);
		this.m_exitButton = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetOrangeColors(true);
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetIcon("menu_icon_close", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x00109164 File Offset: 0x00107564
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.DIRTBIKE_POPUP_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x00109195 File Offset: 0x00107595
	public void SetPrice(string _price)
	{
		this.m_price = _price;
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x0010919E File Offset: 0x0010759E
	public void CreateContent()
	{
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
		this.m_container.Update();
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x001091C0 File Offset: 0x001075C0
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x00109240 File Offset: 0x00107640
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetSpacing(0.02f, RelativeTo.ParentHeight);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetMargins(0f, 0f, 0.11f, 0f, RelativeTo.ParentHeight);
		uiverticalList.SetVerticalAlign(1f);
		Material material = new Material(Shader.Find("Framework/VertexColorUnlitDouble"));
		material.mainTexture = ResourceManager.GetTexture(RESOURCE.menu_dirtbike_starterpack_banner_Texture2D);
		UISprite uisprite = new UISprite(uiverticalList, false, string.Empty, material, true);
		uisprite.SetSize(1f, (float)uisprite.m_textureMaterial.mainTexture.height / (float)uisprite.m_textureMaterial.mainTexture.width, RelativeTo.ParentWidth);
		uisprite.SetDepthOffset(4f);
		UIVerticalList uiverticalList2 = new UIVerticalList(uisprite, string.Empty);
		uiverticalList2.SetMargins(0.1f, 0f, -0.04f, 0f, RelativeTo.ParentHeight);
		uiverticalList2.SetWidth(0.35f, RelativeTo.ParentWidth);
		uiverticalList2.SetAlign(0f, 1f);
		uiverticalList2.SetSpacing(0.015f, RelativeTo.ParentHeight);
		uiverticalList2.RemoveDrawHandler();
		UISprite uisprite2 = new UISprite(uiverticalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_vehicle_logo_dirtbike", null), true);
		uisprite2.SetSize(0.76f, uisprite2.m_frame.height / uisprite2.m_frame.width * 0.78f, RelativeTo.ParentWidth);
		UICanvas uicanvas = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.04f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.IAP_STARTERPACK).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList2, string.Empty);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ParentHeight);
		uihorizontalList.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(0.5f, 0.5f, RelativeTo.ParentWidth);
		uicanvas2.RemoveDrawHandler();
		UISprite uisprite3 = new UISprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_new_planet_icon", null), true);
		uisprite3.SetSize(uisprite3.m_frame.width / uisprite3.m_frame.height * 0.76f, 0.76f, RelativeTo.ParentHeight);
		uisprite3.SetVerticalAlign(1f);
		UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(0.2f, RelativeTo.ParentHeight);
		uicanvas3.SetVerticalAlign(0f);
		uicanvas3.SetMargins(0.1f, RelativeTo.OwnHeight);
		uicanvas3.SetDrawHandler(new UIDrawDelegate(this.BackgroundDrawHandler));
		UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.IAP_NEW_PLANET), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		UICanvas uicanvas4 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas4.SetSize(0.5f, 0.5f, RelativeTo.ParentWidth);
		uicanvas4.RemoveDrawHandler();
		UISprite uisprite4 = new UISprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_dirtbike_bundle_dirtbike", null), true);
		uisprite4.SetSize(uisprite4.m_frame.width / uisprite4.m_frame.height * 0.76f, 0.76f, RelativeTo.ParentHeight);
		uisprite4.SetVerticalAlign(1f);
		UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
		uicanvas5.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas5.SetHeight(0.2f, RelativeTo.ParentHeight);
		uicanvas5.SetVerticalAlign(0f);
		uicanvas5.SetMargins(0.1f, RelativeTo.OwnHeight);
		uicanvas5.SetDrawHandler(new UIDrawDelegate(this.BackgroundDrawHandler));
		UIFittedText uifittedText3 = new UIFittedText(uicanvas5, false, string.Empty, PsStrings.Get(StringID.IAP_NEW_VEHICLE), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList2, string.Empty);
		uihorizontalList2.SetSpacing(0.015f, RelativeTo.ParentHeight);
		uihorizontalList2.RemoveDrawHandler();
		UICanvas uicanvas6 = new UICanvas(uihorizontalList2, false, string.Empty, null, string.Empty);
		uicanvas6.SetSize(0.5f, 0.5f, RelativeTo.ParentWidth);
		uicanvas6.RemoveDrawHandler();
		UISprite uisprite5 = new UISprite(uicanvas6, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_dirtbike_bundle_helmet", null), true);
		uisprite5.SetSize(uisprite5.m_frame.width / uisprite5.m_frame.height * 0.76f, 0.76f, RelativeTo.ParentHeight);
		uisprite5.SetVerticalAlign(1f);
		UICanvas uicanvas7 = new UICanvas(uicanvas6, false, string.Empty, null, string.Empty);
		uicanvas7.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas7.SetHeight(0.2f, RelativeTo.ParentHeight);
		uicanvas7.SetVerticalAlign(0f);
		uicanvas7.SetMargins(0.1f, RelativeTo.OwnHeight);
		uicanvas7.SetDrawHandler(new UIDrawDelegate(this.BackgroundDrawHandler));
		UIFittedText uifittedText4 = new UIFittedText(uicanvas7, false, string.Empty, PsStrings.Get(StringID.SHOP_INFO_EXCLUSIVE_HAT), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		UICanvas uicanvas8 = new UICanvas(uihorizontalList2, false, string.Empty, null, string.Empty);
		uicanvas8.SetSize(0.5f, 0.5f, RelativeTo.ParentWidth);
		uicanvas8.RemoveDrawHandler();
		UISprite uisprite6 = new UISprite(uicanvas8, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_dirtbike_bundle_gems", null), true);
		uisprite6.SetSize(uisprite6.m_frame.width / uisprite6.m_frame.height * 0.76f, 0.76f, RelativeTo.ParentHeight);
		uisprite6.SetVerticalAlign(1f);
		UICanvas uicanvas9 = new UICanvas(uicanvas8, false, string.Empty, null, string.Empty);
		uicanvas9.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas9.SetHeight(0.2f, RelativeTo.ParentHeight);
		uicanvas9.SetVerticalAlign(0f);
		uicanvas9.SetMargins(0.1f, RelativeTo.OwnHeight);
		uicanvas9.SetDrawHandler(new UIDrawDelegate(this.BackgroundDrawHandler));
		UIFittedText uifittedText5 = new UIFittedText(uicanvas9, false, string.Empty, "1000 " + PsStrings.Get(StringID.GEMS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		UICanvas uicanvas10 = new UICanvas(uisprite, false, string.Empty, null, string.Empty);
		uicanvas10.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas10.SetHeight(0.06f, RelativeTo.ParentHeight);
		uicanvas10.SetVerticalAlign(0.045f);
		uicanvas10.SetMargins(0.18f, 0.1f, 0f, 0f, RelativeTo.ParentWidth);
		uicanvas10.RemoveDrawHandler();
		UIFittedText uifittedText6 = new UIFittedText(uicanvas10, false, string.Empty, "+ " + PsStrings.Get(StringID.IAP_BONUS_OPEN_CHESTS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		uifittedText6.SetHorizontalAlign(0f);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.DIRTBIKE_POPUP_FOOTER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ParentHeight, false, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetMaxRows(2);
		uitextbox.SetMinRows(2);
		UIHorizontalList uihorizontalList3 = new UIHorizontalList(this, string.Empty);
		uihorizontalList3.RemoveDrawHandler();
		uihorizontalList3.SetAlign(0.5f, 0f);
		uihorizontalList3.SetMargins(0f, 0f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		this.m_purchaseButton = new PsUIGenericButton(uihorizontalList3, 0.25f, 0.25f, 0.005f, "Button");
		this.m_purchaseButton.SetText(this.m_price, 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_purchaseButton.SetGreenColors(true);
		this.m_purchaseButton.SetVerticalAlign(0f);
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x001099FC File Offset: 0x00107DFC
	public void BackgroundDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x00109A6D File Offset: 0x00107E6D
	public override void Step()
	{
		if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
		}
		base.Step();
	}

	// Token: 0x04001B1E RID: 6942
	private PsUIGenericButton m_purchaseButton;

	// Token: 0x04001B1F RID: 6943
	private string m_price;
}
