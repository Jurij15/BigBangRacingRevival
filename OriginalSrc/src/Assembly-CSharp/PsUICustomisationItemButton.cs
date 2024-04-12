using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class PsUICustomisationItemButton : UICanvas
{
	// Token: 0x060004D0 RID: 1232 RVA: 0x0003F7B0 File Offset: 0x0003DBB0
	public PsUICustomisationItemButton(UIComponent _parent, PsCustomisationItem _item, PsUICustomisationItemButton.ItemButtonTouchEvent _touchEvent)
		: base(_parent, true, "ItemButton", null, string.Empty)
	{
		this.m_item = _item;
		this.m_touchEvent = _touchEvent;
		string text = "menu_trail_icon_empty";
		if (_item != null)
		{
			text = _item.m_iconName;
		}
		float num = 0.8f;
		this.SetSize(0.33333334f, 0.29850748f, RelativeTo.ParentWidth);
		this.SetMargins(0f, 0f, 0f, 0.08f, RelativeTo.OwnHeight);
		this.RemoveDrawHandler();
		this.m_TAC.m_letTouchesThrough = true;
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_hat_shadow", null);
		UISprite uisprite = new UISprite(this, false, "Shadow", PsState.m_uiSheet, frame, true);
		uisprite.SetSize(num, frame.height / frame.width * num, RelativeTo.ParentWidth);
		uisprite.SetVerticalAlign(0f);
		uisprite.SetMargins(0.08f, 0.08f, 0.05f, 0.08f, RelativeTo.OwnWidth);
		uisprite.SetDepthOffset(-2f);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame(text, null);
		float num2 = 0.9f;
		UISprite uisprite2 = new UISprite(uisprite, false, string.Empty, PsState.m_uiSheet, frame2, true);
		uisprite2.SetSize(num2, frame2.height / frame2.width * num2, RelativeTo.ParentWidth);
		uisprite2.SetVerticalAlign(0f);
		uisprite2.SetDepthOffset(-3f);
		this.CreateUnlockedMark();
		if (_item != null && _item.m_installed)
		{
			this.Mark(true);
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0003F92C File Offset: 0x0003DD2C
	public void CreateUnlockedMark()
	{
		if (this.m_item != null && this.m_item.m_unlocked)
		{
			UISprite uisprite = new UISprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_icon_bought", null), true);
			uisprite.SetSize(0.25f, 0.25f * (uisprite.m_frame.height / uisprite.m_frame.width), RelativeTo.ParentWidth);
			uisprite.SetAlign(0f, 1f);
			uisprite.SetDepthOffset(-6f);
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0003F9C0 File Offset: 0x0003DDC0
	public void Mark(bool _selected)
	{
		if (this.m_highlightSprite != null)
		{
			this.m_highlightSprite.Destroy();
			this.m_highlightSprite = null;
		}
		if (_selected)
		{
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_shelf_highlight", null);
			this.m_highlightSprite = new UISprite(this, false, "Highlight", PsState.m_uiSheet, frame, true);
			this.m_highlightSprite.SetWidth(1.25f, RelativeTo.ParentWidth);
			this.m_highlightSprite.SetHeight(1.17f, RelativeTo.ParentHeight);
		}
		this.Update();
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0003FA47 File Offset: 0x0003DE47
	public override void Step()
	{
		if (this.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			this.m_touchEvent(this);
		}
		base.Step();
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0003FA6C File Offset: 0x0003DE6C
	public void ShowInfo(PsCustomisationItem _customisationItem, Action _purchaseAction)
	{
		this.m_info = new CustomisationItemInfo(this, _customisationItem, _purchaseAction);
		this.m_info.SetDepthOffset(-10f);
		this.m_info.Update();
		if (this.notificationBase != null)
		{
			this.notificationBase.Destroy();
			this.notificationBase = null;
		}
		SoundS.PlaySingleShot("/UI/UpgradeSelect", Vector2.zero, 1f);
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0003FAD8 File Offset: 0x0003DED8
	public void HideInfo()
	{
		if (this.m_info != null)
		{
			this.m_info.Destroy();
			this.m_info = null;
		}
	}

	// Token: 0x0400062A RID: 1578
	public PsCustomisationItem m_item;

	// Token: 0x0400062B RID: 1579
	public UISprite m_highlightSprite;

	// Token: 0x0400062C RID: 1580
	private PsUICustomisationItemButton.ItemButtonTouchEvent m_touchEvent;

	// Token: 0x0400062D RID: 1581
	private UICanvas notificationBase;

	// Token: 0x0400062E RID: 1582
	public CustomisationItemInfo m_info;

	// Token: 0x020000DE RID: 222
	// (Invoke) Token: 0x060004D7 RID: 1239
	public delegate void ItemButtonTouchEvent(PsUICustomisationItemButton _button);
}
