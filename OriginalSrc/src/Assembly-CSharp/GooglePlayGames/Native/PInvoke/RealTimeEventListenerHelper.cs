using System;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000710 RID: 1808
	internal class RealTimeEventListenerHelper : BaseReferenceHolder
	{
		// Token: 0x0600343C RID: 13372 RVA: 0x001CD7F1 File Offset: 0x001CBBF1
		internal RealTimeEventListenerHelper(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x001CD7FA File Offset: 0x001CBBFA
		protected override void CallDispose(HandleRef selfPointer)
		{
			RealTimeEventListenerHelper.RealTimeEventListenerHelper_Dispose(selfPointer);
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x001CD802 File Offset: 0x001CBC02
		internal RealTimeEventListenerHelper SetOnRoomStatusChangedCallback(Action<NativeRealTimeRoom> callback)
		{
			RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnRoomStatusChangedCallback(base.SelfPtr(), new RealTimeEventListenerHelper.OnRoomStatusChangedCallback(RealTimeEventListenerHelper.InternalOnRoomStatusChangedCallback), RealTimeEventListenerHelper.ToCallbackPointer(callback));
			return this;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x001CD833 File Offset: 0x001CBC33
		[MonoPInvokeCallback(typeof(RealTimeEventListenerHelper.OnRoomStatusChangedCallback))]
		internal static void InternalOnRoomStatusChangedCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("RealTimeEventListenerHelper#InternalOnRoomStatusChangedCallback", Callbacks.Type.Permanent, response, data);
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x001CD842 File Offset: 0x001CBC42
		internal RealTimeEventListenerHelper SetOnRoomConnectedSetChangedCallback(Action<NativeRealTimeRoom> callback)
		{
			RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnRoomConnectedSetChangedCallback(base.SelfPtr(), new RealTimeEventListenerHelper.OnRoomConnectedSetChangedCallback(RealTimeEventListenerHelper.InternalOnRoomConnectedSetChangedCallback), RealTimeEventListenerHelper.ToCallbackPointer(callback));
			return this;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x001CD873 File Offset: 0x001CBC73
		[MonoPInvokeCallback(typeof(RealTimeEventListenerHelper.OnRoomConnectedSetChangedCallback))]
		internal static void InternalOnRoomConnectedSetChangedCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("RealTimeEventListenerHelper#InternalOnRoomConnectedSetChangedCallback", Callbacks.Type.Permanent, response, data);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x001CD882 File Offset: 0x001CBC82
		internal RealTimeEventListenerHelper SetOnP2PConnectedCallback(Action<NativeRealTimeRoom, MultiplayerParticipant> callback)
		{
			RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnP2PConnectedCallback(base.SelfPtr(), new RealTimeEventListenerHelper.OnP2PConnectedCallback(RealTimeEventListenerHelper.InternalOnP2PConnectedCallback), Callbacks.ToIntPtr(callback));
			return this;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x001CD8B3 File Offset: 0x001CBCB3
		[MonoPInvokeCallback(typeof(RealTimeEventListenerHelper.OnP2PConnectedCallback))]
		internal static void InternalOnP2PConnectedCallback(IntPtr room, IntPtr participant, IntPtr data)
		{
			RealTimeEventListenerHelper.PerformRoomAndParticipantCallback("InternalOnP2PConnectedCallback", room, participant, data);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x001CD8C2 File Offset: 0x001CBCC2
		internal RealTimeEventListenerHelper SetOnP2PDisconnectedCallback(Action<NativeRealTimeRoom, MultiplayerParticipant> callback)
		{
			RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnP2PDisconnectedCallback(base.SelfPtr(), new RealTimeEventListenerHelper.OnP2PDisconnectedCallback(RealTimeEventListenerHelper.InternalOnP2PDisconnectedCallback), Callbacks.ToIntPtr(callback));
			return this;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x001CD8F3 File Offset: 0x001CBCF3
		[MonoPInvokeCallback(typeof(RealTimeEventListenerHelper.OnP2PDisconnectedCallback))]
		internal static void InternalOnP2PDisconnectedCallback(IntPtr room, IntPtr participant, IntPtr data)
		{
			RealTimeEventListenerHelper.PerformRoomAndParticipantCallback("InternalOnP2PDisconnectedCallback", room, participant, data);
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x001CD902 File Offset: 0x001CBD02
		internal RealTimeEventListenerHelper SetOnParticipantStatusChangedCallback(Action<NativeRealTimeRoom, MultiplayerParticipant> callback)
		{
			RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnParticipantStatusChangedCallback(base.SelfPtr(), new RealTimeEventListenerHelper.OnParticipantStatusChangedCallback(RealTimeEventListenerHelper.InternalOnParticipantStatusChangedCallback), Callbacks.ToIntPtr(callback));
			return this;
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x001CD933 File Offset: 0x001CBD33
		[MonoPInvokeCallback(typeof(RealTimeEventListenerHelper.OnParticipantStatusChangedCallback))]
		internal static void InternalOnParticipantStatusChangedCallback(IntPtr room, IntPtr participant, IntPtr data)
		{
			RealTimeEventListenerHelper.PerformRoomAndParticipantCallback("InternalOnParticipantStatusChangedCallback", room, participant, data);
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x001CD944 File Offset: 0x001CBD44
		internal static void PerformRoomAndParticipantCallback(string callbackName, IntPtr room, IntPtr participant, IntPtr data)
		{
			Logger.d("Entering " + callbackName);
			try
			{
				NativeRealTimeRoom nativeRealTimeRoom = NativeRealTimeRoom.FromPointer(room);
				using (MultiplayerParticipant multiplayerParticipant = MultiplayerParticipant.FromPointer(participant))
				{
					Action<NativeRealTimeRoom, MultiplayerParticipant> action = Callbacks.IntPtrToPermanentCallback<Action<NativeRealTimeRoom, MultiplayerParticipant>>(data);
					if (action != null)
					{
						action.Invoke(nativeRealTimeRoom, multiplayerParticipant);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.e(string.Concat(new object[] { "Error encountered executing ", callbackName, ". Smothering to avoid passing exception into Native: ", ex }));
			}
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x001CD9E8 File Offset: 0x001CBDE8
		internal RealTimeEventListenerHelper SetOnDataReceivedCallback(Action<NativeRealTimeRoom, MultiplayerParticipant, byte[], bool> callback)
		{
			IntPtr intPtr = Callbacks.ToIntPtr(callback);
			Logger.d("OnData Callback has addr: " + intPtr.ToInt64());
			RealTimeEventListenerHelper.RealTimeEventListenerHelper_SetOnDataReceivedCallback(base.SelfPtr(), new RealTimeEventListenerHelper.OnDataReceivedCallback(RealTimeEventListenerHelper.InternalOnDataReceived), intPtr);
			return this;
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x001CDA44 File Offset: 0x001CBE44
		[MonoPInvokeCallback(typeof(RealTimeEventListenerHelper.OnDataReceivedCallback))]
		internal static void InternalOnDataReceived(IntPtr room, IntPtr participant, IntPtr data, UIntPtr dataLength, bool isReliable, IntPtr userData)
		{
			Logger.d("Entering InternalOnDataReceived: " + userData.ToInt64());
			Action<NativeRealTimeRoom, MultiplayerParticipant, byte[], bool> action = Callbacks.IntPtrToPermanentCallback<Action<NativeRealTimeRoom, MultiplayerParticipant, byte[], bool>>(userData);
			using (NativeRealTimeRoom nativeRealTimeRoom = NativeRealTimeRoom.FromPointer(room))
			{
				using (MultiplayerParticipant multiplayerParticipant = MultiplayerParticipant.FromPointer(participant))
				{
					if (action != null)
					{
						byte[] array = null;
						if (dataLength.ToUInt64() != 0UL)
						{
							array = new byte[dataLength.ToUInt32()];
							Marshal.Copy(data, array, 0, (int)dataLength.ToUInt32());
						}
						try
						{
							action.Invoke(nativeRealTimeRoom, multiplayerParticipant, array, isReliable);
						}
						catch (Exception ex)
						{
							Logger.e("Error encountered executing InternalOnDataReceived. Smothering to avoid passing exception into Native: " + ex);
						}
					}
				}
			}
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x001CDB30 File Offset: 0x001CBF30
		private static IntPtr ToCallbackPointer(Action<NativeRealTimeRoom> callback)
		{
			Action<IntPtr> action = delegate(IntPtr result)
			{
				NativeRealTimeRoom nativeRealTimeRoom = NativeRealTimeRoom.FromPointer(result);
				if (callback != null)
				{
					callback.Invoke(nativeRealTimeRoom);
				}
				else if (nativeRealTimeRoom != null)
				{
					nativeRealTimeRoom.Dispose();
				}
			};
			return Callbacks.ToIntPtr(action);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x001CDB5D File Offset: 0x001CBF5D
		internal static RealTimeEventListenerHelper Create()
		{
			return new RealTimeEventListenerHelper(RealTimeEventListenerHelper.RealTimeEventListenerHelper_Construct());
		}
	}
}
