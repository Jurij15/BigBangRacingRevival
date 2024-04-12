using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class PsUITabbedJoinedTeam : PsUITabbedTeam
{
	// Token: 0x060019C0 RID: 6592 RVA: 0x0011B552 File Offset: 0x00119952
	public PsUITabbedJoinedTeam(UIComponent _parent)
		: base(_parent)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.LightShadowBackground));
		this.SetWidth(1f, RelativeTo.ScreenWidth);
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x0011B590 File Offset: 0x00119990
	protected override Dictionary<string, Type> SetTabs()
	{
		Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
		dictionary.Add(PsStrings.Get(StringID.TEAMS_TAB_MY_TEAM), typeof(PsUICenterMyTeam));
		dictionary.Add(PsStrings.Get(StringID.EDITOR_TAB_SEARCH), typeof(PsUICenterSearchTeams));
		dictionary.Add(PsStrings.Get(StringID.TEAMS_TAB_TOP_TEAMS), typeof(PsUICenterTopTeams));
		dictionary.Add(PsStrings.Get(StringID.TEAMS_TAB_TOP_PLAYERS), typeof(PsUICenterPlayerLeaderboards));
		return dictionary;
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x0011B60C File Offset: 0x00119A0C
	protected override Dictionary<string, Dictionary<string, Type>> SetSubTabs()
	{
		Dictionary<string, Dictionary<string, Type>> dictionary = base.SetSubTabs();
		Dictionary<string, Type> dictionary2 = new Dictionary<string, Type>();
		dictionary2.Add(PsStrings.Get(StringID.TEAMS_BUTTON_MEMBERS), typeof(PsUICenterMyTeam));
		dictionary2.Add(PsStrings.Get(StringID.TEAMS_BUTTON_CHAT), typeof(PsUICenterTeamChat));
		dictionary.Add(PsStrings.Get(StringID.TEAMS_TAB_MY_TEAM), dictionary2);
		return dictionary;
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x0011B66C File Offset: 0x00119A6C
	protected override void CreateSubTabs(UIComponent _parent)
	{
		Dictionary<string, Type> dictionary = this.m_subTabDict[this.m_selectedKey];
		foreach (KeyValuePair<string, Type> keyValuePair in dictionary)
		{
			bool flag = keyValuePair.Key == this.m_selectedSubKey;
			PsUIGenericButton psUIGenericButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.0025f, "Button");
			psUIGenericButton.m_fieldName = keyValuePair.Key;
			psUIGenericButton.SetHeight(1f, RelativeTo.ParentHeight);
			psUIGenericButton.SetSubTabBlueColors(!flag);
			if (flag)
			{
				psUIGenericButton.DisableTouchAreas(true);
			}
			psUIGenericButton.SetText(keyValuePair.Key, 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_subTabs.Add(psUIGenericButton);
			if (keyValuePair.Key == PsStrings.Get(StringID.TEAMS_BUTTON_CHAT) && PlayerPrefsX.GetNewComments() > 0 && !string.IsNullOrEmpty(PlayerPrefsX.GetTeamId()))
			{
				this.m_notificationBase = new UICanvas(psUIGenericButton, false, "notification", null, string.Empty);
				this.m_notificationBase.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
				this.m_notificationBase.SetMargins(0.04f, -0.04f, -0.03f, 0.03f, RelativeTo.ScreenHeight);
				this.m_notificationBase.SetRogue();
				this.m_notificationBase.SetAlign(1f, 1f);
				this.m_notificationBase.SetDepthOffset(-10f);
				this.m_notificationBase.RemoveDrawHandler();
				UICanvas uicanvas = new UICanvas(this.m_notificationBase, false, string.Empty, null, string.Empty);
				uicanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
				uicanvas.SetMargins(0.15f, RelativeTo.OwnHeight);
				uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.NotificationDrawhandler));
				TweenC tweenC = TweenS.AddTransformTween(uicanvas.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
				TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
				UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PlayerPrefsX.GetNewComments().ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			}
		}
	}

	// Token: 0x060019C4 RID: 6596 RVA: 0x0011B8E8 File Offset: 0x00119CE8
	public void DestroyNotification()
	{
		if (this.m_notificationBase != null)
		{
			this.m_notificationBase.Destroy();
		}
		this.m_notificationBase = null;
	}

	// Token: 0x04001C2F RID: 7215
	private UICanvas m_notificationBase;
}
