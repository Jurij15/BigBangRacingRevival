using System;
using Server;
using UnityEngine;

// Token: 0x0200028A RID: 650
public class InitializeMetagameState : BasicState
{
	// Token: 0x06001384 RID: 4996 RVA: 0x000C2E1C File Offset: 0x000C121C
	public override void Enter(IStatedObject _parent)
	{
		this.m_parent = _parent as StartupScene;
		this.m_initialized = false;
		this.m_utilityEntity = EntityManager.AddEntity("UtilityEntity");
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x000C2E44 File Offset: 0x000C1244
	public override void Execute()
	{
		if (LoginFlow.m_userDataLoaded && !this.m_initialized)
		{
			Debug.Log("METAGAME DATA HANDLED", null);
			this.m_initialized = true;
			Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", true), new FadeLoadingScene(Color.black, true, 0.25f));
			ServerManager.m_dontShowLoginPopup = false;
		}
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x000C2EA8 File Offset: 0x000C12A8
	public override void Exit()
	{
		EntityManager.RemoveEntity(this.m_utilityEntity);
	}

	// Token: 0x04001667 RID: 5735
	private StartupScene m_parent;

	// Token: 0x04001668 RID: 5736
	private bool m_initialized;

	// Token: 0x04001669 RID: 5737
	private Entity m_utilityEntity;
}
