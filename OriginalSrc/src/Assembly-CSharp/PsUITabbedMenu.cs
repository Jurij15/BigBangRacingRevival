using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000328 RID: 808
public abstract class PsUITabbedMenu : UICanvas
{
	// Token: 0x060017B4 RID: 6068 RVA: 0x000C4CE0 File Offset: 0x000C30E0
	public PsUITabbedMenu(UIComponent _parent)
		: base(_parent, false, "TabbedMenu", null, string.Empty)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DebriefBackground));
		this.SetWidth(0.9f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.SetVerticalAlign(0f);
		this.RemoveDrawHandler();
		this.m_tabDict = this.SetTabs();
		this.m_subTabDict = this.SetSubTabs();
		this.m_selectedKey = this.SetDefaultTab();
		this.m_selectedSubKey = this.SetDefaultSubTab();
		if (string.IsNullOrEmpty(this.m_selectedKey))
		{
			this.SetDefaultSelectedTabName();
		}
		if (string.IsNullOrEmpty(this.m_selectedSubKey))
		{
			this.SetDefaultSelectedSubTabName();
		}
		this.m_scrollArea = new UIScrollableCanvas(this, string.Empty);
		this.m_scrollArea.SetHeight(0.85f, RelativeTo.ScreenHeight);
		this.m_scrollArea.SetVerticalAlign(0f);
		this.m_scrollArea.RemoveDrawHandler();
		this.m_scrollArea.m_passTouchesToScrollableParents = true;
		this.CreateContent(this.m_scrollArea, false);
		this.CreateTabCanvas(false);
	}

	// Token: 0x060017B5 RID: 6069 RVA: 0x000C4E18 File Offset: 0x000C3218
	protected virtual Dictionary<string, Type> SetTabs()
	{
		Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
		dictionary.Add("Friends", typeof(PsUIFriendList));
		dictionary.Add("Enemies", typeof(PsUIFriendList));
		dictionary.Add("Leaders", typeof(PsUIFriendList));
		return dictionary;
	}

	// Token: 0x060017B6 RID: 6070 RVA: 0x000C4E6B File Offset: 0x000C326B
	protected virtual Dictionary<string, Dictionary<string, Type>> SetSubTabs()
	{
		return null;
	}

	// Token: 0x060017B7 RID: 6071 RVA: 0x000C4E6E File Offset: 0x000C326E
	protected virtual string SetDefaultTab()
	{
		Debug.LogError("Default selected value is not defined! Override this!");
		return string.Empty;
	}

	// Token: 0x060017B8 RID: 6072 RVA: 0x000C4E7F File Offset: 0x000C327F
	protected virtual string SetDefaultSubTab()
	{
		Debug.LogError("Default selected sub value is not defined! Override this!");
		return string.Empty;
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x000C4E90 File Offset: 0x000C3290
	private string SetDefaultSelectedTabName()
	{
		using (Dictionary<string, Type>.Enumerator enumerator = this.m_tabDict.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				KeyValuePair<string, Type> keyValuePair = enumerator.Current;
				return keyValuePair.Key;
			}
		}
		return string.Empty;
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x000C4EFC File Offset: 0x000C32FC
	private string SetDefaultSelectedSubTabName()
	{
		if (this.m_subTabDict != null && this.m_subTabDict.ContainsKey(this.m_selectedKey))
		{
			using (Dictionary<string, Type>.Enumerator enumerator = this.m_subTabDict[this.m_selectedKey].GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<string, Type> keyValuePair = enumerator.Current;
					return keyValuePair.Key;
				}
			}
		}
		return string.Empty;
	}

	// Token: 0x060017BB RID: 6075 RVA: 0x000C4F94 File Offset: 0x000C3394
	protected virtual void CreateTabCanvas(bool _update)
	{
		this.m_tabs = new List<UICanvas>();
		this.m_subTabs = new List<PsUIGenericButton>();
		if (this.m_tabCanvas != null)
		{
			this.m_tabCanvas.Destroy();
		}
		this.m_tabCanvas = new UIScrollableCanvas(this, "Header");
		this.m_tabCanvas.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_tabCanvas.SetHeight(0.175f, RelativeTo.ScreenHeight);
		this.m_tabCanvas.SetVerticalAlign(1f);
		this.m_tabCanvas.RemoveTouchAreas();
		this.m_tabCanvas.RemoveDrawHandler();
		this.m_tabList = new UIHorizontalList(this.m_tabCanvas, string.Empty);
		this.m_tabList.SetHeight(0.175f, RelativeTo.ScreenHeight);
		this.m_tabList.SetVerticalAlign(1f);
		this.m_tabList.RemoveDrawHandler();
		foreach (KeyValuePair<string, Type> keyValuePair in this.m_tabDict)
		{
			UICanvas uicanvas = new UICanvas(this.m_tabList, true, string.Empty, null, string.Empty);
			uicanvas.SetWidth(0.25f, RelativeTo.ScreenHeight);
			uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas.SetMargins(0f, 0f, 0.02f, 0.01f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			uicanvas.m_fieldName = keyValuePair.Key;
			this.m_tabs.Add(uicanvas);
			this.CreateTab(uicanvas, keyValuePair.Key, this.m_selectedKey == keyValuePair.Key);
		}
		UICanvas uicanvas2 = new UICanvas(this.m_tabCanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(1f, RelativeTo.ScreenHeight);
		uicanvas2.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas2.SetVerticalAlign(0f);
		uicanvas2.RemoveDrawHandler();
		UISprite uisprite = new UISprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tab_border", null), true);
		uisprite.SetWidth(1f, RelativeTo.ScreenWidth);
		uisprite.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uisprite.SetVerticalAlign(1f);
		Vector2 vector = new Vector3(0f, -0.02f * (float)Screen.height, 1f);
		if (this.m_subTabDict != null && this.m_subTabDict.ContainsKey(this.m_selectedKey))
		{
			this.m_tabCanvas.SetHeight(0.2325f, RelativeTo.ScreenHeight);
			uicanvas2.SetHeight(0.11f, RelativeTo.ScreenHeight);
			uisprite.SetHeight(0.09f, RelativeTo.ScreenHeight);
			vector = new Vector3(0f, -0.0485f * (float)Screen.height, 1f);
			UIHorizontalList uihorizontalList = new UIHorizontalList(uisprite, "subTabs");
			uihorizontalList.SetHeight(0.75f, RelativeTo.ParentHeight);
			uihorizontalList.SetVerticalAlign(1f);
			uihorizontalList.SetMargins(0f, 0f, 0.01f, -0.01f, RelativeTo.ScreenHeight);
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			this.CreateSubTabs(uihorizontalList);
		}
		Vector2[] line = DebugDraw.GetLine(new Vector2((float)(-(float)Screen.width) * 0.5f, 0f), new Vector2((float)Screen.width * 0.5f, 0f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(uisprite.m_TC, vector, line, 0.03f * (float)Screen.height, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), uisprite.m_camera, Position.Center, false);
		if (_update)
		{
			this.m_tabCanvas.Update();
		}
	}

	// Token: 0x060017BC RID: 6076 RVA: 0x000C5338 File Offset: 0x000C3738
	protected virtual void CreateSubTabs(UIComponent _parent)
	{
		Dictionary<string, Type> dictionary = this.m_subTabDict[this.m_selectedKey];
		foreach (KeyValuePair<string, Type> keyValuePair in dictionary)
		{
			bool flag = keyValuePair.Key == this.m_selectedSubKey;
			PsUIGenericButton psUIGenericButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.0025f, "Button");
			psUIGenericButton.m_fieldName = keyValuePair.Key;
			psUIGenericButton.SetHeight(1f, RelativeTo.ParentHeight);
			psUIGenericButton.SetSubTabBlueColors(!flag);
			psUIGenericButton.SetDepthOffset(-5f);
			if (flag)
			{
				psUIGenericButton.DisableTouchAreas(true);
			}
			psUIGenericButton.SetText(keyValuePair.Key, 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_subTabs.Add(psUIGenericButton);
		}
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x000C5434 File Offset: 0x000C3834
	protected virtual void CreateTab(UICanvas _parent, string _text, bool _active)
	{
		if (_active)
		{
			UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tab_active", null), true, true);
			uifittedSprite.SetVerticalAlign(1f);
			UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), this.m_tabFontSize, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "ffffff", false, "#0F60A1");
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
			UITextbox uitextbox2 = new UITextbox(_parent, false, string.Empty, _text, PsFontManager.GetFont(PsFonts.KGSecondChances), this.m_tabFontSize, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "#89CFE8", false, "#1060A2");
			uitextbox2.SetVerticalAlign(0.6f);
			uitextbox2.SetWidth(1f, RelativeTo.ParentWidth);
			uitextbox2.SetHeight(1f, RelativeTo.ParentHeight);
			uitextbox2.SetMargins(0.02f, 0.02f, 0f, 0.01f, RelativeTo.ScreenHeight);
		}
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x000C5590 File Offset: 0x000C3990
	protected virtual void CreateContent(UIComponent _parent, bool _update = false)
	{
		_parent.DestroyChildren();
		UIComponent uicomponent = null;
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

	// Token: 0x060017BF RID: 6079 RVA: 0x000C56FC File Offset: 0x000C3AFC
	public virtual void SelectSubTab(int _tab)
	{
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x000C5700 File Offset: 0x000C3B00
	public override void Step()
	{
		for (int i = 0; i < this.m_tabs.Count; i++)
		{
			if (this.m_tabs[i].m_hit && this.m_tabs[i].m_fieldName != this.m_selectedKey)
			{
				SoundS.PlaySingleShot("/UI/SwitchTab", Vector3.zero, 1f);
				this.m_selectedKey = this.m_tabs[i].m_fieldName;
				this.m_selectedSubKey = this.SetDefaultSubTab();
				this.CreateTabCanvas(true);
				this.CreateContent(this.m_scrollArea, true);
				break;
			}
		}
		for (int j = 0; j < this.m_subTabs.Count; j++)
		{
			if (this.m_subTabs[j].m_hit && this.m_selectedSubKey != this.m_subTabs[j].m_fieldName)
			{
				this.m_selectedSubKey = this.m_subTabs[j].m_fieldName;
				this.SelectSubTab(j + 1);
				this.CreateTabCanvas(true);
				this.CreateContent(this.m_scrollArea, true);
				break;
			}
		}
		base.Step();
	}

	// Token: 0x04001A87 RID: 6791
	protected UIScrollableCanvas m_scrollArea;

	// Token: 0x04001A88 RID: 6792
	protected UIScrollableCanvas m_tabCanvas;

	// Token: 0x04001A89 RID: 6793
	protected Dictionary<string, Type> m_tabDict;

	// Token: 0x04001A8A RID: 6794
	protected Dictionary<string, Dictionary<string, Type>> m_subTabDict;

	// Token: 0x04001A8B RID: 6795
	protected string m_selectedKey;

	// Token: 0x04001A8C RID: 6796
	protected string m_selectedSubKey;

	// Token: 0x04001A8D RID: 6797
	protected List<UICanvas> m_tabs;

	// Token: 0x04001A8E RID: 6798
	protected List<PsUIGenericButton> m_subTabs;

	// Token: 0x04001A8F RID: 6799
	private UICanvas m_tab1;

	// Token: 0x04001A90 RID: 6800
	private UICanvas m_tab2;

	// Token: 0x04001A91 RID: 6801
	private UICanvas m_tab3;

	// Token: 0x04001A92 RID: 6802
	protected UIComponent m_currentContent;

	// Token: 0x04001A93 RID: 6803
	public UIHorizontalList m_tabList;

	// Token: 0x04001A94 RID: 6804
	protected float m_tabFontSize = 0.0315f;
}
