using System;
using UnityEngine;

// Token: 0x020001EF RID: 495
public class EditorSelectorPlayer : EditorSelectorBase
{
	// Token: 0x06000EB9 RID: 3769 RVA: 0x00088E0D File Offset: 0x0008720D
	public EditorSelectorPlayer(EditorSelectorContext _context)
		: base(PsStrings.Get(StringID.EDITOR_GUI_HEADER).ToUpper(), PsStrings.Get(StringID.EDITOR_GUI_SUBHEADER), _context, null)
	{
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x00088E30 File Offset: 0x00087230
	public override void Enter(IStatedObject _parent)
	{
		base.Enter(_parent);
		for (int i = 0; i < PsMetagameData.m_playerUnits.Count; i++)
		{
			PsUnlockableCategory psUnlockableCategory = PsMetagameData.m_playerUnits[i];
			for (int j = 0; j < psUnlockableCategory.m_items.Count; j++)
			{
				if (j < psUnlockableCategory.m_items.Count || PsState.m_adminMode)
				{
					PsUpgradeableEditorItem psUpgradeableEditorItem = psUnlockableCategory.m_items[j] as PsUpgradeableEditorItem;
					this.AddItemCard(psUpgradeableEditorItem.m_name, psUpgradeableEditorItem.m_description, PsColors.objectCatCardGrey, psUpgradeableEditorItem);
				}
			}
		}
		this.m_selector.Update();
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x00088ED8 File Offset: 0x000872D8
	public override void ItemTouchHandler(PsEditorItem _itemMetaData)
	{
		PsState.m_activeMinigame.m_changed = true;
		if (this.m_context == EditorSelectorContext.ITEM_SELECT || this.m_context == EditorSelectorContext.FREE_WIZARD)
		{
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			Vector3 vector3 = Vector3.one;
			LevelPlayerNode levelPlayerNode = LevelManager.m_currentLevel.m_currentLayer.GetElement("Player") as LevelPlayerNode;
			if (levelPlayerNode != null)
			{
				vector = levelPlayerNode.m_position;
				vector2 = levelPlayerNode.m_rotation;
				vector3 = levelPlayerNode.m_scale;
				levelPlayerNode.Dispose();
			}
			string identifier = _itemMetaData.m_identifier;
			levelPlayerNode = new LevelPlayerNode(Type.GetType(identifier), "Player", vector, vector2, vector3);
			LevelManager.m_currentLevel.m_currentLayer.AddElement(levelPlayerNode);
			levelPlayerNode.Assemble();
			PsState.m_editorCameraPos = vector;
			CameraS.SnapMainCameraPos(PsState.m_editorCameraPos);
			if (levelPlayerNode != null)
			{
				EditorBaseState.CreateTransformGizmo(levelPlayerNode);
			}
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
		}
		else
		{
			PsState.m_wizardPlayer = _itemMetaData.m_identifier;
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorGameMode(EditorSelectorContext.START_WIZARD));
		}
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x00088FEB File Offset: 0x000873EB
	public override void Execute()
	{
	}
}
