using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200050A RID: 1290
public static class SpriteS
{
	// Token: 0x0600256B RID: 9579 RVA: 0x0019D195 File Offset: 0x0019B595
	public static void Initialize()
	{
		SpriteS.m_sheets = new GenericArray<SpriteSheet>(50);
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x0019D1A3 File Offset: 0x0019B5A3
	public static SpriteSheet AddSpriteSheet(Camera _camera, Material _material, float _globalSpriteScale)
	{
		return SpriteS.AddSpriteSheet(_camera, _material, null, _globalSpriteScale);
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x0019D1B0 File Offset: 0x0019B5B0
	public static SpriteSheet AddSpriteSheet(Camera _camera, Material _material, TextAsset _atlas, float _globalSpriteScale)
	{
		SpriteSheet spriteSheet = new SpriteSheet(_camera, _material, _atlas, _globalSpriteScale);
		int num = SpriteS.m_sheets.AddItem(spriteSheet);
		SpriteS.m_sheets.m_array[num].m_index = num;
		return spriteSheet;
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x0019D1E8 File Offset: 0x0019B5E8
	public static SpriteSheet AddSpriteSheet(Camera _camera, Texture _texture, Shader _shader, float _globalSpriteScale)
	{
		SpriteSheet spriteSheet = new SpriteSheet(_camera, _texture, _shader, _globalSpriteScale);
		int num = SpriteS.m_sheets.AddItem(spriteSheet);
		SpriteS.m_sheets.m_array[num].m_index = num;
		return spriteSheet;
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x0019D220 File Offset: 0x0019B620
	public static void RemoveSpriteSheet(SpriteSheet _sheet)
	{
		SpriteS.RemoveAllComponentsFromSheet(_sheet);
		_sheet.Destroy();
		Object.Destroy(_sheet.m_gameObject);
		_sheet.m_gameObject = null;
		_sheet.m_atlas = null;
		Object.Destroy(_sheet.m_mesh);
		_sheet.m_mesh = null;
		Object.Destroy(_sheet.m_meshFilter);
		Object.Destroy(_sheet.m_meshRenderer);
		_sheet.m_meshFilter = null;
		_sheet.m_meshRenderer = null;
		SpriteS.m_sheets.RemoveItem(_sheet.m_index);
		SpriteS.m_sheets.m_array[_sheet.m_index] = null;
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x0019D2AC File Offset: 0x0019B6AC
	public static SpriteC AddComponent(TransformC _transformComponent, string _frameName, SpriteSheet _sheet)
	{
		Frame frame = _sheet.m_atlas.GetFrame(_frameName, null);
		return SpriteS.AddComponent(_transformComponent, frame, _sheet);
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x0019D2D0 File Offset: 0x0019B6D0
	public static SpriteC AddComponent(TransformC _transformComponent, Frame _frame, SpriteSheet _sheet)
	{
		if (_sheet.m_components.m_aliveCount == 0)
		{
			_sheet.m_gameObject.SetActive(true);
		}
		SpriteC spriteC = _sheet.m_components.AddItem();
		if (_sheet.m_components.m_aliveCount < _sheet.m_currentMeshCapacity)
		{
			spriteC.vertDataIndex = _sheet.m_freeVertexIndices[_sheet.m_freeVertexIndicesCount - 1];
			_sheet.m_freeVertexIndicesCount--;
		}
		else
		{
			spriteC.vertDataIndex = -1;
		}
		spriteC.p_TC = _transformComponent;
		spriteC.p_spriteSheet = _sheet;
		SpriteS.SetFrame(_sheet, spriteC, _frame);
		EntityManager.AddComponentToEntity(_transformComponent.p_entity, spriteC);
		return spriteC;
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x0019D370 File Offset: 0x0019B770
	public static void RemoveComponent(SpriteC _c)
	{
		if (_c.p_entity == null)
		{
			Debug.LogWarning("Trying to remove component that has already been removed");
			return;
		}
		SpriteSheet p_spriteSheet = _c.p_spriteSheet;
		if (_c.vertDataIndex >= 0)
		{
			int num = _c.vertDataIndex * 4;
			for (int i = 0; i < 4; i++)
			{
				p_spriteSheet.m_vertices[num + i] = SpriteS.m_outOfScreen;
			}
			p_spriteSheet.m_vertsChanged = true;
			p_spriteSheet.m_freeVertexIndices[p_spriteSheet.m_freeVertexIndicesCount] = _c.vertDataIndex;
			p_spriteSheet.m_freeVertexIndicesCount++;
		}
		_c.p_TC = null;
		_c.p_spriteSheet = null;
		_c.sortValue = 0f;
		EntityManager.RemoveComponentFromEntity(_c);
		p_spriteSheet.m_components.RemoveItem(_c);
		if (p_spriteSheet.m_components.m_aliveCount == 0)
		{
			p_spriteSheet.m_gameObject.SetActive(false);
		}
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x0019D448 File Offset: 0x0019B848
	public static void RemoveComponentsByEntity(Entity _e)
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Sprite, _e);
		while (componentsByEntity.Count > 0)
		{
			int num = componentsByEntity.Count - 1;
			SpriteS.RemoveComponent(componentsByEntity[num] as SpriteC);
			componentsByEntity.RemoveAt(num);
		}
	}

	// Token: 0x06002574 RID: 9588 RVA: 0x0019D490 File Offset: 0x0019B890
	public static void RemoveAllComponentsFromSheet(SpriteSheet _sheet)
	{
		while (_sheet.m_components.m_aliveCount > 0)
		{
			SpriteC spriteC = _sheet.m_components.m_array[_sheet.m_components.m_aliveIndices[0]];
			SpriteS.RemoveComponent(spriteC);
		}
	}

	// Token: 0x06002575 RID: 9589 RVA: 0x0019D4D4 File Offset: 0x0019B8D4
	public static void RemoveComponentsByTransformComponent(TransformC _tc)
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Sprite, _tc.p_entity);
		for (int i = componentsByEntity.Count - 1; i > -1; i--)
		{
			SpriteC spriteC = componentsByEntity[i] as SpriteC;
			if (spriteC.p_TC == _tc)
			{
				SpriteS.RemoveComponent(spriteC);
			}
		}
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x0019D526 File Offset: 0x0019B926
	public static void SetDimensions(SpriteC _c, float _width, float _height)
	{
		_c.wDimension = _width;
		_c.hDimension = _height;
		_c.width = _width * _c.p_spriteSheet.m_globalSpriteScale;
		_c.height = _height * _c.p_spriteSheet.m_globalSpriteScale;
		_c.updateScale = true;
	}

	// Token: 0x06002577 RID: 9591 RVA: 0x0019D563 File Offset: 0x0019B963
	public static void SetDimensionScale(SpriteC _c, float _scale)
	{
		_c.dimensionScale = _scale;
		_c.updateScale = true;
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x0019D573 File Offset: 0x0019B973
	public static void SetVisibility(SpriteC _s, bool _visible)
	{
		SpriteS.SetVisibility(_s, _visible, true);
	}

	// Token: 0x06002579 RID: 9593 RVA: 0x0019D57D File Offset: 0x0019B97D
	public static void SetVisibility(SpriteC _s, bool _visible, bool _markVisibility)
	{
		_s.visible = _visible;
		if (_markVisibility)
		{
			_s.wasVisible = _visible;
		}
		_s.updatePosition = true;
	}

	// Token: 0x0600257A RID: 9594 RVA: 0x0019D59C File Offset: 0x0019B99C
	public static void SetVisibilityByTransformComponent(TransformC _tc, bool _visible, bool _affectChildren = false, bool _affectWholeHierarchy = false)
	{
		if (_affectWholeHierarchy)
		{
			_tc = TransformS.GetRootTransformComponent(_tc);
		}
		if (_affectChildren || _affectWholeHierarchy)
		{
			for (int i = 0; i < _tc.childs.Count; i++)
			{
				SpriteS.SetVisibilityByTransformComponent(_tc.childs[i], _visible, true, false);
			}
		}
		for (int j = 0; j < SpriteS.m_sheets.m_aliveCount; j++)
		{
			SpriteSheet spriteSheet = SpriteS.m_sheets.m_array[SpriteS.m_sheets.m_aliveIndices[j]];
			SpriteS.p_sprites = spriteSheet.m_components.m_array;
			SpriteS.p_aliveIndices = spriteSheet.m_components.m_aliveIndices;
			int aliveCount = spriteSheet.m_components.m_aliveCount;
			for (int k = 0; k < aliveCount; k++)
			{
				SpriteC spriteC = SpriteS.p_sprites[SpriteS.p_aliveIndices[k]];
				if (spriteC.p_TC == _tc)
				{
					SpriteS.SetVisibility(spriteC, _visible);
				}
			}
		}
	}

	// Token: 0x0600257B RID: 9595 RVA: 0x0019D68D File Offset: 0x0019BA8D
	public static void SetClip(SpriteC _c, float _left, float _right, float _bottom, float _top)
	{
		_c.clip = true;
		_c.clipLeft = _left;
		_c.clipRight = _right;
		_c.clipBottom = _bottom;
		_c.clipTop = _top;
		_c.updateScale = true;
		_c.updateUVs = true;
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x0019D6C4 File Offset: 0x0019BAC4
	public static void SetAlignment(int _sheetIndex, int _spriteIndex, Align _horizontal, Align _vertical)
	{
		float num = 0f;
		if (_horizontal == Align.Left)
		{
			num = -0.5f;
		}
		else if (_horizontal == Align.Right)
		{
			num = 0.5f;
		}
		float num2 = 0f;
		if (_horizontal == Align.Top)
		{
			num2 = 0.5f;
		}
		else if (_horizontal == Align.Bottom)
		{
			num2 = -0.5f;
		}
		SpriteS.SetAlignment(_sheetIndex, _spriteIndex, num, num2);
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x0019D724 File Offset: 0x0019BB24
	public static void SetAlignment(int _sheetIndex, int _spriteIndex, float _horizontal, float _vertical)
	{
		SpriteC spriteC = SpriteS.m_sheets.m_array[_sheetIndex].m_components.m_array[_spriteIndex];
		spriteC.align.x = -_horizontal * spriteC.width;
		spriteC.align.y = _vertical * spriteC.height;
		spriteC.updatePosition = true;
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x0019D778 File Offset: 0x0019BB78
	public static void SetOffset(SpriteC _s, Vector3 _pos, float _rot)
	{
		_s.offset = _pos;
		float num = Mathf.Cos(_rot * 0.017453292f);
		float num2 = Mathf.Sin(_rot * 0.017453292f);
		_s.offsetRight = new Vector3(num, num2, 0f);
		_s.offsetUp = new Vector3(-num2, num, 0f);
		_s.updateRotation = true;
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x0019D7D2 File Offset: 0x0019BBD2
	public static void SetColor(SpriteC _sprite, Color _color)
	{
		_sprite.color = _color;
		_sprite.updateColors = true;
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x0019D7E4 File Offset: 0x0019BBE4
	public static void SetColorByTransformComponent(TransformC _tc, Color _color, bool _affectChildren = false, bool _affectWholeHierarchy = false)
	{
		if (_affectWholeHierarchy)
		{
			_tc = TransformS.GetRootTransformComponent(_tc);
		}
		if (_affectChildren || _affectWholeHierarchy)
		{
			for (int i = 0; i < _tc.childs.Count; i++)
			{
				SpriteS.SetColorByTransformComponent(_tc.childs[i], _color, true, false);
			}
		}
		for (int j = 0; j < SpriteS.m_sheets.m_aliveCount; j++)
		{
			SpriteSheet spriteSheet = SpriteS.m_sheets.m_array[SpriteS.m_sheets.m_aliveIndices[j]];
			SpriteS.p_sprites = spriteSheet.m_components.m_array;
			SpriteS.p_aliveIndices = spriteSheet.m_components.m_aliveIndices;
			int aliveCount = spriteSheet.m_components.m_aliveCount;
			for (int k = 0; k < aliveCount; k++)
			{
				SpriteC spriteC = SpriteS.p_sprites[SpriteS.p_aliveIndices[k]];
				if (spriteC.p_TC == _tc)
				{
					SpriteS.SetColor(spriteC, _color);
				}
			}
		}
	}

	// Token: 0x06002581 RID: 9601 RVA: 0x0019D8D8 File Offset: 0x0019BCD8
	public static void SetAlphaByTransformComponent(TransformC _tc, float _alpha, bool _affectChildren = false, bool _affectWholeHierarchy = false)
	{
		if (_affectWholeHierarchy)
		{
			_tc = TransformS.GetRootTransformComponent(_tc);
		}
		if (_affectChildren || _affectWholeHierarchy)
		{
			for (int i = 0; i < _tc.childs.Count; i++)
			{
				SpriteS.SetAlphaByTransformComponent(_tc.childs[i], _alpha, true, false);
			}
		}
		for (int j = 0; j < SpriteS.m_sheets.m_aliveCount; j++)
		{
			SpriteSheet spriteSheet = SpriteS.m_sheets.m_array[SpriteS.m_sheets.m_aliveIndices[j]];
			SpriteS.p_sprites = spriteSheet.m_components.m_array;
			SpriteS.p_aliveIndices = spriteSheet.m_components.m_aliveIndices;
			int aliveCount = spriteSheet.m_components.m_aliveCount;
			for (int k = 0; k < aliveCount; k++)
			{
				SpriteC spriteC = SpriteS.p_sprites[SpriteS.p_aliveIndices[k]];
				Color color = spriteC.color;
				color.a = _alpha;
				if (spriteC.p_TC == _tc)
				{
					SpriteS.SetColor(spriteC, color);
				}
			}
		}
	}

	// Token: 0x06002582 RID: 9602 RVA: 0x0019D9DB File Offset: 0x0019BDDB
	public static void SetFlip(SpriteC _c, bool _x, bool _y)
	{
		_c.frame.flipX = _x;
		_c.frame.flipY = _y;
		SpriteS.SetFrame(_c.p_spriteSheet, _c, _c.frame);
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x0019DA08 File Offset: 0x0019BE08
	public static void SetSortValue(TransformC _tc, float _sortValue)
	{
		for (int i = 0; i < SpriteS.m_sheets.m_aliveCount; i++)
		{
			SpriteSheet spriteSheet = SpriteS.m_sheets.m_array[SpriteS.m_sheets.m_aliveIndices[i]];
			SpriteS.p_sprites = spriteSheet.m_components.m_array;
			SpriteS.p_aliveIndices = spriteSheet.m_components.m_aliveIndices;
			int aliveCount = spriteSheet.m_components.m_aliveCount;
			for (int j = 0; j < aliveCount; j++)
			{
				SpriteC spriteC = SpriteS.p_sprites[SpriteS.p_aliveIndices[j]];
				if (spriteC.p_TC == _tc)
				{
					SpriteS.SetSortValue(spriteC, _sortValue);
				}
			}
		}
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x0019DAAA File Offset: 0x0019BEAA
	public static void SetSortValue(SpriteC _s, float _sortValue)
	{
		_s.sortValue = _sortValue;
		_s.p_spriteSheet.m_sortMesh = true;
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x0019DAC0 File Offset: 0x0019BEC0
	public static void SortMesh(SpriteSheet _sheet)
	{
		_sheet.resetVertexIndices();
		int[] array = new int[_sheet.m_components.m_aliveCount];
		float[] array2 = new float[_sheet.m_components.m_aliveCount];
		for (int i = 0; i < _sheet.m_components.m_aliveCount; i++)
		{
			int num = _sheet.m_components.m_aliveIndices[i];
			SpriteC spriteC = _sheet.m_components.m_array[num];
			if (spriteC.vertDataIndex >= 0)
			{
				int num2 = spriteC.vertDataIndex * 4;
				for (int j = 0; j < 4; j++)
				{
					_sheet.m_vertices[num2 + j] = SpriteS.m_outOfScreen;
				}
			}
			array[i] = num;
			array2[i] = spriteC.sortValue;
		}
		array = ToolBox.sortTable(array, array2);
		for (int k = 0; k < _sheet.m_components.m_aliveCount; k++)
		{
			SpriteC spriteC2 = _sheet.m_components.m_array[array[k]];
			spriteC2.vertDataIndex = _sheet.m_freeVertexIndices[_sheet.m_freeVertexIndicesCount - 1];
			_sheet.m_freeVertexIndicesCount--;
		}
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x0019DBE4 File Offset: 0x0019BFE4
	public static void SetFrame(SpriteSheet _sheet, SpriteC _sprite, Frame _frame)
	{
		_sprite.frame = _frame;
		if (_sprite.wDimension > 0f)
		{
			_sprite.width = _sprite.wDimension * _sheet.m_globalSpriteScale;
		}
		else
		{
			_sprite.wDimension = _frame.width;
			_sprite.width = _frame.width * _sheet.m_globalSpriteScale;
		}
		if (_sprite.hDimension > 0f)
		{
			_sprite.height = _sprite.hDimension * _sheet.m_globalSpriteScale;
		}
		else
		{
			_sprite.hDimension = _frame.height;
			_sprite.height = _frame.height * _sheet.m_globalSpriteScale;
		}
		_sprite.updateUVs = true;
	}

	// Token: 0x06002587 RID: 9607 RVA: 0x0019DC90 File Offset: 0x0019C090
	public static void Update()
	{
		for (int i = 0; i < SpriteS.m_sheets.m_aliveCount; i++)
		{
			int num = SpriteS.m_sheets.m_aliveIndices[i];
			SpriteSheet spriteSheet = SpriteS.m_sheets.m_array[num];
			SpriteS.UpdateSheet(spriteSheet);
		}
		SpriteS.debug_rotationUpdates = 0;
		SpriteS.debug_scaleUpdates = 0;
		SpriteS.debug_positionUpdates = 0;
	}

	// Token: 0x06002588 RID: 9608 RVA: 0x0019DCEC File Offset: 0x0019C0EC
	public static void UpdateSheet(SpriteSheet _sheet)
	{
		bool flag = _sheet.m_currentMeshCapacity <= _sheet.m_components.m_aliveCount || Mathf.Abs(_sheet.m_components.m_aliveCount - _sheet.m_currentMeshCapacity) >= SpriteS.m_resizeTolerance * 2;
		if (flag)
		{
			_sheet.setMeshData(_sheet.m_components.m_aliveCount + SpriteS.m_resizeTolerance);
			_sheet.assignVertexDataIndices();
			if (_sheet.m_sortingEnabled)
			{
				_sheet.m_sortMesh = true;
			}
		}
		if (_sheet.m_sortingEnabled && _sheet.m_sortMesh)
		{
			SpriteS.SortMesh(_sheet);
			_sheet.m_sortMesh = false;
			_sheet.m_meshCapacityChanged = true;
		}
		if (_sheet.m_components.m_aliveCount == 0)
		{
			if (_sheet.m_gameObject.activeSelf)
			{
				_sheet.m_gameObject.SetActive(false);
			}
		}
		else
		{
			if (!_sheet.m_gameObject.activeSelf)
			{
				_sheet.m_gameObject.SetActive(true);
			}
			SpriteS.p_sprites = _sheet.m_components.m_array;
			SpriteS.p_vertices = _sheet.m_vertices;
			for (int i = 0; i < _sheet.m_components.m_aliveCount; i++)
			{
				int num = _sheet.m_components.m_aliveIndices[i];
				SpriteC spriteC = _sheet.m_components.m_array[num];
				TransformC p_TC = spriteC.p_TC;
				Transform transform = spriteC.p_TC.transform;
				if (p_TC.updatedPosition)
				{
					spriteC.updatePosition = true;
				}
				if (p_TC.updatedRotation)
				{
					spriteC.updateRotation = true;
				}
				if (p_TC.updatedScale)
				{
					spriteC.updateScale = true;
				}
				if (spriteC.vertDataIndex < 0)
				{
					spriteC.vertDataIndex = _sheet.m_freeVertexIndices[_sheet.m_freeVertexIndicesCount - 1];
					_sheet.m_freeVertexIndicesCount--;
				}
				bool flag2 = false;
				if (spriteC.updateScale || _sheet.m_meshCapacityChanged)
				{
					if (p_TC.forceScale)
					{
						spriteC.wScale = spriteC.width * spriteC.dimensionScale * 0.5f * p_TC.forcedScale.x;
						spriteC.hScale = spriteC.height * spriteC.dimensionScale * 0.5f * p_TC.forcedScale.y;
					}
					else
					{
						Vector3 lossyScale = transform.lossyScale;
						spriteC.wScale = spriteC.width * spriteC.dimensionScale * 0.5f * lossyScale.x;
						spriteC.hScale = spriteC.height * spriteC.dimensionScale * 0.5f * lossyScale.y;
					}
					spriteC.updatePosition = true;
					spriteC.updateScale = false;
					flag2 = true;
					SpriteS.debug_scaleUpdates++;
				}
				if (spriteC.updateRotation || _sheet.m_meshCapacityChanged)
				{
					if (p_TC.forceRotation)
					{
						spriteC.relRight = p_TC.forcedRotation * spriteC.offsetRight;
						spriteC.relUp = p_TC.forcedRotation * spriteC.offsetUp;
						spriteC.relOffset = p_TC.forcedRotation * spriteC.offset;
						if (!p_TC.forceScale)
						{
							spriteC.relOffset.Scale(transform.lossyScale);
						}
					}
					else
					{
						Quaternion rotation = transform.rotation;
						spriteC.relRight = rotation * spriteC.offsetRight;
						spriteC.relUp = rotation * spriteC.offsetUp;
						spriteC.relOffset = rotation * spriteC.offset;
						if (!p_TC.forceScale)
						{
							spriteC.relOffset.Scale(transform.lossyScale);
						}
					}
					spriteC.updatePosition = true;
					spriteC.updateRotation = false;
					flag2 = true;
					SpriteS.debug_rotationUpdates++;
				}
				if (flag2)
				{
					spriteC.scaledRelRight = spriteC.relRight * spriteC.wScale;
					spriteC.scaledRelUp = spriteC.relUp * spriteC.hScale;
				}
				if (spriteC.updatePosition)
				{
					int num2 = spriteC.vertDataIndex * 4;
					if (spriteC.visible)
					{
						Vector3 vector = transform.position + spriteC.relOffset;
						if (spriteC.clip)
						{
							SpriteS.p_vertices[num2] = vector + spriteC.scaledRelUp * (2f * (1f - spriteC.clipTop) - 1f) - spriteC.scaledRelRight * (2f * (1f - spriteC.clipLeft) - 1f);
							SpriteS.p_vertices[num2 + 1] = vector - spriteC.scaledRelUp * (2f * (1f - spriteC.clipBottom) - 1f) - spriteC.scaledRelRight * (2f * (1f - spriteC.clipLeft) - 1f);
							SpriteS.p_vertices[num2 + 2] = vector - spriteC.scaledRelUp * (2f * (1f - spriteC.clipBottom) - 1f) + spriteC.scaledRelRight * (2f * (1f - spriteC.clipRight) - 1f);
							SpriteS.p_vertices[num2 + 3] = vector + spriteC.scaledRelUp * (2f * (1f - spriteC.clipTop) - 1f) + spriteC.scaledRelRight * (2f * (1f - spriteC.clipRight) - 1f);
						}
						else
						{
							SpriteS.p_vertices[num2] = vector + spriteC.scaledRelUp - spriteC.scaledRelRight;
							SpriteS.p_vertices[num2 + 1] = vector - spriteC.scaledRelUp - spriteC.scaledRelRight;
							SpriteS.p_vertices[num2 + 2] = vector - spriteC.scaledRelUp + spriteC.scaledRelRight;
							SpriteS.p_vertices[num2 + 3] = vector + spriteC.scaledRelUp + spriteC.scaledRelRight;
						}
					}
					else
					{
						for (int j = 0; j < 4; j++)
						{
							SpriteS.p_vertices[num2 + j] = SpriteS.m_outOfScreen;
						}
					}
					spriteC.updatePosition = false;
					_sheet.m_vertsChanged = true;
					SpriteS.debug_positionUpdates++;
				}
				if (spriteC.updateUVs || _sheet.m_meshCapacityChanged)
				{
					float num3 = spriteC.frame.x / (float)_sheet.m_textureWidth;
					float num4 = (spriteC.frame.x + spriteC.frame.width) / (float)_sheet.m_textureWidth;
					float num5 = 1f - spriteC.frame.y / (float)_sheet.m_textureHeight;
					float num6 = 1f - (spriteC.frame.y + spriteC.frame.height) / (float)_sheet.m_textureHeight;
					if (spriteC.clip)
					{
						float num7 = num4 - num3;
						float num8 = num6 - num5;
						num3 += num7 * spriteC.clipLeft;
						num4 -= num7 * (spriteC.clipLeft + spriteC.clipRight);
						num5 += num8 * spriteC.clipBottom;
						num6 -= num8 * (spriteC.clipBottom + spriteC.clipTop);
					}
					if (spriteC.frame.flipX)
					{
						float num9 = num3;
						num3 = num4;
						num4 = num9;
					}
					if (spriteC.frame.flipY)
					{
						float num10 = num5;
						num5 = num6;
						num6 = num10;
					}
					int num11 = spriteC.vertDataIndex * 4;
					Vector2[] uvs = _sheet.m_UVs;
					uvs[num11 + 3].x = num4;
					uvs[num11 + 3].y = num5;
					uvs[num11 + 2].x = num4;
					uvs[num11 + 2].y = num6;
					uvs[num11 + 1].x = num3;
					uvs[num11 + 1].y = num6;
					uvs[num11].x = num3;
					uvs[num11].y = num5;
					spriteC.updateUVs = false;
					_sheet.m_uvsChanged = true;
				}
				if (spriteC.updateColors || _sheet.m_meshCapacityChanged)
				{
					int num12 = spriteC.vertDataIndex * 4;
					Color[] colors = _sheet.m_colors;
					colors[num12] = spriteC.color;
					colors[num12 + 1] = spriteC.color;
					colors[num12 + 2] = spriteC.color;
					colors[num12 + 3] = spriteC.color;
					spriteC.updateColors = false;
					_sheet.m_colorsChanged = true;
				}
			}
		}
		if (_sheet.m_vertsChanged)
		{
			_sheet.m_mesh.vertices = _sheet.m_vertices;
			_sheet.m_vertsChanged = false;
		}
		if (_sheet.m_colorsChanged)
		{
			_sheet.m_mesh.colors = _sheet.m_colors;
			_sheet.m_colorsChanged = false;
		}
		if (_sheet.m_uvsChanged)
		{
			_sheet.m_mesh.uv = _sheet.m_UVs;
			_sheet.m_uvsChanged = false;
		}
		_sheet.m_meshCapacityChanged = false;
		_sheet.m_components.Update();
	}

	// Token: 0x06002589 RID: 9609 RVA: 0x0019E678 File Offset: 0x0019CA78
	public static List<SpriteC> GetSpritesByTransform(TransformC _tc)
	{
		List<SpriteC> list = new List<SpriteC>();
		Entity p_entity = _tc.p_entity;
		if (p_entity == null)
		{
			return list;
		}
		for (int i = 0; i < p_entity.m_components.Count; i++)
		{
			if (p_entity.m_components[i].m_componentType == ComponentType.Sprite && (p_entity.m_components[i] as SpriteC).p_TC == _tc)
			{
				list.Add(p_entity.m_components[i] as SpriteC);
			}
		}
		return list;
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x0019E701 File Offset: 0x0019CB01
	public static List<PrefabC> ConvertSpritesToPrefabComponent(TransformC _tc, bool _removeSprites)
	{
		return SpriteS.ConvertSpritesToPrefabComponent(_tc, null, _removeSprites, null);
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x0019E70C File Offset: 0x0019CB0C
	public static List<PrefabC> ConvertSpritesToPrefabComponent(TransformC _tc, Camera _camera, bool _removeSprites, Shader _shader = null)
	{
		SpriteS.Update();
		List<PrefabC> list = new List<PrefabC>();
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Sprite, _tc.p_entity);
		List<GameObject> list2 = new List<GameObject>();
		List<SpriteSheet> list3 = new List<SpriteSheet>();
		List<List<int>> list4 = new List<List<int>>();
		if (componentsByEntity.Count > 0)
		{
			for (int i = 0; i < componentsByEntity.Count; i++)
			{
				SpriteC spriteC = componentsByEntity[i] as SpriteC;
				SpriteSheet p_spriteSheet = spriteC.p_spriteSheet;
				bool flag = false;
				if (list3.Count > 0)
				{
					for (int j = 0; j < list3.Count; j++)
					{
						if (p_spriteSheet == list3[j])
						{
							flag = true;
							int num = j;
							list4[num].Add(i);
							break;
						}
					}
				}
				if (!flag || list3.Count == 0)
				{
					int num = list3.Count;
					list3.Add(p_spriteSheet);
					list4.Add(new List<int>());
					list4[num].Add(i);
					list2.Add(new GameObject("ConvertedSprites"));
					if (_camera == null)
					{
						list2[num].layer = p_spriteSheet.m_camera.gameObject.layer;
					}
					else
					{
						list2[num].layer = _camera.gameObject.layer;
					}
					list2[num].AddComponent<MeshFilter>();
					MeshRenderer meshRenderer = list2[num].AddComponent<MeshRenderer>();
					meshRenderer.material = p_spriteSheet.m_material;
					if (_shader != null)
					{
						meshRenderer.material.shader = _shader;
					}
				}
			}
			for (int k = 0; k < list3.Count; k++)
			{
				Vector3[] array = new Vector3[list4[k].Count * 4];
				Vector3[] array2 = new Vector3[list4[k].Count * 4];
				Vector2[] array3 = new Vector2[list4[k].Count * 4];
				Color[] array4 = new Color[list4[k].Count * 4];
				int[] array5 = new int[list4[k].Count * 6];
				for (int l = 0; l < list4[k].Count; l++)
				{
					array5[l * 6 + 5] = l * 4;
					array5[l * 6 + 4] = l * 4 + 1;
					array5[l * 6 + 3] = l * 4 + 3;
					array5[l * 6 + 2] = l * 4 + 3;
					array5[l * 6 + 1] = l * 4 + 1;
					array5[l * 6] = l * 4 + 2;
				}
				int[] array6 = new int[list4[k].Count];
				float[] array7 = new float[list4[k].Count];
				for (int m = 0; m < list4[k].Count; m++)
				{
					array6[m] = list4[k][m];
					array7[m] = (componentsByEntity[list4[k][m]] as SpriteC).sortValue;
				}
				int[] array8 = ToolBox.sortTable(array6, array7);
				for (int n = 0; n < list4[k].Count; n++)
				{
					SpriteC spriteC2 = componentsByEntity[array8[n]] as SpriteC;
					int num2 = spriteC2.vertDataIndex * 4;
					int num3 = n * 4;
					Vector3[] vertices = list3[k].m_vertices;
					array[num3] = vertices[num2];
					array[num3 + 1] = vertices[num2 + 1];
					array[num3 + 2] = vertices[num2 + 2];
					array[num3 + 3] = vertices[num2 + 3];
					Vector2[] uvs = list3[k].m_UVs;
					array3[num3 + 3].x = uvs[num2 + 3].x;
					array3[num3 + 3].y = uvs[num2 + 3].y;
					array3[num3 + 2].x = uvs[num2 + 2].x;
					array3[num3 + 2].y = uvs[num2 + 2].y;
					array3[num3 + 1].x = uvs[num2 + 1].x;
					array3[num3 + 1].y = uvs[num2 + 1].y;
					array3[num3].x = uvs[num2].x;
					array3[num3].y = uvs[num2].y;
					Color[] colors = list3[k].m_colors;
					array4[num3] = colors[num2];
					array4[num3 + 1] = colors[num2 + 1];
					array4[num3 + 2] = colors[num2 + 2];
					array4[num3 + 3] = colors[num2 + 3];
					if (_removeSprites)
					{
						SpriteS.RemoveComponent(spriteC2);
					}
				}
				MeshFilter meshFilter = list2[k].GetComponent("MeshFilter") as MeshFilter;
				Mesh mesh = meshFilter.mesh;
				mesh.vertices = array;
				mesh.triangles = array5;
				mesh.uv = array3;
				mesh.colors = array4;
				mesh.normals = array2;
				mesh.RecalculateBounds();
				PrefabC prefabC = PrefabS.AddComponent(_tc, -_tc.transform.position, list2[k], "ConvertedSprites", true, true);
				if (_shader)
				{
					prefabC.p_gameObject.GetComponent<Renderer>().sharedMaterial = new Material(meshFilter.GetComponent<Renderer>().material);
					Object.DestroyImmediate(meshFilter.GetComponent<Renderer>().material);
				}
				list.Add(prefabC);
				Object.Destroy(mesh);
				Object.Destroy(list2[k]);
			}
		}
		return list;
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x0019ED95 File Offset: 0x0019D195
	public static GameObject CloneAsGameObject(SpriteSheet _sheet)
	{
		return Object.Instantiate<GameObject>(_sheet.m_gameObject);
	}

	// Token: 0x04002B1E RID: 11038
	public static int m_resizeTolerance = 64;

	// Token: 0x04002B1F RID: 11039
	public static GenericArray<SpriteSheet> m_sheets;

	// Token: 0x04002B20 RID: 11040
	private static SpriteC[] p_sprites;

	// Token: 0x04002B21 RID: 11041
	private static Vector3[] p_vertices;

	// Token: 0x04002B22 RID: 11042
	private static int[] p_aliveIndices;

	// Token: 0x04002B23 RID: 11043
	public static Vector3 m_outOfScreen = new Vector3(99999f, 99999f, -99999f);

	// Token: 0x04002B24 RID: 11044
	public static Color m_defaultColor = new Color(0.5f, 0.5f, 0.5f, 1f);

	// Token: 0x04002B25 RID: 11045
	private static int debug_rotationUpdates;

	// Token: 0x04002B26 RID: 11046
	private static int debug_scaleUpdates;

	// Token: 0x04002B27 RID: 11047
	private static int debug_positionUpdates;
}
