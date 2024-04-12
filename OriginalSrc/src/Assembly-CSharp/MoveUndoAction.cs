using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FA RID: 506
public class MoveUndoAction : UndoAction
{
	// Token: 0x06000EE8 RID: 3816 RVA: 0x0008E204 File Offset: 0x0008C604
	public MoveUndoAction(List<GraphElement> _elements, List<Vector3> _originalPositions)
	{
		this.m_elements = new uint[_elements.Count];
		this.m_newPositions = new Vector3[_elements.Count];
		this.m_originalPositions = _originalPositions.ToArray();
		for (int i = 0; i < _elements.Count; i++)
		{
			this.m_elements[i] = _elements[i].m_id;
			this.m_newPositions[i] = _elements[i].m_position;
		}
		UndoManager.Add(this);
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0008E294 File Offset: 0x0008C694
	public override void ApplyUndo()
	{
		for (int i = 0; i < this.m_elements.Length; i++)
		{
			GraphElement element = LevelManager.m_currentLevel.m_currentLayer.GetElement(this.m_elements[i]);
			if (element != null)
			{
				element.m_position = this.m_originalPositions[i];
				element.Reset();
			}
		}
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x0008E2F8 File Offset: 0x0008C6F8
	public override void ApplyRedo()
	{
		for (int i = 0; i < this.m_elements.Length; i++)
		{
			GraphElement element = LevelManager.m_currentLevel.m_currentLayer.GetElement(this.m_elements[i]);
			if (element != null)
			{
				element.m_position = this.m_newPositions[i];
				element.Reset();
			}
		}
	}

	// Token: 0x040011CA RID: 4554
	public uint[] m_elements;

	// Token: 0x040011CB RID: 4555
	public Vector3[] m_originalPositions;

	// Token: 0x040011CC RID: 4556
	public Vector3[] m_newPositions;
}
