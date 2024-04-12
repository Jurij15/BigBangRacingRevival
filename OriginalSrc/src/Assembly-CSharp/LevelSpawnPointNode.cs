using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200001E RID: 30
[Serializable]
public class LevelSpawnPointNode : GraphNode
{
	// Token: 0x060000E8 RID: 232 RVA: 0x00008600 File Offset: 0x00006A00
	public LevelSpawnPointNode(Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(GraphNodeType.Normal, _assembleClassType, _name, _pos, _rot, _sca)
	{
		this.m_width = 15f;
		this.m_height = 15f;
		this.m_isRect = false;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000862D File Offset: 0x00006A2D
	public LevelSpawnPointNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_assembleClassType = Type.GetType((string)info.GetValue("assembleClassType", typeof(string)));
		this.SetPropertyDefaults();
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00008662 File Offset: 0x00006A62
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isRemovable = false;
		this.m_isCopyable = false;
		this.m_isRotateable = false;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00008680 File Offset: 0x00006A80
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelSpawnPointNode levelSpawnPointNode = (LevelSpawnPointNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelSpawnPointNode, this);
			graphElement = levelSpawnPointNode;
		}
		return graphElement;
	}
}
