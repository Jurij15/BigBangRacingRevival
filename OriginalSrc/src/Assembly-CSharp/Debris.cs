using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class Debris
{
	// Token: 0x060000FC RID: 252 RVA: 0x0000C184 File Offset: 0x0000A584
	public Debris(Vector3 _pos, GameObject _prefab, ucpShape _shape, string _particleEffectPath, Vector3 _particleOffSet, bool _calculateCenteroid, float _destroyAfterSecs = -1f, bool tween = false)
	{
		this.m_entity = EntityManager.AddEntity(new string[] { "GTAG_AUTODESTROY", "GTAG_DEBRIS" });
		this.m_tc = TransformS.AddComponent(this.m_entity, "Debris-" + _prefab.name);
		Vector3 eulerAngles = _prefab.transform.eulerAngles;
		Vector2 offset = _shape.offset;
		if (!_calculateCenteroid)
		{
			_shape.offset = Vector2.zero;
		}
		TransformC transformC = TransformS.AddComponent(this.m_entity);
		TransformS.ParentComponent(transformC, this.m_tc, Vector3.zero);
		TransformS.SetTransform(this.m_tc, _pos, eulerAngles);
		if (_prefab != null)
		{
			PrefabC prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, _prefab.transform.position.z) + offset * ((!_calculateCenteroid) ? (-1f) : 1f), _prefab, string.Empty, true, true);
		}
		if (_particleEffectPath != null)
		{
			PrefabS.AddComponent(transformC, _particleOffSet, ResourceManager.GetGameObject(_particleEffectPath));
		}
		if (_destroyAfterSecs > 0f)
		{
			TimerS.AddComponent(this.m_entity, "DebrisTimer", _destroyAfterSecs, 0f, true, null);
		}
		this.m_body = ChipmunkProS.AddDynamicBody(this.m_tc, _shape, null);
		if (tween)
		{
			TweenS.AddTransformTween(transformC, TweenedProperty.Position, TweenStyle.Linear, new Vector3(0f, 0f, 90f), 1f, 0f, true);
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0000C310 File Offset: 0x0000A710
	public static bool CreateDebrisFromGO(Transform _parentTransform, Vector2 _linVel, float _angVel, bool _deleteGO = true, uint _group = 0U, bool _calculateCenteroid = false, string _particleEffectPath = null, Vector3 _particleOffSet = default(Vector3), float _destroyTime = -1f, uint _layer = 1U)
	{
		return Debris.CreateDebrisFromGO(_parentTransform, Vector3.zero, _linVel, _angVel, _deleteGO, _group, _calculateCenteroid, _particleEffectPath, _particleOffSet, _destroyTime, _layer);
	}

	// Token: 0x060000FE RID: 254 RVA: 0x0000C338 File Offset: 0x0000A738
	public static bool CreateDebrisFromGO(Transform _parentTransform, Vector3 _position, Vector2 _linVel, float _angVel, bool _deleteGO = true, uint _group = 0U, bool _calculateCenteroid = false, string _particleEffectPath = null, Vector3 _particleOffSet = default(Vector3), float _destroyTime = -1f, uint _layer = 1U)
	{
		if (_parentTransform == null || _parentTransform.childCount == 0)
		{
			return false;
		}
		Transform transform = _parentTransform.Find(_parentTransform.name + "Collider");
		if (transform != null)
		{
			Vector2 vector = Vector2.zero;
			if (_calculateCenteroid)
			{
				Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
				Vector2[] array = new Vector2[mesh.vertexCount];
				for (int i = 0; i < mesh.vertexCount; i++)
				{
					array[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].y);
				}
				vector = ChipmunkProWrapper.ucpCenteroidForPoly(array.Length, array);
			}
			ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(transform.gameObject, -vector, 1f, 0.5f, 0.5f, (ucpCollisionType)7, _layer, false, false);
			ucpPolyShape.group = _group;
			ucpPolyShape.mass = 1f + ucpPolyShape.area * 0.033f;
			Vector3 vector2;
			if (_position == Vector3.zero)
			{
				vector2..ctor(transform.transform.position.x, transform.transform.position.y);
			}
			else
			{
				vector2 = _position;
			}
			Debris debris = new Debris(vector2, _parentTransform.gameObject, ucpPolyShape, _particleEffectPath, _particleOffSet, _calculateCenteroid, _destroyTime, false);
			ChipmunkProWrapper.ucpBodySetVel(debris.m_body.body, _linVel);
			ChipmunkProWrapper.ucpBodySetAngVel(debris.m_body.body, _angVel);
			if (PsState.m_activeMinigame != null)
			{
				Vector2 vector3 = PsState.m_activeMinigame.m_globalGravity * (float)PsState.m_activeMinigame.m_gravityMultipler;
				ChipmunkProWrapper.ucpBodySetGravity(debris.m_body.body, vector3);
				ChipmunkProWrapper.ucpBodyActivate(debris.m_body.body);
			}
			if (_deleteGO)
			{
				_parentTransform.gameObject.SetActive(false);
			}
			return true;
		}
		return false;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000C530 File Offset: 0x0000A930
	public static Debris ConvertGameObjectToDebrisWithZTween(Transform _objectTransform, Vector3 _position, Vector2 _linVel, float _angVel, bool _deleteGO = true, uint _group = 0U, bool _calculateCenteroid = false, string _particleEffectPath = null, Vector3 _particleOffSet = default(Vector3), float _destroyTime = -1f, uint _layer = 1U, bool _tween = true)
	{
		if (_objectTransform == null || _objectTransform.gameObject.GetComponent<MeshFilter>() == null)
		{
			if (_objectTransform.childCount == 0 || _objectTransform.GetChild(0).gameObject.GetComponent<MeshFilter>() == null)
			{
				return null;
			}
			_objectTransform = _objectTransform.GetChild(0);
		}
		Vector2 vector = Vector2.zero;
		if (_calculateCenteroid)
		{
			Mesh mesh = _objectTransform.GetComponent<MeshFilter>().mesh;
			Vector2[] array = new Vector2[mesh.vertexCount];
			for (int i = 0; i < mesh.vertexCount; i++)
			{
				array[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].y);
			}
			vector = ChipmunkProWrapper.ucpCenteroidForPoly(array.Length, array);
		}
		vector..ctor(_objectTransform.transform.localPosition.x - _objectTransform.parent.transform.localPosition.x, _objectTransform.transform.localPosition.y - _objectTransform.parent.transform.localPosition.y);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(_objectTransform.gameObject, -vector, 1f, 0.5f, 0.5f, (ucpCollisionType)7, _layer, false, false);
		ucpPolyShape.group = _group;
		ucpPolyShape.mass = 1f + ucpPolyShape.area * 0.033f;
		Vector3 vector2;
		if (_position == Vector3.zero)
		{
			vector2..ctor(_objectTransform.transform.position.x, _objectTransform.transform.position.y);
		}
		else
		{
			vector2 = _position;
		}
		Debris debris = new Debris(vector2, _objectTransform.gameObject, ucpPolyShape, _particleEffectPath, _particleOffSet, true, _destroyTime, _tween);
		ChipmunkProWrapper.ucpBodySetVel(debris.m_body.body, _linVel);
		ChipmunkProWrapper.ucpBodySetAngVel(debris.m_body.body, _angVel);
		if (PsState.m_activeMinigame != null)
		{
			Vector2 vector3 = PsState.m_activeMinigame.m_globalGravity * (float)PsState.m_activeMinigame.m_gravityMultipler;
			ChipmunkProWrapper.ucpBodySetGravity(debris.m_body.body, vector3);
			ChipmunkProWrapper.ucpBodyActivate(debris.m_body.body);
		}
		if (_deleteGO)
		{
			Object.DestroyImmediate(_objectTransform.gameObject);
		}
		return debris;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000C79C File Offset: 0x0000AB9C
	public static void CreateDebrisFromChildren(Transform _parentTransform, Vector2 _linVel, Vector2 _linVelRandom, float _angVelRandom, bool _deleteGO = true, uint _group = 0U)
	{
		for (int i = 0; i < _parentTransform.childCount; i++)
		{
			Vector2 vector = _linVel + new Vector2(Random.Range(-_linVelRandom.x, _linVelRandom.x), Random.Range(-_linVelRandom.y, _linVelRandom.y));
			float num = Random.Range(-_angVelRandom, _angVelRandom) * 0.017453292f;
			Debris.CreateDebrisFromGO(_parentTransform.transform.GetChild(i), vector, num, false, _group, false, null, default(Vector3), -1f, 1U);
		}
		if (_deleteGO)
		{
			while (_parentTransform.transform.childCount > 0)
			{
				Object.DestroyImmediate(_parentTransform.transform.GetChild(0).gameObject);
			}
		}
	}

	// Token: 0x040000BD RID: 189
	public Entity m_entity;

	// Token: 0x040000BE RID: 190
	public ChipmunkBodyC m_body;

	// Token: 0x040000BF RID: 191
	public TransformC m_tc;

	// Token: 0x040000C0 RID: 192
	private List<Debris> m_debris;
}
