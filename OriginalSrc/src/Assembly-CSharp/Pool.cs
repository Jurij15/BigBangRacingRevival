using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// Token: 0x0200053D RID: 1341
[Serializable]
public class Pool : GraphNode, ISerializable, IDeserializationCallback
{
	// Token: 0x0600277C RID: 10108 RVA: 0x001A9EA3 File Offset: 0x001A82A3
	public Pool()
		: base(GraphNodeType.Normal)
	{
		this.m_name = "Pool";
	}

	// Token: 0x0600277D RID: 10109 RVA: 0x001A9EB7 File Offset: 0x001A82B7
	public Pool(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
	}

	// Token: 0x0600277E RID: 10110 RVA: 0x001A9EC4 File Offset: 0x001A82C4
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			Pool pool = (Pool)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(pool, this);
			graphElement = pool;
		}
		return graphElement;
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x001A9F28 File Offset: 0x001A8328
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
	}
}
