using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020005F7 RID: 1527
	public struct SavedGameMetadataUpdate
	{
		// Token: 0x06002C6B RID: 11371 RVA: 0x001BE040 File Offset: 0x001BC440
		private SavedGameMetadataUpdate(SavedGameMetadataUpdate.Builder builder)
		{
			this.mDescriptionUpdated = builder.mDescriptionUpdated;
			this.mNewDescription = builder.mNewDescription;
			this.mCoverImageUpdated = builder.mCoverImageUpdated;
			this.mNewPngCoverImage = builder.mNewPngCoverImage;
			this.mNewPlayedTime = builder.mNewPlayedTime;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x001BE08E File Offset: 0x001BC48E
		public bool IsDescriptionUpdated
		{
			get
			{
				return this.mDescriptionUpdated;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x001BE096 File Offset: 0x001BC496
		public string UpdatedDescription
		{
			get
			{
				return this.mNewDescription;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x001BE09E File Offset: 0x001BC49E
		public bool IsCoverImageUpdated
		{
			get
			{
				return this.mCoverImageUpdated;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x001BE0A6 File Offset: 0x001BC4A6
		public byte[] UpdatedPngCoverImage
		{
			get
			{
				return this.mNewPngCoverImage;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x001BE0B0 File Offset: 0x001BC4B0
		public bool IsPlayedTimeUpdated
		{
			get
			{
				return this.mNewPlayedTime != null;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x001BE0CB File Offset: 0x001BC4CB
		public TimeSpan? UpdatedPlayedTime
		{
			get
			{
				return this.mNewPlayedTime;
			}
		}

		// Token: 0x04003100 RID: 12544
		private readonly bool mDescriptionUpdated;

		// Token: 0x04003101 RID: 12545
		private readonly string mNewDescription;

		// Token: 0x04003102 RID: 12546
		private readonly bool mCoverImageUpdated;

		// Token: 0x04003103 RID: 12547
		private readonly byte[] mNewPngCoverImage;

		// Token: 0x04003104 RID: 12548
		private readonly TimeSpan? mNewPlayedTime;

		// Token: 0x020005F8 RID: 1528
		public struct Builder
		{
			// Token: 0x06002C72 RID: 11378 RVA: 0x001BE0D3 File Offset: 0x001BC4D3
			public SavedGameMetadataUpdate.Builder WithUpdatedDescription(string description)
			{
				this.mNewDescription = Misc.CheckNotNull<string>(description);
				this.mDescriptionUpdated = true;
				return this;
			}

			// Token: 0x06002C73 RID: 11379 RVA: 0x001BE0EE File Offset: 0x001BC4EE
			public SavedGameMetadataUpdate.Builder WithUpdatedPngCoverImage(byte[] newPngCoverImage)
			{
				this.mCoverImageUpdated = true;
				this.mNewPngCoverImage = newPngCoverImage;
				return this;
			}

			// Token: 0x06002C74 RID: 11380 RVA: 0x001BE104 File Offset: 0x001BC504
			public SavedGameMetadataUpdate.Builder WithUpdatedPlayedTime(TimeSpan newPlayedTime)
			{
				if (newPlayedTime.TotalMilliseconds > 1.8446744073709552E+19)
				{
					throw new InvalidOperationException("Timespans longer than ulong.MaxValue milliseconds are not allowed");
				}
				this.mNewPlayedTime = new TimeSpan?(newPlayedTime);
				return this;
			}

			// Token: 0x06002C75 RID: 11381 RVA: 0x001BE138 File Offset: 0x001BC538
			public SavedGameMetadataUpdate Build()
			{
				return new SavedGameMetadataUpdate(this);
			}

			// Token: 0x04003105 RID: 12549
			internal bool mDescriptionUpdated;

			// Token: 0x04003106 RID: 12550
			internal string mNewDescription;

			// Token: 0x04003107 RID: 12551
			internal bool mCoverImageUpdated;

			// Token: 0x04003108 RID: 12552
			internal byte[] mNewPngCoverImage;

			// Token: 0x04003109 RID: 12553
			internal TimeSpan? mNewPlayedTime;
		}
	}
}
