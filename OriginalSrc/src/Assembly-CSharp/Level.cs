using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x0200053F RID: 1343
[Serializable]
public class Level : ISerializable
{
	// Token: 0x06002786 RID: 10118 RVA: 0x00003D3C File Offset: 0x0000213C
	public Level(int _width, int _height)
	{
		this.m_settings = new Hashtable();
		this.m_layers = new List<LevelLayer>();
		this.m_currentLayer = new LevelLayer(LevelLayerType.BaseLayer, _width, _height);
		this.m_layers.Add(this.m_currentLayer);
		this.m_currentLayerIndex = 0;
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x00003D8C File Offset: 0x0000218C
	public Level(SerializationInfo info, StreamingContext ctxt)
	{
		this.m_settings = (Hashtable)info.GetValue("settings", typeof(Hashtable));
		this.m_layers = new List<LevelLayer>((LevelLayer[])info.GetValue("layers", typeof(LevelLayer[])));
	}

	// Token: 0x06002788 RID: 10120 RVA: 0x00003DE4 File Offset: 0x000021E4
	public virtual void ItemChanged()
	{
		this.m_changed = true;
	}

	// Token: 0x06002789 RID: 10121 RVA: 0x00003DED File Offset: 0x000021ED
	public virtual void AddItem(string _key)
	{
	}

	// Token: 0x0600278A RID: 10122 RVA: 0x00003DEF File Offset: 0x000021EF
	public virtual void RemoveItem(string _key)
	{
	}

	// Token: 0x0600278B RID: 10123 RVA: 0x00003DF1 File Offset: 0x000021F1
	public virtual void ClearItems()
	{
	}

	// Token: 0x0600278C RID: 10124 RVA: 0x00003DF3 File Offset: 0x000021F3
	public virtual void SetLayerItems()
	{
	}

	// Token: 0x0600278D RID: 10125 RVA: 0x00003DF8 File Offset: 0x000021F8
	public virtual void Destroy()
	{
		while (this.m_layers.Count > 0)
		{
			int num = this.m_layers.Count - 1;
			this.m_layers[num].Dispose();
			this.m_layers.RemoveAt(num);
		}
		this.m_layers = null;
		this.m_currentLayer = null;
		this.m_currentLayerIndex = -1;
		this.m_settings.Clear();
		this.m_settings = null;
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x00003E70 File Offset: 0x00002270
	public virtual void Update()
	{
		for (int i = 0; i < this.m_layers.Count; i++)
		{
			if (this.m_layers[i].m_assembled && !this.m_layers[i].m_disabled)
			{
				this.m_layers[i].Update();
			}
		}
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x00003ED6 File Offset: 0x000022D6
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("settings", this.m_settings);
		info.AddValue("layers", this.m_layers.ToArray());
	}

	// Token: 0x04002D00 RID: 11520
	public Hashtable m_settings;

	// Token: 0x04002D01 RID: 11521
	public List<LevelLayer> m_layers;

	// Token: 0x04002D02 RID: 11522
	public LevelLayer m_currentLayer;

	// Token: 0x04002D03 RID: 11523
	public int m_currentLayerIndex;

	// Token: 0x04002D04 RID: 11524
	public bool m_changed;
}
