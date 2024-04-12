using System;
using System.Collections;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native
{
	// Token: 0x020006D0 RID: 1744
	public class NativeTurnBasedMultiplayerClient : ITurnBasedMultiplayerClient
	{
		// Token: 0x0600321A RID: 12826 RVA: 0x001C841B File Offset: 0x001C681B
		internal NativeTurnBasedMultiplayerClient(NativeClient nativeClient, TurnBasedManager manager)
		{
			this.mTurnBasedManager = manager;
			this.mNativeClient = nativeClient;
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x001C8431 File Offset: 0x001C6831
		public void CreateQuickMatch(uint minOpponents, uint maxOpponents, uint variant, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			this.CreateQuickMatch(minOpponents, maxOpponents, variant, 0UL, callback);
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x001C8440 File Offset: 0x001C6840
		public void CreateQuickMatch(uint minOpponents, uint maxOpponents, uint variant, ulong exclusiveBitmask, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder turnBasedMatchConfigBuilder = GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder.Create())
			{
				turnBasedMatchConfigBuilder.SetVariant(variant).SetMinimumAutomatchingPlayers(minOpponents).SetMaximumAutomatchingPlayers(maxOpponents)
					.SetExclusiveBitMask(exclusiveBitmask);
				using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfig turnBasedMatchConfig = turnBasedMatchConfigBuilder.Build())
				{
					this.mTurnBasedManager.CreateMatch(turnBasedMatchConfig, this.BridgeMatchToUserCallback(delegate(UIStatus status, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match)
					{
						callback.Invoke(status == UIStatus.Valid, match);
					}));
				}
			}
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x001C84F4 File Offset: 0x001C68F4
		public void CreateWithInvitationScreen(uint minOpponents, uint maxOpponents, uint variant, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			this.CreateWithInvitationScreen(minOpponents, maxOpponents, variant, delegate(UIStatus status, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match)
			{
				callback.Invoke(status == UIStatus.Valid, match);
			});
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x001C8524 File Offset: 0x001C6924
		public void CreateWithInvitationScreen(uint minOpponents, uint maxOpponents, uint variant, Action<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			this.mTurnBasedManager.ShowPlayerSelectUI(minOpponents, maxOpponents, true, delegate(PlayerSelectUIResponse result)
			{
				if (result.Status() != CommonErrorStatus.UIStatus.VALID)
				{
					callback.Invoke((UIStatus)result.Status(), null);
					return;
				}
				using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder turnBasedMatchConfigBuilder = GooglePlayGames.Native.PInvoke.TurnBasedMatchConfigBuilder.Create())
				{
					turnBasedMatchConfigBuilder.PopulateFromUIResponse(result).SetVariant(variant);
					using (GooglePlayGames.Native.PInvoke.TurnBasedMatchConfig turnBasedMatchConfig = turnBasedMatchConfigBuilder.Build())
					{
						this.mTurnBasedManager.CreateMatch(turnBasedMatchConfig, this.BridgeMatchToUserCallback(callback));
					}
				}
			});
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x001C8578 File Offset: 0x001C6978
		public void GetAllInvitations(Action<Invitation[]> callback)
		{
			this.mTurnBasedManager.GetAllTurnbasedMatches(delegate(TurnBasedManager.TurnBasedMatchesResponse allMatches)
			{
				Invitation[] array = new Invitation[allMatches.InvitationCount()];
				int num = 0;
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation multiplayerInvitation in allMatches.Invitations())
				{
					array[num++] = multiplayerInvitation.AsInvitation();
				}
				callback.Invoke(array);
			});
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x001C85AC File Offset: 0x001C69AC
		public void GetAllMatches(Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[]> callback)
		{
			this.mTurnBasedManager.GetAllTurnbasedMatches(delegate(TurnBasedManager.TurnBasedMatchesResponse allMatches)
			{
				int num = allMatches.MyTurnMatchesCount() + allMatches.TheirTurnMatchesCount() + allMatches.CompletedMatchesCount();
				GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[] array = new GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch[num];
				int num2 = 0;
				foreach (NativeTurnBasedMatch nativeTurnBasedMatch in allMatches.MyTurnMatches())
				{
					array[num2++] = nativeTurnBasedMatch.AsTurnBasedMatch(this.mNativeClient.GetUserId());
				}
				foreach (NativeTurnBasedMatch nativeTurnBasedMatch2 in allMatches.TheirTurnMatches())
				{
					array[num2++] = nativeTurnBasedMatch2.AsTurnBasedMatch(this.mNativeClient.GetUserId());
				}
				foreach (NativeTurnBasedMatch nativeTurnBasedMatch3 in allMatches.CompletedMatches())
				{
					array[num2++] = nativeTurnBasedMatch3.AsTurnBasedMatch(this.mNativeClient.GetUserId());
				}
				callback.Invoke(array);
			});
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x001C85E4 File Offset: 0x001C69E4
		private Action<TurnBasedManager.TurnBasedMatchResponse> BridgeMatchToUserCallback(Action<UIStatus, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> userCallback)
		{
			return delegate(TurnBasedManager.TurnBasedMatchResponse callbackResult)
			{
				using (NativeTurnBasedMatch nativeTurnBasedMatch = callbackResult.Match())
				{
					if (nativeTurnBasedMatch == null)
					{
						UIStatus uistatus = UIStatus.InternalError;
						CommonErrorStatus.MultiplayerStatus multiplayerStatus = callbackResult.ResponseStatus();
						switch (multiplayerStatus + 5)
						{
						case (CommonErrorStatus.MultiplayerStatus)0:
							uistatus = UIStatus.Timeout;
							break;
						case CommonErrorStatus.MultiplayerStatus.VALID:
							uistatus = UIStatus.VersionUpdateRequired;
							break;
						case CommonErrorStatus.MultiplayerStatus.VALID_BUT_STALE:
							uistatus = UIStatus.NotAuthorized;
							break;
						case (CommonErrorStatus.MultiplayerStatus)3:
							uistatus = UIStatus.InternalError;
							break;
						case (CommonErrorStatus.MultiplayerStatus)6:
							uistatus = UIStatus.Valid;
							break;
						case (CommonErrorStatus.MultiplayerStatus)7:
							uistatus = UIStatus.Valid;
							break;
						}
						userCallback.Invoke(uistatus, null);
					}
					else
					{
						GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch turnBasedMatch = nativeTurnBasedMatch.AsTurnBasedMatch(this.mNativeClient.GetUserId());
						Logger.d("Passing converted match to user callback:" + turnBasedMatch);
						userCallback.Invoke(UIStatus.Valid, turnBasedMatch);
					}
				}
			};
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x001C8614 File Offset: 0x001C6A14
		public void AcceptFromInbox(Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			this.mTurnBasedManager.ShowInboxUI(delegate(TurnBasedManager.MatchInboxUIResponse callbackResult)
			{
				using (NativeTurnBasedMatch nativeTurnBasedMatch = callbackResult.Match())
				{
					if (nativeTurnBasedMatch == null)
					{
						callback.Invoke(false, null);
					}
					else
					{
						GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch turnBasedMatch = nativeTurnBasedMatch.AsTurnBasedMatch(this.mNativeClient.GetUserId());
						Logger.d("Passing converted match to user callback:" + turnBasedMatch);
						callback.Invoke(true, turnBasedMatch);
					}
				}
			});
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x001C8660 File Offset: 0x001C6A60
		public void AcceptInvitation(string invitationId, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			this.FindInvitationWithId(invitationId, delegate(GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation)
			{
				if (invitation == null)
				{
					Logger.e("Could not find invitation with id " + invitationId);
					callback.Invoke(false, null);
					return;
				}
				this.mTurnBasedManager.AcceptInvitation(invitation, this.BridgeMatchToUserCallback(delegate(UIStatus status, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match)
				{
					callback.Invoke(status == UIStatus.Valid, match);
				}));
			});
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x001C86B4 File Offset: 0x001C6AB4
		private void FindInvitationWithId(string invitationId, Action<GooglePlayGames.Native.PInvoke.MultiplayerInvitation> callback)
		{
			this.mTurnBasedManager.GetAllTurnbasedMatches(delegate(TurnBasedManager.TurnBasedMatchesResponse allMatches)
			{
				if (allMatches.Status() <= (CommonErrorStatus.MultiplayerStatus)0)
				{
					callback.Invoke(null);
					return;
				}
				foreach (GooglePlayGames.Native.PInvoke.MultiplayerInvitation multiplayerInvitation in allMatches.Invitations())
				{
					using (multiplayerInvitation)
					{
						if (multiplayerInvitation.Id().Equals(invitationId))
						{
							callback.Invoke(multiplayerInvitation);
							return;
						}
					}
				}
				callback.Invoke(null);
			});
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x001C86EC File Offset: 0x001C6AEC
		public void RegisterMatchDelegate(MatchDelegate del)
		{
			if (del == null)
			{
				this.mMatchDelegate = null;
			}
			else
			{
				this.mMatchDelegate = Callbacks.AsOnGameThreadCallback<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool>(delegate(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, bool autoLaunch)
				{
					del(match, autoLaunch);
				});
			}
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x001C8738 File Offset: 0x001C6B38
		internal void HandleMatchEvent(Types.MultiplayerEvent eventType, string matchId, NativeTurnBasedMatch match)
		{
			Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool> currentDelegate = this.mMatchDelegate;
			if (currentDelegate == null)
			{
				return;
			}
			if (eventType == Types.MultiplayerEvent.REMOVED)
			{
				Logger.d("Ignoring REMOVE event for match " + matchId);
				return;
			}
			bool shouldAutolaunch = eventType == Types.MultiplayerEvent.UPDATED_FROM_APP_LAUNCH;
			match.ReferToMe();
			Callbacks.AsCoroutine(this.WaitForLogin(delegate
			{
				currentDelegate.Invoke(match.AsTurnBasedMatch(this.mNativeClient.GetUserId()), shouldAutolaunch);
				match.ForgetMe();
			}));
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x001C87B8 File Offset: 0x001C6BB8
		private IEnumerator WaitForLogin(Action method)
		{
			if (string.IsNullOrEmpty(this.mNativeClient.GetUserId()))
			{
				yield return null;
			}
			method.Invoke();
			yield break;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x001C87DC File Offset: 0x001C6BDC
		public void TakeTurn(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, byte[] data, string pendingParticipantId, Action<bool> callback)
		{
			Logger.describe(data);
			callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
			this.FindEqualVersionMatchWithParticipant(match, pendingParticipantId, callback, delegate(GooglePlayGames.Native.PInvoke.MultiplayerParticipant pendingParticipant, NativeTurnBasedMatch foundMatch)
			{
				this.mTurnBasedManager.TakeTurn(foundMatch, data, pendingParticipant, delegate(TurnBasedManager.TurnBasedMatchResponse result)
				{
					if (result.RequestSucceeded())
					{
						callback.Invoke(true);
					}
					else
					{
						Logger.d("Taking turn failed: " + result.ResponseStatus());
						callback.Invoke(false);
					}
				});
			});
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x001C883C File Offset: 0x001C6C3C
		private void FindEqualVersionMatch(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> onFailure, Action<NativeTurnBasedMatch> onVersionMatch)
		{
			this.mTurnBasedManager.GetMatch(match.MatchId, delegate(TurnBasedManager.TurnBasedMatchResponse response)
			{
				using (NativeTurnBasedMatch nativeTurnBasedMatch = response.Match())
				{
					if (nativeTurnBasedMatch == null)
					{
						Logger.e(string.Format("Could not find match {0}", match.MatchId));
						onFailure.Invoke(false);
					}
					else if (nativeTurnBasedMatch.Version() != match.Version)
					{
						Logger.e(string.Format("Attempted to update a stale version of the match. Expected version was {0} but current version is {1}.", match.Version, nativeTurnBasedMatch.Version()));
						onFailure.Invoke(false);
					}
					else
					{
						onVersionMatch.Invoke(nativeTurnBasedMatch);
					}
				}
			});
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x001C8888 File Offset: 0x001C6C88
		private void FindEqualVersionMatchWithParticipant(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, string participantId, Action<bool> onFailure, Action<GooglePlayGames.Native.PInvoke.MultiplayerParticipant, NativeTurnBasedMatch> onFoundParticipantAndMatch)
		{
			this.FindEqualVersionMatch(match, onFailure, delegate(NativeTurnBasedMatch foundMatch)
			{
				if (participantId == null)
				{
					using (GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant = GooglePlayGames.Native.PInvoke.MultiplayerParticipant.AutomatchingSentinel())
					{
						onFoundParticipantAndMatch.Invoke(multiplayerParticipant, foundMatch);
						return;
					}
				}
				using (GooglePlayGames.Native.PInvoke.MultiplayerParticipant multiplayerParticipant2 = foundMatch.ParticipantWithId(participantId))
				{
					if (multiplayerParticipant2 == null)
					{
						Logger.e(string.Format("Located match {0} but desired participant with ID {1} could not be found", match.MatchId, participantId));
						onFailure.Invoke(false);
					}
					else
					{
						onFoundParticipantAndMatch.Invoke(multiplayerParticipant2, foundMatch);
					}
				}
			});
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x001C88D6 File Offset: 0x001C6CD6
		public int GetMaxMatchDataSize()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x001C88E0 File Offset: 0x001C6CE0
		public void Finish(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, byte[] data, MatchOutcome outcome, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
			this.FindEqualVersionMatch(match, callback, delegate(NativeTurnBasedMatch foundMatch)
			{
				GooglePlayGames.Native.PInvoke.ParticipantResults participantResults = foundMatch.Results();
				foreach (string text in outcome.ParticipantIds)
				{
					Types.MatchResult matchResult = NativeTurnBasedMultiplayerClient.ResultToMatchResult(outcome.GetResultFor(text));
					uint placementFor = outcome.GetPlacementFor(text);
					if (participantResults.HasResultsForParticipant(text))
					{
						Types.MatchResult matchResult2 = participantResults.ResultsForParticipant(text);
						uint num = participantResults.PlacingForParticipant(text);
						if (matchResult != matchResult2 || placementFor != num)
						{
							Logger.e(string.Format("Attempted to override existing results for participant {0}: Placing {1}, Result {2}", text, num, matchResult2));
							callback.Invoke(false);
							return;
						}
					}
					else
					{
						GooglePlayGames.Native.PInvoke.ParticipantResults participantResults2 = participantResults;
						participantResults = participantResults2.WithResult(text, placementFor, matchResult);
						participantResults2.Dispose();
					}
				}
				this.mTurnBasedManager.FinishMatchDuringMyTurn(foundMatch, data, participantResults, delegate(TurnBasedManager.TurnBasedMatchResponse response)
				{
					callback.Invoke(response.RequestSucceeded());
				});
			});
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x001C893A File Offset: 0x001C6D3A
		private static Types.MatchResult ResultToMatchResult(MatchOutcome.ParticipantResult result)
		{
			switch (result)
			{
			case MatchOutcome.ParticipantResult.None:
				return Types.MatchResult.NONE;
			case MatchOutcome.ParticipantResult.Win:
				return Types.MatchResult.WIN;
			case MatchOutcome.ParticipantResult.Loss:
				return Types.MatchResult.LOSS;
			case MatchOutcome.ParticipantResult.Tie:
				return Types.MatchResult.TIE;
			default:
				Logger.e("Received unknown ParticipantResult " + result);
				return Types.MatchResult.NONE;
			}
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x001C8978 File Offset: 0x001C6D78
		public void AcknowledgeFinished(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
			this.FindEqualVersionMatch(match, callback, delegate(NativeTurnBasedMatch foundMatch)
			{
				this.mTurnBasedManager.ConfirmPendingCompletion(foundMatch, delegate(TurnBasedManager.TurnBasedMatchResponse response)
				{
					callback.Invoke(response.RequestSucceeded());
				});
			});
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x001C89C4 File Offset: 0x001C6DC4
		public void Leave(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
			this.FindEqualVersionMatch(match, callback, delegate(NativeTurnBasedMatch foundMatch)
			{
				this.mTurnBasedManager.LeaveMatchDuringTheirTurn(foundMatch, delegate(CommonErrorStatus.MultiplayerStatus status)
				{
					callback.Invoke(status > (CommonErrorStatus.MultiplayerStatus)0);
				});
			});
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x001C8A10 File Offset: 0x001C6E10
		public void LeaveDuringTurn(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, string pendingParticipantId, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
			this.FindEqualVersionMatchWithParticipant(match, pendingParticipantId, callback, delegate(GooglePlayGames.Native.PInvoke.MultiplayerParticipant pendingParticipant, NativeTurnBasedMatch foundMatch)
			{
				this.mTurnBasedManager.LeaveDuringMyTurn(foundMatch, pendingParticipant, delegate(CommonErrorStatus.MultiplayerStatus status)
				{
					callback.Invoke(status > (CommonErrorStatus.MultiplayerStatus)0);
				});
			});
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x001C8A5C File Offset: 0x001C6E5C
		public void Cancel(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool>(callback);
			this.FindEqualVersionMatch(match, callback, delegate(NativeTurnBasedMatch foundMatch)
			{
				this.mTurnBasedManager.CancelMatch(foundMatch, delegate(CommonErrorStatus.MultiplayerStatus status)
				{
					callback.Invoke(status > (CommonErrorStatus.MultiplayerStatus)0);
				});
			});
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x001C8AA8 File Offset: 0x001C6EA8
		public void Rematch(GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch match, Action<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch> callback)
		{
			callback = Callbacks.AsOnGameThreadCallback<bool, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch>(callback);
			this.FindEqualVersionMatch(match, delegate(bool failed)
			{
				callback.Invoke(false, null);
			}, delegate(NativeTurnBasedMatch foundMatch)
			{
				this.mTurnBasedManager.Rematch(foundMatch, this.BridgeMatchToUserCallback(delegate(UIStatus status, GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch m)
				{
					callback.Invoke(status == UIStatus.Valid, m);
				}));
			});
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x001C8AF9 File Offset: 0x001C6EF9
		public void DeclineInvitation(string invitationId)
		{
			this.FindInvitationWithId(invitationId, delegate(GooglePlayGames.Native.PInvoke.MultiplayerInvitation invitation)
			{
				if (invitation == null)
				{
					return;
				}
				this.mTurnBasedManager.DeclineInvitation(invitation);
			});
		}

		// Token: 0x040032B7 RID: 12983
		private readonly TurnBasedManager mTurnBasedManager;

		// Token: 0x040032B8 RID: 12984
		private readonly NativeClient mNativeClient;

		// Token: 0x040032B9 RID: 12985
		private volatile Action<GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch, bool> mMatchDelegate;
	}
}
