using System;
using System.Collections.Generic;

// Token: 0x020001FD RID: 509
public class SelectUndoAction : UndoAction
{
	// Token: 0x06000EF1 RID: 3825 RVA: 0x0008E5C8 File Offset: 0x0008C9C8
	public SelectUndoAction(List<GraphElement> _oldElements, List<GraphElement> _newElements)
	{
		this.m_oldElements = new uint[_oldElements.Count];
		this.m_newElements = new uint[_newElements.Count];
		for (int i = 0; i < _oldElements.Count; i++)
		{
			this.m_oldElements[i] = _oldElements[i].m_id;
		}
		for (int j = 0; j < _newElements.Count; j++)
		{
			this.m_newElements[j] = _newElements[j].m_id;
		}
		UndoManager.Add(this);
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x0008E65C File Offset: 0x0008CA5C
	public override void ApplyUndo()
	{
		GizmoManager.ClearSelection();
		for (int i = 0; i < this.m_oldElements.Length; i++)
		{
			GraphElement element = LevelManager.m_currentLevel.m_currentLayer.GetElement(this.m_oldElements[i]);
			if (element != null)
			{
				element.Select(true);
				GizmoManager.AddToSelection(element);
			}
		}
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x0008E6B4 File Offset: 0x0008CAB4
	public override void ApplyRedo()
	{
		GizmoManager.ClearSelection();
		for (int i = 0; i < this.m_newElements.Length; i++)
		{
			GraphElement element = LevelManager.m_currentLevel.m_currentLayer.GetElement(this.m_newElements[i]);
			if (element != null)
			{
				element.Select(true);
				GizmoManager.AddToSelection(element);
			}
		}
	}

	// Token: 0x040011D2 RID: 4562
	public uint[] m_oldElements;

	// Token: 0x040011D3 RID: 4563
	public uint[] m_newElements;
}
