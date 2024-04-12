using System;
using System.Runtime.Serialization;

// Token: 0x02000541 RID: 1345
[Serializable]
public class LevelLayer : GraphNode
{
	// Token: 0x06002790 RID: 10128 RVA: 0x001A9F34 File Offset: 0x001A8334
	public LevelLayer(LevelLayerType _layerType, int _width, int _height)
		: base(GraphNodeType.Normal)
	{
		this.m_elementType = GraphElementType.Graph;
		this.m_layerType = _layerType;
		this.m_layerWidth = _width;
		this.m_layerHeight = _height;
		this.m_logicGraph = new GraphNode(GraphNodeType.Normal, "Logic");
		this.m_logicGraph.m_elementType = GraphElementType.Graph;
		base.AddElement(this.m_logicGraph);
	}

	// Token: 0x06002791 RID: 10129 RVA: 0x001A9F90 File Offset: 0x001A8390
	public LevelLayer(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.m_logicFile = (string)info.GetValue("logicFile", typeof(string));
		this.m_logicModified = (bool)info.GetValue("logicModified", typeof(bool));
		this.m_layerType = (LevelLayerType)((uint)info.GetValue("layerType", typeof(uint)));
		this.m_layerWidth = (int)info.GetValue("layerWidth", typeof(int));
		this.m_layerHeight = (int)info.GetValue("layerHeight", typeof(int));
	}

	// Token: 0x06002792 RID: 10130 RVA: 0x001AA045 File Offset: 0x001A8445
	public override void Initialize()
	{
		base.Initialize();
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x001AA04D File Offset: 0x001A844D
	public override void Dispose()
	{
		this.m_logicGraph = null;
		base.Dispose();
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x001AA05C File Offset: 0x001A845C
	public override void OnDeserialization(object sender)
	{
		base.OnDeserialization(sender);
		this.m_logicGraph = this.m_childElements[0] as GraphNode;
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x001AA07C File Offset: 0x001A847C
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("logicFile", this.m_logicFile);
		info.AddValue("logicModified", this.m_logicModified);
		info.AddValue("layerType", (uint)this.m_layerType);
		info.AddValue("layerWidth", this.m_layerWidth);
		info.AddValue("layerHeight", this.m_layerHeight);
	}

	// Token: 0x04002D0A RID: 11530
	public GraphNode m_logicGraph;

	// Token: 0x04002D0B RID: 11531
	public string m_logicFile;

	// Token: 0x04002D0C RID: 11532
	public bool m_logicModified;

	// Token: 0x04002D0D RID: 11533
	public LevelLayerType m_layerType;

	// Token: 0x04002D0E RID: 11534
	public int m_layerWidth;

	// Token: 0x04002D0F RID: 11535
	public int m_layerHeight;
}
