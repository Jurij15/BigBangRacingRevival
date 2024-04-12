using System;

// Token: 0x020000F0 RID: 240
public class PsMultiTaskAchievement : Achievement
{
	// Token: 0x0600054B RID: 1355 RVA: 0x00045F7A File Offset: 0x0004437A
	public PsMultiTaskAchievement(string _id, string _name, string _description)
		: base(_id, null, null, 1, 0)
	{
		this.m_name = _name;
		this.m_description = _description;
		this.m_value = 0;
		this.m_target = 3;
		base.CheckFlags();
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00045FB4 File Offset: 0x000443B4
	public override void IncrementProgress(int _value)
	{
		if (!base.completed)
		{
			if (_value == 1 && (this.m_value & 1) == 0)
			{
				base.IncrementProgress(_value);
			}
			else if (_value == 2 && (this.m_value & 2) == 0)
			{
				base.IncrementProgress(_value);
			}
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00046011 File Offset: 0x00044411
	public override void Refresh()
	{
		base.Refresh();
	}

	// Token: 0x040006CD RID: 1741
	private bool m_task1;

	// Token: 0x040006CE RID: 1742
	private bool m_task2;

	// Token: 0x020000F1 RID: 241
	public enum Tasks
	{
		// Token: 0x040006D0 RID: 1744
		None,
		// Token: 0x040006D1 RID: 1745
		Task1,
		// Token: 0x040006D2 RID: 1746
		Task2,
		// Token: 0x040006D3 RID: 1747
		Task3 = 4
	}
}
