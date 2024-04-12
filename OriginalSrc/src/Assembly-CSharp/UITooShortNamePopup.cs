using System;
using UnityEngine;

// Token: 0x020005AC RID: 1452
public class UITooShortNamePopup : UIScrollableCanvas
{
	// Token: 0x06002A2B RID: 10795 RVA: 0x001B9EC4 File Offset: 0x001B82C4
	public UITooShortNamePopup(int _minLength, Action _closeCallback)
		: base(null, "Popup")
	{
		this.m_minLength = _minLength;
		this.m_closeCallback = _closeCallback;
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetMargins(0.25f, 0.25f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.SetDepthOffset(20f);
		this.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		string text = PsStrings.Get(StringID.SEARCH_INPUT_LIMIT_POPUP);
		text = text.Replace("%1", this.m_minLength + string.Empty);
		new UIText(uiverticalList, false, string.Empty, text, "KGSecondChances_Font", 0.03f, RelativeTo.ScreenShortest, null, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.RemoveTouchAreas();
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		this.m_okButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_okButton.SetText(PsStrings.Get(StringID.OK), 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_okButton.SetOrangeColors(true);
		this.m_okButton.Update();
		this.Update();
		this.UpdatePopupBG();
	}

	// Token: 0x06002A2C RID: 10796 RVA: 0x001BA052 File Offset: 0x001B8452
	public void UpdatePopupBG()
	{
		if (this.d_Draw != null)
		{
			this.d_Draw(this);
		}
	}

	// Token: 0x06002A2D RID: 10797 RVA: 0x001BA06B File Offset: 0x001B846B
	public override void Step()
	{
		if (Input.GetKeyDown(27) || this.m_okButton.m_hit)
		{
			this.Destroy();
			this.m_closeCallback.Invoke();
			return;
		}
		base.Step();
	}

	// Token: 0x06002A2E RID: 10798 RVA: 0x001BA0A4 File Offset: 0x001B84A4
	public static void PopupDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Material material = ResourceManager.GetMaterial(RESOURCE.MenuMetalMat_Material);
		string text = "008ce5";
		string text2 = "0061a3";
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight * 0.95f, (float)Screen.height * 0.025f, 2, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, roundedRect, DebugDraw.HexToUint(text), DebugDraw.HexToUint(text2), material, _c.m_camera, "BG", null);
	}

	// Token: 0x04002F82 RID: 12162
	private int m_minLength;

	// Token: 0x04002F83 RID: 12163
	private PsUIGenericButton m_okButton;

	// Token: 0x04002F84 RID: 12164
	private Action m_closeCallback;
}
