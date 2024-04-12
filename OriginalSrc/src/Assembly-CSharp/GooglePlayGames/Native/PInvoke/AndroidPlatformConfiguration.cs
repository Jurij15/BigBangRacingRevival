using System;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006D5 RID: 1749
	internal sealed class AndroidPlatformConfiguration : PlatformConfiguration
	{
		// Token: 0x06003257 RID: 12887 RVA: 0x001C9DCC File Offset: 0x001C81CC
		private AndroidPlatformConfiguration(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x001C9DD5 File Offset: 0x001C81D5
		internal void SetActivity(IntPtr activity)
		{
			AndroidPlatformConfiguration.AndroidPlatformConfiguration_SetActivity(base.SelfPtr(), activity);
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x001C9DE3 File Offset: 0x001C81E3
		internal void SetOptionalIntentHandlerForUI(Action<IntPtr> intentHandler)
		{
			Misc.CheckNotNull<Action<IntPtr>>(intentHandler);
			AndroidPlatformConfiguration.AndroidPlatformConfiguration_SetOptionalIntentHandlerForUI(base.SelfPtr(), new AndroidPlatformConfiguration.IntentHandler(AndroidPlatformConfiguration.InternalIntentHandler), Callbacks.ToIntPtr(intentHandler));
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x001C9E1A File Offset: 0x001C821A
		internal void SetOptionalViewForPopups(IntPtr view)
		{
			AndroidPlatformConfiguration.AndroidPlatformConfiguration_SetOptionalViewForPopups(base.SelfPtr(), view);
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x001C9E28 File Offset: 0x001C8228
		protected override void CallDispose(HandleRef selfPointer)
		{
			AndroidPlatformConfiguration.AndroidPlatformConfiguration_Dispose(selfPointer);
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x001C9E30 File Offset: 0x001C8230
		[MonoPInvokeCallback(typeof(AndroidPlatformConfiguration.IntentHandlerInternal))]
		private static void InternalIntentHandler(IntPtr intent, IntPtr userData)
		{
			Callbacks.PerformInternalCallback("AndroidPlatformConfiguration#InternalIntentHandler", Callbacks.Type.Permanent, intent, userData);
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x001C9E40 File Offset: 0x001C8240
		internal static AndroidPlatformConfiguration Create()
		{
			IntPtr intPtr = AndroidPlatformConfiguration.AndroidPlatformConfiguration_Construct();
			return new AndroidPlatformConfiguration(intPtr);
		}

		// Token: 0x020006D6 RID: 1750
		// (Invoke) Token: 0x0600325F RID: 12895
		private delegate void IntentHandlerInternal(IntPtr intent, IntPtr userData);
	}
}
