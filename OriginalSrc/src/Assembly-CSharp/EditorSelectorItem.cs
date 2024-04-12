using System;
using UnityEngine;

// Token: 0x020001EE RID: 494
public class EditorSelectorItem : EditorSelectorBase
{
	// Token: 0x06000EB5 RID: 3765 RVA: 0x00088AD1 File Offset: 0x00086ED1
	public EditorSelectorItem(string _categoryName, UIFittedText _notificationAmount)
		: base("insert item", "Select an item to place to your level.", EditorSelectorContext.ITEM_SELECT, _notificationAmount)
	{
		this.m_categoryName = _categoryName;
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x00088AEC File Offset: 0x00086EEC
	public override void Enter(IStatedObject _parent)
	{
		if (EditorSelectorItem.cardColors == null)
		{
			EditorSelectorItem.cardColors = new Color[]
			{
				PsColors.objectCatCardBlue,
				PsColors.objectCatCardGreen,
				PsColors.objectCatCardRed
			};
		}
		if (this.m_categoryName == string.Empty)
		{
			this.m_categoryName = PsMetagameData.m_units[0].m_name;
			PsState.m_editorLastSelectedItemCategory = this.m_categoryName;
		}
		base.Enter(_parent);
		for (int i = 0; i < PsMetagameData.m_units.Count; i++)
		{
			PsUnlockableCategory psUnlockableCategory = PsMetagameData.m_units[i];
			if (psUnlockableCategory.m_name == this.m_categoryName)
			{
				for (int j = 0; j < psUnlockableCategory.m_items.Count; j++)
				{
					PsEditorItem psEditorItem = psUnlockableCategory.m_items[j] as PsEditorItem;
					Color color;
					if (i < EditorSelectorItem.cardColors.Length)
					{
						color = EditorSelectorItem.cardColors[i];
					}
					else
					{
						color = PsColors.objectCatCardGrey;
					}
					this.AddItemCard(psEditorItem.m_name, psEditorItem.m_description, color, psEditorItem);
				}
				break;
			}
		}
		this.m_selector.Update();
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x00088C3C File Offset: 0x0008703C
	public override void ItemTouchHandler(PsEditorItem _itemMetaData)
	{
		Vector3 vector = CameraS.m_mainCamera.transform.position;
		vector.z = 0f;
		Vector3 vector2 = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
		vector += vector2;
		Vector3 zero = Vector3.zero;
		Vector3 one = Vector3.one;
		string graphNodeClassName = _itemMetaData.m_graphNodeClassName;
		string identifier = _itemMetaData.m_identifier;
		if (PsMetagameData.CanPlaceUnit(identifier))
		{
			object[] array = new object[]
			{
				GraphNodeType.Normal,
				Type.GetType(identifier),
				identifier,
				vector,
				zero,
				one
			};
			GraphElement graphElement = (GraphElement)Activator.CreateInstance(Type.GetType(graphNodeClassName), array);
			LevelManager.m_currentLevel.m_currentLayer.AddElement(graphElement);
			graphElement.Assemble();
			if (graphElement != null)
			{
				EditorBaseState.CreateTransformGizmo(graphElement);
			}
			if (PsState.UsingEditorResources())
			{
				EditorScene.CumulateReservedResources(_itemMetaData.m_identifier, -1);
			}
			LevelManager.m_currentLevel.ItemChanged();
			int newEditorItemCount = PsState.m_newEditorItemCount;
			PsMetagameManager.RemoveNewEditorItem(identifier);
			if (newEditorItemCount != PsState.m_newEditorItemCount && this.m_notificationAmount != null)
			{
				int num = Convert.ToInt32(this.m_notificationAmount.m_tmc.m_textMesh.text);
				num--;
				if (num > 0)
				{
					TextMeshS.SetText(this.m_notificationAmount.m_tmc, num.ToString(), true);
				}
				else
				{
					UIComponent root = this.m_notificationAmount.GetRoot();
					this.m_notificationAmount.m_parent.m_parent.Destroy();
					root.Update();
				}
			}
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
		}
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x00088E0B File Offset: 0x0008720B
	public override void Execute()
	{
	}

	// Token: 0x040011A6 RID: 4518
	private static Color[] cardColors;

	// Token: 0x040011A7 RID: 4519
	private string m_categoryName;
}
