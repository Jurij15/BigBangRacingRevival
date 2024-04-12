using System;

namespace Server
{
	// Token: 0x0200045A RID: 1114
	public class RacingGhostData
	{
		// Token: 0x06001EBF RID: 7871 RVA: 0x0015A00E File Offset: 0x0015840E
		public RacingGhostData(string _gameId, string _ghostId, int _time, int _upgradeSum, string _playerUnit, DataBlob _ghostData, bool _final = false)
		{
			this.m_gameId = _gameId;
			this.m_ghostId = _ghostId;
			this.m_time = _time;
			this.m_upgradeSum = _upgradeSum;
			this.m_playerUnit = _playerUnit;
			this.m_ghostData = _ghostData;
			this.m_final = _final;
		}

		// Token: 0x04002213 RID: 8723
		public string m_gameId;

		// Token: 0x04002214 RID: 8724
		public string m_ghostId;

		// Token: 0x04002215 RID: 8725
		public int m_time;

		// Token: 0x04002216 RID: 8726
		public int m_upgradeSum;

		// Token: 0x04002217 RID: 8727
		public string m_playerUnit;

		// Token: 0x04002218 RID: 8728
		public DataBlob m_ghostData;

		// Token: 0x04002219 RID: 8729
		public int m_ghostSize;

		// Token: 0x0400221A RID: 8730
		public bool m_final;
	}
}
