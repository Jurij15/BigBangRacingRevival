using System;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000727 RID: 1831
	internal class VideoManager
	{
		// Token: 0x06003506 RID: 13574 RVA: 0x001CF2B8 File Offset: 0x001CD6B8
		internal VideoManager(GameServices services)
		{
			this.mServices = Misc.CheckNotNull<GameServices>(services);
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x001CF2CC File Offset: 0x001CD6CC
		internal int NumCaptureModes
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x001CF2CF File Offset: 0x001CD6CF
		internal int NumQualityLevels
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x001CF2D4 File Offset: 0x001CD6D4
		internal void GetCaptureCapabilities(Action<GetCaptureCapabilitiesResponse> callback)
		{
			VideoManager.VideoManager_GetCaptureCapabilities(this.mServices.AsHandle(), new VideoManager.CaptureCapabilitiesCallback(VideoManager.InternalCaptureCapabilitiesCallback), Callbacks.ToIntPtr<GetCaptureCapabilitiesResponse>(callback, new Func<IntPtr, GetCaptureCapabilitiesResponse>(GetCaptureCapabilitiesResponse.FromPointer)));
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x001CF331 File Offset: 0x001CD731
		[MonoPInvokeCallback(typeof(VideoManager.CaptureCapabilitiesCallback))]
		internal static void InternalCaptureCapabilitiesCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("VideoManager#CaptureCapabilitiesCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x001CF340 File Offset: 0x001CD740
		internal void ShowCaptureOverlay()
		{
			VideoManager.VideoManager_ShowCaptureOverlay(this.mServices.AsHandle());
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x001CF354 File Offset: 0x001CD754
		internal void GetCaptureState(Action<GetCaptureStateResponse> callback)
		{
			VideoManager.VideoManager_GetCaptureState(this.mServices.AsHandle(), new VideoManager.CaptureStateCallback(VideoManager.InternalCaptureStateCallback), Callbacks.ToIntPtr<GetCaptureStateResponse>(callback, new Func<IntPtr, GetCaptureStateResponse>(GetCaptureStateResponse.FromPointer)));
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x001CF3B1 File Offset: 0x001CD7B1
		[MonoPInvokeCallback(typeof(VideoManager.CaptureStateCallback))]
		internal static void InternalCaptureStateCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("VideoManager#CaptureStateCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x001CF3C0 File Offset: 0x001CD7C0
		internal void IsCaptureAvailable(Types.VideoCaptureMode captureMode, Action<IsCaptureAvailableResponse> callback)
		{
			VideoManager.VideoManager_IsCaptureAvailable(this.mServices.AsHandle(), captureMode, new VideoManager.IsCaptureAvailableCallback(VideoManager.InternalIsCaptureAvailableCallback), Callbacks.ToIntPtr<IsCaptureAvailableResponse>(callback, new Func<IntPtr, IsCaptureAvailableResponse>(IsCaptureAvailableResponse.FromPointer)));
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x001CF41E File Offset: 0x001CD81E
		[MonoPInvokeCallback(typeof(VideoManager.IsCaptureAvailableCallback))]
		internal static void InternalIsCaptureAvailableCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("VideoManager#IsCaptureAvailableCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x001CF42D File Offset: 0x001CD82D
		internal bool IsCaptureSupported()
		{
			return VideoManager.VideoManager_IsCaptureSupported(this.mServices.AsHandle());
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x001CF43F File Offset: 0x001CD83F
		internal void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListenerHelper helper)
		{
			VideoManager.VideoManager_RegisterCaptureOverlayStateChangedListener(this.mServices.AsHandle(), helper.AsPointer());
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x001CF457 File Offset: 0x001CD857
		internal void UnregisterCaptureOverlayStateChangedListener()
		{
			VideoManager.VideoManager_UnregisterCaptureOverlayStateChangedListener(this.mServices.AsHandle());
		}

		// Token: 0x04003332 RID: 13106
		private readonly GameServices mServices;
	}
}
