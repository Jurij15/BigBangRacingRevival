using System;
using Server;

// Token: 0x0200028B RID: 651
public class UserLoginState : BasicState
{
	// Token: 0x06001388 RID: 5000 RVA: 0x000C2EC0 File Offset: 0x000C12C0
	public override void Enter(IStatedObject _parent)
	{
		this.m_parent = _parent as StartupScene;
		PsMetagameData.Initialize();
		if (PsPlanetManager.GetCurrentPlanet() == null)
		{
			PsPlanetManager.AddPlanet(new PsPlanet());
		}
		PlanetTools.LoadPersistentPlanetData();
		if (PsPlanetManager.GetCurrentPlanet().m_planetData == null)
		{
			string currentPlanetIdentifier = PsMainMenuState.GetCurrentPlanetIdentifier();
			if (!string.IsNullOrEmpty(currentPlanetIdentifier) && PlanetTools.m_planetProgressionInfos.ContainsKey(currentPlanetIdentifier))
			{
				Debug.LogWarning("Last planet was: " + currentPlanetIdentifier);
				PsPlanetManager.GetCurrentPlanet().SetPlanetData(PlanetTools.m_planetProgressionInfos[currentPlanetIdentifier]);
			}
			else if (PlanetTools.m_planetProgressionInfos.ContainsKey("AdventureOffroadCar"))
			{
				Debug.LogError("Setting Adventure planet");
				PsPlanetManager.GetCurrentPlanet().SetPlanetData(PlanetTools.m_planetProgressionInfos["AdventureOffroadCar"]);
			}
			else
			{
				Debug.Log("No suitable planet found on device", null);
			}
		}
		this.m_loginComplete = false;
		ServerManager.Login();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x000C2FA8 File Offset: 0x000C13A8
	public override void Execute()
	{
		if (ServerManager.m_playerAuthenticated && LoginFlow.isCompleted && !this.m_loginComplete)
		{
			this.m_loginComplete = true;
			PsState.m_inStartupSequence = false;
			this.m_parent.m_stateMachine.ChangeState(new InitializeMetagameState());
		}
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000C2FF6 File Offset: 0x000C13F6
	public override void Exit()
	{
	}

	// Token: 0x0400166A RID: 5738
	private StartupScene m_parent;

	// Token: 0x0400166B RID: 5739
	public bool m_loginComplete;
}
