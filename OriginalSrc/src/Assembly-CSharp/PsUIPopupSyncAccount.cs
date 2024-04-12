using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003CB RID: 971
public class PsUIPopupSyncAccount : PsUIHeaderedCanvas
{
	// Token: 0x06001B7E RID: 7038 RVA: 0x001325EC File Offset: 0x001309EC
	public PsUIPopupSyncAccount(UIComponent _parent, string _serviceName, bool _hasCloseButton = true)
		: base(_parent, string.Empty, _hasCloseButton, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.85f, RelativeTo.ScreenHeight);
		this.SetHeight(0.65f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.45f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.CLOUD_SAVE_FOUND_TEXT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ParentHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		this.m_current = new UIRectSpriteButton(uihorizontalList, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), true, false);
		this.m_current.SetWidth(0.45f, RelativeTo.ParentHeight);
		this.m_current.SetHeight(this.m_current.m_frame.height / this.m_current.m_frame.width * 0.45f, RelativeTo.ParentHeight);
		this.m_current.SetMargins(0.08f, 0.08f, 0.06f, 0.04f, RelativeTo.OwnWidth);
		UICanvas uicanvas = new UICanvas(this.m_current, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetVerticalAlign(1f);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.CLOUD_SAVE_BUTTON_CURRENT), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UIVerticalList uiverticalList2 = new UIVerticalList(this.m_current, string.Empty);
		uiverticalList2.RemoveDrawHandler();
		uiverticalList2.SetVerticalAlign(0.25f);
		uiverticalList2.SetHorizontalAlign(0.5f);
		uiverticalList2.SetSpacing(0.007f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList2, string.Empty);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		UISprite uisprite = new UISprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true);
		uisprite.SetSize(0.3f, uisprite.m_frame.height / uisprite.m_frame.width * 0.3f, RelativeTo.ParentWidth);
		new UIText(uihorizontalList2, false, string.Empty, PsUIPopupSyncAccount.m_currentData.m_totalTrophies.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, null, null);
		UITextbox uitextbox2 = new UITextbox(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.CLOUD_COUNTER_LEVELS_COMPLETED), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox2.SetMargins(0f, 0f, 0.02f, 0f, RelativeTo.ScreenHeight);
		new UIText(uiverticalList2, false, string.Empty, PsUIPopupSyncAccount.m_currentData.m_levelsCompleted.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, null, null);
		this.m_cloudButtons = new List<PsUIPopupSyncAccount.PsUISyncButton>();
		for (int i = 0; i < PsUIPopupSyncAccount.m_cloudData.Count; i++)
		{
			this.m_cloudButtons.Add(this.CreateCloudDatabutton(PsUIPopupSyncAccount.m_cloudData[i], uihorizontalList));
		}
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x00132A14 File Offset: 0x00130E14
	protected virtual PsUIPopupSyncAccount.PsUISyncButton CreateCloudDatabutton(SyncAccountData _data, UIComponent _parent)
	{
		PsUIPopupSyncAccount.PsUISyncButton psUISyncButton = new PsUIPopupSyncAccount.PsUISyncButton(_parent, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null), _data);
		psUISyncButton.SetWidth(0.45f, RelativeTo.ParentHeight);
		psUISyncButton.SetHeight(psUISyncButton.m_frame.height / psUISyncButton.m_frame.width * 0.45f, RelativeTo.ParentHeight);
		psUISyncButton.SetMargins(0.08f, 0.08f, 0.06f, 0.04f, RelativeTo.OwnWidth);
		UICanvas uicanvas = new UICanvas(psUISyncButton, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.13f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetVerticalAlign(1f);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.CLOUD_SAVE_BUTTON_CLOUD), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		uifittedText.SetShadowShift(new Vector2(0f, -0.8f), 0.1f);
		UIVerticalList uiverticalList = new UIVerticalList(psUISyncButton, string.Empty);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetVerticalAlign(0.25f);
		uiverticalList.SetHorizontalAlign(0.5f);
		uiverticalList.SetSpacing(0.007f, RelativeTo.ScreenHeight);
		if (!string.IsNullOrEmpty(_data.m_name))
		{
			UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas2.SetHeight(0.11f, RelativeTo.ParentHeight);
			uicanvas2.RemoveDrawHandler();
			uicanvas2.SetMargins(0f, 0f, -0.05f, 0.05f, RelativeTo.ParentHeight);
			uicanvas2.SetVerticalAlign(1f);
			new UIFittedText(uicanvas2, false, string.Empty, _data.m_name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", "#000000");
		}
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		UISprite uisprite = new UISprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_league_trophy_icon", null), true);
		uisprite.SetSize(0.3f, uisprite.m_frame.height / uisprite.m_frame.width * 0.3f, RelativeTo.ParentWidth);
		new UIText(uihorizontalList, false, string.Empty, _data.m_totalTrophies.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, null, null);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.CLOUD_COUNTER_LEVELS_COMPLETED), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMargins(0f, 0f, 0.02f, 0f, RelativeTo.ScreenHeight);
		new UIText(uiverticalList, false, string.Empty, _data.m_levelsCompleted.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, null, null);
		return psUISyncButton;
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x00132CF0 File Offset: 0x001310F0
	public void ItemHolderDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_shop_item_card", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x00132D7C File Offset: 0x0013117C
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.075f, 0.075f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.CLOUD_SAVE_FOUND_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x00132DF8 File Offset: 0x001311F8
	public override void Step()
	{
		bool flag = false;
		foreach (PsUIPopupSyncAccount.PsUISyncButton psUISyncButton in this.m_cloudButtons)
		{
			if (psUISyncButton.m_hit)
			{
				PsUIPopupConfirmSyncAccount.m_selectedData = psUISyncButton.m_data;
				flag = true;
				(this.GetRoot() as PsUIBasePopup).CallAction("Cloud");
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
		}
		if (!flag)
		{
			if (this.m_current.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Current");
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
			else if (this.m_exitButton != null && this.m_exitButton.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Cancel");
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
		}
		base.Step();
	}

	// Token: 0x04001DDB RID: 7643
	public static SyncAccountData m_currentData;

	// Token: 0x04001DDC RID: 7644
	public static List<SyncAccountData> m_cloudData;

	// Token: 0x04001DDD RID: 7645
	private UIRectSpriteButton m_current;

	// Token: 0x04001DDE RID: 7646
	private List<PsUIPopupSyncAccount.PsUISyncButton> m_cloudButtons;

	// Token: 0x04001DDF RID: 7647
	private UIRectSpriteButton m_cancel;

	// Token: 0x04001DE0 RID: 7648
	public string m_serviceName;

	// Token: 0x020003CC RID: 972
	protected class PsUISyncButton : UIRectSpriteButton
	{
		// Token: 0x06001B83 RID: 7043 RVA: 0x00132F18 File Offset: 0x00131318
		public PsUISyncButton(UIComponent _parent, string _tag, SpriteSheet _spriteSheet, Frame _frame, SyncAccountData _data)
			: base(_parent, _tag, _spriteSheet, _frame, true, false)
		{
			this.m_data = _data;
		}

		// Token: 0x04001DE3 RID: 7651
		public SyncAccountData m_data;
	}
}
