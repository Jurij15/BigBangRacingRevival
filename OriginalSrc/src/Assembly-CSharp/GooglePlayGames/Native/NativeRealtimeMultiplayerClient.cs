using System;
using System.Collections.Generic;
using System.Linq;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native
{
	// Token: 0x020006C0 RID: 1728
	public class NativeRealtimeMultiplayerClient : IRealTimeMultiplayerClient
	{
		// Token: 0x06003170 RID: 12656 RVA: 0x001C5158 File Offset: 0x001C3558
		internal NativeRealtimeMultiplayerClient(NativeClient nativeClient, RealtimeManager manager)
		{
			this.mNativeClient = Misc.CheckNotNull<NativeClient>(nativeClient);
			this.mRealtimeManager = Misc.CheckNotNull<RealtimeManager>(manager);
			this.mCurrentSession = this.GetTerminatedSession();
			PlayGamesHelperObject.AddPauseCallback(new Action<bool>(this.HandleAppPausing));
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x001C51B0 File Offset: 0x001C35B0
		private NativeRealtimeMultiplayerClient.RoomSession GetTerminatedSession()
		{
			NativeRealtimeMultiplayerClient.RoomSession roomSession = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, new NativeRealtimeMultiplayerClient.NoopListener());
			roomSession.EnterState(new NativeRealtimeMultiplayerClient.ShutdownState(roomSession), false);
			return roomSession;
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x001C51DC File Offset: 0x001C35DC
		public void CreateQuickGame(uint minOpponents, uint maxOpponents, uint variant, RealTimeMultiplayerListener listener)
		{
			this.CreateQuickGame(minOpponents, maxOpponents, variant, 0UL, listener);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x001C51EC File Offset: 0x001C35EC
		public void CreateQuickGame(uint minOpponents, uint maxOpponents, uint variant, ulong exclusiveBitMask, RealTimeMultiplayerListener listener)
		{
			object obj = this.mSessionLock;
			lock (obj)
			{
				NativeRealtimeMultiplayerClient.RoomSession newSession = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, listener);
				if (this.mCurrentSession.IsActive())
				{
					Logger.e("Received attempt to create a new room without cleaning up the old one.");
					newSession.LeaveRoom();
				}
				else
				{
					this.mCurrentSession = newSession;
					Logger.d("QuickGame: Setting MinPlayersToStart = " + minOpponents);
					this.mCurrentSession.MinPlayersToStart = minOpponents;
					using (RealtimeRoomConfigBuilder realtimeRoomConfigBuilder = RealtimeRoomConfigBuilder.Create())
					{
						RealtimeRoomConfig config = realtimeRoomConfigBuilder.SetMinimumAutomatchingPlayers(minOpponents).SetMaximumAutomatchingPlayers(maxOpponents).SetVariant(variant)
							.SetExclusiveBitMask(exclusiveBitMask)
							.Build();
						using (config)
						{
							using (GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper helper = NativeRealtimeMultiplayerClient.HelperForSession(newSession))
							{
								newSession.StartRoomCreation(this.mNativeClient.GetUserId(), delegate
								{
									this.mRealtimeManager.CreateRoom(config, helper, new Action<RealtimeManager.RealTimeRoomResponse>(newSession.HandleRoomResponse));
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x001C53BC File Offset: 0x001C37BC
		private static GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper HelperForSession(NativeRealtimeMultiplayerClient.RoomSession session)
		{
			return GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper.Create().SetOnDataReceivedCallback(delegate(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant, byte[] data, bool isReliable)
			{
				session.OnDataReceived(room, participant, data, isReliable);
			}).SetOnParticipantStatusChangedCallback(delegate(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				session.OnParticipantStatusChanged(room, participant);
			})
				.SetOnRoomConnectedSetChangedCallback(delegate(NativeRealTimeRoom room)
				{
					session.OnConnectedSetChanged(room);
				})
				.SetOnRoomStatusChangedCallback(delegate(NativeRealTimeRoom room)
				{
					session.OnRoomStatusChanged(room);
				});
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x001C541F File Offset: 0x001C381F
		private void HandleAppPausing(bool paused)
		{
			if (paused)
			{
				Logger.d("Application is pausing, which disconnects the RTMP  client.  Leaving room.");
				this.LeaveRoom();
			}
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x001C5438 File Offset: 0x001C3838
		public void CreateWithInvitationScreen(uint minOpponents, uint maxOppponents, uint variant, RealTimeMultiplayerListener listener)
		{
			object obj = this.mSessionLock;
			lock (obj)
			{
				NativeRealtimeMultiplayerClient.RoomSession newRoom = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, listener);
				if (this.mCurrentSession.IsActive())
				{
					Logger.e("Received attempt to create a new room without cleaning up the old one.");
					newRoom.LeaveRoom();
				}
				else
				{
					this.mCurrentSession = newRoom;
					this.mCurrentSession.ShowingUI = true;
					this.mRealtimeManager.ShowPlayerSelectUI(minOpponents, maxOppponents, true, delegate(PlayerSelectUIResponse response)
					{
						this.mCurrentSession.ShowingUI = false;
						if (response.Status() != CommonErrorStatus.UIStatus.VALID)
						{
							Logger.d("User did not complete invitation screen.");
							newRoom.LeaveRoom();
							return;
						}
						this.mCurrentSession.MinPlayersToStart = response.MinimumAutomatchingPlayers() + (uint)Enumerable.Count<string>(response) + 1U;
						using (RealtimeRoomConfigBuilder realtimeRoomConfigBuilder = RealtimeRoomConfigBuilder.Create())
						{
							realtimeRoomConfigBuilder.SetVariant(variant);
							realtimeRoomConfigBuilder.PopulateFromUIResponse(response);
							using (RealtimeRoomConfig config = realtimeRoomConfigBuilder.Build())
							{
								using (GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper helper = NativeRealtimeMultiplayerClient.HelperForSession(newRoom))
								{
									newRoom.StartRoomCreation(this.mNativeClient.GetUserId(), delegate
									{
										this.mRealtimeManager.CreateRoom(config, helper, new Action<RealtimeManager.RealTimeRoomResponse>(newRoom.HandleRoomResponse));
									});
								}
							}
						}
					});
				}
			}
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x001C5504 File Offset: 0x001C3904
		public void ShowWaitingRoomUI()
		{
			object obj = this.mSessionLock;
			lock (obj)
			{
				this.mCurrentSession.ShowWaitingRoomUI();
			}
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x001C5548 File Offset: 0x001C3948
		public void GetAllInvitations(Action<Invitation[]> callback)
		{
			this.mRealtimeManager.FetchInvitations(delegate(RealtimeManager.FetchInvitationsResponse response)
			{
				if (!response.RequestSucceeded())
				{
					Logger.e("Couldn't load invitations.");
					callback.Invoke(new Invitation[0]);
					return;
				}
				List<Invitation> list = new List<Invitation>();
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation multiplayerInvitation in response.Invitations())
				{
					using (multiplayerInvitation)
					{
						list.Add(multiplayerInvitation.AsInvitation());
					}
				}
				callback.Invoke(list.ToArray());
			});
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x001C557C File Offset: 0x001C397C
		public void AcceptFromInbox(RealTimeMultiplayerListener listener)
		{
			object obj = this.mSessionLock;
			lock (obj)
			{
				NativeRealtimeMultiplayerClient.<AcceptFromInbox>c__AnonStorey9 <AcceptFromInbox>c__AnonStorey = new NativeRealtimeMultiplayerClient.<AcceptFromInbox>c__AnonStorey9();
				<AcceptFromInbox>c__AnonStorey.$this = this;
				<AcceptFromInbox>c__AnonStorey.newRoom = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, listener);
				if (this.mCurrentSession.IsActive())
				{
					Logger.e("Received attempt to accept invitation without cleaning up active session.");
					<AcceptFromInbox>c__AnonStorey.newRoom.LeaveRoom();
				}
				else
				{
					this.mCurrentSession = <AcceptFromInbox>c__AnonStorey.newRoom;
					this.mCurrentSession.ShowingUI = true;
					this.mRealtimeManager.ShowRoomInboxUI(delegate(RealtimeManager.RoomInboxUIResponse response)
					{
						NativeRealtimeMultiplayerClient.<AcceptFromInbox>c__AnonStorey9.<AcceptFromInbox>c__AnonStoreyA <AcceptFromInbox>c__AnonStoreyA = new NativeRealtimeMultiplayerClient.<AcceptFromInbox>c__AnonStorey9.<AcceptFromInbox>c__AnonStoreyA();
						<AcceptFromInbox>c__AnonStoreyA.<>f__ref$9 = <AcceptFromInbox>c__AnonStorey;
						<AcceptFromInbox>c__AnonStorey.$this.mCurrentSession.ShowingUI = false;
						if (response.ResponseStatus() != CommonErrorStatus.UIStatus.VALID)
						{
							Logger.d("User did not complete invitation screen.");
							<AcceptFromInbox>c__AnonStorey.newRoom.LeaveRoom();
							return;
						}
						<AcceptFromInbox>c__AnonStoreyA.invitation = response.Invitation();
						using (GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper helper = NativeRealtimeMultiplayerClient.HelperForSession(<AcceptFromInbox>c__AnonStorey.newRoom))
						{
							Logger.d("About to accept invitation " + <AcceptFromInbox>c__AnonStoreyA.invitation.Id());
							<AcceptFromInbox>c__AnonStorey.newRoom.StartRoomCreation(<AcceptFromInbox>c__AnonStorey.$this.mNativeClient.GetUserId(), delegate
							{
								<AcceptFromInbox>c__AnonStorey.mRealtimeManager.AcceptInvitation(<AcceptFromInbox>c__AnonStoreyA.invitation, helper, delegate(RealtimeManager.RealTimeRoomResponse acceptResponse)
								{
									using (<AcceptFromInbox>c__AnonStoreyA.invitation)
									{
										<AcceptFromInbox>c__AnonStorey.newRoom.HandleRoomResponse(acceptResponse);
										<AcceptFromInbox>c__AnonStorey.newRoom.SetInvitation(<AcceptFromInbox>c__AnonStoreyA.invitation.AsInvitation());
									}
								});
							});
						}
					});
				}
			}
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x001C5630 File Offset: 0x001C3A30
		public void AcceptInvitation(string invitationId, RealTimeMultiplayerListener listener)
		{
			object obj = this.mSessionLock;
			lock (obj)
			{
				NativeRealtimeMultiplayerClient.RoomSession newRoom = new NativeRealtimeMultiplayerClient.RoomSession(this.mRealtimeManager, listener);
				if (this.mCurrentSession.IsActive())
				{
					Logger.e("Received attempt to accept invitation without cleaning up active session.");
					newRoom.LeaveRoom();
				}
				else
				{
					this.mCurrentSession = newRoom;
					this.mRealtimeManager.FetchInvitations(delegate(RealtimeManager.FetchInvitationsResponse response)
					{
						if (!response.RequestSucceeded())
						{
							Logger.e("Couldn't load invitations.");
							newRoom.LeaveRoom();
							return;
						}
						using (IEnumerator<GooglePlayGames.Native.PInvoke.MultiplayerInvitation> enumerator = response.Invitations().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation = enumerator.Current;
								using (invitation)
								{
									if (invitation.Id().Equals(invitationId))
									{
										this.mCurrentSession.MinPlayersToStart = invitation.AutomatchingSlots() + invitation.ParticipantCount();
										Logger.d("Setting MinPlayersToStart with invitation to : " + this.mCurrentSession.MinPlayersToStart);
										using (GooglePlayGames.Native.PInvoke.RealTimeEventListenerHelper helper = NativeRealtimeMultiplayerClient.HelperForSession(newRoom))
										{
											newRoom.StartRoomCreation(this.mNativeClient.GetUserId(), delegate
											{
												this.mRealtimeManager.AcceptInvitation(invitation, helper, new Action<RealtimeManager.RealTimeRoomResponse>(newRoom.HandleRoomResponse));
											});
											return;
										}
									}
								}
							}
						}
						Logger.e("Room creation failed since we could not find invitation with ID " + invitationId);
						newRoom.LeaveRoom();
					});
				}
			}
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x001C56EC File Offset: 0x001C3AEC
		public Invitation GetInvitation()
		{
			return this.mCurrentSession.GetInvitation();
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x001C56FB File Offset: 0x001C3AFB
		public void LeaveRoom()
		{
			this.mCurrentSession.LeaveRoom();
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x001C570A File Offset: 0x001C3B0A
		public void SendMessageToAll(bool reliable, byte[] data)
		{
			this.mCurrentSession.SendMessageToAll(reliable, data);
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x001C571B File Offset: 0x001C3B1B
		public void SendMessageToAll(bool reliable, byte[] data, int offset, int length)
		{
			this.mCurrentSession.SendMessageToAll(reliable, data, offset, length);
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x001C572F File Offset: 0x001C3B2F
		public void SendMessage(bool reliable, string participantId, byte[] data)
		{
			this.mCurrentSession.SendMessage(reliable, participantId, data);
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x001C5741 File Offset: 0x001C3B41
		public void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length)
		{
			this.mCurrentSession.SendMessage(reliable, participantId, data, offset, length);
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x001C5757 File Offset: 0x001C3B57
		public List<Participant> GetConnectedParticipants()
		{
			return this.mCurrentSession.GetConnectedParticipants();
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x001C5766 File Offset: 0x001C3B66
		public Participant GetSelf()
		{
			return this.mCurrentSession.GetSelf();
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x001C5775 File Offset: 0x001C3B75
		public Participant GetParticipant(string participantId)
		{
			return this.mCurrentSession.GetParticipant(participantId);
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x001C5785 File Offset: 0x001C3B85
		public bool IsRoomConnected()
		{
			return this.mCurrentSession.IsRoomConnected();
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x001C5794 File Offset: 0x001C3B94
		public void DeclineInvitation(string invitationId)
		{
			this.mRealtimeManager.FetchInvitations(delegate(RealtimeManager.FetchInvitationsResponse response)
			{
				if (!response.RequestSucceeded())
				{
					Logger.e("Couldn't load invitations.");
					return;
				}
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation multiplayerInvitation in response.Invitations())
				{
					using (multiplayerInvitation)
					{
						if (multiplayerInvitation.Id().Equals(invitationId))
						{
							this.mRealtimeManager.DeclineInvitation(multiplayerInvitation);
						}
					}
				}
			});
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x001C57CC File Offset: 0x001C3BCC
		private static T WithDefault<T>(T presented, T defaultValue) where T : class
		{
			return (presented == null) ? defaultValue : presented;
		}

		// Token: 0x04003280 RID: 12928
		private readonly object mSessionLock = new object();

		// Token: 0x04003281 RID: 12929
		private readonly NativeClient mNativeClient;

		// Token: 0x04003282 RID: 12930
		private readonly RealtimeManager mRealtimeManager;

		// Token: 0x04003283 RID: 12931
		private volatile NativeRealtimeMultiplayerClient.RoomSession mCurrentSession;

		// Token: 0x020006C1 RID: 1729
		private class NoopListener : RealTimeMultiplayerListener
		{
			// Token: 0x06003188 RID: 12680 RVA: 0x001C57E8 File Offset: 0x001C3BE8
			public void OnRoomSetupProgress(float percent)
			{
			}

			// Token: 0x06003189 RID: 12681 RVA: 0x001C57EA File Offset: 0x001C3BEA
			public void OnRoomConnected(bool success)
			{
			}

			// Token: 0x0600318A RID: 12682 RVA: 0x001C57EC File Offset: 0x001C3BEC
			public void OnLeftRoom()
			{
			}

			// Token: 0x0600318B RID: 12683 RVA: 0x001C57EE File Offset: 0x001C3BEE
			public void OnParticipantLeft(Participant participant)
			{
			}

			// Token: 0x0600318C RID: 12684 RVA: 0x001C57F0 File Offset: 0x001C3BF0
			public void OnPeersConnected(string[] participantIds)
			{
			}

			// Token: 0x0600318D RID: 12685 RVA: 0x001C57F2 File Offset: 0x001C3BF2
			public void OnPeersDisconnected(string[] participantIds)
			{
			}

			// Token: 0x0600318E RID: 12686 RVA: 0x001C57F4 File Offset: 0x001C3BF4
			public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
			{
			}
		}

		// Token: 0x020006C2 RID: 1730
		private class RoomSession
		{
			// Token: 0x0600318F RID: 12687 RVA: 0x001C57F8 File Offset: 0x001C3BF8
			internal RoomSession(RealtimeManager manager, RealTimeMultiplayerListener listener)
			{
				this.mManager = Misc.CheckNotNull<RealtimeManager>(manager);
				this.mListener = new NativeRealtimeMultiplayerClient.OnGameThreadForwardingListener(listener);
				this.EnterState(new NativeRealtimeMultiplayerClient.BeforeRoomCreateStartedState(this), false);
				this.mStillPreRoomCreation = true;
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06003190 RID: 12688 RVA: 0x001C5844 File Offset: 0x001C3C44
			// (set) Token: 0x06003191 RID: 12689 RVA: 0x001C584E File Offset: 0x001C3C4E
			internal bool ShowingUI
			{
				get
				{
					return this.mShowingUI;
				}
				set
				{
					this.mShowingUI = value;
				}
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06003192 RID: 12690 RVA: 0x001C5859 File Offset: 0x001C3C59
			// (set) Token: 0x06003193 RID: 12691 RVA: 0x001C5861 File Offset: 0x001C3C61
			internal uint MinPlayersToStart
			{
				get
				{
					return this.mMinPlayersToStart;
				}
				set
				{
					this.mMinPlayersToStart = value;
				}
			}

			// Token: 0x06003194 RID: 12692 RVA: 0x001C586A File Offset: 0x001C3C6A
			internal RealtimeManager Manager()
			{
				return this.mManager;
			}

			// Token: 0x06003195 RID: 12693 RVA: 0x001C5872 File Offset: 0x001C3C72
			internal bool IsActive()
			{
				return this.mState.IsActive();
			}

			// Token: 0x06003196 RID: 12694 RVA: 0x001C5881 File Offset: 0x001C3C81
			internal string SelfPlayerId()
			{
				return this.mCurrentPlayerId;
			}

			// Token: 0x06003197 RID: 12695 RVA: 0x001C588B File Offset: 0x001C3C8B
			public void SetInvitation(Invitation invitation)
			{
				this.mInvitation = invitation;
			}

			// Token: 0x06003198 RID: 12696 RVA: 0x001C5894 File Offset: 0x001C3C94
			public Invitation GetInvitation()
			{
				return this.mInvitation;
			}

			// Token: 0x06003199 RID: 12697 RVA: 0x001C589C File Offset: 0x001C3C9C
			internal NativeRealtimeMultiplayerClient.OnGameThreadForwardingListener OnGameThreadListener()
			{
				return this.mListener;
			}

			// Token: 0x0600319A RID: 12698 RVA: 0x001C58A4 File Offset: 0x001C3CA4
			internal void EnterState(NativeRealtimeMultiplayerClient.State handler)
			{
				this.EnterState(handler, true);
			}

			// Token: 0x0600319B RID: 12699 RVA: 0x001C58B0 File Offset: 0x001C3CB0
			internal void EnterState(NativeRealtimeMultiplayerClient.State handler, bool fireStateEnteredEvent)
			{
				object obj = this.mLifecycleLock;
				lock (obj)
				{
					this.mState = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.State>(handler);
					if (fireStateEnteredEvent)
					{
						Logger.d("Entering state: " + handler.GetType().Name);
						this.mState.OnStateEntered();
					}
				}
			}

			// Token: 0x0600319C RID: 12700 RVA: 0x001C5924 File Offset: 0x001C3D24
			internal void LeaveRoom()
			{
				if (!this.ShowingUI)
				{
					object obj = this.mLifecycleLock;
					lock (obj)
					{
						this.mState.LeaveRoom();
					}
				}
				else
				{
					Logger.d("Not leaving room since showing UI");
				}
			}

			// Token: 0x0600319D RID: 12701 RVA: 0x001C5984 File Offset: 0x001C3D84
			internal void ShowWaitingRoomUI()
			{
				this.mState.ShowWaitingRoomUI(this.MinPlayersToStart);
			}

			// Token: 0x0600319E RID: 12702 RVA: 0x001C599C File Offset: 0x001C3D9C
			internal void StartRoomCreation(string currentPlayerId, Action createRoom)
			{
				object obj = this.mLifecycleLock;
				lock (obj)
				{
					if (!this.mStillPreRoomCreation)
					{
						Logger.e("Room creation started more than once, this shouldn't happen!");
					}
					else if (!this.mState.IsActive())
					{
						Logger.w("Received an attempt to create a room after the session was already torn down!");
					}
					else
					{
						this.mCurrentPlayerId = Misc.CheckNotNull<string>(currentPlayerId);
						this.mStillPreRoomCreation = false;
						this.EnterState(new NativeRealtimeMultiplayerClient.RoomCreationPendingState(this));
						createRoom.Invoke();
					}
				}
			}

			// Token: 0x0600319F RID: 12703 RVA: 0x001C5A38 File Offset: 0x001C3E38
			internal void OnRoomStatusChanged(NativeRealTimeRoom room)
			{
				object obj = this.mLifecycleLock;
				lock (obj)
				{
					this.mState.OnRoomStatusChanged(room);
				}
			}

			// Token: 0x060031A0 RID: 12704 RVA: 0x001C5A7C File Offset: 0x001C3E7C
			internal void OnConnectedSetChanged(NativeRealTimeRoom room)
			{
				object obj = this.mLifecycleLock;
				lock (obj)
				{
					this.mState.OnConnectedSetChanged(room);
				}
			}

			// Token: 0x060031A1 RID: 12705 RVA: 0x001C5AC0 File Offset: 0x001C3EC0
			internal void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				object obj = this.mLifecycleLock;
				lock (obj)
				{
					this.mState.OnParticipantStatusChanged(room, participant);
				}
			}

			// Token: 0x060031A2 RID: 12706 RVA: 0x001C5B08 File Offset: 0x001C3F08
			internal void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
			{
				object obj = this.mLifecycleLock;
				lock (obj)
				{
					this.mState.HandleRoomResponse(response);
				}
			}

			// Token: 0x060031A3 RID: 12707 RVA: 0x001C5B4C File Offset: 0x001C3F4C
			internal void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
			{
				this.mState.OnDataReceived(room, sender, data, isReliable);
			}

			// Token: 0x060031A4 RID: 12708 RVA: 0x001C5B60 File Offset: 0x001C3F60
			internal void SendMessageToAll(bool reliable, byte[] data)
			{
				this.SendMessageToAll(reliable, data, 0, data.Length);
			}

			// Token: 0x060031A5 RID: 12709 RVA: 0x001C5B6E File Offset: 0x001C3F6E
			internal void SendMessageToAll(bool reliable, byte[] data, int offset, int length)
			{
				this.mState.SendToAll(data, offset, length, reliable);
			}

			// Token: 0x060031A6 RID: 12710 RVA: 0x001C5B82 File Offset: 0x001C3F82
			internal void SendMessage(bool reliable, string participantId, byte[] data)
			{
				this.SendMessage(reliable, participantId, data, 0, data.Length);
			}

			// Token: 0x060031A7 RID: 12711 RVA: 0x001C5B91 File Offset: 0x001C3F91
			internal void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length)
			{
				this.mState.SendToSpecificRecipient(participantId, data, offset, length, reliable);
			}

			// Token: 0x060031A8 RID: 12712 RVA: 0x001C5BA7 File Offset: 0x001C3FA7
			internal List<Participant> GetConnectedParticipants()
			{
				return this.mState.GetConnectedParticipants();
			}

			// Token: 0x060031A9 RID: 12713 RVA: 0x001C5BB6 File Offset: 0x001C3FB6
			internal virtual Participant GetSelf()
			{
				return this.mState.GetSelf();
			}

			// Token: 0x060031AA RID: 12714 RVA: 0x001C5BC5 File Offset: 0x001C3FC5
			internal virtual Participant GetParticipant(string participantId)
			{
				return this.mState.GetParticipant(participantId);
			}

			// Token: 0x060031AB RID: 12715 RVA: 0x001C5BD5 File Offset: 0x001C3FD5
			internal virtual bool IsRoomConnected()
			{
				return this.mState.IsRoomConnected();
			}

			// Token: 0x04003284 RID: 12932
			private readonly object mLifecycleLock = new object();

			// Token: 0x04003285 RID: 12933
			private readonly NativeRealtimeMultiplayerClient.OnGameThreadForwardingListener mListener;

			// Token: 0x04003286 RID: 12934
			private readonly RealtimeManager mManager;

			// Token: 0x04003287 RID: 12935
			private volatile string mCurrentPlayerId;

			// Token: 0x04003288 RID: 12936
			private volatile NativeRealtimeMultiplayerClient.State mState;

			// Token: 0x04003289 RID: 12937
			private volatile bool mStillPreRoomCreation;

			// Token: 0x0400328A RID: 12938
			private Invitation mInvitation;

			// Token: 0x0400328B RID: 12939
			private volatile bool mShowingUI;

			// Token: 0x0400328C RID: 12940
			private uint mMinPlayersToStart;
		}

		// Token: 0x020006C3 RID: 1731
		private class OnGameThreadForwardingListener
		{
			// Token: 0x060031AC RID: 12716 RVA: 0x001C5BE4 File Offset: 0x001C3FE4
			internal OnGameThreadForwardingListener(RealTimeMultiplayerListener listener)
			{
				this.mListener = Misc.CheckNotNull<RealTimeMultiplayerListener>(listener);
			}

			// Token: 0x060031AD RID: 12717 RVA: 0x001C5BF8 File Offset: 0x001C3FF8
			public void RoomSetupProgress(float percent)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnRoomSetupProgress(percent);
				});
			}

			// Token: 0x060031AE RID: 12718 RVA: 0x001C5C2C File Offset: 0x001C402C
			public void RoomConnected(bool success)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnRoomConnected(success);
				});
			}

			// Token: 0x060031AF RID: 12719 RVA: 0x001C5C5E File Offset: 0x001C405E
			public void LeftRoom()
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnLeftRoom();
				});
			}

			// Token: 0x060031B0 RID: 12720 RVA: 0x001C5C74 File Offset: 0x001C4074
			public void PeersConnected(string[] participantIds)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnPeersConnected(participantIds);
				});
			}

			// Token: 0x060031B1 RID: 12721 RVA: 0x001C5CA8 File Offset: 0x001C40A8
			public void PeersDisconnected(string[] participantIds)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnPeersDisconnected(participantIds);
				});
			}

			// Token: 0x060031B2 RID: 12722 RVA: 0x001C5CDC File Offset: 0x001C40DC
			public void RealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnRealTimeMessageReceived(isReliable, senderId, data);
				});
			}

			// Token: 0x060031B3 RID: 12723 RVA: 0x001C5D1C File Offset: 0x001C411C
			public void ParticipantLeft(Participant participant)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnParticipantLeft(participant);
				});
			}

			// Token: 0x0400328D RID: 12941
			private readonly RealTimeMultiplayerListener mListener;
		}

		// Token: 0x020006C4 RID: 1732
		internal abstract class State
		{
			// Token: 0x060031B6 RID: 12726 RVA: 0x001C5E2F File Offset: 0x001C422F
			internal virtual void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
			{
				Logger.d(base.GetType().Name + ".HandleRoomResponse: Defaulting to no-op.");
			}

			// Token: 0x060031B7 RID: 12727 RVA: 0x001C5E4B File Offset: 0x001C424B
			internal virtual bool IsActive()
			{
				Logger.d(base.GetType().Name + ".IsNonPreemptable: Is preemptable by default.");
				return true;
			}

			// Token: 0x060031B8 RID: 12728 RVA: 0x001C5E68 File Offset: 0x001C4268
			internal virtual void LeaveRoom()
			{
				Logger.d(base.GetType().Name + ".LeaveRoom: Defaulting to no-op.");
			}

			// Token: 0x060031B9 RID: 12729 RVA: 0x001C5E84 File Offset: 0x001C4284
			internal virtual void ShowWaitingRoomUI(uint minimumParticipantsBeforeStarting)
			{
				Logger.d(base.GetType().Name + ".ShowWaitingRoomUI: Defaulting to no-op.");
			}

			// Token: 0x060031BA RID: 12730 RVA: 0x001C5EA0 File Offset: 0x001C42A0
			internal virtual void OnStateEntered()
			{
				Logger.d(base.GetType().Name + ".OnStateEntered: Defaulting to no-op.");
			}

			// Token: 0x060031BB RID: 12731 RVA: 0x001C5EBC File Offset: 0x001C42BC
			internal virtual void OnRoomStatusChanged(NativeRealTimeRoom room)
			{
				Logger.d(base.GetType().Name + ".OnRoomStatusChanged: Defaulting to no-op.");
			}

			// Token: 0x060031BC RID: 12732 RVA: 0x001C5ED8 File Offset: 0x001C42D8
			internal virtual void OnConnectedSetChanged(NativeRealTimeRoom room)
			{
				Logger.d(base.GetType().Name + ".OnConnectedSetChanged: Defaulting to no-op.");
			}

			// Token: 0x060031BD RID: 12733 RVA: 0x001C5EF4 File Offset: 0x001C42F4
			internal virtual void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				Logger.d(base.GetType().Name + ".OnParticipantStatusChanged: Defaulting to no-op.");
			}

			// Token: 0x060031BE RID: 12734 RVA: 0x001C5F10 File Offset: 0x001C4310
			internal virtual void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
			{
				Logger.d(base.GetType().Name + ".OnDataReceived: Defaulting to no-op.");
			}

			// Token: 0x060031BF RID: 12735 RVA: 0x001C5F2C File Offset: 0x001C432C
			internal virtual void SendToSpecificRecipient(string recipientId, byte[] data, int offset, int length, bool isReliable)
			{
				Logger.d(base.GetType().Name + ".SendToSpecificRecipient: Defaulting to no-op.");
			}

			// Token: 0x060031C0 RID: 12736 RVA: 0x001C5F48 File Offset: 0x001C4348
			internal virtual void SendToAll(byte[] data, int offset, int length, bool isReliable)
			{
				Logger.d(base.GetType().Name + ".SendToApp: Defaulting to no-op.");
			}

			// Token: 0x060031C1 RID: 12737 RVA: 0x001C5F64 File Offset: 0x001C4364
			internal virtual List<Participant> GetConnectedParticipants()
			{
				Logger.d(base.GetType().Name + ".GetConnectedParticipants: Returning empty connected participants");
				return new List<Participant>();
			}

			// Token: 0x060031C2 RID: 12738 RVA: 0x001C5F85 File Offset: 0x001C4385
			internal virtual Participant GetSelf()
			{
				Logger.d(base.GetType().Name + ".GetSelf: Returning null self.");
				return null;
			}

			// Token: 0x060031C3 RID: 12739 RVA: 0x001C5FA2 File Offset: 0x001C43A2
			internal virtual Participant GetParticipant(string participantId)
			{
				Logger.d(base.GetType().Name + ".GetSelf: Returning null participant.");
				return null;
			}

			// Token: 0x060031C4 RID: 12740 RVA: 0x001C5FBF File Offset: 0x001C43BF
			internal virtual bool IsRoomConnected()
			{
				Logger.d(base.GetType().Name + ".IsRoomConnected: Returning room not connected.");
				return false;
			}
		}

		// Token: 0x020006C5 RID: 1733
		private abstract class MessagingEnabledState : NativeRealtimeMultiplayerClient.State
		{
			// Token: 0x060031C5 RID: 12741 RVA: 0x001C5FDC File Offset: 0x001C43DC
			internal MessagingEnabledState(NativeRealtimeMultiplayerClient.RoomSession session, NativeRealTimeRoom room)
			{
				this.mSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
				this.UpdateCurrentRoom(room);
			}

			// Token: 0x060031C6 RID: 12742 RVA: 0x001C5FF8 File Offset: 0x001C43F8
			internal void UpdateCurrentRoom(NativeRealTimeRoom room)
			{
				if (this.mRoom != null)
				{
					this.mRoom.Dispose();
				}
				this.mRoom = Misc.CheckNotNull<NativeRealTimeRoom>(room);
				this.mNativeParticipants = Enumerable.ToDictionary<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>(this.mRoom.Participants(), (GooglePlayGames.Native.PInvoke.MultiplayerParticipant p) => p.Id());
				this.mParticipants = Enumerable.ToDictionary<Participant, string>(Enumerable.Select<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>(this.mNativeParticipants.Values, (GooglePlayGames.Native.PInvoke.MultiplayerParticipant p) => p.AsParticipant()), (Participant p) => p.ParticipantId);
			}

			// Token: 0x060031C7 RID: 12743 RVA: 0x001C60AF File Offset: 0x001C44AF
			internal sealed override void OnRoomStatusChanged(NativeRealTimeRoom room)
			{
				this.HandleRoomStatusChanged(room);
				this.UpdateCurrentRoom(room);
			}

			// Token: 0x060031C8 RID: 12744 RVA: 0x001C60BF File Offset: 0x001C44BF
			internal virtual void HandleRoomStatusChanged(NativeRealTimeRoom room)
			{
			}

			// Token: 0x060031C9 RID: 12745 RVA: 0x001C60C1 File Offset: 0x001C44C1
			internal sealed override void OnConnectedSetChanged(NativeRealTimeRoom room)
			{
				this.HandleConnectedSetChanged(room);
				this.UpdateCurrentRoom(room);
			}

			// Token: 0x060031CA RID: 12746 RVA: 0x001C60D1 File Offset: 0x001C44D1
			internal virtual void HandleConnectedSetChanged(NativeRealTimeRoom room)
			{
			}

			// Token: 0x060031CB RID: 12747 RVA: 0x001C60D3 File Offset: 0x001C44D3
			internal sealed override void OnParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				this.HandleParticipantStatusChanged(room, participant);
				this.UpdateCurrentRoom(room);
			}

			// Token: 0x060031CC RID: 12748 RVA: 0x001C60E4 File Offset: 0x001C44E4
			internal virtual void HandleParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
			}

			// Token: 0x060031CD RID: 12749 RVA: 0x001C60E8 File Offset: 0x001C44E8
			internal sealed override List<Participant> GetConnectedParticipants()
			{
				List<Participant> list = Enumerable.ToList<Participant>(Enumerable.Where<Participant>(this.mParticipants.Values, (Participant p) => p.IsConnectedToRoom));
				list.Sort();
				return list;
			}

			// Token: 0x060031CE RID: 12750 RVA: 0x001C6130 File Offset: 0x001C4530
			internal override void SendToSpecificRecipient(string recipientId, byte[] data, int offset, int length, bool isReliable)
			{
				if (!this.mNativeParticipants.ContainsKey(recipientId))
				{
					Logger.e("Attempted to send message to unknown participant " + recipientId);
					return;
				}
				if (isReliable)
				{
					this.mSession.Manager().SendReliableMessage(this.mRoom, this.mNativeParticipants[recipientId], Misc.GetSubsetBytes(data, offset, length), null);
				}
				else
				{
					RealtimeManager realtimeManager = this.mSession.Manager();
					NativeRealTimeRoom nativeRealTimeRoom = this.mRoom;
					List<GooglePlayGames.Native.PInvoke.MultiplayerParticipant> list = new List<GooglePlayGames.Native.PInvoke.MultiplayerParticipant>();
					list.Add(this.mNativeParticipants[recipientId]);
					realtimeManager.SendUnreliableMessageToSpecificParticipants(nativeRealTimeRoom, list, Misc.GetSubsetBytes(data, offset, length));
				}
			}

			// Token: 0x060031CF RID: 12751 RVA: 0x001C61D0 File Offset: 0x001C45D0
			internal override void SendToAll(byte[] data, int offset, int length, bool isReliable)
			{
				byte[] subsetBytes = Misc.GetSubsetBytes(data, offset, length);
				if (isReliable)
				{
					foreach (string text in this.mNativeParticipants.Keys)
					{
						this.SendToSpecificRecipient(text, subsetBytes, 0, subsetBytes.Length, true);
					}
				}
				else
				{
					this.mSession.Manager().SendUnreliableMessageToAll(this.mRoom, subsetBytes);
				}
			}

			// Token: 0x060031D0 RID: 12752 RVA: 0x001C6264 File Offset: 0x001C4664
			internal override void OnDataReceived(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant sender, byte[] data, bool isReliable)
			{
				this.mSession.OnGameThreadListener().RealTimeMessageReceived(isReliable, sender.Id(), data);
			}

			// Token: 0x0400328E RID: 12942
			protected readonly NativeRealtimeMultiplayerClient.RoomSession mSession;

			// Token: 0x0400328F RID: 12943
			protected NativeRealTimeRoom mRoom;

			// Token: 0x04003290 RID: 12944
			protected Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant> mNativeParticipants;

			// Token: 0x04003291 RID: 12945
			protected Dictionary<string, Participant> mParticipants;
		}

		// Token: 0x020006C6 RID: 1734
		private class BeforeRoomCreateStartedState : NativeRealtimeMultiplayerClient.State
		{
			// Token: 0x060031D5 RID: 12757 RVA: 0x001C629F File Offset: 0x001C469F
			internal BeforeRoomCreateStartedState(NativeRealtimeMultiplayerClient.RoomSession session)
			{
				this.mContainingSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
			}

			// Token: 0x060031D6 RID: 12758 RVA: 0x001C62B3 File Offset: 0x001C46B3
			internal override void LeaveRoom()
			{
				Logger.d("Session was torn down before room was created.");
				this.mContainingSession.OnGameThreadListener().RoomConnected(false);
				this.mContainingSession.EnterState(new NativeRealtimeMultiplayerClient.ShutdownState(this.mContainingSession));
			}

			// Token: 0x04003296 RID: 12950
			private readonly NativeRealtimeMultiplayerClient.RoomSession mContainingSession;
		}

		// Token: 0x020006C7 RID: 1735
		private class RoomCreationPendingState : NativeRealtimeMultiplayerClient.State
		{
			// Token: 0x060031D7 RID: 12759 RVA: 0x001C62E6 File Offset: 0x001C46E6
			internal RoomCreationPendingState(NativeRealtimeMultiplayerClient.RoomSession session)
			{
				this.mContainingSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
			}

			// Token: 0x060031D8 RID: 12760 RVA: 0x001C62FC File Offset: 0x001C46FC
			internal override void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
			{
				if (!response.RequestSucceeded())
				{
					this.mContainingSession.EnterState(new NativeRealtimeMultiplayerClient.ShutdownState(this.mContainingSession));
					this.mContainingSession.OnGameThreadListener().RoomConnected(false);
					return;
				}
				this.mContainingSession.EnterState(new NativeRealtimeMultiplayerClient.ConnectingState(response.Room(), this.mContainingSession));
			}

			// Token: 0x060031D9 RID: 12761 RVA: 0x001C6358 File Offset: 0x001C4758
			internal override bool IsActive()
			{
				return true;
			}

			// Token: 0x060031DA RID: 12762 RVA: 0x001C635B File Offset: 0x001C475B
			internal override void LeaveRoom()
			{
				Logger.d("Received request to leave room during room creation, aborting creation.");
				this.mContainingSession.EnterState(new NativeRealtimeMultiplayerClient.AbortingRoomCreationState(this.mContainingSession));
			}

			// Token: 0x04003297 RID: 12951
			private readonly NativeRealtimeMultiplayerClient.RoomSession mContainingSession;
		}

		// Token: 0x020006C8 RID: 1736
		private class ConnectingState : NativeRealtimeMultiplayerClient.MessagingEnabledState
		{
			// Token: 0x060031DB RID: 12763 RVA: 0x001C637D File Offset: 0x001C477D
			internal ConnectingState(NativeRealTimeRoom room, NativeRealtimeMultiplayerClient.RoomSession session)
				: base(session, room)
			{
				this.mPercentPerParticipant = 80f / session.MinPlayersToStart;
			}

			// Token: 0x060031DC RID: 12764 RVA: 0x001C63B1 File Offset: 0x001C47B1
			internal override void OnStateEntered()
			{
				this.mSession.OnGameThreadListener().RoomSetupProgress(this.mPercentComplete);
			}

			// Token: 0x060031DD RID: 12765 RVA: 0x001C63CC File Offset: 0x001C47CC
			internal override void HandleConnectedSetChanged(NativeRealTimeRoom room)
			{
				HashSet<string> hashSet = new HashSet<string>();
				if ((room.Status() == Types.RealTimeRoomStatus.AUTO_MATCHING || room.Status() == Types.RealTimeRoomStatus.CONNECTING) && this.mSession.MinPlayersToStart <= room.ParticipantCount())
				{
					this.mSession.MinPlayersToStart = this.mSession.MinPlayersToStart + room.ParticipantCount();
					this.mPercentPerParticipant = 80f / this.mSession.MinPlayersToStart;
				}
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant in room.Participants())
				{
					using (multiplayerParticipant)
					{
						if (multiplayerParticipant.IsConnectedToRoom())
						{
							hashSet.Add(multiplayerParticipant.Id());
						}
					}
				}
				if (this.mConnectedParticipants.Equals(hashSet))
				{
					Logger.w("Received connected set callback with unchanged connected set!");
					return;
				}
				IEnumerable<string> enumerable = Enumerable.Except<string>(this.mConnectedParticipants, hashSet);
				if (room.Status() == Types.RealTimeRoomStatus.DELETED)
				{
					Logger.e("Participants disconnected during room setup, failing. Participants were: " + string.Join(",", Enumerable.ToArray<string>(enumerable)));
					this.mSession.OnGameThreadListener().RoomConnected(false);
					this.mSession.EnterState(new NativeRealtimeMultiplayerClient.ShutdownState(this.mSession));
					return;
				}
				IEnumerable<string> enumerable2 = Enumerable.Except<string>(hashSet, this.mConnectedParticipants);
				Logger.d("New participants connected: " + string.Join(",", Enumerable.ToArray<string>(enumerable2)));
				if (room.Status() == Types.RealTimeRoomStatus.ACTIVE)
				{
					Logger.d("Fully connected! Transitioning to active state.");
					this.mSession.EnterState(new NativeRealtimeMultiplayerClient.ActiveState(room, this.mSession));
					this.mSession.OnGameThreadListener().RoomConnected(true);
					return;
				}
				this.mPercentComplete += this.mPercentPerParticipant * (float)Enumerable.Count<string>(enumerable2);
				this.mConnectedParticipants = hashSet;
				this.mSession.OnGameThreadListener().RoomSetupProgress(this.mPercentComplete);
			}

			// Token: 0x060031DE RID: 12766 RVA: 0x001C65E4 File Offset: 0x001C49E4
			internal override void HandleParticipantStatusChanged(NativeRealTimeRoom room, GooglePlayGames.Native.PInvoke.MultiplayerParticipant participant)
			{
				if (!NativeRealtimeMultiplayerClient.ConnectingState.FailedStatuses.Contains(participant.Status()))
				{
					return;
				}
				this.mSession.OnGameThreadListener().ParticipantLeft(participant.AsParticipant());
				if (room.Status() != Types.RealTimeRoomStatus.CONNECTING && room.Status() != Types.RealTimeRoomStatus.AUTO_MATCHING)
				{
					this.LeaveRoom();
				}
			}

			// Token: 0x060031DF RID: 12767 RVA: 0x001C663B File Offset: 0x001C4A3B
			internal override void LeaveRoom()
			{
				this.mSession.EnterState(new NativeRealtimeMultiplayerClient.LeavingRoom(this.mSession, this.mRoom, delegate
				{
					this.mSession.OnGameThreadListener().RoomConnected(false);
				}));
			}

			// Token: 0x060031E0 RID: 12768 RVA: 0x001C6665 File Offset: 0x001C4A65
			internal override void ShowWaitingRoomUI(uint minimumParticipantsBeforeStarting)
			{
				this.mSession.ShowingUI = true;
				this.mSession.Manager().ShowWaitingRoomUI(this.mRoom, minimumParticipantsBeforeStarting, delegate(RealtimeManager.WaitingRoomUIResponse response)
				{
					this.mSession.ShowingUI = false;
					Logger.d("ShowWaitingRoomUI Response: " + response.ResponseStatus());
					if (response.ResponseStatus() == CommonErrorStatus.UIStatus.VALID)
					{
						Logger.d(string.Concat(new object[]
						{
							"Connecting state ShowWaitingRoomUI: room pcount:",
							response.Room().ParticipantCount(),
							" status: ",
							response.Room().Status()
						}));
						if (response.Room().Status() == Types.RealTimeRoomStatus.ACTIVE)
						{
							this.mSession.EnterState(new NativeRealtimeMultiplayerClient.ActiveState(response.Room(), this.mSession));
						}
					}
					else if (response.ResponseStatus() == CommonErrorStatus.UIStatus.ERROR_LEFT_ROOM)
					{
						this.LeaveRoom();
					}
					else
					{
						this.mSession.OnGameThreadListener().RoomSetupProgress(this.mPercentComplete);
					}
				});
			}

			// Token: 0x060031E1 RID: 12769 RVA: 0x001C6698 File Offset: 0x001C4A98
			// Note: this type is marked as 'beforefieldinit'.
			static ConnectingState()
			{
				HashSet<Types.ParticipantStatus> hashSet = new HashSet<Types.ParticipantStatus>();
				hashSet.Add(Types.ParticipantStatus.DECLINED);
				hashSet.Add(Types.ParticipantStatus.LEFT);
				NativeRealtimeMultiplayerClient.ConnectingState.FailedStatuses = hashSet;
			}

			// Token: 0x04003298 RID: 12952
			private const float InitialPercentComplete = 20f;

			// Token: 0x04003299 RID: 12953
			private static readonly HashSet<Types.ParticipantStatus> FailedStatuses;

			// Token: 0x0400329A RID: 12954
			private HashSet<string> mConnectedParticipants = new HashSet<string>();

			// Token: 0x0400329B RID: 12955
			private float mPercentComplete = 20f;

			// Token: 0x0400329C RID: 12956
			private float mPercentPerParticipant;
		}

		// Token: 0x020006C9 RID: 1737
		private class ActiveState : NativeRealtimeMultiplayerClient.MessagingEnabledState
		{
			// Token: 0x060031E4 RID: 12772 RVA: 0x001C67B9 File Offset: 0x001C4BB9
			internal ActiveState(NativeRealTimeRoom room, NativeRealtimeMultiplayerClient.RoomSession session)
				: base(session, room)
			{
			}

			// Token: 0x060031E5 RID: 12773 RVA: 0x001C67C3 File Offset: 0x001C4BC3
			internal override void OnStateEntered()
			{
				if (this.GetSelf() == null)
				{
					Logger.e("Room reached active state with unknown participant for the player");
					this.LeaveRoom();
				}
			}

			// Token: 0x060031E6 RID: 12774 RVA: 0x001C67E0 File Offset: 0x001C4BE0
			internal override bool IsRoomConnected()
			{
				return true;
			}

			// Token: 0x060031E7 RID: 12775 RVA: 0x001C67E3 File Offset: 0x001C4BE3
			internal override Participant GetParticipant(string participantId)
			{
				if (!this.mParticipants.ContainsKey(participantId))
				{
					Logger.e("Attempted to retrieve unknown participant " + participantId);
					return null;
				}
				return this.mParticipants[participantId];
			}

			// Token: 0x060031E8 RID: 12776 RVA: 0x001C6814 File Offset: 0x001C4C14
			internal override Participant GetSelf()
			{
				foreach (Participant participant in this.mParticipants.Values)
				{
					if (participant.Player != null && participant.Player.id.Equals(this.mSession.SelfPlayerId()))
					{
						return participant;
					}
				}
				return null;
			}

			// Token: 0x060031E9 RID: 12777 RVA: 0x001C68A4 File Offset: 0x001C4CA4
			internal override void HandleConnectedSetChanged(NativeRealTimeRoom room)
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				Dictionary<string, GooglePlayGames.Native.PInvoke.MultiplayerParticipant> dictionary = Enumerable.ToDictionary<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, string>(room.Participants(), (GooglePlayGames.Native.PInvoke.MultiplayerParticipant p) => p.Id());
				foreach (string text in this.mNativeParticipants.Keys)
				{
					GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant = dictionary[text];
					GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant2 = this.mNativeParticipants[text];
					if (!multiplayerParticipant.IsConnectedToRoom())
					{
						list2.Add(text);
					}
					if (!multiplayerParticipant2.IsConnectedToRoom() && multiplayerParticipant.IsConnectedToRoom())
					{
						list.Add(text);
					}
				}
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant3 in this.mNativeParticipants.Values)
				{
					multiplayerParticipant3.Dispose();
				}
				this.mNativeParticipants = dictionary;
				this.mParticipants = Enumerable.ToDictionary<Participant, string>(Enumerable.Select<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, Participant>(this.mNativeParticipants.Values, (GooglePlayGames.Native.PInvoke.MultiplayerParticipant p) => p.AsParticipant()), (Participant p) => p.ParticipantId);
				Logger.d("Updated participant statuses: " + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<Participant, string>(this.mParticipants.Values, (Participant p) => p.ToString()))));
				if (list2.Contains(this.GetSelf().ParticipantId))
				{
					Logger.w("Player was disconnected from the multiplayer session.");
				}
				string selfId = this.GetSelf().ParticipantId;
				list = Enumerable.ToList<string>(Enumerable.Where<string>(list, (string peerId) => !peerId.Equals(selfId)));
				list2 = Enumerable.ToList<string>(Enumerable.Where<string>(list2, (string peerId) => !peerId.Equals(selfId)));
				if (list.Count > 0)
				{
					list.Sort();
					this.mSession.OnGameThreadListener().PeersConnected(Enumerable.ToArray<string>(Enumerable.Where<string>(list, (string peer) => !peer.Equals(selfId))));
				}
				if (list2.Count > 0)
				{
					list2.Sort();
					this.mSession.OnGameThreadListener().PeersDisconnected(Enumerable.ToArray<string>(Enumerable.Where<string>(list2, (string peer) => !peer.Equals(selfId))));
				}
			}

			// Token: 0x060031EA RID: 12778 RVA: 0x001C6B54 File Offset: 0x001C4F54
			internal override void LeaveRoom()
			{
				this.mSession.EnterState(new NativeRealtimeMultiplayerClient.LeavingRoom(this.mSession, this.mRoom, delegate
				{
					this.mSession.OnGameThreadListener().LeftRoom();
				}));
			}
		}

		// Token: 0x020006CA RID: 1738
		private class ShutdownState : NativeRealtimeMultiplayerClient.State
		{
			// Token: 0x060031F0 RID: 12784 RVA: 0x001C6BFC File Offset: 0x001C4FFC
			internal ShutdownState(NativeRealtimeMultiplayerClient.RoomSession session)
			{
				this.mSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
			}

			// Token: 0x060031F1 RID: 12785 RVA: 0x001C6C10 File Offset: 0x001C5010
			internal override bool IsActive()
			{
				return false;
			}

			// Token: 0x060031F2 RID: 12786 RVA: 0x001C6C13 File Offset: 0x001C5013
			internal override void LeaveRoom()
			{
				this.mSession.OnGameThreadListener().LeftRoom();
			}

			// Token: 0x040032A1 RID: 12961
			private readonly NativeRealtimeMultiplayerClient.RoomSession mSession;
		}

		// Token: 0x020006CB RID: 1739
		private class LeavingRoom : NativeRealtimeMultiplayerClient.State
		{
			// Token: 0x060031F3 RID: 12787 RVA: 0x001C6C25 File Offset: 0x001C5025
			internal LeavingRoom(NativeRealtimeMultiplayerClient.RoomSession session, NativeRealTimeRoom room, Action leavingCompleteCallback)
			{
				this.mSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
				this.mRoomToLeave = Misc.CheckNotNull<NativeRealTimeRoom>(room);
				this.mLeavingCompleteCallback = Misc.CheckNotNull<Action>(leavingCompleteCallback);
			}

			// Token: 0x060031F4 RID: 12788 RVA: 0x001C6C51 File Offset: 0x001C5051
			internal override bool IsActive()
			{
				return false;
			}

			// Token: 0x060031F5 RID: 12789 RVA: 0x001C6C54 File Offset: 0x001C5054
			internal override void OnStateEntered()
			{
				this.mSession.Manager().LeaveRoom(this.mRoomToLeave, delegate(CommonErrorStatus.ResponseStatus status)
				{
					this.mLeavingCompleteCallback.Invoke();
					this.mSession.EnterState(new NativeRealtimeMultiplayerClient.ShutdownState(this.mSession));
				});
			}

			// Token: 0x040032A2 RID: 12962
			private readonly NativeRealtimeMultiplayerClient.RoomSession mSession;

			// Token: 0x040032A3 RID: 12963
			private readonly NativeRealTimeRoom mRoomToLeave;

			// Token: 0x040032A4 RID: 12964
			private readonly Action mLeavingCompleteCallback;
		}

		// Token: 0x020006CC RID: 1740
		private class AbortingRoomCreationState : NativeRealtimeMultiplayerClient.State
		{
			// Token: 0x060031F7 RID: 12791 RVA: 0x001C6C9B File Offset: 0x001C509B
			internal AbortingRoomCreationState(NativeRealtimeMultiplayerClient.RoomSession session)
			{
				this.mSession = Misc.CheckNotNull<NativeRealtimeMultiplayerClient.RoomSession>(session);
			}

			// Token: 0x060031F8 RID: 12792 RVA: 0x001C6CAF File Offset: 0x001C50AF
			internal override bool IsActive()
			{
				return false;
			}

			// Token: 0x060031F9 RID: 12793 RVA: 0x001C6CB4 File Offset: 0x001C50B4
			internal override void HandleRoomResponse(RealtimeManager.RealTimeRoomResponse response)
			{
				if (!response.RequestSucceeded())
				{
					this.mSession.EnterState(new NativeRealtimeMultiplayerClient.ShutdownState(this.mSession));
					this.mSession.OnGameThreadListener().RoomConnected(false);
					return;
				}
				this.mSession.EnterState(new NativeRealtimeMultiplayerClient.LeavingRoom(this.mSession, response.Room(), delegate
				{
					this.mSession.OnGameThreadListener().RoomConnected(false);
				}));
			}

			// Token: 0x040032A5 RID: 12965
			private readonly NativeRealtimeMultiplayerClient.RoomSession mSession;
		}
	}
}
