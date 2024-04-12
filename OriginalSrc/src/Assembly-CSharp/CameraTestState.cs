using System;

// Token: 0x020005B9 RID: 1465
public class CameraTestState : BasicState
{
	// Token: 0x06002ACF RID: 10959 RVA: 0x001BB3E2 File Offset: 0x001B97E2
	public override void Enter(IStatedObject _parent)
	{
	}

	// Token: 0x06002AD0 RID: 10960 RVA: 0x001BB3E4 File Offset: 0x001B97E4
	public override void Execute()
	{
	}

	// Token: 0x06002AD1 RID: 10961 RVA: 0x001BB3E6 File Offset: 0x001B97E6
	public override void Exit()
	{
		EntityManager.RemoveAllEntities();
	}
}
