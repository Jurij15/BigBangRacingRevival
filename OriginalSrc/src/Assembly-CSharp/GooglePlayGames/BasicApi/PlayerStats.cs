using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005ED RID: 1517
	public class PlayerStats
	{
		// Token: 0x06002C14 RID: 11284 RVA: 0x001BDC82 File Offset: 0x001BC082
		public PlayerStats()
		{
			this.Valid = false;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x001BDC91 File Offset: 0x001BC091
		// (set) Token: 0x06002C16 RID: 11286 RVA: 0x001BDC99 File Offset: 0x001BC099
		public bool Valid { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x001BDCA2 File Offset: 0x001BC0A2
		// (set) Token: 0x06002C18 RID: 11288 RVA: 0x001BDCAA File Offset: 0x001BC0AA
		public int NumberOfPurchases { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x001BDCB3 File Offset: 0x001BC0B3
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x001BDCBB File Offset: 0x001BC0BB
		public float AvgSessonLength { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x001BDCC4 File Offset: 0x001BC0C4
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x001BDCCC File Offset: 0x001BC0CC
		public int DaysSinceLastPlayed { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x001BDCD5 File Offset: 0x001BC0D5
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x001BDCDD File Offset: 0x001BC0DD
		public int NumberOfSessions { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x001BDCE6 File Offset: 0x001BC0E6
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x001BDCEE File Offset: 0x001BC0EE
		public float SessPercentile { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x001BDCF7 File Offset: 0x001BC0F7
		// (set) Token: 0x06002C22 RID: 11298 RVA: 0x001BDCFF File Offset: 0x001BC0FF
		public float SpendPercentile { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x001BDD08 File Offset: 0x001BC108
		// (set) Token: 0x06002C24 RID: 11300 RVA: 0x001BDD10 File Offset: 0x001BC110
		public float SpendProbability { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x001BDD19 File Offset: 0x001BC119
		// (set) Token: 0x06002C26 RID: 11302 RVA: 0x001BDD21 File Offset: 0x001BC121
		public float ChurnProbability { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x001BDD2A File Offset: 0x001BC12A
		// (set) Token: 0x06002C28 RID: 11304 RVA: 0x001BDD32 File Offset: 0x001BC132
		public float HighSpenderProbability { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x001BDD3B File Offset: 0x001BC13B
		// (set) Token: 0x06002C2A RID: 11306 RVA: 0x001BDD43 File Offset: 0x001BC143
		public float TotalSpendNext28Days { get; set; }

		// Token: 0x06002C2B RID: 11307 RVA: 0x001BDD4C File Offset: 0x001BC14C
		public bool HasNumberOfPurchases()
		{
			return this.NumberOfPurchases != (int)PlayerStats.UNSET_VALUE;
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x001BDD5F File Offset: 0x001BC15F
		public bool HasAvgSessonLength()
		{
			return this.AvgSessonLength != PlayerStats.UNSET_VALUE;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x001BDD71 File Offset: 0x001BC171
		public bool HasDaysSinceLastPlayed()
		{
			return this.DaysSinceLastPlayed != (int)PlayerStats.UNSET_VALUE;
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x001BDD84 File Offset: 0x001BC184
		public bool HasNumberOfSessions()
		{
			return this.NumberOfSessions != (int)PlayerStats.UNSET_VALUE;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x001BDD97 File Offset: 0x001BC197
		public bool HasSessPercentile()
		{
			return this.SessPercentile != PlayerStats.UNSET_VALUE;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x001BDDA9 File Offset: 0x001BC1A9
		public bool HasSpendPercentile()
		{
			return this.SpendPercentile != PlayerStats.UNSET_VALUE;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x001BDDBB File Offset: 0x001BC1BB
		public bool HasChurnProbability()
		{
			return this.ChurnProbability != PlayerStats.UNSET_VALUE;
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x001BDDCD File Offset: 0x001BC1CD
		public bool HasHighSpenderProbability()
		{
			return this.HighSpenderProbability != PlayerStats.UNSET_VALUE;
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x001BDDDF File Offset: 0x001BC1DF
		public bool HasTotalSpendNext28Days()
		{
			return this.TotalSpendNext28Days != PlayerStats.UNSET_VALUE;
		}

		// Token: 0x040030C9 RID: 12489
		private static float UNSET_VALUE = -1f;
	}
}
