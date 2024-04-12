using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004A9 RID: 1193
public class AutoGeometryLayer
{
	// Token: 0x060021FA RID: 8698 RVA: 0x0018A3A8 File Offset: 0x001887A8
	public AutoGeometryLayer(Ground _groundClass, int _tileSize, int _samplesPerTile, int _texelScale, float _simplifyTreshold = 1f)
	{
		this.m_groundBodyEntity = EntityManager.AddEntity("AGLayerGroundBodyEntity");
		this.m_groundBodyEntity.m_persistent = true;
		this.m_groundBodyTC = TransformS.AddComponent(this.m_groundBodyEntity, "AGLayerGroundBodyTC");
		this.m_groundBody = ChipmunkProS.AddStaticBody(this.m_groundBodyTC, null);
		this.m_groundC = PsS.AddGround(this.m_groundBodyEntity, _groundClass);
		this.m_groundBody.customComponent = this.m_groundC;
		this.m_tileOffset.z = this.m_tileOffset.z + this.m_groundC.m_ground.m_zOffset;
		this.m_tempAgPolys = new DynamicArray<AgPolygon>(50, 0.5f, 0.25f, 0.5f);
		this.m_width = AutoGeometryManager.m_width / _texelScale;
		this.m_height = AutoGeometryManager.m_height / _texelScale;
		this.m_texelScale = _texelScale;
		this.m_tileSize = _tileSize;
		this.m_bytes = new byte[this.m_width * this.m_height];
		cpBB cpBB = new cpBB(0.5f * (float)this.m_texelScale, 0.5f * (float)this.m_texelScale, ((float)this.m_width - 0.5f) * (float)this.m_texelScale, ((float)this.m_height - 0.5f) * (float)this.m_texelScale);
		this.m_sampler = AutoGeometryWrapper.agSimpleSamplerNew(this.m_width, this.m_height, cpBB, this.m_bytes);
		this.m_xTiles = AutoGeometryManager.m_width / _tileSize;
		this.m_yTiles = AutoGeometryManager.m_height / _tileSize;
		int num = this.m_xTiles * this.m_yTiles;
		this.m_tileCache = AutoGeometryWrapper.agBasicTileCacheNew(this.m_sampler, ChipmunkProWrapper.ucpGetSpace(), (float)_tileSize, num, (float)_samplesPerTile, false);
		this.m_dirtyTileList = new IntPtr[num];
		this.m_ensureRect = new cpBB(0f, 0f, 0f, 0f);
		this.m_updateEnsureRect = false;
		AutoGeometryWrapper.agBasicTileCacheSetOffsets(this.m_tileCache, AutoGeometryManager.m_tileCacheOffset, Vector2.zero);
		AutoGeometryWrapper.agBasicTileCacheSetMarchProperties(this.m_tileCache, _groundClass.m_marchHard, _simplifyTreshold);
		ucpShapeFilter ucpShapeFilter = default(ucpShapeFilter);
		ucpShapeFilter.categories = 0U;
		ucpShapeFilter.group = 0U;
		ucpShapeFilter.mask = 0U;
		AutoGeometryWrapper.agBasicTileCacheSetSegmentProperties(this.m_tileCache, 5f, 0.5f, 0.5f, ucpShapeFilter, 0U);
		this.m_beltMaterial = Object.Instantiate<Material>(ResourceManager.GetMaterial(this.m_groundC.m_ground.m_beltMaterialResourceName));
		this.m_frontMaterial = Object.Instantiate<Material>(ResourceManager.GetMaterial(this.m_groundC.m_ground.m_frontMaterialResourceName));
		if (this.m_groundC.m_ground.m_groundPropStorageName != null)
		{
			this.m_groundPropStorage = Object.Instantiate<GameObject>(ResourceManager.GetGameObject(this.m_groundC.m_ground.m_groundPropStorageName)).GetComponent<AGPropStorage>();
			this.m_groundPropStorage.Initialize();
		}
		this.m_frontMaterial.SetColor("_Emission", Color.black);
		this.m_beltMaterial.SetColor("_Emission", Color.black);
		this.InitMaskTexture();
		this.m_frontMaterial.SetTexture("_MaskTex", this.m_maskTexture);
		this.m_frontMaterial.SetTexture("_UnitMask", CameraS.m_mainCameraRenderTexture);
		Debug.Log(string.Concat(new object[] { "NEW AUTOGEOMETRY LAYER WITH ", this.m_xTiles, " x ", this.m_yTiles, " tiles. TotalTiles: ", num, ". TileSize: ", _tileSize }), null);
	}

	// Token: 0x060021FB RID: 8699 RVA: 0x0018A7B5 File Offset: 0x00188BB5
	public void TrackDirtyTiles(bool _track)
	{
		this.m_trackDirtyTiles = _track;
	}

	// Token: 0x060021FC RID: 8700 RVA: 0x0018A7BE File Offset: 0x00188BBE
	public void SetLayerShaders(bool _inGame)
	{
		if (_inGame)
		{
			this.m_frontMaterial.shader = Shader.Find("WOE/Ground/GroundFrontWithBandGame");
		}
		else
		{
			this.m_frontMaterial.shader = Shader.Find("WOE/Ground/GroundFrontWithBandEditor");
		}
	}

	// Token: 0x060021FD RID: 8701 RVA: 0x0018A7F8 File Offset: 0x00188BF8
	public void SetLayerShaderPropertiesInEditor(Rect _cameraRect)
	{
		float height = _cameraRect.height;
		this.m_frontMaterial.SetFloat("_ViewportYScale", height);
	}

	// Token: 0x060021FE RID: 8702 RVA: 0x0018A820 File Offset: 0x00188C20
	public void SetHighlight(float _emissionAmount, float _speed = 0.1f, bool _flash = false)
	{
		TweenS.RemoveAllTweensFromEntity(this.m_groundBodyEntity);
		this.m_highlightTween = null;
		this.m_highlightGlowTween = null;
		float r = this.m_frontMaterial.GetColor("_Emission").r;
		if (!_flash)
		{
			if (_emissionAmount > 0f)
			{
				this.m_highlightTween = TweenS.AddTween(this.m_groundBodyEntity, TweenStyle.Linear, new Vector3(r, 0f, 0f), new Vector3(_emissionAmount, 0f, 0f), _speed, 0f);
				this.m_highlightGlowTween = TweenS.AddTween(this.m_groundBodyEntity, TweenStyle.QuadInOut, new Vector3(_emissionAmount, 0f, 0f), new Vector3(_emissionAmount * 0.5f, 0f, 0f), 1f, _speed);
				TweenS.SetAdditionalTweenProperties(this.m_highlightGlowTween, -1, true, TweenStyle.QuadInOut);
			}
			else
			{
				this.m_highlightTween = TweenS.AddTween(this.m_groundBodyEntity, TweenStyle.Linear, new Vector3(r, 0f, 0f), new Vector3(_emissionAmount, 0f, 0f), _speed, 0f);
			}
		}
		else
		{
			this.m_highlightTween = TweenS.AddTween(this.m_groundBodyEntity, TweenStyle.Linear, new Vector3(r, 0f, 0f), new Vector3(_emissionAmount, 0f, 0f), 0.2f, 0f);
			TweenS.SetAdditionalTweenProperties(this.m_highlightTween, 0, true, TweenStyle.Linear);
		}
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x0018A980 File Offset: 0x00188D80
	public ByteBlock ReadByteBlock(ref byte[] _bytes, cpBB _worldBB)
	{
		cpBB cpBB = _worldBB;
		_worldBB.l /= (float)this.m_texelScale;
		_worldBB.b /= (float)this.m_texelScale;
		_worldBB.r /= (float)this.m_texelScale;
		_worldBB.t /= (float)this.m_texelScale;
		int num = (int)_worldBB.r - (int)_worldBB.l;
		int num2 = (int)_worldBB.t - (int)_worldBB.b;
		ByteBlock byteBlock = default(ByteBlock);
		if (num > 0 && num2 > 0)
		{
			byteBlock.xOffset = (int)_worldBB.l;
			byteBlock.yOffset = (int)_worldBB.b;
			byteBlock.width = num;
			byteBlock.height = num2;
			byteBlock.bytes = new byte[byteBlock.width * byteBlock.height];
			byteBlock.worldBB = cpBB;
			int num3 = 0;
			for (int i = (int)_worldBB.b; i < (int)_worldBB.t; i++)
			{
				int num4 = 0;
				for (int j = (int)_worldBB.l; j < (int)_worldBB.r; j++)
				{
					if (j >= 1 && i >= 1 && j <= this.m_width - 2 && i <= this.m_height - 2)
					{
						int num5 = i * this.m_width + j;
						int num6 = num3 * byteBlock.width + num4;
						byteBlock.bytes[num6] = _bytes[num5];
					}
					num4++;
				}
				num3++;
			}
		}
		return byteBlock;
	}

	// Token: 0x06002200 RID: 8704 RVA: 0x0018AB28 File Offset: 0x00188F28
	public void WriteByteBlock(ByteBlock _block, ref byte[] _bytes)
	{
		for (int i = 0; i < _block.height; i++)
		{
			for (int j = 0; j < _block.width; j++)
			{
				int num = j + _block.xOffset;
				int num2 = i + _block.yOffset;
				if (num >= 1 && num2 >= 1 && num <= this.m_width - 2 && num2 <= this.m_height - 2)
				{
					int num3 = num2 * this.m_width + num;
					int num4 = i * _block.width + j;
					_bytes[num3] = _block.bytes[num4];
				}
			}
		}
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x0018ABCB File Offset: 0x00188FCB
	private void InitMaskTexture()
	{
		this.m_maskTexture = new Texture2D(this.m_width, this.m_height, 1, false);
		this.m_maskTexture.wrapMode = 1;
		this.m_maskTexture.anisoLevel = 0;
		this.m_maskTexture.filterMode = 1;
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x0018AC0C File Offset: 0x0018900C
	public void CopyByteArrayToMaskTexture(Texture2D _texture, byte[] _bytes)
	{
		if (_texture != null)
		{
			Color[] array = new Color[_bytes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Color(0f, 0f, 0f, (float)_bytes[i] / 255f);
			}
			_texture.SetPixels(array);
			_texture.Apply();
		}
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x0018AC79 File Offset: 0x00189079
	public void TakeSnapshot()
	{
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x0018AC7B File Offset: 0x0018907B
	public void ResetUndoRect()
	{
		this.m_undoRect = new cpBB(0f, 0f, 0f, 0f);
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x0018AC9C File Offset: 0x0018909C
	public cpBB GetUndoRect()
	{
		return this.m_undoRect;
	}

	// Token: 0x06002206 RID: 8710 RVA: 0x0018ACA4 File Offset: 0x001890A4
	public bool HasUndoRect()
	{
		return this.m_undoRect.r - this.m_undoRect.l > 0f || this.m_undoRect.t - this.m_undoRect.b > 0f;
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x0018ACF4 File Offset: 0x001890F4
	public void RevertAllDirtyTiles(byte[] source)
	{
		List<AgTile> list = new List<AgTile>();
		IEnumerator enumerator = this.m_tileHash.Values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AgTile agTile = (AgTile)obj;
				if (agTile.dirty)
				{
					list.Add(agTile);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		cpBB cpBB = default(cpBB);
		foreach (AgTile agTile2 in list)
		{
			if (agTile2.dirty)
			{
				cpBB bb = agTile2.bb;
				bb.l /= (float)this.m_texelScale;
				bb.b /= (float)this.m_texelScale;
				bb.r /= (float)this.m_texelScale;
				bb.t /= (float)this.m_texelScale;
				for (int i = (int)bb.b; i < (int)bb.t; i++)
				{
					for (int j = (int)bb.l; j < (int)bb.r; j++)
					{
						int num = i * this.m_width + j;
						if (j >= 1 && i >= 1 && j <= this.m_width - 2 && i <= this.m_height - 2)
						{
							this.m_bytes[num] = source[num];
						}
					}
				}
				cpBB bb2 = agTile2.bb;
				bb2.b += 16f;
				bb2.t -= 16f;
				bb2.l += 16f;
				bb2.r -= 16f;
				AutoGeometryWrapper.agBasicTileCacheMarkDirtyRect(this.m_tileCache, bb2);
				if (cpBB.isNull())
				{
					cpBB = agTile2.bb;
				}
				else
				{
					cpBB = ChipmunkProWrapper.ucpBBMerge(cpBB, agTile2.bb);
				}
			}
		}
		if (!cpBB.isNull())
		{
			AutoGeometryWrapper.agBasicTileCacheEnsureRect(this.m_tileCache, this.clampWorldBB(cpBB));
			this.m_dirty = true;
			this.UpdateSegments(false, true);
			this.CopyByteArrayToMaskTexture(this.m_maskTexture, this.m_bytes);
		}
	}

	// Token: 0x06002208 RID: 8712 RVA: 0x0018AF9C File Offset: 0x0018939C
	private cpBB clampWorldBB(cpBB _bb)
	{
		_bb.l = ToolBox.limitBetween(_bb.l, 16f, (float)(AutoGeometryManager.m_width - 32));
		_bb.b = ToolBox.limitBetween(_bb.b, 16f, (float)(AutoGeometryManager.m_height - 32));
		_bb.r = ToolBox.limitBetween(_bb.r, 16f, (float)(AutoGeometryManager.m_width - 32));
		_bb.t = ToolBox.limitBetween(_bb.t, 16f, (float)(AutoGeometryManager.m_height - 32));
		return _bb;
	}

	// Token: 0x06002209 RID: 8713 RVA: 0x0018B02E File Offset: 0x0018942E
	public void MarchTiles(cpBB _fullRect)
	{
		_fullRect = this.clampWorldBB(_fullRect);
		AutoGeometryWrapper.agBasicTileCacheMarkDirtyRect(this.m_tileCache, _fullRect);
		AutoGeometryWrapper.agBasicTileCacheEnsureRect(this.m_tileCache, _fullRect);
		this.m_dirty = true;
		this.UpdateSegments(false, true);
	}

	// Token: 0x0600220A RID: 8714 RVA: 0x0018B060 File Offset: 0x00189460
	public float ReadDataFromWorldPosAsFloat(Vector2 _worldPos)
	{
		return (float)this.ReadDataFromWorldPos(_worldPos) / 255f;
	}

	// Token: 0x0600220B RID: 8715 RVA: 0x0018B070 File Offset: 0x00189470
	public byte ReadDataFromWorldPos(Vector2 _worldPos)
	{
		_worldPos -= AutoGeometryManager.m_tileCacheOffset;
		Vector2 vector = _worldPos / (float)this.m_texelScale;
		if (vector.x >= 0f && vector.y >= 0f && vector.x <= (float)(this.m_width - 1) && vector.y <= (float)(this.m_height - 1))
		{
			int num = (int)vector.y * this.m_width + (int)vector.x;
			return this.m_bytes[num];
		}
		return 0;
	}

	// Token: 0x0600220C RID: 8716 RVA: 0x0018B108 File Offset: 0x00189508
	public byte ReadDataFromImagePos(Vector2 _imagePos)
	{
		Vector2 vector = _imagePos;
		if (vector.x >= 0f && vector.y >= 0f && vector.x <= (float)(this.m_width - 1) && vector.y <= (float)(this.m_height - 1))
		{
			int num = (int)vector.y * this.m_width + (int)vector.x;
			return this.m_bytes[num];
		}
		return 0;
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x0018B186 File Offset: 0x00189586
	public byte ReadDataFromDataPos(int _dataPos)
	{
		return this.m_bytes[_dataPos];
	}

	// Token: 0x0600220E RID: 8718 RVA: 0x0018B190 File Offset: 0x00189590
	private bool IsTileFilled(cpBB _tileBB)
	{
		Vector2 vector;
		vector..ctor(_tileBB.l - (_tileBB.l - _tileBB.r) * 0.5f, _tileBB.t - (_tileBB.t - _tileBB.b) * 0.5f);
		Vector2 vector2 = vector / (float)this.m_texelScale;
		int num = this.m_tileSize / this.m_texelScale / 2;
		int num2 = (int)vector2.x - num / 2;
		int num3 = (int)vector2.y - num / 2;
		int num4 = 0;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				num4 += (int)this.ReadDataFromImagePos(new Vector2((float)(num2 + j), (float)(num3 + i)));
			}
		}
		return num4 > 128 * num * num;
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x0018B270 File Offset: 0x00189670
	private void PlotPixel(float _val, int _targetByteOffset, AGDrawMode _drawMode, ref byte[] _bytes)
	{
		byte b = _bytes[_targetByteOffset];
		float num = (float)b / 255f;
		byte b2 = 0;
		if (_drawMode == AGDrawMode.ADD)
		{
			byte b3 = AutoGeometryManager.m_maxValueLookupData[_targetByteOffset].b;
			if (AutoGeometryManager.m_useDome)
			{
				b3 = (byte)Mathf.Max((int)b3, (int)AutoGeometryManager.m_domeValueLookupBytes[_targetByteOffset]);
			}
			float num2 = 1f - num;
			float num3 = 1f - _val;
			float num4 = 1f - num2 * num3;
			float num5 = (float)b3 / 255f;
			num4 = Mathf.Min(num4, 1f - num5);
			b2 = (byte)(num4 * 255f);
		}
		else if (_drawMode == AGDrawMode.SUB)
		{
			float num6 = num;
			float num7 = 1f - _val;
			b2 = (byte)(num6 * num7 * 255f);
		}
		if (this.m_bytes[_targetByteOffset] != b2)
		{
			this.m_plotDidChangePixel = true;
		}
		_bytes[_targetByteOffset] = b2;
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x0018B348 File Offset: 0x00189748
	private int GetByteOffset(float _x, float _y)
	{
		if (_x >= 1f && _y >= 1f && _x <= (float)(this.m_width - 2) && _y <= (float)(this.m_height - 2))
		{
			return (int)_y * this.m_width + (int)_x;
		}
		return -1;
	}

	// Token: 0x06002211 RID: 8721 RVA: 0x0018B398 File Offset: 0x00189798
	public void PregenerateRandomGround(float _maxHeight)
	{
		float num = (float)Random.Range(0, 360);
		int num2 = 0;
		float num3 = 1f;
		float num4 = num3;
		AutoGeometryBrush autoGeometryBrush = new AutoGeometryBrush(7f, false, 0.5f, 0f);
		for (int i = 0; i < this.m_width; i++)
		{
			float num5 = Mathf.Abs(Mathf.Sin(num * 0.017453292f)) * _maxHeight * 1f;
			float num6 = -15f;
			if (num2 < 0)
			{
				num3 = Mathf.Sign(Random.Range(-1f, 1f)) * Random.Range(0.5f, 2f);
				num2 = Random.Range(20, 60);
			}
			num2--;
			num += num4;
			num4 += (num3 - num4) * 0.05f;
			this.PaintWithBrush(autoGeometryBrush, new Vector2((float)i, num6 + num5) * (float)this.m_texelScale + AutoGeometryManager.m_tileCacheOffset, AGDrawMode.ADD, false, ref this.m_bytes, false, false);
			int num7 = 0;
			while ((float)num7 < num5)
			{
				float num8 = 1f;
				int byteOffset = this.GetByteOffset((float)i, num6 + (float)num7);
				if (byteOffset > 0)
				{
					this.PlotPixel(num8, byteOffset, AGDrawMode.ADD, ref this.m_bytes);
				}
				num7++;
			}
		}
		autoGeometryBrush.Destroy();
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x0018B4E0 File Offset: 0x001898E0
	public void PaintWithBrush(AutoGeometryBrush _brush, Vector2 _pos, AGDrawMode _drawMode, bool _soften, ref byte[] _bytes, bool _updateAG = true, bool _updateMask = false)
	{
		if (this.m_maskTexture == null)
		{
			_updateMask = false;
		}
		_pos -= AutoGeometryManager.m_tileCacheOffset;
		Vector2 vector = _pos;
		this.m_plotDidChangePixel = false;
		_pos /= (float)this.m_texelScale;
		_pos -= new Vector2((float)_brush.m_width * 0.5f, (float)_brush.m_height * 0.5f);
		for (int i = 0; i < _brush.m_height; i++)
		{
			int num = i * _brush.m_width;
			for (int j = 0; j < _brush.m_width; j++)
			{
				float num2 = _pos.x + (float)j;
				float num3 = _pos.y + (float)i;
				float num4 = 1f;
				float num5 = (float)_brush.m_bytes[num + j] / 255f;
				int num15;
				if (_soften)
				{
					float num6 = num2 - Mathf.Floor(num2);
					float num7 = num3 - Mathf.Floor(num3);
					int num8 = ((num6 >= 0.5f) ? 1 : (-1));
					int num9 = ((num7 >= 0.5f) ? 1 : (-1));
					float num10 = Mathf.InverseLerp(0f, 0.5f, Mathf.Abs(num6 - 0.5f));
					float num11 = Mathf.InverseLerp(0f, 0.5f, Mathf.Abs(num7 - 0.5f));
					float num12 = 0.25f * num10 * (1f - num11);
					float num13 = 0.25f * num11 * (1f - num10);
					float num14 = 0.25f * num10 * num11;
					num4 = 1f - (num12 + num13 + num14);
					num15 = this.GetByteOffset(num2 + (float)num8, num3);
					if (num15 > 0)
					{
						this.PlotPixel(num12 * num5, num15, _drawMode, ref _bytes);
					}
					num15 = this.GetByteOffset(num2, num3 + (float)num9);
					if (num15 > 0)
					{
						this.PlotPixel(num13 * num5, num15, _drawMode, ref _bytes);
					}
					num15 = this.GetByteOffset(num2 + (float)num8, num3 + (float)num9);
					if (num15 > 0)
					{
						this.PlotPixel(num14 * num5, num15, _drawMode, ref _bytes);
					}
				}
				num15 = this.GetByteOffset(num2, num3);
				if (num15 > 0)
				{
					this.PlotPixel(num4 * num5, num15, _drawMode, ref _bytes);
				}
			}
		}
		if (this.m_plotDidChangePixel)
		{
			if (_updateMask)
			{
				this.m_maskTextureModified = true;
				int num16 = _brush.m_width + 2;
				int num17 = _brush.m_height + 2;
				_pos.x -= 1f;
				_pos.y -= 1f;
				if ((int)_pos.x < 1)
				{
					num16 -= 1 - (int)_pos.x;
					_pos.x = 1f;
				}
				if ((int)_pos.y < 1)
				{
					num17 -= 1 - (int)_pos.y;
					_pos.y = 1f;
				}
				if ((int)_pos.x + num16 >= this.m_width - 2)
				{
					num16 -= (int)_pos.x + num16 - (this.m_width - 1);
				}
				if ((int)_pos.y + num17 >= this.m_height - 2)
				{
					num17 -= (int)_pos.y + num17 - (this.m_height - 1);
				}
				Color[] array = new Color[num16 * num17];
				Color color;
				color..ctor(0f, 0f, 0f, 0f);
				for (int k = 0; k < num17; k++)
				{
					int num18 = k * num16;
					for (int l = 0; l < num16; l++)
					{
						float num19 = _pos.x + (float)l;
						float num20 = _pos.y + (float)k;
						int byteOffset = this.GetByteOffset(num19, num20);
						if (byteOffset >= 0)
						{
							array[num18 + l] = new Color(0f, 0f, 0f, (float)_bytes[byteOffset] / 255f);
						}
						else
						{
							array[num18 + l] = color;
						}
					}
				}
				this.m_maskTexture.SetPixels((int)_pos.x, (int)_pos.y, num16, num17, array);
			}
			if (_updateAG)
			{
				if (_drawMode == AGDrawMode.SUB)
				{
					Vector2 vector2 = new Vector2((float)(_brush.m_width + 2), (float)(_brush.m_height + 2)) * (float)this.m_texelScale;
					cpBB cpBB = new cpBB(vector.x - vector2.x / 2f, vector.y - vector2.y / 2f, vector.x + vector2.x / 2f, vector.y + vector2.y / 2f);
					cpBB = this.clampWorldBB(cpBB);
					int num21 = AutoGeometryWrapper.agBasicTileCacheGetTileCountInRect(this.m_tileCache, cpBB);
					AutoGeometryWrapper.agBasicTileCacheGetTilesInRect(this.m_tileCache, cpBB, this.m_brushPaintTileList);
					for (int m = 0; m < num21; m++)
					{
						IntPtr intPtr = this.m_brushPaintTileList[m];
						AgTile agTile = this.GetAgTile(intPtr, false);
						if (agTile != null)
						{
							AutoGeometryWrapper.agCachedTileSetDirty(intPtr, true);
						}
					}
					this.updateRect(vector, vector2, false);
				}
				else
				{
					this.updateRect(vector, new Vector2((float)(_brush.m_width + 2), (float)(_brush.m_height + 2)) * (float)this.m_texelScale, true);
				}
			}
		}
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x0018BA4C File Offset: 0x00189E4C
	private void updateRect(Vector2 _pos, Vector2 _area, bool _markDirty = true)
	{
		cpBB cpBB = new cpBB(_pos.x - _area.x / 2f, _pos.y - _area.y / 2f, _pos.x + _area.x / 2f, _pos.y + _area.y / 2f);
		cpBB = this.clampWorldBB(cpBB);
		if (_markDirty)
		{
			AutoGeometryWrapper.agBasicTileCacheMarkDirtyRect(this.m_tileCache, cpBB);
		}
		if (!this.m_updateEnsureRect)
		{
			this.m_ensureRect = cpBB;
			this.m_updateEnsureRect = true;
		}
		else
		{
			cpBB cpBB2 = ChipmunkProWrapper.ucpBBMerge(cpBB, this.m_ensureRect);
			this.m_ensureRect = cpBB2;
		}
		if (this.HasUndoRect())
		{
			this.m_undoRect = ChipmunkProWrapper.ucpBBMerge(this.m_undoRect, cpBB);
		}
		else
		{
			this.m_undoRect = cpBB;
		}
		this.m_dirty = true;
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x0018BB30 File Offset: 0x00189F30
	private void FixEdgeVertNormalsWithNeighbour(AgTile _tile, int x, int y)
	{
		AgTile tileNeighbour = this.GetTileNeighbour(_tile, x, y);
		bool flag = false;
		if (tileNeighbour != null && tileNeighbour.edgeVerts != null && tileNeighbour.edgeVerts.Length > 4)
		{
			for (int i = 0; i < _tile.edgeVerts.Length - 4; i++)
			{
				for (int j = 0; j < tileNeighbour.edgeVerts.Length - 4; j++)
				{
					if (_tile.edgeVerts[i].pos == tileNeighbour.edgeVerts[j].pos)
					{
						Vector2 normal = _tile.edgeVerts[i].normal;
						Vector2 normal2 = tileNeighbour.edgeVerts[j].normal;
						Vector2 vector = (normal + normal2) / 2f;
						_tile.edgeVerts[i].fixedNormal = vector;
						_tile.edgeVerts[i].cTangent = new Vector2(normal2.y, -normal2.x);
						if (vector != tileNeighbour.edgeVerts[j].fixedNormal)
						{
							tileNeighbour.edgeVerts[j].fixedNormal = vector;
							tileNeighbour.edgeVerts[j].cTangent = new Vector2(normal.y, -normal.x);
							flag = true;
						}
					}
				}
			}
		}
		if (flag && !this.m_geometryUpdateQueue.Contains(tileNeighbour))
		{
			this.m_geometryUpdateQueue.Add(tileNeighbour);
		}
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x0018BCB8 File Offset: 0x0018A0B8
	private Vector2 GetFixedEdgeVertNormalForPoint(AgTile _tile, Vector2 _pos)
	{
		if (_tile.edgeVerts != null)
		{
			for (int i = 0; i < _tile.edgeVerts.Length - 4; i++)
			{
				if (_pos == _tile.edgeVerts[i].pos)
				{
					return _tile.edgeVerts[i].fixedNormal;
				}
			}
		}
		return Vector2.one;
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x0018BD20 File Offset: 0x0018A120
	public void GenerateEdgeVertArray(AgTile _tile)
	{
		IntPtr intPtr = AutoGeometryWrapper.agCachedTileGetPolylineSet(_tile.tilePtr);
		int num = AutoGeometryWrapper.agPolylineSetGetLineCount(intPtr);
		AutoGeometryWrapper.agPolylineSetGetLines(intPtr, this.m_tempPolylines);
		_tile.edgeVerts = null;
		if (num > 0)
		{
			List<AgTileEdgeVert> list = new List<AgTileEdgeVert>();
			for (int i = 0; i < num; i++)
			{
				if (!AutoGeometryWrapper.agPolylineIsLooped(this.m_tempPolylines[i]))
				{
					int num2 = AutoGeometryWrapper.agPolylineGetVertCount(this.m_tempPolylines[i]);
					if (num2 > 1)
					{
						AutoGeometryWrapper.agPolyLineGetVertices(this.m_tempPolylines[i], this.m_tempVertices);
						Vector2 vector = this.m_tempVertices[0] - this.m_tempVertices[1];
						Vector2 vector2 = this.m_tempVertices[num2 - 2] - this.m_tempVertices[num2 - 1];
						Vector2 vector3;
						vector3..ctor(vector.y, -vector.x);
						vector = vector3.normalized;
						Vector2 vector4;
						vector4..ctor(vector2.y, -vector2.x);
						vector2 = vector4.normalized;
						AgTileEdgeVert agTileEdgeVert = new AgTileEdgeVert(AutoGeometryManager.m_tileCacheOffset + this.m_tempVertices[0], vector, false);
						AgTileEdgeVert agTileEdgeVert2 = new AgTileEdgeVert(AutoGeometryManager.m_tileCacheOffset + this.m_tempVertices[num2 - 1], vector2, false);
						agTileEdgeVert.polyline = this.m_tempPolylines[i];
						list.Add(agTileEdgeVert);
						list.Add(agTileEdgeVert2);
					}
				}
			}
			Vector2 vector5 = AutoGeometryManager.m_tileCacheOffset + new Vector2(_tile.bb.l, _tile.bb.b);
			Vector2 vector6 = AutoGeometryManager.m_tileCacheOffset + new Vector2(_tile.bb.r, _tile.bb.b);
			Vector2 vector7 = AutoGeometryManager.m_tileCacheOffset + new Vector2(_tile.bb.r, _tile.bb.t);
			Vector2 vector8 = AutoGeometryManager.m_tileCacheOffset + new Vector2(_tile.bb.l, _tile.bb.t);
			list.Add(new AgTileEdgeVert(vector5, Vector2.zero, true));
			list.Add(new AgTileEdgeVert(vector6, Vector2.zero, true));
			list.Add(new AgTileEdgeVert(vector7, Vector2.zero, true));
			list.Add(new AgTileEdgeVert(vector8, Vector2.zero, true));
			_tile.edgeVerts = list.ToArray();
		}
	}

	// Token: 0x06002217 RID: 8727 RVA: 0x0018BFA4 File Offset: 0x0018A3A4
	private int findNearestEdgeVertex(AgTile _tile, Vector2 _tileCenter, int _index)
	{
		int num = -1;
		float num2 = 99999f;
		for (int i = 0; i < _tile.edgeVerts.Length; i++)
		{
			if (i != _index)
			{
				float sqrMagnitude = (_tile.edgeVerts[i].pos - _tile.edgeVerts[_index].pos).sqrMagnitude;
				if (sqrMagnitude < num2 && ToolBox.Sign(_tile.edgeVerts[_index].pos, _tile.edgeVerts[i].pos, _tileCenter) < 0f)
				{
					num2 = sqrMagnitude;
					num = i;
				}
			}
		}
		return num;
	}

	// Token: 0x06002218 RID: 8728 RVA: 0x0018C04C File Offset: 0x0018A44C
	private void GenerateTileGeometry(AgTile _tile)
	{
		PrefabS.RemoveComponentsByEntity(_tile.TC.p_entity, false);
		IntPtr intPtr = AutoGeometryWrapper.agCachedTileGetPolylineSet(_tile.tilePtr);
		int num = AutoGeometryWrapper.agPolylineSetGetLineCount(intPtr);
		AutoGeometryWrapper.agPolylineSetGetLines(intPtr, this.m_tempPolylines);
		Vector2 pos = _tile.pos;
		this.m_tempBeltPolyMeshes.Clear();
		this.m_tempAgPolys.Destroy();
		bool flag = true;
		if (_tile.edgeVerts.Length > 4)
		{
			int i = 0;
			while (i >= 0)
			{
				Vector2 pos2 = _tile.edgeVerts[i].pos;
				AgPolygon agPolygon = this.m_tempAgPolys.AddItem();
				for (int j = 0; j < _tile.edgeVerts.Length; j++)
				{
					IntPtr polyline = _tile.edgeVerts[i].polyline;
					if (polyline != IntPtr.Zero)
					{
						if (!_tile.edgeVerts[i].wasTravelled)
						{
							int num2 = AutoGeometryWrapper.agPolylineGetVertCount(polyline);
							this.m_tempBeltPoly.Clear();
							AutoGeometryWrapper.agPolyLineGetVertices(polyline, this.m_tempVertices);
							for (int k = 0; k < num2; k++)
							{
								Vector2 vector = AutoGeometryManager.m_tileCacheOffset + this.m_tempVertices[k];
								Vector2 vector2;
								if (k == 0 || k == num2 - 1)
								{
									vector2 = this.GetFixedEdgeVertNormalForPoint(_tile, vector);
								}
								else
								{
									Vector2 vector3 = this.m_tempVertices[k - 1] - this.m_tempVertices[k];
									Vector2 vector4 = this.m_tempVertices[k] - this.m_tempVertices[k + 1];
									Vector2 vector5;
									vector5..ctor(vector3.y, -vector3.x);
									vector3 = vector5.normalized;
									Vector2 vector6;
									vector6..ctor(vector4.y, -vector4.x);
									vector4 = vector6.normalized;
									vector2 = (vector3 + vector4) / 2f;
								}
								agPolygon.vertices.Add(vector);
								agPolygon.extraData.Add(vector2);
								this.m_tempBeltPoly.vertices.Add(vector);
								this.m_tempBeltPoly.extraData.Add(vector2);
							}
							if (_tile.regenerateCollisionShapes)
							{
								this.GenerateCollisionShapesFromPolyLine(_tile, this.m_tempBeltPoly, _tile.edgeVerts[i].cTangent, _tile.edgeVerts[i + 1].cTangent);
							}
							if (this.m_tempBeltPoly.vertices.Count > 0)
							{
								this.m_tempBeltPolyMeshes.Add(AutogeometryVisuals.CreateBeltMeshFromVertexArray(this.m_tempBeltPoly, this.m_groundC.m_ground.m_depth, this.m_tileOffset, false, pos, this.m_groundC.m_ground.m_smoothingAngle));
							}
							_tile.edgeVerts[i].wasTravelled = true;
							i++;
						}
					}
					else
					{
						agPolygon.vertices.Add(_tile.edgeVerts[i].pos);
						agPolygon.extraData.Add(new Vector2(0f, 0f));
					}
					i = this.findNearestEdgeVertex(_tile, pos, i);
					if (i < 0)
					{
						break;
					}
					if (_tile.edgeVerts[i].pos == pos2)
					{
						break;
					}
				}
				flag = false;
				i = -1;
				for (int l = 0; l < _tile.edgeVerts.Length; l++)
				{
					if (_tile.edgeVerts[l].polyline != IntPtr.Zero && !_tile.edgeVerts[l].wasTravelled)
					{
						i = l;
						break;
					}
				}
			}
			for (int m = 0; m < _tile.edgeVerts.Length; m++)
			{
				_tile.edgeVerts[m].wasTravelled = false;
			}
		}
		bool flag2 = true;
		bool flag3 = false;
		int num3 = 0;
		_tile.props.RemoveProps();
		for (int n = 0; n < num; n++)
		{
			IntPtr intPtr2 = this.m_tempPolylines[n];
			if (AutoGeometryWrapper.agPolylineIsLooped(intPtr2))
			{
				AgPolygon agPolygon2 = new AgPolygon();
				int num4 = AutoGeometryWrapper.agPolylineGetVertCount(intPtr2);
				AutoGeometryWrapper.agPolyLineGetVertices(intPtr2, this.m_tempVertices);
				float num5 = 0f;
				for (int num6 = 0; num6 < num4 - 1; num6++)
				{
					num5 += (this.m_tempVertices[num6 + 1].x - this.m_tempVertices[num6].x) * (this.m_tempVertices[num6 + 1].y + this.m_tempVertices[num6].y);
				}
				agPolygon2.isHole = num5 < 0f;
				if (num3 == 0 && agPolygon2.isHole)
				{
					flag3 = true;
				}
				if (Mathf.Abs(num5) > 0.01f)
				{
					for (int num7 = 0; num7 < num4; num7++)
					{
						agPolygon2.vertices.Add(AutoGeometryManager.m_tileCacheOffset + this.m_tempVertices[num7]);
						int rolledValue = ToolBox.getRolledValue(num7 - 1, 0, num4 - 1);
						int rolledValue2 = ToolBox.getRolledValue(num7 + 1, 0, num4 - 1);
						Vector2 vector7 = this.m_tempVertices[rolledValue] - this.m_tempVertices[num7];
						Vector2 vector8 = this.m_tempVertices[num7] - this.m_tempVertices[rolledValue2];
						Vector2 vector9;
						vector9..ctor(vector7.y, -vector7.x);
						vector7 = vector9.normalized;
						Vector2 vector10;
						vector10..ctor(vector8.y, -vector8.x);
						vector8 = vector10.normalized;
						Vector2 vector11 = vector7 + vector8 / 2f;
						agPolygon2.extraData.Add(vector11);
					}
					if (_tile.regenerateCollisionShapes)
					{
						this.GenerateCollisionShapesFromPolyLine(_tile, agPolygon2, Vector2.zero, Vector2.zero);
					}
					if (agPolygon2.vertices.Count > 0)
					{
						this.m_tempBeltPolyMeshes.Add(AutogeometryVisuals.CreateBeltMeshFromVertexArray(agPolygon2, this.m_groundC.m_ground.m_depth, this.m_tileOffset, true, pos, this.m_groundC.m_ground.m_smoothingAngle));
					}
					if (!agPolygon2.isHole)
					{
						flag2 = false;
					}
					AgPolygon agPolygon3 = this.m_tempAgPolys.AddItem();
					agPolygon2.m_index = agPolygon3.m_index;
					this.m_tempAgPolys.m_array[agPolygon2.m_index] = agPolygon2;
					num3++;
				}
			}
		}
		bool flag4 = false;
		if (flag && num3 > 0)
		{
			if (flag2 || flag3)
			{
				flag4 = true;
			}
		}
		else if (this.m_tempAgPolys.m_aliveCount == 0 && this.IsTileFilled(_tile.bb))
		{
			flag4 = true;
		}
		if (flag4)
		{
			AgPolygon agPolygon4 = this.m_tempAgPolys.AddItem();
			this.addSquarePoly(ref agPolygon4, _tile);
		}
		else if (AutoGeometryLayer.ENABLE_AUTODECOS && this.m_tempAgPolys.m_aliveCount > 0 && this.m_groundPropStorage != null)
		{
			_tile.props.Update(this.m_tempAgPolys.ToArray());
		}
		if (this.m_tempAgPolys.m_aliveCount > 0)
		{
			PrefabS.CreatePrefabFromMesh(_tile.TC, AutogeometryVisuals.CreateFrontFaceMeshFromPolygons(this.m_tempAgPolys.ToArray(), this.m_tileOffset, pos), 9, this.m_frontMaterial, true, true, false);
		}
		if (this.m_tempBeltPolyMeshes.Count > 0)
		{
			PrefabC prefabC = PrefabS.CreatePrefabFromMeshArray(_tile.TC, this.m_tempBeltPolyMeshes.ToArray(), 9, this.m_beltMaterial, true, false, false);
			prefabC.p_gameObject.GetComponent<Renderer>().castShadows = false;
			prefabC.p_gameObject.GetComponent<Renderer>().receiveShadows = false;
		}
		_tile.regenerateCollisionShapes = false;
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x0018C894 File Offset: 0x0018AC94
	private void addSquarePoly(ref AgPolygon _poly, AgTile _tile)
	{
		_poly.vertices = new List<Vector2>();
		int num = _tile.edgeVerts.Length - 4;
		_poly.vertices.Add(_tile.edgeVerts[num + 3].pos);
		_poly.vertices.Add(_tile.edgeVerts[num + 2].pos);
		_poly.vertices.Add(_tile.edgeVerts[num + 1].pos);
		_poly.vertices.Add(_tile.edgeVerts[num].pos);
	}

	// Token: 0x0600221A RID: 8730 RVA: 0x0018C934 File Offset: 0x0018AD34
	private AgTile GetAgTile(IntPtr _tilePtr, bool _setAsDirty)
	{
		AgTile agTile = this.m_tileHash[_tilePtr] as AgTile;
		if (agTile != null && _setAsDirty)
		{
			agTile.dirty = true;
		}
		return agTile;
	}

	// Token: 0x0600221B RID: 8731 RVA: 0x0018C96C File Offset: 0x0018AD6C
	private int GetTilePositionalHashKey(Vector2 _tileCenter)
	{
		int num = Mathf.CeilToInt(_tileCenter.x / (float)this.m_tileSize);
		int num2 = Mathf.CeilToInt(_tileCenter.y / (float)this.m_tileSize);
		return num2 * this.m_xTiles + num;
	}

	// Token: 0x0600221C RID: 8732 RVA: 0x0018C9B0 File Offset: 0x0018ADB0
	private AgTile GetTileNeighbour(AgTile _tile, int _x, int _y)
	{
		int num = Mathf.CeilToInt(_tile.pos.x / (float)this.m_tileSize) + _x;
		int num2 = Mathf.CeilToInt(_tile.pos.y / (float)this.m_tileSize) + _y;
		int num3 = num2 * this.m_xTiles + num;
		return this.m_tilePositionalHash[num3] as AgTile;
	}

	// Token: 0x0600221D RID: 8733 RVA: 0x0018CA18 File Offset: 0x0018AE18
	private AgTile CreateAgTile(IntPtr _tilePtr, bool _setDirty)
	{
		cpBB cpBB = AutoGeometryWrapper.agCachedTileGetBB(_tilePtr);
		Vector2 vector = AutoGeometryManager.m_tileCacheOffset + new Vector2(cpBB.l - (cpBB.l - cpBB.r) * 0.5f, cpBB.t - (cpBB.t - cpBB.b) * 0.5f);
		AgTile agTile = new AgTile(20, this);
		agTile.TC = EntityManager.AddEntityWithTC();
		agTile.TC.transform.position = vector;
		agTile.bb = cpBB;
		agTile.pos = vector;
		agTile.dirty = _setDirty;
		agTile.props = new AutoGeometryProps(agTile);
		agTile.tilePtr = _tilePtr;
		this.m_tileHash.Add(_tilePtr, agTile);
		this.m_tilePositionalHash.Add(this.GetTilePositionalHashKey(agTile.pos), agTile);
		return agTile;
	}

	// Token: 0x0600221E RID: 8734 RVA: 0x0018CAF8 File Offset: 0x0018AEF8
	public void ClearAgTileDirtyFlags()
	{
		IEnumerator enumerator = this.m_tileHash.Values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AgTile agTile = (AgTile)obj;
				agTile.dirty = false;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600221F RID: 8735 RVA: 0x0018CB64 File Offset: 0x0018AF64
	private void RemoveAgTile(IntPtr _tilePtr)
	{
		AgTile agTile = this.m_tileHash[_tilePtr] as AgTile;
		if (agTile != null && !agTile.dirty)
		{
			this.m_tileHash.Remove(_tilePtr);
			this.m_tilePositionalHash.Remove(this.GetTilePositionalHashKey(agTile.pos));
			agTile.Destroy();
		}
	}

	// Token: 0x06002220 RID: 8736 RVA: 0x0018CBCC File Offset: 0x0018AFCC
	private void EnsureRect()
	{
		if (this.m_updateEnsureRect)
		{
			this.m_updateEnsureRect = false;
			AutoGeometryWrapper.agBasicTileCacheEnsureRect(this.m_tileCache, this.m_ensureRect);
		}
	}

	// Token: 0x06002221 RID: 8737 RVA: 0x0018CBF4 File Offset: 0x0018AFF4
	private void ClearTileCollisionShapes(AgTile _tile)
	{
		for (int i = 0; i < _tile.shapeCount; i++)
		{
			ChipmunkProWrapper.ucpRemoveShape(_tile.shapes[i]);
		}
		_tile.shapeCount = 0;
		_tile.regenerateCollisionShapes = true;
	}

	// Token: 0x06002222 RID: 8738 RVA: 0x0018CC34 File Offset: 0x0018B034
	private void GenerateCollisionShapesFromPolyLine(AgTile _tile, AgPolygon _poly, Vector2 _prevTangent, Vector2 _nextTangent)
	{
		float num = 10f;
		IntPtr body = this.m_groundBody.body;
		Vector2[] array = _poly.vertices.ToArray();
		int num2 = array.Length;
		Vector3[] array2 = _poly.extraData.ToArray();
		for (int i = 0; i < num2; i++)
		{
			Vector2[] array3 = array;
			int num3 = i;
			array3[num3].x = array3[num3].x - array2[i].x * num * 0.75f;
			Vector2[] array4 = array;
			int num4 = i;
			array4[num4].y = array4[num4].y - array2[i].y * num * 0.75f;
		}
		if (num2 > 0)
		{
			for (int j = 0; j < num2 - 1; j++)
			{
				Vector2 vector = array[j];
				Vector2 vector2 = array[j + 1];
				if (_tile.shapeCount >= _tile.shapes.Length)
				{
					Array.Resize<IntPtr>(ref _tile.shapes, _tile.shapeCount * 2);
				}
				IntPtr intPtr = ChipmunkProWrapper.ucpSegmentShapeNew(body, vector, vector2, num, (ucpCollisionType)2);
				ChipmunkProWrapper.ucpShapeSetElasticity(intPtr, this.m_groundC.m_ground.m_elasticity);
				ChipmunkProWrapper.ucpShapeSetFriction(intPtr, this.m_groundC.m_ground.m_friction);
				ChipmunkProWrapper.ucpShapeSetLayers(intPtr, 17U);
				Vector2 vector3 = vector;
				Vector2 vector4 = vector2;
				if (j >= 0 && j <= num2 - 2)
				{
					if (j > 0)
					{
						vector3 = array[j - 1];
					}
					if (j < num2 - 2)
					{
						vector4 = array[j + 2];
					}
					ChipmunkProWrapper.ucpSegmentShapeSetNeighbors(intPtr, vector3, vector4);
				}
				if (j == num2 - 2)
				{
					ChipmunkProWrapper.ucpSegmentShapeSetNextNeighborTangent(intPtr, _nextTangent);
				}
				if (j == 0)
				{
					ChipmunkProWrapper.ucpSegmentShapeSetPrevNeighborTangent(intPtr, -_prevTangent);
				}
				ChipmunkProWrapper.ucpSpaceAddShape(intPtr);
				_tile.shapes[_tile.shapeCount] = intPtr;
				_tile.shapeCount++;
			}
		}
	}

	// Token: 0x06002223 RID: 8739 RVA: 0x0018CE2E File Offset: 0x0018B22E
	private void FixNormalsWithNeighbours(AgTile _tile)
	{
		if (_tile.edgeVerts != null && _tile.edgeVerts.Length > 4)
		{
			this.FixEdgeVertNormalsWithNeighbour(_tile, 1, 0);
			this.FixEdgeVertNormalsWithNeighbour(_tile, 0, 1);
			this.FixEdgeVertNormalsWithNeighbour(_tile, -1, 0);
			this.FixEdgeVertNormalsWithNeighbour(_tile, 0, -1);
		}
	}

	// Token: 0x06002224 RID: 8740 RVA: 0x0018CE70 File Offset: 0x0018B270
	private void UpdateSegments(bool _debugLogDirty = false, bool _updateVisuals = true)
	{
		if (!this.m_dirty)
		{
			return;
		}
		this.m_dirty = false;
		this.EnsureRect();
		int num = AutoGeometryWrapper.agBasicTileCacheGetDirtyTileCount(this.m_tileCache);
		if (num == 0)
		{
			return;
		}
		if (_debugLogDirty)
		{
			Debug.Log("AG Dirtycount: " + num, null);
		}
		AutoGeometryWrapper.agBasicTileCacheGetDirtyTileList(this.m_tileCache, this.m_dirtyTileList);
		this.m_geometryUpdateQueue.Clear();
		ChipmunkProWrapper.ucpClearCollisionLists();
		for (int i = 0; i < num; i++)
		{
			IntPtr intPtr = this.m_dirtyTileList[i];
			AgTile agTile = this.GetAgTile(intPtr, this.m_trackDirtyTiles);
			if (agTile != null)
			{
				this.ClearTileCollisionShapes(agTile);
			}
			IntPtr intPtr2 = AutoGeometryWrapper.agCachedTileGetPolylineSet(intPtr);
			int num2 = AutoGeometryWrapper.agPolylineSetGetLineCount(intPtr2);
			if (num2 > 0)
			{
				if (agTile == null)
				{
					agTile = this.CreateAgTile(intPtr, this.m_trackDirtyTiles);
				}
				this.GenerateEdgeVertArray(agTile);
				this.m_geometryUpdateQueue.Add(agTile);
			}
			else
			{
				cpBB cpBB = AutoGeometryWrapper.agCachedTileGetBB(intPtr);
				bool flag = this.IsTileFilled(cpBB);
				if (agTile != null || flag)
				{
					if (agTile == null)
					{
						agTile = this.CreateAgTile(intPtr, this.m_trackDirtyTiles);
					}
					PrefabS.RemoveComponentsByEntity(agTile.TC.p_entity, false);
					agTile.props.RemoveProps();
					agTile.edgeVerts = null;
					if (flag)
					{
						Vector2[] array = new Vector2[]
						{
							default(Vector2),
							default(Vector2),
							default(Vector2),
							AutoGeometryManager.m_tileCacheOffset + new Vector2(cpBB.l, cpBB.b)
						};
						array[2] = AutoGeometryManager.m_tileCacheOffset + new Vector2(cpBB.r, cpBB.b);
						array[1] = AutoGeometryManager.m_tileCacheOffset + new Vector2(cpBB.r, cpBB.t);
						array[0] = AutoGeometryManager.m_tileCacheOffset + new Vector2(cpBB.l, cpBB.t);
						AgPolygon[] array2 = new AgPolygon[]
						{
							new AgPolygon()
						};
						array2[0].vertices.Add(array[0]);
						array2[0].vertices.Add(array[1]);
						array2[0].vertices.Add(array[2]);
						array2[0].vertices.Add(array[3]);
						PrefabS.CreatePrefabFromMesh(agTile.TC, AutogeometryVisuals.CreateFrontFaceMeshFromPolygons(array2, this.m_tileOffset, agTile.pos), 9, this.m_frontMaterial, true, true, false);
					}
					else
					{
						this.RemoveAgTile(intPtr);
					}
				}
			}
		}
		if (this.m_geometryUpdateQueue.Count > 0)
		{
			AgTile[] array3 = this.m_geometryUpdateQueue.ToArray();
			for (int j = 0; j < array3.Length; j++)
			{
				this.FixNormalsWithNeighbours(array3[j]);
			}
		}
		if (_updateVisuals)
		{
			this.UpdateVisuals();
		}
		ChipmunkProS.HandleCollisionEvents();
	}

	// Token: 0x06002225 RID: 8741 RVA: 0x0018D17C File Offset: 0x0018B57C
	public void UpdateVisuals()
	{
		foreach (AgTile agTile in this.m_geometryUpdateQueue)
		{
			this.GenerateTileGeometry(agTile);
		}
	}

	// Token: 0x06002226 RID: 8742 RVA: 0x0018D1D8 File Offset: 0x0018B5D8
	public void Update()
	{
		this.UpdateSegments(false, true);
		if (this.m_highlightTween != null || this.m_highlightGlowTween != null)
		{
			float num;
			if (this.m_highlightTween != null)
			{
				num = this.m_highlightTween.currentValue.x;
			}
			else
			{
				num = this.m_highlightGlowTween.currentValue.x;
			}
			Color color;
			color..ctor(num, num, num, 0f);
			this.m_frontMaterial.SetColor("_Emission", color);
			this.m_beltMaterial.SetColor("_Emission", color);
			if (this.m_highlightTween != null && this.m_highlightTween.hasFinished)
			{
				TweenS.RemoveComponent(this.m_highlightTween);
				this.m_highlightTween = null;
			}
		}
		if (this.m_maskTextureModified && this.agMaskTicker % AutoGeometryLayer.AG_MASK_UPDATE_TICK_INTERVAL == 0)
		{
			this.m_maskTexture.Apply();
			this.m_maskTextureModified = false;
		}
		this.agMaskTicker++;
	}

	// Token: 0x06002227 RID: 8743 RVA: 0x0018D2D8 File Offset: 0x0018B6D8
	public void Destroy()
	{
		Debug.Log("Removing material: " + this.m_groundC.m_ground.m_groundItemIdentifier, null);
		EntityManager.RemoveEntity(this.m_groundBodyEntity);
		IEnumerator enumerator = this.m_tileHash.Values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AgTile agTile = (AgTile)obj;
				agTile.Destroy();
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		this.m_tileHash.Clear();
		this.m_tilePositionalHash.Clear();
		this.m_geometryUpdateQueue.Clear();
		this.m_tileHash = null;
		this.m_tilePositionalHash = null;
		this.m_geometryUpdateQueue = null;
		this.m_tempPolylines = null;
		this.m_tempVertices = null;
		this.m_bytes = null;
		Object.DestroyImmediate(this.m_maskTexture);
		this.m_maskTexture = null;
		AutoGeometryWrapper.agSimpleSamplerFree(this.m_sampler);
		AutoGeometryWrapper.agBasicTileCacheFree(this.m_tileCache);
		this.m_tileCache = IntPtr.Zero;
		this.m_sampler = IntPtr.Zero;
		if (this.m_beltMaterial != null)
		{
			Object.Destroy(this.m_beltMaterial);
			this.m_beltMaterial = null;
		}
		if (this.m_frontMaterial != null)
		{
			Object.Destroy(this.m_frontMaterial);
			this.m_frontMaterial = null;
		}
		if (this.m_groundPropStorage != null)
		{
			Object.Destroy(this.m_groundPropStorage.gameObject);
			this.m_groundPropStorage = null;
		}
	}

	// Token: 0x0400282F RID: 10287
	public const float MAX_PIXEL_VALUE = 255f;

	// Token: 0x04002830 RID: 10288
	public const int CAMERA_LAYER = 9;

	// Token: 0x04002831 RID: 10289
	public const int GROUND_DEFAULT_DEPTH = 300;

	// Token: 0x04002832 RID: 10290
	private Vector3 m_tileOffset = new Vector3(0f, 0f, -75f);

	// Token: 0x04002833 RID: 10291
	public static int AG_MASK_UPDATE_TICK_INTERVAL = 3;

	// Token: 0x04002834 RID: 10292
	public static bool ENABLE_AUTODECOS = true;

	// Token: 0x04002835 RID: 10293
	private bool m_trackDirtyTiles = true;

	// Token: 0x04002836 RID: 10294
	public int m_index;

	// Token: 0x04002837 RID: 10295
	public int m_width;

	// Token: 0x04002838 RID: 10296
	public int m_height;

	// Token: 0x04002839 RID: 10297
	public int m_tileSize;

	// Token: 0x0400283A RID: 10298
	public int m_xTiles;

	// Token: 0x0400283B RID: 10299
	public int m_yTiles;

	// Token: 0x0400283C RID: 10300
	public IntPtr m_sampler;

	// Token: 0x0400283D RID: 10301
	public IntPtr m_tileCache;

	// Token: 0x0400283E RID: 10302
	public IntPtr[] m_dirtyTileList;

	// Token: 0x0400283F RID: 10303
	public float m_simplifyTreshold;

	// Token: 0x04002840 RID: 10304
	public int m_texelScale;

	// Token: 0x04002841 RID: 10305
	public bool m_dirty = true;

	// Token: 0x04002842 RID: 10306
	public Material m_beltMaterial;

	// Token: 0x04002843 RID: 10307
	public Material m_frontMaterial;

	// Token: 0x04002844 RID: 10308
	public Hashtable m_tileHash = new Hashtable();

	// Token: 0x04002845 RID: 10309
	private Hashtable m_tilePositionalHash = new Hashtable();

	// Token: 0x04002846 RID: 10310
	private Vector2[] m_tempVertices = new Vector2[200];

	// Token: 0x04002847 RID: 10311
	private IntPtr[] m_tempPolylines = new IntPtr[200];

	// Token: 0x04002848 RID: 10312
	private DynamicArray<AgPolygon> m_tempAgPolys;

	// Token: 0x04002849 RID: 10313
	private cpBB m_ensureRect;

	// Token: 0x0400284A RID: 10314
	private cpBB m_undoRect;

	// Token: 0x0400284B RID: 10315
	private bool m_updateEnsureRect;

	// Token: 0x0400284C RID: 10316
	private List<AgTile> m_geometryUpdateQueue = new List<AgTile>();

	// Token: 0x0400284D RID: 10317
	private Entity m_groundBodyEntity;

	// Token: 0x0400284E RID: 10318
	private TransformC m_groundBodyTC;

	// Token: 0x0400284F RID: 10319
	public ChipmunkBodyC m_groundBody;

	// Token: 0x04002850 RID: 10320
	public GroundC m_groundC;

	// Token: 0x04002851 RID: 10321
	private bool m_plotDidChangePixel;

	// Token: 0x04002852 RID: 10322
	private bool m_maskTextureModified;

	// Token: 0x04002853 RID: 10323
	private IntPtr[] m_brushPaintTileList = new IntPtr[20];

	// Token: 0x04002854 RID: 10324
	private TweenC m_highlightTween;

	// Token: 0x04002855 RID: 10325
	private TweenC m_highlightGlowTween;

	// Token: 0x04002856 RID: 10326
	public byte[] m_bytes;

	// Token: 0x04002857 RID: 10327
	public Texture2D m_maskTexture;

	// Token: 0x04002858 RID: 10328
	public AGPropStorage m_groundPropStorage;

	// Token: 0x04002859 RID: 10329
	private List<Mesh> m_tempBeltPolyMeshes = new List<Mesh>(100);

	// Token: 0x0400285A RID: 10330
	private AgPolygon m_tempBeltPoly = new AgPolygon(100);

	// Token: 0x0400285B RID: 10331
	private int agMaskTicker;
}
