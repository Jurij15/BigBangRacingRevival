using System;
using UnityEngine;

// Token: 0x020001E8 RID: 488
public class EditorSelectorArea : EditorSelectorBase
{
	// Token: 0x06000EA1 RID: 3745 RVA: 0x000880F7 File Offset: 0x000864F7
	public EditorSelectorArea(EditorSelectorContext _context)
		: base(PsStrings.Get(StringID.EDITOR_GUI_DOME_SIZE_HEADER).ToUpper(), PsStrings.Get(StringID.EDITOR_GUI_DOME_SIZE_SUBHEADER), _context, null)
	{
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0008811C File Offset: 0x0008651C
	public override void Enter(IStatedObject _parent)
	{
		base.Enter(_parent);
		foreach (PsUnlockable psUnlockable in PsMetagameData.m_gameAreas[0].m_items)
		{
			PsEditorItem psEditorItem = (PsEditorItem)psUnlockable;
			this.AddItemCard(psEditorItem.m_name, psEditorItem.m_description, PsColors.objectCatCardGrey, psEditorItem);
		}
		this.m_selector.Update();
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x000881AC File Offset: 0x000865AC
	public override void AddItemCard(string _name, string _description, Color _color, PsEditorItem _itemMetaData)
	{
		PsEditorItemCard psEditorItemCard = new PsEditorItemCard(this.m_itemList, new Action<PsEditorItem>(this.ItemTouchHandler), PsStrings.Get(_name), PsStrings.Get(_description), _color, _itemMetaData, this.GetDomeSizeIndex(_itemMetaData.m_identifier) < PsState.m_wizardDomeIndex);
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x000881F8 File Offset: 0x000865F8
	public int GetDomeSizeIndex(string _identifier)
	{
		if (_identifier != null && !(_identifier == "AreaSmall"))
		{
			if (_identifier == "AreaMedium")
			{
				return 1;
			}
			if (_identifier == "AreaLarge")
			{
				return 2;
			}
		}
		return 0;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x00088258 File Offset: 0x00086658
	public override void ItemTouchHandler(PsEditorItem _itemMetaData)
	{
		TouchAreaS.CancelAllTouches(null);
		string identifier = _itemMetaData.m_identifier;
		int num;
		if (identifier != null && !(identifier == "AreaSmall"))
		{
			if (identifier == "AreaMedium")
			{
				num = 1;
				goto IL_5F;
			}
			if (identifier == "AreaLarge")
			{
				num = 2;
				goto IL_5F;
			}
		}
		num = 0;
		IL_5F:
		if ((this.m_context == EditorSelectorContext.ITEM_SELECT || this.m_context == EditorSelectorContext.FREE_WIZARD) && num < PsState.m_wizardDomeIndex)
		{
			new PsUIBasePopup(typeof(EditorSelectorArea.PsUICenterDomeAreaPrompt), null, null, null, false, true, InitialPage.Center, false, false, false);
			return;
		}
		PsState.m_activeMinigame.m_changed = true;
		if (this.m_context == EditorSelectorContext.ITEM_SELECT || this.m_context == EditorSelectorContext.FREE_WIZARD)
		{
			PsState.m_wizardDomeIndex = num;
			PsState.m_activeMinigame.UpdateAreaLimits(num, true, true);
			PsState.m_activeGameLoop.CreateEnvironment();
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
		}
		else
		{
			PsState.m_wizardDomeIndex = num;
			DefaultMinigame.ApplyWizard(PsState.m_activeMinigame);
			(PsState.m_activeGameLoop as PsGameLoopEditor).CreateEnvironment();
			(PsState.m_activeGameLoop as PsGameLoopEditor).CreateEditMenu();
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
		}
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x000883A4 File Offset: 0x000867A4
	public override void Execute()
	{
	}

	// Token: 0x020001E9 RID: 489
	private class PsUICenterDomeAreaPrompt : PsUIHeaderedCanvas
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x000883A8 File Offset: 0x000867A8
		public PsUICenterDomeAreaPrompt(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.4f, RelativeTo.ScreenWidth);
			this.SetHeight(0.4f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.Destroy();
			UITextbox uitextbox = new UITextbox(this, false, string.Empty, PsStrings.Get(StringID.EDITOR_POPUP_SIZE_WARNING), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenShortest, true, Align.Center, Align.Top, null, true, null);
			uitextbox.SetMargins(0.05f, RelativeTo.ScreenHeight);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetAlign(0.5f, 0f);
			uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
			this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetText(PsStrings.Get(StringID.OK), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetGreenColors(true);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00088529 File Offset: 0x00086929
		public override void Step()
		{
			if (this.m_ok.m_hit)
			{
				this.GetRoot().Destroy();
			}
			base.Step();
		}

		// Token: 0x04001191 RID: 4497
		private PsUIGenericButton m_ok;
	}
}
