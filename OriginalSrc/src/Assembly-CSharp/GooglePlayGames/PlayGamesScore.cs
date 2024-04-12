using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x02000604 RID: 1540
	public class PlayGamesScore : IScore
	{
		// Token: 0x06002D1A RID: 11546 RVA: 0x001BFD34 File Offset: 0x001BE134
		internal PlayGamesScore(DateTime date, string leaderboardId, ulong rank, string playerId, ulong value, string metadata)
		{
			this.mDate = date;
			this.mLbId = this.leaderboardID;
			this.mRank = rank;
			this.mPlayerId = playerId;
			this.mValue = (long)value;
			this.mMetadata = metadata;
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x001BFDA4 File Offset: 0x001BE1A4
		public void ReportScore(Action<bool> callback)
		{
			PlayGamesPlatform.Instance.ReportScore(this.mValue, this.mLbId, this.mMetadata, callback);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x001BFDC3 File Offset: 0x001BE1C3
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x001BFDCB File Offset: 0x001BE1CB
		public string leaderboardID
		{
			get
			{
				return this.mLbId;
			}
			set
			{
				this.mLbId = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x001BFDD4 File Offset: 0x001BE1D4
		// (set) Token: 0x06002D1F RID: 11551 RVA: 0x001BFDDC File Offset: 0x001BE1DC
		public long value
		{
			get
			{
				return this.mValue;
			}
			set
			{
				this.mValue = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x001BFDE5 File Offset: 0x001BE1E5
		public DateTime date
		{
			get
			{
				return this.mDate;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x001BFDED File Offset: 0x001BE1ED
		public string formattedValue
		{
			get
			{
				return this.mValue.ToString();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x001BFE00 File Offset: 0x001BE200
		public string userID
		{
			get
			{
				return this.mPlayerId;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x001BFE08 File Offset: 0x001BE208
		public int rank
		{
			get
			{
				return (int)this.mRank;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x001BFE11 File Offset: 0x001BE211
		public string metaData
		{
			get
			{
				return this.mMetadata;
			}
		}

		// Token: 0x04003147 RID: 12615
		private string mLbId;

		// Token: 0x04003148 RID: 12616
		private long mValue;

		// Token: 0x04003149 RID: 12617
		private ulong mRank;

		// Token: 0x0400314A RID: 12618
		private string mPlayerId = string.Empty;

		// Token: 0x0400314B RID: 12619
		private string mMetadata = string.Empty;

		// Token: 0x0400314C RID: 12620
		private DateTime mDate = new DateTime(1970, 1, 1, 0, 0, 0);
	}
}
