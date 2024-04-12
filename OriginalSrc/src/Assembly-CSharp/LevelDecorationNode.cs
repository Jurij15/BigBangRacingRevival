using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000014 RID: 20
[Serializable]
public class LevelDecorationNode : GraphNode
{
	// Token: 0x0600009E RID: 158 RVA: 0x0000723B File Offset: 0x0000563B
	public LevelDecorationNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
		this.m_index = 0U;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00007253 File Offset: 0x00005653
	public LevelDecorationNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_index = (uint)info.GetValue("index", typeof(uint));
		this.SetPropertyDefaults();
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00007283 File Offset: 0x00005683
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("index", this.m_index);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000072A0 File Offset: 0x000056A0
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelDecorationNode levelDecorationNode = (LevelDecorationNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelDecorationNode, this);
			levelDecorationNode.m_flipped = false;
			levelDecorationNode.m_inFront = false;
			levelDecorationNode.m_storedRotation = Vector3.zero;
			if (levelDecorationNode.m_childElements.Count != 0)
			{
				for (int i = 0; i < levelDecorationNode.m_childElements.Count; i++)
				{
					this.CopyAttributes(levelDecorationNode.m_childElements[i], this.m_childElements[i]);
					levelDecorationNode.m_childElements[i].m_storedRotation = Vector3.zero;
				}
			}
			levelDecorationNode.m_inFront = this.m_inFront;
			levelDecorationNode.m_index = this.m_index;
			graphElement = levelDecorationNode;
		}
		return graphElement;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00007398 File Offset: 0x00005798
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isFlippable = true;
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
		this.m_isForegroundable = false;
	}

	// Token: 0x04000089 RID: 137
	public uint m_index;
}
