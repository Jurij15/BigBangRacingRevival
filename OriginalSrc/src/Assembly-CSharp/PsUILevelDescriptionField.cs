using System;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class PsUILevelDescriptionField : PsUIInputTextField
{
	// Token: 0x0600128B RID: 4747 RVA: 0x000B723E File Offset: 0x000B563E
	public PsUILevelDescriptionField()
		: base(null)
	{
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x000B7247 File Offset: 0x000B5647
	public PsUILevelDescriptionField(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x000B7250 File Offset: 0x000B5650
	protected override void ConstructUI()
	{
		base.SetMinMaxCharacterCount(2, 80);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetWidth(0.56f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.EDITOR_PUBLISH_DESCRIPTION), PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, null, null);
		uitext.SetColor("#a8e2ff", null);
		uitext.SetMargins(0.03f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uitext.SetHorizontalAlign(0f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetCharacterCustomisationData().GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		string text = null;
		if (installedItemByCategory != null)
		{
			text = installedItemByCategory.m_identifier;
		}
		UIHorizontalList uihorizontalList2 = uihorizontalList;
		bool flag = false;
		string text2 = "ProfileImage";
		string facebookId = PlayerPrefsX.GetFacebookId();
		string gameCenterId = PlayerPrefsX.GetGameCenterId();
		string text3 = text;
		this.m_profileImage = new PsUIProfileImage(uihorizontalList2, flag, text2, facebookId, gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", text3, true, true);
		this.m_profileImage.SetSize(0.08f, 0.08f, RelativeTo.ScreenHeight);
		TransformS.SetRotation(this.m_profileImage.m_TC, Vector3.forward * 6f);
		this.m_profileImage.SetAlign(0.5f, 1f);
		UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList2.SetWidth(0.8f, RelativeTo.ParentWidth);
		uiverticalList2.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, PlayerPrefsX.GetUserName(), PsFontManager.GetFont(PsFonts.HurmeBold), 0.035f, RelativeTo.ScreenHeight, "#fffec6", null);
		uitext2.SetHorizontalAlign(0f);
		UITextbox uitextbox = new UITextbox(uiverticalList2, true, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.025f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetMargins(0.01f, 0.01f, 0.005f, 0.015f, RelativeTo.ScreenHeight);
		uitextbox.m_tmc.m_textMesh.color = DebugDraw.HexToColor("#33a8d4");
		uitextbox.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.DescriptionField));
		uitextbox.SetMinRows(3);
		uitextbox.SetMaxRows(3);
		base.SetTextField(uitextbox);
	}

	// Token: 0x040015B8 RID: 5560
	private PsUIProfileImage m_profileImage;
}
