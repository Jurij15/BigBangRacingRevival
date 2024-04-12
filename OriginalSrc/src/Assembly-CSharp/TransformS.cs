using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000512 RID: 1298
public static class TransformS
{
	// Token: 0x060025DC RID: 9692 RVA: 0x001A23EA File Offset: 0x001A07EA
	public static void Initialize()
	{
		TransformS.m_components = new DynamicArray<TransformC>(100, 0.5f, 0.25f, 0.5f);
		TransformS.m_transformHelper.SetActive(false);
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x001A2412 File Offset: 0x001A0812
	public static TransformC AddComponent(Entity _entity)
	{
		return TransformS.AddComponent(_entity, string.Empty, Vector3.zero, Vector3.zero);
	}

	// Token: 0x060025DE RID: 9694 RVA: 0x001A2429 File Offset: 0x001A0829
	public static TransformC AddComponent(Entity _entity, string _name)
	{
		return TransformS.AddComponent(_entity, _name, Vector3.zero, Vector3.zero);
	}

	// Token: 0x060025DF RID: 9695 RVA: 0x001A243C File Offset: 0x001A083C
	public static TransformC AddComponent(Entity _entity, Vector3 _pos)
	{
		return TransformS.AddComponent(_entity, string.Empty, _pos, Vector3.zero);
	}

	// Token: 0x060025E0 RID: 9696 RVA: 0x001A244F File Offset: 0x001A084F
	public static TransformC AddComponent(Entity _entity, string _name, Vector3 _pos)
	{
		return TransformS.AddComponent(_entity, _name, _pos, Vector3.zero);
	}

	// Token: 0x060025E1 RID: 9697 RVA: 0x001A2460 File Offset: 0x001A0860
	public static TransformC AddComponent(Entity _entity, string _name, Vector3 _pos, Vector3 _rot)
	{
		TransformC transformC = TransformS.m_components.AddItem();
		if (!_name.Equals(string.Empty))
		{
			transformC.transform.name = _name;
		}
		transformC.transform.position = _pos;
		if (_rot != Vector3.zero)
		{
			transformC.transform.rotation = Quaternion.Euler(_rot);
		}
		else
		{
			transformC.transform.rotation = Quaternion.identity;
		}
		transformC.transform.gameObject.SetActive(true);
		EntityManager.AddComponentToEntity(_entity, transformC);
		return transformC;
	}

	// Token: 0x060025E2 RID: 9698 RVA: 0x001A24F0 File Offset: 0x001A08F0
	public static void RemoveComponent(TransformC _c)
	{
		if (_c.p_entity == null)
		{
			Debug.LogWarning("Trying to remove component that has already been removed");
			return;
		}
		if (_c.parent != null)
		{
			_c.parent.childs.Remove(_c);
			_c.parent = null;
		}
		_c.transform.parent = null;
		while (_c.childs.Count > 0)
		{
			int num = _c.childs.Count - 1;
			_c.childs[num].parent = null;
			_c.childs[num].transform.parent = null;
			_c.childs[num].level = 0;
			_c.childs.RemoveAt(num);
		}
		_c.transform.DetachChildren();
		_c.transform.gameObject.SetActive(false);
		_c.transform.name = "TransformComponent";
		_c.transform.gameObject.layer = 1;
		EntityManager.RemoveComponentFromEntity(_c);
		TransformS.m_components.RemoveItem(_c);
	}

	// Token: 0x060025E3 RID: 9699 RVA: 0x001A25FC File Offset: 0x001A09FC
	public static void UpdateChildHierarchyLevel(TransformC _parent)
	{
		for (int i = 0; i < _parent.childs.Count; i++)
		{
			_parent.childs[i].level = _parent.level + 1;
			TransformS.UpdateChildHierarchyLevel(_parent.childs[i]);
		}
	}

	// Token: 0x060025E4 RID: 9700 RVA: 0x001A264F File Offset: 0x001A0A4F
	public static void ParentComponent(TransformC _c, TransformC _parent)
	{
		TransformS.ParentComponent(_c, _parent, _c.transform.position - _parent.transform.position);
	}

	// Token: 0x060025E5 RID: 9701 RVA: 0x001A2674 File Offset: 0x001A0A74
	public static void ParentComponent(TransformC _c, TransformC _parent, Vector3 _childLocalPos)
	{
		if (_c.transform.parent != null)
		{
			TransformS.UnparentComponent(_c, true);
		}
		_c.transform.parent = _parent.transform;
		_parent.childs.Add(_c);
		_c.parent = _parent;
		_c.updatePosition = true;
		_c.updateRotation = true;
		_c.updateScale = true;
		TransformS.UpdateChildHierarchyLevel(_parent);
		TransformS.SetPosition(_c, _childLocalPos);
		TransformS.LoosenPhysicsConnections(_c);
	}

	// Token: 0x060025E6 RID: 9702 RVA: 0x001A26EC File Offset: 0x001A0AEC
	public static void UnparentComponent(TransformC _c, bool _affectUnityTransform = true)
	{
		if (_c.parent != null)
		{
			_c.parent.childs.Remove(_c);
		}
		_c.parent = null;
		if (_affectUnityTransform)
		{
			_c.transform.parent = null;
		}
		_c.level = 0;
		_c.updatePosition = true;
		_c.updateRotation = true;
		_c.updateScale = true;
		TransformS.UpdateChildHierarchyLevel(_c);
	}

	// Token: 0x060025E7 RID: 9703 RVA: 0x001A2754 File Offset: 0x001A0B54
	public static void LoosenPhysicsConnections(TransformC _c)
	{
		for (int i = 0; i < _c.childs.Count; i++)
		{
			TransformS.LoosenPhysicsConnections(_c.childs[i]);
		}
		_c.lastLocalPos = _c.transform.localPosition;
	}

	// Token: 0x060025E8 RID: 9704 RVA: 0x001A279F File Offset: 0x001A0B9F
	public static TransformC GetRootTransformComponent(TransformC _tc)
	{
		if (_tc.parent != null)
		{
			return TransformS.GetRootTransformComponent(_tc.parent);
		}
		return _tc;
	}

	// Token: 0x060025E9 RID: 9705 RVA: 0x001A27B9 File Offset: 0x001A0BB9
	public static TransformC GetParentTransformComponent(TransformC _tc)
	{
		if (_tc.parent != null)
		{
			return _tc.parent;
		}
		return _tc;
	}

	// Token: 0x060025EA RID: 9706 RVA: 0x001A27CE File Offset: 0x001A0BCE
	public static void SetTransform(TransformC _c, Vector3 _position, Vector3 _rotation, Vector3 _scale)
	{
		TransformS.SetPosition(_c, _position);
		TransformS.SetRotation(_c, _rotation);
		TransformS.SetScale(_c, _scale);
	}

	// Token: 0x060025EB RID: 9707 RVA: 0x001A27E5 File Offset: 0x001A0BE5
	public static void SetTransform(TransformC _c, Vector3 _position, Vector3 _rotation)
	{
		TransformS.SetPosition(_c, _position);
		TransformS.SetRotation(_c, _rotation);
	}

	// Token: 0x060025EC RID: 9708 RVA: 0x001A27F5 File Offset: 0x001A0BF5
	public static void SetGlobalTransform(TransformC _c, Vector3 _position, Vector3 _rotation)
	{
		TransformS.SetGlobalPosition(_c, _position);
		TransformS.SetGlobalRotation(_c, _rotation);
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x001A2805 File Offset: 0x001A0C05
	public static void SetTransform(TransformC _c, Vector3 _position, Vector3 _rotation, IntPtr _cpBody)
	{
		TransformS.SetPosition(_c, _position);
		TransformS.SetRotation(_c, _rotation);
		if (_cpBody != IntPtr.Zero)
		{
			ChipmunkProWrapper.ucpBodySetPos(_cpBody, _position);
			ChipmunkProWrapper.ucpBodySetAngle(_cpBody, _rotation.z * 0.017453292f);
		}
	}

	// Token: 0x060025EE RID: 9710 RVA: 0x001A2844 File Offset: 0x001A0C44
	public static void SetPosition(TransformC _c, Vector3 _position)
	{
		_c.transform.localPosition = _position;
		_c.updatePosition = true;
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x001A2859 File Offset: 0x001A0C59
	public static void SetGlobalPositionWithoutChildren(TransformC _c, Vector3 _position)
	{
		TransformS.SetGlobalPositionWithoutChildren(_c, _position, IntPtr.Zero);
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x001A2868 File Offset: 0x001A0C68
	public static void SetGlobalPositionWithoutChildren(TransformC _c, Vector3 _position, IntPtr _cpBody)
	{
		for (int i = 0; i < _c.childs.Count; i++)
		{
			_c.childs[i].transform.parent = null;
		}
		_c.transform.position = _position;
		_c.updatePosition = true;
		for (int j = 0; j < _c.childs.Count; j++)
		{
			_c.childs[j].transform.parent = _c.transform;
		}
		if (_cpBody != IntPtr.Zero)
		{
			ChipmunkProWrapper.ucpBodySetPos(_cpBody, _position);
		}
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x001A290F File Offset: 0x001A0D0F
	public static void SetGlobalPosition(TransformC _c, Vector3 _position)
	{
		_c.transform.position = _position;
		_c.updatePosition = true;
	}

	// Token: 0x060025F2 RID: 9714 RVA: 0x001A2924 File Offset: 0x001A0D24
	public static void SetGlobalPosition(TransformC _c, Vector3 _position, IntPtr _cpBody)
	{
		_c.transform.position = _position;
		_c.updatePosition = true;
		if (_cpBody != IntPtr.Zero)
		{
			ChipmunkProWrapper.ucpBodySetPos(_cpBody, _position);
		}
	}

	// Token: 0x060025F3 RID: 9715 RVA: 0x001A2955 File Offset: 0x001A0D55
	public static void Move(TransformC _c, Vector3 _step)
	{
		_c.transform.localPosition += _step;
		_c.updatePosition = true;
	}

	// Token: 0x060025F4 RID: 9716 RVA: 0x001A2975 File Offset: 0x001A0D75
	public static void GlobalMove(TransformC _c, Vector3 _step)
	{
		_c.transform.position += _step;
		_c.updatePosition = true;
	}

	// Token: 0x060025F5 RID: 9717 RVA: 0x001A2995 File Offset: 0x001A0D95
	public static void LookAt(TransformC _c, Transform _t, Vector3 _worldUp)
	{
		_c.transform.LookAt(_t, _worldUp);
		_c.updateRotation = true;
	}

	// Token: 0x060025F6 RID: 9718 RVA: 0x001A29AB File Offset: 0x001A0DAB
	public static void SetRotation(TransformC _c, Vector3 _rotation)
	{
		if (_c.forceRotation)
		{
			_c.forcedRotation = Quaternion.Euler(_rotation);
		}
		else
		{
			_c.transform.localRotation = Quaternion.Euler(_rotation);
		}
		_c.updateRotation = true;
	}

	// Token: 0x060025F7 RID: 9719 RVA: 0x001A29E4 File Offset: 0x001A0DE4
	public static void SetRotation(TransformC _c, Vector3 _rotation, IntPtr _cpBody)
	{
		if (_c.forceRotation)
		{
			_c.forcedRotation = Quaternion.Euler(_rotation);
		}
		else
		{
			_c.transform.localRotation = Quaternion.Euler(_rotation);
		}
		_c.updateRotation = true;
		if (_cpBody != IntPtr.Zero)
		{
			ChipmunkProWrapper.ucpBodySetAngle(_cpBody, _rotation.z * 0.017453292f);
		}
	}

	// Token: 0x060025F8 RID: 9720 RVA: 0x001A2A48 File Offset: 0x001A0E48
	public static void SetGlobalRotation(TransformC _c, Vector3 _rotation)
	{
		_c.transform.rotation = Quaternion.Euler(_rotation);
		_c.updateRotation = true;
	}

	// Token: 0x060025F9 RID: 9721 RVA: 0x001A2A62 File Offset: 0x001A0E62
	public static void SetGlobalRotation(TransformC _c, Vector3 _rotation, IntPtr _cpBody)
	{
		_c.transform.rotation = Quaternion.Euler(_rotation);
		_c.updateRotation = true;
		if (_cpBody != IntPtr.Zero)
		{
			ChipmunkProWrapper.ucpBodySetAngle(_cpBody, _rotation.z * 0.017453292f);
		}
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x001A2A9F File Offset: 0x001A0E9F
	public static void SetGlobalRotationWithoutChildren(TransformC _c, Vector3 _rotation)
	{
		TransformS.SetGlobalRotationWithoutChildren(_c, _rotation, IntPtr.Zero);
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x001A2AB0 File Offset: 0x001A0EB0
	public static void SetGlobalRotationWithoutChildren(TransformC _c, Vector3 _rotation, IntPtr _cpBody)
	{
		List<Vector3> list = new List<Vector3>();
		List<Quaternion> list2 = new List<Quaternion>();
		for (int i = 0; i < _c.childs.Count; i++)
		{
			list.Add(_c.childs[i].transform.position);
			list2.Add(_c.childs[i].transform.rotation);
			_c.childs[i].transform.parent = null;
		}
		_c.transform.rotation = Quaternion.Euler(_rotation);
		_c.updateRotation = true;
		for (int j = 0; j < _c.childs.Count; j++)
		{
			_c.childs[j].transform.parent = _c.transform;
			_c.childs[j].transform.position = list[j];
			_c.childs[j].transform.rotation = list2[j];
		}
		if (_cpBody != IntPtr.Zero)
		{
			ChipmunkProWrapper.ucpBodySetAngle(_cpBody, _rotation.z * 0.017453292f);
		}
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x001A2BE4 File Offset: 0x001A0FE4
	public static Vector3 Rotate(TransformC _c, Vector3 _rotation)
	{
		if (_c.forceRotation)
		{
			_c.forcedRotation.eulerAngles = _c.forcedRotation.eulerAngles + _rotation;
		}
		else
		{
			_c.transform.Rotate(_rotation);
		}
		_c.updateRotation = true;
		if (_c.forceRotation)
		{
			return _c.forcedRotation.eulerAngles;
		}
		return _c.transform.rotation.eulerAngles;
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x001A2C55 File Offset: 0x001A1055
	public static void SetScale(TransformC _c, Vector3 _scale)
	{
		if (_c.forceScale)
		{
			_c.forcedScale = _scale;
		}
		else
		{
			_c.transform.localScale = _scale;
		}
		_c.updateScale = true;
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x001A2C81 File Offset: 0x001A1081
	public static void SetScale(TransformC _c, float _scale)
	{
		if (_c.forceScale)
		{
			_c.forcedScale = Vector3.one * _scale;
		}
		else
		{
			_c.transform.localScale = Vector3.one * _scale;
		}
		_c.updateScale = true;
	}

	// Token: 0x060025FF RID: 9727 RVA: 0x001A2CC4 File Offset: 0x001A10C4
	public static void Scale(TransformC _c, float _scale)
	{
		if (_c.forceScale)
		{
			_c.forcedScale *= _scale;
		}
		else
		{
			_c.transform.localScale *= _scale;
		}
		_c.updateScale = true;
	}

	// Token: 0x06002600 RID: 9728 RVA: 0x001A2D14 File Offset: 0x001A1114
	public static void Scale(TransformC _c, Vector3 _scale)
	{
		if (_c.forceScale)
		{
			_c.forcedScale.x = _c.forcedScale.x * _scale.x;
			_c.forcedScale.y = _c.forcedScale.y * _scale.y;
			_c.forcedScale.z = _c.forcedScale.z * _scale.z;
		}
		else
		{
			Vector3 localScale = _c.transform.localScale;
			localScale.x *= _scale.x;
			localScale.y *= _scale.y;
			localScale.z *= _scale.z;
			_c.transform.localScale = localScale;
		}
		_c.updateScale = true;
	}

	// Token: 0x06002601 RID: 9729 RVA: 0x001A2DDC File Offset: 0x001A11DC
	public static void Update()
	{
		int aliveCount = TransformS.m_components.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			TransformC transformC = TransformS.m_components.m_array[TransformS.m_components.m_aliveIndices[i]];
			if (transformC.m_active)
			{
				if (!transformC.updatePosition && transformC.updatedPosition)
				{
					transformC.updatedPosition = false;
				}
				if (!transformC.updateRotation && transformC.updatedRotation)
				{
					transformC.updatedRotation = false;
				}
				if (!transformC.updateScale && transformC.updatedScale)
				{
					transformC.updatedScale = false;
				}
				if (transformC.parent != null)
				{
					TransformC parent = transformC.parent;
					if (parent.updatedPosition)
					{
						transformC.updatePosition = true;
					}
					if (parent.updatedRotation)
					{
						transformC.updatePosition = true;
						if (!transformC.forceRotation)
						{
							transformC.updateRotation = true;
						}
						else
						{
							transformC.transform.rotation = transformC.forcedRotation;
						}
					}
					if (parent.updatedScale)
					{
						transformC.updatePosition = true;
						if (!transformC.forceScale)
						{
							transformC.updateScale = true;
						}
						else
						{
							transformC.transform.localScale = transformC.forcedScale;
						}
					}
					if (transformC.parentedToPhysics)
					{
						transformC.updatedPosition = true;
						transformC.delta = transformC.transform.localPosition - transformC.lastLocalPos;
						TransformS.SetPosition(transformC, transformC.delta);
						transformC.lastLocalPos = transformC.transform.localPosition;
					}
				}
				if (transformC.updateRotation)
				{
					if (transformC.forceRotation)
					{
						transformC.transform.rotation = transformC.forcedRotation;
					}
					transformC.updateRotation = false;
					transformC.updatedRotation = true;
				}
				if (transformC.updateScale)
				{
					if (transformC.forceScale)
					{
						transformC.transform.localScale = transformC.forcedScale;
					}
					transformC.updateScale = false;
					transformC.updatedScale = true;
				}
				if (transformC.updatePosition)
				{
					transformC.updatePosition = false;
					transformC.updatedPosition = true;
				}
			}
		}
		TransformS.m_components.Update();
	}

	// Token: 0x04002B51 RID: 11089
	public static DynamicArray<TransformC> m_components;

	// Token: 0x04002B52 RID: 11090
	public static GameObject m_transformHelper = new GameObject("TransformComponent");
}
