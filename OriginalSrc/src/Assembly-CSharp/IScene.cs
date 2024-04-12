using System;

// Token: 0x0200055B RID: 1371
public interface IScene
{
	// Token: 0x170000DA RID: 218
	// (get) Token: 0x060027FD RID: 10237
	// (set) Token: 0x060027FE RID: 10238
	string m_name { get; set; }

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060027FF RID: 10239
	// (set) Token: 0x06002800 RID: 10240
	StateMachine m_stateMachine { get; set; }

	// Token: 0x06002801 RID: 10241
	IState GetCurrentState();

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06002802 RID: 10242
	bool m_initComplete { get; }

	// Token: 0x06002803 RID: 10243
	void Load();

	// Token: 0x06002804 RID: 10244
	void Initialize();

	// Token: 0x06002805 RID: 10245
	void Reset();

	// Token: 0x06002806 RID: 10246
	void Update();

	// Token: 0x06002807 RID: 10247
	void Destroy();
}
