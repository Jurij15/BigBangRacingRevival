using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003DD RID: 989
public class PsUIHiddenHatPopup : PsUIHeaderedCanvas
{
	// Token: 0x06001C23 RID: 7203 RVA: 0x0013D6A4 File Offset: 0x0013BAA4
	public PsUIHiddenHatPopup(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		PsUIHiddenHatPopup.m_popupCount++;
		this.SetWidth(0.87f, RelativeTo.ScreenHeight);
		this.SetHeight(0.65f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_header, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIComponent uicomponent = new UIComponent(uihorizontalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(0.06f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicomponent, false, string.Empty, PsStrings.Get(StringID.HAT_FOUND_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", "#000000");
	}

	// Token: 0x06001C24 RID: 7204 RVA: 0x0013D818 File Offset: 0x0013BC18
	public void CreateContent(PsCustomisationItem _item, string _playerName)
	{
		this.RemoveTouchAreas();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.2f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.2f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_item.m_iconName, null), true, true);
		UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList2.SetVerticalAlign(1f);
		uiverticalList2.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList2, false, string.Empty, PsStrings.Get(_item.m_title), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.036f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetHorizontalAlign(0f);
		uitextbox.SetWidth(0.65f, RelativeTo.ParentWidth);
		UIText uitext = new UIText(uiverticalList2, false, string.Empty, "<color=#f387ff>" + PsStrings.Get(StringID.HAT_TYPE_HIDDEN_NAME).ToUpper() + "</color>", PsFontManager.GetFont(PsFonts.HurmeBold), 0.031f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(0f);
		string text = PsStrings.Get(StringID.HAT_FOUND_REASON);
		text = text.Replace("%1", _playerName);
		UITextbox uitextbox2 = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.039f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox2.SetWidth(0.88f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this, string.Empty);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetAlign(0.5f, 0f);
		uihorizontalList2.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_okButton = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_okButton.SetText(PsStrings.Get(StringID.DIALOGUE_RACING_WELCOME_BUTTON).ToUpper(), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_okButton.SetGreenColors(true);
		this.m_okButton.SetVerticalAlign(0f);
		this.m_container.Update();
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x0013DA84 File Offset: 0x0013BE84
	public override void Destroy()
	{
		PsUIHiddenHatPopup.m_popupCount--;
		base.Destroy();
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x0013DA98 File Offset: 0x0013BE98
	public override void Step()
	{
		if (this.m_okButton != null && this.m_okButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x0013DAD4 File Offset: 0x0013BED4
	public static void CreatePopups(string _playerName, List<PsCustomisationItem> _hats, Action _callback)
	{
		if (_hats == null || _hats.Count == 0)
		{
			return;
		}
		PsCustomisationItem psCustomisationItem = _hats[0];
		_hats.Remove(psCustomisationItem);
		if (psCustomisationItem != null && psCustomisationItem.m_category == PsCustomisationManager.CustomisationCategory.HAT)
		{
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUIHiddenHatPopup), null, null, null, false, true, InitialPage.Center, false, true, false);
			(popup.m_mainContent as PsUIHiddenHatPopup).CreateContent(psCustomisationItem, _playerName);
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
				if (_hats != null && _hats.Count > 0)
				{
					PsUIHiddenHatPopup.CreatePopups(_playerName, _hats, _callback);
				}
				else
				{
					if (_callback != null)
					{
						_callback.Invoke();
					}
					if (PsUIHiddenHatPopup.m_closeLastPopupCallback != null)
					{
						PsUIHiddenHatPopup.m_closeLastPopupCallback.Invoke();
						PsUIHiddenHatPopup.m_closeLastPopupCallback = null;
					}
				}
			});
			TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else
		{
			PsUIHiddenHatPopup.CreatePopups(_playerName, _hats, _callback);
		}
	}

	// Token: 0x04001E22 RID: 7714
	private PsUIGenericButton m_okButton;

	// Token: 0x04001E23 RID: 7715
	public static int m_popupCount;

	// Token: 0x04001E24 RID: 7716
	public static Action m_closeLastPopupCallback;
}
