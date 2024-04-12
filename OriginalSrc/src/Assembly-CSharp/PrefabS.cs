using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000505 RID: 1285
public static class PrefabS
{
	// Token: 0x06002513 RID: 9491 RVA: 0x00199A6C File Offset: 0x00197E6C
	public static void Initialize()
	{
		PrefabS.m_lateAnimUpdateList = new List<PrefabC>();
		PrefabS.m_components = new DynamicArray<PrefabC>(100, 0.5f, 0.25f, 0.5f);
		PrefabS.m_emptyGameObject = new GameObject("PrefabSystem: InstantiateHelper");
		MeshFilter meshFilter = PrefabS.m_emptyGameObject.AddComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();
		MeshRenderer meshRenderer = PrefabS.m_emptyGameObject.AddComponent<MeshRenderer>();
		meshRenderer.enabled = false;
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x00199AD5 File Offset: 0x00197ED5
	public static PrefabC AddComponent(TransformC _parentTC, Vector3 _offset)
	{
		return PrefabS.AddComponent(_parentTC, _offset, string.Empty);
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x00199AE4 File Offset: 0x00197EE4
	public static PrefabC AddComponent(TransformC _parentTC, Vector3 _offset, string _name)
	{
		PrefabC prefabC = PrefabS.m_components.AddItem();
		prefabC.p_gameObject = Object.Instantiate<GameObject>(PrefabS.m_emptyGameObject);
		prefabC.p_gameObject.GetComponent<Renderer>().enabled = true;
		prefabC.p_mesh = (prefabC.p_gameObject.GetComponent("MeshFilter") as MeshFilter).mesh;
		prefabC.p_gameObject.transform.parent = _parentTC.transform;
		prefabC.p_gameObject.transform.localPosition = _offset;
		prefabC.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		prefabC.p_parentTC = _parentTC;
		prefabC.m_name = _name;
		prefabC.m_wasVisible = true;
		prefabC.p_gameObject.GetComponent<Renderer>().castShadows = false;
		prefabC.p_gameObject.GetComponent<Renderer>().receiveShadows = false;
		EntityManager.AddComponentToEntity(_parentTC.p_entity, prefabC);
		return prefabC;
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x00199BC2 File Offset: 0x00197FC2
	public static PrefabC AddComponent(TransformC _parentTC, Vector3 _offset, GameObject _gameObject)
	{
		return PrefabS.AddComponent(_parentTC, _offset, _gameObject, string.Empty, true, true);
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x00199BD4 File Offset: 0x00197FD4
	public static PrefabC AddComponent(TransformC _parentTC, Vector3 _offset, GameObject _gameObject, string _identifier, bool _resetLocalRotation = true, bool _resetLocalPosition = true)
	{
		PrefabC prefabC = PrefabS.m_components.AddItem();
		Vector3 position = _gameObject.transform.position;
		prefabC.p_gameObject = Object.Instantiate<GameObject>(_gameObject, _parentTC.transform.position, _gameObject.transform.rotation);
		MeshFilter meshFilter = prefabC.p_gameObject.GetComponent("MeshFilter") as MeshFilter;
		if (meshFilter != null)
		{
			prefabC.p_mesh = meshFilter.mesh;
		}
		else
		{
			prefabC.p_mesh = null;
		}
		prefabC.p_gameObject.transform.parent = _parentTC.transform;
		if (_resetLocalPosition)
		{
			prefabC.p_gameObject.transform.localPosition = _offset;
		}
		else
		{
			prefabC.p_gameObject.transform.localPosition = position;
		}
		if (_resetLocalRotation)
		{
			prefabC.p_gameObject.transform.localRotation = Quaternion.identity;
		}
		prefabC.p_parentTC = _parentTC;
		prefabC.m_name = _identifier;
		prefabC.m_wasVisible = true;
		EntityManager.AddComponentToEntity(_parentTC.p_entity, prefabC);
		return prefabC;
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x00199CD8 File Offset: 0x001980D8
	public static PrefabC AddComponent(TransformC _parentTC, GameObject _gameObject, string _identifier = "")
	{
		PrefabC prefabC = PrefabS.m_components.AddItem();
		prefabC.p_gameObject = Object.Instantiate<GameObject>(_gameObject);
		MeshFilter meshFilter = prefabC.p_gameObject.GetComponent("MeshFilter") as MeshFilter;
		if (meshFilter != null)
		{
			prefabC.p_mesh = meshFilter.mesh;
		}
		else
		{
			prefabC.p_mesh = null;
		}
		prefabC.p_gameObject.transform.parent = _parentTC.transform;
		prefabC.p_parentTC = _parentTC;
		prefabC.m_name = _identifier;
		prefabC.m_wasVisible = true;
		EntityManager.AddComponentToEntity(_parentTC.p_entity, prefabC);
		return prefabC;
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x00199D70 File Offset: 0x00198170
	public static void RemoveComponent(PrefabC _c, bool _removeInstancedSharedMaterial = true)
	{
		if (_c.p_entity == null)
		{
			Debug.LogError("Trying to remove component that has already been removed");
			return;
		}
		if (_removeInstancedSharedMaterial && _c.p_gameObject != null)
		{
			Renderer[] componentsInChildren = _c.p_gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].sharedMaterial != null && (componentsInChildren[i].sharedMaterial.name.Contains("Instance") || componentsInChildren[i].sharedMaterial.name.Contains("Clone")))
				{
					Object.DestroyImmediate(componentsInChildren[i].sharedMaterial);
				}
			}
		}
		if (_c.p_mesh != null)
		{
			Object.Destroy(_c.p_mesh);
		}
		if (_c.p_gameObject != null)
		{
			Object.Destroy(_c.p_gameObject);
		}
		_c.p_mesh = null;
		_c.p_gameObject = null;
		_c.p_parentTC = null;
		_c.m_name = string.Empty;
		_c.m_wasVisible = false;
		_c.p_animators = null;
		_c.p_animatorInfos = null;
		EntityManager.RemoveComponentFromEntity(_c);
		PrefabS.m_components.RemoveItem(_c);
		if (PrefabS.m_lateAnimUpdateList.Contains(_c))
		{
			PrefabS.m_lateAnimUpdateList.Remove(_c);
		}
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x00199EBC File Offset: 0x001982BC
	public static void RemoveComponentsByEntity(Entity _e, bool _removeInstancedSharedMaterial = true)
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Prefab, _e);
		while (componentsByEntity.Count > 0)
		{
			int num = componentsByEntity.Count - 1;
			PrefabS.RemoveComponent(componentsByEntity[num] as PrefabC, _removeInstancedSharedMaterial);
			componentsByEntity.RemoveAt(num);
		}
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x00199F04 File Offset: 0x00198304
	public static void Update()
	{
		PrefabS.m_components.Update();
		if (PrefabS.m_lateAnimUpdateList.Count > 0)
		{
			PrefabS.m_lateUpdateTick--;
			if (PrefabS.m_lateUpdateTick < 0)
			{
				for (int i = 0; i < PrefabS.m_lateAnimUpdateList.Count; i++)
				{
					PrefabS.PlayLastAnimState(PrefabS.m_lateAnimUpdateList[i]);
				}
				PrefabS.m_lateAnimUpdateList.Clear();
				PrefabS.m_lateUpdateTick = 1;
			}
		}
	}

	// Token: 0x0600251C RID: 9500 RVA: 0x00199F7D File Offset: 0x0019837D
	public static void SetCamera(PrefabC _c, Camera _camera)
	{
		PrefabS.SetCameraLayer(_c, _camera.gameObject.layer);
	}

	// Token: 0x0600251D RID: 9501 RVA: 0x00199F90 File Offset: 0x00198390
	public static void SetCameraLayer(PrefabC _c, int _cameraLayer)
	{
		_c.p_gameObject.layer = _cameraLayer;
		for (int i = 0; i < _c.p_gameObject.transform.childCount; i++)
		{
			PrefabS.SetCameraLayer(_c.p_gameObject.transform.GetChild(i).gameObject, _cameraLayer);
		}
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x00199FE6 File Offset: 0x001983E6
	public static void SetCamera(GameObject _go, Camera _camera)
	{
		PrefabS.SetCameraLayer(_go, _camera.gameObject.layer);
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x00199FFC File Offset: 0x001983FC
	public static void SetCameraLayer(GameObject _go, int _cameraLayer)
	{
		_go.layer = _cameraLayer;
		for (int i = 0; i < _go.transform.childCount; i++)
		{
			PrefabS.SetCameraLayer(_go.transform.GetChild(i).gameObject, _cameraLayer);
		}
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x0019A044 File Offset: 0x00198444
	public static void PauseParticleEmission(PrefabC _c, bool _pause)
	{
		Component[] componentsInChildren = _c.p_gameObject.GetComponentsInChildren(typeof(ParticleSystem));
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ParticleSystem particleSystem = componentsInChildren[i] as ParticleSystem;
			if (_pause)
			{
				particleSystem.enableEmission = false;
			}
			else
			{
				particleSystem.enableEmission = true;
			}
		}
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x0019A0A0 File Offset: 0x001984A0
	public static void PauseParticleSystems(PrefabC _c, bool _pause)
	{
		if (_c != null && _c.p_gameObject != null)
		{
			Component[] componentsInChildren = _c.p_gameObject.GetComponentsInChildren(typeof(ParticleSystem));
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem particleSystem = componentsInChildren[i] as ParticleSystem;
				if (_pause && particleSystem.IsAlive())
				{
					particleSystem.Pause(true);
				}
				else if (particleSystem.isPaused && !particleSystem.isStopped)
				{
					particleSystem.Play(true);
				}
			}
		}
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x0019A134 File Offset: 0x00198534
	public static void PauseAnimations(PrefabC _c, bool _pause, bool _visibility)
	{
		if (_c != null && _c.p_gameObject != null)
		{
			Component[] componentsInChildren = _c.p_gameObject.GetComponentsInChildren(typeof(Animator));
			if (componentsInChildren != null)
			{
				if (_pause)
				{
					if (_visibility)
					{
						bool flag = false;
						if (_c.p_animatorInfos == null || _c.p_animators == null)
						{
							flag = true;
						}
						if (flag)
						{
							_c.p_animatorInfos = new LastAnimatorStateInfo[componentsInChildren.Length];
							_c.p_animators = new Animator[componentsInChildren.Length];
							for (int i = 0; i < componentsInChildren.Length; i++)
							{
								Animator animator = componentsInChildren[i] as Animator;
								_c.p_animatorInfos[i] = new LastAnimatorStateInfo(animator);
								_c.p_animators[i] = animator;
							}
							SkinnedMeshRenderer[] componentsInChildren2 = _c.p_gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
							for (int j = 0; j < componentsInChildren2.Length; j++)
							{
								for (int k = 0; k < componentsInChildren2[j].sharedMesh.blendShapeCount; k++)
								{
									componentsInChildren2[j].SetBlendShapeWeight(k, 0f);
								}
							}
						}
					}
					else
					{
						for (int l = 0; l < componentsInChildren.Length; l++)
						{
							Animator animator2 = componentsInChildren[l] as Animator;
							animator2.speed = 0f;
						}
					}
				}
				else if (_visibility && _c.p_animators != null)
				{
					PrefabS.m_lateAnimUpdateList.Add(_c);
				}
				else
				{
					for (int m = 0; m < componentsInChildren.Length; m++)
					{
						Animator animator3 = componentsInChildren[m] as Animator;
						animator3.speed = 1f;
					}
				}
			}
		}
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x0019A2D4 File Offset: 0x001986D4
	public static void PlayLastAnimState(PrefabC _c)
	{
		if (_c.m_active)
		{
			if (_c.p_animators != null)
			{
				for (int i = 0; i < _c.p_animators.Length; i++)
				{
					if (_c.p_animators[i] != null)
					{
						if (!_c.p_animators[i].isInitialized)
						{
							_c.p_animators[i].Rebind();
						}
						_c.p_animatorInfos[i].ApplyLastState(_c.p_animators[i]);
					}
				}
			}
			_c.p_animators = null;
			_c.p_animatorInfos = null;
		}
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x0019A368 File Offset: 0x00198768
	public static void SetVertexColors(PrefabC _c, Color _color)
	{
		MeshFilter meshFilter = _c.p_gameObject.GetComponent("MeshFilter") as MeshFilter;
		Mesh mesh = meshFilter.mesh;
		PrefabS.SetVertexColors(mesh, _color);
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x0019A39C File Offset: 0x0019879C
	public static void SetVertexColors(GameObject _gameObject, Color _color)
	{
		MeshFilter meshFilter = _gameObject.GetComponent("MeshFilter") as MeshFilter;
		Mesh mesh = meshFilter.mesh;
		PrefabS.SetVertexColors(mesh, _color);
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x0019A3C8 File Offset: 0x001987C8
	public static void SetVertexColors(Mesh _mesh, Color _color)
	{
		Color[] array = new Color[_mesh.colors.Length];
		for (int i = 0; i < _mesh.colors.Length; i++)
		{
			array[i] = _color;
		}
		_mesh.colors = array;
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x0019A410 File Offset: 0x00198810
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
				PrefabS.SetVisibilityByTransformComponent(_tc.childs[i], _visible, true, false);
			}
		}
		int aliveCount = PrefabS.m_components.m_aliveCount;
		for (int j = 0; j < aliveCount; j++)
		{
			PrefabC prefabC = PrefabS.m_components.m_array[PrefabS.m_components.m_aliveIndices[j]];
			if (prefabC.p_parentTC == _tc)
			{
				PrefabS.SetVisibility(prefabC, _visible, true);
			}
		}
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x0019A4B0 File Offset: 0x001988B0
	public static void SetVisibility(PrefabC _c, bool _visible, bool _markVisibility = true)
	{
		_c.p_gameObject.SetActive(_visible);
		if (_markVisibility)
		{
			_c.m_wasVisible = _visible;
		}
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x0019A4CC File Offset: 0x001988CC
	public static void ColorizeByTransformComponent(TransformC _tc, Color _color, bool _affectChildren, bool _affectWholeHierarchy)
	{
		if (_affectWholeHierarchy)
		{
			_tc = TransformS.GetRootTransformComponent(_tc);
		}
		if (_affectChildren || _affectWholeHierarchy)
		{
			for (int i = 0; i < _tc.childs.Count; i++)
			{
				PrefabS.ColorizeByTransformComponent(_tc.childs[i], _color, true, false);
			}
		}
		int aliveCount = PrefabS.m_components.m_aliveCount;
		for (int j = 0; j < aliveCount; j++)
		{
			PrefabC prefabC = PrefabS.m_components.m_array[PrefabS.m_components.m_aliveIndices[j]];
			if (prefabC.p_parentTC == _tc && prefabC.p_mesh != null)
			{
				PrefabS.SetVertexColors(prefabC.p_mesh, _color);
			}
		}
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x0019A581 File Offset: 0x00198981
	public static Color GetShaderColor(PrefabC _c)
	{
		return _c.p_gameObject.GetComponent<Renderer>().material.GetColor("_Color");
	}

	// Token: 0x0600252B RID: 9515 RVA: 0x0019A5A0 File Offset: 0x001989A0
	public static void SetShaderColor(PrefabC _c, Color _color)
	{
		if (_c.p_gameObject.GetComponent<Renderer>() != null)
		{
			_c.p_gameObject.GetComponent<Renderer>().material.SetColor("_Color", _color);
		}
		for (int i = 0; i < _c.p_gameObject.transform.childCount; i++)
		{
			PrefabS.SetShaderColorToGameObjectHierarchy(_c.p_gameObject.transform.GetChild(i), _color);
		}
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x0019A618 File Offset: 0x00198A18
	public static void SetShaderColorToGameObjectHierarchy(Transform _t, Color _color)
	{
		if (_t.gameObject.GetComponent<Renderer>() != null)
		{
			_t.gameObject.GetComponent<Renderer>().material.SetColor("_Color", _color);
		}
		for (int i = 0; i < _t.gameObject.transform.childCount; i++)
		{
			PrefabS.SetShaderColorToGameObjectHierarchy(_t.gameObject.transform.GetChild(i), _color);
		}
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x0019A690 File Offset: 0x00198A90
	public static PrefabC CreateRect(TransformC _tc, Vector3 _offset, float _width, float _height, Color _color, Material _material, Camera _camera)
	{
		PrefabC prefabC = PrefabS.AddComponent(_tc, Vector3.zero);
		prefabC.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		prefabC.p_gameObject.layer = _camera.gameObject.layer;
		prefabC.p_gameObject.GetComponent<Renderer>().material = _material;
		Vector3[] array = new Vector3[]
		{
			new Vector3(_width * -0.5f, _height * 0.5f, 0f) + _offset,
			new Vector3(_width * 0.5f, _height * 0.5f, 0f) + _offset,
			new Vector3(_width * 0.5f, _height * -0.5f, 0f) + _offset,
			new Vector3(_width * -0.5f, _height * -0.5f, 0f) + _offset
		};
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(0f, 1f),
			new Vector2(1f, 1f),
			new Vector2(1f, 0f),
			new Vector2(0f, 0f)
		};
		Color[] array3 = new Color[] { _color, _color, _color, _color };
		int[] array4 = new int[] { 0, 1, 2, 2, 3, 0 };
		prefabC.p_mesh.triangles = null;
		prefabC.p_mesh.vertices = null;
		prefabC.p_mesh.vertices = array;
		prefabC.p_mesh.triangles = array4;
		prefabC.p_mesh.uv = array2;
		prefabC.p_mesh.colors = array3;
		prefabC.p_mesh.RecalculateBounds();
		prefabC.p_mesh.RecalculateNormals();
		return prefabC;
	}

	// Token: 0x0600252E RID: 9518 RVA: 0x0019A8D4 File Offset: 0x00198CD4
	public static PrefabC CreatePathPrefabComponentFromVectorArray(TransformC _tc, Vector3 _offset, Vector2[] _points, float _width, Color _color, Material _material, Camera _camera, Position _align, bool _closed)
	{
		return PrefabS.CreatePathPrefabComponentFromVectorArray(_tc, _offset, _points, _width, _color, _color, _material, _camera, _align, _closed);
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x0019A8F8 File Offset: 0x00198CF8
	public static PrefabC CreatePathPrefabComponentFromVectorArray(TransformC _tc, Vector3 _offset, Vector2[] _points, float _width, Color _bottomColor, Color _topColor, Material _material, Camera _camera, Position _align, bool _closed)
	{
		float num = 99999f;
		float num2 = -99999f;
		float num3 = 99999f;
		float num4 = -99999f;
		bool flag = _topColor != _bottomColor;
		if (flag)
		{
			foreach (Vector2 vector in _points)
			{
				if (vector.y < num)
				{
					num = vector.y;
				}
				if (vector.y > num2)
				{
					num2 = vector.y;
				}
				if (vector.x < num3)
				{
					num3 = vector.x;
				}
				if (vector.x > num4)
				{
					num4 = vector.x;
				}
			}
		}
		float num5 = num2 - num;
		float num6 = num4 - num3;
		PrefabC prefabC = PrefabS.AddComponent(_tc, Vector3.zero);
		prefabC.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		prefabC.p_gameObject.layer = _camera.gameObject.layer;
		prefabC.p_gameObject.GetComponent<Renderer>().material = _material;
		Vector2[] array;
		if (_points[0] - _points[_points.Length - 1] == Vector2.zero || !_closed)
		{
			array = _points;
		}
		else
		{
			array = new Vector2[_points.Length + 1];
			_points.CopyTo(array, 0);
			array[array.Length - 1] = array[0];
		}
		Vector3[] array2 = new Vector3[array.Length * 2];
		Vector3[] array3 = new Vector3[array.Length * 2];
		Vector2[] array4 = new Vector2[array.Length * 2];
		Color[] array5 = new Color[array.Length * 2];
		int[] array6 = new int[array.Length * 6];
		float num7 = 0f;
		for (int j = 0; j < array.Length; j++)
		{
			Vector2 vector2 = array[j];
			Vector2 vector3;
			Vector2 vector4;
			if (!_closed)
			{
				if (j == 0)
				{
					vector3 = array[j] - (array[j + 1] - array[j]);
					vector4 = array[j + 1];
				}
				else if (j == array.Length - 1)
				{
					vector3 = array[j - 1];
					vector4 = array[j] + (array[j] - array[j - 1]);
				}
				else
				{
					vector3 = array[j - 1];
					vector4 = array[j + 1];
				}
			}
			else if (j == 0)
			{
				vector3 = array[array.Length - 2];
				vector4 = array[j + 1];
			}
			else if (j == array.Length - 1)
			{
				vector3 = array[j - 1];
				vector4 = array[1];
			}
			else
			{
				vector3 = array[j - 1];
				vector4 = array[j + 1];
			}
			Vector2 normalized = (vector3 - vector2).normalized;
			Vector2 normalized2 = (vector2 - vector4).normalized;
			float num8 = Mathf.Atan2(-normalized.y, normalized.x);
			float num9 = Mathf.Sin(num8);
			float num10 = Mathf.Cos(num8);
			Vector2 vector5;
			vector5..ctor(num9, num10);
			float num11 = Mathf.Atan2(-normalized2.y, normalized2.x);
			float num12 = Mathf.Sin(num11);
			float num13 = Mathf.Cos(num11);
			Vector2 vector6;
			vector6..ctor(num12, num13);
			Vector2 normalized3 = ((vector5 + vector6) * 0.5f).normalized;
			Vector3 vector7;
			vector7..ctor(normalized3.x, normalized3.y, 0f);
			Vector3 vector8;
			vector8..ctor(vector2.x, vector2.y, 0f);
			Vector3 vector9 = vector8;
			Vector3 vector10 = vector8;
			if (_align == Position.Center)
			{
				vector9 = vector8 + vector7 * _width * 0.5f;
				vector10 = vector8 - vector7 * _width * 0.5f;
			}
			else if (_align == Position.Inside)
			{
				vector9 = vector8 + vector7 * _width;
				vector10 = vector8;
			}
			else if (_align == Position.Outside)
			{
				vector9 = vector8;
				vector10 = vector8 - vector7 * _width;
			}
			array2[j * 2] = vector9 + _offset;
			array2[j * 2 + 1] = vector10 + _offset;
			num7 += (vector2 - vector3).magnitude;
			array4[j * 2] = Vector2.up * num7;
			array4[j * 2 + 1] = Vector2.up * num7 + Vector2.right;
			array3[j * 2] = Vector3.forward;
			array3[j * 2 + 1] = Vector3.forward;
			if (flag)
			{
				float num14 = (vector2.y - num) / num5;
				array5[j * 2] = _topColor * num14 + _bottomColor * (1f - num14);
				array5[j * 2 + 1] = array5[j * 2];
			}
			else
			{
				array5[j * 2] = _topColor;
				array5[j * 2 + 1] = _topColor;
			}
			if (j < array.Length - 1)
			{
				array6[j * 6] = j * 2;
				array6[j * 6 + 1] = j * 2 + 1;
				array6[j * 6 + 2] = j * 2 + 2;
				array6[j * 6 + 3] = j * 2 + 2;
				array6[j * 6 + 4] = j * 2 + 1;
				array6[j * 6 + 5] = j * 2 + 3;
			}
		}
		prefabC.p_mesh.triangles = null;
		prefabC.p_mesh.vertices = null;
		prefabC.p_mesh.vertices = array2;
		prefabC.p_mesh.triangles = array6;
		prefabC.p_mesh.uv = array4;
		prefabC.p_mesh.colors = array5;
		prefabC.p_mesh.normals = array3;
		prefabC.p_mesh.RecalculateBounds();
		prefabC.p_mesh.RecalculateNormals();
		return prefabC;
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x0019AFE4 File Offset: 0x001993E4
	public static PrefabC CreateSphericalPathFromVectorArray(TransformC _tc, Vector3 _offset, Vector3[] _points, float _width, Color _color, Material _material, Camera _camera, Position _align, bool _closed)
	{
		PrefabC prefabC = PrefabS.AddComponent(_tc, Vector3.zero);
		prefabC.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		prefabC.p_gameObject.layer = _camera.gameObject.layer;
		prefabC.p_gameObject.GetComponent<Renderer>().material = _material;
		Vector3[] array;
		if (_points[0] - _points[_points.Length - 1] == Vector3.zero || !_closed)
		{
			array = _points;
		}
		else
		{
			array = new Vector3[_points.Length + 1];
			_points.CopyTo(array, 0);
			array[array.Length - 1] = array[0];
		}
		Vector3[] array2 = new Vector3[array.Length * 2];
		Vector3[] array3 = new Vector3[array.Length * 2];
		Vector2[] array4 = new Vector2[array.Length * 2];
		Color[] array5 = new Color[array.Length * 2];
		int[] array6 = new int[array.Length * 6];
		float num = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector = array[i];
			Vector3 vector2;
			Vector3 vector3;
			if (!_closed)
			{
				if (i == 0)
				{
					vector2 = array[i] - (array[i + 1] - array[i]);
					vector3 = array[i + 1];
				}
				else if (i == array.Length - 1)
				{
					vector2 = array[i - 1];
					vector3 = array[i] + (array[i] - array[i - 1]);
				}
				else
				{
					vector2 = array[i - 1];
					vector3 = array[i + 1];
				}
			}
			else if (i == 0)
			{
				vector2 = array[array.Length - 2];
				vector3 = array[i + 1];
			}
			else if (i == array.Length - 1)
			{
				vector2 = array[i - 1];
				vector3 = array[1];
			}
			else
			{
				vector2 = array[i - 1];
				vector3 = array[i + 1];
			}
			Vector3 normalized = (vector2 - vector).normalized;
			Vector3 normalized2 = (vector - vector3).normalized;
			Vector3 vector4 = (normalized + normalized2) * 0.5f;
			Vector3 normalized3 = vector.normalized;
			Vector3 zero = Vector3.zero;
			Vector3.OrthoNormalize(ref normalized3, ref vector4, ref zero);
			Vector3 vector5 = vector;
			Vector3 vector6 = vector;
			if (_align == Position.Center)
			{
				vector5 = vector - zero * _width * 0.5f;
				vector6 = vector + zero * _width * 0.5f;
			}
			else if (_align == Position.Inside)
			{
				vector5 = vector - zero * _width;
				vector6 = vector;
			}
			else if (_align == Position.Outside)
			{
				vector5 = vector;
				vector6 = vector + zero * _width;
			}
			array2[i * 2] = vector5 + _offset;
			array2[i * 2 + 1] = vector6 + _offset;
			num += (vector - vector2).magnitude;
			array4[i * 2] = Vector2.up * num;
			array4[i * 2 + 1] = Vector2.up * num + Vector2.right;
			array3[i * 2] = Vector3.forward;
			array3[i * 2 + 1] = Vector3.forward;
			array5[i * 2] = _color;
			array5[i * 2 + 1] = _color;
			if (i < array.Length - 1)
			{
				array6[i * 6] = i * 2;
				array6[i * 6 + 1] = i * 2 + 1;
				array6[i * 6 + 2] = i * 2 + 2;
				array6[i * 6 + 3] = i * 2 + 2;
				array6[i * 6 + 4] = i * 2 + 1;
				array6[i * 6 + 5] = i * 2 + 3;
			}
		}
		prefabC.p_mesh.triangles = null;
		prefabC.p_mesh.vertices = null;
		prefabC.p_mesh.vertices = array2;
		prefabC.p_mesh.triangles = array6;
		prefabC.p_mesh.uv = array4;
		prefabC.p_mesh.colors = array5;
		prefabC.p_mesh.normals = array3;
		prefabC.p_mesh.RecalculateBounds();
		prefabC.p_mesh.RecalculateNormals();
		return prefabC;
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x0019B4FC File Offset: 0x001998FC
	public static PrefabC CreateFlatPrefabComponentsFromPolygon(TransformC _tc, Vector3 _offset, GGData _polygon, Color _color, Material _material, Camera _camera)
	{
		uint num = DebugDraw.ColorToUInt(_color);
		return PrefabS.CreateFlatPrefabComponentsFromPolygon(_tc, _offset, _polygon, num, num, _material, _camera, string.Empty, null);
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x0019B524 File Offset: 0x00199924
	public static PrefabC CreateFlatPrefabComponentsFromPolygon(TransformC _tc, Vector3 _offset, GGData _polygon, Color _bottomColor, Color _topColor, Material _material, Camera _camera)
	{
		uint num = DebugDraw.ColorToUInt(_topColor);
		uint num2 = DebugDraw.ColorToUInt(_bottomColor);
		return PrefabS.CreateFlatPrefabComponentsFromPolygon(_tc, _offset, _polygon, num2, num, _material, _camera, string.Empty, null);
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x0019B554 File Offset: 0x00199954
	public static PrefabC CreateFlatPrefabComponentsFromVectorArray(TransformC _tc, Vector3 _offset, Vector2[] _points, uint _bottomColor, uint _topColor, Material _material, Camera _camera, string _identifier, UVRect _normalizeRect = null)
	{
		GGData ggdata = new GGData(_points);
		return PrefabS.CreateFlatPrefabComponentsFromPolygon(_tc, _offset, ggdata, _bottomColor, _topColor, _material, _camera, _identifier, _normalizeRect);
	}

	// Token: 0x06002534 RID: 9524 RVA: 0x0019B57C File Offset: 0x0019997C
	public static PrefabC CreateFlatPrefabComponentsFromPolygon(TransformC _tc, Vector3 _offset, GGData _polygon, uint _bottomColor, uint _topColor, Material _material, Camera _camera, string _identifier, UVRect _normalizeRect)
	{
		if (_polygon.m_vertices.Count > 0)
		{
			_polygon.SetUvMapPlanar(1f, 1f, Vector2.zero, _normalizeRect);
			_polygon.SetColorGradientVertical(_topColor, _bottomColor);
			_polygon.SetOffset(_offset);
			Mesh mesh = GeometryGenerator.GenerateFlatMesh(_polygon);
			if (mesh != null)
			{
				return PrefabS.CreatePrefabFromMesh(_tc, mesh, _camera.gameObject.layer, _material, true, true, false);
			}
		}
		else
		{
			Debug.LogError("NO VERTICES IN POLYGON!");
		}
		return null;
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x0019B600 File Offset: 0x00199A00
	public static PrefabC CreateBeltPrefabFromVectorArray(TransformC _tc, Vector3 _offset, float _depth, Vector2[] _points, Color _color, Material _material, Camera _camera, string _identifier)
	{
		PrefabC prefabC = PrefabS.AddComponent(_tc, _offset);
		prefabC.p_gameObject.layer = _camera.gameObject.layer;
		prefabC.p_gameObject.GetComponent<Renderer>().material = _material;
		if (_points[0] != _points[_points.Length - 1])
		{
			Vector2[] array = new Vector2[_points.Length + 1];
			_points.CopyTo(array, 0);
			array[array.Length - 1] = array[0];
			_points = array;
		}
		List<Vector3> list = new List<Vector3>(_points.Length * 2);
		List<Vector2> list2 = new List<Vector2>(_points.Length * 2);
		List<Color> list3 = new List<Color>(_points.Length * 2);
		List<int> list4 = new List<int>((_points.Length * 2 - 2) * 3);
		float num = 0f;
		Vector2 zero = Vector2.zero;
		for (int i = 0; i < _points.Length; i++)
		{
			Vector2 vector = _points[i];
			if (i > 0)
			{
				num += (vector - _points[i - 1]).magnitude;
			}
			list.AddRange(new Vector3[2]);
			list2.AddRange(new Vector2[2]);
			list3.AddRange(new Color[2]);
			list4.AddRange(new int[6]);
			list[i * 2] = new Vector3(vector.x, vector.y, _depth);
			list[i * 2 + 1] = new Vector3(vector.x, vector.y, 0f);
			list2[i * 2] = new Vector2(num / 100f, 1f);
			list2[i * 2 + 1] = new Vector2(num / 100f, 0.5f);
			list3[i * 2] = _color;
			list3[i * 2 + 1] = _color;
			if (i > 0)
			{
				list4[(i * 2 - 2) * 3] = i * 2 - 2;
				list4[(i * 2 - 2) * 3 + 1] = i * 2 - 2 + 2;
				list4[(i * 2 - 2) * 3 + 2] = i * 2 - 2 + 1;
				list4[(i * 2 - 1) * 3] = i * 2 - 1;
				list4[(i * 2 - 1) * 3 + 1] = i * 2 - 1 + 1;
				list4[(i * 2 - 1) * 3 + 2] = i * 2 - 1 + 2;
			}
		}
		prefabC.p_mesh.vertices = list.ToArray();
		prefabC.p_mesh.triangles = list4.ToArray();
		prefabC.p_mesh.uv = list2.ToArray();
		prefabC.p_mesh.colors = list3.ToArray();
		prefabC.p_mesh.RecalculateBounds();
		prefabC.p_mesh.RecalculateNormals();
		return prefabC;
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x0019B8E8 File Offset: 0x00199CE8
	public static PrefabC CreateBeltPrefabFromVectorArray(TransformC _tc, Vector3 _offset, float _depth, Vector2[] _pointsFar, Vector2[] _pointsNear, Color _color, Material _material, Camera _camera, string _identifier)
	{
		if (_pointsFar.Length != _pointsNear.Length)
		{
			Debug.LogError("Vertex arrays has to be of same length");
			return null;
		}
		PrefabC prefabC = PrefabS.AddComponent(_tc, _offset);
		prefabC.p_gameObject.layer = _camera.gameObject.layer;
		prefabC.p_gameObject.GetComponent<Renderer>().material = _material;
		if (_pointsFar[0] != _pointsFar[_pointsFar.Length - 1])
		{
			Vector2[] array = new Vector2[_pointsFar.Length + 1];
			_pointsFar.CopyTo(array, 0);
			array[array.Length - 1] = array[0];
			_pointsFar = array;
			array = new Vector2[_pointsNear.Length + 1];
			_pointsNear.CopyTo(array, 0);
			array[array.Length - 1] = array[0];
			_pointsNear = array;
		}
		List<Vector3> list = new List<Vector3>(_pointsFar.Length * 2);
		List<Vector2> list2 = new List<Vector2>(_pointsFar.Length * 2);
		List<Color> list3 = new List<Color>(_pointsFar.Length * 2);
		List<int> list4 = new List<int>((_pointsFar.Length * 2 - 2) * 3);
		float num = 0f;
		Vector2 zero = Vector2.zero;
		for (int i = 0; i < _pointsFar.Length; i++)
		{
			Vector2 vector = _pointsFar[i];
			Vector2 vector2 = _pointsNear[i];
			if (i > 0)
			{
				num += (vector - _pointsFar[i - 1]).magnitude;
			}
			list.AddRange(new Vector3[2]);
			list2.AddRange(new Vector2[2]);
			list3.AddRange(new Color[2]);
			list4.AddRange(new int[6]);
			list[i * 2] = new Vector3(vector.x, vector.y, _depth);
			list[i * 2 + 1] = new Vector3(vector2.x, vector2.y, 0f);
			list2[i * 2] = new Vector2(num / 100f, 1f);
			list2[i * 2 + 1] = new Vector2(num / 100f, 0.5f);
			list3[i * 2] = _color;
			list3[i * 2 + 1] = _color;
			if (i > 0)
			{
				list4[(i * 2 - 2) * 3] = i * 2 - 2;
				list4[(i * 2 - 2) * 3 + 1] = i * 2 - 2 + 2;
				list4[(i * 2 - 2) * 3 + 2] = i * 2 - 2 + 1;
				list4[(i * 2 - 1) * 3] = i * 2 - 1;
				list4[(i * 2 - 1) * 3 + 1] = i * 2 - 1 + 1;
				list4[(i * 2 - 1) * 3 + 2] = i * 2 - 1 + 2;
			}
		}
		prefabC.p_mesh.vertices = list.ToArray();
		prefabC.p_mesh.triangles = list4.ToArray();
		prefabC.p_mesh.uv = list2.ToArray();
		prefabC.p_mesh.colors = list3.ToArray();
		prefabC.p_mesh.RecalculateBounds();
		prefabC.p_mesh.RecalculateNormals();
		return prefabC;
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x0019BC2A File Offset: 0x0019A02A
	public static PrefabC CreatePrefabFromMesh(TransformC _tc, Mesh _mesh, Camera _camera, Material _material, bool _destroyMesh, bool _recalcNormals = false, bool _optimize = false)
	{
		return PrefabS.CreatePrefabFromMesh(_tc, _mesh, _camera.gameObject.layer, _material, _destroyMesh, _recalcNormals, _optimize);
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x0019BC48 File Offset: 0x0019A048
	public static PrefabC CreatePrefabFromMesh(TransformC _tc, Mesh _mesh, int _cameraLayer, Material _material, bool _destroyMesh, bool _recalcNormals = false, bool _optimize = false)
	{
		if (_mesh != null)
		{
			PrefabC prefabC = PrefabS.AddComponent(_tc, Vector3.zero);
			CombineInstance[] array = new CombineInstance[1];
			array[0].mesh = _mesh;
			prefabC.p_mesh.CombineMeshes(array, true, false);
			prefabC.p_mesh.RecalculateBounds();
			if (_recalcNormals)
			{
				prefabC.p_mesh.RecalculateNormals();
			}
			if (_optimize)
			{
			}
			prefabC.p_gameObject.GetComponent<Renderer>().material = _material;
			prefabC.p_gameObject.layer = _cameraLayer;
			if (_destroyMesh)
			{
				Object.DestroyImmediate(_mesh);
			}
			return prefabC;
		}
		return null;
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x0019BCE0 File Offset: 0x0019A0E0
	public static PrefabC CreatePrefabFromMeshArray(TransformC _tc, Mesh[] _meshes, Camera _camera, Material _material, bool _destroyMeshes, bool _recalcNormals = false, bool _optimize = false)
	{
		return PrefabS.CreatePrefabFromMeshArray(_tc, _meshes, _camera.gameObject.layer, _material, _destroyMeshes, _recalcNormals, _optimize);
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x0019BCFC File Offset: 0x0019A0FC
	public static PrefabC CreatePrefabFromMeshArray(TransformC _tc, Mesh[] _meshes, int _cameraLayer, Material _material, bool _destroyMeshes, bool _recalcNormals = false, bool _optimize = false)
	{
		CombineInstance[] array = new CombineInstance[_meshes.Length];
		for (int i = 0; i < _meshes.Length; i++)
		{
			CombineInstance combineInstance = default(CombineInstance);
			combineInstance.mesh = _meshes[i];
			array[i] = combineInstance;
		}
		PrefabC prefabC = PrefabS.AddComponent(_tc, Vector3.zero);
		prefabC.p_mesh.CombineMeshes(array, true, false);
		prefabC.p_mesh.RecalculateBounds();
		if (_recalcNormals)
		{
			prefabC.p_mesh.RecalculateNormals();
		}
		if (_optimize)
		{
		}
		prefabC.p_gameObject.GetComponent<Renderer>().material = _material;
		prefabC.p_gameObject.layer = _cameraLayer;
		if (_destroyMeshes)
		{
			for (int j = 0; j < _meshes.Length; j++)
			{
				if (_meshes[j] != null)
				{
					Object.DestroyImmediate(_meshes[j]);
				}
			}
		}
		return prefabC;
	}

	// Token: 0x0600253B RID: 9531 RVA: 0x0019BDD8 File Offset: 0x0019A1D8
	public static UVRect GetLargestUVRect(float _texWidth, float _texHeight, float _targetWidth, float _targetHeight)
	{
		if (_texWidth == _targetWidth && _texHeight == _targetHeight)
		{
			return UVRect.Normal();
		}
		if (_texWidth <= _targetWidth && _texHeight <= _targetHeight)
		{
			float num = _texWidth / _targetWidth;
			float num2 = _texHeight / _targetHeight;
			if (num > num2)
			{
				float num3 = _targetHeight / _targetWidth / (_texHeight / _texWidth);
				return new UVRect(0f, (1f - num3) * 0.5f, 1f, num3);
			}
			float num4 = _texHeight / _texWidth / (_targetHeight / _targetWidth);
			return new UVRect((1f - num4) * 0.5f, 0f, num4, 1f);
		}
		else if (_texWidth > _targetWidth && _texHeight > _targetHeight)
		{
			float num5 = _texWidth / _targetWidth;
			float num6 = _texHeight / _targetHeight;
			if (num5 > num6)
			{
				float num7 = _texHeight / _texWidth / (_targetHeight / _targetWidth);
				return new UVRect((1f - num7) * 0.5f, 0f, num7, 1f);
			}
			float num8 = _targetHeight / _targetWidth / (_texHeight / _texWidth);
			return new UVRect(0f, (1f - num8) * 0.5f, 1f, num8);
		}
		else
		{
			if (_texWidth <= _targetWidth && _texHeight > _targetHeight)
			{
				float num9 = _targetHeight / _targetWidth / (_texHeight / _texWidth);
				return new UVRect(0f, (1f - num9) * 0.5f, 1f, num9);
			}
			if (_texWidth > _targetWidth && _texHeight <= _targetHeight)
			{
				float num10 = _texHeight / _texWidth / (_targetHeight / _targetWidth);
				return new UVRect((1f - num10) * 0.5f, 0f, num10, 1f);
			}
			return UVRect.Normal();
		}
	}

	// Token: 0x04002AED RID: 10989
	public static DynamicArray<PrefabC> m_components;

	// Token: 0x04002AEE RID: 10990
	public static GameObject m_emptyGameObject;

	// Token: 0x04002AEF RID: 10991
	private static List<PrefabC> m_lateAnimUpdateList;

	// Token: 0x04002AF0 RID: 10992
	private static int m_lateUpdateTick = 1;
}
