using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005F9 RID: 1529
	public class ScorePageToken
	{
		// Token: 0x06002C76 RID: 11382 RVA: 0x001BE145 File Offset: 0x001BC545
		internal ScorePageToken(object internalObject, string id, LeaderboardCollection collection, LeaderboardTimeSpan timespan)
		{
			this.mInternalObject = internalObject;
			this.mId = id;
			this.mCollection = collection;
			this.mTimespan = timespan;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x001BE16A File Offset: 0x001BC56A
		public LeaderboardCollection Collection
		{
			get
			{
				return this.mCollection;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x001BE172 File Offset: 0x001BC572
		public LeaderboardTimeSpan TimeSpan
		{
			get
			{
				return this.mTimespan;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x001BE17A File Offset: 0x001BC57A
		public string LeaderboardId
		{
			get
			{
				return this.mId;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x001BE182 File Offset: 0x001BC582
		internal object InternalObject
		{
			get
			{
				return this.mInternalObject;
			}
		}

		// Token: 0x0400310A RID: 12554
		private string mId;

		// Token: 0x0400310B RID: 12555
		private object mInternalObject;

		// Token: 0x0400310C RID: 12556
		private LeaderboardCollection mCollection;

		// Token: 0x0400310D RID: 12557
		private LeaderboardTimeSpan mTimespan;
	}
}
