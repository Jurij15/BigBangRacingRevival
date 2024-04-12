using System;
using System.Collections.Generic;

// Token: 0x020001FB RID: 507
public class RemoveUndoAction : UndoAction
{
	// Token: 0x06000EEB RID: 3819 RVA: 0x0008E35C File Offset: 0x0008C75C
	public RemoveUndoAction(GraphElement[] _elements)
	{
		this.m_elements = _elements;
		this.m_elementIDs = new uint[_elements.Length];
		for (int i = 0; i < _elements.Length; i++)
		{
			this.m_elementIDs[i] = _elements[i].m_id;
		}
		UndoManager.Add(this);
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x0008E3B0 File Offset: 0x0008C7B0
	public override void ApplyUndo()
	{
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

	// Token: 0x06000EED RID: 3821 RVA: 0x0008E410 File Offset: 0x0008C810
	public override void ApplyRedo()
	{
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
	}

	// Token: 0x040011CD RID: 4557
	public GraphElement[] m_elements;

	// Token: 0x040011CE RID: 4558
	public uint[] m_elementIDs;
}
