using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Video;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames.Native
{
	// Token: 0x020006B9 RID: 1721
	public class NativeClient : IPlayGamesClient
	{
		// Token: 0x06003115 RID: 12565 RVA: 0x001C2A75 File Offset: 0x001C0E75
		internal NativeClient(PlayGamesClientConfiguration configuration, IClientImpl clientImpl)
		{
			PlayGamesHelperObject.CreateObject();
			this.mConfiguration = Misc.CheckNotNull<PlayGamesClientConfiguration>(configuration);
			this.clientImpl = clientImpl;
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x001C2AAC File Offset: 0x001C0EAC
		private GooglePlayGames.Native.PInvoke.GameServices GameServices()
		{
			object gameServicesLock = this.GameServicesLock;
			GooglePlayGames.Native.PInvoke.GameServices gameServices;
			lock (gameServicesLock)
			{
				gameServices = this.mServices;
			}
			return gameServices;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x001C2AEC File Offset: 0x001C0EEC
		public void Authenticate(Action<bool, string> callback, bool silent)
		{
			object authStateLock = this.AuthStateLock;
			lock (authStateLock)
			{
				if (this.mAuthState == NativeClient.AuthState.Authenticated)
				{
					NativeClient.InvokeCallbackOnGameThread<bool, string>(callback, true, null);
					return;
				}
				if (this.mSilentAuthFailed && silent)
				{
					NativeClient.InvokeCallbackOnGameThread<bool, string>(callback, false, "silent auth failed");
					return;
				}
				if (callback != null)
				{
					if (silent)
					{
						this.mSilentAuthCallbacks = (Action<bool, string>)Delegate.Combine(this.mSilentAuthCallbacks, callback);
					}
					else
					{
						this.mPendingAuthCallbacks = (Action<bool, string>)Delegate.Combine(this.mPendingAuthCallbacks, callback);
					}
				}
			}
			this.friendsLoading = false;
			this.InitializeTokenClient();
			if (this.mTokenClient.NeedsToRun())
			{
				Debug.Log("Starting Auth with token client.");
				this.mTokenClient.FetchTokens(delegate(int result)
				{
					this.InitializeGameServices();
					if (result == 0)
					{
						this.GameServices().StartAuthorizationUI();
					}
					else
					{
						this.HandleAuthTransition(Types.AuthOperation.SIGN_IN, (CommonErrorStatus.AuthStatus)result);
					}
				});
			}
			else
			{
				this.InitializeGameServices();
				if (!silent)
				{
					this.GameServices().StartAuthorizationUI();
				}
			}
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x001C2C08 File Offset: 0x001C1008
		private static Action<T> AsOnGameThreadCallback<T>(Action<T> callback)
		{
			if (callback == null)
			{
				return delegate
				{
				};
			}
			return delegate(T result)
			{
				NativeClient.InvokeCallbackOnGameThread<T>(callback, result);
			};
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x001C2C48 File Offset: 0x001C1048
		private static void InvokeCallbackOnGameThread<T, S>(Action<T, S> callback, T data, S msg)
		{
			if (callback == null)
			{
				return;
			}
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				Logger.d("Invoking user callback on game thread");
				callback.Invoke(data, msg);
			});
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x001C2C90 File Offset: 0x001C1090
		private static void InvokeCallbackOnGameThread<T>(Action<T> callback, T data)
		{
			if (callback == null)
			{
				return;
			}
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				Logger.d("Invoking user callback on game thread");
				callback.Invoke(data);
			});
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x001C2CD0 File Offset: 0x001C10D0
		private void InitializeGameServices()
		{
			object gameServicesLock = this.GameServicesLock;
			lock (gameServicesLock)
			{
				if (this.mServices == null)
				{
					using (GameServicesBuilder gameServicesBuilder = GameServicesBuilder.Create())
					{
						using (PlatformConfiguration platformConfiguration = this.clientImpl.CreatePlatformConfiguration(this.mConfiguration))
						{
							this.RegisterInvitationDelegate(this.mConfiguration.InvitationDelegate);
							gameServicesBuilder.SetOnAuthFinishedCallback(new GameServicesBuilder.AuthFinishedCallback(this.HandleAuthTransition));
							gameServicesBuilder.SetOnTurnBasedMatchEventCallback(delegate(Types.MultiplayerEvent eventType, string matchId, NativeTurnBasedMatch match)
							{
								this.mTurnBasedClient.HandleMatchEvent(eventType, matchId, match);
							});
							gameServicesBuilder.SetOnMultiplayerInvitationEventCallback(new Action<Types.MultiplayerEvent, string, GooglePlayGames.Native.PInvoke.MultiplayerInvitation>(this.HandleInvitation));
							if (this.mConfiguration.EnableSavedGames)
							{
								gameServicesBuilder.EnableSnapshots();
							}
							string[] scopes = this.mConfiguration.Scopes;
							for (int i = 0; i < scopes.Length; i++)
							{
								gameServicesBuilder.AddOauthScope(scopes[i]);
							}
							if (this.mConfiguration.IsHidingPopups)
							{
								gameServicesBuilder.SetShowConnectingPopup(false);
							}
							Debug.Log("Building GPG services, implicitly attempts silent auth");
							this.mAuthState = NativeClient.AuthState.SilentPending;
							this.mServices = gameServicesBuilder.Build(platformConfiguration);
							this.mEventsClient = new NativeEventClient(new GooglePlayGames.Native.PInvoke.EventManager(this.mServices));
							this.mVideoClient = new NativeVideoClient(new GooglePlayGames.Native.PInvoke.VideoManager(this.mServices));
							this.mTurnBasedClient = new NativeTurnBasedMultiplayerClient(this, new TurnBasedManager(this.mServices));
							this.mTurnBasedClient.RegisterMatchDelegate(this.mConfiguration.MatchDelegate);
							this.mRealTimeClient = new NativeRealtimeMultiplayerClient(this, new RealtimeManager(this.mServices));
							if (this.mConfiguration.EnableSavedGames)
							{
								this.mSavedGameClient = new NativeSavedGameClient(new GooglePlayGames.Native.PInvoke.SnapshotManager(this.mServices));
							}
							else
							{
								this.mSavedGameClient = new UnsupportedSavedGamesClient("You must enable saved games before it can be used. See PlayGamesClientConfiguration.Builder.EnableSavedGames.");
							}
							this.mAuthState = NativeClient.AuthState.SilentPending;
							this.InitializeTokenClient();
						}
					}
				}
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x001C2F34 File Offset: 0x001C1334
		private void InitializeTokenClient()
		{
			if (this.mTokenClient != null)
			{
				return;
			}
			this.mTokenClient = this.clientImpl.CreateTokenClient(true);
			if (!GameInfo.WebClientIdInitialized() && (this.mConfiguration.IsRequestingIdToken || this.mConfiguration.IsRequestingAuthCode))
			{
				Logger.e("Server Auth Code and ID Token require web clientId to configured.");
			}
			string[] scopes = this.mConfiguration.Scopes;
			this.mTokenClient.SetWebClientId(string.Empty);
			this.mTokenClient.SetRequestAuthCode(this.mConfiguration.IsRequestingAuthCode, this.mConfiguration.IsForcingRefresh);
			this.mTokenClient.SetRequestEmail(this.mConfiguration.IsRequestingEmail);
			this.mTokenClient.SetRequestIdToken(this.mConfiguration.IsRequestingIdToken);
			this.mTokenClient.SetHidePopups(this.mConfiguration.IsHidingPopups);
			this.mTokenClient.AddOauthScopes(scopes);
			this.mTokenClient.SetAccountName(this.mConfiguration.AccountName);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x001C3068 File Offset: 0x001C1468
		internal void HandleInvitation(Types.MultiplayerEvent eventType, string invitationId, GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation)
		{
			Action<Invitation, bool> currentHandler = this.mInvitationDelegate;
			if (currentHandler == null)
			{
				Logger.d(string.Concat(new object[] { "Received ", eventType, " for invitation ", invitationId, " but no handler was registered." }));
				return;
			}
			if (eventType == Types.MultiplayerEvent.REMOVED)
			{
				Logger.d("Ignoring REMOVED for invitation " + invitationId);
				return;
			}
			bool shouldAutolaunch = eventType == Types.MultiplayerEvent.UPDATED_FROM_APP_LAUNCH;
			Invitation invite = invitation.AsInvitation();
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				currentHandler.Invoke(invite, shouldAutolaunch);
			});
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x001C3109 File Offset: 0x001C1509
		public string GetUserEmail()
		{
			if (!this.IsAuthenticated())
			{
				Debug.Log("Cannot get API client - not authenticated");
				return null;
			}
			return this.mTokenClient.GetEmail();
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x001C312F File Offset: 0x001C152F
		public string GetIdToken()
		{
			if (!this.IsAuthenticated())
			{
				Debug.Log("Cannot get API client - not authenticated");
				return null;
			}
			return this.mTokenClient.GetIdToken();
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x001C3155 File Offset: 0x001C1555
		public string GetServerAuthCode()
		{
			if (!this.IsAuthenticated())
			{
				Debug.Log("Cannot get API client - not authenticated");
				return null;
			}
			return this.mTokenClient.GetAuthCode();
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x001C317B File Offset: 0x001C157B
		public void GetAnotherServerAuthCode(bool reAuthenticateIfNeeded, Action<string> callback)
		{
			this.mTokenClient.GetAnotherServerAuthCode(reAuthenticateIfNeeded, callback);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x001C318C File Offset: 0x001C158C
		public bool IsAuthenticated()
		{
			object authStateLock = this.AuthStateLock;
			bool flag;
			lock (authStateLock)
			{
				flag = this.mAuthState == NativeClient.AuthState.Authenticated;
			}
			return flag;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x001C31D0 File Offset: 0x001C15D0
		public void LoadFriends(Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.d("Cannot loadFriends when not authenticated");
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					callback.Invoke(false);
				});
				return;
			}
			if (this.mFriends != null)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					callback.Invoke(true);
				});
				return;
			}
			this.mServices.PlayerManager().FetchFriends(delegate(ResponseStatus status, List<GooglePlayGames.BasicApi.Multiplayer.Player> players)
			{
				if (status == ResponseStatus.Success || status == ResponseStatus.SuccessWithStale)
				{
					this.mFriends = players;
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						callback.Invoke(true);
					});
				}
				else
				{
					this.mFriends = new List<GooglePlayGames.BasicApi.Multiplayer.Player>();
					Logger.e("Got " + status + " loading friends");
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						callback.Invoke(false);
					});
				}
			});
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x001C3254 File Offset: 0x001C1654
		public IUserProfile[] GetFriends()
		{
			if (this.mFriends == null && !this.friendsLoading)
			{
				Logger.w("Getting friends before they are loaded!!!");
				this.friendsLoading = true;
				this.LoadFriends(delegate(bool ok)
				{
					Logger.d(string.Concat(new object[] { "loading: ", ok, " mFriends = ", this.mFriends }));
					if (!ok)
					{
						Logger.e("Friends list did not load successfully.  Disabling loading until re-authenticated");
					}
					this.friendsLoading = !ok;
				});
			}
			return (this.mFriends != null) ? this.mFriends.ToArray() : new IUserProfile[0];
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x001C32C8 File Offset: 0x001C16C8
		private void PopulateAchievements(uint authGeneration, GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse response)
		{
			if (authGeneration != this.mAuthGeneration)
			{
				Logger.d("Received achievement callback after signout occurred, ignoring");
				return;
			}
			Logger.d("Populating Achievements, status = " + response.Status());
			object authStateLock = this.AuthStateLock;
			lock (authStateLock)
			{
				if (response.Status() != CommonErrorStatus.ResponseStatus.VALID && response.Status() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
				{
					Logger.e("Error retrieving achievements - check the log for more information. Failing signin.");
					Action<bool, string> action = this.mPendingAuthCallbacks;
					this.mPendingAuthCallbacks = null;
					if (action != null)
					{
						NativeClient.InvokeCallbackOnGameThread<bool, string>(action, false, "Cannot load achievements, Authenication failing");
					}
					this.SignOut();
					return;
				}
				Dictionary<string, GooglePlayGames.BasicApi.Achievement> dictionary = new Dictionary<string, GooglePlayGames.BasicApi.Achievement>();
				foreach (NativeAchievement nativeAchievement in response)
				{
					using (nativeAchievement)
					{
						dictionary[nativeAchievement.Id()] = nativeAchievement.AsAchievement();
					}
				}
				Logger.d("Found " + dictionary.Count + " Achievements");
				this.mAchievements = dictionary;
			}
			Logger.d("Maybe finish for Achievements");
			this.MaybeFinishAuthentication();
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x001C3438 File Offset: 0x001C1838
		private void MaybeFinishAuthentication()
		{
			Action<bool, string> action = null;
			object authStateLock = this.AuthStateLock;
			lock (authStateLock)
			{
				if (this.mUser == null || this.mAchievements == null)
				{
					Logger.d(string.Concat(new object[] { "Auth not finished. User=", this.mUser, " achievements=", this.mAchievements }));
					return;
				}
				Logger.d("Auth finished. Proceeding.");
				action = this.mPendingAuthCallbacks;
				this.mPendingAuthCallbacks = null;
				this.mAuthState = NativeClient.AuthState.Authenticated;
			}
			if (action != null)
			{
				Logger.d("Invoking Callbacks: " + action);
				NativeClient.InvokeCallbackOnGameThread<bool, string>(action, true, null);
			}
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x001C3508 File Offset: 0x001C1908
		private void PopulateUser(uint authGeneration, GooglePlayGames.Native.PInvoke.PlayerManager.FetchSelfResponse response)
		{
			Logger.d("Populating User");
			if (authGeneration != this.mAuthGeneration)
			{
				Logger.d("Received user callback after signout occurred, ignoring");
				return;
			}
			object authStateLock = this.AuthStateLock;
			lock (authStateLock)
			{
				if (response.Status() != CommonErrorStatus.ResponseStatus.VALID && response.Status() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
				{
					Logger.e("Error retrieving user, signing out");
					Action<bool, string> action = this.mPendingAuthCallbacks;
					this.mPendingAuthCallbacks = null;
					if (action != null)
					{
						NativeClient.InvokeCallbackOnGameThread<bool, string>(action, false, "Cannot load user profile");
					}
					this.SignOut();
					return;
				}
				this.mUser = response.Self().AsPlayer();
				this.mFriends = null;
			}
			Logger.d("Found User: " + this.mUser);
			Logger.d("Maybe finish for User");
			this.MaybeFinishAuthentication();
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x001C35F8 File Offset: 0x001C19F8
		private void HandleAuthTransition(Types.AuthOperation operation, CommonErrorStatus.AuthStatus status)
		{
			Logger.d(string.Concat(new object[] { "Starting Auth Transition. Op: ", operation, " status: ", status }));
			object authStateLock = this.AuthStateLock;
			lock (authStateLock)
			{
				if (operation != Types.AuthOperation.SIGN_IN)
				{
					if (operation != Types.AuthOperation.SIGN_OUT)
					{
						Logger.e("Unknown AuthOperation " + operation);
					}
					else
					{
						this.ToUnauthenticated();
					}
				}
				else if (status == CommonErrorStatus.AuthStatus.VALID)
				{
					if (this.mSilentAuthCallbacks != null)
					{
						this.mPendingAuthCallbacks = (Action<bool, string>)Delegate.Combine(this.mPendingAuthCallbacks, this.mSilentAuthCallbacks);
						this.mSilentAuthCallbacks = null;
					}
					uint currentAuthGeneration = this.mAuthGeneration;
					this.mServices.AchievementManager().FetchAll(delegate(GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse results)
					{
						this.PopulateAchievements(currentAuthGeneration, results);
					});
					this.mServices.PlayerManager().FetchSelf(delegate(GooglePlayGames.Native.PInvoke.PlayerManager.FetchSelfResponse results)
					{
						this.PopulateUser(currentAuthGeneration, results);
					});
				}
				else if (this.mAuthState == NativeClient.AuthState.SilentPending)
				{
					this.mSilentAuthFailed = true;
					this.mAuthState = NativeClient.AuthState.Unauthenticated;
					Action<bool, string> action = this.mSilentAuthCallbacks;
					this.mSilentAuthCallbacks = null;
					Logger.d("Invoking callbacks, AuthState changed from silentPending to Unauthenticated.");
					NativeClient.InvokeCallbackOnGameThread<bool, string>(action, false, "silent auth failed");
					if (this.mPendingAuthCallbacks != null)
					{
						Logger.d("there are pending auth callbacks - starting AuthUI");
						this.GameServices().StartAuthorizationUI();
					}
				}
				else
				{
					Logger.d("AuthState == " + this.mAuthState + " calling auth callbacks with failure");
					this.UnpauseUnityPlayer();
					Action<bool, string> action2 = this.mPendingAuthCallbacks;
					this.mPendingAuthCallbacks = null;
					NativeClient.InvokeCallbackOnGameThread<bool, string>(action2, false, "Authentication failed");
				}
			}
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x001C37F4 File Offset: 0x001C1BF4
		private void UnpauseUnityPlayer()
		{
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x001C37F8 File Offset: 0x001C1BF8
		private void ToUnauthenticated()
		{
			object authStateLock = this.AuthStateLock;
			lock (authStateLock)
			{
				this.mUser = null;
				this.mFriends = null;
				this.mAchievements = null;
				this.mAuthState = NativeClient.AuthState.Unauthenticated;
				this.mTokenClient = this.clientImpl.CreateTokenClient(true);
				this.mAuthGeneration += 1U;
			}
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x001C3878 File Offset: 0x001C1C78
		public void SignOut()
		{
			this.ToUnauthenticated();
			if (this.GameServices() == null)
			{
				return;
			}
			this.mTokenClient.Signout();
			this.GameServices().SignOut();
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x001C38A4 File Offset: 0x001C1CA4
		public string GetUserId()
		{
			if (this.mUser == null)
			{
				return null;
			}
			return this.mUser.id;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x001C38C2 File Offset: 0x001C1CC2
		public string GetUserDisplayName()
		{
			if (this.mUser == null)
			{
				return null;
			}
			return this.mUser.userName;
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x001C38E0 File Offset: 0x001C1CE0
		public string GetUserImageUrl()
		{
			if (this.mUser == null)
			{
				return null;
			}
			return this.mUser.AvatarURL;
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x001C3900 File Offset: 0x001C1D00
		public void SetGravityForPopups(Gravity gravity)
		{
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				this.clientImpl.SetGravityForPopups(this.GetApiClient(), gravity);
			});
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x001C3934 File Offset: 0x001C1D34
		public void GetPlayerStats(Action<CommonStatusCodes, GooglePlayGames.BasicApi.PlayerStats> callback)
		{
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				this.clientImpl.GetPlayerStats(this.GetApiClient(), callback);
			});
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x001C3968 File Offset: 0x001C1D68
		public void LoadUsers(string[] userIds, Action<IUserProfile[]> callback)
		{
			this.mServices.PlayerManager().FetchList(userIds, delegate(NativePlayer[] nativeUsers)
			{
				IUserProfile[] users = new IUserProfile[nativeUsers.Length];
				for (int i = 0; i < users.Length; i++)
				{
					users[i] = nativeUsers[i].AsPlayer();
				}
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					callback.Invoke(users);
				});
			});
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x001C399F File Offset: 0x001C1D9F
		public GooglePlayGames.BasicApi.Achievement GetAchievement(string achId)
		{
			if (this.mAchievements == null || !this.mAchievements.ContainsKey(achId))
			{
				return null;
			}
			return this.mAchievements[achId];
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x001C39D4 File Offset: 0x001C1DD4
		public void LoadAchievements(Action<GooglePlayGames.BasicApi.Achievement[]> callback)
		{
			GooglePlayGames.BasicApi.Achievement[] data = new GooglePlayGames.BasicApi.Achievement[this.mAchievements.Count];
			this.mAchievements.Values.CopyTo(data, 0);
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				callback.Invoke(data);
			});
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x001C3A30 File Offset: 0x001C1E30
		public void UnlockAchievement(string achId, Action<bool> callback)
		{
			this.UpdateAchievement("Unlock", achId, callback, (GooglePlayGames.BasicApi.Achievement a) => a.IsUnlocked, delegate(GooglePlayGames.BasicApi.Achievement a)
			{
				a.IsUnlocked = true;
				this.GameServices().AchievementManager().Unlock(achId);
			});
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x001C3A8C File Offset: 0x001C1E8C
		public void RevealAchievement(string achId, Action<bool> callback)
		{
			this.UpdateAchievement("Reveal", achId, callback, (GooglePlayGames.BasicApi.Achievement a) => a.IsRevealed, delegate(GooglePlayGames.BasicApi.Achievement a)
			{
				a.IsRevealed = true;
				this.GameServices().AchievementManager().Reveal(achId);
			});
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x001C3AE8 File Offset: 0x001C1EE8
		private void UpdateAchievement(string updateType, string achId, Action<bool> callback, Predicate<GooglePlayGames.BasicApi.Achievement> alreadyDone, Action<GooglePlayGames.BasicApi.Achievement> updateAchievment)
		{
			callback = NativeClient.AsOnGameThreadCallback<bool>(callback);
			Misc.CheckNotNull<string>(achId);
			this.InitializeGameServices();
			GooglePlayGames.BasicApi.Achievement achievement = this.GetAchievement(achId);
			if (achievement == null)
			{
				Logger.d("Could not " + updateType + ", no achievement with ID " + achId);
				callback.Invoke(false);
				return;
			}
			if (alreadyDone.Invoke(achievement))
			{
				Logger.d("Did not need to perform " + updateType + ": on achievement " + achId);
				callback.Invoke(true);
				return;
			}
			Logger.d("Performing " + updateType + " on " + achId);
			updateAchievment.Invoke(achievement);
			this.GameServices().AchievementManager().Fetch(achId, delegate(GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse rsp)
			{
				if (rsp.Status() == CommonErrorStatus.ResponseStatus.VALID)
				{
					this.mAchievements.Remove(achId);
					this.mAchievements.Add(achId, rsp.Achievement().AsAchievement());
					callback.Invoke(true);
				}
				else
				{
					Logger.e(string.Concat(new object[]
					{
						"Cannot refresh achievement ",
						achId,
						": ",
						rsp.Status()
					}));
					callback.Invoke(false);
				}
			});
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x001C3BE8 File Offset: 0x001C1FE8
		public void IncrementAchievement(string achId, int steps, Action<bool> callback)
		{
			Misc.CheckNotNull<string>(achId);
			callback = NativeClient.AsOnGameThreadCallback<bool>(callback);
			this.InitializeGameServices();
			GooglePlayGames.BasicApi.Achievement achievement = this.GetAchievement(achId);
			if (achievement == null)
			{
				Logger.e("Could not increment, no achievement with ID " + achId);
				callback.Invoke(false);
				return;
			}
			if (!achievement.IsIncremental)
			{
				Logger.e("Could not increment, achievement with ID " + achId + " was not incremental");
				callback.Invoke(false);
				return;
			}
			if (steps < 0)
			{
				Logger.e("Attempted to increment by negative steps");
				callback.Invoke(false);
				return;
			}
			this.GameServices().AchievementManager().Increment(achId, Convert.ToUInt32(steps));
			this.GameServices().AchievementManager().Fetch(achId, delegate(GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse rsp)
			{
				if (rsp.Status() == CommonErrorStatus.ResponseStatus.VALID)
				{
					this.mAchievements.Remove(achId);
					this.mAchievements.Add(achId, rsp.Achievement().AsAchievement());
					callback.Invoke(true);
				}
				else
				{
					Logger.e(string.Concat(new object[]
					{
						"Cannot refresh achievement ",
						achId,
						": ",
						rsp.Status()
					}));
					callback.Invoke(false);
				}
			});
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x001C3CF8 File Offset: 0x001C20F8
		public void SetStepsAtLeast(string achId, int steps, Action<bool> callback)
		{
			Misc.CheckNotNull<string>(achId);
			callback = NativeClient.AsOnGameThreadCallback<bool>(callback);
			this.InitializeGameServices();
			GooglePlayGames.BasicApi.Achievement achievement = this.GetAchievement(achId);
			if (achievement == null)
			{
				Logger.e("Could not increment, no achievement with ID " + achId);
				callback.Invoke(false);
				return;
			}
			if (!achievement.IsIncremental)
			{
				Logger.e("Could not increment, achievement with ID " + achId + " is not incremental");
				callback.Invoke(false);
				return;
			}
			if (steps < 0)
			{
				Logger.e("Attempted to increment by negative steps");
				callback.Invoke(false);
				return;
			}
			this.GameServices().AchievementManager().SetStepsAtLeast(achId, Convert.ToUInt32(steps));
			this.GameServices().AchievementManager().Fetch(achId, delegate(GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse rsp)
			{
				if (rsp.Status() == CommonErrorStatus.ResponseStatus.VALID)
				{
					this.mAchievements.Remove(achId);
					this.mAchievements.Add(achId, rsp.Achievement().AsAchievement());
					callback.Invoke(true);
				}
				else
				{
					Logger.e(string.Concat(new object[]
					{
						"Cannot refresh achievement ",
						achId,
						": ",
						rsp.Status()
					}));
					callback.Invoke(false);
				}
			});
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x001C3E08 File Offset: 0x001C2208
		public void ShowAchievementsUI(Action<UIStatus> cb)
		{
			if (!this.IsAuthenticated())
			{
				return;
			}
			Action<CommonErrorStatus.UIStatus> action = Callbacks.NoopUICallback;
			if (cb != null)
			{
				action = delegate(CommonErrorStatus.UIStatus result)
				{
					cb.Invoke((UIStatus)result);
				};
			}
			action = NativeClient.AsOnGameThreadCallback<CommonErrorStatus.UIStatus>(action);
			this.GameServices().AchievementManager().ShowAllUI(action);
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x001C3E64 File Offset: 0x001C2264
		public int LeaderboardMaxResults()
		{
			return this.GameServices().LeaderboardManager().LeaderboardMaxResults;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x001C3E78 File Offset: 0x001C2278
		public void ShowLeaderboardUI(string leaderboardId, LeaderboardTimeSpan span, Action<UIStatus> cb)
		{
			if (!this.IsAuthenticated())
			{
				return;
			}
			Action<CommonErrorStatus.UIStatus> action = Callbacks.NoopUICallback;
			if (cb != null)
			{
				action = delegate(CommonErrorStatus.UIStatus result)
				{
					cb.Invoke((UIStatus)result);
				};
			}
			action = NativeClient.AsOnGameThreadCallback<CommonErrorStatus.UIStatus>(action);
			if (leaderboardId == null)
			{
				this.GameServices().LeaderboardManager().ShowAllUI(action);
			}
			else
			{
				this.GameServices().LeaderboardManager().ShowUI(leaderboardId, span, action);
			}
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x001C3EF2 File Offset: 0x001C22F2
		public void LoadScores(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, Action<LeaderboardScoreData> callback)
		{
			callback = NativeClient.AsOnGameThreadCallback<LeaderboardScoreData>(callback);
			this.GameServices().LeaderboardManager().LoadLeaderboardData(leaderboardId, start, rowCount, collection, timeSpan, this.mUser.id, callback);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x001C3F23 File Offset: 0x001C2323
		public void LoadMoreScores(ScorePageToken token, int rowCount, Action<LeaderboardScoreData> callback)
		{
			callback = NativeClient.AsOnGameThreadCallback<LeaderboardScoreData>(callback);
			this.GameServices().LeaderboardManager().LoadScorePage(null, rowCount, token, callback);
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x001C3F44 File Offset: 0x001C2344
		public void SubmitScore(string leaderboardId, long score, Action<bool> callback)
		{
			callback = NativeClient.AsOnGameThreadCallback<bool>(callback);
			if (!this.IsAuthenticated())
			{
				callback.Invoke(false);
			}
			this.InitializeGameServices();
			if (leaderboardId == null)
			{
				throw new ArgumentNullException("leaderboardId");
			}
			this.GameServices().LeaderboardManager().SubmitScore(leaderboardId, score, null);
			callback.Invoke(true);
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x001C3F9C File Offset: 0x001C239C
		public void SubmitScore(string leaderboardId, long score, string metadata, Action<bool> callback)
		{
			callback = NativeClient.AsOnGameThreadCallback<bool>(callback);
			if (!this.IsAuthenticated())
			{
				callback.Invoke(false);
			}
			this.InitializeGameServices();
			if (leaderboardId == null)
			{
				throw new ArgumentNullException("leaderboardId");
			}
			this.GameServices().LeaderboardManager().SubmitScore(leaderboardId, score, metadata);
			callback.Invoke(true);
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x001C3FF8 File Offset: 0x001C23F8
		public IRealTimeMultiplayerClient GetRtmpClient()
		{
			if (!this.IsAuthenticated())
			{
				return null;
			}
			object gameServicesLock = this.GameServicesLock;
			IRealTimeMultiplayerClient realTimeMultiplayerClient;
			lock (gameServicesLock)
			{
				realTimeMultiplayerClient = this.mRealTimeClient;
			}
			return realTimeMultiplayerClient;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x001C4048 File Offset: 0x001C2448
		public ITurnBasedMultiplayerClient GetTbmpClient()
		{
			object gameServicesLock = this.GameServicesLock;
			ITurnBasedMultiplayerClient turnBasedMultiplayerClient;
			lock (gameServicesLock)
			{
				turnBasedMultiplayerClient = this.mTurnBasedClient;
			}
			return turnBasedMultiplayerClient;
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x001C4088 File Offset: 0x001C2488
		public ISavedGameClient GetSavedGameClient()
		{
			object gameServicesLock = this.GameServicesLock;
			ISavedGameClient savedGameClient;
			lock (gameServicesLock)
			{
				savedGameClient = this.mSavedGameClient;
			}
			return savedGameClient;
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x001C40C8 File Offset: 0x001C24C8
		public IEventsClient GetEventsClient()
		{
			object gameServicesLock = this.GameServicesLock;
			IEventsClient eventsClient;
			lock (gameServicesLock)
			{
				eventsClient = this.mEventsClient;
			}
			return eventsClient;
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x001C4108 File Offset: 0x001C2508
		public IVideoClient GetVideoClient()
		{
			object gameServicesLock = this.GameServicesLock;
			IVideoClient videoClient;
			lock (gameServicesLock)
			{
				videoClient = this.mVideoClient;
			}
			return videoClient;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x001C4148 File Offset: 0x001C2548
		public void RegisterInvitationDelegate(InvitationReceivedDelegate invitationDelegate)
		{
			if (invitationDelegate == null)
			{
				this.mInvitationDelegate = null;
			}
			else
			{
				this.mInvitationDelegate = Callbacks.AsOnGameThreadCallback<Invitation, bool>(delegate(Invitation invitation, bool autoAccept)
				{
					invitationDelegate(invitation, autoAccept);
				});
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x001C4194 File Offset: 0x001C2594
		public IntPtr GetApiClient()
		{
			return InternalHooks.InternalHooks_GetApiClient(this.mServices.AsHandle());
		}

		// Token: 0x0400325D RID: 12893
		private readonly IClientImpl clientImpl;

		// Token: 0x0400325E RID: 12894
		private readonly object GameServicesLock = new object();

		// Token: 0x0400325F RID: 12895
		private readonly object AuthStateLock = new object();

		// Token: 0x04003260 RID: 12896
		private readonly PlayGamesClientConfiguration mConfiguration;

		// Token: 0x04003261 RID: 12897
		private GooglePlayGames.Native.PInvoke.GameServices mServices;

		// Token: 0x04003262 RID: 12898
		private volatile NativeTurnBasedMultiplayerClient mTurnBasedClient;

		// Token: 0x04003263 RID: 12899
		private volatile NativeRealtimeMultiplayerClient mRealTimeClient;

		// Token: 0x04003264 RID: 12900
		private volatile ISavedGameClient mSavedGameClient;

		// Token: 0x04003265 RID: 12901
		private volatile IEventsClient mEventsClient;

		// Token: 0x04003266 RID: 12902
		private volatile IVideoClient mVideoClient;

		// Token: 0x04003267 RID: 12903
		private volatile TokenClient mTokenClient;

		// Token: 0x04003268 RID: 12904
		private volatile Action<Invitation, bool> mInvitationDelegate;

		// Token: 0x04003269 RID: 12905
		private volatile Dictionary<string, GooglePlayGames.BasicApi.Achievement> mAchievements;

		// Token: 0x0400326A RID: 12906
		private volatile GooglePlayGames.BasicApi.Multiplayer.Player mUser;

		// Token: 0x0400326B RID: 12907
		private volatile List<GooglePlayGames.BasicApi.Multiplayer.Player> mFriends;

		// Token: 0x0400326C RID: 12908
		private volatile Action<bool, string> mPendingAuthCallbacks;

		// Token: 0x0400326D RID: 12909
		private volatile Action<bool, string> mSilentAuthCallbacks;

		// Token: 0x0400326E RID: 12910
		private volatile NativeClient.AuthState mAuthState;

		// Token: 0x0400326F RID: 12911
		private volatile uint mAuthGeneration;

		// Token: 0x04003270 RID: 12912
		private volatile bool mSilentAuthFailed;

		// Token: 0x04003271 RID: 12913
		private volatile bool friendsLoading;

		// Token: 0x020006BA RID: 1722
		private enum AuthState
		{
			// Token: 0x04003275 RID: 12917
			Unauthenticated,
			// Token: 0x04003276 RID: 12918
			Authenticated,
			// Token: 0x04003277 RID: 12919
			SilentPending
		}
	}
}
