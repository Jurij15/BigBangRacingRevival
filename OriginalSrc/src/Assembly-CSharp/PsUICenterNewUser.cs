using System;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class PsUICenterNewUser : UICanvas
{
	// Token: 0x0600103D RID: 4157 RVA: 0x000970F4 File Offset: 0x000954F4
	public PsUICenterNewUser(UIComponent _parent)
		: base(_parent, false, "NewUser", null, string.Empty)
	{
		this.spriteTC = TransformS.AddComponent(EntityManager.AddEntity());
		this.spriteTC.transform.position = new Vector3(0f, 0f, 26f);
		Material material = new Material(Shader.Find("WOE/Unlit/UnlitTransparentBg"));
		this.m_darkenSpriteSheet = SpriteS.AddSpriteSheet(this.m_camera, material, 1f);
		SpriteC spriteC = SpriteS.AddComponent(this.spriteTC, new Frame(0f, 0f, (float)Screen.width, (float)Screen.height), this.m_darkenSpriteSheet);
		SpriteS.SetColor(spriteC, new Color(0f, 0f, 0f, 0f));
		TweenS.AddTransformTween(this.spriteTC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.zero, new Vector3(0.5f, 0.5f, 0.5f), 0.2f, 0f, true);
		this.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetMargins(0.02f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0f, 1f);
		uicanvas.RemoveDrawHandler();
		this.m_backButton = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
		this.m_backButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_backButton.SetSound("/UI/ButtonBack");
		this.m_backButton.SetOrangeColors(true);
		this.m_backButton.SetAlign(0f, 1f);
		this.m_backButton.SetDepthOffset(-10f);
		this.m_backButton.SetReleaseAction(new Action(this.Back));
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetVerticalAlign(0.7f);
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		this.m_playerNameField = new PsUIPlayerNameField(uiverticalList);
		this.m_playerNameField.SetHorizontalAlign(0.5f);
		this.m_playerNameField.SetCallbacks(new Action<string>(this.DoneWriting), null);
		if (this.m_playerNameField.GetText().Length < 3)
		{
			this.m_playerNameField.ChangeTitleColor("fff7e8");
		}
		bool flag = true;
		if (flag)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, "fbList");
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetSpacing(0.06f, RelativeTo.ScreenHeight);
			UIVerticalList uiverticalList2 = new UIVerticalList(uihorizontalList, string.Empty);
			uiverticalList2.RemoveDrawHandler();
			uiverticalList2.SetWidth(0.6f, RelativeTo.ScreenHeight);
			string text = PsStrings.Get(StringID.NAME_POPUP_FACEBOOK_PROMPT);
			if (this.m_alternativeBackUpEnabled)
			{
				text = PsStrings.Get(StringID.SOCIAL_CONNECT_FB);
			}
			this.m_facebookInfo = new PsUIInfoBar(uiverticalList2, text, false);
			this.m_facebookInfo.SetDepthOffset(-15f);
			this.m_facebookButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.003f, "Button");
			this.m_facebookButton.SetIcon("menu_icon_facebook", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_facebookButton.SetText(PsStrings.Get(StringID.FACEBOOK_BUTTON_CONNECT), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_facebookButton.SetDepthOffset(-30f);
			this.m_facebookButton.SetHorizontalAlign(0f);
		}
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x00097468 File Offset: 0x00095868
	private void CheckBackups()
	{
		PsPersistentData.CheckBackups(null);
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x00097470 File Offset: 0x00095870
	private void DoneWriting(string _name)
	{
		this.m_playerNameField.ChangeTitleColor("60caf5");
		this.m_nameSet = false;
		this.CreateDoneButton();
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x00097490 File Offset: 0x00095890
	private void CreateDoneButton()
	{
		if (this.m_doneButton != null)
		{
			return;
		}
		this.m_doneButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_doneButton.SetGreenColors(true);
		this.m_doneButton.SetText(PsStrings.Get(StringID.FACEBOOK_BUTTON_DONE), 0.07f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_doneButton.SetAlign(0.5f, 0.15f);
		this.m_doneButton.Update();
		TweenS.AddTransformTween(this.m_doneButton.m_TC, TweenedProperty.Scale, TweenStyle.ExpoInOut, Vector3.zero, Vector3.one, 0.4f, 0f, true);
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x00097540 File Offset: 0x00095940
	private void RemoveDoneButton()
	{
		if (this.m_doneButton == null)
		{
			return;
		}
		this.m_doneButton.Destroy();
		this.m_doneButton = null;
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x00097560 File Offset: 0x00095960
	public override void Step()
	{
		if (!this.m_openedAutoInput)
		{
			UIComponent textField = this.m_playerNameField.m_textField;
			if (textField != null)
			{
				this.m_playerNameField.m_textField.m_hit = true;
				this.m_openedAutoInput = true;
			}
		}
		if (this.m_doneButton != null && this.m_doneButton.m_hit)
		{
			this.DoneButtonPressed();
		}
		else if (this.m_facebookButton != null && this.m_facebookButton.m_hit)
		{
			this.m_waitingPopup = new PsUIWaitPopup(null);
			FacebookManager.Login(new Action(this.FacebookLoginDone));
		}
		base.Step();
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x00097606 File Offset: 0x00095A06
	private void DoneButtonPressed()
	{
		if (!this.m_nameSet)
		{
			this.ChangeName(this.m_playerNameField.GetText(), delegate
			{
				this.Exit();
			});
		}
		else
		{
			this.Exit();
		}
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x0009763B File Offset: 0x00095A3B
	public void Exit()
	{
		PlayerPrefsX.SetNameChangesCount(PlayerPrefsX.GetNameChangesCount() + 1);
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x0009765F File Offset: 0x00095A5F
	public void Back()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Back");
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x00097678 File Offset: 0x00095A78
	private void FacebookLoginDone()
	{
		if (!string.IsNullOrEmpty(PlayerPrefsX.GetFacebookId()))
		{
			this.m_playerNameField.UpdateProfileImage();
			this.m_facebookButton.Destroy();
			this.m_facebookInfo.Destroy();
			string facebookName = PlayerPrefsX.GetFacebookName();
			if (string.IsNullOrEmpty(this.m_playerNameField.GetText()) && !string.IsNullOrEmpty(facebookName))
			{
				this.m_playerNameField.SetText(facebookName);
				this.ChangeName(facebookName, null);
			}
			else
			{
				this.DestroyWaitingPopup();
			}
		}
		else
		{
			this.DestroyWaitingPopup();
			if (!string.IsNullOrEmpty(this.m_playerNameField.GetText()))
			{
				this.CreateDoneButton();
			}
		}
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x00097720 File Offset: 0x00095B20
	private void ChangeName(string _name, Action customCallBack = null)
	{
		if (customCallBack == null)
		{
			customCallBack = delegate
			{
				Debug.Log("Name was changed", null);
			};
		}
		if (!_name.Equals(PlayerPrefsX.GetUserName()))
		{
			PlayerPrefsX.SetUserName(_name);
			new PsServerQueueFlow(null, delegate
			{
				PsServerRequest.ServerChangeName(_name, delegate(HttpC c)
				{
					this.NameChangeSUCCEED(c);
				}, new Action<HttpC>(this.NameChangeFAILED), null);
			}, null);
			customCallBack.Invoke();
		}
		else
		{
			this.m_nameSet = true;
			customCallBack.Invoke();
		}
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x000977B2 File Offset: 0x00095BB2
	private void DestroyWaitingPopup()
	{
		if (this.m_waitingPopup != null)
		{
			this.m_waitingPopup.Destroy();
			this.m_waitingPopup = null;
		}
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x000977D1 File Offset: 0x00095BD1
	private void NameChangeSUCCEED(HttpC _c)
	{
		this.CreateDoneButton();
		this.m_nameSet = true;
		Debug.Log("NAME CHANGE SUCCEED", null);
		PsAchievementManager.Complete("uniqueSnowflake");
		this.DestroyWaitingPopup();
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x000977FC File Offset: 0x00095BFC
	private void NameChangeFAILED(HttpC _c)
	{
		Debug.Log("NAME CHANGE FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, () => PsServerRequest.ServerChangeName(PlayerPrefsX.GetUserName(), new Action<HttpC>(this.NameChangeSUCCEED), new Action<HttpC>(this.NameChangeFAILED), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x00097848 File Offset: 0x00095C48
	public override void Destroy()
	{
		if (this.m_darkenSpriteSheet != null)
		{
			SpriteS.RemoveSpriteSheet(this.m_darkenSpriteSheet);
			this.m_darkenSpriteSheet = null;
		}
		if (this.spriteTC.p_entity != null)
		{
			EntityManager.RemoveEntity(this.spriteTC.p_entity);
		}
		base.Destroy();
	}

	// Token: 0x040012FE RID: 4862
	private PsUIPlayerNameField m_playerNameField;

	// Token: 0x040012FF RID: 4863
	private PsUIGenericButton m_backButton;

	// Token: 0x04001300 RID: 4864
	private PsUIGenericButton m_facebookButton;

	// Token: 0x04001301 RID: 4865
	private PsUIInfoBar m_facebookInfo;

	// Token: 0x04001302 RID: 4866
	private PsUIGenericButton m_doneButton;

	// Token: 0x04001303 RID: 4867
	private PsUIWaitPopup m_waitingPopup;

	// Token: 0x04001304 RID: 4868
	private bool m_nameSet;

	// Token: 0x04001305 RID: 4869
	private SpriteSheet m_darkenSpriteSheet;

	// Token: 0x04001306 RID: 4870
	private TransformC spriteTC;

	// Token: 0x04001307 RID: 4871
	private bool m_openedAutoInput = true;

	// Token: 0x04001308 RID: 4872
	private bool m_alternativeBackUpEnabled;
}
