using System;
using UnityEngine;

namespace Server
{
	// Token: 0x0200045C RID: 1116
	public class RacingGetData
	{
		// Token: 0x06001EC2 RID: 7874 RVA: 0x0015A0A0 File Offset: 0x001584A0
		public RacingGetData(string _gameId, int[] _secondaryGhostTimeScores = null, int _presetTrophyGhostTimeScore = 0, string _trophyGhostIds = null)
		{
			this.m_gameId = _gameId;
			this.m_trophyGhostIds = _trophyGhostIds;
			this.m_trophyGhostTimes = _presetTrophyGhostTimeScore.ToString();
			for (int i = 0; i < _secondaryGhostTimeScores.Length; i++)
			{
				this.m_trophyGhostTimes = this.m_trophyGhostTimes + "," + _secondaryGhostTimeScores[i];
			}
			this.m_trophyGhostTrophies = (PsMetagameManager.m_playerStats.trophies + 100).ToString();
			this.m_trophyGhostTrophies = this.m_trophyGhostTrophies + "," + PsMetagameManager.m_playerStats.trophies.ToString();
			this.m_trophyGhostTrophies = this.m_trophyGhostTrophies + "," + Mathf.Max(0, PsMetagameManager.m_playerStats.trophies - 100).ToString();
		}

		// Token: 0x0400221D RID: 8733
		public string m_gameId;

		// Token: 0x0400221E RID: 8734
		public string m_trophyGhostIds;

		// Token: 0x0400221F RID: 8735
		public string m_trophyGhostTimes;

		// Token: 0x04002220 RID: 8736
		public string m_trophyGhostTrophies;
	}
}
