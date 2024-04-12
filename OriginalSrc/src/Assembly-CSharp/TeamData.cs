using System;
using System.Collections.Generic;

// Token: 0x020003EE RID: 1006
public class TeamData
{
	// Token: 0x06001C58 RID: 7256 RVA: 0x00140924 File Offset: 0x0013ED24
	public TeamData(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("id"))
		{
			this.id = (string)_dict["id"];
		}
		if (_dict.ContainsKey("name"))
		{
			this.name = (string)_dict["name"];
		}
		if (_dict.ContainsKey("description"))
		{
			this.description = (string)_dict["description"];
		}
		if (_dict.ContainsKey("joinType"))
		{
			this.joinType = ClientTools.ParseEnum<JoinType>(_dict["joinType"]);
		}
		if (_dict.ContainsKey("requiredTrophies"))
		{
			this.requiredTrophies = Convert.ToInt32(_dict["requiredTrophies"]);
		}
		if (_dict.ContainsKey("memberList"))
		{
			this.memberList = ClientTools.ParsePlayerList(_dict["memberList"] as List<object>);
		}
		if (_dict.ContainsKey("members"))
		{
			this.memberIds = ClientTools.ParsePlayerIds(_dict["members"] as List<object>);
		}
		if (_dict.ContainsKey("memberCount"))
		{
			this.memberCount = Convert.ToInt32(_dict["memberCount"]);
		}
		if (_dict.ContainsKey("score"))
		{
			this.teamScore = Convert.ToInt32(_dict["score"]);
		}
		if (_dict.ContainsKey("seasonReward"))
		{
			this.teamSeasonReward = Convert.ToInt32(_dict["seasonReward"]);
		}
		if (_dict.ContainsKey("topRanked"))
		{
			this.topTenRank = Convert.ToInt32(_dict["topRanked"]);
		}
		if (_dict.ContainsKey("teamRole"))
		{
			this.role = ClientTools.ParseEnum<TeamRole>(_dict["teamRole"]);
		}
		if (_dict.ContainsKey("lastSeasonEndScore"))
		{
			this.lastSeasonEndScore = Convert.ToInt32(_dict["lastSeasonEndScore"]);
		}
		if (_dict.ContainsKey("teamKickReason"))
		{
			this.teamKickReason = (string)_dict["teamKickReason"];
		}
		if (!string.IsNullOrEmpty(this.teamKickReason))
		{
			PsMetagameManager.m_playerStats.m_teamKickReason = this.teamKickReason;
		}
	}

	// Token: 0x06001C59 RID: 7257 RVA: 0x00140B7C File Offset: 0x0013EF7C
	public TeamData()
	{
	}

	// Token: 0x06001C5A RID: 7258 RVA: 0x00140B8C File Offset: 0x0013EF8C
	public void CopyValuesFrom(TeamData _team)
	{
		this.id = _team.id;
		this.name = _team.name;
		this.description = _team.description;
		this.joinType = _team.joinType;
		this.requiredTrophies = _team.requiredTrophies;
		this.memberList = _team.memberList;
		this.teamScore = _team.teamScore;
		this.teamSeasonReward = _team.teamSeasonReward;
		this.memberCount = _team.memberCount;
		this.topTenRank = _team.topTenRank;
		this.role = _team.role;
	}

	// Token: 0x04001E9D RID: 7837
	public string id;

	// Token: 0x04001E9E RID: 7838
	public string name;

	// Token: 0x04001E9F RID: 7839
	public string description;

	// Token: 0x04001EA0 RID: 7840
	public JoinType joinType;

	// Token: 0x04001EA1 RID: 7841
	public int requiredTrophies;

	// Token: 0x04001EA2 RID: 7842
	public PlayerData[] memberList;

	// Token: 0x04001EA3 RID: 7843
	public string[] memberIds;

	// Token: 0x04001EA4 RID: 7844
	public int teamScore;

	// Token: 0x04001EA5 RID: 7845
	public int lastSeasonEndScore;

	// Token: 0x04001EA6 RID: 7846
	public int teamSeasonReward;

	// Token: 0x04001EA7 RID: 7847
	public int memberCount;

	// Token: 0x04001EA8 RID: 7848
	public string teamKickReason;

	// Token: 0x04001EA9 RID: 7849
	public int topTenRank = -1;

	// Token: 0x04001EAA RID: 7850
	public TeamRole role;
}
