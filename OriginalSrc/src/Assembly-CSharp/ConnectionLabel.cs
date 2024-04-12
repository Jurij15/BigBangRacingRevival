using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// Token: 0x0200052F RID: 1327
[Serializable]
public class ConnectionLabel : ISerializable, IDeserializationCallback
{
	// Token: 0x06002714 RID: 10004 RVA: 0x001A99B9 File Offset: 0x001A7DB9
	public ConnectionLabel()
	{
		this.m_labelType = LabelType.None;
		this.m_value = 1f;
		this.m_range = 0f;
		this.m_minValue = -9999f;
		this.m_maxValue = 9999f;
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x001A99F4 File Offset: 0x001A7DF4
	public ConnectionLabel(SerializationInfo info, StreamingContext ctxt)
	{
		this.m_label = (string)info.GetValue("label", typeof(string));
		this.m_labelType = (LabelType)((uint)info.GetValue("labelType", typeof(uint)));
		this.m_range = (float)info.GetValue("range", typeof(float));
		this.m_minValue = (float)info.GetValue("min", typeof(float));
		this.m_maxValue = (float)info.GetValue("max", typeof(float));
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x001A9AA7 File Offset: 0x001A7EA7
	public void ParseLabel()
	{
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x001A9AA9 File Offset: 0x001A7EA9
	public float Calculate(float _resourceCount)
	{
		return _resourceCount * this.m_value;
	}

	// Token: 0x06002718 RID: 10008 RVA: 0x001A9AB4 File Offset: 0x001A7EB4
	public ConnectionLabel DeepCopy()
	{
		ConnectionLabel connectionLabel;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			connectionLabel = (ConnectionLabel)binaryFormatter.Deserialize(memoryStream);
		}
		return connectionLabel;
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x001A9B10 File Offset: 0x001A7F10
	public void OnDeserialization(object sender)
	{
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x001A9B14 File Offset: 0x001A7F14
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("label", this.m_label);
		info.AddValue("labelType", (uint)this.m_labelType);
		info.AddValue("range", this.m_range);
		info.AddValue("min", this.m_minValue);
		info.AddValue("max", this.m_maxValue);
	}

	// Token: 0x04002C96 RID: 11414
	public string m_label;

	// Token: 0x04002C97 RID: 11415
	public LabelType m_labelType;

	// Token: 0x04002C98 RID: 11416
	public float m_value;

	// Token: 0x04002C99 RID: 11417
	public float m_range;

	// Token: 0x04002C9A RID: 11418
	public float m_minValue;

	// Token: 0x04002C9B RID: 11419
	public float m_maxValue;
}
