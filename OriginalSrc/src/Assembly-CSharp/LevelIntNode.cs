using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000017 RID: 23
[Serializable]
public class LevelIntNode : GraphNode
{
	// Token: 0x060000B9 RID: 185 RVA: 0x00007C58 File Offset: 0x00006058
	public LevelIntNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00007C69 File Offset: 0x00006069
	public LevelIntNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_int = info.GetInt32("int");
		this.SetPropertyDefaults();
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00007C8A File Offset: 0x0000608A
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("int", this.m_int);
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00007CA8 File Offset: 0x000060A8
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelIntNode levelIntNode = (LevelIntNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelIntNode, this);
			graphElement = levelIntNode;
		}
		return graphElement;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00007D0C File Offset: 0x0000610C
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isFlippable = true;
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
	}

	// Token: 0x04000095 RID: 149
	public int m_int;
}
