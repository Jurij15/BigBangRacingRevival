using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Video;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native
{
	// Token: 0x020006D1 RID: 1745
	internal class NativeVideoClient : IVideoClient
	{
		// Token: 0x06003235 RID: 12853 RVA: 0x001C9641 File Offset: 0x001C7A41
		internal NativeVideoClient(GooglePlayGames.Native.PInvoke.VideoManager manager)
		{
			this.mManager = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.VideoManager>(manager);
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x001C9658 File Offset: 0x001C7A58
		public void GetCaptureCapabilities(Action<ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCapabilities> callback)
		{
			Misc.CheckNotNull<Action<ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCapabilities>>(callback);
			callback = CallbackUtils.ToOnGameThread<ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCapabilities>(callback);
			this.mManager.GetCaptureCapabilities(delegate(GetCaptureCapabilitiesResponse response)
			{
				ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.GetStatus());
				if (!response.RequestSucceeded())
				{
					callback.Invoke(responseStatus, null);
				}
				else
				{
					callback.Invoke(responseStatus, this.FromNativeVideoCapabilities(response.GetData()));
				}
			});
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x001C96B0 File Offset: 0x001C7AB0
		private GooglePlayGames.BasicApi.Video.VideoCapabilities FromNativeVideoCapabilities(NativeVideoCapabilities capabilities)
		{
			bool[] array = new bool[this.mManager.NumCaptureModes];
			array[0] = capabilities.SupportsCaptureMode(Types.VideoCaptureMode.FILE);
			array[1] = capabilities.SupportsCaptureMode(Types.VideoCaptureMode.STREAM);
			bool[] array2 = new bool[this.mManager.NumQualityLevels];
			array2[0] = capabilities.SupportsQualityLevel(Types.VideoQualityLevel.SD);
			array2[1] = capabilities.SupportsQualityLevel(Types.VideoQualityLevel.HD);
			array2[2] = capabilities.SupportsQualityLevel(Types.VideoQualityLevel.XHD);
			array2[3] = capabilities.SupportsQualityLevel(Types.VideoQualityLevel.FULLHD);
			return new GooglePlayGames.BasicApi.Video.VideoCapabilities(capabilities.IsCameraSupported(), capabilities.IsMicSupported(), capabilities.IsWriteStorageSupported(), array, array2);
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x001C9734 File Offset: 0x001C7B34
		public void ShowCaptureOverlay()
		{
			this.mManager.ShowCaptureOverlay();
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x001C9744 File Offset: 0x001C7B44
		public void GetCaptureState(Action<ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCaptureState> callback)
		{
			Misc.CheckNotNull<Action<ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCaptureState>>(callback);
			callback = CallbackUtils.ToOnGameThread<ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCaptureState>(callback);
			this.mManager.GetCaptureState(delegate(GetCaptureStateResponse response)
			{
				ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.GetStatus());
				if (!response.RequestSucceeded())
				{
					callback.Invoke(responseStatus, null);
				}
				else
				{
					callback.Invoke(responseStatus, this.FromNativeVideoCaptureState(response.GetData()));
				}
			});
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x001C9799 File Offset: 0x001C7B99
		private GooglePlayGames.BasicApi.Video.VideoCaptureState FromNativeVideoCaptureState(NativeVideoCaptureState captureState)
		{
			return new GooglePlayGames.BasicApi.Video.VideoCaptureState(captureState.IsCapturing(), ConversionUtils.ConvertNativeVideoCaptureMode(captureState.CaptureMode()), ConversionUtils.ConvertNativeVideoQualityLevel(captureState.QualityLevel()), captureState.IsOverlayVisible(), captureState.IsPaused());
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x001C97C8 File Offset: 0x001C7BC8
		public void IsCaptureAvailable(VideoCaptureMode captureMode, Action<ResponseStatus, bool> callback)
		{
			Misc.CheckNotNull<Action<ResponseStatus, bool>>(callback);
			callback = CallbackUtils.ToOnGameThread<ResponseStatus, bool>(callback);
			this.mManager.IsCaptureAvailable(ConversionUtils.ConvertVideoCaptureMode(captureMode), delegate(IsCaptureAvailableResponse response)
			{
				ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.GetStatus());
				if (!response.RequestSucceeded())
				{
					callback.Invoke(responseStatus, false);
				}
				else
				{
					callback.Invoke(responseStatus, response.IsCaptureAvailable());
				}
			});
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x001C981C File Offset: 0x001C7C1C
		public bool IsCaptureSupported()
		{
			return this.mManager.IsCaptureSupported();
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x001C982C File Offset: 0x001C7C2C
		public void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListener listener)
		{
			Misc.CheckNotNull<CaptureOverlayStateListener>(listener);
			GooglePlayGames.Native.PInvoke.CaptureOverlayStateListenerHelper captureOverlayStateListenerHelper = GooglePlayGames.Native.PInvoke.CaptureOverlayStateListenerHelper.Create().SetOnCaptureOverlayStateChangedCallback(delegate(Types.VideoCaptureOverlayState response)
			{
				listener.OnCaptureOverlayStateChanged(ConversionUtils.ConvertNativeVideoCaptureOverlayState(response));
			});
			this.mManager.RegisterCaptureOverlayStateChangedListener(captureOverlayStateListenerHelper);
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x001C9875 File Offset: 0x001C7C75
		public void UnregisterCaptureOverlayStateChangedListener()
		{
			this.mManager.UnregisterCaptureOverlayStateChangedListener();
		}

		// Token: 0x040032BA RID: 12986
		private readonly GooglePlayGames.Native.PInvoke.VideoManager mManager;
	}
}
