using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200053A RID: 1338
[Serializable]
public class GraphNode : GraphElement, ISerializable, IDeserializationCallback
{
	// Token: 0x0600275E RID: 10078 RVA: 0x00006124 File Offset: 0x00004524
	public GraphNode(GraphNodeType _nodeType)
		: base("GraphNode")
	{
		this.m_childElements = new List<GraphElement>();
		this.m_nodeType = _nodeType;
		this.m_elementType = GraphElementType.Node;
		this.m_pullMode = PullMode.PullAny;
		this.m_triggerMode = TriggerMode.Passive;
		this.m_triggerInterval = 0.0;
		this.m_resourceCount = 0f;
		this.m_maxResourceCount = -1f;
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x00006188 File Offset: 0x00004588
	public GraphNode(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_name)
	{
		Debug.Log("GraphNode Constructor", null);
		this.m_childElements = new List<GraphElement>();
		this.m_nodeType = _nodeType;
		this.m_elementType = GraphElementType.Node;
		this.m_assembleClassType = _assembleClassType;
		this.m_position = _pos;
		this.m_rotation = _rot;
		this.m_scale = _sca;
		this.m_width = 15f;
		this.m_height = 15f;
		this.m_isRect = false;
	}

	// Token: 0x06002760 RID: 10080 RVA: 0x000061FC File Offset: 0x000045FC
	public GraphNode(GraphNodeType _nodeType, string _name)
		: base(_name)
	{
		this.m_childElements = new List<GraphElement>();
		this.m_nodeType = _nodeType;
		this.m_elementType = GraphElementType.Node;
	}

	// Token: 0x06002761 RID: 10081 RVA: 0x00006220 File Offset: 0x00004620
	public GraphNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_nodeType = (GraphNodeType)((uint)info.GetValue("nodeType", typeof(uint)));
		this.m_pullMode = (PullMode)((uint)info.GetValue("pullMode", typeof(uint)));
		this.m_triggerMode = (TriggerMode)((uint)info.GetValue("triggerMode", typeof(uint)));
		try
		{
			this.m_triggerInterval = (double)((float)info.GetValue("triggerInterval", typeof(float)));
		}
		catch
		{
			this.m_triggerInterval = 0.0;
		}
		this.m_tempElements = (GraphElement[])info.GetValue("elements", typeof(GraphElement[]));
		this.m_assembleClassType = Type.GetType((string)info.GetValue("assembleClassType", typeof(string)));
	}

	// Token: 0x06002762 RID: 10082 RVA: 0x00006328 File Offset: 0x00004728
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_moveChilds = true;
		this.m_rotateChilds = true;
	}

	// Token: 0x06002763 RID: 10083 RVA: 0x00006340 File Offset: 0x00004740
	public override void Initialize()
	{
		base.Initialize();
		for (int i = 0; i < this.m_childElements.Count; i++)
		{
			this.m_childElements[i].Initialize();
		}
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x00006380 File Offset: 0x00004780
	public override void Assemble()
	{
		base.Assemble();
		if (this.m_assembleClassType != null)
		{
			object[] array = new object[] { this };
			IAssembledClass assembledClass = Activator.CreateInstance(this.m_assembleClassType, array) as IAssembledClass;
			this.m_assembledClasses.Add(assembledClass);
			LevelManager.m_currentLevel.AddItem(this.m_assembleClassType.ToString());
		}
		for (int i = 0; i < this.m_childElements.Count; i++)
		{
			this.m_childElements[i].Assemble();
		}
	}

	// Token: 0x06002765 RID: 10085 RVA: 0x0000640C File Offset: 0x0000480C
	public GraphElement GetElement(uint _id)
	{
		for (int i = 0; i < this.m_childElements.Count; i++)
		{
			if (this.m_childElements[i].m_id == _id)
			{
				return this.m_childElements[i];
			}
		}
		return null;
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x0000645C File Offset: 0x0000485C
	public GraphElement GetElement(string _name)
	{
		for (int i = 0; i < this.m_childElements.Count; i++)
		{
			if (this.m_childElements[i].m_name == _name)
			{
				return this.m_childElements[i];
			}
		}
		return null;
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x000064B0 File Offset: 0x000048B0
	public GraphElement[] GetElements(string _name)
	{
		List<GraphElement> list = new List<GraphElement>();
		for (int i = 0; i < this.m_childElements.Count; i++)
		{
			if (this.m_childElements[i].m_name == _name)
			{
				list.Add(this.m_childElements[i]);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06002768 RID: 10088 RVA: 0x00006513 File Offset: 0x00004913
	public void AddElement(GraphElement _element)
	{
		_element.m_parentElement = this;
		if (!this.m_childElements.Contains(_element))
		{
			this.m_childElements.Add(_element);
		}
	}

	// Token: 0x06002769 RID: 10089 RVA: 0x00006539 File Offset: 0x00004939
	public bool RemoveElement(GraphElement _element)
	{
		return this.m_childElements.Remove(_element);
	}

	// Token: 0x0600276A RID: 10090 RVA: 0x00006548 File Offset: 0x00004948
	public override void Dispose()
	{
		if (this.m_assembleClassType != null)
		{
			LevelManager.m_currentLevel.RemoveItem(this.m_assembleClassType.ToString());
		}
		while (this.m_childElements.Count > 0)
		{
			int num = this.m_childElements.Count - 1;
			this.m_childElements[num].Dispose();
		}
		this.m_childElements = null;
		base.Clear(false);
		base.Dispose();
	}

	// Token: 0x0600276B RID: 10091 RVA: 0x000065C0 File Offset: 0x000049C0
	public override void Clear(bool _isReset)
	{
		if (this.m_childElements != null)
		{
			for (int i = 0; i < this.m_childElements.Count; i++)
			{
				this.m_childElements[i].Clear(_isReset);
			}
		}
		base.Clear(_isReset);
	}

	// Token: 0x0600276C RID: 10092 RVA: 0x00006610 File Offset: 0x00004A10
	public override void Reset()
	{
		if (this.m_assembleClassType != null)
		{
			LevelManager.m_currentLevel.RemoveItem(this.m_assembleClassType.ToString());
		}
		for (int i = 0; i < this.m_childElements.Count; i++)
		{
			this.m_childElements[i].Reset();
		}
		base.Reset();
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x00006670 File Offset: 0x00004A70
	public override void Update()
	{
		if (this.m_disabled)
		{
			return;
		}
		for (int i = 0; i < this.m_childElements.Count; i++)
		{
			this.m_childElements[i].Update();
		}
	}

	// Token: 0x0600276E RID: 10094 RVA: 0x000066B8 File Offset: 0x00004AB8
	public override void Trigger()
	{
		if (!this.m_disabled)
		{
			if (this.m_pullMode == PullMode.TriggerAny)
			{
				for (int i = 0; i < this.m_outputs.Count; i++)
				{
					this.m_outputs[i].Trigger();
				}
			}
			else if (this.m_pullMode == PullMode.TriggerAllOrNone)
			{
				if (!this.ResourceDemandSatisfied(this.m_stateOutputCount))
				{
					return;
				}
				for (int j = 0; j < this.m_outputs.Count; j++)
				{
					this.m_outputs[j].Trigger();
				}
			}
			else if (this.m_pullMode == PullMode.PullAny)
			{
				for (int k = 0; k < this.m_inputs.Count; k++)
				{
					this.m_inputs[k].Pull();
				}
			}
			else if (this.m_pullMode == PullMode.PullAndTriggerAny)
			{
				for (int l = 0; l < this.m_inputs.Count; l++)
				{
					this.m_inputs[l].Pull();
				}
				for (int m = 0; m < this.m_outputs.Count; m++)
				{
					this.m_outputs[m].Trigger();
				}
			}
			else if (this.m_pullMode == PullMode.PullAllOrNone)
			{
				if (!this.EnoughResourcesToPull(this.m_stateInputCount))
				{
					return;
				}
				for (int n = 0; n < this.m_inputs.Count; n++)
				{
					this.m_inputs[n].Pull();
				}
			}
			else if (this.m_pullMode == PullMode.PullAndTriggerAllOrNone)
			{
				if (!this.EnoughResourcesToPull(this.m_stateInputCount))
				{
					return;
				}
				for (int num = 0; num < this.m_inputs.Count; num++)
				{
					this.m_inputs[num].Pull();
				}
				if (!this.ResourceDemandSatisfied(this.m_stateOutputCount))
				{
					return;
				}
				for (int num2 = 0; num2 < this.m_outputs.Count; num2++)
				{
					this.m_outputs[num2].Trigger();
				}
			}
		}
	}

	// Token: 0x0600276F RID: 10095 RVA: 0x000068F4 File Offset: 0x00004CF4
	public virtual bool ResourceDemandSatisfied(int _index)
	{
		if (_index == this.m_outputs.Count)
		{
			return true;
		}
		float num = 0f;
		for (int i = _index; i < this.m_outputs.Count; i++)
		{
			num += this.m_outputs[i].m_label.m_value;
		}
		return this.m_resourceCount >= num;
	}

	// Token: 0x06002770 RID: 10096 RVA: 0x00006960 File Offset: 0x00004D60
	public virtual bool EnoughResourcesToPull(int _index)
	{
		if (_index == this.m_inputs.Count)
		{
			return true;
		}
		for (int i = _index; i < this.m_inputs.Count; i++)
		{
			GraphConnection graphConnection = this.m_inputs[i];
			if (graphConnection.m_start != null && graphConnection.m_label.m_value > (graphConnection.m_start as GraphNode).m_resourceCount)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002771 RID: 10097 RVA: 0x000069D8 File Offset: 0x00004DD8
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			GraphNode graphNode = (GraphNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(graphNode, this);
			graphNode.m_storedRotation = Vector3.zero;
			if (graphNode.m_childElements.Count != 0)
			{
				for (int i = 0; i < graphNode.m_childElements.Count; i++)
				{
					this.CopyAttributes(graphNode.m_childElements[i], this.m_childElements[i]);
					graphNode.m_childElements[i].m_storedRotation = Vector3.zero;
				}
			}
			graphElement = graphNode;
		}
		return graphElement;
	}

	// Token: 0x06002772 RID: 10098 RVA: 0x00006AA8 File Offset: 0x00004EA8
	public override void CopyAttributes(GraphElement _newElement, GraphElement _oldElement)
	{
		base.CopyAttributes(_newElement, _oldElement);
		GraphNode graphNode = _newElement as GraphNode;
		GraphNode graphNode2 = _oldElement as GraphNode;
		if (graphNode != null && graphNode2 != null)
		{
			graphNode.m_minDistanceFromParent = graphNode2.m_minDistanceFromParent;
			graphNode.m_maxDistanceFromParent = graphNode2.m_maxDistanceFromParent;
		}
	}

	// Token: 0x06002773 RID: 10099 RVA: 0x00006AF0 File Offset: 0x00004EF0
	public override void OnDeserialization(object sender)
	{
		base.OnDeserialization(sender);
		this.m_childElements = new List<GraphElement>(this.m_tempElements);
		this.m_tempElements = null;
		for (int i = 0; i < this.m_childElements.Count; i++)
		{
			this.m_childElements[i].m_parentElement = this;
			if (this.m_childElements[i].m_elementType == GraphElementType.Connection)
			{
				GraphConnection graphConnection = this.m_childElements[i] as GraphConnection;
				GraphElement element = this.GetElement(graphConnection.m_startId);
				GraphElement element2 = this.GetElement(graphConnection.m_endId);
				element.AddOutput(graphConnection);
				element2.AddInput(graphConnection);
			}
		}
	}

	// Token: 0x06002774 RID: 10100 RVA: 0x00006B9C File Offset: 0x00004F9C
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("nodeType", (uint)this.m_nodeType);
		info.AddValue("pullMode", (uint)this.m_pullMode);
		info.AddValue("triggerMode", (uint)this.m_triggerMode);
		info.AddValue("triggerInterval", this.m_triggerInterval);
		info.AddValue("elements", this.m_childElements.ToArray());
		if (this.m_assembleClassType != null)
		{
			info.AddValue("assembleClassType", this.m_assembleClassType.ToString());
		}
		else
		{
			info.AddValue("assembleClassType", string.Empty);
		}
	}

	// Token: 0x04002CE8 RID: 11496
	public GraphNodeType m_nodeType;

	// Token: 0x04002CE9 RID: 11497
	public Type m_assembleClassType;

	// Token: 0x04002CEA RID: 11498
	public List<GraphElement> m_childElements;

	// Token: 0x04002CEB RID: 11499
	public GraphElement[] m_tempElements;

	// Token: 0x04002CEC RID: 11500
	public bool m_rotateChilds;

	// Token: 0x04002CED RID: 11501
	public bool m_moveChilds;

	// Token: 0x04002CEE RID: 11502
	public float m_minDistanceFromParent;

	// Token: 0x04002CEF RID: 11503
	public float m_maxDistanceFromParent;

	// Token: 0x04002CF0 RID: 11504
	public PullMode m_pullMode;

	// Token: 0x04002CF1 RID: 11505
	public TriggerMode m_triggerMode;

	// Token: 0x04002CF2 RID: 11506
	public bool m_triggerFlag;

	// Token: 0x04002CF3 RID: 11507
	public double m_triggerInterval;

	// Token: 0x04002CF4 RID: 11508
	public float m_resourceCount;

	// Token: 0x04002CF5 RID: 11509
	public float m_maxResourceCount;
}
