using System;

// Token: 0x0200055F RID: 1375
public class BasicState : IState
{
	// Token: 0x170000DD RID: 221
	// (get) Token: 0x0600281E RID: 10270 RVA: 0x00076B97 File Offset: 0x00074F97
	// (set) Token: 0x0600281F RID: 10271 RVA: 0x00076B9F File Offset: 0x00074F9F
	public StateMachine m_stateMachine
	{
		get
		{
			return this._stateMachine;
		}
		set
		{
			this._stateMachine = value;
		}
	}

	// Token: 0x06002820 RID: 10272 RVA: 0x00076BA8 File Offset: 0x00074FA8
	public virtual void Enter(IStatedObject _parent)
	{
	}

	// Token: 0x06002821 RID: 10273 RVA: 0x00076BAA File Offset: 0x00074FAA
	public virtual void Execute()
	{
	}

	// Token: 0x06002822 RID: 10274 RVA: 0x00076BAC File Offset: 0x00074FAC
	public virtual void Exit()
	{
	}

	// Token: 0x04002D7F RID: 11647
	private StateMachine _stateMachine;
}
