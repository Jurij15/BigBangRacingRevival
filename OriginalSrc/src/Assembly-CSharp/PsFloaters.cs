using System;
using System.Collections.Generic;

// Token: 0x02000129 RID: 297
public static class PsFloaters
{
	// Token: 0x060008F7 RID: 2295 RVA: 0x00060835 File Offset: 0x0005EC35
	public static void Initialize()
	{
		if (PsFloaters.sharedFloatingPath == null)
		{
			PsFloaters.sharedFloatingPath = new PsPlanetPath("FloatingPath", "Shared", PsPlanetPathType.floating);
		}
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00060858 File Offset: 0x0005EC58
	public static void AddFloaters()
	{
		PsFloaters.Initialize();
		PsMinigameContext[] array = new PsMinigameContext[] { PsMinigameContext.Fresh };
		List<PsGameLoop> list = new List<PsGameLoop>();
		for (int i = 0; i < PsFloaters.sharedFloatingPath.m_nodeInfos.Count; i++)
		{
			PsTimedEventLoop psTimedEventLoop = PsFloaters.sharedFloatingPath.m_nodeInfos[i] as PsTimedEventLoop;
			bool flag = PsFloaters.CreateFloatingNode(psTimedEventLoop, false, false, false);
			if (flag)
			{
				list.Add(psTimedEventLoop);
			}
		}
		foreach (PsGameLoop psGameLoop in list)
		{
			psGameLoop.m_path.m_nodeInfos.Remove(psGameLoop);
		}
		for (int j = 0; j < array.Length; j++)
		{
			if (!PsFloaters.HasContext(array[j], PsFloaters.sharedFloatingPath))
			{
				PsFloaters.CreateContext(array[j], PsFloaters.sharedFloatingPath);
			}
		}
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0006095C File Offset: 0x0005ED5C
	public static bool CreateFloatingNode(PsTimedEventLoop _loop, bool _setAsNew = false, bool _hasTimer = false, bool _removeFromPath = true)
	{
		PsFloatingPlanetNode psFloatingPlanetNode = null;
		if (PsPlanetManager.GetCurrentPlanet() != null && PsPlanetManager.GetCurrentPlanet().m_floatingNodeList != null)
		{
			_loop.m_levelNumber = PsPlanetManager.GetCurrentPlanet().m_floatingNodeList.Count + 1;
			psFloatingPlanetNode = PsFloaters.CreateExistingFloatingNode(_loop, _hasTimer);
			PsPlanetManager.GetCurrentPlanet().m_floatingNodeList.Add(psFloatingPlanetNode);
			if (_setAsNew)
			{
				psFloatingPlanetNode.SetNewNode();
			}
		}
		if (_loop.m_eventOver)
		{
			psFloatingPlanetNode.TimedDestructionOfNode(1.3125f);
			_loop.m_path.m_nodeInfos.Remove(_loop);
			return true;
		}
		return false;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x000609EC File Offset: 0x0005EDEC
	public static bool HasContext(PsMinigameContext _context, PsPlanetPath _floatingPath)
	{
		for (int i = 0; i < _floatingPath.m_nodeInfos.Count; i++)
		{
			if (_floatingPath.m_nodeInfos[i].m_context == _context)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00060A2F File Offset: 0x0005EE2F
	public static void CreateContext(PsMinigameContext _context, PsPlanetPath _floatingPath)
	{
		if (_context != PsMinigameContext.Fresh)
		{
			Debug.LogError("Not suitable context: " + _context);
		}
		else
		{
			PsFloaters.CreateFresh(_floatingPath);
		}
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00060A64 File Offset: 0x0005EE64
	public static void CreateFresh(PsPlanetPath _path)
	{
		int currentNodeId = PlanetTools.m_planetProgressionInfos[PlanetTools.PlanetTypes.AdventureOffroadCar.ToString()].GetMainPath().m_currentNodeId;
		if (PlayerPrefsX.GetFreshAndFree() && currentNodeId > 5 && PsMetagameManager.IsFreshLevelAvailable())
		{
			PsGameLoopFresh psGameLoopFresh = new PsGameLoopFresh(PsMinigameContext.Fresh, null, _path, 1, _path.m_nodeInfos.Count, 0, -1, -1, null, false, false);
			_path.m_nodeInfos.Add(psGameLoopFresh);
			if (Main.m_currentGame.m_currentScene.m_name == "MenuScene")
			{
				PsFloaters.CreateFloatingNode(psGameLoopFresh, true, false, true);
			}
			PsMetagameManager.FreshLevelIsCreated();
		}
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00060B04 File Offset: 0x0005EF04
	public static void CreateFreshAndFreePlanetForced()
	{
		PsPlanetPath psPlanetPath = PsFloaters.sharedFloatingPath;
		PlayerPrefsX.SetFreshAndFree(true);
		if (psPlanetPath == null)
		{
			return;
		}
		for (int i = 0; i < psPlanetPath.m_nodeInfos.Count; i++)
		{
			if (psPlanetPath.m_nodeInfos[i].m_context == PsMinigameContext.Fresh)
			{
				return;
			}
		}
		string text = PsState.m_fixedFreshMinigameId;
		if (psPlanetPath.m_planet.Contains("Motorcycle"))
		{
			text = PsState.m_fixedFreshMcId;
		}
		PsGameLoopFreshForced psGameLoopFreshForced = new PsGameLoopFreshForced(text, psPlanetPath);
		psPlanetPath.m_nodeInfos.Add(psGameLoopFreshForced);
		if (Main.m_currentGame.m_currentScene.m_name == "MenuScene")
		{
			PsFloaters.CreateFloatingNode(psGameLoopFreshForced, true, false, true);
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00060BB8 File Offset: 0x0005EFB8
	public static void CreateNewsDome(EventMessage _eventMessage, bool _hasTimer = false, bool _gift = false)
	{
		PsPlanetPath psPlanetPath = PsFloaters.sharedFloatingPath;
		if (psPlanetPath == null)
		{
			return;
		}
		for (int i = 0; i < psPlanetPath.m_nodeInfos.Count; i++)
		{
			if (psPlanetPath.m_nodeInfos[i].m_context == PsMinigameContext.News && !(psPlanetPath.m_nodeInfos[i] is PsGameLoopGift))
			{
				return;
			}
		}
		PsGameLoopNews psGameLoopNews = new PsGameLoopNews(_eventMessage, PsMinigameContext.News, psPlanetPath);
		psPlanetPath.m_nodeInfos.Add(psGameLoopNews);
		if (Main.m_currentGame.m_currentScene.m_name == "MenuScene")
		{
			PsFloaters.CreateFloatingNode(psGameLoopNews, true, _hasTimer, true);
		}
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00060C5C File Offset: 0x0005F05C
	public static void CreateGiftNode(EventMessage _eventMessage)
	{
		PsPlanetPath psPlanetPath = PsFloaters.sharedFloatingPath;
		if (psPlanetPath == null)
		{
			return;
		}
		for (int i = 0; i < psPlanetPath.m_nodeInfos.Count; i++)
		{
			if (psPlanetPath.m_nodeInfos[i].m_context == PsMinigameContext.News && psPlanetPath.m_nodeInfos[i] is PsGameLoopGift)
			{
				return;
			}
		}
		PsGameLoopNews psGameLoopNews = new PsGameLoopGift(_eventMessage, PsMinigameContext.News, psPlanetPath);
		psPlanetPath.m_nodeInfos.Add(psGameLoopNews);
		if (Main.m_currentGame.m_currentScene.m_name == "MenuScene")
		{
			PsFloaters.CreateFloatingNode(psGameLoopNews, true, false, true);
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00060D00 File Offset: 0x0005F100
	public static void CreateTournamentDome(EventMessage _eventMessage)
	{
		PsPlanetPath psPlanetPath = PsFloaters.sharedFloatingPath;
		if (psPlanetPath == null)
		{
			return;
		}
		for (int i = 0; i < psPlanetPath.m_nodeInfos.Count; i++)
		{
			if (psPlanetPath.m_nodeInfos[i].m_context == PsMinigameContext.News && psPlanetPath.m_nodeInfos[i] is PsGameLoopNews && (psPlanetPath.m_nodeInfos[i] as PsGameLoopNews).m_eventMessage.eventType == "Tournament")
			{
				return;
			}
		}
		PsGameLoopTournament psGameLoopTournament = new PsGameLoopTournament(_eventMessage, PsMinigameContext.News, psPlanetPath);
		if (Main.m_currentGame.m_currentScene.m_name == "MenuScene")
		{
			PsFloaters.CreateFloatingNode(psGameLoopTournament, true, false, true);
		}
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00060DC0 File Offset: 0x0005F1C0
	public static PsFloatingPlanetNode CreateExistingFloatingNode(PsTimedEventLoop _loop, bool _hasTimer = false)
	{
		if (_loop.m_context == PsMinigameContext.Fresh)
		{
			return new PsFloatingFreshAndFreeNode(_loop as PsGameLoopFresh, false);
		}
		if (_loop.m_context == PsMinigameContext.News)
		{
			if (_loop is PsGameLoopNews && (_loop as PsGameLoopNews).m_eventMessage != null && (_loop as PsGameLoopNews).m_eventMessage.messageId <= PsMetagameManager.m_giftEvents.lastClaimedGift && (_loop as PsGameLoopNews).m_eventMessage.giftContent != null)
			{
				_loop.m_eventOver = true;
			}
			return new PsFloatingNewsNode(_loop as PsGameLoopNews, false, _hasTimer);
		}
		Debug.LogError("Not suitable floating node context: " + _loop.m_context);
		return null;
	}

	// Token: 0x0400085D RID: 2141
	public static PsPlanetPath sharedFloatingPath;
}
