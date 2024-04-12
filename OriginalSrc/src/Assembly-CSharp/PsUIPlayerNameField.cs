using System;

// Token: 0x0200026E RID: 622
public class PsUIPlayerNameField : PsUIInputTextField
{
	// Token: 0x06001295 RID: 4757 RVA: 0x000B7715 File Offset: 0x000B5B15
	public PsUIPlayerNameField()
		: this(null)
	{
	}

	// Token: 0x06001296 RID: 4758 RVA: 0x000B771E File Offset: 0x000B5B1E
	public PsUIPlayerNameField(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x000B7728 File Offset: 0x000B5B28
	protected override void ConstructUI()
	{
		base.SetMinMaxCharacterCount(3, 16);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetCharacterCustomisationData().GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		string text = null;
		if (installedItemByCategory != null)
		{
			text = installedItemByCategory.m_identifier;
		}
		UIHorizontalList uihorizontalList2 = uihorizontalList;
		bool flag = false;
		string text2 = "NameFieldProfile";
		string facebookId = PlayerPrefsX.GetFacebookId();
		string gameCenterId = PlayerPrefsX.GetGameCenterId();
		string text3 = text;
		this.m_profileImage = new PsUIProfileImage(uihorizontalList2, flag, text2, facebookId, gameCenterId, -1, true, false, true, 0.1f, 0.06f, "fff9e6", text3, true, true);
		this.m_profileImage.SetSize(0.135f, 0.135f, RelativeTo.ScreenHeight);
		this.m_profileImage.SetAlign(0f, 0f);
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetWidth(0.6f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		this.m_title = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.PROMPT_NAME_CHANGE_HEADER), PsFontManager.GetFont(PsFonts.HurmeBold), 0.045f, RelativeTo.ScreenHeight, null, null);
		this.m_title.SetColor("#60caf5", null);
		this.m_title.SetHorizontalAlign(0.07f);
		UICanvas uicanvas = new UICanvas(uiverticalList, true, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.6f, 0.09f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.02f, 0.02f, 0.015f, 0.015f, RelativeTo.ScreenHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldDark));
		UIFittedText uifittedText = new UIFittedText(uicanvas, true, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#b1f92a", null);
		uifittedText.SetHorizontalAlign(0f);
		base.SetTextField(uifittedText);
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x000B7900 File Offset: 0x000B5D00
	public void UpdateProfileImage()
	{
		this.m_profileImage.m_facebookId = PlayerPrefsX.GetFacebookId();
		this.m_profileImage.LoadPicture();
		this.m_profileImage.Update();
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x000B7928 File Offset: 0x000B5D28
	public void ChangeTitleColor(string _color = "fff7e8")
	{
		this.m_title.SetColor(_color, null);
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x000B7937 File Offset: 0x000B5D37
	public void ChangeTitleText(string _text)
	{
		this.m_title.SetText(_text);
	}

	// Token: 0x040015BC RID: 5564
	private PsUIProfileImage m_profileImage;

	// Token: 0x040015BD RID: 5565
	private UIText m_title;
}
