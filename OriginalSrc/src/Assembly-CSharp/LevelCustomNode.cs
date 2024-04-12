using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000013 RID: 19
[Serializable]
public class LevelCustomNode : GraphNode
{
	// Token: 0x06000099 RID: 153 RVA: 0x00006F9E File Offset: 0x0000539E
	public LevelCustomNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
		this.m_uint = 0U;
		this.m_int = 0;
		this.m_bool = false;
		this.m_float = 0f;
		this.m_string = string.Empty;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00006FDC File Offset: 0x000053DC
	public LevelCustomNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_uint = (uint)info.GetValue("uint", typeof(uint));
		this.m_int = (int)info.GetValue("int", typeof(int));
		this.m_bool = (bool)info.GetValue("bool", typeof(bool));
		this.m_float = (float)info.GetValue("float", typeof(float));
		this.m_string = (string)info.GetValue("string", typeof(string));
		this.SetPropertyDefaults();
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00007098 File Offset: 0x00005498
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("uint", this.m_uint);
		info.AddValue("int", this.m_int);
		info.AddValue("bool", this.m_bool);
		info.AddValue("float", this.m_float);
		info.AddValue("string", this.m_string);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00007104 File Offset: 0x00005504
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelCustomNode levelCustomNode = (LevelCustomNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelCustomNode, this);
			levelCustomNode.m_storedRotation = Vector3.zero;
			if (levelCustomNode.m_childElements.Count != 0)
			{
				for (int i = 0; i < levelCustomNode.m_childElements.Count; i++)
				{
					this.CopyAttributes(levelCustomNode.m_childElements[i], this.m_childElements[i]);
					levelCustomNode.m_childElements[i].m_storedRotation = Vector3.zero;
				}
			}
			levelCustomNode.m_uint = this.m_uint;
			levelCustomNode.m_int = this.m_int;
			levelCustomNode.m_bool = this.m_bool;
			levelCustomNode.m_float = this.m_float;
			levelCustomNode.m_string = this.m_string;
			graphElement = levelCustomNode;
		}
		return graphElement;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00007210 File Offset: 0x00005610
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isFlippable = true;
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
		this.m_isForegroundable = false;
	}

	// Token: 0x04000084 RID: 132
	public uint m_uint;

	// Token: 0x04000085 RID: 133
	public int m_int;

	// Token: 0x04000086 RID: 134
	public bool m_bool;

	// Token: 0x04000087 RID: 135
	public float m_float;

	// Token: 0x04000088 RID: 136
	public string m_string;
}
