using System;
using System.Collections.Generic;

// Token: 0x020001F8 RID: 504
public class CopyUndoAction : UndoAction
{
	// Token: 0x06000EE2 RID: 3810 RVA: 0x0008E024 File Offset: 0x0008C424
	public CopyUndoAction(GraphElement[] _originalElements, GraphElement[] _copiedElements)
	{
		this.m_elements = _copiedElements;
		this.m_elementIDs = new uint[_copiedElements.Length];
		this.m_originalElementIDs = new uint[_originalElements.Length];
		for (int i = 0; i < _copiedElements.Length; i++)
		{
			this.m_elementIDs[i] = _copiedElements[i].m_id;
			this.m_originalElementIDs[i] = _originalElements[i].m_id;
		}
		UndoManager.Add(this);
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0008E094 File Offset: 0x0008C494
	public override void ApplyUndo()
	{
		Debug.Log("undo copy", null);
		for (int i = 0; i < this.m_elements.Length; i++)
		{
			GraphElement element = LevelManager.m_currentLevel.m_currentLayer.GetElement(this.m_elementIDs[i]);
			if (element != null)
			{
				LevelManager.m_currentLevel.m_currentLayer.RemoveElement(element);
				element.Dispose();
			}
		}
		GizmoManager.ClearSelection();
		for (int j = 0; j < this.m_originalElementIDs.Length; j++)
		{
			GraphElement element2 = LevelManager.m_currentLevel.m_currentLayer.GetElement(this.m_originalElementIDs[j]);
			GizmoManager.AddToSelection(element2);
		}
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0008E138 File Offset: 0x0008C538
	public override void ApplyRedo()
	{
		Debug.Log("redo copy", null);
		List<GraphElement> list = new List<GraphElement>();
		for (int i = 0; i < this.m_elements.Length; i++)
		{
			GraphElement graphElement = this.m_elements[i].DeepCopy();
			LevelManager.m_currentLevel.m_currentLayer.AddElement(graphElement);
			graphElement.Assemble();
			list.Add(graphElement);
		}
		GizmoManager.SetSelection(list);
	}

	// Token: 0x040011C6 RID: 4550
	public GraphElement[] m_elements;

	// Token: 0x040011C7 RID: 4551
	public uint[] m_originalElementIDs;

	// Token: 0x040011C8 RID: 4552
	public uint[] m_elementIDs;
}
