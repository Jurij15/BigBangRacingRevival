using System;
using UnityEngine;

// Token: 0x020001ED RID: 493
public class EditorSelectorGameMode : EditorSelectorBase
{
	// Token: 0x06000EB1 RID: 3761 RVA: 0x00088938 File Offset: 0x00086D38
	public EditorSelectorGameMode(EditorSelectorContext _context)
		: base(PsStrings.Get(StringID.EDITOR_GUI_GAMEMODE_HEADER).ToUpper(), PsStrings.Get(StringID.EDITOR_GUI_GAMEMODE_SUBHEADER), _context, null)
	{
		this.m_minigame = LevelManager.m_currentLevel as Minigame;
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x0008896C File Offset: 0x00086D6C
	public override void Enter(IStatedObject _parent)
	{
		base.Enter(_parent);
		foreach (PsUnlockable psUnlockable in PsMetagameData.m_gameModes[0].m_items)
		{
			PsEditorItem psEditorItem = (PsEditorItem)psUnlockable;
			this.AddItemCard(psEditorItem.m_name, psEditorItem.m_description, PsColors.objectCatCardGrey, psEditorItem);
		}
		this.m_selector.Update();
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x000889FC File Offset: 0x00086DFC
	public override void ItemTouchHandler(PsEditorItem _itemMetaData)
	{
		string text = _itemMetaData.m_identifier.Substring(8);
		PsState.m_activeMinigame.m_changed = true;
		if (this.m_context == EditorSelectorContext.ITEM_SELECT || this.m_context == EditorSelectorContext.FREE_WIZARD)
		{
			this.m_minigame.SetGameMode(text, CameraS.m_mainCamera.transform.position + new Vector3(-50f, 0f, 0f), false);
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
			Debug.Log("Setting game mode to: " + PsState.m_activeGameLoop.GetGameMode(), null);
		}
		else
		{
			PsState.m_wizardGameMode = text;
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorArea(EditorSelectorContext.START_WIZARD));
		}
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x00088ACF File Offset: 0x00086ECF
	public override void Execute()
	{
	}

	// Token: 0x040011A5 RID: 4517
	private Minigame m_minigame;
}
