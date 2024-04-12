using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000128 RID: 296
public class PsFloaterManager
{
	// Token: 0x060008E7 RID: 2279 RVA: 0x00060181 File Offset: 0x0005E581
	public PsFloaterManager(PsPlanet _planet)
	{
		this.m_planet = _planet;
		this.m_floatingNodeList = new List<PsFloatingPlanetNode>();
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x0006019C File Offset: 0x0005E59C
	public void Update()
	{
		for (int i = this.m_floatingNodeList.Count - 1; i >= 0; i--)
		{
			this.m_floatingNodeList[i].Update();
		}
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x000601D8 File Offset: 0x0005E5D8
	public void ReplacePathWithServerPath(PsPlanetPath _floatingPathFromServer)
	{
		bool flag = true;
		if (PsState.m_activeGameLoop is PsTimedEventLoop)
		{
			flag = false;
		}
		if (this.m_floatingNodeList != null && this.m_floatingNodeList.Count > 0)
		{
			flag = false;
		}
		if (flag)
		{
			for (int i = 0; i < _floatingPathFromServer.m_nodeInfos.Count; i++)
			{
				if (_floatingPathFromServer.m_nodeInfos[i].m_levelNumber == -1)
				{
					_floatingPathFromServer.m_nodeInfos[i].m_levelNumber = _floatingPathFromServer.m_nodeInfos[i].m_nodeId;
				}
			}
			this.m_planet.m_planetData.m_floatingPath = _floatingPathFromServer;
		}
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00060284 File Offset: 0x0005E684
	public void LoadFloatingPath()
	{
		if (this.m_planet.m_planetData.m_floatingPath == null || this.m_floatingPath.m_nodeInfos.Count == 0)
		{
			Debug.LogWarning("Floatingpath was null");
			this.m_planet.m_planetData.m_floatingPath = new PsPlanetPath("FloatingPath", this.m_planet.m_planetData.m_planetIdentifier, PsPlanetPathType.floating);
			this.m_planet.m_planetData.m_floatingPath.m_overwrite = true;
			this.m_planet.m_planetData.m_floatingPath.m_lane = -2;
		}
		else
		{
			Debug.LogWarning("Floating path found");
			this.m_planet.m_planetData.m_floatingPath.m_lane = -2;
			this.m_planet.m_planetData.m_floatingPath.m_overwrite = true;
			this.UpdateFloatingPath(this.m_floatingPath);
		}
		this.m_floatersAlive = true;
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0006036C File Offset: 0x0005E76C
	public void UpdateFloatingPath(PsPlanetPath _path)
	{
		bool flag = false;
		List<PsGameLoop> list = new List<PsGameLoop>();
		for (int i = 0; i < this.m_floatingPath.m_nodeInfos.Count; i++)
		{
			PsTimedEventLoop psTimedEventLoop = this.m_floatingPath.m_nodeInfos[i] as PsTimedEventLoop;
			if (psTimedEventLoop.m_eventOver)
			{
				if (!psTimedEventLoop.m_domeDestroyed)
				{
					if (psTimedEventLoop.m_timeRunOut)
					{
						psTimedEventLoop.m_domeDestroyed = true;
					}
					else
					{
						this.CreateFloatingNodeAndDestroy(psTimedEventLoop);
						list.Add(psTimedEventLoop);
					}
					flag = true;
				}
			}
			else
			{
				psTimedEventLoop.m_levelNumber = this.m_floatingNodeList.Count + 1;
				PsFloatingPlanetNode psFloatingPlanetNode = this.CreateExistingFloatingNode(psTimedEventLoop);
				this.m_floatingNodeList.Add(psFloatingPlanetNode);
			}
		}
		foreach (PsGameLoop psGameLoop in list)
		{
			this.m_floatingPath.m_nodeInfos.Remove(psGameLoop);
		}
		if (flag)
		{
			Hashtable hashtable = ClientTools.GenerateProgressionFloatingPathJson(this.m_floatingPath);
			PsMetagameManager.SaveProgression(hashtable, this.m_floatingPath.m_planet, false);
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x000604A4 File Offset: 0x0005E8A4
	public void CreateFloatingNodeAndDestroy(PsTimedEventLoop _loop)
	{
		_loop.m_domeDestroyed = true;
		_loop.m_levelNumber = this.m_floatingNodeList.Count + 1;
		PsFloatingPlanetNode psFloatingPlanetNode = this.CreateExistingFloatingNode(_loop);
		this.m_floatingNodeList.Add(psFloatingPlanetNode);
		psFloatingPlanetNode.TimedDestructionOfNode(1.3125f);
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x000604EA File Offset: 0x0005E8EA
	public PsFloatingPlanetNode CreateExistingFloatingNode(PsTimedEventLoop _loop)
	{
		if (_loop.m_context == PsMinigameContext.Fresh)
		{
			return new PsFloatingFreshAndFreeNode(_loop as PsGameLoopFresh, false);
		}
		Debug.LogError("Not suitable floating node context: " + _loop.m_context);
		return null;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00060520 File Offset: 0x0005E920
	public void RearrangeFloaters(PsFloatingPlanetNode _deleteNode)
	{
		Debug.LogWarning("Arranging floatingpath");
		int num = this.m_floatingNodeList.IndexOf(_deleteNode) + 1;
		for (int i = num; i < this.m_floatingNodeList.Count; i++)
		{
			this.m_floatingNodeList[i].m_loop.m_levelNumber--;
			this.m_floatingNodeList[i].RearrangeNodePosition();
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00060594 File Offset: 0x0005E994
	public bool CreateFreshAndFreePlanet()
	{
		if (PsMetagameData.GetUnlockableByIdentifier("Rating").m_unlocked && PsMetagameManager.IsFreshLevelAvailable() && PsPlanetManager.m_timedEvents != null && PsPlanetManager.m_timedEvents.HasFreshAndFree())
		{
			PsMetagameManager.FreshLevelIsCreated();
			PsGameLoopFresh loop = this.CreateFreshAndFreePlanetFromMetadata(PsPlanetManager.m_timedEvents.freshAndFree[0]);
			this.m_floatingPath.m_nodeInfos.Add(loop);
			TimerS.AddComponent(this.m_planet.m_spaceEntity, "Floater spawn timer", 0f, 0.2f, false, delegate(TimerC _c)
			{
				TimerS.RemoveComponent(_c);
				if (this.m_planet.m_spaceEntity != null)
				{
					loop.m_levelNumber = this.m_floatingNodeList.Count + 1;
					PsFloatingPlanetNode psFloatingPlanetNode = new PsFloatingFreshAndFreeNode(loop, false);
					this.m_floatingNodeList.Add(psFloatingPlanetNode);
					psFloatingPlanetNode.SetNewNode();
				}
			});
			return true;
		}
		return false;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0006064C File Offset: 0x0005EA4C
	public PsGameLoopFresh CreateFreshAndFreePlanetFromMetadata(PsMinigameMetaData _metadata)
	{
		int num = PlayerPrefsX.GetClientConfig().freshFreeInterval * 60;
		return new PsGameLoopFresh(PsMinigameContext.Fresh, _metadata.id, this.m_floatingPath, this.m_floatingNodeList.Count + 1, this.m_floatingNodeList.Count + 1, 0, num, num, null, false, false);
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0006069C File Offset: 0x0005EA9C
	public void CreateFreshAndFreePlanetForced()
	{
		for (int i = 0; i < this.m_floatingPath.m_nodeInfos.Count; i++)
		{
			if (this.m_floatingPath.m_nodeInfos[i].m_context == PsMinigameContext.Fresh)
			{
				return;
			}
		}
		string fixedFreshMinigameId = PsState.m_fixedFreshMinigameId;
		int num = PlayerPrefsX.GetClientConfig().freshFreeInterval * 60;
		PsGameLoopFresh psGameLoopFresh = new PsGameLoopFresh(PsMinigameContext.Fresh, fixedFreshMinigameId, this.m_floatingPath, this.m_floatingNodeList.Count + 1, this.m_floatingNodeList.Count + 1, 0, num, num, null, false, false);
		this.m_floatingPath.m_nodeInfos.Add(psGameLoopFresh);
		PsFloatingPlanetNode psFloatingPlanetNode = new PsFloatingFreshAndFreeNode(psGameLoopFresh, true);
		this.m_floatingNodeList.Add(psFloatingPlanetNode);
		psFloatingPlanetNode.SetNewNode();
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00060757 File Offset: 0x0005EB57
	public void AddVersus()
	{
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00060759 File Offset: 0x0005EB59
	public void AddFreshAndFree()
	{
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0006075B File Offset: 0x0005EB5B
	public void AddDiamondChallenge()
	{
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0006075D File Offset: 0x0005EB5D
	public void Destroy()
	{
		this.ClearFloatingNodes();
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00060768 File Offset: 0x0005EB68
	public void ClearFloatingNodes()
	{
		if (this.m_floatingNodeList != null)
		{
			for (int i = this.m_floatingNodeList.Count - 1; i >= 0; i--)
			{
				this.m_floatingNodeList[i].Destroy();
			}
			this.m_floatingNodeList.Clear();
		}
		this.m_floatersAlive = false;
	}

	// Token: 0x04000859 RID: 2137
	public PsPlanet m_planet;

	// Token: 0x0400085A RID: 2138
	public PsPlanetPath m_floatingPath;

	// Token: 0x0400085B RID: 2139
	public List<PsFloatingPlanetNode> m_floatingNodeList;

	// Token: 0x0400085C RID: 2140
	public bool m_floatersAlive;
}
