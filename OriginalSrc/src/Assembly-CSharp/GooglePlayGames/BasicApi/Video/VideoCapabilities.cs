using System;
using System.Linq;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020005FC RID: 1532
	public class VideoCapabilities
	{
		// Token: 0x06002C83 RID: 11395 RVA: 0x001BE18A File Offset: 0x001BC58A
		internal VideoCapabilities(bool isCameraSupported, bool isMicSupported, bool isWriteStorageSupported, bool[] captureModesSupported, bool[] qualityLevelsSupported)
		{
			this.mIsCameraSupported = isCameraSupported;
			this.mIsMicSupported = isMicSupported;
			this.mIsWriteStorageSupported = isWriteStorageSupported;
			this.mCaptureModesSupported = captureModesSupported;
			this.mQualityLevelsSupported = qualityLevelsSupported;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x001BE1B7 File Offset: 0x001BC5B7
		public bool IsCameraSupported
		{
			get
			{
				return this.mIsCameraSupported;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x001BE1BF File Offset: 0x001BC5BF
		public bool IsMicSupported
		{
			get
			{
				return this.mIsMicSupported;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x001BE1C7 File Offset: 0x001BC5C7
		public bool IsWriteStorageSupported
		{
			get
			{
				return this.mIsWriteStorageSupported;
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x001BE1CF File Offset: 0x001BC5CF
		public bool SupportsCaptureMode(VideoCaptureMode captureMode)
		{
			if (captureMode != VideoCaptureMode.Unknown)
			{
				return this.mCaptureModesSupported[(int)captureMode];
			}
			Logger.w("SupportsCaptureMode called with an unknown captureMode.");
			return false;
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x001BE1EC File Offset: 0x001BC5EC
		public bool SupportsQualityLevel(VideoQualityLevel qualityLevel)
		{
			if (qualityLevel != VideoQualityLevel.Unknown)
			{
				return this.mQualityLevelsSupported[(int)qualityLevel];
			}
			Logger.w("SupportsCaptureMode called with an unknown qualityLevel.");
			return false;
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x001BE20C File Offset: 0x001BC60C
		public override string ToString()
		{
			string text = "[VideoCapabilities: mIsCameraSupported={0}, mIsMicSupported={1}, mIsWriteStorageSupported={2}, mCaptureModesSupported={3}, mQualityLevelsSupported={4}]";
			object[] array = new object[5];
			array[0] = this.mIsCameraSupported;
			array[1] = this.mIsMicSupported;
			array[2] = this.mIsWriteStorageSupported;
			array[3] = string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<bool, string>(this.mCaptureModesSupported, (bool p) => p.ToString())));
			array[4] = string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<bool, string>(this.mQualityLevelsSupported, (bool p) => p.ToString())));
			return string.Format(text, array);
		}

		// Token: 0x0400310E RID: 12558
		private bool mIsCameraSupported;

		// Token: 0x0400310F RID: 12559
		private bool mIsMicSupported;

		// Token: 0x04003110 RID: 12560
		private bool mIsWriteStorageSupported;

		// Token: 0x04003111 RID: 12561
		private bool[] mCaptureModesSupported;

		// Token: 0x04003112 RID: 12562
		private bool[] mQualityLevelsSupported;
	}
}
