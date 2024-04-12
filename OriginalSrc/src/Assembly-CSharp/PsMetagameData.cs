using System;
using System.Collections.Generic;

// Token: 0x0200013C RID: 316
public static class PsMetagameData
{
	// Token: 0x0600096C RID: 2412 RVA: 0x00064DC8 File Offset: 0x000631C8
	public static void Initialize()
	{
		PsMetagameData.m_units = new List<PsUnlockableCategory>();
		PsMetagameData.m_playerUnits = new List<PsUnlockableCategory>();
		PsMetagameData.m_gameModes = new List<PsUnlockableCategory>();
		PsMetagameData.m_gameAreas = new List<PsUnlockableCategory>();
		PsMetagameData.m_gameEnvironments = new List<PsUnlockableCategory>();
		PsMetagameData.m_gameMaterials = new List<PsUnlockableCategory>();
		PsMetagameData.m_menus = new List<PsUnlockableCategory>();
		PsMetagameData.m_dialogues = new List<PsDialogue>();
		PsMetagameData.m_planetUnlocks = new Dictionary<string, List<PsUnlock>>();
		PsMetagameData.m_levelTemplates = new List<PsLevelTemplate>();
		PsMetagameData.m_unlockedTutorials = new List<string>();
		PsMetagameData.CreateLeagueData();
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x00064E48 File Offset: 0x00063248
	public static void Clear()
	{
		PsMetagameData.m_playerUnits.Clear();
		PsMetagameData.m_units.Clear();
		PsMetagameData.m_gameModes.Clear();
		PsMetagameData.m_gameAreas.Clear();
		PsMetagameData.m_gameEnvironments.Clear();
		PsMetagameData.m_gameMaterials.Clear();
		PsMetagameData.m_menus.Clear();
		PsMetagameData.m_planetUnlocks.Clear();
		PsMetagameData.m_levelTemplates.Clear();
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00064EAF File Offset: 0x000632AF
	public static List<PsUnlock> GetPlanetUnlocks(string _planetIdentifier)
	{
		if (!PsMetagameData.m_planetUnlocks.ContainsKey(_planetIdentifier))
		{
			Debug.Log("No unlocks with identifier: " + _planetIdentifier, null);
			return null;
		}
		return PsMetagameData.m_planetUnlocks[_planetIdentifier];
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00064EE0 File Offset: 0x000632E0
	public static void CreateLeagueData()
	{
		PsMetagameData.m_leagueData = new List<PsLeagueData>();
		PsMetagameData.m_leagueData.Add(new PsLeagueData(1, StringID.LEAGUE1, 0, "menu_league_banner_1", StringID.LEAGUE1_A));
		PsMetagameData.m_leagueData.Add(new PsLeagueData(2, StringID.LEAGUE2, 120, "menu_league_banner_2", StringID.LEAGUE2_A));
		PsMetagameData.m_leagueData.Add(new PsLeagueData(3, StringID.LEAGUE3, 400, "menu_league_banner_3", StringID.LEAGUE3_A));
		PsMetagameData.m_leagueData.Add(new PsLeagueData(4, StringID.LEAGUE4, 700, "menu_league_banner_4", StringID.LEAGUE4_A));
		PsMetagameData.m_leagueData.Add(new PsLeagueData(5, StringID.LEAGUE5, 1100, "menu_league_banner_5", StringID.LEAGUE5_A));
		PsMetagameData.m_leagueData.Add(new PsLeagueData(6, StringID.LEAGUE6, 1500, "menu_league_banner_6", StringID.LEAGUE6_A));
		PsMetagameData.m_leagueData.Add(new PsLeagueData(7, StringID.LEAGUE7, 2000, "menu_league_banner_7", StringID.LEAGUE7_A));
		PsMetagameData.m_leagueData.Add(new PsLeagueData(8, StringID.LEAGUE8, 3000, "menu_league_banner_8", StringID.LEAGUE8_A));
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00065010 File Offset: 0x00063410
	public static int GetLeagueIndex(int _trophyCount)
	{
		for (int i = PsMetagameData.m_leagueData.Count - 1; i > -1; i--)
		{
			if (PsMetagameData.m_leagueData[i].m_trophyLimit <= _trophyCount)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00065054 File Offset: 0x00063454
	public static int GetCurrentLeagueIndex()
	{
		for (int i = PsMetagameData.m_leagueData.Count - 1; i > -1; i--)
		{
			if (PsMetagameData.m_leagueData[i].m_trophyLimit <= PsMetagameManager.m_playerStats.trophies)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x000650A0 File Offset: 0x000634A0
	public static int GetHighestLeagueIndex()
	{
		return PsMetagameData.m_leagueData.Count - 1;
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x000650AE File Offset: 0x000634AE
	public static PsLeagueData GetCurrentLeague()
	{
		return PsMetagameData.m_leagueData[PsMetagameData.GetCurrentLeagueIndex()];
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x000650BF File Offset: 0x000634BF
	public static PsLeagueData GetLeague(int _leagueIndex)
	{
		return PsMetagameData.m_leagueData[_leagueIndex];
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x000650CC File Offset: 0x000634CC
	public static PsLeagueData GetNextLeague()
	{
		int currentLeagueIndex = PsMetagameData.GetCurrentLeagueIndex();
		if (currentLeagueIndex == PsMetagameData.m_leagueData.Count - 1)
		{
			return null;
		}
		return PsMetagameData.m_leagueData[currentLeagueIndex + 1];
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00065100 File Offset: 0x00063500
	public static PsLevelTemplate AddLevelTemplate(PsMetagameDataHelper _helper)
	{
		PsLevelTemplate psLevelTemplate = PsMetagameData.GetLevelTemplateByIdentifier(_helper.m_metagameNodeData.m_dataIdentifier);
		if (psLevelTemplate != null)
		{
			return psLevelTemplate;
		}
		psLevelTemplate = new PsLevelTemplate();
		PsMetagameData.m_levelTemplates.Add(psLevelTemplate);
		psLevelTemplate.m_identifier = _helper.m_metagameNodeData.m_dataIdentifier;
		psLevelTemplate.m_gameArea = _helper.m_metagameNodeData.m_levelTemplateDomeSize;
		psLevelTemplate.m_gameMode = _helper.m_metagameNodeData.m_levelGameMode;
		psLevelTemplate.m_limitedItems = _helper.m_metagameNodeData.m_levelItems;
		psLevelTemplate.m_playerUnit = _helper.m_metagameNodeData.m_levelPlayerUnit;
		psLevelTemplate.m_templateMinigames = _helper.m_metagameNodeData.m_levelTemplateMinigames;
		for (int i = 0; i < _helper.m_inputs.Count; i++)
		{
			if (_helper.m_inputs[i].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Dialogue)
			{
				if (psLevelTemplate.m_dialogueIdentifiers == null)
				{
					psLevelTemplate.m_dialogueIdentifiers = new List<string>();
				}
				PsDialogue psDialogue = PsMetagameData.AddDialogue(_helper.m_inputs[i]);
				psLevelTemplate.m_dialogueIdentifiers.Add(psDialogue.m_identifier);
				Debug.LogWarning(psLevelTemplate.m_identifier + " has dialogue: " + psDialogue.m_identifier);
			}
		}
		return psLevelTemplate;
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0006522C File Offset: 0x0006362C
	public static PsDialogue AddDialogue(PsMetagameDataHelper _helper)
	{
		PsDialogue psDialogue = PsMetagameData.GetDialogueByIdentifier(_helper.m_metagameNodeData.m_dataIdentifier);
		if (psDialogue != null)
		{
			return psDialogue;
		}
		psDialogue = new PsDialogue(_helper.m_metagameNodeData.m_dataIdentifier, _helper.m_metagameNodeData.m_dialogueTrigger);
		PsMetagameData.m_dialogues.Add(psDialogue);
		PsMetagameDataHelper psMetagameDataHelper2;
		for (PsMetagameDataHelper psMetagameDataHelper = _helper; psMetagameDataHelper != null; psMetagameDataHelper = psMetagameDataHelper2)
		{
			PsDialogueStep psDialogueStep = new PsDialogueStep();
			psDialogue.m_steps.Add(psDialogueStep);
			psDialogueStep.m_character = psMetagameDataHelper.m_metagameNodeData.m_dialogueCharacter;
			psDialogueStep.m_characterPosition = psMetagameDataHelper.m_metagameNodeData.m_dialogueCharacterPosition;
			psDialogueStep.m_charactertext = psMetagameDataHelper.m_metagameNodeData.m_dialogueText;
			psDialogueStep.m_proceedText = psMetagameDataHelper.m_metagameNodeData.m_dialogueProceed;
			psDialogueStep.m_characterTextLocalized = psMetagameDataHelper.m_metagameNodeData.m_dialogueTextLocalized;
			try
			{
				psDialogueStep.m_proceedTextLocalized = (StringID)Enum.Parse(typeof(StringID), psMetagameDataHelper.m_metagameNodeData.m_dialogueTextLocalized.ToString() + "_BUTTON");
			}
			catch
			{
				psDialogueStep.m_proceedTextLocalized = StringID.EMPTY;
			}
			psDialogueStep.m_cancelText = psMetagameDataHelper.m_metagameNodeData.m_dialogueCancel;
			psMetagameDataHelper2 = null;
			for (int i = 0; i < psMetagameDataHelper.m_outputs.Count; i++)
			{
				if (psMetagameDataHelper.m_outputs[i].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Dialogue)
				{
					psMetagameDataHelper2 = psMetagameDataHelper.m_outputs[i];
					break;
				}
			}
		}
		return psDialogue;
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x000653B4 File Offset: 0x000637B4
	public static void AddUnlocks(PsMetagameDataHelper _helper, string _planetIdentifier)
	{
		if (!PsMetagameData.m_planetUnlocks.ContainsKey(_planetIdentifier))
		{
			PsMetagameData.m_planetUnlocks.Add(_planetIdentifier, new List<PsUnlock>());
		}
		PsUnlock psUnlock = new PsUnlock(_helper.m_metagameNodeData.m_dataIdentifier);
		PsMetagameData.m_planetUnlocks[_planetIdentifier].Add(psUnlock);
		for (int i = 0; i < _helper.m_inputs.Count; i++)
		{
			if (_helper.m_inputs[i].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Unlockable || _helper.m_inputs[i].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableSimple || _helper.m_inputs[i].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableUpgradeable)
			{
				string dataIdentifier = _helper.m_inputs[i].m_metagameNodeData.m_dataIdentifier;
				PsUnlockable unlockableByIdentifier = PsMetagameData.GetUnlockableByIdentifier(dataIdentifier);
				if (unlockableByIdentifier != null)
				{
					psUnlock.m_unlockables.Add(unlockableByIdentifier);
				}
				else
				{
					Debug.LogWarning("couldn't find requested unlockable item: " + dataIdentifier);
				}
			}
		}
		PsMetagameDataHelper psMetagameDataHelper = null;
		for (int j = 0; j < _helper.m_outputs.Count; j++)
		{
			if (_helper.m_outputs[j].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Unlock)
			{
				psMetagameDataHelper = _helper.m_outputs[j];
			}
			else if (_helper.m_outputs[j].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Level || _helper.m_outputs[j].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.EditorPuzzle || _helper.m_outputs[j].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Signal || _helper.m_outputs[j].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Block)
			{
				PsMetagameDataHelper psMetagameDataHelper3;
				for (PsMetagameDataHelper psMetagameDataHelper2 = _helper.m_outputs[j]; psMetagameDataHelper2 != null; psMetagameDataHelper2 = psMetagameDataHelper3)
				{
					psUnlock.m_levels.Add(psMetagameDataHelper2.m_metagameNodeData);
					for (int k = 0; k < psMetagameDataHelper2.m_inputs.Count; k++)
					{
						if (psMetagameDataHelper2.m_inputs[k].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Dialogue)
						{
							PsDialogue psDialogue = PsMetagameData.AddDialogue(psMetagameDataHelper2.m_inputs[k]);
							psMetagameDataHelper2.m_metagameNodeData.m_dialogues.Add(psDialogue);
						}
					}
					psMetagameDataHelper3 = null;
					for (int l = 0; l < psMetagameDataHelper2.m_outputs.Count; l++)
					{
						if (psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Level || psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.EditorPuzzle || psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.VersusRace || psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.LevelTemplate || psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Block || psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Signal)
						{
							psMetagameDataHelper3 = psMetagameDataHelper2.m_outputs[l];
						}
						else if (psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.SidePath)
						{
							psMetagameDataHelper2.m_metagameNodeData.m_sidePathLevels = new List<MetagameNodeData>();
							PsMetagameDataHelper psMetagameDataHelper4 = psMetagameDataHelper2.m_outputs[l];
							if (psMetagameDataHelper4.m_outputs.Count > 0)
							{
								psMetagameDataHelper4 = psMetagameDataHelper4.m_outputs[0];
							}
							while (psMetagameDataHelper4 != null)
							{
								psMetagameDataHelper2.m_metagameNodeData.m_sidePathLevels.Add(psMetagameDataHelper4.m_metagameNodeData);
								for (int m = 0; m < psMetagameDataHelper4.m_inputs.Count; m++)
								{
									if (psMetagameDataHelper4.m_inputs[m].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Dialogue)
									{
										PsDialogue psDialogue2 = PsMetagameData.AddDialogue(psMetagameDataHelper4.m_inputs[m]);
										Debug.Log(psDialogue2.m_identifier, null);
										psMetagameDataHelper4.m_metagameNodeData.m_dialogues.Add(psDialogue2);
									}
								}
								if (psMetagameDataHelper4.m_outputs.Count > 0)
								{
									psMetagameDataHelper4 = psMetagameDataHelper4.m_outputs[0];
								}
								else
								{
									psMetagameDataHelper4 = null;
								}
							}
						}
						else if (psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Unlock)
						{
							psMetagameDataHelper2.m_metagameNodeData.m_blockWillUnlock = psMetagameDataHelper2.m_outputs[l].m_metagameNodeData.m_dataIdentifier;
						}
					}
				}
			}
		}
		if (psMetagameDataHelper != null)
		{
			PsMetagameData.AddUnlocks(psMetagameDataHelper, _planetIdentifier);
		}
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00065884 File Offset: 0x00063C84
	public static void Unlock(string _unlockIdentifier, string _planetIdentifier)
	{
		if (!PsMetagameData.m_planetUnlocks.ContainsKey(_planetIdentifier))
		{
			Debug.LogError("[unlocking] No planetidentifier: " + _planetIdentifier + ", Unlockidentifier: " + _unlockIdentifier);
			return;
		}
		List<PsUnlock> list = PsMetagameData.m_planetUnlocks[_planetIdentifier];
		Debug.Log(_planetIdentifier + ": unlocks COUNT: " + list.Count, null);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].m_name == _unlockIdentifier)
			{
				list[i].m_unlocked = true;
				Debug.LogWarning("UNLOCK: " + _unlockIdentifier);
				List<PsUnlockable> unlockables = list[i].m_unlockables;
				for (int j = 0; j < unlockables.Count; j++)
				{
					Debug.LogWarning("UNLOCKABLE: " + unlockables[j].m_name);
					unlockables[j].m_unlocked = true;
				}
				return;
			}
		}
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00065978 File Offset: 0x00063D78
	public static void LockAll()
	{
		foreach (KeyValuePair<string, List<PsUnlock>> keyValuePair in PsMetagameData.m_planetUnlocks)
		{
			PsMetagameData.LockPlanet(keyValuePair.Value);
		}
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x000659D8 File Offset: 0x00063DD8
	public static void LockPlanet(List<PsUnlock> _unlocks)
	{
		for (int i = 0; i < _unlocks.Count; i++)
		{
			_unlocks[i].m_unlocked = false;
			List<PsUnlockable> unlockables = _unlocks[i].m_unlockables;
			for (int j = 0; j < unlockables.Count; j++)
			{
				unlockables[j].m_unlocked = false;
			}
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00065A3C File Offset: 0x00063E3C
	public static void TemporarilyUnlockUnlockable(string _unlockIdentifier)
	{
		PsUnlockable unlockableByIdentifier = PsMetagameData.GetUnlockableByIdentifier(_unlockIdentifier);
		if (unlockableByIdentifier != null)
		{
			if (PsState.m_activeGameLoop != null)
			{
				PsState.m_activeGameLoop.m_temporaryUnlock = _unlockIdentifier;
			}
			unlockableByIdentifier.m_unlocked = true;
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00065A74 File Offset: 0x00063E74
	public static void Unlock(PsUnlock _unlock)
	{
		_unlock.m_unlocked = true;
		List<PsUnlockable> unlockables = _unlock.m_unlockables;
		for (int i = 0; i < unlockables.Count; i++)
		{
			unlockables[i].m_unlocked = true;
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00065AB4 File Offset: 0x00063EB4
	public static PsUnlock GetNextUnlock(string _planetIdentifier, bool _unlock = true, string _lastIdentifier = null)
	{
		if (!PsMetagameData.m_planetUnlocks.ContainsKey(_planetIdentifier))
		{
			Debug.LogError("No planet with identifier: " + _planetIdentifier);
			return null;
		}
		bool flag = false;
		List<PsUnlock> list = PsMetagameData.m_planetUnlocks[_planetIdentifier];
		if (!string.IsNullOrEmpty(_lastIdentifier) && list[list.Count - 1].m_name == _lastIdentifier)
		{
			flag = true;
		}
		for (int i = 0; i < list.Count; i++)
		{
			PsUnlock psUnlock = list[i];
			if (!psUnlock.m_unlocked)
			{
				if (_unlock)
				{
					PsMetagameData.Unlock(psUnlock);
				}
				return psUnlock;
			}
		}
		if (list.Count <= 0)
		{
			PsUnlock psUnlock2 = new PsUnlock("error");
			MetagameNodeData metagameNodeData = new MetagameNodeData(string.Empty);
			metagameNodeData.m_nodeDataType = MetagameNodeDataType.Level;
			psUnlock2.m_levels.Add(metagameNodeData);
			MetagameNodeData metagameNodeData2 = new MetagameNodeData(string.Empty);
			metagameNodeData2.m_nodeDataType = MetagameNodeDataType.Block;
			psUnlock2.m_levels.Add(metagameNodeData2);
			return psUnlock2;
		}
		if (flag && list.Count > 1)
		{
			return list[list.Count - 2];
		}
		return list[list.Count - 1];
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00065BE4 File Offset: 0x00063FE4
	public static PsUnlock GetCurrentUnlock(string _planetIdentifier)
	{
		if (!PsMetagameData.m_planetUnlocks.ContainsKey(_planetIdentifier))
		{
			Debug.LogError("No planet with identifier: " + _planetIdentifier);
			return null;
		}
		List<PsUnlock> list = PsMetagameData.m_planetUnlocks[_planetIdentifier];
		int num = 0;
		for (int i = 0; i < list.Count; i++)
		{
			PsUnlock psUnlock = list[i];
			if (!psUnlock.m_unlocked)
			{
				return list[num];
			}
			num = i;
		}
		if (list.Count > 0)
		{
			return list[list.Count - 1];
		}
		PsUnlock psUnlock2 = new PsUnlock("error");
		MetagameNodeData metagameNodeData = new MetagameNodeData(string.Empty);
		metagameNodeData.m_nodeDataType = MetagameNodeDataType.Level;
		psUnlock2.m_levels.Add(metagameNodeData);
		MetagameNodeData metagameNodeData2 = new MetagameNodeData(string.Empty);
		metagameNodeData2.m_nodeDataType = MetagameNodeDataType.Block;
		psUnlock2.m_levels.Add(metagameNodeData2);
		return psUnlock2;
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00065CC4 File Offset: 0x000640C4
	public static PsUnlock GetNextUnlockFromIdentifier(string _unlockIdentifier, string _planetIdentifier)
	{
		if (!PsMetagameData.m_planetUnlocks.ContainsKey(_planetIdentifier))
		{
			Debug.LogError("No planet with identifier: " + _planetIdentifier);
			return null;
		}
		List<PsUnlock> list = PsMetagameData.m_planetUnlocks[_planetIdentifier];
		for (int i = 0; i < list.Count; i++)
		{
			PsUnlock psUnlock = list[i];
			if (psUnlock.m_name == _unlockIdentifier && i + 1 < list.Count)
			{
				return list[i + 1];
			}
		}
		if (list.Count > 0)
		{
			return list[list.Count - 1];
		}
		PsUnlock psUnlock2 = new PsUnlock("error");
		MetagameNodeData metagameNodeData = new MetagameNodeData(string.Empty);
		metagameNodeData.m_nodeDataType = MetagameNodeDataType.Level;
		psUnlock2.m_levels.Add(metagameNodeData);
		MetagameNodeData metagameNodeData2 = new MetagameNodeData(string.Empty);
		metagameNodeData2.m_nodeDataType = MetagameNodeDataType.Block;
		psUnlock2.m_levels.Add(metagameNodeData2);
		return psUnlock2;
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00065DB0 File Offset: 0x000641B0
	public static void AddCategory(PsMetagameDataHelper _helper, PsUnlockableCategory _container = null)
	{
		if (_helper.m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableCategory)
		{
			PsUnlockableCategory psUnlockableCategory = new PsUnlockableCategory(_helper.m_metagameNodeData.m_unlockableName);
			psUnlockableCategory.m_description = _helper.m_metagameNodeData.m_unlockableDescription;
			psUnlockableCategory.m_type = _helper.m_metagameNodeData.m_unlockableType;
			psUnlockableCategory.m_name = _helper.m_metagameNodeData.m_unlockableName;
			if (_container != null)
			{
				psUnlockableCategory.m_container = _container;
				_container.m_items.Add(psUnlockableCategory);
			}
			if (_helper.m_metagameNodeData.m_unlockableType == PsUnlockableType.Unit)
			{
				PsMetagameData.m_units.Add(psUnlockableCategory);
			}
			else if (_helper.m_metagameNodeData.m_unlockableType == PsUnlockableType.PlayerUnit)
			{
				PsMetagameData.m_playerUnits.Add(psUnlockableCategory);
			}
			else if (_helper.m_metagameNodeData.m_unlockableType == PsUnlockableType.GameArea)
			{
				PsMetagameData.m_gameAreas.Add(psUnlockableCategory);
			}
			else if (_helper.m_metagameNodeData.m_unlockableType == PsUnlockableType.GameEnvironment)
			{
				PsMetagameData.m_gameEnvironments.Add(psUnlockableCategory);
			}
			else if (_helper.m_metagameNodeData.m_unlockableType == PsUnlockableType.GameMode)
			{
				PsMetagameData.m_gameModes.Add(psUnlockableCategory);
			}
			else if (_helper.m_metagameNodeData.m_unlockableType == PsUnlockableType.GameMaterial)
			{
				PsMetagameData.m_gameMaterials.Add(psUnlockableCategory);
			}
			else if (_helper.m_metagameNodeData.m_unlockableType == PsUnlockableType.Menu)
			{
				PsMetagameData.m_menus.Add(psUnlockableCategory);
			}
			for (int i = 0; i < _helper.m_outputs.Count; i++)
			{
				PsMetagameData.AddCategory(_helper.m_outputs[i], psUnlockableCategory);
			}
		}
		else if (_helper.m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.Unlockable)
		{
			PsEditorItem psEditorItem = new PsEditorItem(_helper.m_metagameNodeData.m_unlockableName);
			psEditorItem.m_description = _helper.m_metagameNodeData.m_unlockableDescription;
			psEditorItem.m_iconImage = _helper.m_metagameNodeData.m_unlockableIcon;
			psEditorItem.m_identifier = _helper.m_metagameNodeData.m_dataIdentifier;
			psEditorItem.m_graphNodeClassName = _helper.m_metagameNodeData.m_unlockableGraphNodeClass;
			psEditorItem.m_maxAmount = _helper.m_metagameNodeData.m_unlockableMaxCount;
			psEditorItem.m_complexity = _helper.m_metagameNodeData.m_unlockableComplexity;
			psEditorItem.m_itemLevel = _helper.m_metagameNodeData.m_unlockableItemLevel;
			psEditorItem.m_gachaProbability = _helper.m_metagameNodeData.m_unlockableGachaProbability;
			psEditorItem.m_rarity = _helper.m_metagameNodeData.m_unlockableRarity;
			psEditorItem.m_currency = _helper.m_metagameNodeData.m_unlockableCurrency;
			psEditorItem.m_price = _helper.m_metagameNodeData.m_unlockablePrice;
			PsMetagameData.AddLevelTemplates(_helper, psEditorItem);
			psEditorItem.m_container = _container;
			_container.m_items.Add(psEditorItem);
		}
		else if (_helper.m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableSimple)
		{
			PsEditorItem psEditorItem2 = new PsEditorItem(_helper.m_metagameNodeData.m_unlockableName);
			psEditorItem2.m_description = _helper.m_metagameNodeData.m_unlockableDescription;
			psEditorItem2.m_iconImage = _helper.m_metagameNodeData.m_unlockableIcon;
			psEditorItem2.m_identifier = _helper.m_metagameNodeData.m_dataIdentifier;
			psEditorItem2.m_itemLevel = _helper.m_metagameNodeData.m_unlockableItemLevel;
			PsMetagameData.AddLevelTemplates(_helper, psEditorItem2);
			psEditorItem2.m_container = _container;
			_container.m_items.Add(psEditorItem2);
		}
		else if (_helper.m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.UnlockableUpgradeable)
		{
			PsUpgradeableEditorItem psUpgradeableEditorItem = new PsUpgradeableEditorItem(_helper.m_metagameNodeData.m_unlockableName);
			psUpgradeableEditorItem.m_description = _helper.m_metagameNodeData.m_unlockableDescription;
			psUpgradeableEditorItem.m_iconImage = _helper.m_metagameNodeData.m_unlockableIcon;
			psUpgradeableEditorItem.m_identifier = _helper.m_metagameNodeData.m_dataIdentifier;
			psUpgradeableEditorItem.m_graphNodeClassName = _helper.m_metagameNodeData.m_unlockableGraphNodeClass;
			psUpgradeableEditorItem.m_maxAmount = _helper.m_metagameNodeData.m_unlockableMaxCount;
			psUpgradeableEditorItem.m_complexity = _helper.m_metagameNodeData.m_unlockableComplexity;
			psUpgradeableEditorItem.m_itemLevel = _helper.m_metagameNodeData.m_unlockableItemLevel;
			psUpgradeableEditorItem.m_upgradeValues = _helper.m_metagameNodeData.m_unlockableUpgradeValues;
			psUpgradeableEditorItem.m_upgradePrices = _helper.m_metagameNodeData.m_unlockableUpgradePrices;
			psUpgradeableEditorItem.m_upgradeSteps = _helper.m_metagameNodeData.m_unlockableUpgradeSteps;
			psUpgradeableEditorItem.m_rentName = _helper.m_metagameNodeData.m_unlockableRentName;
			psUpgradeableEditorItem.m_rentButton = _helper.m_metagameNodeData.m_unlockableRentButton;
			PsMetagameData.AddLevelTemplates(_helper, psUpgradeableEditorItem);
			psUpgradeableEditorItem.m_container = _container;
			_container.m_items.Add(psUpgradeableEditorItem);
		}
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x000661E0 File Offset: 0x000645E0
	public static void AddLevelTemplates(PsMetagameDataHelper _helper, PsUnlockable _item)
	{
		for (int i = 0; i < _helper.m_inputs.Count; i++)
		{
			if (_helper.m_inputs[i].m_metagameNodeData.m_nodeDataType == MetagameNodeDataType.LevelTemplate)
			{
				_item.m_tutorialIdentifier = _helper.m_inputs[i].m_metagameNodeData.m_dataIdentifier;
				Debug.LogWarning(_item.m_identifier + " has tutorial: " + _item.m_tutorialIdentifier);
			}
		}
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0006625C File Offset: 0x0006465C
	public static PsUnlock GetUnlockByIdentifier(string _identifier, string _planetIdentifier)
	{
		if (!PsMetagameData.m_planetUnlocks.ContainsKey(_planetIdentifier))
		{
			Debug.LogError("Metagamedata planet: " + _planetIdentifier + ": does not contain unlock: " + _identifier);
			return null;
		}
		List<PsUnlock> list = PsMetagameData.m_planetUnlocks[_planetIdentifier];
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].m_name == _identifier)
			{
				return list[i];
			}
		}
		return null;
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x000662D4 File Offset: 0x000646D4
	public static PsDialogue GetDialogueByIdentifier(string _identifier)
	{
		foreach (PsDialogue psDialogue in PsMetagameData.m_dialogues)
		{
			if (_identifier.Equals(psDialogue.m_identifier))
			{
				return psDialogue;
			}
		}
		return null;
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00066344 File Offset: 0x00064744
	public static PsLevelTemplate GetLevelTemplateByIdentifier(string _identifier)
	{
		foreach (PsLevelTemplate psLevelTemplate in PsMetagameData.m_levelTemplates)
		{
			if (_identifier.Equals(psLevelTemplate.m_identifier))
			{
				return psLevelTemplate;
			}
		}
		return null;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x000663B4 File Offset: 0x000647B4
	public static PsEditorItem GetUnlockableByIdentifier(string _identifier)
	{
		foreach (PsUnlockableCategory psUnlockableCategory in PsMetagameData.m_playerUnits)
		{
			foreach (PsUnlockable psUnlockable in psUnlockableCategory.m_items)
			{
				PsUpgradeableEditorItem psUpgradeableEditorItem = (PsUpgradeableEditorItem)psUnlockable;
				if (_identifier.Equals(psUpgradeableEditorItem.m_identifier))
				{
					return psUpgradeableEditorItem;
				}
			}
		}
		foreach (PsUnlockableCategory psUnlockableCategory2 in PsMetagameData.m_units)
		{
			foreach (PsUnlockable psUnlockable2 in psUnlockableCategory2.m_items)
			{
				PsEditorItem psEditorItem = (PsEditorItem)psUnlockable2;
				if (_identifier.Equals(psEditorItem.m_identifier))
				{
					return psEditorItem;
				}
			}
		}
		foreach (PsUnlockableCategory psUnlockableCategory3 in PsMetagameData.m_gameAreas)
		{
			foreach (PsUnlockable psUnlockable3 in psUnlockableCategory3.m_items)
			{
				PsEditorItem psEditorItem2 = (PsEditorItem)psUnlockable3;
				if (_identifier.Equals(psEditorItem2.m_identifier))
				{
					return psEditorItem2;
				}
			}
		}
		foreach (PsUnlockableCategory psUnlockableCategory4 in PsMetagameData.m_gameEnvironments)
		{
			foreach (PsUnlockable psUnlockable4 in psUnlockableCategory4.m_items)
			{
				PsEditorItem psEditorItem3 = (PsEditorItem)psUnlockable4;
				if (_identifier.Equals(psEditorItem3.m_identifier))
				{
					return psEditorItem3;
				}
			}
		}
		foreach (PsUnlockableCategory psUnlockableCategory5 in PsMetagameData.m_gameModes)
		{
			foreach (PsUnlockable psUnlockable5 in psUnlockableCategory5.m_items)
			{
				PsEditorItem psEditorItem4 = (PsEditorItem)psUnlockable5;
				if (_identifier.Equals(psEditorItem4.m_identifier))
				{
					return psEditorItem4;
				}
			}
		}
		foreach (PsUnlockableCategory psUnlockableCategory6 in PsMetagameData.m_gameMaterials)
		{
			foreach (PsUnlockable psUnlockable6 in psUnlockableCategory6.m_items)
			{
				PsEditorItem psEditorItem5 = (PsEditorItem)psUnlockable6;
				if (_identifier.Equals(psEditorItem5.m_identifier))
				{
					return psEditorItem5;
				}
			}
		}
		foreach (PsUnlockableCategory psUnlockableCategory7 in PsMetagameData.m_menus)
		{
			foreach (PsUnlockable psUnlockable7 in psUnlockableCategory7.m_items)
			{
				PsEditorItem psEditorItem6 = (PsEditorItem)psUnlockable7;
				if (_identifier.Equals(psEditorItem6.m_identifier))
				{
					return psEditorItem6;
				}
			}
		}
		return null;
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00066878 File Offset: 0x00064C78
	public static bool CanPlaceUnit(string _unitClassName)
	{
		if (Main.m_currentGame.m_currentScene.m_name != "EditorScene")
		{
			return true;
		}
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		if (minigame != null)
		{
			PsEditorItem unlockableByIdentifier = PsMetagameData.GetUnlockableByIdentifier(_unitClassName);
			if (unlockableByIdentifier == null)
			{
				return true;
			}
			Debug.Log(string.Concat(new object[] { "Current complexity: ", minigame.m_complexity, "/", minigame.m_maxComplexity }), null);
			if (minigame.m_complexity + unlockableByIdentifier.m_complexity > minigame.m_maxComplexity)
			{
				PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(UIEditorWarningPopup), null, null, null, false, true, InitialPage.Center, false, false, false);
				return false;
			}
			List<string> list = new List<string>();
			list.Add("CCPlatform1");
			list.Add("CCPlatform2");
			list.Add("CCPlatform3");
			list.Add("CCPlatform4");
			list.Add("CCPlatform5");
			List<string> list2 = list;
			if (list2.Contains(_unitClassName) && PsState.m_activeGameLoop.m_minigameMetaData.gameMode != PsGameMode.StarCollect)
			{
				string text = PsStrings.Get(StringID.WORD_WARNING);
				string text2 = PsStrings.Get(StringID.EDITOR_CC_ONLY_ADVENTURE);
				PsUIBasePopup psUIBasePopup2 = new PsUIBasePopup(typeof(UIEditorWarningPopup), null, null, null, false, true, InitialPage.Center, false, false, false);
				(psUIBasePopup2.m_mainContent as UIEditorWarningPopup).m_headerText.SetText(text);
				(psUIBasePopup2.m_mainContent as UIEditorWarningPopup).m_textBox.SetText(text2);
				psUIBasePopup2.Update();
				return false;
			}
			if (list2.Contains(_unitClassName))
			{
				foreach (string text3 in list2)
				{
					if (minigame.m_itemCount.ContainsKey(text3))
					{
						string text4 = PsStrings.Get(StringID.WORD_WARNING);
						string text5 = PsStrings.Get(StringID.EDITOR_CC_ONLY_ONE);
						PsUIBasePopup psUIBasePopup3 = new PsUIBasePopup(typeof(UIEditorWarningPopup), null, null, null, false, true, InitialPage.Center, false, false, false);
						(psUIBasePopup3.m_mainContent as UIEditorWarningPopup).m_headerText.SetText(text4);
						(psUIBasePopup3.m_mainContent as UIEditorWarningPopup).m_textBox.SetText(text5);
						psUIBasePopup3.Update();
						return false;
					}
				}
			}
			if (!minigame.m_itemCount.ContainsKey(_unitClassName))
			{
				return true;
			}
			if (unlockableByIdentifier != null)
			{
				if (unlockableByIdentifier.m_maxAmount > minigame.m_itemCount[_unitClassName])
				{
					return true;
				}
				string text6 = PsStrings.Get(StringID.EDITOR_PROMPT_SINGLE_ITEM_LIMIT_REACHED);
				text6 = text6.Replace("%1", PsStrings.Get(unlockableByIdentifier.m_name));
				text6 = text6.Replace("%2", unlockableByIdentifier.m_maxAmount.ToString());
				PsUIBasePopup psUIBasePopup4 = new PsUIBasePopup(typeof(UIEditorWarningPopup), null, null, null, false, true, InitialPage.Center, false, false, false);
				(psUIBasePopup4.m_mainContent as UIEditorWarningPopup).m_textBox.SetText(text6);
				psUIBasePopup4.Update();
				return false;
			}
		}
		return false;
	}

	// Token: 0x040008EF RID: 2287
	public static List<PsUnlockableCategory> m_units;

	// Token: 0x040008F0 RID: 2288
	public static List<PsUnlockableCategory> m_playerUnits;

	// Token: 0x040008F1 RID: 2289
	public static List<PsUnlockableCategory> m_gameModes;

	// Token: 0x040008F2 RID: 2290
	public static List<PsUnlockableCategory> m_gameAreas;

	// Token: 0x040008F3 RID: 2291
	public static List<PsUnlockableCategory> m_gameEnvironments;

	// Token: 0x040008F4 RID: 2292
	public static List<PsUnlockableCategory> m_gameMaterials;

	// Token: 0x040008F5 RID: 2293
	public static List<PsUnlockableCategory> m_menus;

	// Token: 0x040008F6 RID: 2294
	public static List<PsDialogue> m_dialogues;

	// Token: 0x040008F7 RID: 2295
	public static Dictionary<string, List<PsUnlock>> m_planetUnlocks;

	// Token: 0x040008F8 RID: 2296
	public static List<PsLevelTemplate> m_levelTemplates;

	// Token: 0x040008F9 RID: 2297
	public static List<string> m_unlockedTutorials;

	// Token: 0x040008FA RID: 2298
	public static List<PsLeagueData> m_leagueData;
}
