using System;
using System.Collections.Generic;

// Token: 0x02000297 RID: 663
public class PsUITabbedEvents : PsUITabbedMenu
{
	// Token: 0x060013FA RID: 5114 RVA: 0x000C93D4 File Offset: 0x000C77D4
	public PsUITabbedEvents(UIComponent _parent)
		: base(_parent)
	{
		FrbMetrics.SetCurrentScreen("events_tourney_menu");
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.LightShadowBackground));
		this.SetWidth(1f, RelativeTo.ScreenWidth);
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x000C9428 File Offset: 0x000C7828
	protected override Dictionary<string, Type> SetTabs()
	{
		Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
		dictionary.Add(PsStrings.Get(StringID.TOUR_EVENTS_MENU).ToUpper(), typeof(PsUICenterEventPopup));
		dictionary.Add(PsStrings.Get(StringID.NEWS).ToUpper(), typeof(PsUICenterNewsPopup));
		return dictionary;
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x000C9478 File Offset: 0x000C7878
	protected override string SetDefaultTab()
	{
		string text = null;
		int num = 1;
		foreach (string text2 in this.m_tabDict.Keys)
		{
			if (num == PsUITabbedEvents.m_selectedTab)
			{
				text = text2;
				break;
			}
			num++;
		}
		return text;
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x000C94F0 File Offset: 0x000C78F0
	public override void SelectSubTab(int _tab)
	{
		PsUITabbedEvents.m_selectedSubTab = _tab;
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x000C94F8 File Offset: 0x000C78F8
	protected override string SetDefaultSubTab()
	{
		if (this.m_subTabDict != null && this.m_subTabDict.ContainsKey(this.m_selectedKey))
		{
			string text = null;
			int num = 1;
			bool flag = false;
			foreach (string text2 in this.m_subTabDict[this.m_selectedKey].Keys)
			{
				if (num == PsUITabbedEvents.m_selectedSubTab)
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
						PsUITabbedEvents.m_selectedSubTab = 1;
					}
				}
			}
			return text;
		}
		return string.Empty;
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x000C9610 File Offset: 0x000C7A10
	protected override void CreateTab(UICanvas _parent, string _text, bool _active)
	{
		if (_active)
		{
			UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tab_active", null), true, true);
			uifittedSprite.SetVerticalAlign(1f);
			uifittedSprite.SetMargins(0.05f, RelativeTo.OwnHeight);
			UITextbox uitextbox = new UITextbox(uifittedSprite, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "ffffff", false, "#0F60A1");
			uitextbox.SetVerticalAlign(0.65f);
			uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
			uitextbox.SetHeight(1f, RelativeTo.ParentHeight);
			uitextbox.SetMargins(0.02f, 0.02f, 0f, 0.01f, RelativeTo.ScreenHeight);
		}
		else
		{
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tab_deactive", null), true, true);
			uifittedSprite2.SetDepthOffset(20f);
			uifittedSprite2.SetVerticalAlign(0.9f);
			uifittedSprite2.SetMargins(0.05f, RelativeTo.OwnHeight);
			UITextbox uitextbox2 = new UITextbox(uifittedSprite2, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "#89CFE8", false, "#1060A2");
			uitextbox2.SetVerticalAlign(0.6f);
			uitextbox2.SetWidth(1f, RelativeTo.ParentWidth);
			uitextbox2.SetHeight(1f, RelativeTo.ParentHeight);
			uitextbox2.SetMargins(0.02f, 0.02f, 0f, 0.01f, RelativeTo.ScreenHeight);
		}
	}

	// Token: 0x040016B1 RID: 5809
	public static int m_selectedTab = 1;

	// Token: 0x040016B2 RID: 5810
	public static int m_selectedSubTab = 1;
}
