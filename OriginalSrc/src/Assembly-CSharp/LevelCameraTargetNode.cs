using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000012 RID: 18
[Serializable]
public class LevelCameraTargetNode : GraphNode
{
	// Token: 0x06000093 RID: 147 RVA: 0x00006E10 File Offset: 0x00005210
	public LevelCameraTargetNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
		this.m_cameraTargetSize = 0;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00006E28 File Offset: 0x00005228
	public LevelCameraTargetNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_cameraTargetSize = (int)info.GetValue("cameraTargetSize", typeof(int));
		this.SetPropertyDefaults();
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00006E58 File Offset: 0x00005258
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("cameraTargetSize", this.m_cameraTargetSize);
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00006E74 File Offset: 0x00005274
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelCameraTargetNode levelCameraTargetNode = (LevelCameraTargetNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelCameraTargetNode, this);
			levelCameraTargetNode.m_storedRotation = Vector3.zero;
			if (levelCameraTargetNode.m_childElements.Count != 0)
			{
				for (int i = 0; i < levelCameraTargetNode.m_childElements.Count; i++)
				{
					this.CopyAttributes(levelCameraTargetNode.m_childElements[i], this.m_childElements[i]);
					levelCameraTargetNode.m_childElements[i].m_storedRotation = Vector3.zero;
				}
			}
			levelCameraTargetNode.m_cameraTargetSize = this.m_cameraTargetSize;
			graphElement = levelCameraTargetNode;
		}
		return graphElement;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00006F50 File Offset: 0x00005350
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isFlippable = true;
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
		this.m_isForegroundable = false;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00006F7B File Offset: 0x0000537B
	public override void Flipped()
	{
		this.m_cameraTargetSize++;
		if (this.m_cameraTargetSize > 4)
		{
			this.m_cameraTargetSize = 0;
		}
	}

	// Token: 0x04000083 RID: 131
	public int m_cameraTargetSize;
}
