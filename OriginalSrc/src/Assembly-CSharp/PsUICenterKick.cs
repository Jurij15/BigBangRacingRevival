using System;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class PsUICenterKick : PsUIHeaderedCanvas
{
	// Token: 0x0600169C RID: 5788 RVA: 0x000EEA98 File Offset: 0x000ECE98
	public PsUICenterKick(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.05f, 0.05f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x000EEB91 File Offset: 0x000ECF91
	public void SetCustomCallback(Action<string> _action)
	{
		this.m_customCallBackAction = _action;
	}

	// Token: 0x0600169E RID: 5790 RVA: 0x000EEB9C File Offset: 0x000ECF9C
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.TEAM_BUTTON_KICK).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x0600169F RID: 5791 RVA: 0x000EEBD4 File Offset: 0x000ECFD4
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x000EEC54 File Offset: 0x000ED054
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.KICK_REASON), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(0f);
		this.m_field = new PsUIKickField(uiverticalList);
		this.m_field.m_textField.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_field.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		(this.m_field.m_textField as UITextbox).SetMinRows(2);
		(this.m_field.m_textField as UITextbox).SetMaxRows(4);
		this.m_field.m_textField.SetDrawHandler(new UIDrawDelegate(this.CommentDrawhandler));
		this.m_field.SetMinMaxCharacterCount(0, 100);
		this.m_field.SetCallbacks(new Action<string>(this.DoneWriting), null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_continue.SetRedColors();
		this.m_continue.SetText(PsStrings.Get(StringID.TEAM_BUTTON_KICK), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x000EEE3C File Offset: 0x000ED23C
	private void CommentDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.015f * (float)Screen.height, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#ffffff");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x000EEEEE File Offset: 0x000ED2EE
	public void DoneWriting(string _input)
	{
		this.m_input = _input;
		this.m_field.SetText(this.m_input);
		this.Update();
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x000EEF10 File Offset: 0x000ED310
	public override void Step()
	{
		if (this.m_continue.m_hit && !this.m_kicking && this.m_input != null && this.m_input.Length >= 3)
		{
			this.m_kicking = true;
			if (this.m_customCallBackAction != null)
			{
				this.m_customCallBackAction.Invoke(this.m_input);
			}
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		base.Step();
	}

	// Token: 0x04001957 RID: 6487
	private PsUIGenericButton m_continue;

	// Token: 0x04001958 RID: 6488
	private PsUIKickField m_field;

	// Token: 0x04001959 RID: 6489
	private string m_input;

	// Token: 0x0400195A RID: 6490
	private bool m_kicking;

	// Token: 0x0400195B RID: 6491
	private Action<string> m_customCallBackAction;
}
