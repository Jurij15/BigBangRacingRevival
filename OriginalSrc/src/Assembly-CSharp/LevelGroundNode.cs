using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000016 RID: 22
[Serializable]
public class LevelGroundNode : GraphNode
{
	// Token: 0x060000B1 RID: 177 RVA: 0x00007708 File Offset: 0x00005B08
	public LevelGroundNode()
		: base(GraphNodeType.Invisible, null, "LevelGround", Vector3.zero, Vector3.zero, Vector3.one)
	{
		this.m_assembleClassType = typeof(BasicAssembledClass);
		LevelGroundNode.m_AGLayerCount = this.m_AGLayerData.Length;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00007768 File Offset: 0x00005B68
	public LevelGroundNode(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		try
		{
			LevelGroundNode.m_AGLayerCount = (int)info.GetValue("layerCount", typeof(int));
			if (LevelGroundNode.m_AGLayerCount > 5)
			{
				LevelGroundNode.m_AGLayerCount = 5;
			}
		}
		catch
		{
			LevelGroundNode.m_AGLayerCount = 5;
		}
		this.m_AGLayer = new AutoGeometryLayer[5];
		this.m_AGLayerData = new byte[5][];
		for (int i = 0; i < LevelGroundNode.m_AGLayerCount; i++)
		{
			this.m_AGLayerData[i] = (byte[])info.GetValue("layer" + i + "Data", typeof(byte[]));
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00007848 File Offset: 0x00005C48
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
		info.AddValue("layerCount", LevelGroundNode.m_AGLayerCount);
		for (int i = 0; i < LevelGroundNode.m_AGLayerCount; i++)
		{
			info.AddValue("layer" + i + "Data", this.m_AGLayer[i].m_bytes);
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x000078AC File Offset: 0x00005CAC
	public override GraphElement DeepCopy()
	{
		GraphElement graphElement;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			LevelGroundNode levelGroundNode = (LevelGroundNode)binaryFormatter.Deserialize(memoryStream);
			this.CopyAttributes(levelGroundNode, this);
			graphElement = levelGroundNode;
		}
		return graphElement;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00007910 File Offset: 0x00005D10
	public override void Assemble()
	{
		Debug.LogWarning("assemble ground");
		if (!this.m_assembled)
		{
			AutoGeometryManager.Free();
			AutoGeometryManager.Initialize(LevelManager.m_currentLevel.m_currentLayer.m_layerWidth, LevelManager.m_currentLevel.m_currentLayer.m_layerHeight, true);
			Ground[] array = new Ground[]
			{
				new SandGround(this),
				new IceGround(this),
				new MudGround(this),
				new MetalGround(this),
				new DangerousGround(this)
			};
			bool flag = this.m_AGLayerData[0] != null;
			for (int i = 0; i < array.Length; i++)
			{
				this.m_AGLayer[i] = AutoGeometryManager.AddLayer(array[i], 16, 17, 3f);
				this.m_AGLayer[i].m_index = i;
				if (flag && i < LevelGroundNode.m_AGLayerCount)
				{
					this.m_AGLayerData[i].CopyTo(this.m_AGLayer[i].m_bytes, 0);
					this.m_AGLayer[i].MarchTiles(new cpBB(0f, 0f, (float)AutoGeometryManager.m_width, (float)AutoGeometryManager.m_height));
					this.m_AGLayer[i].TakeSnapshot();
					this.m_AGLayer[i].CopyByteArrayToMaskTexture(this.m_AGLayer[i].m_maskTexture, this.m_AGLayer[i].m_bytes);
					if (!this.m_AGLayer[i].m_groundC.m_ground.m_destructible)
					{
						this.m_AGLayerData[i] = null;
						GC.Collect();
					}
					Debug.Log("Marching tiles...", null);
				}
				else if (this.m_AGLayer[i].m_groundC.m_ground.m_destructible)
				{
					this.m_AGLayerData[i] = new byte[this.m_AGLayer[i].m_bytes.Length];
				}
				else
				{
					this.m_AGLayerData[i] = null;
				}
			}
			AutoGeometryManager.ClearTileDirtyFlags();
			AutoGeometryManager.SetLayerShaders(PsState.m_activeGameLoop.m_context != PsMinigameContext.Editor);
			this.m_assembled = true;
		}
		else
		{
			AutoGeometryManager.SetLayerShaders(PsState.m_activeGameLoop.m_context != PsMinigameContext.Editor);
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00007B1C File Offset: 0x00005F1C
	public void SaveGroundBeforePlay()
	{
		for (int i = 0; i < LevelGroundNode.m_AGLayerCount; i++)
		{
			if (this.m_AGLayer[i].m_groundC.m_ground.m_destructible)
			{
				Array.Copy(this.m_AGLayer[i].m_bytes, this.m_AGLayerData[i], this.m_AGLayer[i].m_bytes.Length);
			}
		}
		AutoGeometryManager.ClearTileDirtyFlags();
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00007B8C File Offset: 0x00005F8C
	public void RevertGroundFromPlay()
	{
		for (int i = 0; i < LevelGroundNode.m_AGLayerCount; i++)
		{
			if (this.m_AGLayer[i].m_groundC.m_ground.m_destructible)
			{
				this.m_AGLayer[i].RevertAllDirtyTiles(this.m_AGLayerData[i]);
			}
		}
		AutoGeometryManager.ClearTileDirtyFlags();
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00007BE8 File Offset: 0x00005FE8
	public override void Clear(bool _isReset)
	{
		if (!_isReset)
		{
			Debug.Log("REMOVING ALL LEVEL GROUNDS", null);
			AutoGeometryManager.DestroyAllLayers();
			(LevelManager.m_currentLevel as Minigame).m_groundNode = null;
			for (int i = 0; i < LevelGroundNode.m_AGLayerCount; i++)
			{
				this.m_AGLayer[i] = null;
				this.m_AGLayerData[i] = null;
			}
			this.m_AGLayerData = null;
			this.m_AGLayer = null;
			base.Clear(_isReset);
		}
	}

	// Token: 0x04000090 RID: 144
	public const int LAYERS = 5;

	// Token: 0x04000091 RID: 145
	public AutoGeometryLayer[] m_AGLayer = new AutoGeometryLayer[5];

	// Token: 0x04000092 RID: 146
	public byte[][] m_AGLayerData = new byte[5][];

	// Token: 0x04000093 RID: 147
	public static int m_AGLayerCount;

	// Token: 0x04000094 RID: 148
	public Entity m_entity;
}
