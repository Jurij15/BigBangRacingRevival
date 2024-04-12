using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000533 RID: 1331
[Serializable]
public abstract class GraphConnection : GraphElement, ISerializable, IDeserializationCallback
{
	// Token: 0x06002725 RID: 10021 RVA: 0x000370BC File Offset: 0x000354BC
	public GraphConnection(string _name)
		: base(_name)
	{
		this.m_points = new List<Vector3>();
		this.m_points.Add(Vector3.zero);
		this.m_points.Add(Vector3.zero);
		this.m_label = new ConnectionLabel();
		this.m_labelPos = 0.75f;
		this.m_startId = 0U;
		this.m_endId = 0U;
		this.m_elementType = GraphElementType.Connection;
		this.m_connectionType = ConnectionType.Link;
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x0003712D File Offset: 0x0003552D
	public GraphConnection(GraphElement _start, GraphElement _end)
		: this("GraphConnection")
	{
		this.m_disabledAtStart = false;
		_start.AddOutput(this);
		_end.AddInput(this);
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x00037150 File Offset: 0x00035550
	public GraphConnection(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_tempPoints = (Vertex3[])info.GetValue("points", typeof(Vertex3[]));
		this.m_label = (ConnectionLabel)info.GetValue("label", typeof(ConnectionLabel));
		this.m_connectionType = (ConnectionType)((uint)info.GetValue("connectionType", typeof(uint)));
		this.m_startId = (uint)info.GetValue("startId", typeof(uint));
		this.m_endId = (uint)info.GetValue("endId", typeof(uint));
		this.m_labelPos = (float)info.GetValue("labelPos", typeof(float));
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x00037228 File Offset: 0x00035628
	public override Vector3 GetPosition()
	{
		this.m_length = 0f;
		for (int i = 1; i < this.m_points.Count; i++)
		{
			this.m_length += (this.m_points[i] - this.m_points[i - 1]).magnitude;
		}
		float num = this.m_length * this.m_labelPos;
		float num2 = 0f;
		int num3 = 0;
		Vector3 vector = Vector3.zero;
		for (int j = 0; j < this.m_points.Count - 1; j++)
		{
			vector = this.m_points[j + 1] - this.m_points[j];
			float magnitude = vector.magnitude;
			num2 += magnitude;
			if (num2 > num)
			{
				num3 = j;
				num2 -= magnitude;
				break;
			}
		}
		this.m_position = this.m_points[num3] + vector.normalized * (num - num2);
		return this.m_position;
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x00037343 File Offset: 0x00035743
	public virtual void CalculateStartPosition(bool _reassemble)
	{
		if (this.m_start != null)
		{
			this.m_points[0] = this.m_start.GetConnectionPosition(this.m_points[1]);
			if (_reassemble)
			{
				this.Reset();
			}
		}
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x00037380 File Offset: 0x00035780
	public virtual void CalculateEndPosition(bool _reassemble)
	{
		if (this.m_end != null)
		{
			this.m_points[this.m_points.Count - 1] = this.m_end.GetConnectionPosition(this.m_points[this.m_points.Count - 2]);
			if (_reassemble)
			{
				this.Reset();
			}
		}
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x000373E0 File Offset: 0x000357E0
	public override void Dispose()
	{
		if (this.m_start != null)
		{
			this.m_start.RemoveOutput(this);
		}
		this.m_start = null;
		if (this.m_end != null)
		{
			this.m_end.RemoveInput(this);
		}
		this.m_end = null;
		this.Clear(false);
		base.Dispose();
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x00037438 File Offset: 0x00035838
	public override void Initialize()
	{
		if (this.m_startId > 0U)
		{
			this.m_start = this.m_parentElement.GetElement(this.m_startId);
			this.m_start.AddOutput(this);
		}
		if (this.m_endId > 0U)
		{
			this.m_end = this.m_parentElement.GetElement(this.m_endId);
			this.m_end.AddInput(this);
		}
		this.GetPosition();
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x000374AA File Offset: 0x000358AA
	public override void Assemble()
	{
		base.Assemble();
		this.GetPosition();
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x000374B9 File Offset: 0x000358B9
	public override void Clear(bool _isReset)
	{
		base.Clear(_isReset);
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x000374C2 File Offset: 0x000358C2
	public virtual void Pull()
	{
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x000374C4 File Offset: 0x000358C4
	public override void Trigger()
	{
		if (this.m_end != null)
		{
			this.m_end.Trigger();
		}
	}

	// Token: 0x06002731 RID: 10033 RVA: 0x000374DC File Offset: 0x000358DC
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x06002732 RID: 10034 RVA: 0x000374E4 File Offset: 0x000358E4
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			GraphConnection graphConnection = (GraphConnection)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(graphConnection, this);
			graphElement = graphConnection;
		}
		return graphElement;
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x00037548 File Offset: 0x00035948
	public override void OnDeserialization(object sender)
	{
		this.m_points = new List<Vector3>(this.m_tempPoints.Length);
		for (int i = 0; i < this.m_tempPoints.Length; i++)
		{
			this.m_points.Add(this.m_tempPoints[i].ToVector3());
		}
	}

	// Token: 0x06002734 RID: 10036 RVA: 0x0003759C File Offset: 0x0003599C
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		if (this.m_start != null)
		{
			this.m_startId = this.m_start.m_id;
		}
		else
		{
			this.m_startId = 0U;
		}
		if (this.m_end != null)
		{
			this.m_endId = this.m_end.m_id;
		}
		else
		{
			this.m_endId = 0U;
		}
		this.m_tempPoints = new Vertex3[this.m_points.Count];
		for (int i = 0; i < this.m_tempPoints.Length; i++)
		{
			this.m_tempPoints[i] = new Vertex3(this.m_points[i]);
		}
		info.AddValue("points", this.m_tempPoints);
		info.AddValue("label", this.m_label);
		info.AddValue("connectionType", (uint)this.m_connectionType);
		info.AddValue("startId", this.m_startId);
		info.AddValue("endId", this.m_endId);
		info.AddValue("labelPos", this.m_labelPos);
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x000376AF File Offset: 0x00035AAF
	public override bool CanConnect(GraphElement _host)
	{
		return false;
	}

	// Token: 0x04002CA0 RID: 11424
	public ConnectionType m_connectionType;

	// Token: 0x04002CA1 RID: 11425
	public ConnectionLabel m_label;

	// Token: 0x04002CA2 RID: 11426
	public uint m_startId;

	// Token: 0x04002CA3 RID: 11427
	public uint m_endId;

	// Token: 0x04002CA4 RID: 11428
	private float m_length;

	// Token: 0x04002CA5 RID: 11429
	private float m_labelPos;

	// Token: 0x04002CA6 RID: 11430
	public GraphElement m_start;

	// Token: 0x04002CA7 RID: 11431
	public GraphElement m_end;

	// Token: 0x04002CA8 RID: 11432
	public List<Vector3> m_points;

	// Token: 0x04002CA9 RID: 11433
	private Vertex3[] m_tempPoints;
}
