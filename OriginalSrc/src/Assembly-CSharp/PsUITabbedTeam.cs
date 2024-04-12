using System;
using System.Collections.Generic;

// Token: 0x0200037B RID: 891
public class PsUITabbedTeam : PsUITabbedMenu
{
	// Token: 0x060019C5 RID: 6597 RVA: 0x0011B077 File Offset: 0x00119477
	public PsUITabbedTeam(UIComponent _parent)
		: base(_parent)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.LightShadowBackground));
		this.SetWidth(1f, RelativeTo.ScreenWidth);
	}

	// Token: 0x060019C6 RID: 6598 RVA: 0x0011B0B4 File Offset: 0x001194B4
	protected override Dictionary<string, Type> SetTabs()
	{
		Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
		dictionary.Add(PsStrings.Get(StringID.TEAM_TAB_TEAM_UP), typeof(PsUICenterTeamUp));
		dictionary.Add(PsStrings.Get(StringID.EDITOR_TAB_SEARCH), typeof(PsUICenterSearchTeams));
		dictionary.Add(PsStrings.Get(StringID.TEAMS_TAB_TOP_TEAMS), typeof(PsUICenterTopTeams));
		dictionary.Add(PsStrings.Get(StringID.TEAMS_TAB_TOP_PLAYERS), typeof(PsUICenterPlayerLeaderboards));
		return dictionary;
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x0011B130 File Offset: 0x00119530
	protected override string SetDefaultTab()
	{
		string text = null;
		int num = 1;
		foreach (string text2 in this.m_tabDict.Keys)
		{
			if (num == PsUITabbedTeam.m_selectedTab)
			{
				text = text2;
				break;
			}
			num++;
		}
		return text;
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x0011B1A8 File Offset: 0x001195A8
	protected override Dictionary<string, Dictionary<string, Type>> SetSubTabs()
	{
		Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
		dictionary.Add(PsStrings.Get(StringID.BOARD_GLOBAL), typeof(PsUICenterPlayerLeaderboards));
		dictionary.Add(PsStrings.Get(StringID.BOARD_LOCAL), typeof(PsUICenterPlayerLeaderboards));
		dictionary.Add(PsStrings.Get(StringID.BOARD_FRIENDS), typeof(PsUICenterPlayerLeaderboards));
		Dictionary<string, Dictionary<string, Type>> dictionary2 = new Dictionary<string, Dictionary<string, Type>>();
		dictionary2.Add(PsStrings.Get(StringID.TEAMS_TAB_TOP_PLAYERS), dictionary);
		return dictionary2;
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x0011B224 File Offset: 0x00119624
	protected override string SetDefaultSubTab()
	{
		if (this.m_subTabDict != null && this.m_subTabDict.ContainsKey(this.m_selectedKey))
		{
			string text = null;
			int num = 1;
			bool flag = false;
			foreach (string text2 in this.m_subTabDict[this.m_selectedKey].Keys)
			{
				if (num == PsUITabbedTeam.m_selectedSubTab)
				{
					text = text2;
					flag = true;
					break;
				}
				num++;
			}
			if (!flag)
			{
				using (Dictionary<string, Type>.KeyCollection.Enumerator enumerator2 = this.m_subTabDict[this.m_selectedKey].Keys.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						string text3 = enumerator2.Current;
						text = text3;
						PsUITabbedTeam.m_selectedSubTab = 1;
					}
				}
			}
			return text;
		}
		return string.Empty;
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x0011B33C File Offset: 0x0011973C
	public override void SelectSubTab(int _tab)
	{
		PsUITabbedTeam.m_selectedSubTab = _tab;
	}

	// Token: 0x060019CB RID: 6603 RVA: 0x0011B344 File Offset: 0x00119744
	protected override void CreateContent(UIComponent _parent, bool _update = false)
	{
		UIComponent uicomponent = null;
		if (this.m_currentContent != null && this.m_subTabDict.ContainsKey(this.m_selectedKey) && this.m_subTabDict[this.m_selectedKey][this.m_selectedSubKey] == typeof(PsUICenterPlayerLeaderboards) && this.m_currentContent.GetType() == this.m_subTabDict[this.m_selectedKey][this.m_selectedSubKey])
		{
			(this.m_currentContent as PsUICenterPlayerLeaderboards).ChangeBoard(PsUITabbedTeam.m_selectedSubTab);
		}
		else
		{
			_parent.DestroyChildren();
			object[] array = new object[] { _parent };
			foreach (KeyValuePair<string, Type> keyValuePair in this.m_tabDict)
			{
				if (keyValuePair.Key == this.m_selectedKey)
				{
					if (this.m_subTabDict == null || !this.m_subTabDict.ContainsKey(this.m_selectedKey))
					{
						_parent.SetHeight(0.8475f, RelativeTo.ScreenHeight);
						uicomponent = Activator.CreateInstance(keyValuePair.Value, array) as UIComponent;
					}
					else
					{
						_parent.SetHeight(0.7875f, RelativeTo.ScreenHeight);
						Dictionary<string, Type> dictionary = this.m_subTabDict[this.m_selectedKey];
						uicomponent = Activator.CreateInstance(dictionary[this.m_selectedSubKey], array) as UIComponent;
					}
					break;
				}
			}
			this.m_currentContent = uicomponent;
			if (uicomponent != null && _update)
			{
				CameraS.BringToFront(this.m_tabCanvas.m_camera, true);
				if (this.GetRoot() is PsUIBasePopup && (this.GetRoot() as PsUIBasePopup).m_overlayCamera != null)
				{
					CameraS.BringToFront((this.GetRoot() as PsUIBasePopup).m_overlayCamera, true);
				}
				_parent.Update();
			}
		}
	}

	// Token: 0x04001C32 RID: 7218
	public static int m_selectedTab = 1;

	// Token: 0x04001C33 RID: 7219
	public static int m_selectedSubTab = 1;
}
