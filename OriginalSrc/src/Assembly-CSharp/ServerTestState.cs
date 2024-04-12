using System;

// Token: 0x020005BC RID: 1468
public class ServerTestState : BasicState
{
	// Token: 0x06002ADE RID: 10974 RVA: 0x001BC06A File Offset: 0x001BA46A
	public override void Enter(IStatedObject _parent)
	{
		this.save();
		this.loadAll();
	}

	// Token: 0x06002ADF RID: 10975 RVA: 0x001BC078 File Offset: 0x001BA478
	private void save()
	{
	}

	// Token: 0x06002AE0 RID: 10976 RVA: 0x001BC07A File Offset: 0x001BA47A
	private void loadAll()
	{
	}

	// Token: 0x06002AE1 RID: 10977 RVA: 0x001BC07C File Offset: 0x001BA47C
	private void Handler(TLTouch _t, bool _secondary)
	{
	}

	// Token: 0x06002AE2 RID: 10978 RVA: 0x001BC07E File Offset: 0x001BA47E
	public override void Execute()
	{
	}

	// Token: 0x06002AE3 RID: 10979 RVA: 0x001BC080 File Offset: 0x001BA480
	public override void Exit()
	{
		EntityManager.RemoveAllEntities();
	}
}
