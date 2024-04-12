using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// Token: 0x0200012E RID: 302
[Serializable]
public class PsPlanetPath : ISerializable
{
	// Token: 0x06000934 RID: 2356 RVA: 0x00063904 File Offset: 0x00061D04
	public PsPlanetPath(string _name, string _planet, PsPlanetPathType _type)
	{
		this.m_name = _name;
		this.m_planet = _planet;
		this.m_type = _type;
		this.m_nodeInfos = new List<PsGameLoop>();
		this.m_currentNodeId = 1;
		this.m_startNodeId = 0;
		this.m_lane = 0;
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00063944 File Offset: 0x00061D44
	public PsPlanetPath(SerializationInfo info, StreamingContext ctxt)
	{
		this.m_name = (string)info.GetValue("name", typeof(string));
		this.m_type = (PsPlanetPathType)info.GetValue("type", typeof(PsPlanetPathType));
		this.m_planet = (string)info.GetValue("planet", typeof(string));
		this.m_currentNodeId = (int)info.GetValue("currentNodeId", typeof(int));
		this.m_startNodeId = (int)info.GetValue("startNodeId", typeof(int));
		this.m_lane = (int)info.GetValue("lane", typeof(int));
		this.m_nodeInfos = new List<PsGameLoop>((PsGameLoop[])info.GetValue("nodeInfos", typeof(PsGameLoop[])));
		for (int i = 0; i < this.m_nodeInfos.Count; i++)
		{
			this.m_nodeInfos[i].m_path = this;
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00063A6A File Offset: 0x00061E6A
	public PsPlanetPathType GetPathType()
	{
		return this.m_type;
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00063A72 File Offset: 0x00061E72
	public PlanetProgressionInfo GetPlanetInfo()
	{
		if (!PlanetTools.m_planetProgressionInfos.ContainsKey(this.m_planet))
		{
			Debug.LogError("NO PLANET FOUND: " + this.m_planet);
			return null;
		}
		return PlanetTools.m_planetProgressionInfos[this.m_planet];
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00063AB0 File Offset: 0x00061EB0
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("name", this.m_name);
		info.AddValue("planet", this.m_planet);
		info.AddValue("type", this.GetPathType());
		info.AddValue("currentNodeId", this.m_currentNodeId);
		info.AddValue("nodeInfos", this.m_nodeInfos.ToArray());
		info.AddValue("startNodeId", this.m_startNodeId);
		info.AddValue("lane", this.m_lane);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00063B40 File Offset: 0x00061F40
	public PsPlanetPath DeepCopy()
	{
		PsPlanetPath psPlanetPath2;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			PsPlanetPath psPlanetPath = (PsPlanetPath)binaryFormatter.Deserialize(memoryStream);
			psPlanetPath2 = psPlanetPath;
		}
		return psPlanetPath2;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00063B9C File Offset: 0x00061F9C
	public PsGameLoop GetNodeInfo(int _nodeId)
	{
		if (_nodeId > 0 && this.m_nodeInfos.Count >= _nodeId)
		{
			return this.m_nodeInfos[_nodeId - 1];
		}
		return null;
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00063BC8 File Offset: 0x00061FC8
	public PsGameLoop GetNodeInfoFromPartialPath(int _nodeId)
	{
		for (int i = 0; i < this.m_nodeInfos.Count; i++)
		{
			if (this.m_nodeInfos[i].m_nodeId == _nodeId)
			{
				return this.m_nodeInfos[i];
			}
		}
		return null;
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00063C16 File Offset: 0x00062016
	public PsGameLoop GetCurrentNodeInfo()
	{
		if (this.m_nodeInfos.Count >= this.m_currentNodeId && this.m_currentNodeId > 0)
		{
			return this.m_nodeInfos[this.m_currentNodeId - 1];
		}
		return null;
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00063C50 File Offset: 0x00062050
	public int GetCurrentBlockId()
	{
		for (PsGameLoop psGameLoop = this.GetNodeInfo(this.m_currentNodeId); psGameLoop != null; psGameLoop = this.GetNodeInfo(psGameLoop.m_nodeId + 1))
		{
			if (psGameLoop.m_context == PsMinigameContext.Block)
			{
				return psGameLoop.m_nodeId;
			}
		}
		return -1;
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00063C98 File Offset: 0x00062098
	public int GetNextBlockId(int _nextFromId)
	{
		if (this.m_nodeInfos.Count < _nextFromId)
		{
			return -1;
		}
		for (PsGameLoop psGameLoop = this.GetNodeInfo(_nextFromId + 1); psGameLoop != null; psGameLoop = this.GetNodeInfo(psGameLoop.m_nodeId + 1))
		{
			if (psGameLoop.m_context == PsMinigameContext.Block)
			{
				return psGameLoop.m_nodeId;
			}
		}
		return -1;
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00063CF0 File Offset: 0x000620F0
	public int GetLastBlockId()
	{
		int num = this.m_nodeInfos.Count - 1;
		for (int i = num; i > -1; i--)
		{
			PsGameLoop psGameLoop = this.m_nodeInfos[i];
			if (psGameLoop.m_context == PsMinigameContext.Block)
			{
				return psGameLoop.m_nodeId;
			}
		}
		return 0;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00063D3D File Offset: 0x0006213D
	public PsGameLoop GetLastLevel()
	{
		if (this.m_nodeInfos != null && this.m_nodeInfos.Count > 0)
		{
			return this.m_nodeInfos[this.m_nodeInfos.Count - 1];
		}
		return null;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00063D78 File Offset: 0x00062178
	public int GetLastLevelId()
	{
		int num = this.m_nodeInfos.Count - 1;
		for (int i = num; i > -1; i--)
		{
			PsGameLoop psGameLoop = this.m_nodeInfos[i];
			if (psGameLoop.m_context == PsMinigameContext.Level)
			{
				return psGameLoop.m_nodeId;
			}
		}
		return 0;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00063DC8 File Offset: 0x000621C8
	public int GetPrevBlockId(int _prevFromId)
	{
		if (this.m_nodeInfos.Count < _prevFromId)
		{
			return -1;
		}
		for (PsGameLoop psGameLoop = this.GetNodeInfo(_prevFromId - 1); psGameLoop != null; psGameLoop = this.GetNodeInfo(psGameLoop.m_nodeId - 1))
		{
			if (psGameLoop.m_context == PsMinigameContext.Block)
			{
				return psGameLoop.m_nodeId;
			}
		}
		return 0;
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00063E20 File Offset: 0x00062220
	public PsGameLoop GetCurrentSidePathNodeInfo()
	{
		int i = this.m_currentNodeId;
		PsGameLoop psGameLoop = null;
		while (i > 0)
		{
			PsGameLoop nodeInfo = this.GetNodeInfo(i);
			if (nodeInfo.m_sidePath != null)
			{
				psGameLoop = nodeInfo.m_sidePath.GetCurrentNodeInfo();
			}
			if (psGameLoop != null)
			{
				return psGameLoop;
			}
			i--;
		}
		return psGameLoop;
	}

	// Token: 0x0400089C RID: 2204
	public string m_name;

	// Token: 0x0400089D RID: 2205
	public string m_planet;

	// Token: 0x0400089E RID: 2206
	private PsPlanetPathType m_type;

	// Token: 0x0400089F RID: 2207
	public List<PsGameLoop> m_nodeInfos;

	// Token: 0x040008A0 RID: 2208
	public int m_currentNodeId;

	// Token: 0x040008A1 RID: 2209
	public int m_startNodeId;

	// Token: 0x040008A2 RID: 2210
	public int m_lane;

	// Token: 0x040008A3 RID: 2211
	public PsPlanetPath m_parentPath;

	// Token: 0x040008A4 RID: 2212
	public bool m_overwrite;
}
