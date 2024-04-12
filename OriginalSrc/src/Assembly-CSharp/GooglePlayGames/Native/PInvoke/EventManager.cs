using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006DC RID: 1756
	internal class EventManager
	{
		// Token: 0x06003285 RID: 12933 RVA: 0x001CA460 File Offset: 0x001C8860
		internal EventManager(GameServices services)
		{
			this.mServices = Misc.CheckNotNull<GameServices>(services);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x001CA474 File Offset: 0x001C8874
		internal void FetchAll(Types.DataSource source, Action<EventManager.FetchAllResponse> callback)
		{
			EventManager.EventManager_FetchAll(this.mServices.AsHandle(), source, new EventManager.FetchAllCallback(EventManager.InternalFetchAllCallback), Callbacks.ToIntPtr<EventManager.FetchAllResponse>(callback, new Func<IntPtr, EventManager.FetchAllResponse>(EventManager.FetchAllResponse.FromPointer)));
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x001CA4D2 File Offset: 0x001C88D2
		[MonoPInvokeCallback(typeof(EventManager.FetchAllCallback))]
		internal static void InternalFetchAllCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("EventManager#FetchAllCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x001CA4E4 File Offset: 0x001C88E4
		internal void Fetch(Types.DataSource source, string eventId, Action<EventManager.FetchResponse> callback)
		{
			EventManager.EventManager_Fetch(this.mServices.AsHandle(), source, eventId, new EventManager.FetchCallback(EventManager.InternalFetchCallback), Callbacks.ToIntPtr<EventManager.FetchResponse>(callback, new Func<IntPtr, EventManager.FetchResponse>(EventManager.FetchResponse.FromPointer)));
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x001CA543 File Offset: 0x001C8943
		[MonoPInvokeCallback(typeof(EventManager.FetchCallback))]
		internal static void InternalFetchCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("EventManager#FetchCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x001CA552 File Offset: 0x001C8952
		internal void Increment(string eventId, uint steps)
		{
			EventManager.EventManager_Increment(this.mServices.AsHandle(), eventId, steps);
		}

		// Token: 0x040032C9 RID: 13001
		private readonly GameServices mServices;

		// Token: 0x020006DD RID: 1757
		internal class FetchResponse : BaseReferenceHolder
		{
			// Token: 0x0600328B RID: 12939 RVA: 0x001CA566 File Offset: 0x001C8966
			internal FetchResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x0600328C RID: 12940 RVA: 0x001CA56F File Offset: 0x001C896F
			internal CommonErrorStatus.ResponseStatus ResponseStatus()
			{
				return EventManager.EventManager_FetchResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x0600328D RID: 12941 RVA: 0x001CA57C File Offset: 0x001C897C
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.ResponseStatus)0;
			}

			// Token: 0x0600328E RID: 12942 RVA: 0x001CA587 File Offset: 0x001C8987
			internal NativeEvent Data()
			{
				if (!this.RequestSucceeded())
				{
					return null;
				}
				return new NativeEvent(EventManager.EventManager_FetchResponse_GetData(base.SelfPtr()));
			}

			// Token: 0x0600328F RID: 12943 RVA: 0x001CA5A6 File Offset: 0x001C89A6
			protected override void CallDispose(HandleRef selfPointer)
			{
				EventManager.EventManager_FetchResponse_Dispose(selfPointer);
			}

			// Token: 0x06003290 RID: 12944 RVA: 0x001CA5AE File Offset: 0x001C89AE
			internal static EventManager.FetchResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new EventManager.FetchResponse(pointer);
			}
		}

		// Token: 0x020006DE RID: 1758
		internal class FetchAllResponse : BaseReferenceHolder
		{
			// Token: 0x06003291 RID: 12945 RVA: 0x001CA5D4 File Offset: 0x001C89D4
			internal FetchAllResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x06003292 RID: 12946 RVA: 0x001CA5DD File Offset: 0x001C89DD
			internal CommonErrorStatus.ResponseStatus ResponseStatus()
			{
				return EventManager.EventManager_FetchAllResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x06003293 RID: 12947 RVA: 0x001CA5EC File Offset: 0x001C89EC
			internal List<NativeEvent> Data()
			{
				IntPtr[] array = PInvokeUtilities.OutParamsToArray<IntPtr>((IntPtr[] out_arg, UIntPtr out_size) => EventManager.EventManager_FetchAllResponse_GetData(base.SelfPtr(), out_arg, out_size));
				return Enumerable.ToList<NativeEvent>(Enumerable.Select<IntPtr, NativeEvent>(array, (IntPtr ptr) => new NativeEvent(ptr)));
			}

			// Token: 0x06003294 RID: 12948 RVA: 0x001CA633 File Offset: 0x001C8A33
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.ResponseStatus)0;
			}

			// Token: 0x06003295 RID: 12949 RVA: 0x001CA63E File Offset: 0x001C8A3E
			protected override void CallDispose(HandleRef selfPointer)
			{
				EventManager.EventManager_FetchAllResponse_Dispose(selfPointer);
			}

			// Token: 0x06003296 RID: 12950 RVA: 0x001CA646 File Offset: 0x001C8A46
			internal static EventManager.FetchAllResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new EventManager.FetchAllResponse(pointer);
			}
		}
	}
}
