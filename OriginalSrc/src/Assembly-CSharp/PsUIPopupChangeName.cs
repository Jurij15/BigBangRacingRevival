using System;
using UnityEngine;

// Token: 0x020003C4 RID: 964
public class PsUIPopupChangeName : PsUIHeaderedCanvas
{
	// Token: 0x06001B71 RID: 7025 RVA: 0x00131F34 File Offset: 0x00130334
	public PsUIPopupChangeName(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		Debug.LogWarning("Change name");
		(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.m_changeEnabled = true;
		this.m_fieldName = "ChangeName";
		this.m_nameModel = new UIModel(this, null);
		this.m_userName = PlayerPrefsX.GetUserName();
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x00132074 File Offset: 0x00130474
	public void CreateUI(bool login = false)
	{
		this.m_login = login;
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_header, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, "What's your name?", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
		this.m_playerNameField = new PsUIPlayerNameField(this);
		this.m_playerNameField.SetText(PlayerPrefsX.GetUserName());
		this.m_playerNameField.SetCallbacks(new Action<string>(this.ChangeName), null);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this, "nameChangeButtons");
		uihorizontalList2.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetVerticalAlign(0f);
		uihorizontalList2.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_changeButton = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_changeButton.SetText("Ok", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_changeButton.SetGreenColors(true);
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x001321B8 File Offset: 0x001305B8
	private void ChangeName(string _name)
	{
		if (!_name.Equals(PlayerPrefsX.GetUserName()))
		{
			this.m_userName = _name;
			if (this.m_changeButton != null && (string.IsNullOrEmpty(this.m_userName) || this.m_userName.Length < 3) && this.m_changeEnabled)
			{
				this.m_changeEnabled = false;
				this.m_changeButton.SetGreenColors(false);
				this.m_changeButton.DisableTouchAreas(true);
				this.m_changeButton.Update();
			}
			else if (this.m_changeButton != null && !string.IsNullOrEmpty(this.m_userName) && this.m_userName.Length >= 3 && !this.m_changeEnabled)
			{
				this.m_changeEnabled = true;
				this.m_changeButton.SetGreenColors(true);
				this.m_changeButton.EnableTouchAreas(true);
				this.m_changeButton.Update();
			}
		}
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x001322A4 File Offset: 0x001306A4
	public override void Step()
	{
		if (this.m_changeButton != null && this.m_changeButton.m_hit)
		{
			if (!this.m_userName.Equals(PlayerPrefsX.GetUserName()) || this.m_login)
			{
				PlayerPrefsX.SetUserName(this.m_userName);
				(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
			else
			{
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
		}
		base.Step();
	}

	// Token: 0x04001DCD RID: 7629
	public string m_userName = string.Empty;

	// Token: 0x04001DCE RID: 7630
	public UIModel m_nameModel;

	// Token: 0x04001DCF RID: 7631
	private PsUIPlayerNameField m_playerNameField;

	// Token: 0x04001DD0 RID: 7632
	private PsUIGenericButton m_changeButton;

	// Token: 0x04001DD1 RID: 7633
	private bool m_changeEnabled;

	// Token: 0x04001DD2 RID: 7634
	private bool m_login;

	// Token: 0x020003C5 RID: 965
	private class UINameTextArea : UITextArea
	{
		// Token: 0x06001B75 RID: 7029 RVA: 0x00132338 File Offset: 0x00130738
		public UINameTextArea(UIComponent _parent, string _tag, string _label, string _tip, Camera _camera, UIModel _model, string _STRINGfieldName, int rows = -1, int _maxCharacterCount = 128)
			: base(_parent, _tag, _label, _tip, _camera, _model, _STRINGfieldName, rows, _maxCharacterCount, "464646", "464646")
		{
			this.m_labelText = (string)this.GetValue();
		}
	}
}
