using System;
using System.Collections.Generic;

namespace Server
{
	// Token: 0x02000427 RID: 1063
	public static class ResetFlow
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00150236 File Offset: 0x0014E636
		public static bool IsActive
		{
			get
			{
				return ResetFlow.m_active;
			}
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00150240 File Offset: 0x0014E640
		public static void Start(bool removeOld = true)
		{
			ResetFlow.m_active = true;
			PsState.m_inLoginFlow = false;
			TouchAreaS.Disable();
			PlanetTools.ResetLocalData();
			PsMetagameData.Clear();
			PsMetagamePlayerData.m_playerData.Clear();
			PsGachaManager.Initialize();
			PsUpgradeManager.Initialize();
			PsCaches.Initialize();
			PsMetagameManager.m_playerStats.upgrades.Clear();
			if (removeOld)
			{
				Player.SwitchPlayer(new Action<HttpC>(ResetFlow.SwitchOk), new Action<HttpC>(ResetFlow.SwitchFailed), null);
			}
			else
			{
				ResetFlow.Done();
			}
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x001502E4 File Offset: 0x0014E6E4
		private static void Done()
		{
			PlayerPrefsX.DeleteAll();
			PlayerPrefsX.SetGameCenterId(string.Empty);
			PlayerPrefsX.SetUserId(string.Empty);
			TouchAreaS.Enable();
			ServerManager.m_playerAuthenticated = false;
			ResetFlow.m_active = false;
			Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), null);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00150338 File Offset: 0x0014E738
		private static void SwitchOk(HttpC _c)
		{
			PlayerPrefsX.DeleteAll();
			PlayerPrefsX.SetGameCenterId(string.Empty);
			Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
			Debug.LogError("ID from serveR: " + (string)dictionary["id"]);
			PlayerPrefsX.SetUserId((string)dictionary["id"]);
			PlayerPrefsX.SetSession((string)dictionary["sessionId"]);
			TouchAreaS.Enable();
			ServerManager.m_playerAuthenticated = false;
			ResetFlow.m_active = false;
			Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), null);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x001503DC File Offset: 0x0014E7DC
		private static void SwitchFailed(HttpC _c)
		{
			ResetFlow.m_active = false;
			Player.SwitchPlayer(new Action<HttpC>(ResetFlow.SwitchOk), new Action<HttpC>(ResetFlow.SwitchFailed), null);
		}

		// Token: 0x04002054 RID: 8276
		private static bool m_active;
	}
}
