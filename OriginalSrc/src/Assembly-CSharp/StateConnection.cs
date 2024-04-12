using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// Token: 0x02000531 RID: 1329
[Serializable]
public class StateConnection : GraphConnection, ISerializable, IDeserializationCallback
{
	// Token: 0x06002721 RID: 10017 RVA: 0x001A9CCA File Offset: 0x001A80CA
	public StateConnection()
		: base("StateConnection")
	{
		this.m_connectionType = ConnectionType.State;
	}

	// Token: 0x06002722 RID: 10018 RVA: 0x001A9CDE File Offset: 0x001A80DE
	public StateConnection(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x001A9CE8 File Offset: 0x001A80E8
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			StateConnection stateConnection = (StateConnection)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(stateConnection, this);
			graphElement = stateConnection;
		}
		return graphElement;
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x001A9D4C File Offset: 0x001A814C
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
	}
}
