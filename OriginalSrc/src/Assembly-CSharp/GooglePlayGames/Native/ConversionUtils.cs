using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;
using UnityEngine;

namespace GooglePlayGames.Native
{
	// Token: 0x02000622 RID: 1570
	internal static class ConversionUtils
	{
		// Token: 0x06002E2B RID: 11819 RVA: 0x001C2728 File Offset: 0x001C0B28
		internal static ResponseStatus ConvertResponseStatus(CommonErrorStatus.ResponseStatus status)
		{
			switch (status + 5)
			{
			case (CommonErrorStatus.ResponseStatus)0:
				return ResponseStatus.Timeout;
			case CommonErrorStatus.ResponseStatus.VALID:
				return ResponseStatus.VersionUpdateRequired;
			case CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
				return ResponseStatus.NotAuthorized;
			case (CommonErrorStatus.ResponseStatus)3:
				return ResponseStatus.InternalError;
			case (CommonErrorStatus.ResponseStatus)4:
				return ResponseStatus.LicenseCheckFailed;
			case (CommonErrorStatus.ResponseStatus)6:
				return ResponseStatus.Success;
			case (CommonErrorStatus.ResponseStatus)7:
				return ResponseStatus.SuccessWithStale;
			}
			throw new InvalidOperationException("Unknown status: " + status);
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x001C278C File Offset: 0x001C0B8C
		internal static CommonStatusCodes ConvertResponseStatusToCommonStatus(CommonErrorStatus.ResponseStatus status)
		{
			switch (status + 5)
			{
			case (CommonErrorStatus.ResponseStatus)0:
				return CommonStatusCodes.Timeout;
			case CommonErrorStatus.ResponseStatus.VALID:
				return CommonStatusCodes.ServiceVersionUpdateRequired;
			case CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
				return CommonStatusCodes.AuthApiAccessForbidden;
			case (CommonErrorStatus.ResponseStatus)3:
				return CommonStatusCodes.InternalError;
			case (CommonErrorStatus.ResponseStatus)4:
				return CommonStatusCodes.LicenseCheckFailed;
			case (CommonErrorStatus.ResponseStatus)6:
				return CommonStatusCodes.Success;
			case (CommonErrorStatus.ResponseStatus)7:
				return CommonStatusCodes.SuccessCached;
			}
			Debug.LogWarning("Unknown ResponseStatus: " + status + ", defaulting to CommonStatusCodes.Error");
			return CommonStatusCodes.Error;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x001C27F8 File Offset: 0x001C0BF8
		internal static UIStatus ConvertUIStatus(CommonErrorStatus.UIStatus status)
		{
			switch (status + 6)
			{
			case (CommonErrorStatus.UIStatus)0:
				return UIStatus.UserClosedUI;
			case CommonErrorStatus.UIStatus.VALID:
				return UIStatus.Timeout;
			case (CommonErrorStatus.UIStatus)2:
				return UIStatus.VersionUpdateRequired;
			case (CommonErrorStatus.UIStatus)3:
				return UIStatus.NotAuthorized;
			case (CommonErrorStatus.UIStatus)4:
				return UIStatus.InternalError;
			default:
				if (status != CommonErrorStatus.UIStatus.ERROR_UI_BUSY)
				{
					throw new InvalidOperationException("Unknown status: " + status);
				}
				return UIStatus.UiBusy;
			case (CommonErrorStatus.UIStatus)7:
				return UIStatus.Valid;
			}
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x001C2863 File Offset: 0x001C0C63
		internal static Types.DataSource AsDataSource(DataSource source)
		{
			if (source == DataSource.ReadCacheOrNetwork)
			{
				return Types.DataSource.CACHE_OR_NETWORK;
			}
			if (source != DataSource.ReadNetworkOnly)
			{
				throw new InvalidOperationException("Found unhandled DataSource: " + source);
			}
			return Types.DataSource.NETWORK_ONLY;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x001C2890 File Offset: 0x001C0C90
		internal static Types.VideoCaptureMode ConvertVideoCaptureMode(VideoCaptureMode captureMode)
		{
			switch (captureMode + 1)
			{
			case VideoCaptureMode.File:
				return Types.VideoCaptureMode.UNKNOWN;
			case VideoCaptureMode.Stream:
				return Types.VideoCaptureMode.FILE;
			case (VideoCaptureMode)2:
				return Types.VideoCaptureMode.STREAM;
			default:
				Debug.LogWarning("Unknown VideoCaptureMode: " + captureMode + ", defaulting to Types.VideoCaptureMode.UNKNOWN.");
				return Types.VideoCaptureMode.UNKNOWN;
			}
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x001C28CC File Offset: 0x001C0CCC
		internal static VideoCaptureMode ConvertNativeVideoCaptureMode(Types.VideoCaptureMode nativeCaptureMode)
		{
			switch (nativeCaptureMode + 1)
			{
			case Types.VideoCaptureMode.FILE:
				return VideoCaptureMode.Unknown;
			case Types.VideoCaptureMode.STREAM:
				return VideoCaptureMode.File;
			case (Types.VideoCaptureMode)2:
				return VideoCaptureMode.Stream;
			default:
				Debug.LogWarning("Unknown Types.VideoCaptureMode: " + nativeCaptureMode + ", defaulting to VideoCaptureMode.Unknown.");
				return VideoCaptureMode.Unknown;
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x001C2908 File Offset: 0x001C0D08
		internal static VideoQualityLevel ConvertNativeVideoQualityLevel(Types.VideoQualityLevel nativeQualityLevel)
		{
			switch (nativeQualityLevel + 1)
			{
			case Types.VideoQualityLevel.SD:
				return VideoQualityLevel.Unknown;
			case Types.VideoQualityLevel.HD:
				return VideoQualityLevel.SD;
			case Types.VideoQualityLevel.XHD:
				return VideoQualityLevel.HD;
			case Types.VideoQualityLevel.FULLHD:
				return VideoQualityLevel.XHD;
			case (Types.VideoQualityLevel)4:
				return VideoQualityLevel.FullHD;
			default:
				Debug.LogWarning("Unknown Types.VideoQualityLevel: " + nativeQualityLevel + ", defaulting to VideoQualityLevel.Unknown.");
				return VideoQualityLevel.Unknown;
			}
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x001C295C File Offset: 0x001C0D5C
		internal static VideoCaptureOverlayState ConvertNativeVideoCaptureOverlayState(Types.VideoCaptureOverlayState nativeOverlayState)
		{
			switch (nativeOverlayState + 1)
			{
			case (Types.VideoCaptureOverlayState)0:
				return VideoCaptureOverlayState.Unknown;
			case Types.VideoCaptureOverlayState.STARTED:
				return VideoCaptureOverlayState.Shown;
			case Types.VideoCaptureOverlayState.STOPPED:
				return VideoCaptureOverlayState.Started;
			case Types.VideoCaptureOverlayState.DISMISSED:
				return VideoCaptureOverlayState.Stopped;
			case (Types.VideoCaptureOverlayState)5:
				return VideoCaptureOverlayState.Dismissed;
			}
			Debug.LogWarning("Unknown Types.VideoCaptureOverlayState: " + nativeOverlayState + ", defaulting to VideoCaptureOverlayState.Unknown.");
			return VideoCaptureOverlayState.Unknown;
		}
	}
}
