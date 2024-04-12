using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000011 RID: 17
[Serializable]
public class LevelBlockNode : GraphNode
{
	// Token: 0x0600008D RID: 141 RVA: 0x00006C41 File Offset: 0x00005041
	public LevelBlockNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00006C59 File Offset: 0x00005059
	public LevelBlockNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_shape = (PsBlockShape)((uint)info.GetValue("shape", typeof(uint)));
		this.SetPropertyDefaults();
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00006C90 File Offset: 0x00005090
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("shape", (uint)this.m_shape);
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00006CAC File Offset: 0x000050AC
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelBlockNode levelBlockNode = (LevelBlockNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelBlockNode, this);
			levelBlockNode.m_created = false;
			levelBlockNode.m_storedRotation = Vector3.zero;
			if (levelBlockNode.m_childElements.Count != 0)
			{
				for (int i = 0; i < levelBlockNode.m_childElements.Count; i++)
				{
					this.CopyAttributes(levelBlockNode.m_childElements[i], this.m_childElements[i]);
					levelBlockNode.m_childElements[i].m_storedRotation = Vector3.zero;
				}
			}
			int num = Enum.GetNames(typeof(PsBlockShape)).Length;
			int num2;
			for (num2 = Random.Range(0, num); num2 == (int)levelBlockNode.m_shape; num2 = Random.Range(0, num))
			{
			}
			levelBlockNode.m_shape = (PsBlockShape)Enum.GetValues(typeof(PsBlockShape)).GetValue(num2);
			graphElement = levelBlockNode;
		}
		return graphElement;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00006DE0 File Offset: 0x000051E0
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isFlippable = true;
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
	}

	// Token: 0x0400007F RID: 127
	public PsBlockShape m_shape;

	// Token: 0x04000080 RID: 128
	public Entity m_entity;

	// Token: 0x04000081 RID: 129
	public bool m_created = true;

	// Token: 0x04000082 RID: 130
	public static List<int> m_randomOrder = new List<int>();
}
