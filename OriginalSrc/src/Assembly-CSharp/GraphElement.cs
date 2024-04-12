using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000536 RID: 1334
[Serializable]
public abstract class GraphElement : ISerializable, IDeserializationCallback
{
	// Token: 0x0600273A RID: 10042 RVA: 0x0000554C File Offset: 0x0000394C
	public GraphElement(string _name)
	{
		this.m_name = _name;
		this.m_id = LevelManager.GetUniqueId();
		this.m_inputs = new List<GraphConnection>();
		this.m_outputs = new List<GraphConnection>();
		this.m_assembledClasses = new List<IAssembledClass>();
		this.m_disabled = true;
		this.m_disabledAtStart = true;
		this.m_radius = 25f;
		this.m_width = 50f;
		this.m_height = 50f;
		this.m_isRect = false;
		this.m_stateInputCount = 0;
		this.m_stateOutputCount = 0;
		this.m_position = Vector3.zero;
		this.m_rotation = Vector3.zero;
		this.m_scale = Vector3.one;
		this.m_storedRotation = Vector3.zero;
		this.m_flipped = false;
		this.m_assembled = false;
		this.m_inFront = false;
		this.SetPropertyDefaults();
	}

	// Token: 0x0600273B RID: 10043 RVA: 0x00005620 File Offset: 0x00003A20
	public GraphElement(SerializationInfo info, StreamingContext ctxt)
	{
		this.m_elementType = (GraphElementType)((uint)info.GetValue("elementType", typeof(uint)));
		this.m_name = (string)info.GetValue("name", typeof(string));
		this.m_id = (uint)info.GetValue("id", typeof(uint));
		float num = (float)info.GetValue("pX", typeof(float));
		float num2 = (float)info.GetValue("pY", typeof(float));
		float num3 = (float)info.GetValue("pZ", typeof(float));
		if (float.IsNaN(num))
		{
			num = 0f;
		}
		if (float.IsNaN(num2))
		{
			num2 = 0f;
		}
		if (float.IsNaN(num3))
		{
			num3 = 0f;
		}
		float num4 = (float)info.GetValue("rX", typeof(float));
		float num5 = (float)info.GetValue("rY", typeof(float));
		float num6 = (float)info.GetValue("rZ", typeof(float));
		float num7 = (float)info.GetValue("sX", typeof(float));
		float num8 = (float)info.GetValue("sY", typeof(float));
		float num9 = (float)info.GetValue("sZ", typeof(float));
		try
		{
			float num10 = (float)info.GetValue("r2X", typeof(float));
			float num11 = (float)info.GetValue("r2Y", typeof(float));
			float num12 = (float)info.GetValue("r2Z", typeof(float));
			this.m_storedRotation = new Vector3(num10, num11, num12);
		}
		catch
		{
			this.m_storedRotation = Vector3.zero;
		}
		this.m_position = new Vector3(num, num2, num3);
		this.m_rotation = new Vector3(num4, num5, num6);
		this.m_scale = new Vector3(num7, num8, num9);
		this.m_flipped = (bool)info.GetValue("flipped", typeof(bool));
		this.m_width = (float)info.GetValue("width", typeof(float));
		this.m_height = (float)info.GetValue("height", typeof(float));
		try
		{
			this.m_radius = (float)info.GetValue("radius", typeof(float));
		}
		catch
		{
			this.m_radius = (this.m_width + this.m_height) * 0.25f;
		}
		this.m_isRect = (bool)info.GetValue("isRect", typeof(bool));
		this.m_disabledAtStart = (bool)info.GetValue("disabled", typeof(bool));
		try
		{
			this.m_inFront = info.GetBoolean("inFront");
		}
		catch
		{
			this.m_inFront = false;
		}
		this.SetPropertyDefaults();
		this.m_inputs = new List<GraphConnection>();
		this.m_outputs = new List<GraphConnection>();
		this.m_assembledClasses = new List<IAssembledClass>();
		this.m_disabled = true;
		this.m_stateInputCount = 0;
		this.m_stateOutputCount = 0;
		if (this.m_id > LevelManager.m_uniqueID)
		{
			LevelManager.m_uniqueID = this.m_id + 1U;
		}
	}

	// Token: 0x0600273C RID: 10044 RVA: 0x000059F0 File Offset: 0x00003DF0
	public virtual void SetPropertyDefaults()
	{
		this.m_isMoveable = true;
		this.m_isRotateable = false;
		this.m_isScaleable = false;
		this.m_isScaleableUniform = false;
		this.m_isCopyable = true;
		this.m_isRemovable = true;
		this.m_isModifiable = false;
		this.m_isReplaceable = false;
		this.m_isFlippable = false;
		this.m_isLinkable = false;
		this.m_isSelectable = true;
		this.m_isForegroundable = false;
	}

	// Token: 0x0600273D RID: 10045 RVA: 0x00005A51 File Offset: 0x00003E51
	public virtual void Select(bool _select)
	{
		if (this.m_isSelectable)
		{
			this.m_selected = _select;
		}
	}

	// Token: 0x0600273E RID: 10046 RVA: 0x00005A68 File Offset: 0x00003E68
	public virtual Vector3 GetConnectionPosition(Vector3 _point)
	{
		Vector3 vector = _point - this.m_position;
		return this.m_position + vector.normalized * this.m_width;
	}

	// Token: 0x0600273F RID: 10047 RVA: 0x00005A9F File Offset: 0x00003E9F
	public virtual Vector3 GetPosition()
	{
		return this.m_position;
	}

	// Token: 0x06002740 RID: 10048 RVA: 0x00005AA7 File Offset: 0x00003EA7
	public virtual Vector3 GetInputPosition()
	{
		return this.m_position;
	}

	// Token: 0x06002741 RID: 10049 RVA: 0x00005AAF File Offset: 0x00003EAF
	public virtual Vector3 GetOutputPosition()
	{
		return this.m_position;
	}

	// Token: 0x06002742 RID: 10050 RVA: 0x00005AB8 File Offset: 0x00003EB8
	public virtual void AddInput(GraphConnection _connection)
	{
		if (_connection.m_end != null)
		{
			_connection.m_end.RemoveInput(_connection);
		}
		if (_connection.m_connectionType == ConnectionType.State)
		{
			this.m_inputs.Insert(this.m_stateInputCount, _connection);
			this.m_stateInputCount++;
		}
		else
		{
			this.m_inputs.Add(_connection);
		}
		_connection.m_end = this;
		_connection.m_points[_connection.m_points.Count - 1] = this.GetPosition();
		_connection.CalculateStartPosition(false);
		_connection.CalculateEndPosition(false);
	}

	// Token: 0x06002743 RID: 10051 RVA: 0x00005B50 File Offset: 0x00003F50
	public virtual void AddOutput(GraphConnection _connection)
	{
		if (_connection.m_start != null)
		{
			_connection.m_start.RemoveOutput(_connection);
		}
		if (_connection.m_connectionType == ConnectionType.State)
		{
			this.m_outputs.Insert(this.m_stateOutputCount, _connection);
			this.m_stateOutputCount++;
		}
		else
		{
			this.m_outputs.Add(_connection);
		}
		_connection.m_start = this;
		_connection.m_points[0] = this.GetPosition();
		_connection.CalculateEndPosition(false);
		_connection.CalculateStartPosition(false);
	}

	// Token: 0x06002744 RID: 10052 RVA: 0x00005BDC File Offset: 0x00003FDC
	public virtual bool RemoveInput(GraphConnection _connection)
	{
		if (this.m_inputs.Contains(_connection))
		{
			_connection.m_end = null;
			if (_connection.m_connectionType == ConnectionType.State)
			{
				this.m_stateInputCount--;
			}
			this.m_inputs.Remove(_connection);
			return true;
		}
		return false;
	}

	// Token: 0x06002745 RID: 10053 RVA: 0x00005C2C File Offset: 0x0000402C
	public virtual bool RemoveOutput(GraphConnection _connection)
	{
		if (this.m_outputs.Contains(_connection))
		{
			_connection.m_start = null;
			if (_connection.m_connectionType == ConnectionType.State)
			{
				this.m_stateOutputCount--;
			}
			this.m_outputs.Remove(_connection);
			return true;
		}
		return false;
	}

	// Token: 0x06002746 RID: 10054 RVA: 0x00005C7C File Offset: 0x0000407C
	public virtual void Dispose()
	{
		this.Clear(false);
		while (this.m_inputs.Count > 0)
		{
			int num = this.m_inputs.Count - 1;
			if (this.m_inputs[num] != null)
			{
				this.m_inputs[num].Dispose();
			}
		}
		this.m_inputs = null;
		while (this.m_outputs.Count > 0)
		{
			int num2 = this.m_outputs.Count - 1;
			if (this.m_outputs[num2] != null)
			{
				this.m_outputs[num2].Dispose();
			}
		}
		this.m_outputs = null;
		if (this.m_parentElement != null)
		{
			this.m_parentElement.RemoveElement(this);
		}
	}

	// Token: 0x06002747 RID: 10055 RVA: 0x00005D42 File Offset: 0x00004142
	public virtual void Initialize()
	{
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x00005D44 File Offset: 0x00004144
	public virtual void Assemble()
	{
		if (!this.m_disabledAtStart)
		{
			this.m_disabled = false;
		}
		if (this.m_TC == null)
		{
			this.m_TC = EntityManager.AddEntityWithTC();
			this.m_TC.transform.name = this.m_name;
			TransformS.SetGlobalPosition(this.m_TC, this.m_position);
			TransformS.SetGlobalRotation(this.m_TC, this.m_rotation);
		}
		this.m_assembled = true;
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x00005DB8 File Offset: 0x000041B8
	public virtual void Copied()
	{
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x00005DBA File Offset: 0x000041BA
	public virtual void Flipped()
	{
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x00005DBC File Offset: 0x000041BC
	public virtual void Rotated()
	{
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x00005DBE File Offset: 0x000041BE
	public virtual void Foregrounded()
	{
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x00005DC0 File Offset: 0x000041C0
	public virtual void Linked()
	{
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x00005DC2 File Offset: 0x000041C2
	public virtual void Removed()
	{
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x00005DC4 File Offset: 0x000041C4
	public virtual void Clear(bool _isReset)
	{
		while (this.m_assembledClasses.Count > 0)
		{
			int num = this.m_assembledClasses.Count - 1;
			this.m_assembledClasses[num].Destroy();
		}
		if (this.m_TC != null)
		{
			EntityManager.RemoveEntity(this.m_TC.p_entity);
			this.m_TC = null;
			this.m_TAC = null;
		}
		this.m_assembled = false;
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x00005E36 File Offset: 0x00004236
	public virtual void Reset()
	{
		this.Clear(true);
		this.Assemble();
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x00005E45 File Offset: 0x00004245
	public virtual void Update()
	{
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x00005E47 File Offset: 0x00004247
	public virtual void Trigger()
	{
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x00005E49 File Offset: 0x00004249
	public virtual void Start()
	{
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x00005E4B File Offset: 0x0000424B
	public virtual void End()
	{
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x00005E4D File Offset: 0x0000424D
	public virtual GraphConnection CreateConnection(GraphElement _target)
	{
		return null;
	}

	// Token: 0x06002756 RID: 10070 RVA: 0x00005E50 File Offset: 0x00004250
	public virtual void CopyAttributes(GraphElement _newElement, GraphElement _oldElement)
	{
		_newElement.m_isMoveable = _oldElement.m_isMoveable;
		_newElement.m_isRotateable = _oldElement.m_isRotateable;
		_newElement.m_isScaleable = this.m_isScaleable;
		_newElement.m_isScaleableUniform = _oldElement.m_isScaleableUniform;
		_newElement.m_isCopyable = _oldElement.m_isCopyable;
		_newElement.m_isRemovable = _oldElement.m_isRemovable;
		_newElement.m_isModifiable = _oldElement.m_isModifiable;
		_newElement.m_isReplaceable = _oldElement.m_isReplaceable;
		_newElement.m_isFlippable = _oldElement.m_isFlippable;
		_newElement.m_isLinkable = _oldElement.m_isLinkable;
		_newElement.m_isForegroundable = _oldElement.m_isForegroundable;
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x00005EE4 File Offset: 0x000042E4
	public virtual GraphElement DeepCopy()
	{
		GraphElement graphElement2;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			GraphElement graphElement = (GraphElement)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(graphElement, this);
			graphElement2 = graphElement;
		}
		return graphElement2;
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x00005F48 File Offset: 0x00004348
	public virtual void OnDeserialization(object sender)
	{
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x00005F4C File Offset: 0x0000434C
	public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("elementType", (uint)this.m_elementType);
		info.AddValue("name", this.m_name);
		info.AddValue("id", this.m_id);
		info.AddValue("pX", this.m_position.x);
		info.AddValue("pY", this.m_position.y);
		info.AddValue("pZ", this.m_position.z);
		info.AddValue("rX", this.m_rotation.x);
		info.AddValue("rY", this.m_rotation.y);
		info.AddValue("rZ", this.m_rotation.z);
		info.AddValue("sX", this.m_scale.x);
		info.AddValue("sY", this.m_scale.y);
		info.AddValue("sZ", this.m_scale.z);
		info.AddValue("r2X", this.m_storedRotation.x);
		info.AddValue("r2Y", this.m_storedRotation.y);
		info.AddValue("r2Z", this.m_storedRotation.z);
		info.AddValue("flipped", this.m_flipped);
		info.AddValue("width", this.m_width);
		info.AddValue("height", this.m_height);
		info.AddValue("radius", this.m_radius);
		info.AddValue("isRect", this.m_isRect);
		info.AddValue("disabled", this.m_disabledAtStart);
		info.AddValue("inFront", this.m_inFront);
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x0000610B File Offset: 0x0000450B
	public virtual bool CanConnect(GraphElement _host)
	{
		return true;
	}

	// Token: 0x0600275B RID: 10075 RVA: 0x0000610E File Offset: 0x0000450E
	public virtual bool CanCreateConnection()
	{
		return true;
	}

	// Token: 0x0600275C RID: 10076 RVA: 0x00006111 File Offset: 0x00004511
	public virtual List<GraphElement> FilterSuitables(List<GraphElement> _list)
	{
		return _list;
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x00006114 File Offset: 0x00004514
	public virtual GraphConnectionVisual GetVisualConnection(Vector3 _start, Vector3 _end)
	{
		return new GraphConnectionVisual(this.m_TC, _start, _end);
	}

	// Token: 0x04002CB2 RID: 11442
	public TransformC m_TC;

	// Token: 0x04002CB3 RID: 11443
	public TouchAreaC m_TAC;

	// Token: 0x04002CB4 RID: 11444
	public GraphNode m_parentElement;

	// Token: 0x04002CB5 RID: 11445
	public GraphElementType m_elementType;

	// Token: 0x04002CB6 RID: 11446
	public string m_name;

	// Token: 0x04002CB7 RID: 11447
	public uint m_id;

	// Token: 0x04002CB8 RID: 11448
	public float m_radius;

	// Token: 0x04002CB9 RID: 11449
	public float m_width;

	// Token: 0x04002CBA RID: 11450
	public float m_height;

	// Token: 0x04002CBB RID: 11451
	public bool m_isRect;

	// Token: 0x04002CBC RID: 11452
	public Vector3 m_position;

	// Token: 0x04002CBD RID: 11453
	public Vector3 m_rotation;

	// Token: 0x04002CBE RID: 11454
	public Vector3 m_scale;

	// Token: 0x04002CBF RID: 11455
	public Vector3 m_storedRotation;

	// Token: 0x04002CC0 RID: 11456
	public bool m_flipped;

	// Token: 0x04002CC1 RID: 11457
	public bool m_inFront;

	// Token: 0x04002CC2 RID: 11458
	public bool m_disabled;

	// Token: 0x04002CC3 RID: 11459
	public bool m_disabledAtStart;

	// Token: 0x04002CC4 RID: 11460
	public bool m_selected;

	// Token: 0x04002CC5 RID: 11461
	public List<GraphConnection> m_inputs;

	// Token: 0x04002CC6 RID: 11462
	public List<GraphConnection> m_outputs;

	// Token: 0x04002CC7 RID: 11463
	public int m_stateInputCount;

	// Token: 0x04002CC8 RID: 11464
	public int m_stateOutputCount;

	// Token: 0x04002CC9 RID: 11465
	public bool m_assembled;

	// Token: 0x04002CCA RID: 11466
	public List<IAssembledClass> m_assembledClasses;

	// Token: 0x04002CCB RID: 11467
	public bool m_isMoveable;

	// Token: 0x04002CCC RID: 11468
	public bool m_isRotateable;

	// Token: 0x04002CCD RID: 11469
	public bool m_isScaleable;

	// Token: 0x04002CCE RID: 11470
	public bool m_isScaleableUniform;

	// Token: 0x04002CCF RID: 11471
	public bool m_isCopyable;

	// Token: 0x04002CD0 RID: 11472
	public bool m_isRemovable;

	// Token: 0x04002CD1 RID: 11473
	public bool m_isModifiable;

	// Token: 0x04002CD2 RID: 11474
	public bool m_isReplaceable;

	// Token: 0x04002CD3 RID: 11475
	public bool m_isFlippable;

	// Token: 0x04002CD4 RID: 11476
	public bool m_isLinkable;

	// Token: 0x04002CD5 RID: 11477
	public bool m_isSelectable;

	// Token: 0x04002CD6 RID: 11478
	public bool m_isForegroundable;
}
