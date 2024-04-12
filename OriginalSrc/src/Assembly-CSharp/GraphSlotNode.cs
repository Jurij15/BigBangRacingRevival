using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200053C RID: 1340
[Serializable]
public class GraphSlotNode : GraphNode
{
	// Token: 0x06002775 RID: 10101 RVA: 0x001A9D58 File Offset: 0x001A8158
	public GraphSlotNode(GraphNodeType _nodeType, string _name, Vector3 _pos, Vector3 _rot)
		: base(_nodeType, _name)
	{
		this.m_position = _pos;
		this.m_rotation = _rot;
		this.m_width = 15f;
		this.m_height = 15f;
		this.m_isRect = false;
		this.m_externalNodes = new List<GraphNode>();
	}

	// Token: 0x06002776 RID: 10102 RVA: 0x001A9DA4 File Offset: 0x001A81A4
	public GraphSlotNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_slotType = (SlotType)info.GetValue("slotType", typeof(SlotType));
		this.m_externalNodeIds = (int[])info.GetValue("externalNodeIds", typeof(int[]));
	}

	// Token: 0x06002777 RID: 10103 RVA: 0x001A9DF9 File Offset: 0x001A81F9
	public override void Initialize()
	{
		base.Initialize();
	}

	// Token: 0x06002778 RID: 10104 RVA: 0x001A9E01 File Offset: 0x001A8201
	public override void Assemble()
	{
		this.m_assembled = true;
	}

	// Token: 0x06002779 RID: 10105 RVA: 0x001A9E0C File Offset: 0x001A820C
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			GraphSlotNode graphSlotNode = (GraphSlotNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(graphSlotNode, this);
			graphElement = graphSlotNode;
		}
		return graphElement;
	}

	// Token: 0x0600277A RID: 10106 RVA: 0x001A9E70 File Offset: 0x001A8270
	public override void OnDeserialization(object sender)
	{
	}

	// Token: 0x0600277B RID: 10107 RVA: 0x001A9E72 File Offset: 0x001A8272
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("slotType", this.m_slotType);
		info.AddValue("externalNodes", this.m_externalNodeIds);
	}

	// Token: 0x04002CFC RID: 11516
	public SlotType m_slotType;

	// Token: 0x04002CFD RID: 11517
	public List<GraphNode> m_externalNodes;

	// Token: 0x04002CFE RID: 11518
	public int[] m_externalGraphIds;

	// Token: 0x04002CFF RID: 11519
	public int[] m_externalNodeIds;
}
