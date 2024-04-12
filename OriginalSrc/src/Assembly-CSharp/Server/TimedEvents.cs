using System;
using System.Collections.Generic;

namespace Server
{
	// Token: 0x02000451 RID: 1105
	public static class TimedEvents
	{
		// Token: 0x06001E9B RID: 7835 RVA: 0x00158C14 File Offset: 0x00157014
		public static void Initialize(Action _callback, Entity _e = null)
		{
			if (_e != null)
			{
				TimedEvents.m_isSpaceEntity = true;
				TimedEvents.m_loaderEntity = _e;
			}
			else
			{
				TimedEvents.m_isSpaceEntity = false;
				TimedEvents.m_loaderEntity = EntityManager.AddEntity();
			}
			if (_callback == null)
			{
				_callback = delegate
				{
					Debug.Log("No callback.", null);
				};
			}
			TimedEvents.m_callback = _callback;
			TimedEvents.Load(_callback);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00158C79 File Offset: 0x00157079
		public static void SubscribeToLoadFinished(Action _a)
		{
			TimedEvents.m_subscribers = (Action)Delegate.Combine(TimedEvents.m_subscribers, _a);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00158C90 File Offset: 0x00157090
		public static void UnsubscribeToLoadFinished(Action _a)
		{
			TimedEvents.m_subscribers = (Action)Delegate.Remove(TimedEvents.m_subscribers, _a);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00158CA8 File Offset: 0x001570A8
		private static void Load(Action _callback)
		{
			TimedEvents.m_loading = true;
			HttpC timedEvents = Metagame.GetTimedEvents(delegate(HttpC c)
			{
				TimedEvents.TimedEventLoadOk(c, _callback);
			}, delegate(HttpC c)
			{
				TimedEvents.TimedEventLoadFail(c, _callback);
			}, null);
			EntityManager.AddComponentToEntity(TimedEvents.m_loaderEntity, timedEvents);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00158CF4 File Offset: 0x001570F4
		private static void LoadFresh(Action _callback)
		{
			if (TimedEvents.m_loaderEntity != null)
			{
				HttpC fresh = MiniGame.GetFresh(delegate(PsMinigameMetaData meta)
				{
					TimedEvents.FreshLoadOk(meta, _callback);
				}, delegate(HttpC c)
				{
					TimedEvents.FreshLoadFail(c, _callback);
				}, "OffroadCar", string.Empty, null);
				EntityManager.AddComponentToEntity(TimedEvents.m_loaderEntity, fresh);
			}
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00158D4C File Offset: 0x0015714C
		private static void FreshLoadOk(PsMinigameMetaData _meta, Action _callback)
		{
			if (TimedEvents.m_loaderEntity != null && PsPlanetManager.m_timedEvents != null)
			{
				if (_meta != null && !PsPlanetManager.m_timedEvents.freshAndFree.Contains(_meta))
				{
					PsPlanetManager.m_timedEvents.freshAndFree.Add(_meta);
				}
				if (TimedEvents.m_versusCount <= 0)
				{
					_callback.Invoke();
				}
				if (!TimedEvents.m_isSpaceEntity)
				{
					EntityManager.RemoveEntity(TimedEvents.m_loaderEntity);
				}
				TimedEvents.m_loaderEntity = null;
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00158DC3 File Offset: 0x001571C3
		private static void FreshLoadFail(HttpC _c, Action _callback)
		{
			TimedEvents.LoadFresh(_callback);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00158DCB File Offset: 0x001571CB
		private static void TimedEventLoadOk(HttpC _c, Action _callback)
		{
			PsPlanetManager.m_timedEvents = ClientTools.ParseTimedEvents(_c);
			TimedEvents.m_loading = false;
			TimedEvents.EndDueOwnTurnChallenges();
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00158DE3 File Offset: 0x001571E3
		private static void TimedEventLoadFail(HttpC _c, Action _callback)
		{
			TimedEvents.Load(_callback);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00158DEC File Offset: 0x001571EC
		private static void EndDueOwnTurnChallenges()
		{
			List<VersusMetaData> list = PsPlanetManager.m_timedEvents.versusChallenges;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (string.IsNullOrEmpty(list[i].winner) && list[i].currentPlayer == PlayerPrefsX.GetUserId() && list[i].GetOwnInfo().tries >= PsState.m_versusChallengeTryAmount)
				{
					if (list[i].GetOpponentInfo() == null && list[i].GetOwnInfo().score.time > 0)
					{
						HttpC httpC = Versus.Quit(list[i].timedEventId, list[i].gameId, 0, true, false, new Action<HttpC>(TimedEvents.VersusEndSUCCEED), new Action<HttpC>(TimedEvents.VersusQuitFAILED), null);
						httpC.objectData = new TimedEvents.ChallengeEndInfo
						{
							data = list[i],
							winRound = true
						};
						TimedEvents.m_versusCount++;
					}
					else
					{
						HttpC httpC2 = Versus.Quit(list[i].timedEventId, list[i].gameId, 0, false, true, new Action<HttpC>(TimedEvents.VersusEndSUCCEED), new Action<HttpC>(TimedEvents.VersusQuitFAILED), null);
						httpC2.objectData = new TimedEvents.ChallengeEndInfo
						{
							data = list[i],
							winRound = false
						};
						TimedEvents.m_versusCount++;
					}
					Debug.LogError("VersusQuit send on id: " + list[i].timedEventId);
				}
			}
			list = PsPlanetManager.m_timedEvents.friendlyChallenges;
			for (int j = list.Count - 1; j >= 0; j--)
			{
				if (string.IsNullOrEmpty(list[j].winner) && list[j].currentPlayer == PlayerPrefsX.GetUserId() && list[j].GetOwnInfo() != null && list[j].GetOwnInfo().tries >= PsState.m_versusChallengeTryAmount)
				{
					if (list[j].GetOpponentInfo() == null && list[j].GetOwnInfo() != null && list[j].GetOwnInfo().score.time > 0)
					{
						HttpC httpC3 = Friendly.Quit(list[j].timedEventId, list[j].gameId, 0, true, false, new Action<HttpC>(TimedEvents.VersusEndSUCCEED), new Action<HttpC>(TimedEvents.VersusQuitFAILED), string.Empty, null);
						httpC3.objectData = new TimedEvents.ChallengeEndInfo
						{
							data = list[j],
							winRound = true
						};
						TimedEvents.m_versusCount++;
					}
					else
					{
						HttpC httpC4 = Friendly.Quit(list[j].timedEventId, list[j].gameId, 0, false, true, new Action<HttpC>(TimedEvents.VersusEndSUCCEED), new Action<HttpC>(TimedEvents.VersusQuitFAILED), string.Empty, null);
						httpC4.objectData = new TimedEvents.ChallengeEndInfo
						{
							data = list[j],
							winRound = false
						};
						TimedEvents.m_versusCount++;
					}
				}
			}
			if (TimedEvents.m_versusCount <= 0)
			{
				TimedEvents.m_callback.Invoke();
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x001591FC File Offset: 0x001575FC
		private static void VersusEndSUCCEED(HttpC _c)
		{
			Debug.LogWarning("VERSUS END SUCCEED");
			TimedEvents.ChallengeEndInfo challengeEndInfo = (TimedEvents.ChallengeEndInfo)_c.objectData;
			TimedEvents.m_versusCount--;
			if (TimedEvents.m_versusCount <= 0)
			{
				TimedEvents.m_callback.Invoke();
			}
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00159240 File Offset: 0x00157640
		private static void VersusQuitFAILED(HttpC _c)
		{
			Debug.LogWarning("VERSUS QUIT FAILED");
			TimedEvents.ChallengeEndInfo challengeEndInfo = (TimedEvents.ChallengeEndInfo)_c.objectData;
			HttpC httpC = Versus.Quit(challengeEndInfo.data.timedEventId, challengeEndInfo.data.gameId, 0, challengeEndInfo.winRound, !challengeEndInfo.winRound, new Action<HttpC>(TimedEvents.VersusEndSUCCEED), new Action<HttpC>(TimedEvents.VersusQuitFAILED), null);
			httpC.objectData = challengeEndInfo;
		}

		// Token: 0x040021CB RID: 8651
		private static bool m_isSpaceEntity;

		// Token: 0x040021CC RID: 8652
		private static Entity m_loaderEntity;

		// Token: 0x040021CD RID: 8653
		private static int m_versusCount;

		// Token: 0x040021CE RID: 8654
		private static Action m_callback;

		// Token: 0x040021CF RID: 8655
		public static bool m_loading = false;

		// Token: 0x040021D0 RID: 8656
		private static Action m_finishedLoading = null;

		// Token: 0x040021D1 RID: 8657
		public static Action m_subscribers = delegate
		{
			Debug.Log("TimedEvents loaded", null);
		};

		// Token: 0x02000452 RID: 1106
		private struct ChallengeEndInfo
		{
			// Token: 0x040021DD RID: 8669
			public VersusMetaData data;

			// Token: 0x040021DE RID: 8670
			public bool winRound;
		}
	}
}
