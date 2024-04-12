using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class PsUICenterGooglePlay : PsUIHeaderedCanvas
{
	// Token: 0x06001890 RID: 6288 RVA: 0x0010B2D0 File Offset: 0x001096D0
	public PsUICenterGooglePlay(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.6f, RelativeTo.ScreenWidth);
		this.SetHeight(0.6f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.05f, 0.05f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x0010B3CC File Offset: 0x001097CC
	public void CreateHeaderContent(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.PLAY_NAME), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x0010B44C File Offset: 0x0010984C
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x0010B4CC File Offset: 0x001098CC
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(0.4f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ParentWidth);
		uiverticalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList2.SetHorizontalAlign(0f);
		uiverticalList2.RemoveDrawHandler();
		string text = string.Empty;
		string text2 = "ffffff";
		if (PlayGamesPlatform.Instance.IsAuthenticated())
		{
			text = PsStrings.Get(StringID.PLAY_SIGNED_IN);
		}
		else
		{
			text = PsStrings.Get(StringID.PLAY_NOT_SIGNED_IN);
			text2 = "aeaeae";
		}
		UIVerticalList uiverticalList3 = uiverticalList2;
		bool flag = false;
		string empty = string.Empty;
		string text3 = text;
		string font = PsFontManager.GetFont(PsFonts.KGSecondChances);
		float num = 0.03f;
		RelativeTo relativeTo = RelativeTo.ScreenHeight;
		bool flag2 = true;
		string text4 = text2;
		this.m_signText = new UITextbox(uiverticalList3, flag, empty, text3, font, num, relativeTo, flag2, Align.Left, Align.Top, text4, true, "313131");
		this.m_signText.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_signText.SetHorizontalAlign(0f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList2, string.Empty);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.RemoveDrawHandler();
		this.m_sign = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_sign.SetGreenColors(true);
		this.m_sign.SetIcon("menu_games_controller", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		Action action;
		if (!PlayGamesPlatform.Instance.IsAuthenticated())
		{
			action = delegate
			{
				GooglePlayManager.Login(new Action<bool>(this.SignedIn));
			};
		}
		else
		{
			action = delegate
			{
				GooglePlayManager.Logout();
				this.SignedOut();
			};
		}
		this.m_sign.SetReleaseAction(action);
		string text5 = string.Empty;
		if (PlayGamesPlatform.Instance.IsAuthenticated())
		{
			text5 = PsStrings.Get(StringID.PLAY_SIGN_OUT_BUTTON);
		}
		else
		{
			text5 = PsStrings.Get(StringID.PLAY_SIGN_IN_BUTTON);
		}
		this.m_signInfo = new UITextbox(uihorizontalList, false, string.Empty, text5, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, true, Align.Left, Align.Top, "ffffff", true, "313131");
		this.m_signInfo.SetWidth(0.25f, RelativeTo.ScreenWidth);
		this.m_signInfo.SetHorizontalAlign(0f);
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList2.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetHorizontalAlign(0f);
		uihorizontalList2.RemoveDrawHandler();
		this.m_achievements = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_achievements.SetIcon("menu_games_achievements", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		if (!PlayGamesPlatform.Instance.IsAuthenticated())
		{
			this.m_achievements.SetGrayColors();
			this.m_achievements.DisableTouchAreas(true);
		}
		UITextbox uitextbox = new UITextbox(uihorizontalList2, false, string.Empty, PsStrings.Get(StringID.PLAY_ACHIEVEMENTS), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, true, Align.Left, Align.Top, "ffffff", true, "313131");
		uitextbox.SetWidth(0.25f, RelativeTo.ScreenWidth);
		uitextbox.SetHorizontalAlign(0f);
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x0010B818 File Offset: 0x00109C18
	public void SignedIn(bool _success)
	{
		if (_success)
		{
			this.m_achievements.SetGreenColors(true);
			this.m_achievements.EnableTouchAreas(true);
			this.m_signInfo.SetText(PsStrings.Get(StringID.PLAY_SIGN_OUT_BUTTON));
			this.m_signText.SetText(PsStrings.Get(StringID.PLAY_SIGNED_IN));
			this.m_signText.SetColor("ffffff", "313131");
			Action action = delegate
			{
				GooglePlayManager.Logout();
				this.SignedOut();
			};
			this.m_sign.SetReleaseAction(action);
			this.Update();
		}
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x0010B8A4 File Offset: 0x00109CA4
	public void SignedOut()
	{
		this.m_achievements.SetGrayColors();
		this.m_achievements.DisableTouchAreas(true);
		this.m_signInfo.SetText(PsStrings.Get(StringID.PLAY_SIGN_IN_BUTTON));
		this.m_signText.SetText(PsStrings.Get(StringID.PLAY_NOT_SIGNED_IN));
		this.m_signText.SetColor("aeaeae", "313131");
		Action action = delegate
		{
			GooglePlayManager.Login(new Action<bool>(this.SignedIn));
		};
		this.m_sign.SetReleaseAction(action);
		this.Update();
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x0010B928 File Offset: 0x00109D28
	public override void Step()
	{
		if (this.m_achievements.m_hit)
		{
			PlayerPrefsX.SetGPGSSignedOut(true);
			PlayGamesPlatform.Instance.ShowAchievementsUI(delegate(UIStatus c)
			{
				if (c != UIStatus.NotAuthorized)
				{
					PlayerPrefsX.SetGPGSSignedOut(false);
				}
				else
				{
					this.SignedOut();
				}
			});
		}
		base.Step();
	}

	// Token: 0x04001B3C RID: 6972
	private PsUIGenericButton m_sign;

	// Token: 0x04001B3D RID: 6973
	private PsUIGenericButton m_achievements;

	// Token: 0x04001B3E RID: 6974
	private UITextbox m_signText;

	// Token: 0x04001B3F RID: 6975
	private UITextbox m_signInfo;
}
