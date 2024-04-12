using System;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020005FD RID: 1533
	public class VideoCaptureState
	{
		// Token: 0x06002C8C RID: 11404 RVA: 0x001BE2E5 File Offset: 0x001BC6E5
		internal VideoCaptureState(bool isCapturing, VideoCaptureMode captureMode, VideoQualityLevel qualityLevel, bool isOverlayVisible, bool isPaused)
		{
			this.mIsCapturing = isCapturing;
			this.mCaptureMode = captureMode;
			this.mQualityLevel = qualityLevel;
			this.mIsOverlayVisible = isOverlayVisible;
			this.mIsPaused = isPaused;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x001BE312 File Offset: 0x001BC712
		public bool IsCapturing
		{
			get
			{
				return this.mIsCapturing;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x001BE31A File Offset: 0x001BC71A
		public VideoCaptureMode CaptureMode
		{
			get
			{
				return this.mCaptureMode;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x001BE322 File Offset: 0x001BC722
		public VideoQualityLevel QualityLevel
		{
			get
			{
				return this.mQualityLevel;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x001BE32A File Offset: 0x001BC72A
		public bool IsOverlayVisible
		{
			get
			{
				return this.mIsOverlayVisible;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x001BE332 File Offset: 0x001BC732
		public bool IsPaused
		{
			get
			{
				return this.mIsPaused;
			}
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x001BE33C File Offset: 0x001BC73C
		public override string ToString()
		{
			return string.Format("[VideoCaptureState: mIsCapturing={0}, mCaptureMode={1}, mQualityLevel={2}, mIsOverlayVisible={3}, mIsPaused={4}]", new object[]
			{
				this.mIsCapturing,
				this.mCaptureMode.ToString(),
				this.mQualityLevel.ToString(),
				this.mIsOverlayVisible,
				this.mIsPaused
			});
		}

		// Token: 0x04003115 RID: 12565
		private bool mIsCapturing;

		// Token: 0x04003116 RID: 12566
		private VideoCaptureMode mCaptureMode;

		// Token: 0x04003117 RID: 12567
		private VideoQualityLevel mQualityLevel;

		// Token: 0x04003118 RID: 12568
		private bool mIsOverlayVisible;

		// Token: 0x04003119 RID: 12569
		private bool mIsPaused;
	}
}
