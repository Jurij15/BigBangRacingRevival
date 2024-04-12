using System;

// Token: 0x020001F9 RID: 505
public class FlipUndoAction : UndoAction
{
	// Token: 0x06000EE5 RID: 3813 RVA: 0x0008E1A0 File Offset: 0x0008C5A0
	public FlipUndoAction(GraphElement[] _elements)
	{
		this.m_elements = new uint[_elements.Length];
		for (int i = 0; i < _elements.Length; i++)
		{
			this.m_elements[i] = _elements[i].m_id;
		}
		UndoManager.Add(this);
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0008E1EB File Offset: 0x0008C5EB
	public override void ApplyUndo()
	{
		Debug.LogWarning("not implemented");
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x0008E1F7 File Offset: 0x0008C5F7
	public override void ApplyRedo()
	{
		Debug.LogWarning("not implemented");
	}

	// Token: 0x040011C9 RID: 4553
	public uint[] m_elements;
}
