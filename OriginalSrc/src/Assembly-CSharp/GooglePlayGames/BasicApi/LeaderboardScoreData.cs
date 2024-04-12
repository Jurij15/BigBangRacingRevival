using System;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005D3 RID: 1491
	public class LeaderboardScoreData
	{
		// Token: 0x06002B67 RID: 11111 RVA: 0x001BD059 File Offset: 0x001BB459
		internal LeaderboardScoreData(string leaderboardId)
		{
			this.mId = leaderboardId;
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x001BD073 File Offset: 0x001BB473
		internal LeaderboardScoreData(string leaderboardId, ResponseStatus status)
		{
			this.mId = leaderboardId;
			this.mStatus = status;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x001BD094 File Offset: 0x001BB494
		public bool Valid
		{
			get
			{
				return this.mStatus == ResponseStatus.Success || this.mStatus == ResponseStatus.SuccessWithStale;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x001BD0AE File Offset: 0x001BB4AE
		// (set) Token: 0x06002B6B RID: 11115 RVA: 0x001BD0B6 File Offset: 0x001BB4B6
		public ResponseStatus Status
		{
			get
			{
				return this.mStatus;
			}
			internal set
			{
				this.mStatus = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x001BD0BF File Offset: 0x001BB4BF
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x001BD0C7 File Offset: 0x001BB4C7
		public ulong ApproximateCount
		{
			get
			{
				return this.mApproxCount;
			}
			internal set
			{
				this.mApproxCount = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x001BD0D0 File Offset: 0x001BB4D0
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x001BD0D8 File Offset: 0x001BB4D8
		public string Title
		{
			get
			{
				return this.mTitle;
			}
			internal set
			{
				this.mTitle = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x001BD0E1 File Offset: 0x001BB4E1
		// (set) Token: 0x06002B71 RID: 11121 RVA: 0x001BD0E9 File Offset: 0x001BB4E9
		public string Id
		{
			get
			{
				return this.mId;
			}
			internal set
			{
				this.mId = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x001BD0F2 File Offset: 0x001BB4F2
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x001BD0FA File Offset: 0x001BB4FA
		public IScore PlayerScore
		{
			get
			{
				return this.mPlayerScore;
			}
			internal set
			{
				this.mPlayerScore = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x001BD103 File Offset: 0x001BB503
		public IScore[] Scores
		{
			get
			{
				return this.mScores.ToArray();
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x001BD110 File Offset: 0x001BB510
		internal int AddScore(PlayGamesScore score)
		{
			this.mScores.Add(score);
			return this.mScores.Count;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x001BD129 File Offset: 0x001BB529
		// (set) Token: 0x06002B77 RID: 11127 RVA: 0x001BD131 File Offset: 0x001BB531
		public ScorePageToken PrevPageToken
		{
			get
			{
				return this.mPrevPage;
			}
			internal set
			{
				this.mPrevPage = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x001BD13A File Offset: 0x001BB53A
		// (set) Token: 0x06002B79 RID: 11129 RVA: 0x001BD142 File Offset: 0x001BB542
		public ScorePageToken NextPageToken
		{
			get
			{
				return this.mNextPage;
			}
			internal set
			{
				this.mNextPage = value;
			}
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x001BD14B File Offset: 0x001BB54B
		public override string ToString()
		{
			return string.Format("[LeaderboardScoreData: mId={0},  mStatus={1}, mApproxCount={2}, mTitle={3}]", new object[] { this.mId, this.mStatus, this.mApproxCount, this.mTitle });
		}

		// Token: 0x0400306C RID: 12396
		private string mId;

		// Token: 0x0400306D RID: 12397
		private ResponseStatus mStatus;

		// Token: 0x0400306E RID: 12398
		private ulong mApproxCount;

		// Token: 0x0400306F RID: 12399
		private string mTitle;

		// Token: 0x04003070 RID: 12400
		private IScore mPlayerScore;

		// Token: 0x04003071 RID: 12401
		private ScorePageToken mPrevPage;

		// Token: 0x04003072 RID: 12402
		private ScorePageToken mNextPage;

		// Token: 0x04003073 RID: 12403
		private List<PlayGamesScore> mScores = new List<PlayGamesScore>();
	}
}
