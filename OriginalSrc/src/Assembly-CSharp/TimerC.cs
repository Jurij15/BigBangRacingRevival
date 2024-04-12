using System;

// Token: 0x020004D1 RID: 1233
public class TimerC : BasicComponent
{
	// Token: 0x060022E7 RID: 8935 RVA: 0x0019149D File Offset: 0x0018F89D
	public TimerC()
		: base(ComponentType.Timer)
	{
		TimerC.m_componentCount++;
	}

	// Token: 0x060022E8 RID: 8936 RVA: 0x001914B2 File Offset: 0x0018F8B2
	public override void Reset()
	{
		base.Reset();
		this.customComponent = null;
		this.customObject = null;
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x001914C8 File Offset: 0x0018F8C8
	~TimerC()
	{
		TimerC.m_componentCount--;
	}

	// Token: 0x04002964 RID: 10596
	public static int m_componentCount;

	// Token: 0x04002965 RID: 10597
	public string name;

	// Token: 0x04002966 RID: 10598
	public double currentTime;

	// Token: 0x04002967 RID: 10599
	public double startTime;

	// Token: 0x04002968 RID: 10600
	public double duration;

	// Token: 0x04002969 RID: 10601
	public double delay;

	// Token: 0x0400296A RID: 10602
	public bool isDone;

	// Token: 0x0400296B RID: 10603
	public bool destroyEntityOnTimeout;

	// Token: 0x0400296C RID: 10604
	public IComponent customComponent;

	// Token: 0x0400296D RID: 10605
	public object customObject;

	// Token: 0x0400296E RID: 10606
	public bool useUnscaledDeltaTime;

	// Token: 0x0400296F RID: 10607
	public TimerComponentDelegate timeoutHandler;
}
