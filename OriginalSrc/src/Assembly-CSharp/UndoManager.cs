using System;
using System.Collections.Generic;

// Token: 0x020001FF RID: 511
public static class UndoManager
{
	// Token: 0x06000EF7 RID: 3831 RVA: 0x0008E70C File Offset: 0x0008CB0C
	public static void Undo()
	{
		GizmoManager.Update();
		if (UndoManager.m_steps.Count > UndoManager.m_currentStep)
		{
			Debug.Log("undo: " + UndoManager.m_currentStep, null);
			UndoManager.m_steps[UndoManager.m_currentStep].ApplyUndo();
			UndoManager.m_currentStep++;
		}
		else
		{
			Debug.Log("No steps to undo", null);
		}
		GizmoManager.Update();
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0008E784 File Offset: 0x0008CB84
	public static void Redo()
	{
		GizmoManager.Update();
		if (UndoManager.m_steps.Count >= UndoManager.m_currentStep && UndoManager.m_currentStep > 0)
		{
			UndoManager.m_currentStep--;
			UndoManager.m_steps[UndoManager.m_currentStep].ApplyRedo();
			Debug.Log("redo: " + UndoManager.m_currentStep, null);
		}
		else
		{
			Debug.Log("No steps to redo", null);
		}
		GizmoManager.Update();
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x0008E804 File Offset: 0x0008CC04
	public static void Purge()
	{
		UndoManager.m_steps.Clear();
		UndoManager.m_currentStep = 0;
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x0008E818 File Offset: 0x0008CC18
	public static void Add(UndoAction _step)
	{
		if (UndoManager.m_currentStep > 0)
		{
			UndoManager.m_steps.RemoveRange(0, UndoManager.m_currentStep);
			UndoManager.m_currentStep = 0;
		}
		UndoManager.m_steps.Insert(0, _step);
		if (UndoManager.m_steps.Count > UndoManager.m_maxSteps)
		{
			UndoManager.m_steps.RemoveRange(UndoManager.m_maxSteps, UndoManager.m_steps.Count - UndoManager.m_maxSteps);
		}
	}

	// Token: 0x040011D4 RID: 4564
	public static List<UndoAction> m_steps = new List<UndoAction>();

	// Token: 0x040011D5 RID: 4565
	public static int m_currentStep = 0;

	// Token: 0x040011D6 RID: 4566
	public static int m_maxSteps = 10;
}
