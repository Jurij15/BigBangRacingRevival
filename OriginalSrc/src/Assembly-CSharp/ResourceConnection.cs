using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// Token: 0x02000530 RID: 1328
[Serializable]
public class ResourceConnection : GraphConnection, ISerializable, IDeserializationCallback
{
	// Token: 0x0600271B RID: 10011 RVA: 0x001A9B76 File Offset: 0x001A7F76
	public ResourceConnection()
		: base("ResourceConnection")
	{
		this.m_connectionType = ConnectionType.Resource;
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x001A9B8A File Offset: 0x001A7F8A
	public ResourceConnection(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
	}

	// Token: 0x0600271D RID: 10013 RVA: 0x001A9B94 File Offset: 0x001A7F94
	public override void Pull()
	{
		if (this.m_start != null && this.m_start.m_elementType != GraphElementType.Connection)
		{
			GraphNode graphNode = this.m_start as GraphNode;
			GraphNode graphNode2 = this.m_end as GraphNode;
			if (graphNode.m_resourceCount == 0f)
			{
				return;
			}
			float num = this.m_label.Calculate(graphNode.m_resourceCount);
			if (num > graphNode.m_resourceCount)
			{
				graphNode2.m_resourceCount += graphNode.m_resourceCount;
				graphNode.m_resourceCount = 0f;
			}
			else
			{
				graphNode.m_resourceCount -= num;
				graphNode2.m_resourceCount += num;
			}
		}
	}

	// Token: 0x0600271E RID: 10014 RVA: 0x001A9C42 File Offset: 0x001A8042
	public override void Trigger()
	{
		if (this.m_end != null)
		{
			this.m_end.Trigger();
		}
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x001A9C5C File Offset: 0x001A805C
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			ResourceConnection resourceConnection = (ResourceConnection)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(resourceConnection, this);
			graphElement = resourceConnection;
		}
		return graphElement;
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x001A9CC0 File Offset: 0x001A80C0
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
	}
}
