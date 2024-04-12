using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class PsUITournamentLeaderboard : UICanvas
{
	// Token: 0x060012DF RID: 4831 RVA: 0x000BA308 File Offset: 0x000B8708
	public PsUITournamentLeaderboard(UIComponent _parent)
		: base(_parent, false, "LeaderboardBase", null, string.Empty)
	{
		TournamentInfo tournament = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament;
		this.m_currentRoom = (this.m_loadedRoom = tournament.room);
		this.m_leaderboardAnimationList = new Queue<LeaderboardAnimation>();
		this.SetHorizontalAlign(1f);
		this.GetLeaderboard();
		if (PsUITournamentLeaderboard.m_oldLeaderboard != null)
		{
			this.CreateLeaderboard(PsUITournamentLeaderboard.m_oldLeaderboard);
			this.m_hasLeaderboard = true;
		}
		else
		{
			this.CreateLeaderboard(null);
		}
		if ((PsState.m_activeGameLoop.m_gameMode as PsGameModeTournament).m_waitForNextGhost)
		{
			(PsState.m_activeGameLoop.m_gameMode as PsGameModeTournament).AddGhostLoadedCallback(new Action(this.GhostLoaderCallback));
		}
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x000BA455 File Offset: 0x000B8855
	public void SetCameraToTop()
	{
		CameraS.BringToFront(this.m_scrollCanvas.m_camera, true);
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x000BA468 File Offset: 0x000B8868
	public void SetCameraToBottom(Camera _camera)
	{
		CameraS.MoveToBehindOther(this.m_scrollCanvas.m_camera, _camera);
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x000BA47C File Offset: 0x000B887C
	public static int GetOwnPosition()
	{
		int num = 0;
		List<HighscoreDataEntry> newestLeaderboard = PsUITournamentLeaderboard.GetNewestLeaderboard();
		if (newestLeaderboard != null)
		{
			num = PsUITournamentLeaderboard.GetOwnPositionFromLeaderboard(newestLeaderboard);
		}
		return num;
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x000BA4A0 File Offset: 0x000B88A0
	public static int GetPlayerCount()
	{
		List<HighscoreDataEntry> newestLeaderboard = PsUITournamentLeaderboard.GetNewestLeaderboard();
		if (newestLeaderboard != null)
		{
			return newestLeaderboard.Count;
		}
		return 0;
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x000BA4C1 File Offset: 0x000B88C1
	public static List<HighscoreDataEntry> GetNewestLeaderboard()
	{
		return (PsUITournamentLeaderboard.m_newLeaderboard == null) ? PsUITournamentLeaderboard.m_oldLeaderboard : PsUITournamentLeaderboard.m_newLeaderboard;
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x000BA4DC File Offset: 0x000B88DC
	private static int GetOwnPositionFromLeaderboard(List<HighscoreDataEntry> _leaderboard)
	{
		int num = 0;
		for (int i = 0; i < _leaderboard.Count; i++)
		{
			if (_leaderboard[i].playerId == PlayerPrefsX.GetUserId())
			{
				return i + 1 + num;
			}
			if (num == 0 && _leaderboard[i].playerId == PsUITournamentLeaderboard.m_hostId)
			{
				num = -1;
			}
		}
		return 0;
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000BA547 File Offset: 0x000B8947
	public static int GetNextTimeScore(int _timeScore)
	{
		return PsUITournamentLeaderboard.GetNextTimeScore(_timeScore, false);
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x000BA550 File Offset: 0x000B8950
	public static int GetNextTimeScore(int _timeScore, bool _behind)
	{
		List<HighscoreDataEntry> list = ((PsUITournamentLeaderboard.m_newLeaderboard == null) ? PsUITournamentLeaderboard.m_oldLeaderboard : PsUITournamentLeaderboard.m_newLeaderboard);
		if (list != null)
		{
			int num = 0;
			if (_behind)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (list[i].time <= _timeScore)
					{
						return num;
					}
					num = list[i].time;
				}
			}
			else
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].time >= _timeScore)
					{
						return num;
					}
					num = list[j].time;
				}
			}
		}
		return 0;
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x000BA608 File Offset: 0x000B8A08
	public static int GetBestTimeScore()
	{
		List<HighscoreDataEntry> list = ((PsUITournamentLeaderboard.m_newLeaderboard == null) ? PsUITournamentLeaderboard.m_oldLeaderboard : PsUITournamentLeaderboard.m_newLeaderboard);
		if (list != null && list.Count > 0)
		{
			return list[0].time;
		}
		return 0;
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x000BA650 File Offset: 0x000B8A50
	public override void Destroy()
	{
		(PsState.m_activeGameLoop.m_gameMode as PsGameModeTournament).RemoveGhostLoadedCallback(new Action(this.GhostLoaderCallback));
		if (PsUITournamentLeaderboard.m_newLeaderboard != null)
		{
			PsUITournamentLeaderboard.m_oldLeaderboard = PsUITournamentLeaderboard.m_newLeaderboard;
			PsUITournamentLeaderboard.m_newLeaderboard = null;
		}
		base.Destroy();
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x000BA69D File Offset: 0x000B8A9D
	private void GhostLoaderCallback()
	{
		this.RemoveGhostTriangles();
		this.AddTriangles(true);
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x000BA6AC File Offset: 0x000B8AAC
	private void AddTriangles(bool _update = false)
	{
		PsGameModeTournament psGameModeTournament = PsState.m_activeGameLoop.m_gameMode as PsGameModeTournament;
		List<GhostObject> list = ((psGameModeTournament.m_freshGhosts == null) ? psGameModeTournament.m_playbackGhosts : psGameModeTournament.m_freshGhosts);
		for (int i = 0; i < list.Count; i++)
		{
			if (this.m_entryDictionary.ContainsKey(list[i].m_playerId))
			{
				this.m_entryDictionary[list[i].m_playerId].CreateTriangle(i, _update);
			}
		}
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x000BA738 File Offset: 0x000B8B38
	public void CreatePlayerAnimationData()
	{
		List<HighscoreDataEntry> newestLeaderboard = PsUITournamentLeaderboard.GetNewestLeaderboard();
		int num = Mathf.Min(PsState.m_activeGameLoop.m_timeScoreBest, PsState.m_activeGameLoop.m_timeScoreCurrent);
		if (num == 0)
		{
			num = int.MaxValue;
		}
		string userId = PlayerPrefsX.GetUserId();
		bool flag = false;
		if (num != 2147483647 && userId == PsUITournamentLeaderboard.m_hostId)
		{
			PsUITournamentLeaderboard.m_hostTime = num;
		}
		for (int i = 0; i < newestLeaderboard.Count; i++)
		{
			if (newestLeaderboard[i].playerId == userId)
			{
				if (newestLeaderboard[i].time > num)
				{
					this.m_leaderboardAnimationList.Enqueue(new LeaderboardAnimation(userId, num, null));
				}
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			Debug.LogError("DID NOT HAVE PLAYER!!!");
			HighscoreDataEntry playerHighscoreDataentry = this.GetPlayerHighscoreDataentry(int.MaxValue);
			PsUITournamentLeaderboard.m_oldLeaderboard.Add(playerHighscoreDataentry);
			this.m_leaderboardAnimationList.Enqueue(new LeaderboardAnimation(userId, num, null));
		}
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x000BA838 File Offset: 0x000B8C38
	private Tournament.TournamentLeaderboard GetFakeHighscoreList()
	{
		Tournament.TournamentLeaderboard tournamentLeaderboard = new Tournament.TournamentLeaderboard();
		tournamentLeaderboard.m_leaderboard = PsUITournamentLeaderboard.m_newLeaderboard;
		tournamentLeaderboard.m_leaderboard.Reverse();
		return tournamentLeaderboard;
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x000BA864 File Offset: 0x000B8C64
	public static void SetTournamentData(Tournament.TournamentLeaderboard _data)
	{
		_data.timeStamp = Main.m_EPOCHSeconds;
		PsUITournamentLeaderboard.m_leaderboardDatas[_data.room] = _data;
		if (PsState.m_activeGameLoop != null)
		{
			(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.globalParticipants = _data.globalParticipants;
			(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.roomParticipants = _data.m_leaderboard.Count;
			(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.acceptingNewScores = _data.acceptingNewScores;
			(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.globalNitroPot = _data.globalNitroPot;
			(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.roomNitroPot = _data.roomNitroPot;
			(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.roomCount = _data.roomCount;
			(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.ownerTime = _data.ownerTime;
			PsUITournamentLeaderboard.m_hostTime = _data.ownerTime;
			PsUITournamentLeaderboard.m_hostPosition = PsUITournamentLeaderboard.GetPlayerIndex(PsUITournamentLeaderboard.m_hostTime, _data.m_leaderboard);
			HighscoreDataEntry hostHighscoreDataEntry = PsUITournamentLeaderboard.GetHostHighscoreDataEntry(PsUITournamentLeaderboard.m_hostTime);
			_data.m_leaderboard.Insert(PsUITournamentLeaderboard.m_hostPosition, hostHighscoreDataEntry);
			if (PlayerPrefsX.GetUserId() == PsUITournamentLeaderboard.m_hostId)
			{
				PsState.m_activeGameLoop.m_timeScoreBest = PsUITournamentLeaderboard.m_hostTime;
			}
		}
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x000BA9D4 File Offset: 0x000B8DD4
	public static HttpC GetLeaderboardData(Action _callback)
	{
		return Tournament.GetLeaderboard(delegate(Tournament.TournamentLeaderboard c)
		{
			PsUITournamentLeaderboard.m_leaderboardDatas[c.room] = c;
			PsUITournamentLeaderboard.SetTournamentData(c);
			if (c.m_leaderboard.Find((HighscoreDataEntry _c) => _c.playerId == PlayerPrefsX.GetUserId()) == null)
			{
				HighscoreDataEntry highscoreDataEntry = new HighscoreDataEntry();
				highscoreDataEntry.country = PlayerPrefsX.GetCountryCode();
				highscoreDataEntry.name = PlayerPrefsX.GetUserName();
				highscoreDataEntry.playerId = PlayerPrefsX.GetUserId();
				highscoreDataEntry.time = int.MaxValue;
				highscoreDataEntry.facebookId = PlayerPrefsX.GetFacebookId();
				c.m_leaderboard.Add(highscoreDataEntry);
			}
			PsUITournamentLeaderboard.m_oldLeaderboard = c.m_leaderboard;
			_callback.Invoke();
		}, delegate(HttpC c)
		{
			ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), c.www, () => PsUITournamentLeaderboard.GetLeaderboardData(_callback), null);
		}, null, null);
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x000BAA10 File Offset: 0x000B8E10
	private void GetLeaderboard()
	{
		this.m_updatingLeaderboard = true;
		if (this.fakeLeaderboardAnimation && PsUITournamentLeaderboard.m_oldLeaderboard != null)
		{
			TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0f, 0.5f, false, delegate(TimerC c)
			{
				TimerS.RemoveComponent(c);
				this.GetLeaderboardSucceed(this.GetFakeHighscoreList());
			});
		}
		else
		{
			HttpC leaderboard = Tournament.GetLeaderboard(new Action<Tournament.TournamentLeaderboard>(this.GetLeaderboardSucceed), new Action<HttpC>(this.GetLeaderboardFailed), null, null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, leaderboard);
		}
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x000BAA9C File Offset: 0x000B8E9C
	private void CreateLeaderboardAnimationData(List<HighscoreDataEntry> _leaderboard)
	{
		List<LeaderboardAnimation> list = new List<LeaderboardAnimation>();
		for (int i = 0; i < _leaderboard.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < PsUITournamentLeaderboard.m_oldLeaderboard.Count; j++)
			{
				if (_leaderboard[i].playerId == PsUITournamentLeaderboard.m_oldLeaderboard[j].playerId)
				{
					flag = true;
					if (_leaderboard[i].time < PsUITournamentLeaderboard.m_oldLeaderboard[j].time)
					{
						list.Add(new LeaderboardAnimation(_leaderboard[i].playerId, _leaderboard[i].time, null));
					}
					break;
				}
			}
			if (!flag)
			{
				list.Add(new LeaderboardAnimation(_leaderboard[i].playerId, _leaderboard[i].time, _leaderboard[i]));
				_leaderboard[i].time = int.MaxValue;
			}
		}
		Random random = new Random();
		int k = list.Count;
		while (k > 1)
		{
			k--;
			int num = random.Next(k + 1);
			LeaderboardAnimation leaderboardAnimation = list[num];
			list[num] = list[k];
			list[k] = leaderboardAnimation;
		}
		foreach (LeaderboardAnimation leaderboardAnimation2 in list)
		{
			this.m_leaderboardAnimationList.Enqueue(leaderboardAnimation2);
		}
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x000BAC3C File Offset: 0x000B903C
	private void AnimateLeaderboard()
	{
		if (this.m_leaderboardAnimationList.Count > 0)
		{
			this.m_animation = true;
			LeaderboardAnimation data = this.m_leaderboardAnimationList.Dequeue();
			if (data.newEntry != null && !this.m_entryDictionary.ContainsKey(data.id))
			{
				int num = ((PsUITournamentLeaderboard.m_hostTime >= data.time && (PsUITournamentLeaderboard.m_hostTime != int.MaxValue || data.time != int.MaxValue)) ? 0 : (-1));
				PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = new PsUITournamentLeaderboardEntry(this.vlist, data.newEntry, this.vlist.m_childs.Count + 1 + num, (PsUITournamentLeaderboard.m_oldLeaderboard == null) ? 0 : PsUITournamentLeaderboard.m_oldLeaderboard[0].time);
				psUITournamentLeaderboardEntry.SetHeight(0.02f, RelativeTo.ScreenLongest);
				psUITournamentLeaderboardEntry.SetHorizontalAlign(0f);
				this.m_entryDictionary.Add(data.newEntry.playerId, psUITournamentLeaderboardEntry);
				psUITournamentLeaderboardEntry.Update();
				psUITournamentLeaderboardEntry.SetRollIndex(this.currentElement);
				UIScrollableCanvas scrollCanvas = this.m_scrollCanvas;
				scrollCanvas.m_actualMargins.t = scrollCanvas.m_actualMargins.t - 0.02f * (float)Screen.width;
				this.m_scrollCanvas.m_contentHeight += 0.04f * (float)Screen.width;
				float num2 = this.vlist.m_childs[1].m_TC.transform.localPosition.y - this.vlist.m_childs[0].m_TC.transform.localPosition.y;
				psUITournamentLeaderboardEntry.m_TC.transform.localPosition = new Vector3(this.vlist.m_childs[this.vlist.m_childs.Count - 2].m_TC.transform.localPosition.x, this.vlist.m_childs[this.vlist.m_childs.Count - 2].m_TC.transform.localPosition.y + num2, this.vlist.m_childs[this.vlist.m_childs.Count - 2].m_TC.transform.localPosition.z);
				PsUITournamentLeaderboard.m_oldLeaderboard.Add(data.newEntry);
			}
			int i = 0;
			while (i < this.vlist.m_childs.Count)
			{
				if ((this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry).m_highscoreData.playerId == data.id)
				{
					string text = HighScores.TimeScoreToTimeString((this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry).m_highscoreData.time);
					string text2 = HighScores.TimeScoreToTimeString(data.time);
					PsUITournamentLeaderboardEntry leaderboardEntry = this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry;
					if (text == text2)
					{
						this.m_animation = false;
						return;
					}
					leaderboardEntry.m_highscoreData.time = data.time;
					int target = PsUITournamentLeaderboard.GetPlayerIndex(data.time, PsUITournamentLeaderboard.m_oldLeaderboard);
					leaderboardEntry.MakeSolid();
					leaderboardEntry.d_Draw(leaderboardEntry);
					leaderboardEntry.AnimateTimescore(data.time, 0.3f, delegate
					{
						PsUITournamentLeaderboard.m_oldLeaderboard[i].time = data.time;
						if (target != i)
						{
							this.ChangePosition(i, target, delegate
							{
								this.EntryAnimationEndCallback(leaderboardEntry);
							});
							HighscoreDataEntry highscoreDataEntry = PsUITournamentLeaderboard.m_oldLeaderboard[i];
							PsUITournamentLeaderboard.m_oldLeaderboard.RemoveAt(i);
							PsUITournamentLeaderboard.m_oldLeaderboard.Insert(target, highscoreDataEntry);
						}
						else
						{
							this.EntryAnimationEndCallback(leaderboardEntry);
						}
						if (target == 0)
						{
							int num3 = ((target != 0) ? i : this.vlist.m_childs.Count);
							leaderboardEntry.SetGapToBest(PsUITournamentLeaderboard.m_oldLeaderboard[0].time);
							for (int j = 1; j < this.vlist.m_childs.Count; j++)
							{
								(this.vlist.m_childs[j] as PsUITournamentLeaderboardEntry).SetGapToBest(data.time);
							}
						}
						else
						{
							leaderboardEntry.SetGapToBest(PsUITournamentLeaderboard.m_oldLeaderboard[0].time);
						}
					});
					break;
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x000BB080 File Offset: 0x000B9480
	private void EntryAnimationEndCallback(PsUITournamentLeaderboardEntry _entry)
	{
		_entry.MakeTransparent();
		_entry.m_TC.transform.position += new Vector3(0f, 0f, 10f);
		_entry.d_Draw(_entry);
		this.m_animation = false;
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x000BB0D8 File Offset: 0x000B94D8
	private void GetLeaderboardSucceed(Tournament.TournamentLeaderboard _leaderboard)
	{
		PsUITournamentLeaderboard.SetTournamentData(_leaderboard);
		PsUITournamentLeaderboard.m_leaderboardCount = _leaderboard.m_leaderboard.Count;
		this.m_updatingLeaderboard = false;
		PsUITournamentLeaderboard.m_newLeaderboard = _leaderboard.m_leaderboard;
		if (this.m_hasLeaderboard)
		{
			this.CreateLeaderboardAnimationData(_leaderboard.m_leaderboard);
		}
		else
		{
			this.m_hasLeaderboard = true;
			this.CreateLeaderboardEntries(_leaderboard.m_leaderboard);
			this.Update();
			PsUITournamentLeaderboard.m_oldLeaderboard = _leaderboard.m_leaderboard;
		}
		this.m_lastUpdateTime = Main.m_EPOCHSeconds;
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x000BB158 File Offset: 0x000B9558
	private void RemoveHttpComponents()
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Http, this.m_TC.p_entity);
		foreach (IComponent component in componentsByEntity)
		{
			HttpS.RemoveComponent(component as HttpC);
		}
		this.m_updatingLeaderboard = false;
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x000BB1D0 File Offset: 0x000B95D0
	public void Deactivate()
	{
		this.RemoveHttpComponents();
		if (this.m_backgroundParent != null)
		{
			this.m_backgroundParent.Destroy();
			this.m_backgroundParent = null;
		}
		if (Mathf.Min(PsState.m_activeGameLoop.m_timeScoreBest, PsState.m_activeGameLoop.m_timeScoreCurrent) == 0)
		{
		}
		this.m_leaderboardAnimationList.Clear();
		this.m_animation = false;
		this.RemoveTweens();
		this.SetEntryInfos(PsUITournamentLeaderboard.GetNewestLeaderboard());
		this.SetEntryPositions();
		this.m_scrollCanvas.m_camera.gameObject.SetActive(false);
		this.m_activated = false;
		PsUITournamentLeaderboard.m_oldLeaderboard = PsUITournamentLeaderboard.GetNewestLeaderboard();
		PsUITournamentLeaderboard.m_newLeaderboard = null;
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x000BB27C File Offset: 0x000B967C
	public void Activate()
	{
		if (this.m_backgroundParent != null)
		{
			this.m_backgroundParent.Destroy();
		}
		this.m_backgroundParent = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_backgroundParent.SetMargins(0.025f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_backgroundParent.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this.m_backgroundParent, false, string.Empty, null, string.Empty);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentLeaderboardBGDrawHandler));
		this.m_backgroundParent.Update();
		this.m_scrollCanvas.m_camera.gameObject.SetActive(true);
		this.m_creationTick = Main.m_gameTicks;
		this.m_activated = true;
		this.CreatePlayerAnimationData();
		this.CreateFollowPlayerTweens();
		if ((PsState.m_activeGameLoop.m_gameMode as PsGameModeTournament).m_waitForNextGhost)
		{
			(PsState.m_activeGameLoop.m_gameMode as PsGameModeTournament).AddGhostLoadedCallback(new Action(this.GhostLoaderCallback));
		}
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x000BB398 File Offset: 0x000B9798
	public void AnimationStep()
	{
		if (!this.m_animation && this.m_leaderboardAnimationList.Count > 0)
		{
			this.AnimateLeaderboard();
		}
		else if (this.m_lastUpdateTime != 0.0 && !this.m_updatingLeaderboard && this.m_leaderboardAnimationList.Count < 1 && Main.m_EPOCHSeconds - this.m_lastUpdateTime > 6.0 && this.m_currentRoom == this.m_loadedRoom)
		{
			this.GetLeaderboard();
		}
		if (this.m_creationTick != -1 && (Main.m_gameTicks - this.m_creationTick - 120) % 180 == 1)
		{
			if (this.m_scrollCanvas.m_TAC.m_touchCount > 0)
			{
				this.m_creationTick = Main.m_gameTicks;
			}
			else
			{
				this.RollInfo(0f);
			}
		}
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x000BB488 File Offset: 0x000B9888
	public override void Step()
	{
		if (PsUITournamentLeaderboard.m_hostId == PlayerPrefsX.GetUserId())
		{
			int roomCount = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.roomCount;
			if (this.m_rightRoomChange != null && this.m_rightRoomChange.m_hit)
			{
				if (this.m_currentRoom < roomCount)
				{
					this.m_currentRoom++;
				}
				else
				{
					this.m_currentRoom = 1;
				}
				this.ChangeRoom();
			}
			else if (this.m_leftRoomChange != null && this.m_leftRoomChange.m_hit)
			{
				if (this.m_currentRoom > 1)
				{
					this.m_currentRoom--;
				}
				else
				{
					this.m_currentRoom = roomCount;
				}
				this.ChangeRoom();
			}
		}
		if (this.m_activated)
		{
			this.AnimationStep();
		}
		if (this.m_roomChangeDelay && (double)Main.m_gameTicks - this.m_roomChangeTimestamp > (double)this.m_roomChangeDelayTicks)
		{
			this.LoadNewRoom();
		}
		base.Step();
		if (Input.GetKeyUp(115))
		{
			int num = Random.Range(0, this.vlist.m_childs.Count);
			PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = this.vlist.m_childs[num] as PsUITournamentLeaderboardEntry;
			this.m_leaderboardAnimationList.Enqueue(new LeaderboardAnimation(psUITournamentLeaderboardEntry.m_highscoreData.playerId, psUITournamentLeaderboardEntry.m_highscoreData.time - 400000, null));
		}
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x000BB5FE File Offset: 0x000B99FE
	private void ChangeRoom()
	{
		this.m_changingRooms.Invoke();
		this.m_roomChangeDelay = true;
		this.m_roomChangeTimestamp = (double)Main.m_gameTicks;
		this.CreateLeaderboardNumberUI(this.m_hlistRoomNumber.m_parent, true);
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x000BB630 File Offset: 0x000B9A30
	private void LoadNewRoom()
	{
		this.m_roomChangeDelay = false;
		if (this.m_loadedRoom != this.m_currentRoom)
		{
			if (PsUITournamentLeaderboard.m_leaderboardDatas.ContainsKey(this.m_currentRoom))
			{
				Debug.LogError("Change to room from cache");
				this.ChangeRoomData(PsUITournamentLeaderboard.m_leaderboardDatas[this.m_currentRoom]);
			}
			this.m_changeRoomStartedCallback.Invoke();
			this.ServerChangeRoom();
		}
		else
		{
			this.m_changeRoomCompletedCallback.Invoke(PsUITournamentLeaderboard.m_leaderboardDatas[this.m_currentRoom]);
		}
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x000BB6BC File Offset: 0x000B9ABC
	private HttpC ServerChangeRoom()
	{
		this.RemoveHttpComponents();
		this.m_loadingNewRoom = true;
		HttpC httpC = Tournament.HostChangeRoom(this.m_currentRoom, new Action<Tournament.TournamentLeaderboard>(this.ServerRoomChangeSucceed), new Action<HttpC>(this.ServerRoomChangeFailed), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
		return httpC;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x000BB70D File Offset: 0x000B9B0D
	private void ServerRoomChangeFailed(HttpC _c)
	{
		Debug.LogError("Server room change FAILED");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => this.ServerChangeRoom(), null);
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x000BB73C File Offset: 0x000B9B3C
	private void ChangeRoomData(Tournament.TournamentLeaderboard _data)
	{
		this.m_loadedRoom = _data.room;
		this.m_leaderboardAnimationList.Clear();
		this.m_animation = false;
		this.RemoveTweens();
		this.RemoveGhostTriangles();
		this.SetFreshEntryInfos(_data.m_leaderboard);
		this.SetEntryPositions();
		this.SetRollInfos(0);
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x000BB78C File Offset: 0x000B9B8C
	private void ServerRoomChangeSucceed(Tournament.TournamentLeaderboard _data)
	{
		this.m_changeRoomCompletedCallback.Invoke(_data);
		Debug.LogError("Server room change SUCCEED: ");
		this.m_loadingNewRoom = false;
		PsUITournamentLeaderboard.m_oldLeaderboard = (PsUITournamentLeaderboard.m_newLeaderboard = _data.m_leaderboard);
		PsUITournamentLeaderboard.SetTournamentData(_data);
		if (_data.room != this.m_loadedRoom)
		{
			this.ChangeRoomData(_data);
		}
		else
		{
			this.CreatePlayerAnimationData();
		}
		PsGameLoopTournament psGameLoopTournament = PsState.m_activeGameLoop as PsGameLoopTournament;
		psGameLoopTournament.m_gameMode.LoadGhosts(null);
		(psGameLoopTournament.m_gameMode as PsGameModeTournament).AddGhostLoadedCallback(new Action(this.GhostLoaderCallback));
		Debug.LogError("Game ended: " + PsState.m_activeMinigame.m_gameEnded);
		if (PsState.m_activeMinigame != null && PsState.m_activeGameLoop != null && !PsState.m_activeMinigame.m_gameEnded)
		{
			(psGameLoopTournament.m_gameMode as PsGameModeTournament).AddGhostLoadedCallback(new Action(this.GhostsLoadedOnRoomChangeCallback));
		}
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x000BB880 File Offset: 0x000B9C80
	private void GhostsLoadedOnRoomChangeCallback()
	{
		if (PsState.m_activeMinigame != null && PsState.m_activeGameLoop != null)
		{
			if (!PsState.m_activeMinigame.m_gameEnded)
			{
				(PsState.m_activeGameLoop as PsGameLoopTournament).m_gameMode.CreatePlaybackGhostVisuals();
			}
			((PsState.m_activeGameLoop as PsGameLoopTournament).m_gameMode as PsGameModeTournament).RemoveGhostLoadedCallback(new Action(this.GhostsLoadedOnRoomChangeCallback));
		}
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x000BB8E9 File Offset: 0x000B9CE9
	public void AddRoomChangeCompletedAction(Action<Tournament.TournamentLeaderboard> _callback)
	{
		this.m_changeRoomCompletedCallback = (Action<Tournament.TournamentLeaderboard>)Delegate.Combine(this.m_changeRoomCompletedCallback, _callback);
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x000BB902 File Offset: 0x000B9D02
	public void RemoveRoomChangeCompletedAction(Action<Tournament.TournamentLeaderboard> _callback)
	{
		this.m_changeRoomCompletedCallback = (Action<Tournament.TournamentLeaderboard>)Delegate.Remove(this.m_changeRoomCompletedCallback, _callback);
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x000BB91B File Offset: 0x000B9D1B
	public void AddRoomChangeStartedAction(Action _callback)
	{
		this.m_changeRoomStartedCallback = (Action)Delegate.Combine(this.m_changeRoomStartedCallback, _callback);
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x000BB934 File Offset: 0x000B9D34
	public void RemoveRoomChangeStartedAction(Action _callback)
	{
		this.m_changeRoomStartedCallback = (Action)Delegate.Remove(this.m_changeRoomStartedCallback, _callback);
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x000BB94D File Offset: 0x000B9D4D
	public void AddRoomChangingAction(Action _callback)
	{
		this.m_changingRooms = (Action)Delegate.Combine(this.m_changingRooms, _callback);
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x000BB966 File Offset: 0x000B9D66
	public void RemoveRoomChangingAction(Action _callback)
	{
		this.m_changingRooms = (Action)Delegate.Remove(this.m_changingRooms, _callback);
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x000BB980 File Offset: 0x000B9D80
	private void RemoveGhostTriangles()
	{
		foreach (KeyValuePair<string, PsUITournamentLeaderboardEntry> keyValuePair in this.m_entryDictionary)
		{
			keyValuePair.Value.RemoveTriangle();
		}
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x000BB9E4 File Offset: 0x000B9DE4
	private bool HasPlayer(int _time, List<HighscoreDataEntry> _leaderboard)
	{
		string userId = PlayerPrefsX.GetUserId();
		for (int i = 0; i < _leaderboard.Count; i++)
		{
			if (_leaderboard[i].playerId == userId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x000BBA28 File Offset: 0x000B9E28
	public static int GetPlayerIndex(int _time, List<HighscoreDataEntry> _leaderboard)
	{
		int num = _leaderboard.Count;
		if (_time == 2147483647 || _time == 0 || _time == -1)
		{
			return num;
		}
		for (int i = 0; i < _leaderboard.Count; i++)
		{
			if (_leaderboard[i].time >= _time)
			{
				num = i;
				break;
			}
		}
		return num;
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x000BBA88 File Offset: 0x000B9E88
	public static int GetPlayerPosition(int _time)
	{
		List<HighscoreDataEntry> newestLeaderboard = PsUITournamentLeaderboard.GetNewestLeaderboard();
		int num = newestLeaderboard.Count;
		if (_time == 2147483647 || _time == 0 || _time == -1)
		{
			return num;
		}
		int num2 = 0;
		for (int i = 0; i < newestLeaderboard.Count; i++)
		{
			if (newestLeaderboard[i].time >= _time)
			{
				num = i + 1 + num2;
				break;
			}
			if (num2 == 0 && newestLeaderboard[i].playerId == PsUITournamentLeaderboard.m_hostId)
			{
				num2 = -1;
			}
		}
		return num;
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000BBB18 File Offset: 0x000B9F18
	public static int GetPlayerPosition(string _id, List<HighscoreDataEntry> _leaderboard)
	{
		for (int i = 0; i < _leaderboard.Count; i++)
		{
			if (_leaderboard[i].playerId == _id)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x000BBB58 File Offset: 0x000B9F58
	private static HighscoreDataEntry GetHostHighscoreDataEntry(int _time)
	{
		EventMessage eventMessage = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage;
		HighscoreDataEntry highscoreDataEntry = new HighscoreDataEntry();
		highscoreDataEntry.facebookId = eventMessage.tournament.ownerFacebookId;
		highscoreDataEntry.gameId = PsState.m_activeGameLoop.m_minigameId;
		highscoreDataEntry.name = eventMessage.tournament.ownerName;
		highscoreDataEntry.time = _time;
		highscoreDataEntry.playerId = eventMessage.tournament.ownerId;
		PsUITournamentLeaderboard.m_hostId = highscoreDataEntry.playerId;
		return highscoreDataEntry;
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x000BBBD4 File Offset: 0x000B9FD4
	private HighscoreDataEntry GetPlayerHighscoreDataentry(int _time)
	{
		return new HighscoreDataEntry
		{
			facebookId = PlayerPrefsX.GetFacebookId(),
			gameId = PsState.m_activeGameLoop.m_minigameId,
			name = PlayerPrefsX.GetUserName(),
			time = _time,
			playerId = PlayerPrefsX.GetUserId(),
			country = PlayerPrefsX.GetCountryCode()
		};
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000BBC2C File Offset: 0x000BA02C
	private void ChangeRandomPosition()
	{
		int count = this.vlist.m_childs.Count;
		if (count < 2)
		{
			return;
		}
		Random.InitState((int)Main.m_gameTimeSinceAppStarted);
		int num = Random.Range(1, count);
		int num2 = Random.Range(0, num);
		Debug.LogError(string.Concat(new object[] { "Playerindex: ", num, " target: ", num2 }));
		int num3 = num - num2;
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x000BBCA4 File Offset: 0x000BA0A4
	private void ChangePosition(int _player, int _target, Action _endAction)
	{
		float y = this.vlist.m_childs[0].m_TC.transform.localPosition.y;
		float num = -((float)Screen.width * 0.02f);
		int num2 = _player - _target;
		PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = this.vlist.m_childs[_player] as PsUITournamentLeaderboardEntry;
		bool flag = psUITournamentLeaderboardEntry.m_highscoreData.playerId == PsUITournamentLeaderboard.m_hostId;
		bool flag2 = PsUITournamentLeaderboard.m_hostTime != int.MaxValue && PsUITournamentLeaderboard.m_hostTime != 0;
		for (int i = _target; i < _player; i++)
		{
			PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry2 = this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry;
			Vector3 vector;
			vector..ctor(psUITournamentLeaderboardEntry2.m_TC.transform.localPosition.x, y + (float)(i + 1) * num, 0f);
			TweenC tweenC = TweenS.AddTransformTween(psUITournamentLeaderboardEntry2.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, vector, 1.5f, 0f, true);
			float num3 = (float)(i + 1 - _target) / ((float)num2 + 1f);
			if (!flag)
			{
				int num4 = ((psUITournamentLeaderboardEntry2.m_highscoreData.time <= PsUITournamentLeaderboard.m_hostTime || !flag2) ? 0 : (-1));
				psUITournamentLeaderboardEntry2.AnimatePositionNumber(i + 1 + num4, i + num4, tweenC, num3 - 0.5f);
			}
		}
		psUITournamentLeaderboardEntry.m_TC.transform.localPosition = new Vector3(psUITournamentLeaderboardEntry.m_TC.transform.localPosition.x, psUITournamentLeaderboardEntry.m_TC.transform.localPosition.y, -20f);
		Vector3 vector2;
		vector2..ctor(this.vlist.m_childs[_player].m_TC.transform.localPosition.x, y + (float)_target * num, -20f);
		TweenC tweenC2 = TweenS.AddTransformTween(this.vlist.m_childs[_player].m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, vector2, 1.5f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC2, delegate(TweenC _c)
		{
			_endAction.Invoke();
		});
		int num5 = ((psUITournamentLeaderboardEntry.m_highscoreData.time <= PsUITournamentLeaderboard.m_hostTime || !flag2) ? 0 : (-1));
		if (num5 == -1)
		{
			int playerIndex = PsUITournamentLeaderboard.GetPlayerIndex(PsUITournamentLeaderboard.m_hostTime, PsUITournamentLeaderboard.m_oldLeaderboard);
			psUITournamentLeaderboardEntry.AnimatePositionNumber(_target + num5, _player + num5, tweenC2, 0f);
		}
		else
		{
			psUITournamentLeaderboardEntry.AnimatePositionNumber(_target + num5, _player + num5, tweenC2, 0f);
		}
		UIComponent uicomponent = this.vlist.m_childs[_player];
		this.vlist.m_childs.RemoveAt(_player);
		this.vlist.m_childs.Insert(_target, uicomponent);
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x000BBF94 File Offset: 0x000BA394
	private int GetOldTime()
	{
		if (PsUITournamentLeaderboard.m_oldLeaderboard == null)
		{
			return -1;
		}
		string userId = PlayerPrefsX.GetUserId();
		int num = -1;
		for (int i = 0; i < PsUITournamentLeaderboard.m_oldLeaderboard.Count; i++)
		{
			if (PsUITournamentLeaderboard.m_oldLeaderboard[i].playerId == userId)
			{
				num = PsUITournamentLeaderboard.m_oldLeaderboard[i].time;
				break;
			}
		}
		return num;
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x000BC004 File Offset: 0x000BA404
	private void CreateLeaderboardNumberUI(UIComponent _parent, bool _update = false)
	{
		if (this.m_hlistRoomNumber != null)
		{
			this.m_hlistRoomNumber.Destroy();
		}
		this.m_hlistRoomNumber = new UIHorizontalList(_parent, string.Empty);
		this.m_hlistRoomNumber.RemoveDrawHandler();
		this.m_hlistRoomNumber.SetHeight(0.4f, RelativeTo.ParentHeight);
		this.m_hlistRoomNumber.SetSpacing(0.2f, RelativeTo.ParentHeight);
		this.m_hlistRoomNumber.SetAlign(0.15f, 0.05f);
		TournamentInfo tournament = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament;
		string text = string.Concat(new object[]
		{
			PsStrings.Get(StringID.TOUR_ROOM),
			this.m_currentRoom,
			"/",
			tournament.roomCount
		});
		UIText uitext = new UIText(this.m_hlistRoomNumber, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 1f, RelativeTo.ParentHeight, "#5D767D", null);
		if (PsUITournamentLeaderboard.m_hostId == PlayerPrefsX.GetUserId())
		{
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_roomchange_arrow", null);
			frame.flipX = true;
			this.m_leftRoomChange = new UIRectSpriteButton(this.m_hlistRoomNumber, string.Empty, PsState.m_uiSheet, frame, true, false);
			this.m_leftRoomChange.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_leftRoomChange.SetHorizontalAlign(-0.5f);
			this.m_leftRoomChange.MoveToIndexAtParentsChildList(0);
			this.m_rightRoomChange = new UIRectSpriteButton(this.m_hlistRoomNumber, "rightRoomChange", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_roomchange_arrow", null), true, false);
			this.m_rightRoomChange.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_rightRoomChange.SetHorizontalAlign(1.5f);
		}
		if (_update)
		{
			this.m_hlistRoomNumber.Update();
		}
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x000BC1D4 File Offset: 0x000BA5D4
	private void CreateLeaderboard(List<HighscoreDataEntry> _leaderboard)
	{
		this.DestroyChildren();
		this.RemoveDrawHandler();
		this.m_backgroundParent = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_backgroundParent.SetMargins(0.025f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_backgroundParent.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this.m_backgroundParent, false, string.Empty, null, string.Empty);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentLeaderboardBGDrawHandler));
		this.m_scrollCanvas = new UIScrollableCanvas(this, "ScrollCanvas");
		this.m_scrollCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_scrollCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_scrollCanvas.RemoveDrawHandler();
		this.m_scrollCanvas.m_TAC.m_allowSecondary = true;
		float num = 0.05f;
		UICanvas uicanvas2 = new UICanvas(this.m_scrollCanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(num, RelativeTo.ScreenWidth);
		uicanvas2.SetVerticalAlign(1f);
		uicanvas2.RemoveDrawHandler();
		this.CreateLeaderboardNumberUI(uicanvas2, false);
		UIText uitext = new UIText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TOUR_TIME), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.4f, RelativeTo.ParentHeight, "#5D767D", null);
		uitext.SetAlign(0.95f, 0.05f);
		UIText uitext2 = new UIText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TOUR_PRIZE), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.4f, RelativeTo.ParentHeight, "#5D767D", null);
		uitext2.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		uitext2.SetAlign(0.95f, 0.05f);
		UIText uitext3 = new UIText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.TOUR_GAP), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.4f, RelativeTo.ParentHeight, "#5D767D", null);
		uitext3.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		uitext3.SetAlign(0.95f, 0.05f);
		List<UIComponent> list = new List<UIComponent>();
		list.Add(uitext);
		list.Add(uitext2);
		list.Add(uitext3);
		this.m_rollingInfoElements = list;
		this.vlist = new UIVerticalList(this.m_scrollCanvas, "leaderboard");
		this.vlist.RemoveDrawHandler();
		this.vlist.SetVerticalAlign(1f);
		this.vlist.SetMargins(0.025f, 0f, num, num, RelativeTo.ScreenWidth);
		this.CreateLeaderboardEntries(_leaderboard);
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x000BC478 File Offset: 0x000BA878
	public void CreateFollowPlayerTweens()
	{
		if (this.m_leaderboardAnimationList != null && this.m_leaderboardAnimationList.Count > 0 && PsUITournamentLeaderboard.GetNewestLeaderboard().Count > 19)
		{
			string userid = PlayerPrefsX.GetUserId();
			LeaderboardAnimation leaderboardAnimation = Enumerable.First<LeaderboardAnimation>(this.m_leaderboardAnimationList, (LeaderboardAnimation c) => c.id == userid);
			if (leaderboardAnimation != null && this.m_entryDictionary.ContainsKey(userid) && PsUITournamentLeaderboard.GetPlayerIndex(leaderboardAnimation.time, PsUITournamentLeaderboard.GetNewestLeaderboard()) != PsUITournamentLeaderboard.GetPlayerIndex(this.m_entryDictionary[userid].m_highscoreData.time, PsUITournamentLeaderboard.GetNewestLeaderboard()))
			{
				int playerIndex = PsUITournamentLeaderboard.GetPlayerIndex(leaderboardAnimation.time, PsUITournamentLeaderboard.GetNewestLeaderboard());
				int playerIndex2 = PsUITournamentLeaderboard.GetPlayerIndex(this.m_entryDictionary[userid].m_highscoreData.time, PsUITournamentLeaderboard.GetNewestLeaderboard());
				float num = (float)Screen.width * 0.02f * 7f;
				float num2 = Math.Min((float)Screen.width * 0.02f * (float)(-(float)playerIndex) + num, 0f);
				float num3 = Math.Max((float)Screen.width * 0.02f * (float)(-(float)playerIndex2) + num, (float)Screen.width * 0.02f * (float)(-(float)PsUITournamentLeaderboard.GetNewestLeaderboard().Count + 7) + num);
				this.m_scrollCanvas.m_TAC.m_allowPrimary = false;
				this.m_scrollCanvas.m_TAC.m_allowSecondary = false;
				this.m_scrollCanvas.m_overrideScrollPosition = false;
				Vector3 vector;
				vector..ctor(0f, num3);
				this.m_scrollCanvas.m_scrollTC.transform.localPosition = vector;
				Vector3 vector2;
				vector2..ctor(0f, num2);
				TweenC tweenC = TweenS.AddTransformTween(this.m_scrollCanvas.m_scrollTC, TweenedProperty.Position, TweenStyle.CubicInOut, vector, vector2, 1.5f, 0.3f, true);
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
				{
					this.m_scrollCanvas.m_TAC.m_allowPrimary = true;
					this.m_scrollCanvas.m_TAC.m_allowSecondary = true;
				});
			}
		}
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x000BC674 File Offset: 0x000BAA74
	private void RollInfo(float _delay = 0f)
	{
		UIComponent uicomponent = this.m_rollingInfoElements[this.currentElement];
		int num = ((this.currentElement < this.m_rollingInfoElements.Count - 1) ? (this.currentElement + 1) : 0);
		UIComponent next = this.m_rollingInfoElements[num];
		this.currentElement = num;
		this.rollTween = TweenS.AddTransformTween(uicomponent.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, 0f), new Vector3(-90f, 0f, 0f), 0.2f, _delay, false, true);
		TweenS.AddTweenEndEventListener(this.rollTween, delegate(TweenC c)
		{
			this.rollTween = TweenS.AddTransformTween(next.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(90f, 0f, 0f), new Vector3(0f, 0f, 0f), 0.2f, 0f, false, true);
			TweenS.AddTweenEndEventListener(this.rollTween, delegate(TweenC _c)
			{
				this.rollTween = null;
			});
		});
		for (int i = 0; i < this.vlist.m_childs.Count; i++)
		{
			PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry;
			float num2 = (float)i * 0.008f;
			psUITournamentLeaderboardEntry.RollInfo(num2, this.currentElement);
		}
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x000BC78C File Offset: 0x000BAB8C
	public void SetRollInfos(int _targetIndex)
	{
		for (int i = 0; i < this.vlist.m_childs.Count; i++)
		{
			PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry;
			psUITournamentLeaderboardEntry.SetRollIndex(_targetIndex);
		}
		if (this.rollTween != null)
		{
			EntityManager.RemoveComponentFromEntity(this.rollTween);
			this.rollTween = null;
		}
		for (int j = 0; j < this.m_rollingInfoElements.Count; j++)
		{
			TransformS.SetRotation(this.m_rollingInfoElements[j].m_TC, new Vector3(90f, 0f, 0f));
		}
		TransformS.SetRotation(this.m_rollingInfoElements[_targetIndex].m_TC, new Vector3(0f, 0f, 0f));
		this.currentElement = _targetIndex;
		this.m_creationTick = Main.m_gameTicks;
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000BC878 File Offset: 0x000BAC78
	public void RemoveTweens()
	{
		for (int i = 0; i < this.vlist.m_childs.Count; i++)
		{
			(this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry).RemoveAnimations();
		}
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x000BC8C4 File Offset: 0x000BACC4
	public void SetEntryPositions()
	{
		if (this.vlist != null && this.vlist.m_childs.Count > 0)
		{
			float y = this.vlist.m_childs[0].m_TC.transform.localPosition.y;
			for (int i = 0; i < this.vlist.m_childs.Count; i++)
			{
				PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry;
				float num = -((float)Screen.width * 0.02f);
				float num2 = y + (float)i * num;
				float y2 = psUITournamentLeaderboardEntry.m_TC.transform.localPosition.y;
				Vector3 vector;
				vector..ctor(psUITournamentLeaderboardEntry.m_TC.transform.localPosition.x, num2, -1f);
				TransformS.SetPosition(psUITournamentLeaderboardEntry.m_TC, vector);
			}
		}
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x000BC9BC File Offset: 0x000BADBC
	public void SetEntryInfos(List<HighscoreDataEntry> _leaderboard)
	{
		int num = 0;
		for (int i = 0; i < _leaderboard.Count; i++)
		{
			PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry;
			if (i < this.vlist.m_childs.Count)
			{
				psUITournamentLeaderboardEntry = this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry;
				if (psUITournamentLeaderboardEntry.m_highscoreData.playerId != _leaderboard[i].playerId)
				{
					psUITournamentLeaderboardEntry.SetNewPlayerInfo(_leaderboard[i]);
					psUITournamentLeaderboardEntry.SetInfo(_leaderboard[i], i + 1 + num, _leaderboard.Count, _leaderboard[0].time);
				}
				else
				{
					psUITournamentLeaderboardEntry.SetInfo(_leaderboard[i], i + 1 + num, _leaderboard.Count, _leaderboard[0].time);
				}
			}
			else
			{
				Debug.LogError("Create new entry for id: " + _leaderboard[i].playerId);
				psUITournamentLeaderboardEntry = new PsUITournamentLeaderboardEntry(this.vlist, _leaderboard[i], i + 1 + num, _leaderboard[0].time);
				psUITournamentLeaderboardEntry.SetHeight(0.02f, RelativeTo.ScreenLongest);
				psUITournamentLeaderboardEntry.Update();
				psUITournamentLeaderboardEntry.SetRollIndex(this.currentElement);
				UIScrollableCanvas scrollCanvas = this.m_scrollCanvas;
				scrollCanvas.m_actualMargins.t = scrollCanvas.m_actualMargins.t - 0.02f * (float)Screen.width;
				this.m_scrollCanvas.m_contentHeight += 0.04f * (float)Screen.width;
			}
			this.m_entryDictionary[_leaderboard[i].playerId] = psUITournamentLeaderboardEntry;
			if (num == 0 && psUITournamentLeaderboardEntry.m_highscoreData.playerId == PsUITournamentLeaderboard.m_hostId)
			{
				num = -1;
				psUITournamentLeaderboardEntry.SetHost(true, true);
			}
			else
			{
				psUITournamentLeaderboardEntry.SetHost(false, true);
			}
		}
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x000BCB80 File Offset: 0x000BAF80
	public void SetFreshEntryInfos(List<HighscoreDataEntry> _leaderboard)
	{
		int count = this.vlist.m_childs.Count;
		int count2 = _leaderboard.Count;
		int num = 0;
		int num2 = 0;
		this.m_entryDictionary.Clear();
		for (int i = 0; i < _leaderboard.Count; i++)
		{
			PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry;
			if (i < this.vlist.m_childs.Count)
			{
				psUITournamentLeaderboardEntry = this.vlist.m_childs[i] as PsUITournamentLeaderboardEntry;
				psUITournamentLeaderboardEntry.SetNewPlayerInfo(_leaderboard[i]);
				psUITournamentLeaderboardEntry.SetInfo(_leaderboard[i], i + 1 + num, _leaderboard.Count, _leaderboard[0].time);
			}
			else
			{
				psUITournamentLeaderboardEntry = new PsUITournamentLeaderboardEntry(this.vlist, _leaderboard[i], i + 1 + num, _leaderboard[0].time);
				psUITournamentLeaderboardEntry.SetHeight(0.02f, RelativeTo.ScreenLongest);
				psUITournamentLeaderboardEntry.Update();
			}
			psUITournamentLeaderboardEntry.m_topTimeScore = _leaderboard[0].time;
			this.m_entryDictionary[_leaderboard[i].playerId] = psUITournamentLeaderboardEntry;
			if (num == 0 && psUITournamentLeaderboardEntry.m_highscoreData.playerId == PsUITournamentLeaderboard.m_hostId)
			{
				num = -1;
				psUITournamentLeaderboardEntry.SetHost(true, true);
			}
			else
			{
				psUITournamentLeaderboardEntry.SetHost(false, true);
			}
			num2 = i;
		}
		if (this.vlist.m_childs.Count - 1 > num2)
		{
			Debug.LogError("Destroying empty leaderboard entries: " + (this.vlist.m_childs.Count - num2));
			this.vlist.DestroyChildren(num2 + 1);
		}
		int num3 = count2 - count;
		UIScrollableCanvas scrollCanvas = this.m_scrollCanvas;
		scrollCanvas.m_actualMargins.t = scrollCanvas.m_actualMargins.t - (float)num3 * 0.02f * (float)Screen.width;
		this.m_scrollCanvas.m_contentHeight += (float)(num3 * 2) * 0.02f * (float)Screen.width;
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x000BCD80 File Offset: 0x000BB180
	private void CreateLeaderboardEntries(List<HighscoreDataEntry> _leaderboard)
	{
		if (this.vlist.m_childs.Count > 0)
		{
			this.vlist.DestroyChildren();
		}
		if (_leaderboard == null)
		{
			new PsUILoadingAnimation(this.vlist, false);
		}
		else
		{
			this.m_entryDictionary = new Dictionary<string, PsUITournamentLeaderboardEntry>();
			int num = 0;
			int num2 = 0;
			while (_leaderboard != null && num2 < _leaderboard.Count)
			{
				PsUITournamentLeaderboardEntry psUITournamentLeaderboardEntry = new PsUITournamentLeaderboardEntry(this.vlist, _leaderboard[num2], num2 + 1 + num, _leaderboard[0].time);
				psUITournamentLeaderboardEntry.SetHeight(0.02f, RelativeTo.ScreenLongest);
				this.m_entryDictionary.Add(_leaderboard[num2].playerId, psUITournamentLeaderboardEntry);
				string ownerId = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament.ownerId;
				if (num == 0 && _leaderboard[num2].playerId == ownerId)
				{
					psUITournamentLeaderboardEntry.SetHost(true, false);
					num = -1;
				}
				num2++;
			}
			this.AddTriangles(false);
			this.m_creationTick = Main.m_gameTicks;
		}
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x000BCE8B File Offset: 0x000BB28B
	private void GetLeaderboardFailed(HttpC _c)
	{
		Debug.LogError("LEADERBOARD GET FAILED");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => Tournament.GetLeaderboard(new Action<Tournament.TournamentLeaderboard>(this.GetLeaderboardSucceed), new Action<HttpC>(this.GetLeaderboardFailed), null, null), null);
	}

	// Token: 0x040015DA RID: 5594
	public static List<HighscoreDataEntry> m_oldLeaderboard;

	// Token: 0x040015DB RID: 5595
	public static List<HighscoreDataEntry> m_newLeaderboard;

	// Token: 0x040015DC RID: 5596
	public static int m_leaderboardCount;

	// Token: 0x040015DD RID: 5597
	public Queue<LeaderboardAnimation> m_leaderboardAnimationList;

	// Token: 0x040015DE RID: 5598
	private bool m_hasLeaderboard;

	// Token: 0x040015DF RID: 5599
	private int m_creationTick = -1;

	// Token: 0x040015E0 RID: 5600
	private UICanvas m_backgroundParent;

	// Token: 0x040015E1 RID: 5601
	private const float TIMESCORE_ROLL_TIME = 0.3f;

	// Token: 0x040015E2 RID: 5602
	private const float POSITION_TWEEN_ROLL_TIME = 1.5f;

	// Token: 0x040015E3 RID: 5603
	private const float ENTRY_HEIGHT = 0.02f;

	// Token: 0x040015E4 RID: 5604
	public static string m_hostId;

	// Token: 0x040015E5 RID: 5605
	public static int m_hostTime;

	// Token: 0x040015E6 RID: 5606
	private static int m_hostPosition;

	// Token: 0x040015E7 RID: 5607
	private int m_currentRoom;

	// Token: 0x040015E8 RID: 5608
	private static Dictionary<int, Tournament.TournamentLeaderboard> m_leaderboardDatas = new Dictionary<int, Tournament.TournamentLeaderboard>();

	// Token: 0x040015E9 RID: 5609
	private bool m_updatingLeaderboard;

	// Token: 0x040015EA RID: 5610
	private bool fakeLeaderboardAnimation;

	// Token: 0x040015EB RID: 5611
	private bool m_animation;

	// Token: 0x040015EC RID: 5612
	private double m_lastUpdateTime;

	// Token: 0x040015ED RID: 5613
	public bool m_activated = true;

	// Token: 0x040015EE RID: 5614
	private bool m_loadingNewRoom;

	// Token: 0x040015EF RID: 5615
	private bool m_roomChangeDelay;

	// Token: 0x040015F0 RID: 5616
	private int m_roomChangeDelayTicks = 30;

	// Token: 0x040015F1 RID: 5617
	private double m_roomChangeTimestamp;

	// Token: 0x040015F2 RID: 5618
	private int m_loadedRoom;

	// Token: 0x040015F3 RID: 5619
	private Action<Tournament.TournamentLeaderboard> m_changeRoomCompletedCallback = delegate(Tournament.TournamentLeaderboard c)
	{
		Debug.LogError("Room change completed: " + c.room);
	};

	// Token: 0x040015F4 RID: 5620
	private Action m_changeRoomStartedCallback = delegate
	{
		Debug.LogError("Room change started: ");
	};

	// Token: 0x040015F5 RID: 5621
	private Action m_changingRooms = delegate
	{
		Debug.LogError("Room changing started: ");
	};

	// Token: 0x040015F6 RID: 5622
	private int m_oldTime = -1;

	// Token: 0x040015F7 RID: 5623
	public UIVerticalList vlist;

	// Token: 0x040015F8 RID: 5624
	public UIScrollableCanvas m_scrollCanvas;

	// Token: 0x040015F9 RID: 5625
	public UIRectSpriteButton m_leftRoomChange;

	// Token: 0x040015FA RID: 5626
	public UIRectSpriteButton m_rightRoomChange;

	// Token: 0x040015FB RID: 5627
	private UIHorizontalList m_hlistRoomNumber;

	// Token: 0x040015FC RID: 5628
	private List<UIComponent> m_rollingInfoElements;

	// Token: 0x040015FD RID: 5629
	private int currentElement;

	// Token: 0x040015FE RID: 5630
	private TweenC rollTween;

	// Token: 0x040015FF RID: 5631
	public Dictionary<string, PsUITournamentLeaderboardEntry> m_entryDictionary;
}
