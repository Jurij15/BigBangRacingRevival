using System;
using UnityEngine;

// Token: 0x020002BF RID: 703
public class PsUIReportProfile : PsUIHeaderedCanvas
{
	// Token: 0x060014D4 RID: 5332 RVA: 0x000D96FC File Offset: 0x000D7AFC
	public PsUIReportProfile(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.6f, RelativeTo.ScreenWidth);
		this.SetHeight(0.6f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.1f, 0.1f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000D9800 File Offset: 0x000D7C00
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.REPORT_POPUP_TITLE).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x000D9838 File Offset: 0x000D7C38
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x000D98B8 File Offset: 0x000D7CB8
	public void CreateContent(UIComponent _parent)
	{
		Debug.LogError("Creating content");
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.SetMargins(0f, 0f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		_parent.RemoveTouchAreas();
		string text = PsStrings.Get(StringID.REPORT_POPUP_TEXT);
		text = text.Replace("%1", "<color=#84F22F>" + this.m_name + "</color>");
		this.m_textBox = new UITextbox(uiverticalList, true, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		this.m_textBox.SetWidth(0.8f, RelativeTo.ParentWidth);
		this.m_textBox.SetMargins(0.05f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetWidth(0.8f, RelativeTo.ParentWidth);
		uiverticalList2.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.KICK_REASON), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(0f);
		this.m_field = new PsUIKickField(uiverticalList2);
		this.m_field.m_textField.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_field.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		(this.m_field.m_textField as UITextbox).SetMinRows(2);
		(this.m_field.m_textField as UITextbox).SetMaxRows(4);
		this.m_field.m_textField.SetDrawHandler(new UIDrawDelegate(this.LeftCommentDrawhandler));
		this.m_field.SetMinMaxCharacterCount(10, 100);
		this.m_field.SetCallbacks(new Action<string>(this.DoneWriting), null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_continue.SetRedColors();
		this.m_continue.SetText(PsStrings.Get(StringID.BUTTON_REPORT).ToUpper(), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x000D9B54 File Offset: 0x000D7F54
	public void SetUser(PlayerData _user)
	{
		Debug.LogError("Setting user!");
		this.m_name = _user.name;
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000D9B6D File Offset: 0x000D7F6D
	public void SetUser(string _name)
	{
		this.m_name = _name;
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x000D9B76 File Offset: 0x000D7F76
	public void DoneWriting(string _input)
	{
		this.m_input = _input;
		this.m_field.SetText(this.m_input);
		this.Update();
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x000D9B98 File Offset: 0x000D7F98
	public override void Step()
	{
		if (this.m_continue.m_hit)
		{
			if (this.m_field != null && this.m_input.Length > this.m_field.m_minCharacterCount)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
			}
			else
			{
				UITooShortNamePopup uitooShortNamePopup = new UITooShortNamePopup(this.m_field.m_minCharacterCount, new Action(this.TooShortClosed));
			}
		}
		else if (this.m_textBox != null && this.m_textBox.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			Application.OpenURL("http://www.traplightgames.com/terms/");
		}
		base.Step();
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000D9C49 File Offset: 0x000D8049
	private void TooShortClosed()
	{
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x000D9C4C File Offset: 0x000D804C
	private void LeftCommentDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.015f * (float)Screen.height, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#ffffff");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x04001791 RID: 6033
	private PsUIGenericButton m_continue;

	// Token: 0x04001792 RID: 6034
	private PsUIKickField m_field;

	// Token: 0x04001793 RID: 6035
	private string m_name;

	// Token: 0x04001794 RID: 6036
	public UITextbox m_textBox;

	// Token: 0x04001795 RID: 6037
	public string m_input = string.Empty;
}
