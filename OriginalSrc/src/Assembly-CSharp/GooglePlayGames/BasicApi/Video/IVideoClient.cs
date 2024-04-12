using System;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020005FB RID: 1531
	public interface IVideoClient
	{
		// Token: 0x06002C7C RID: 11388
		void GetCaptureCapabilities(Action<ResponseStatus, VideoCapabilities> callback);

		// Token: 0x06002C7D RID: 11389
		void ShowCaptureOverlay();

		// Token: 0x06002C7E RID: 11390
		void GetCaptureState(Action<ResponseStatus, VideoCaptureState> callback);

		// Token: 0x06002C7F RID: 11391
		void IsCaptureAvailable(VideoCaptureMode captureMode, Action<ResponseStatus, bool> callback);

		// Token: 0x06002C80 RID: 11392
		bool IsCaptureSupported();

		// Token: 0x06002C81 RID: 11393
		void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListener listener);

		// Token: 0x06002C82 RID: 11394
		void UnregisterCaptureOverlayStateChangedListener();
	}
}
