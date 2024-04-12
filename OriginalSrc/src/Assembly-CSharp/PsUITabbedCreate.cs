using System;
using System.Collections.Generic;

// Token: 0x02000290 RID: 656
public class PsUITabbedCreate : PsUITabbedMenu
{
	// Token: 0x060013B6 RID: 5046 RVA: 0x000C5844 File Offset: 0x000C3C44
	public PsUITabbedCreate(UIComponent _parent)
		: base(_parent)
	{
		FrbMetrics.SetCurrentScreen("create_search_menu");
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.LightShadowBackground));
		this.SetWidth(1f, RelativeTo.ScreenWidth);
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x000C5898 File Offset: 0x000C3C98
	protected override Dictionary<string, Type> SetTabs()
	{
		Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
		dictionary.Add(PsStrings.Get(StringID.EDITOR_TAB_MY_LEVELS), typeof(PsUICenterOwnLevels));
		dictionary.Add(PsStrings.Get(StringID.EDITOR_TAB_FRIEND_LEVELS), typeof(PsUIFriendLevelList));
		dictionary.Add(PsStrings.Get(StringID.EDITOR_TAB_SEARCH), typeof(PsUICenterSearch));
		dictionary.Add(PsStrings.Get(StringID.TOP_CREATORS), typeof(PsUICreatorLeaderboard));
		return dictionary;
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x000C5914 File Offset: 0x000C3D14
	protected override string SetDefaultTab()
	{
		string text = null;
		int num = 1;
		foreach (string text2 in this.m_tabDict.Keys)
		{
			if (num == PsUITabbedCreate.m_selectedTab)
			{
				text = text2;
				break;
			}
			num++;
		}
		return text;
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x000C598C File Offset: 0x000C3D8C
	protected override Dictionary<string, Dictionary<string, Type>> SetSubTabs()
	{
		Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
		dictionary.Add(PsStrings.Get(StringID.EDITOR_TAB_FRIEND_LEVELS_SUBTAB_ALL), typeof(PsUIFriendProfiles));
		dictionary.Add(PsStrings.Get(StringID.EDITOR_TAB_FRIEND_LEVELS_SUBTAB_LATEST), typeof(PsUIFriendLevelList));
		Dictionary<string, Dictionary<string, Type>> dictionary2 = new Dictionary<string, Dictionary<string, Type>>();
		dictionary2.Add(PsStrings.Get(StringID.EDITOR_TAB_FRIEND_LEVELS), dictionary);
		Dictionary<string, Type> dictionary3 = new Dictionary<string, Type>();
		dictionary3.Add(PsStrings.Get(StringID.BOARD_GLOBAL), typeof(PsUICreatorLeaderboard));
		dictionary3.Add(PsStrings.Get(StringID.BOARD_LOCAL), typeof(PsUICreatorLeaderboard));
		dictionary3.Add(PsStrings.Get(StringID.BOARD_FRIENDS), typeof(PsUICreatorLeaderboard));
		dictionary2.Add(PsStrings.Get(StringID.TOP_CREATORS), dictionary3);
		return dictionary2;
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x000C5A50 File Offset: 0x000C3E50
	public override void SelectSubTab(int _tab)
	{
		PsUITabbedCreate.m_selectedSubTab = _tab;
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x000C5A58 File Offset: 0x000C3E58
	protected override string SetDefaultSubTab()
	{
		if (this.m_subTabDict != null && this.m_subTabDict.ContainsKey(this.m_selectedKey))
		{
			string text = null;
			int num = 1;
			bool flag = false;
			foreach (string text2 in this.m_subTabDict[this.m_selectedKey].Keys)
			{
				if (num == PsUITabbedCreate.m_selectedSubTab)
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
						PsUITabbedCreate.m_selectedSubTab = 1;
					}
				}
			}
			return text;
		}
		return string.Empty;
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x000C5B70 File Offset: 0x000C3F70
	protected override void CreateTabCanvas(bool _update)
	{
		base.CreateTabCanvas(_update);
		this.m_tabList.SetHorizontalAlign(0.4f);
		this.Update();
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x000C5B8F File Offset: 0x000C3F8F
	protected override void CreateTab(UICanvas _parent, string _text, bool _active)
	{
		this.m_tabFontSize = 0.0245f;
		base.CreateTab(_parent, _text, _active);
		_parent.SetWidth(0.215f, RelativeTo.ScreenHeight);
		_parent.SetMargins(0f, 0f, 0.035f, 0.01f, RelativeTo.ScreenHeight);
	}

	// Token: 0x04001680 RID: 5760
	public static int m_selectedTab = 1;

	// Token: 0x04001681 RID: 5761
	public static int m_selectedSubTab = 1;
}
