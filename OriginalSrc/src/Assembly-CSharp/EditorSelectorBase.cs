using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
public class EditorSelectorBase : BasicState
{
	// Token: 0x06000EA9 RID: 3753 RVA: 0x00087EDC File Offset: 0x000862DC
	public EditorSelectorBase(string _header, string _description, EditorSelectorContext _context, UIFittedText _notificationAmount = null)
	{
		this.m_context = _context;
		this.m_headerName = _header;
		this.m_description = _description;
		this.m_notificationAmount = _notificationAmount;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00087F04 File Offset: 0x00086304
	public override void Enter(IStatedObject _parent)
	{
		this.m_model = new UIModel(this, null);
		EntityManager.SetActivityOfEntitiesWithTag("UIComponent", false, true, true, true, false, false);
		this.m_selector = new UICanvas(null, true, "SelectorContainer", null, string.Empty);
		this.m_selector.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_selector.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_selector.SetDepthOffset(100f);
		this.m_selector.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.EditorPopupBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this.m_selector, "SelectorArea");
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetVerticalAlign(1f);
		bool flag = true;
		this.m_navigator = new UIEditorSelectorNavigator(uiverticalList, this.m_headerName, this.m_description, "Navigation", this.m_context, flag, this.m_notificationAmount);
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(uiverticalList, "ItemArea");
		uiscrollableCanvas.SetWidth(1f, RelativeTo.ScreenWidth);
		uiscrollableCanvas.SetHeight(0.4f, RelativeTo.ScreenHeight);
		uiscrollableCanvas.SetMargins(0.35f, 0.3f, 0.02f, 0f, RelativeTo.ScreenShortest);
		uiscrollableCanvas.m_maxScrollInertialY = 0f;
		uiscrollableCanvas.m_maxScrollInertialX = 200f;
		uiscrollableCanvas.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiscrollableCanvas, "ItemList");
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.RemoveDrawHandler();
		this.m_itemList = uihorizontalList;
		this.m_selector.Update();
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x0008807C File Offset: 0x0008647C
	public virtual void AddItemCard(string _name, string _description, Color _color, PsEditorItem _itemMetaData)
	{
		PsEditorItemCard psEditorItemCard = new PsEditorItemCard(this.m_itemList, new Action<PsEditorItem>(this.ItemTouchHandler), PsStrings.Get(_name), PsStrings.Get(_description), _color, _itemMetaData, false);
		if (psEditorItemCard.locked)
		{
			psEditorItemCard.RemoveTouchAreas();
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x000880C3 File Offset: 0x000864C3
	public virtual void ItemTouchHandler(PsEditorItem _itemMetaData)
	{
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x000880C5 File Offset: 0x000864C5
	public override void Execute()
	{
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x000880C7 File Offset: 0x000864C7
	public override void Exit()
	{
		if (this.m_selector != null)
		{
			this.m_selector.Destroy();
			this.m_selector = null;
		}
		EntityManager.SetActivityOfEntitiesWithTag("UIComponent", true, true, true, true, false, false);
	}

	// Token: 0x04001198 RID: 4504
	public EditorSelectorContext m_context;

	// Token: 0x04001199 RID: 4505
	public UIModel m_model;

	// Token: 0x0400119A RID: 4506
	public UIHorizontalList m_itemList;

	// Token: 0x0400119B RID: 4507
	public UICanvas m_selector;

	// Token: 0x0400119C RID: 4508
	public UIEditorSelectorNavigator m_navigator;

	// Token: 0x0400119D RID: 4509
	public string m_headerName;

	// Token: 0x0400119E RID: 4510
	public string m_description;

	// Token: 0x0400119F RID: 4511
	protected UIFittedText m_notificationAmount;
}
