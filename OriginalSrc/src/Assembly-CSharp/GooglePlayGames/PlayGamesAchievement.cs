using System;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x02000600 RID: 1536
	internal class PlayGamesAchievement : IAchievement, IAchievementDescription
	{
		// Token: 0x06002C9C RID: 11420 RVA: 0x001BE464 File Offset: 0x001BC864
		internal PlayGamesAchievement()
			: this(new ReportProgress(PlayGamesPlatform.Instance.ReportProgress))
		{
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x001BE47C File Offset: 0x001BC87C
		internal PlayGamesAchievement(ReportProgress progressCallback)
		{
			this.mProgressCallback = progressCallback;
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x001BE4E4 File Offset: 0x001BC8E4
		internal PlayGamesAchievement(GooglePlayGames.BasicApi.Achievement ach)
			: this()
		{
			this.mId = ach.Id;
			this.mIsIncremental = ach.IsIncremental;
			this.mCurrentSteps = ach.CurrentSteps;
			this.mTotalSteps = ach.TotalSteps;
			if (ach.IsIncremental)
			{
				if (ach.TotalSteps > 0)
				{
					this.mPercentComplete = (double)ach.CurrentSteps / (double)ach.TotalSteps * 100.0;
				}
				else
				{
					this.mPercentComplete = 0.0;
				}
			}
			else
			{
				this.mPercentComplete = ((!ach.IsUnlocked) ? 0.0 : 100.0);
			}
			this.mCompleted = ach.IsUnlocked;
			this.mHidden = !ach.IsRevealed;
			this.mLastModifiedTime = ach.LastModifiedTime;
			this.mTitle = ach.Name;
			this.mDescription = ach.Description;
			this.mPoints = ach.Points;
			this.mRevealedImageUrl = ach.RevealedImageUrl;
			this.mUnlockedImageUrl = ach.UnlockedImageUrl;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x001BE601 File Offset: 0x001BCA01
		public void ReportProgress(Action<bool> callback)
		{
			this.mProgressCallback(this.mId, this.mPercentComplete, callback);
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x001BE61C File Offset: 0x001BCA1C
		private Texture2D LoadImage()
		{
			if (this.hidden)
			{
				return null;
			}
			string text = ((!this.completed) ? this.mRevealedImageUrl : this.mUnlockedImageUrl);
			if (!string.IsNullOrEmpty(text))
			{
				if (this.mImageFetcher == null || this.mImageFetcher.url != text)
				{
					this.mImageFetcher = new WWW(text);
					this.mImage = null;
				}
				if (this.mImage != null)
				{
					return this.mImage;
				}
				if (this.mImageFetcher.isDone)
				{
					this.mImage = this.mImageFetcher.texture;
					return this.mImage;
				}
			}
			return null;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x001BE6D3 File Offset: 0x001BCAD3
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x001BE6DB File Offset: 0x001BCADB
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

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x001BE6E4 File Offset: 0x001BCAE4
		public bool isIncremental
		{
			get
			{
				return this.mIsIncremental;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x001BE6EC File Offset: 0x001BCAEC
		public int currentSteps
		{
			get
			{
				return this.mCurrentSteps;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x001BE6F4 File Offset: 0x001BCAF4
		public int totalSteps
		{
			get
			{
				return this.mTotalSteps;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x001BE6FC File Offset: 0x001BCAFC
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x001BE704 File Offset: 0x001BCB04
		public double percentCompleted
		{
			get
			{
				return this.mPercentComplete;
			}
			set
			{
				this.mPercentComplete = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x001BE70D File Offset: 0x001BCB0D
		public bool completed
		{
			get
			{
				return this.mCompleted;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x001BE715 File Offset: 0x001BCB15
		public bool hidden
		{
			get
			{
				return this.mHidden;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x001BE71D File Offset: 0x001BCB1D
		public DateTime lastReportedDate
		{
			get
			{
				return this.mLastModifiedTime;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x001BE725 File Offset: 0x001BCB25
		public string title
		{
			get
			{
				return this.mTitle;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x001BE72D File Offset: 0x001BCB2D
		public Texture2D image
		{
			get
			{
				return this.LoadImage();
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x001BE735 File Offset: 0x001BCB35
		public string achievedDescription
		{
			get
			{
				return this.mDescription;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x001BE73D File Offset: 0x001BCB3D
		public string unachievedDescription
		{
			get
			{
				return this.mDescription;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x001BE745 File Offset: 0x001BCB45
		public int points
		{
			get
			{
				return (int)this.mPoints;
			}
		}

		// Token: 0x04003122 RID: 12578
		private readonly ReportProgress mProgressCallback;

		// Token: 0x04003123 RID: 12579
		private string mId = string.Empty;

		// Token: 0x04003124 RID: 12580
		private bool mIsIncremental;

		// Token: 0x04003125 RID: 12581
		private int mCurrentSteps;

		// Token: 0x04003126 RID: 12582
		private int mTotalSteps;

		// Token: 0x04003127 RID: 12583
		private double mPercentComplete;

		// Token: 0x04003128 RID: 12584
		private bool mCompleted;

		// Token: 0x04003129 RID: 12585
		private bool mHidden;

		// Token: 0x0400312A RID: 12586
		private DateTime mLastModifiedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		// Token: 0x0400312B RID: 12587
		private string mTitle = string.Empty;

		// Token: 0x0400312C RID: 12588
		private string mRevealedImageUrl = string.Empty;

		// Token: 0x0400312D RID: 12589
		private string mUnlockedImageUrl = string.Empty;

		// Token: 0x0400312E RID: 12590
		private WWW mImageFetcher;

		// Token: 0x0400312F RID: 12591
		private Texture2D mImage;

		// Token: 0x04003130 RID: 12592
		private string mDescription = string.Empty;

		// Token: 0x04003131 RID: 12593
		private ulong mPoints;
	}
}
