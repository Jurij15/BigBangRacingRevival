using System;

// Token: 0x02000560 RID: 1376
public interface IState
{
	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06002823 RID: 10275
	// (set) Token: 0x06002824 RID: 10276
	StateMachine m_stateMachine { get; set; }

	// Token: 0x06002825 RID: 10277
	void Enter(IStatedObject _parent);

	// Token: 0x06002826 RID: 10278
	void Execute();

	// Token: 0x06002827 RID: 10279
	void Exit();
}
