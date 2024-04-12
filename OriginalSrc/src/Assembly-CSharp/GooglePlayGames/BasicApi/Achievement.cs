using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020005C0 RID: 1472
	public class Achievement
	{
		// Token: 0x06002AF4 RID: 10996 RVA: 0x001BCC88 File Offset: 0x001BB088
		public override string ToString()
		{
			return string.Format("[Achievement] id={0}, name={1}, desc={2}, type={3}, revealed={4}, unlocked={5}, steps={6}/{7}", new object[]
			{
				this.mId,
				this.mName,
				this.mDescription,
				(!this.mIsIncremental) ? "STANDARD" : "INCREMENTAL",
				this.mIsRevealed,
				this.mIsUnlocked,
				this.mCurrentSteps,
				this.mTotalSteps
			});
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x001BCD15 File Offset: 0x001BB115
		// (set) Token: 0x06002AF6 RID: 10998 RVA: 0x001BCD1D File Offset: 0x001BB11D
		public bool IsIncremental
		{
			get
			{
				return this.mIsIncremental;
			}
			set
			{
				this.mIsIncremental = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x001BCD26 File Offset: 0x001BB126
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x001BCD2E File Offset: 0x001BB12E
		public int CurrentSteps
		{
			get
			{
				return this.mCurrentSteps;
			}
			set
			{
				this.mCurrentSteps = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x001BCD37 File Offset: 0x001BB137
		// (set) Token: 0x06002AFA RID: 11002 RVA: 0x001BCD3F File Offset: 0x001BB13F
		public int TotalSteps
		{
			get
			{
				return this.mTotalSteps;
			}
			set
			{
				this.mTotalSteps = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x001BCD48 File Offset: 0x001BB148
		// (set) Token: 0x06002AFC RID: 11004 RVA: 0x001BCD50 File Offset: 0x001BB150
		public bool IsUnlocked
		{
			get
			{
				return this.mIsUnlocked;
			}
			set
			{
				this.mIsUnlocked = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x001BCD59 File Offset: 0x001BB159
		// (set) Token: 0x06002AFE RID: 11006 RVA: 0x001BCD61 File Offset: 0x001BB161
		public bool IsRevealed
		{
			get
			{
				return this.mIsRevealed;
			}
			set
			{
				this.mIsRevealed = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x001BCD6A File Offset: 0x001BB16A
		// (set) Token: 0x06002B00 RID: 11008 RVA: 0x001BCD72 File Offset: 0x001BB172
		public string Id
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x001BCD7B File Offset: 0x001BB17B
		// (set) Token: 0x06002B02 RID: 11010 RVA: 0x001BCD83 File Offset: 0x001BB183
		public string Description
		{
			get
			{
				return this.mDescription;
			}
			set
			{
				this.mDescription = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x001BCD8C File Offset: 0x001BB18C
		// (set) Token: 0x06002B04 RID: 11012 RVA: 0x001BCD94 File Offset: 0x001BB194
		public string Name
		{
			get
			{
				return this.mName;
			}
			set
			{
				this.mName = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x001BCDA0 File Offset: 0x001BB1A0
		// (set) Token: 0x06002B06 RID: 11014 RVA: 0x001BCDC4 File Offset: 0x001BB1C4
		public DateTime LastModifiedTime
		{
			get
			{
				return Achievement.UnixEpoch.AddMilliseconds((double)this.mLastModifiedTime);
			}
			set
			{
				this.mLastModifiedTime = (long)(value - Achievement.UnixEpoch).TotalMilliseconds;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x001BCDEB File Offset: 0x001BB1EB
		// (set) Token: 0x06002B08 RID: 11016 RVA: 0x001BCDF3 File Offset: 0x001BB1F3
		public ulong Points
		{
			get
			{
				return this.mPoints;
			}
			set
			{
				this.mPoints = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x001BCDFC File Offset: 0x001BB1FC
		// (set) Token: 0x06002B0A RID: 11018 RVA: 0x001BCE04 File Offset: 0x001BB204
		public string RevealedImageUrl
		{
			get
			{
				return this.mRevealedImageUrl;
			}
			set
			{
				this.mRevealedImageUrl = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x001BCE0D File Offset: 0x001BB20D
		// (set) Token: 0x06002B0C RID: 11020 RVA: 0x001BCE15 File Offset: 0x001BB215
		public string UnlockedImageUrl
		{
			get
			{
				return this.mUnlockedImageUrl;
			}
			set
			{
				this.mUnlockedImageUrl = value;
			}
		}

		// Token: 0x0400300F RID: 12303
		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, 1);

		// Token: 0x04003010 RID: 12304
		private string mId = string.Empty;

		// Token: 0x04003011 RID: 12305
		private bool mIsIncremental;

		// Token: 0x04003012 RID: 12306
		private bool mIsRevealed;

		// Token: 0x04003013 RID: 12307
		private bool mIsUnlocked;

		// Token: 0x04003014 RID: 12308
		private int mCurrentSteps;

		// Token: 0x04003015 RID: 12309
		private int mTotalSteps;

		// Token: 0x04003016 RID: 12310
		private string mDescription = string.Empty;

		// Token: 0x04003017 RID: 12311
		private string mName = string.Empty;

		// Token: 0x04003018 RID: 12312
		private long mLastModifiedTime;

		// Token: 0x04003019 RID: 12313
		private ulong mPoints;

		// Token: 0x0400301A RID: 12314
		private string mRevealedImageUrl;

		// Token: 0x0400301B RID: 12315
		private string mUnlockedImageUrl;
	}
}
