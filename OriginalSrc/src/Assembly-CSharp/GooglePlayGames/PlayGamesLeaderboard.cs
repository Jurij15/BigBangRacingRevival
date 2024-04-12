using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x02000601 RID: 1537
	public class PlayGamesLeaderboard : ILeaderboard
	{
		// Token: 0x06002CB0 RID: 11440 RVA: 0x001BE74E File Offset: 0x001BCB4E
		public PlayGamesLeaderboard(string id)
		{
			this.mId = id;
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x001BE768 File Offset: 0x001BCB68
		public void SetUserFilter(string[] userIDs)
		{
			this.mFilteredUserIds = userIDs;
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x001BE771 File Offset: 0x001BCB71
		public void LoadScores(Action<bool> callback)
		{
			PlayGamesPlatform.Instance.LoadScores(this, callback);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x001BE77F File Offset: 0x001BCB7F
		// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x001BE787 File Offset: 0x001BCB87
		public bool loading
		{
			get
			{
				return this.mLoading;
			}
			internal set
			{
				this.mLoading = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x001BE790 File Offset: 0x001BCB90
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x001BE798 File Offset: 0x001BCB98
		public string id
		{
			get
			{
				return this.mId;
			}
			set
			{
				this.mId = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x001BE7A1 File Offset: 0x001BCBA1
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x001BE7A9 File Offset: 0x001BCBA9
		public UserScope userScope
		{
			get
			{
				return this.mUserScope;
			}
			set
			{
				this.mUserScope = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x001BE7B2 File Offset: 0x001BCBB2
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x001BE7BA File Offset: 0x001BCBBA
		public Range range
		{
			get
			{
				return this.mRange;
			}
			set
			{
				this.mRange = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x001BE7C3 File Offset: 0x001BCBC3
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x001BE7CB File Offset: 0x001BCBCB
		public TimeScope timeScope
		{
			get
			{
				return this.mTimeScope;
			}
			set
			{
				this.mTimeScope = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x001BE7D4 File Offset: 0x001BCBD4
		public IScore localUserScore
		{
			get
			{
				return this.mLocalUserScore;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x001BE7DC File Offset: 0x001BCBDC
		public uint maxRange
		{
			get
			{
				return this.mMaxRange;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x001BE7E4 File Offset: 0x001BCBE4
		public IScore[] scores
		{
			get
			{
				PlayGamesScore[] array = new PlayGamesScore[this.mScoreList.Count];
				this.mScoreList.CopyTo(array);
				return array;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x001BE80F File Offset: 0x001BCC0F
		public string title
		{
			get
			{
				return this.mTitle;
			}
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x001BE818 File Offset: 0x001BCC18
		internal bool SetFromData(LeaderboardScoreData data)
		{
			if (data.Valid)
			{
				Debug.Log("Setting leaderboard from: " + data);
				this.SetMaxRange(data.ApproximateCount);
				this.SetTitle(data.Title);
				this.SetLocalUserScore((PlayGamesScore)data.PlayerScore);
				foreach (IScore score in data.Scores)
				{
					this.AddScore((PlayGamesScore)score);
				}
				this.mLoading = data.Scores.Length == 0 || this.HasAllScores();
			}
			return data.Valid;
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x001BE8B7 File Offset: 0x001BCCB7
		internal void SetMaxRange(ulong val)
		{
			this.mMaxRange = (uint)val;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x001BE8C1 File Offset: 0x001BCCC1
		internal void SetTitle(string value)
		{
			this.mTitle = value;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x001BE8CA File Offset: 0x001BCCCA
		internal void SetLocalUserScore(PlayGamesScore score)
		{
			this.mLocalUserScore = score;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x001BE8D4 File Offset: 0x001BCCD4
		internal int AddScore(PlayGamesScore score)
		{
			if (this.mFilteredUserIds == null || this.mFilteredUserIds.Length == 0)
			{
				this.mScoreList.Add(score);
			}
			else
			{
				foreach (string text in this.mFilteredUserIds)
				{
					if (text.Equals(score.userID))
					{
						return this.mScoreList.Count;
					}
				}
				this.mScoreList.Add(score);
			}
			return this.mScoreList.Count;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x001BE95D File Offset: 0x001BCD5D
		public int ScoreCount
		{
			get
			{
				return this.mScoreList.Count;
			}
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x001BE96A File Offset: 0x001BCD6A
		internal bool HasAllScores()
		{
			return this.mScoreList.Count >= this.mRange.count || (long)this.mScoreList.Count >= (long)((ulong)this.maxRange);
		}

		// Token: 0x04003132 RID: 12594
		private string mId;

		// Token: 0x04003133 RID: 12595
		private UserScope mUserScope;

		// Token: 0x04003134 RID: 12596
		private Range mRange;

		// Token: 0x04003135 RID: 12597
		private TimeScope mTimeScope;

		// Token: 0x04003136 RID: 12598
		private string[] mFilteredUserIds;

		// Token: 0x04003137 RID: 12599
		private bool mLoading;

		// Token: 0x04003138 RID: 12600
		private IScore mLocalUserScore;

		// Token: 0x04003139 RID: 12601
		private uint mMaxRange;

		// Token: 0x0400313A RID: 12602
		private List<PlayGamesScore> mScoreList = new List<PlayGamesScore>();

		// Token: 0x0400313B RID: 12603
		private string mTitle;
	}
}
