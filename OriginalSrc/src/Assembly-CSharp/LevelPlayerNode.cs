using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000018 RID: 24
[Serializable]
public class LevelPlayerNode : GraphNode
{
	// Token: 0x060000BE RID: 190 RVA: 0x00007D30 File Offset: 0x00006130
	public LevelPlayerNode(Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(GraphNodeType.Normal, _assembleClassType, _name, _pos, _rot, _sca)
	{
		this.m_width = 15f;
		this.m_height = 15f;
		this.m_isRect = false;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00007D5D File Offset: 0x0000615D
	public LevelPlayerNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_assembleClassType = Type.GetType((string)info.GetValue("assembleClassType", typeof(string)));
		this.SetPropertyDefaults();
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00007D92 File Offset: 0x00006192
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isRemovable = false;
		this.m_isCopyable = false;
		this.m_isRotateable = true;
		this.m_isFlippable = false;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00007DB8 File Offset: 0x000061B8
	public override void Flipped()
	{
		Debug.LogError("Was flipped");
		this.m_flipped = !this.m_flipped;
		if (this.m_assembleClassType == typeof(Motorcycle))
		{
			this.m_assembleClassType = typeof(OffroadCar);
		}
		else
		{
			this.m_assembleClassType = typeof(Motorcycle);
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00007E18 File Offset: 0x00006218
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelPlayerNode levelPlayerNode = (LevelPlayerNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelPlayerNode, this);
			graphElement = levelPlayerNode;
		}
		return graphElement;
	}
}
