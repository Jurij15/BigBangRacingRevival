using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200001B RID: 27
[Serializable]
public class LevelRockNode : GraphNode
{
	// Token: 0x060000DD RID: 221 RVA: 0x000081B4 File Offset: 0x000065B4
	public LevelRockNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000081C8 File Offset: 0x000065C8
	public LevelRockNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		try
		{
			this.m_rockRadius = (float)info.GetValue("rockRadius", typeof(float));
		}
		catch
		{
			this.m_rockRadius = (float)info.GetValue("radius", typeof(float));
		}
		this.m_totalVertexCount = (int)info.GetValue("vertexCount", typeof(int));
		this.m_rockVertices = new Vector2[this.m_totalVertexCount];
		for (int i = 0; i < this.m_totalVertexCount; i++)
		{
			this.m_rockVertices[i] = new Vector2((float)info.GetValue("pos" + i + "x", typeof(float)), (float)info.GetValue("pos" + i + "y", typeof(float)));
		}
		this.SetPropertyDefaults();
	}

	// Token: 0x060000DF RID: 223 RVA: 0x000082F0 File Offset: 0x000066F0
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("rockRadius", this.m_rockRadius);
		info.AddValue("vertexCount", this.m_totalVertexCount);
		for (int i = 0; i < this.m_totalVertexCount; i++)
		{
			info.AddValue("pos" + i + "x", this.m_rockVertices[i].x);
			info.AddValue("pos" + i + "y", this.m_rockVertices[i].y);
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00008398 File Offset: 0x00006798
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelRockNode levelRockNode = (LevelRockNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelRockNode, this);
			levelRockNode.m_rockVertices = null;
			levelRockNode.m_totalVertexCount = 0;
			graphElement = levelRockNode;
		}
		return graphElement;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0000840C File Offset: 0x0000680C
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
		this.m_isFlippable = true;
	}

	// Token: 0x040000A0 RID: 160
	public Vector2[] m_rockVertices;

	// Token: 0x040000A1 RID: 161
	public int m_totalVertexCount;

	// Token: 0x040000A2 RID: 162
	public float m_rockRadius;

	// Token: 0x040000A3 RID: 163
	public Entity m_entity;
}
