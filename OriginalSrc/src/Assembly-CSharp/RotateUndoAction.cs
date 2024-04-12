using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FC RID: 508
public class RotateUndoAction : UndoAction
{
	// Token: 0x06000EEE RID: 3822 RVA: 0x0008E470 File Offset: 0x0008C870
	public RotateUndoAction(List<GraphElement> _elements, List<Vector3> _originalRotations)
	{
		this.m_elements = new uint[_elements.Count];
		this.m_newRotations = new Vector3[_elements.Count];
		this.m_originalRotations = _originalRotations.ToArray();
		for (int i = 0; i < _elements.Count; i++)
		{
			this.m_elements[i] = _elements[i].m_id;
			this.m_newRotations[i] = _elements[i].m_rotation;
		}
		UndoManager.Add(this);
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x0008E500 File Offset: 0x0008C900
	public override void ApplyUndo()
	{
		for (int i = 0; i < this.m_elements.Length; i++)
		{
			GraphElement element = LevelManager.m_currentLevel.m_currentLayer.GetElement(this.m_elements[i]);
			if (element != null)
			{
				element.m_rotation = this.m_originalRotations[i];
				element.Reset();
			}
		}
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x0008E564 File Offset: 0x0008C964
	public override void ApplyRedo()
	{
		for (int i = 0; i < this.m_elements.Length; i++)
		{
			GraphElement element = LevelManager.m_currentLevel.m_currentLayer.GetElement(this.m_elements[i]);
			if (element != null)
			{
				element.m_rotation = this.m_newRotations[i];
				element.Reset();
			}
		}
	}

	// Token: 0x040011CF RID: 4559
	public uint[] m_elements;

	// Token: 0x040011D0 RID: 4560
	public Vector3[] m_originalRotations;

	// Token: 0x040011D1 RID: 4561
	public Vector3[] m_newRotations;
}
