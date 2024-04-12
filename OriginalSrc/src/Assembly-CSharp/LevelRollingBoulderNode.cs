using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200001D RID: 29
[Serializable]
public class LevelRollingBoulderNode : GraphNode
{
	// Token: 0x060000E2 RID: 226 RVA: 0x00008430 File Offset: 0x00006830
	public LevelRollingBoulderNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00008448 File Offset: 0x00006848
	public LevelRollingBoulderNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_boulderRadius = (PsRollingBoulderRadius)((uint)info.GetValue("boulderRadius", typeof(uint)));
		this.SetPropertyDefaults();
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x0000847F File Offset: 0x0000687F
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("boulderRadius", (uint)this.m_boulderRadius);
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x0000849C File Offset: 0x0000689C
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelRollingBoulderNode levelRollingBoulderNode = (LevelRollingBoulderNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelRollingBoulderNode, this);
			levelRollingBoulderNode.m_created = false;
			levelRollingBoulderNode.m_storedRotation = Vector3.zero;
			if (levelRollingBoulderNode.m_childElements.Count != 0)
			{
				for (int i = 0; i < levelRollingBoulderNode.m_childElements.Count; i++)
				{
					this.CopyAttributes(levelRollingBoulderNode.m_childElements[i], this.m_childElements[i]);
					levelRollingBoulderNode.m_childElements[i].m_storedRotation = Vector3.zero;
				}
			}
			int num = Enum.GetNames(typeof(PsRollingBoulderRadius)).Length;
			int num2;
			for (num2 = Random.Range(0, num); num2 == (int)levelRollingBoulderNode.m_boulderRadius; num2 = Random.Range(0, num))
			{
			}
			levelRollingBoulderNode.m_boulderRadius = (PsRollingBoulderRadius)Enum.GetValues(typeof(PsRollingBoulderRadius)).GetValue(num2);
			graphElement = levelRollingBoulderNode;
		}
		return graphElement;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x000085D0 File Offset: 0x000069D0
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isFlippable = true;
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
	}

	// Token: 0x040000A8 RID: 168
	public PsRollingBoulderRadius m_boulderRadius;

	// Token: 0x040000A9 RID: 169
	public Entity m_entity;

	// Token: 0x040000AA RID: 170
	public bool m_created = true;

	// Token: 0x040000AB RID: 171
	public static List<int> m_randomOrder = new List<int>();
}
