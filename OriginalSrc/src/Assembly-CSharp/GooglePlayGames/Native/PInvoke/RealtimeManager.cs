using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000711 RID: 1809
	internal class RealtimeManager
	{
		// Token: 0x0600344D RID: 13389 RVA: 0x001CDBB0 File Offset: 0x001CBFB0
		internal RealtimeManager(GameServices gameServices)
		{
			this.mGameServices = Misc.CheckNotNull<GameServices>(gameServices);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x001CDBC4 File Offset: 0x001CBFC4
		internal void CreateRoom(RealtimeRoomConfig config, RealTimeEventListenerHelper helper, Action<RealtimeManager.RealTimeRoomResponse> callback)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_CreateRealTimeRoom(this.mGameServices.AsHandle(), config.AsPointer(), helper.AsPointer(), new RealTimeMultiplayerManager.RealTimeRoomCallback(RealtimeManager.InternalRealTimeRoomCallback), RealtimeManager.ToCallbackPointer(callback));
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x001CDC10 File Offset: 0x001CC010
		internal void ShowPlayerSelectUI(uint minimumPlayers, uint maxiumPlayers, bool allowAutomatching, Action<PlayerSelectUIResponse> callback)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_ShowPlayerSelectUI(this.mGameServices.AsHandle(), minimumPlayers, maxiumPlayers, allowAutomatching, new RealTimeMultiplayerManager.PlayerSelectUICallback(RealtimeManager.InternalPlayerSelectUIcallback), Callbacks.ToIntPtr<PlayerSelectUIResponse>(callback, new Func<IntPtr, PlayerSelectUIResponse>(PlayerSelectUIResponse.FromPointer)));
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x001CDC71 File Offset: 0x001CC071
		[MonoPInvokeCallback(typeof(RealTimeMultiplayerManager.PlayerSelectUICallback))]
		internal static void InternalPlayerSelectUIcallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("RealtimeManager#PlayerSelectUICallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x001CDC80 File Offset: 0x001CC080
		[MonoPInvokeCallback(typeof(RealTimeMultiplayerManager.RealTimeRoomCallback))]
		internal static void InternalRealTimeRoomCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("RealtimeManager#InternalRealTimeRoomCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x001CDC8F File Offset: 0x001CC08F
		[MonoPInvokeCallback(typeof(RealTimeMultiplayerManager.RoomInboxUICallback))]
		internal static void InternalRoomInboxUICallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("RealtimeManager#InternalRoomInboxUICallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x001CDCA0 File Offset: 0x001CC0A0
		internal void ShowRoomInboxUI(Action<RealtimeManager.RoomInboxUIResponse> callback)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_ShowRoomInboxUI(this.mGameServices.AsHandle(), new RealTimeMultiplayerManager.RoomInboxUICallback(RealtimeManager.InternalRoomInboxUICallback), Callbacks.ToIntPtr<RealtimeManager.RoomInboxUIResponse>(callback, new Func<IntPtr, RealtimeManager.RoomInboxUIResponse>(RealtimeManager.RoomInboxUIResponse.FromPointer)));
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x001CDD00 File Offset: 0x001CC100
		internal void ShowWaitingRoomUI(NativeRealTimeRoom room, uint minimumParticipantsBeforeStarting, Action<RealtimeManager.WaitingRoomUIResponse> callback)
		{
			Misc.CheckNotNull<NativeRealTimeRoom>(room);
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_ShowWaitingRoomUI(this.mGameServices.AsHandle(), room.AsPointer(), minimumParticipantsBeforeStarting, new RealTimeMultiplayerManager.WaitingRoomUICallback(RealtimeManager.InternalWaitingRoomUICallback), Callbacks.ToIntPtr<RealtimeManager.WaitingRoomUIResponse>(callback, new Func<IntPtr, RealtimeManager.WaitingRoomUIResponse>(RealtimeManager.WaitingRoomUIResponse.FromPointer)));
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x001CDD6B File Offset: 0x001CC16B
		[MonoPInvokeCallback(typeof(RealTimeMultiplayerManager.WaitingRoomUICallback))]
		internal static void InternalWaitingRoomUICallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("RealtimeManager#InternalWaitingRoomUICallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x001CDD7A File Offset: 0x001CC17A
		[MonoPInvokeCallback(typeof(RealTimeMultiplayerManager.FetchInvitationsCallback))]
		internal static void InternalFetchInvitationsCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("RealtimeManager#InternalFetchInvitationsCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x001CDD8C File Offset: 0x001CC18C
		internal void FetchInvitations(Action<RealtimeManager.FetchInvitationsResponse> callback)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitations(this.mGameServices.AsHandle(), new RealTimeMultiplayerManager.FetchInvitationsCallback(RealtimeManager.InternalFetchInvitationsCallback), Callbacks.ToIntPtr<RealtimeManager.FetchInvitationsResponse>(callback, new Func<IntPtr, RealtimeManager.FetchInvitationsResponse>(RealtimeManager.FetchInvitationsResponse.FromPointer)));
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x001CDDEC File Offset: 0x001CC1EC
		[MonoPInvokeCallback(typeof(RealTimeMultiplayerManager.LeaveRoomCallback))]
		internal static void InternalLeaveRoomCallback(CommonErrorStatus.ResponseStatus response, IntPtr data)
		{
			Logger.d("Entering internal callback for InternalLeaveRoomCallback");
			Action<CommonErrorStatus.ResponseStatus> action = Callbacks.IntPtrToTempCallback<Action<CommonErrorStatus.ResponseStatus>>(data);
			if (action == null)
			{
				return;
			}
			try
			{
				action.Invoke(response);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing InternalLeaveRoomCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x001CDE44 File Offset: 0x001CC244
		internal void LeaveRoom(NativeRealTimeRoom room, Action<CommonErrorStatus.ResponseStatus> callback)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_LeaveRoom(this.mGameServices.AsHandle(), room.AsPointer(), new RealTimeMultiplayerManager.LeaveRoomCallback(RealtimeManager.InternalLeaveRoomCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x001CDE80 File Offset: 0x001CC280
		internal void AcceptInvitation(MultiplayerInvitation invitation, RealTimeEventListenerHelper listener, Action<RealtimeManager.RealTimeRoomResponse> callback)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_AcceptInvitation(this.mGameServices.AsHandle(), invitation.AsPointer(), listener.AsPointer(), new RealTimeMultiplayerManager.RealTimeRoomCallback(RealtimeManager.InternalRealTimeRoomCallback), RealtimeManager.ToCallbackPointer(callback));
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x001CDECC File Offset: 0x001CC2CC
		internal void DeclineInvitation(MultiplayerInvitation invitation)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_DeclineInvitation(this.mGameServices.AsHandle(), invitation.AsPointer());
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x001CDEE4 File Offset: 0x001CC2E4
		internal void SendReliableMessage(NativeRealTimeRoom room, MultiplayerParticipant participant, byte[] data, Action<CommonErrorStatus.MultiplayerStatus> callback)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_SendReliableMessage(this.mGameServices.AsHandle(), room.AsPointer(), participant.AsPointer(), data, PInvokeUtilities.ArrayToSizeT<byte>(data), new RealTimeMultiplayerManager.SendReliableMessageCallback(RealtimeManager.InternalSendReliableMessageCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x001CDF38 File Offset: 0x001CC338
		[MonoPInvokeCallback(typeof(RealTimeMultiplayerManager.SendReliableMessageCallback))]
		internal static void InternalSendReliableMessageCallback(CommonErrorStatus.MultiplayerStatus response, IntPtr data)
		{
			Logger.d("Entering internal callback for InternalSendReliableMessageCallback " + response);
			Action<CommonErrorStatus.MultiplayerStatus> action = Callbacks.IntPtrToTempCallback<Action<CommonErrorStatus.MultiplayerStatus>>(data);
			if (action == null)
			{
				return;
			}
			try
			{
				action.Invoke(response);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing InternalSendReliableMessageCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x001CDF9C File Offset: 0x001CC39C
		internal void SendUnreliableMessageToAll(NativeRealTimeRoom room, byte[] data)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_SendUnreliableMessageToOthers(this.mGameServices.AsHandle(), room.AsPointer(), data, PInvokeUtilities.ArrayToSizeT<byte>(data));
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x001CDFBC File Offset: 0x001CC3BC
		internal void SendUnreliableMessageToSpecificParticipants(NativeRealTimeRoom room, List<MultiplayerParticipant> recipients, byte[] data)
		{
			RealTimeMultiplayerManager.RealTimeMultiplayerManager_SendUnreliableMessage(this.mGameServices.AsHandle(), room.AsPointer(), Enumerable.ToArray<IntPtr>(Enumerable.Select<MultiplayerParticipant, IntPtr>(recipients, (MultiplayerParticipant r) => r.AsPointer())), new UIntPtr((ulong)Enumerable.LongCount<MultiplayerParticipant>(recipients)), data, PInvokeUtilities.ArrayToSizeT<byte>(data));
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x001CE019 File Offset: 0x001CC419
		private static IntPtr ToCallbackPointer(Action<RealtimeManager.RealTimeRoomResponse> callback)
		{
			return Callbacks.ToIntPtr<RealtimeManager.RealTimeRoomResponse>(callback, new Func<IntPtr, RealtimeManager.RealTimeRoomResponse>(RealtimeManager.RealTimeRoomResponse.FromPointer));
		}

		// Token: 0x040032FF RID: 13055
		private readonly GameServices mGameServices;

		// Token: 0x02000712 RID: 1810
		internal class RealTimeRoomResponse : BaseReferenceHolder
		{
			// Token: 0x06003462 RID: 13410 RVA: 0x001CE046 File Offset: 0x001CC446
			internal RealTimeRoomResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x06003463 RID: 13411 RVA: 0x001CE04F File Offset: 0x001CC44F
			internal CommonErrorStatus.MultiplayerStatus ResponseStatus()
			{
				return RealTimeMultiplayerManager.RealTimeMultiplayerManager_RealTimeRoomResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x06003464 RID: 13412 RVA: 0x001CE05C File Offset: 0x001CC45C
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.MultiplayerStatus)0;
			}

			// Token: 0x06003465 RID: 13413 RVA: 0x001CE067 File Offset: 0x001CC467
			internal NativeRealTimeRoom Room()
			{
				if (!this.RequestSucceeded())
				{
					return null;
				}
				return new NativeRealTimeRoom(RealTimeMultiplayerManager.RealTimeMultiplayerManager_RealTimeRoomResponse_GetRoom(base.SelfPtr()));
			}

			// Token: 0x06003466 RID: 13414 RVA: 0x001CE086 File Offset: 0x001CC486
			protected override void CallDispose(HandleRef selfPointer)
			{
				RealTimeMultiplayerManager.RealTimeMultiplayerManager_RealTimeRoomResponse_Dispose(selfPointer);
			}

			// Token: 0x06003467 RID: 13415 RVA: 0x001CE08E File Offset: 0x001CC48E
			internal static RealtimeManager.RealTimeRoomResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new RealtimeManager.RealTimeRoomResponse(pointer);
			}
		}

		// Token: 0x02000713 RID: 1811
		internal class RoomInboxUIResponse : BaseReferenceHolder
		{
			// Token: 0x06003468 RID: 13416 RVA: 0x001CE0B4 File Offset: 0x001CC4B4
			internal RoomInboxUIResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x06003469 RID: 13417 RVA: 0x001CE0BD File Offset: 0x001CC4BD
			internal CommonErrorStatus.UIStatus ResponseStatus()
			{
				return RealTimeMultiplayerManager.RealTimeMultiplayerManager_RoomInboxUIResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x0600346A RID: 13418 RVA: 0x001CE0CA File Offset: 0x001CC4CA
			internal MultiplayerInvitation Invitation()
			{
				if (this.ResponseStatus() != CommonErrorStatus.UIStatus.VALID)
				{
					return null;
				}
				return new MultiplayerInvitation(RealTimeMultiplayerManager.RealTimeMultiplayerManager_RoomInboxUIResponse_GetInvitation(base.SelfPtr()));
			}

			// Token: 0x0600346B RID: 13419 RVA: 0x001CE0EA File Offset: 0x001CC4EA
			protected override void CallDispose(HandleRef selfPointer)
			{
				RealTimeMultiplayerManager.RealTimeMultiplayerManager_RoomInboxUIResponse_Dispose(selfPointer);
			}

			// Token: 0x0600346C RID: 13420 RVA: 0x001CE0F2 File Offset: 0x001CC4F2
			internal static RealtimeManager.RoomInboxUIResponse FromPointer(IntPtr pointer)
			{
				if (PInvokeUtilities.IsNull(pointer))
				{
					return null;
				}
				return new RealtimeManager.RoomInboxUIResponse(pointer);
			}
		}

		// Token: 0x02000714 RID: 1812
		internal class WaitingRoomUIResponse : BaseReferenceHolder
		{
			// Token: 0x0600346D RID: 13421 RVA: 0x001CE107 File Offset: 0x001CC507
			internal WaitingRoomUIResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x0600346E RID: 13422 RVA: 0x001CE110 File Offset: 0x001CC510
			internal CommonErrorStatus.UIStatus ResponseStatus()
			{
				return RealTimeMultiplayerManager.RealTimeMultiplayerManager_WaitingRoomUIResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x0600346F RID: 13423 RVA: 0x001CE11D File Offset: 0x001CC51D
			internal NativeRealTimeRoom Room()
			{
				if (this.ResponseStatus() != CommonErrorStatus.UIStatus.VALID)
				{
					return null;
				}
				return new NativeRealTimeRoom(RealTimeMultiplayerManager.RealTimeMultiplayerManager_WaitingRoomUIResponse_GetRoom(base.SelfPtr()));
			}

			// Token: 0x06003470 RID: 13424 RVA: 0x001CE13D File Offset: 0x001CC53D
			protected override void CallDispose(HandleRef selfPointer)
			{
				RealTimeMultiplayerManager.RealTimeMultiplayerManager_WaitingRoomUIResponse_Dispose(selfPointer);
			}

			// Token: 0x06003471 RID: 13425 RVA: 0x001CE145 File Offset: 0x001CC545
			internal static RealtimeManager.WaitingRoomUIResponse FromPointer(IntPtr pointer)
			{
				if (PInvokeUtilities.IsNull(pointer))
				{
					return null;
				}
				return new RealtimeManager.WaitingRoomUIResponse(pointer);
			}
		}

		// Token: 0x02000715 RID: 1813
		internal class FetchInvitationsResponse : BaseReferenceHolder
		{
			// Token: 0x06003472 RID: 13426 RVA: 0x001CE15A File Offset: 0x001CC55A
			internal FetchInvitationsResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x06003473 RID: 13427 RVA: 0x001CE163 File Offset: 0x001CC563
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.ResponseStatus)0;
			}

			// Token: 0x06003474 RID: 13428 RVA: 0x001CE16E File Offset: 0x001CC56E
			internal CommonErrorStatus.ResponseStatus ResponseStatus()
			{
				return RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitationsResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x06003475 RID: 13429 RVA: 0x001CE17B File Offset: 0x001CC57B
			internal IEnumerable<MultiplayerInvitation> Invitations()
			{
				return PInvokeUtilities.ToEnumerable<MultiplayerInvitation>(RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitationsResponse_GetInvitations_Length(base.SelfPtr()), (UIntPtr index) => new MultiplayerInvitation(RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitationsResponse_GetInvitations_GetElement(base.SelfPtr(), index)));
			}

			// Token: 0x06003476 RID: 13430 RVA: 0x001CE199 File Offset: 0x001CC599
			protected override void CallDispose(HandleRef selfPointer)
			{
				RealTimeMultiplayerManager.RealTimeMultiplayerManager_FetchInvitationsResponse_Dispose(selfPointer);
			}

			// Token: 0x06003477 RID: 13431 RVA: 0x001CE1A1 File Offset: 0x001CC5A1
			internal static RealtimeManager.FetchInvitationsResponse FromPointer(IntPtr pointer)
			{
				if (PInvokeUtilities.IsNull(pointer))
				{
					return null;
				}
				return new RealtimeManager.FetchInvitationsResponse(pointer);
			}
		}
	}
}
