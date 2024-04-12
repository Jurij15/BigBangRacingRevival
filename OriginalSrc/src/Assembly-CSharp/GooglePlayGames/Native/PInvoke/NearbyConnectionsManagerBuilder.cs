using System;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000704 RID: 1796
	internal class NearbyConnectionsManagerBuilder : BaseReferenceHolder
	{
		// Token: 0x060033F4 RID: 13300 RVA: 0x001CCE57 File Offset: 0x001CB257
		internal NearbyConnectionsManagerBuilder()
			: base(NearbyConnectionsBuilder.NearbyConnections_Builder_Construct())
		{
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x001CCE64 File Offset: 0x001CB264
		internal NearbyConnectionsManagerBuilder SetOnInitializationFinished(Action<NearbyConnectionsStatus.InitializationStatus> callback)
		{
			NearbyConnectionsBuilder.NearbyConnections_Builder_SetOnInitializationFinished(base.SelfPtr(), new NearbyConnectionsBuilder.OnInitializationFinishedCallback(NearbyConnectionsManagerBuilder.InternalOnInitializationFinishedCallback), Callbacks.ToIntPtr(callback));
			return this;
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x001CCE98 File Offset: 0x001CB298
		[MonoPInvokeCallback(typeof(NearbyConnectionsBuilder.OnInitializationFinishedCallback))]
		private static void InternalOnInitializationFinishedCallback(NearbyConnectionsStatus.InitializationStatus status, IntPtr userData)
		{
			Action<NearbyConnectionsStatus.InitializationStatus> action = Callbacks.IntPtrToPermanentCallback<Action<NearbyConnectionsStatus.InitializationStatus>>(userData);
			if (action == null)
			{
				Logger.w("Callback for Initialization is null. Received status: " + status);
				return;
			}
			try
			{
				action.Invoke(status);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing NearbyConnectionsManagerBuilder#InternalOnInitializationFinishedCallback. Smothering exception: " + ex);
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x001CCEFC File Offset: 0x001CB2FC
		internal NearbyConnectionsManagerBuilder SetLocalClientId(long localClientId)
		{
			NearbyConnectionsBuilder.NearbyConnections_Builder_SetClientId(base.SelfPtr(), localClientId);
			return this;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x001CCF0B File Offset: 0x001CB30B
		internal NearbyConnectionsManagerBuilder SetDefaultLogLevel(Types.LogLevel minLevel)
		{
			NearbyConnectionsBuilder.NearbyConnections_Builder_SetDefaultOnLog(base.SelfPtr(), minLevel);
			return this;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x001CCF1A File Offset: 0x001CB31A
		internal NearbyConnectionsManager Build(PlatformConfiguration configuration)
		{
			return new NearbyConnectionsManager(NearbyConnectionsBuilder.NearbyConnections_Builder_Create(base.SelfPtr(), configuration.AsPointer()));
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x001CCF32 File Offset: 0x001CB332
		protected override void CallDispose(HandleRef selfPointer)
		{
			NearbyConnectionsBuilder.NearbyConnections_Builder_Dispose(selfPointer);
		}
	}
}
