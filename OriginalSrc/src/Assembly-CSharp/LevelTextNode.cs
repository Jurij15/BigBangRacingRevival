using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200001F RID: 31
[Serializable]
public class LevelTextNode : GraphNode
{
	// Token: 0x060000EC RID: 236 RVA: 0x000086E4 File Offset: 0x00006AE4
	public LevelTextNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
		this.m_text = "Lorem ipsum dolor sit amet.";
		this.m_stringID = "EMTPY";
	}

	// Token: 0x060000ED RID: 237 RVA: 0x0000870C File Offset: 0x00006B0C
	public LevelTextNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_text = (string)info.GetValue("text", typeof(string));
		try
		{
			this.m_stringID = (string)info.GetValue("stringID", typeof(string));
		}
		catch
		{
			this.m_stringID = "EMPTY";
		}
		this.SetPropertyDefaults();
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00008790 File Offset: 0x00006B90
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("text", this.m_text);
		info.AddValue("stringID", this.m_stringID);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x000087BC File Offset: 0x00006BBC
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelTextNode levelTextNode = (LevelTextNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelTextNode, this);
			graphElement = levelTextNode;
		}
		return graphElement;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00008820 File Offset: 0x00006C20
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
	}

	// Token: 0x040000AC RID: 172
	public string m_text;

	// Token: 0x040000AD RID: 173
	public string m_stringID;

	// Token: 0x040000AE RID: 174
	public Entity m_entity;
}
