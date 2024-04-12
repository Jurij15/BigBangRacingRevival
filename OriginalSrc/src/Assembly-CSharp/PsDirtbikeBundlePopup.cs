using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class PsDirtbikeBundlePopup : PsUIHeaderedCanvas
{
	// Token: 0x060017C3 RID: 6083 RVA: 0x0010079C File Offset: 0x000FEB9C
	public PsDirtbikeBundlePopup(UIComponent _parent)
		: base(_parent, "DirtbikeBundlePopup", true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.85f, RelativeTo.ScreenHeight);
		this.SetHeight(0.6f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.m_price = "NaN";
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x00100890 File Offset: 0x000FEC90
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.SHOP_INFO_SPECIAL_OFFER), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x0010090A File Offset: 0x000FED0A
	public void SetPrice(string _price)
	{
		this.m_price = _price;
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x00100913 File Offset: 0x000FED13
	public void CreateContent()
	{
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
		this.m_container.Update();
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x00100934 File Offset: 0x000FED34
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x001009B4 File Offset: 0x000FEDB4
	public void CreateContent(UIComponent _parent)
	{
		UIText uitext = new UIText(this, false, string.Empty, PsStrings.Get(StringID.SHOP_IAP_DIRTBIKE_NAME), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenHeight, "#ffffff", null);
		uitext.SetVerticalAlign(0.95f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0.5f);
		UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.25f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.35f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.0125f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIComponent uicomponent = new UIComponent(uicanvas, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetVerticalAlign(0.07f);
		UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, PsStrings.Get(StringID.EDITOR_GUI_VEHICLE_NAME_BIKE), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_dirtbike_bundle_dirtbike", null), true);
		uisprite.SetSize(0.9f, 0.9f, RelativeTo.ParentWidth);
		uisprite.SetMargins(0f, 0f, 0f, -0.04f, RelativeTo.ScreenHeight);
		uisprite.SetVerticalAlign(0.55f);
		uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.25f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.35f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.0125f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIComponent uicomponent2 = new UIComponent(uicanvas, false, string.Empty, null, null, string.Empty);
		uicomponent2.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicomponent2.RemoveDrawHandler();
		uicomponent2.SetVerticalAlign(0.07f);
		UIFittedText uifittedText2 = new UIFittedText(uicomponent2, false, string.Empty, PsStrings.Get(StringID.SHOP_INFO_EXCLUSIVE_HAT), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", null);
		UISprite uisprite2 = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_dirtbike_bundle_helmet", null), true);
		uisprite2.SetSize(0.9f, 0.9f, RelativeTo.ParentWidth);
		uisprite2.SetMargins(0f, 0f, 0f, -0.04f, RelativeTo.ScreenHeight);
		uisprite2.SetVerticalAlign(0.55f);
		uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.25f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.35f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.0125f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIComponent uicomponent3 = new UIComponent(uicanvas, false, string.Empty, null, null, string.Empty);
		uicomponent3.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicomponent3.RemoveDrawHandler();
		uicomponent3.SetVerticalAlign(0.07f);
		UIFittedText uifittedText3 = new UIFittedText(uicomponent3, false, string.Empty, "1000 " + PsStrings.Get(StringID.GEMS), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", null);
		UISprite uisprite3 = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_dirtbike_bundle_gems", null), true);
		uisprite3.SetSize(0.9f, 0.9f, RelativeTo.ParentWidth);
		uisprite3.SetMargins(0f, 0f, 0f, -0.04f, RelativeTo.ScreenHeight);
		uisprite3.SetVerticalAlign(0.55f);
		uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		this.m_purchaseButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_purchaseButton.SetText(this.m_price, 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_purchaseButton.SetGreenColors(true);
		this.m_purchaseButton.SetVerticalAlign(0f);
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x00100DA4 File Offset: 0x000FF1A4
	public override void Step()
	{
		if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
		}
		base.Step();
	}

	// Token: 0x04001A97 RID: 6807
	private PsUIGenericButton m_purchaseButton;

	// Token: 0x04001A98 RID: 6808
	private string m_price;
}
