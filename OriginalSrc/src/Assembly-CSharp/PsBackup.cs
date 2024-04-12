using System;
using System.Collections.Generic;
using MiniJSON;
using Server;

// Token: 0x02000002 RID: 2
public abstract class PsBackup
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000450
	public PsBackup(string _fileName)
	{
		this.m_fileName = _fileName;
		this.ReadBackup();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002065 File Offset: 0x00000465
	protected string GetFileName()
	{
		return this.m_fileName;
	}

	// Token: 0x06000003 RID: 3
	public abstract string GetServiceName();

	// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000470
	protected PsBackup.Player ParsePlayerFromBackupJSON(string _json)
	{
		PsBackup.Player player = default(PsBackup.Player);
		if (string.IsNullOrEmpty(_json))
		{
			return player;
		}
		try
		{
			Dictionary<string, object> dictionary = Json.Deserialize(_json) as Dictionary<string, object>;
			if (dictionary.ContainsKey("i") && dictionary.ContainsKey("h"))
			{
				string text = (string)dictionary["i"];
				string text2 = (string)dictionary["h"];
				if (ClientTools.GenerateHash(text) == text2)
				{
					player.userId = (string)dictionary["i"];
					if (dictionary.ContainsKey("n"))
					{
						player.userName = (string)dictionary["n"];
					}
					if (dictionary.ContainsKey("tn"))
					{
						player.teamName = (string)dictionary["tn"];
					}
					return player;
				}
			}
		}
		catch
		{
			Debug.LogError("JSON was broken: " + _json);
		}
		return player;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002190 File Offset: 0x00000590
	private string CreateBackUpJSON(PlayerData _data)
	{
		string playerId = _data.playerId;
		string text = ClientTools.GenerateHash(_data.playerId);
		string name = _data.name;
		string teamName = _data.teamName;
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("i", playerId);
		dictionary.Add("h", text);
		if (!string.IsNullOrEmpty(name))
		{
			dictionary.Add("n", name);
		}
		if (!string.IsNullOrEmpty(teamName))
		{
			dictionary.Add("tn", teamName);
		}
		string text2 = Json.Serialize(dictionary);
		Debug.Log("Created backup json: " + text2, null);
		return text2;
	}

	// Token: 0x06000006 RID: 6
	public abstract void ReadBackup();

	// Token: 0x06000007 RID: 7
	protected abstract void SetBackup(string _json);

	// Token: 0x06000008 RID: 8
	public abstract void Clear();

	// Token: 0x06000009 RID: 9 RVA: 0x00002230 File Offset: 0x00000630
	protected void SetPlayer(string _json)
	{
		this.m_player = this.ParsePlayerFromBackupJSON(_json);
		if (!string.IsNullOrEmpty(this.m_player.userId) && this.m_player.userId != PlayerPrefsX.GetUserId())
		{
			this.GetPlayerProfile(this.m_player.userId);
		}
		else
		{
			this.m_status = PsBackup.Status.Ready;
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002298 File Offset: 0x00000698
	private void GetPlayerProfile(string _userId)
	{
		HttpC playerProfile = global::Server.Player.GetPlayerProfile(_userId, new Action<PlayerData>(this.GetPlayerProfileSucceed), new Action<HttpC>(this.GetPlayerProfileFailed), new Action(this.GetPlayerProfileError));
		playerProfile.objectData = _userId;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000022D7 File Offset: 0x000006D7
	private void GetPlayerProfileSucceed(PlayerData _playerData)
	{
		this.m_playerData = _playerData;
		this.m_status = PsBackup.Status.Ready;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000022E7 File Offset: 0x000006E7
	private void GetPlayerProfileFailed(HttpC _c)
	{
		Debug.LogError("Failure: Playerdata get failure");
		this.GetPlayerProfile((string)_c.objectData);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002304 File Offset: 0x00000704
	private void GetPlayerProfileError()
	{
		Debug.LogError("Error: invalid userid");
		this.m_player = default(PsBackup.Player);
		this.m_status = PsBackup.Status.Ready;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002334 File Offset: 0x00000734
	public void SaveBackup()
	{
		this.SaveBackup(new PlayerData
		{
			playerId = PlayerPrefsX.GetUserId(),
			name = PlayerPrefsX.GetUserName(),
			teamName = PlayerPrefsX.GetTeamName(),
			playerId = PlayerPrefsX.GetUserId()
		});
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002380 File Offset: 0x00000780
	public void SaveBackup(PlayerData _data)
	{
		this.m_status = PsBackup.Status.Loading;
		string text = this.CreateBackUpJSON(_data);
		this.m_player = default(PsBackup.Player);
		this.m_playerData = default(PlayerData);
		this.SetBackup(text);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000023C1 File Offset: 0x000007C1
	public PsBackup.Player GetPlayer()
	{
		return this.m_player;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000023C9 File Offset: 0x000007C9
	public string GetBackup()
	{
		return this.m_json;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000023D1 File Offset: 0x000007D1
	public PsBackup.Status GetStatus()
	{
		return this.m_status;
	}

	// Token: 0x04000001 RID: 1
	private const string ID = "i";

	// Token: 0x04000002 RID: 2
	private const string HASH = "h";

	// Token: 0x04000003 RID: 3
	private const string NAME = "n";

	// Token: 0x04000004 RID: 4
	private const string TEAM_NAME = "tn";

	// Token: 0x04000005 RID: 5
	private string m_fileName;

	// Token: 0x04000006 RID: 6
	protected string m_json;

	// Token: 0x04000007 RID: 7
	protected PsBackup.Player m_player;

	// Token: 0x04000008 RID: 8
	protected PsBackup.Status m_status;

	// Token: 0x04000009 RID: 9
	public PlayerData m_playerData;

	// Token: 0x02000003 RID: 3
	public enum Status
	{
		// Token: 0x0400000B RID: 11
		Loading,
		// Token: 0x0400000C RID: 12
		Ready
	}

	// Token: 0x02000004 RID: 4
	public struct Player
	{
		// Token: 0x0400000D RID: 13
		public string userId;

		// Token: 0x0400000E RID: 14
		public string userName;

		// Token: 0x0400000F RID: 15
		public string teamName;

		// Token: 0x04000010 RID: 16
		public string hash;
	}
}
