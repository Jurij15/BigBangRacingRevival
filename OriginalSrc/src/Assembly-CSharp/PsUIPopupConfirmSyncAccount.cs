using System;
using UnityEngine;

// Token: 0x020003CD RID: 973
public class PsUIPopupConfirmSyncAccount : PsUIHeaderedCanvas
{
	// Token: 0x06001B84 RID: 7044 RVA: 0x00132F50 File Offset: 0x00131350
	public PsUIPopupConfirmSyncAccount(UIComponent _parent, string _serviceName)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.85f, RelativeTo.ScreenHeight);
		this.SetHeight(0.65f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.45f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		this.m_confirm = new UIRectSpriteButton(uihorizontalList, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), true, false);
		this.m_confirm.SetWidth(0.45f, RelativeTo.ParentHeight);
		this.m_confirm.SetHeight(this.m_confirm.m_frame.height / this.m_confirm.m_frame.width * 0.45f, RelativeTo.ParentHeight);
		this.m_confirm.SetMargins(0.08f, 0.08f, 0.06f, 0.04f, RelativeTo.OwnWidth);
		UICanvas uicanvas = new UICanvas(this.m_confirm, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetVerticalAlign(1f);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get((!PsUIPopupConfirmSyncAccount.m_selectedData.m_cloud) ? StringID.CLOUD_SAVE_BUTTON_CURRENT : StringID.CLOUD_SAVE_BUTTON_CLOUD), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UIVerticalList uiverticalList2 = new UIVerticalList(this.m_confirm, string.Empty);
		uiverticalList2.RemoveDrawHandler();
		uiverticalList2.SetVerticalAlign(0.25f);
		uiverticalList2.SetHorizontalAlign(0.5f);
		uiverticalList2.SetSpacing(0.007f, RelativeTo.ScreenHeight);
		if (!string.IsNullOrEmpty(PsUIPopupConfirmSyncAccount.m_selectedData.m_name))
		{
			UICanvas uicanvas2 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
			uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas2.SetHeight(0.11f, RelativeTo.ParentHeight);
			uicanvas2.RemoveDrawHandler();
			uicanvas2.SetMargins(0f, 0f, -0.05f, 0.05f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(1f);
			new UIFittedText(uicanvas2, false, string.Empty, PsUIPopupConfirmSyncAccount.m_selectedData.m_name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		}
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList2, string.Empty);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		UISprite uisprite = new UISprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true);
		uisprite.SetSize(0.3f, uisprite.m_frame.height / uisprite.m_frame.width * 0.3f, RelativeTo.ParentWidth);
		new UIText(uihorizontalList2, false, string.Empty, PsUIPopupConfirmSyncAccount.m_selectedData.m_totalTrophies.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, null, null);
		UITextbox uitextbox = new UITextbox(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.CLOUD_COUNTER_LEVELS_COMPLETED), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMargins(0f, 0f, 0.02f, 0f, RelativeTo.ScreenHeight);
		new UIText(uiverticalList2, false, string.Empty, PsUIPopupConfirmSyncAccount.m_selectedData.m_levelsCompleted.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, null, null);
		UITextbox uitextbox2 = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get((!PsUIPopupConfirmSyncAccount.m_selectedData.m_cloud) ? StringID.CLOUD_CONFIRM_OVERRIDE_TEXT : StringID.CLOUD_CONFIRM_LOAD_TEXT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ParentHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox2.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x001333EC File Offset: 0x001317EC
	public void ItemHolderDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x00133478 File Offset: 0x00131878
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.075f, 0.075f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.CLOUD_CONFIRM_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x001334F4 File Offset: 0x001318F4
	public override void Step()
	{
		if (this.m_confirm.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Confirm");
			(this.GetRoot() as PsUIBasePopup).Destroy();
		}
		else if (this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Cancel");
			(this.GetRoot() as PsUIBasePopup).Destroy();
		}
		base.Step();
	}

	// Token: 0x04001DE4 RID: 7652
	private UIRectSpriteButton m_confirm;

	// Token: 0x04001DE5 RID: 7653
	public string m_serviceName;

	// Token: 0x04001DE6 RID: 7654
	public static SyncAccountData m_selectedData;
}
