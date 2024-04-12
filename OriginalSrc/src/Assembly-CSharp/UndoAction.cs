using System;

// Token: 0x020001FE RID: 510
public class UndoAction
{
	// Token: 0x06000EF5 RID: 3829 RVA: 0x0008E007 File Offset: 0x0008C407
	public virtual void ApplyUndo()
	{
		Debug.Log("undo", null);
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x0008E014 File Offset: 0x0008C414
	public virtual void ApplyRedo()
	{
		Debug.Log("redo", null);
	}
}
