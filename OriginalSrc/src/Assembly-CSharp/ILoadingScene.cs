using System;

// Token: 0x0200055A RID: 1370
public interface ILoadingScene
{
	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x060027F0 RID: 10224
	// (set) Token: 0x060027F1 RID: 10225
	StateMachine StateMachine { get; set; }

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060027F2 RID: 10226
	// (set) Token: 0x060027F3 RID: 10227
	IScene FromScene { get; set; }

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x060027F4 RID: 10228
	// (set) Token: 0x060027F5 RID: 10229
	IScene ToScene { get; set; }

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060027F6 RID: 10230
	bool InitComplete { get; }

	// Token: 0x060027F7 RID: 10231
	void Load();

	// Token: 0x060027F8 RID: 10232
	void Update();

	// Token: 0x060027F9 RID: 10233
	void Destroy();

	// Token: 0x060027FA RID: 10234
	void StartOutro();

	// Token: 0x060027FB RID: 10235
	bool IntroComplete();

	// Token: 0x060027FC RID: 10236
	bool OutroComplete();
}
