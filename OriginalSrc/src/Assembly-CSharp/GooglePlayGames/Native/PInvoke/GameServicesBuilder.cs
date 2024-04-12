using System;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006E0 RID: 1760
	internal class GameServicesBuilder : BaseReferenceHolder
	{
		// Token: 0x060032A3 RID: 12963 RVA: 0x001CA6E3 File Offset: 0x001C8AE3
		private GameServicesBuilder(IntPtr selfPointer)
			: base(selfPointer)
		{
			InternalHooks.InternalHooks_ConfigureForUnityPlugin(base.SelfPtr(), "0.9.50");
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x001CA6FC File Offset: 0x001C8AFC
		internal void SetOnAuthFinishedCallback(GameServicesBuilder.AuthFinishedCallback callback)
		{
			Builder.GameServices_Builder_SetOnAuthActionFinished(base.SelfPtr(), new Builder.OnAuthActionFinishedCallback(GameServicesBuilder.InternalAuthFinishedCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x001CA72C File Offset: 0x001C8B2C
		internal void EnableSnapshots()
		{
			Builder.GameServices_Builder_EnableSnapshots(base.SelfPtr());
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x001CA739 File Offset: 0x001C8B39
		internal void AddOauthScope(string scope)
		{
			Builder.GameServices_Builder_AddOauthScope(base.SelfPtr(), scope);
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x001CA748 File Offset: 0x001C8B48
		[MonoPInvokeCallback(typeof(Builder.OnAuthActionFinishedCallback))]
		private static void InternalAuthFinishedCallback(Types.AuthOperation op, CommonErrorStatus.AuthStatus status, IntPtr data)
		{
			GameServicesBuilder.AuthFinishedCallback authFinishedCallback = Callbacks.IntPtrToPermanentCallback<GameServicesBuilder.AuthFinishedCallback>(data);
			if (authFinishedCallback == null)
			{
				return;
			}
			try
			{
				authFinishedCallback(op, status);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing InternalAuthFinishedCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x001CA798 File Offset: 0x001C8B98
		internal void SetOnAuthStartedCallback(GameServicesBuilder.AuthStartedCallback callback)
		{
			Builder.GameServices_Builder_SetOnAuthActionStarted(base.SelfPtr(), new Builder.OnAuthActionStartedCallback(GameServicesBuilder.InternalAuthStartedCallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x001CA7C8 File Offset: 0x001C8BC8
		[MonoPInvokeCallback(typeof(Builder.OnAuthActionStartedCallback))]
		private static void InternalAuthStartedCallback(Types.AuthOperation op, IntPtr data)
		{
			GameServicesBuilder.AuthStartedCallback authStartedCallback = Callbacks.IntPtrToPermanentCallback<GameServicesBuilder.AuthStartedCallback>(data);
			try
			{
				if (authStartedCallback != null)
				{
					authStartedCallback(op);
				}
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing InternalAuthStartedCallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x001CA814 File Offset: 0x001C8C14
		internal void SetShowConnectingPopup(bool flag)
		{
			Builder.GameServices_Builder_SetShowConnectingPopup(base.SelfPtr(), flag);
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x001CA822 File Offset: 0x001C8C22
		protected override void CallDispose(HandleRef selfPointer)
		{
			Builder.GameServices_Builder_Dispose(selfPointer);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x001CA82C File Offset: 0x001C8C2C
		[MonoPInvokeCallback(typeof(Builder.OnTurnBasedMatchEventCallback))]
		private static void InternalOnTurnBasedMatchEventCallback(Types.MultiplayerEvent eventType, string matchId, IntPtr match, IntPtr userData)
		{
			Action<Types.MultiplayerEvent, string, NativeTurnBasedMatch> action = Callbacks.IntPtrToPermanentCallback<Action<Types.MultiplayerEvent, string, NativeTurnBasedMatch>>(userData);
			using (NativeTurnBasedMatch nativeTurnBasedMatch = NativeTurnBasedMatch.FromPointer(match))
			{
				try
				{
					if (action != null)
					{
						action.Invoke(eventType, matchId, nativeTurnBasedMatch);
					}
				}
				catch (Exception ex)
				{
					Logger.e("Error encountered executing InternalOnTurnBasedMatchEventCallback. Smothering to avoid passing exception into Native: " + ex);
				}
			}
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x001CA8A0 File Offset: 0x001C8CA0
		internal void SetOnTurnBasedMatchEventCallback(Action<Types.MultiplayerEvent, string, NativeTurnBasedMatch> callback)
		{
			IntPtr intPtr = Callbacks.ToIntPtr(callback);
			Builder.GameServices_Builder_SetOnTurnBasedMatchEvent(base.SelfPtr(), new Builder.OnTurnBasedMatchEventCallback(GameServicesBuilder.InternalOnTurnBasedMatchEventCallback), intPtr);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x001CA8E0 File Offset: 0x001C8CE0
		[MonoPInvokeCallback(typeof(Builder.OnMultiplayerInvitationEventCallback))]
		private static void InternalOnMultiplayerInvitationEventCallback(Types.MultiplayerEvent eventType, string matchId, IntPtr match, IntPtr userData)
		{
			Action<Types.MultiplayerEvent, string, MultiplayerInvitation> action = Callbacks.IntPtrToPermanentCallback<Action<Types.MultiplayerEvent, string, MultiplayerInvitation>>(userData);
			using (MultiplayerInvitation multiplayerInvitation = MultiplayerInvitation.FromPointer(match))
			{
				try
				{
					if (action != null)
					{
						action.Invoke(eventType, matchId, multiplayerInvitation);
					}
				}
				catch (Exception ex)
				{
					Logger.e("Error encountered executing InternalOnMultiplayerInvitationEventCallback. Smothering to avoid passing exception into Native: " + ex);
				}
			}
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x001CA954 File Offset: 0x001C8D54
		internal void SetOnMultiplayerInvitationEventCallback(Action<Types.MultiplayerEvent, string, MultiplayerInvitation> callback)
		{
			IntPtr intPtr = Callbacks.ToIntPtr(callback);
			Builder.GameServices_Builder_SetOnMultiplayerInvitationEvent(base.SelfPtr(), new Builder.OnMultiplayerInvitationEventCallback(GameServicesBuilder.InternalOnMultiplayerInvitationEventCallback), intPtr);
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x001CA994 File Offset: 0x001C8D94
		internal GameServices Build(PlatformConfiguration configRef)
		{
			IntPtr intPtr = Builder.GameServices_Builder_Create(base.SelfPtr(), HandleRef.ToIntPtr(configRef.AsHandle()));
			if (intPtr.Equals(IntPtr.Zero))
			{
				throw new InvalidOperationException("There was an error creating a GameServices object. Check for log errors from GamesNativeSDK");
			}
			return new GameServices(intPtr);
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x001CA9E8 File Offset: 0x001C8DE8
		internal static GameServicesBuilder Create()
		{
			IntPtr intPtr = Builder.GameServices_Builder_Construct();
			return new GameServicesBuilder(intPtr);
		}

		// Token: 0x020006E1 RID: 1761
		// (Invoke) Token: 0x060032B3 RID: 12979
		internal delegate void AuthFinishedCallback(Types.AuthOperation operation, CommonErrorStatus.AuthStatus status);

		// Token: 0x020006E2 RID: 1762
		// (Invoke) Token: 0x060032B7 RID: 12983
		internal delegate void AuthStartedCallback(Types.AuthOperation operation);
	}
}
