using System;
using System.Collections;

namespace Server
{
	// Token: 0x0200045B RID: 1115
	public class OpponentData
	{
		// Token: 0x06001EC0 RID: 7872 RVA: 0x0015A04B File Offset: 0x0015844B
		public OpponentData(string _ghostId, int _trophyChange)
		{
			this.m_ghostId = _ghostId;
			this.m_trophyChange = _trophyChange;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0015A064 File Offset: 0x00158464
		public Hashtable TojsonHashtable()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("ghostId", this.m_ghostId);
			hashtable.Add("trophyChange", this.m_trophyChange);
			return hashtable;
		}

		// Token: 0x0400221B RID: 8731
		public string m_ghostId;

		// Token: 0x0400221C RID: 8732
		public int m_trophyChange;
	}
}
