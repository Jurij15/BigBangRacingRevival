using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004AB RID: 1195
public static class AutoGeometryManager
{
	// Token: 0x06002229 RID: 8745 RVA: 0x0018D474 File Offset: 0x0018B874
	public static void Initialize(int _width, int _height, bool _useDome)
	{
		AutoGeometryManager.m_useDome = _useDome;
		AutoGeometryManager.m_width = _width;
		AutoGeometryManager.m_height = _height;
		AutoGeometryManager.m_layerWidth = AutoGeometryManager.m_width / 16;
		AutoGeometryManager.m_layerHeight = AutoGeometryManager.m_height / 16;
		AutoGeometryManager.SetDomeSize(AutoGeometryManager.m_layerWidth, AutoGeometryManager.m_layerHeight, 0f);
		AutoGeometryManager.m_domeValueLookupBytes = new byte[AutoGeometryManager.m_layerWidth * AutoGeometryManager.m_layerHeight];
		AutoGeometryManager.m_maxValueLookupData = new AutoGeometryManager.lookupData[AutoGeometryManager.m_layerWidth * AutoGeometryManager.m_layerHeight];
		Debug.Log(string.Concat(new object[]
		{
			"Autogeometry manager initialize for world: ",
			AutoGeometryManager.m_width,
			"/",
			AutoGeometryManager.m_height
		}), null);
		AutoGeometryManager.m_tileCacheOffset = -new Vector2((float)AutoGeometryManager.m_width, (float)AutoGeometryManager.m_height) * 0.5f;
	}

	// Token: 0x0600222A RID: 8746 RVA: 0x0018D54B File Offset: 0x0018B94B
	public static void Free()
	{
		AutoGeometryManager.m_domeValueLookupBytes = null;
		AutoGeometryManager.m_maxValueLookupData = null;
		AutoGeometryManager.m_layers.Clear();
	}

	// Token: 0x0600222B RID: 8747 RVA: 0x0018D564 File Offset: 0x0018B964
	public static void SetDomeSize(int _width, int _height, float domeYOffset)
	{
		float num = 8f;
		AutoGeometryManager.m_domeCenter = new Vector2((float)AutoGeometryManager.m_layerWidth * 0.5f, domeYOffset / 16f);
		AutoGeometryManager.m_domeDiameter = new Vector2((float)_width - num, ((float)_height - num * ((float)_height / (float)_width)) * 2f);
		AutoGeometryManager.m_domeDiameterBias = 25f / (float)_width;
	}

	// Token: 0x0600222C RID: 8748 RVA: 0x0018D5C0 File Offset: 0x0018B9C0
	public static void Update()
	{
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			AutoGeometryManager.m_layers[i].Update();
		}
	}

	// Token: 0x0600222D RID: 8749 RVA: 0x0018D5F8 File Offset: 0x0018B9F8
	public static AutoGeometryLayer AddLayer(Ground _groundClass, int _tileSize, int _samplesPerTile, float _simplifyTreshold = 1f)
	{
		Debug.Log(AutoGeometryManager.m_layers.Count, null);
		_tileSize *= 16;
		if (AutoGeometryManager.m_layers.Count < 10)
		{
			AutoGeometryLayer autoGeometryLayer = new AutoGeometryLayer(_groundClass, _tileSize, _samplesPerTile, 16, _simplifyTreshold);
			AutoGeometryManager.m_layers.Add(autoGeometryLayer);
			return autoGeometryLayer;
		}
		Debug.LogError("No more layers allowed!");
		return null;
	}

	// Token: 0x0600222E RID: 8750 RVA: 0x0018D658 File Offset: 0x0018BA58
	public static void DestroyAllLayers()
	{
		while (AutoGeometryManager.m_layers.Count > 0)
		{
			int num = AutoGeometryManager.m_layers.Count - 1;
			AutoGeometryManager.m_layers[num].Destroy();
			AutoGeometryManager.m_layers.RemoveAt(num);
		}
	}

	// Token: 0x0600222F RID: 8751 RVA: 0x0018D6A4 File Offset: 0x0018BAA4
	public static void ClearTileDirtyFlags()
	{
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			AutoGeometryManager.m_layers[i].ClearAgTileDirtyFlags();
		}
	}

	// Token: 0x06002230 RID: 8752 RVA: 0x0018D6DC File Offset: 0x0018BADC
	public static void SetDirtyTileTracking(bool _track)
	{
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			AutoGeometryManager.m_layers[i].TrackDirtyTiles(_track);
		}
	}

	// Token: 0x06002231 RID: 8753 RVA: 0x0018D718 File Offset: 0x0018BB18
	private static int GetLayerByteOffset(float _x, float _y)
	{
		if (_x >= 1f && _y >= 1f && _x <= (float)(AutoGeometryManager.m_layerWidth - 1) && _y <= (float)(AutoGeometryManager.m_layerHeight - 1))
		{
			return (int)_y * AutoGeometryManager.m_layerWidth + (int)_x;
		}
		return -1;
	}

	// Token: 0x06002232 RID: 8754 RVA: 0x0018D764 File Offset: 0x0018BB64
	public static AutoGeometryManager.lookupData ReadMaxLayerValueFromWorldPos(Vector2 _worldPos)
	{
		_worldPos -= AutoGeometryManager.m_tileCacheOffset;
		Vector2 vector = _worldPos / 16f;
		if (vector.x >= 0f && vector.y >= 0f && vector.x <= (float)(AutoGeometryManager.m_layerWidth - 1) && vector.y <= (float)(AutoGeometryManager.m_layerHeight - 1))
		{
			int num = (int)vector.y * AutoGeometryManager.m_layerWidth + (int)vector.x;
			AutoGeometryManager.lookupData lookupData = AutoGeometryManager.m_maxValueLookupData[num];
			if (AutoGeometryManager.m_useDome)
			{
				lookupData.b = (byte)Mathf.Max((int)lookupData.b, (int)AutoGeometryManager.m_domeValueLookupBytes[num]);
			}
			return lookupData;
		}
		return default(AutoGeometryManager.lookupData);
	}

	// Token: 0x06002233 RID: 8755 RVA: 0x0018D830 File Offset: 0x0018BC30
	private static AutoGeometryManager.lookupData GetBiggestValueFromLayers(AutoGeometryLayer _selectedLayer, int _byteOffset)
	{
		AutoGeometryManager.lookupData lookupData = default(AutoGeometryManager.lookupData);
		lookupData.b = 0;
		lookupData.layerId = -1;
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			if (AutoGeometryManager.m_layers[i] != _selectedLayer)
			{
				byte b = AutoGeometryManager.m_layers[i].m_bytes[_byteOffset];
				if (b > 0 && b > lookupData.b)
				{
					lookupData.b = b;
					lookupData.layerId = i;
				}
			}
		}
		return lookupData;
	}

	// Token: 0x06002234 RID: 8756 RVA: 0x0018D8BC File Offset: 0x0018BCBC
	public static void UpdateMaxValueLookupTable(AutoGeometryLayer _selectedLayer, LookupUpdateMode _lookupUpdateMode, bool _updateDome = false)
	{
		Debug.Log(string.Concat(new object[] { "Lookup updated: ", _lookupUpdateMode, "/ Dome Updated: ", _updateDome }), null);
		if (_lookupUpdateMode != LookupUpdateMode.LEVEL_LOADED)
		{
			if (_lookupUpdateMode != LookupUpdateMode.IN_EDITOR)
			{
				if (_lookupUpdateMode == LookupUpdateMode.NEW_LEVEL)
				{
					for (int i = 0; i < AutoGeometryManager.m_layerWidth * AutoGeometryManager.m_layerHeight; i++)
					{
						AutoGeometryManager.m_maxValueLookupData[i].b = 0;
						AutoGeometryManager.m_maxValueLookupData[i].layerId = -1;
					}
				}
			}
			else
			{
				int num = AutoGeometryManager.lastUpdatedLookupLayer;
				int index = _selectedLayer.m_index;
				if (num == index)
				{
					return;
				}
				for (int j = 0; j < AutoGeometryManager.m_layerWidth * AutoGeometryManager.m_layerHeight; j++)
				{
					AutoGeometryManager.lookupData lookupData = AutoGeometryManager.m_maxValueLookupData[j];
					if (lookupData.layerId == index)
					{
						AutoGeometryManager.m_maxValueLookupData[j] = AutoGeometryManager.GetBiggestValueFromLayers(_selectedLayer, j);
					}
					else
					{
						byte b = AutoGeometryManager.m_layers[num].m_bytes[j];
						if (b > lookupData.b)
						{
							AutoGeometryManager.m_maxValueLookupData[j].b = b;
							AutoGeometryManager.m_maxValueLookupData[j].layerId = num;
						}
					}
				}
			}
		}
		else
		{
			for (int k = 0; k < AutoGeometryManager.m_layerWidth * AutoGeometryManager.m_layerHeight; k++)
			{
				AutoGeometryManager.m_maxValueLookupData[k] = AutoGeometryManager.GetBiggestValueFromLayers(_selectedLayer, k);
			}
		}
		AutoGeometryManager.lastUpdatedLookupLayer = _selectedLayer.m_index;
		if (AutoGeometryManager.m_useDome && _updateDome)
		{
			float num2 = (float)AutoGeometryManager.m_layerWidth * 0.5f - AutoGeometryManager.m_domeDiameter.x * 0.25f;
			float num3 = (float)AutoGeometryManager.m_layerWidth * 0.5f + AutoGeometryManager.m_domeDiameter.x * 0.25f;
			float num4 = AutoGeometryManager.m_domeDiameter.y * 0.25f;
			for (int l = 0; l < AutoGeometryManager.m_layerHeight; l++)
			{
				for (int m = 0; m < AutoGeometryManager.m_layerWidth; m++)
				{
					int layerByteOffset = AutoGeometryManager.GetLayerByteOffset((float)m, (float)l);
					if (layerByteOffset >= 0)
					{
						bool flag = false;
						if ((float)m > num2 && (float)m < num3 && (float)l < num4)
						{
							flag = true;
						}
						if (!flag)
						{
							float num5 = ToolBox.PointInsideEllipse(new Vector2((float)m, (float)l), AutoGeometryManager.m_domeCenter, AutoGeometryManager.m_domeDiameter * 0.5f, 0f);
							if (num5 > 1f)
							{
								float positionBetween = ToolBox.getPositionBetween(num5 - 1f, 0f, AutoGeometryManager.m_domeDiameterBias);
								byte b2 = (byte)ToolBox.limitBetween(positionBetween * 255f, 0f, 255f);
								AutoGeometryManager.m_domeValueLookupBytes[layerByteOffset] = b2;
							}
							else
							{
								AutoGeometryManager.m_domeValueLookupBytes[layerByteOffset] = 0;
							}
						}
						else
						{
							AutoGeometryManager.m_domeValueLookupBytes[layerByteOffset] = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x06002235 RID: 8757 RVA: 0x0018DBBC File Offset: 0x0018BFBC
	public static int GetLayerAtWorldPos(Vector2 _worldPosition, byte _tolerance = 0)
	{
		byte b = 127;
		int num = -1;
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			byte b2 = AutoGeometryManager.m_layers[i].ReadDataFromWorldPos(_worldPosition);
			if (b2 > _tolerance && b2 > b)
			{
				num = i;
				b = b2;
			}
		}
		return num;
	}

	// Token: 0x06002236 RID: 8758 RVA: 0x0018DC10 File Offset: 0x0018C010
	public static void HighlightLayer(AutoGeometryLayer _layer, float _amount, float _speed = 0.1f)
	{
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			float num = 0f;
			if (_layer == AutoGeometryManager.m_layers[i])
			{
				num = _amount;
			}
			AutoGeometryManager.m_layers[i].SetHighlight(num, _speed, false);
		}
	}

	// Token: 0x06002237 RID: 8759 RVA: 0x0018DC64 File Offset: 0x0018C064
	public static void SetLayerShaders(bool _inGame)
	{
		Debug.Log("Setting Ground shaders to " + ((!_inGame) ? "EDITOR" : "INGAME"), null);
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			AutoGeometryManager.m_layers[i].SetLayerShaders(_inGame);
		}
	}

	// Token: 0x06002238 RID: 8760 RVA: 0x0018DCC4 File Offset: 0x0018C0C4
	public static void SetLayerShaderPropertiesInEditor(Rect _cameraRect)
	{
		for (int i = 0; i < AutoGeometryManager.m_layers.Count; i++)
		{
			AutoGeometryManager.m_layers[i].SetLayerShaderPropertiesInEditor(_cameraRect);
		}
	}

	// Token: 0x06002239 RID: 8761 RVA: 0x0018DD00 File Offset: 0x0018C100
	public static void PaintWithBrush(AutoGeometryLayer _layer, AutoGeometryBrush _brush, Vector2 _pos, bool _additive, bool _soften, bool _updateAgMask = true)
	{
		int num = AutoGeometryManager.m_layers.IndexOf(_layer);
		AutoGeometryManager.m_layers[num].PaintWithBrush(_brush, _pos, (!_additive) ? AGDrawMode.SUB : AGDrawMode.ADD, _soften, ref AutoGeometryManager.m_layers[num].m_bytes, true, _updateAgMask);
	}

	// Token: 0x04002860 RID: 10336
	public const int MAX_LAYERS = 10;

	// Token: 0x04002861 RID: 10337
	public const int TEXEL_SCALE = 16;

	// Token: 0x04002862 RID: 10338
	public static bool m_useDome = false;

	// Token: 0x04002863 RID: 10339
	public static int m_width;

	// Token: 0x04002864 RID: 10340
	public static int m_height;

	// Token: 0x04002865 RID: 10341
	public static Vector2 m_tileCacheOffset;

	// Token: 0x04002866 RID: 10342
	public static List<AutoGeometryLayer> m_layers = new List<AutoGeometryLayer>();

	// Token: 0x04002867 RID: 10343
	public static int m_layerWidth;

	// Token: 0x04002868 RID: 10344
	public static int m_layerHeight;

	// Token: 0x04002869 RID: 10345
	public static AutoGeometryManager.lookupData[] m_maxValueLookupData;

	// Token: 0x0400286A RID: 10346
	public static byte[] m_domeValueLookupBytes;

	// Token: 0x0400286B RID: 10347
	public static Vector2 m_domeCenter;

	// Token: 0x0400286C RID: 10348
	public static Vector2 m_domeDiameter;

	// Token: 0x0400286D RID: 10349
	public static float m_domeDiameterBias;

	// Token: 0x0400286E RID: 10350
	private static int lastUpdatedLookupLayer = -1;

	// Token: 0x020004AC RID: 1196
	public struct lookupData
	{
		// Token: 0x0400286F RID: 10351
		public byte b;

		// Token: 0x04002870 RID: 10352
		public int layerId;
	}
}
