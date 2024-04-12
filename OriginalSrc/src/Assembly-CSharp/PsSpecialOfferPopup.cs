using System;
using UnityEngine;

// Token: 0x02000334 RID: 820
public class PsSpecialOfferPopup : PsUIHeaderedCanvas
{
	// Token: 0x060017F1 RID: 6129 RVA: 0x00102730 File Offset: 0x00100B30
	public PsSpecialOfferPopup(UIComponent _parent)
		: base(_parent, "DirtbikeBundlePopup", true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.8f, RelativeTo.ScreenWidth);
		this.SetHeight(0.6f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.m_price = "NaN";
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x00102824 File Offset: 0x00100C24
	public void CreateHeaderContent(UIComponent _parent)
	{
		PsSpecialOfferData specialOfferById = PsSpecialOfferManager.GetSpecialOfferById(this.m_bundleId);
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.SHOP_INFO_SPECIAL_OFFER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenHeight, "#95e5ff", null);
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.14f, 0.14f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.SetMargins(-0.05f, 0.05f, -0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetRogue();
		uicanvas.RemoveDrawHandler();
		uicanvas.SetDepthOffset(-20f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_advert_badge", null), true, true);
		uifittedSprite.SetColor(Color.Lerp(DebugDraw.HexToColor("#7BE500"), Color.black, 0.5f));
		uifittedSprite.SetMargins(0.16f, RelativeTo.OwnHeight);
		UIFittedText uifittedText = new UIFittedText(uifittedSprite, false, string.Empty, "+" + specialOfferById.percentage + "%", PsFontManager.GetFont(PsFonts.HurmeBold), true, "#223F00", null);
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x001029A4 File Offset: 0x00100DA4
	public void SetPrice(string _price)
	{
		this.m_price = _price;
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x001029AD File Offset: 0x00100DAD
	public void CreateBundle(string _id)
	{
		this.m_bundleId = _id;
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
		this.GetRoot().Update();
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x001029D4 File Offset: 0x00100DD4
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x00102A54 File Offset: 0x00100E54
	public void CreateContent(UIComponent _parent)
	{
		PsSpecialOfferData specialOfferById = PsSpecialOfferManager.GetSpecialOfferById(this.m_bundleId);
		float num = 0f;
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0.75f);
		if (!string.IsNullOrEmpty(specialOfferById.trailIdentifier))
		{
			num += 0.305f;
			UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas.SetWidth(0.25f, RelativeTo.ScreenHeight);
			uicanvas.SetHeight(0.35f, RelativeTo.ScreenHeight);
			uicanvas.SetMargins(0.0125f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			PsCustomisationItem itemByIdentifier = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(specialOfferById.trailIdentifier);
			UIComponent uicomponent = new UIComponent(uicanvas, false, string.Empty, null, null, string.Empty);
			uicomponent.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicomponent.RemoveDrawHandler();
			uicomponent.SetVerticalAlign(0.07f);
			UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, PsStrings.Get(itemByIdentifier.m_title), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", null);
			UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(itemByIdentifier.m_iconName, null), true);
			uisprite.SetSize(0.9f, 0.9f, RelativeTo.ParentWidth);
			uisprite.SetMargins(0f, 0f, 0f, -0.04f, RelativeTo.ScreenHeight);
			uisprite.SetVerticalAlign(0.55f);
		}
		if (!string.IsNullOrEmpty(specialOfferById.hatIdentifier))
		{
			num += 0.305f;
			UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas2.SetWidth(0.25f, RelativeTo.ScreenHeight);
			uicanvas2.SetHeight(0.35f, RelativeTo.ScreenHeight);
			uicanvas2.SetMargins(0.0125f, RelativeTo.ScreenHeight);
			uicanvas2.RemoveDrawHandler();
			PsCustomisationItem itemByIdentifier2 = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(specialOfferById.hatIdentifier);
			UIComponent uicomponent2 = new UIComponent(uicanvas2, false, string.Empty, null, null, string.Empty);
			uicomponent2.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicomponent2.RemoveDrawHandler();
			uicomponent2.SetVerticalAlign(0.07f);
			UIFittedText uifittedText2 = new UIFittedText(uicomponent2, false, string.Empty, PsStrings.Get(itemByIdentifier2.m_title), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", null);
			UISprite uisprite2 = new UISprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(itemByIdentifier2.m_iconName, null), true);
			uisprite2.SetSize(0.9f, 0.9f, RelativeTo.ParentWidth);
			uisprite2.SetMargins(0f, 0f, 0f, -0.04f, RelativeTo.ScreenHeight);
			uisprite2.SetVerticalAlign(0.55f);
		}
		if (!string.IsNullOrEmpty(specialOfferById.gemIconIdentifier))
		{
			num += 0.305f;
			UICanvas uicanvas3 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas3.SetWidth(0.25f, RelativeTo.ScreenHeight);
			uicanvas3.SetHeight(0.35f, RelativeTo.ScreenHeight);
			uicanvas3.SetMargins(0.0125f, RelativeTo.ScreenHeight);
			uicanvas3.RemoveDrawHandler();
			UIComponent uicomponent3 = new UIComponent(uicanvas3, false, string.Empty, null, null, string.Empty);
			uicomponent3.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicomponent3.RemoveDrawHandler();
			uicomponent3.SetVerticalAlign(0.07f);
			UIFittedText uifittedText3 = new UIFittedText(uicomponent3, false, string.Empty, specialOfferById.gems + " " + PsStrings.Get(StringID.GEMS), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", null);
			UISprite uisprite3 = new UISprite(uicanvas3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(specialOfferById.gemIconIdentifier, null), true);
			uisprite3.SetSize(0.9f, 0.9f, RelativeTo.ParentWidth);
			uisprite3.SetMargins(0f, 0f, 0f, -0.04f, RelativeTo.ScreenHeight);
			uisprite3.SetVerticalAlign(0.55f);
		}
		if (!string.IsNullOrEmpty(specialOfferById.coinIconIdentifier))
		{
			num += 0.305f;
			UICanvas uicanvas4 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas4.SetWidth(0.25f, RelativeTo.ScreenHeight);
			uicanvas4.SetHeight(0.35f, RelativeTo.ScreenHeight);
			uicanvas4.SetMargins(0.0125f, RelativeTo.ScreenHeight);
			uicanvas4.RemoveDrawHandler();
			UIComponent uicomponent4 = new UIComponent(uicanvas4, false, string.Empty, null, null, string.Empty);
			uicomponent4.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uicomponent4.RemoveDrawHandler();
			uicomponent4.SetVerticalAlign(0.07f);
			UIFittedText uifittedText4 = new UIFittedText(uicomponent4, false, string.Empty, specialOfferById.coins + " " + PsStrings.Get(StringID.GACHA_OPEN_COINS), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FFFFFF", null);
			UISprite uisprite4 = new UISprite(uicanvas4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(specialOfferById.coinIconIdentifier, null), true);
			uisprite4.SetSize(0.9f, 0.9f, RelativeTo.ParentWidth);
			uisprite4.SetMargins(0f, 0f, 0f, -0.04f, RelativeTo.ScreenHeight);
			uisprite4.SetVerticalAlign(0.55f);
		}
		uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		this.m_purchaseButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_purchaseButton.SetText(this.m_price, 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_purchaseButton.SetGreenColors(true);
		this.m_purchaseButton.SetVerticalAlign(0f);
		this.SetWidth(Mathf.Max(0.6f, num), RelativeTo.ScreenHeight);
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x00103012 File Offset: 0x00101412
	public override void Step()
	{
		if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Purchased");
		}
		base.Step();
	}

	// Token: 0x04001AC4 RID: 6852
	private PsUIGenericButton m_purchaseButton;

	// Token: 0x04001AC5 RID: 6853
	private string m_price;

	// Token: 0x04001AC6 RID: 6854
	private string m_bundleId;
}
