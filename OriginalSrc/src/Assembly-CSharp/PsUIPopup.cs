using System;
using UnityEngine;

// Token: 0x020003C3 RID: 963
public abstract class PsUIPopup : UIScrollableCanvas
{
	// Token: 0x06001B6A RID: 7018 RVA: 0x001311D8 File Offset: 0x0012F5D8
	public PsUIPopup(Action _proceed = null, Action _cancel = null, bool _hideUI = false, string _tag = "Popup")
		: base(null, _tag)
	{
		this.m_hideUI = _hideUI;
		if (this.m_hideUI)
		{
			PsState.m_UIComponentsHidden = true;
		}
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetMargins(0.25f, 0.25f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.SetDepthOffset(20f);
		this.m_PROCEED = _proceed;
		this.m_CANCEL = _cancel;
		this.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
		PsState.m_openPopups.Add(this);
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x00131280 File Offset: 0x0012F680
	public void UpdatePopupBG()
	{
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x00131282 File Offset: 0x0012F682
	public void Proceed()
	{
		if (this.m_PROCEED != null)
		{
			this.m_PROCEED.Invoke();
		}
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x0013129A File Offset: 0x0012F69A
	public void Cancel()
	{
		if (this.m_CANCEL != null)
		{
			this.m_CANCEL.Invoke();
		}
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x001312B2 File Offset: 0x0012F6B2
	public virtual void ToggleUI(bool _visible)
	{
		PsState.m_UIComponentsHidden = !_visible;
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x001312BD File Offset: 0x0012F6BD
	public override void Destroy()
	{
		PsState.m_openPopups.Remove(this);
		base.Destroy();
		if (this.m_hideUI)
		{
			this.ToggleUI(true);
		}
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x001312E4 File Offset: 0x0012F6E4
	public static void PopupDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Material material = ResourceManager.GetMaterial(RESOURCE.MenuMetalMat_Material);
		string text = "008ce5";
		string text2 = "0061a3";
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.height * 0.025f, 2, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, roundedRect, DebugDraw.HexToUint(text), DebugDraw.HexToUint(text2), material, _c.m_camera, "BG", null);
	}

	// Token: 0x04001DC9 RID: 7625
	private Action m_PROCEED;

	// Token: 0x04001DCA RID: 7626
	private Action m_CANCEL;

	// Token: 0x04001DCB RID: 7627
	private bool m_hideUI;
}
