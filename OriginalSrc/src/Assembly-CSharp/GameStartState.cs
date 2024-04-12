using System;

// Token: 0x02000215 RID: 533
public class GameStartState : BasicState
{
	// Token: 0x06000F6C RID: 3948 RVA: 0x000918DE File Offset: 0x0008FCDE
	public override void Enter(IStatedObject _parent)
	{
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x000918E0 File Offset: 0x0008FCE0
	public override void Execute()
	{
		if (!this.m_cameraIsSet)
		{
			GraphElement element = LevelManager.m_currentLevel.m_currentLayer.GetElement("Player");
			if (element != null)
			{
				CameraS.SnapMainCameraFrame();
				this.m_cameraIsSet = true;
			}
		}
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x0009191F File Offset: 0x0008FD1F
	public override void Exit()
	{
	}

	// Token: 0x04001237 RID: 4663
	private bool m_cameraIsSet;
}
