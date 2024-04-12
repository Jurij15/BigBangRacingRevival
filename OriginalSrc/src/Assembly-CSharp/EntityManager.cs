using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DB RID: 1243
public static class EntityManager
{
	// Token: 0x06002304 RID: 8964 RVA: 0x0019194C File Offset: 0x0018FD4C
	public static void Initialize()
	{
		EntityManager.m_entities = new DynamicArray<Entity>(100, 0.5f, 0.25f, 0.5f);
		EntityManager.m_updateList = new List<Entity>();
		EntityManager.m_removeList = new List<Entity>();
		EntityManager.m_tags = new DynamicArray<Tag>(100, 0.5f, 0.25f, 0.5f);
		EntityManager.m_destroyLinks = new List<DestroyLink>();
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x001919B0 File Offset: 0x0018FDB0
	public static Entity AddEntity()
	{
		string[] array = new string[0];
		return EntityManager.AddEntity(array);
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x001919CC File Offset: 0x0018FDCC
	public static Entity AddEntity(string _tag)
	{
		if (_tag != string.Empty)
		{
			string[] array = new string[] { _tag };
			return EntityManager.AddEntity(array);
		}
		return EntityManager.AddEntity();
	}

	// Token: 0x06002307 RID: 8967 RVA: 0x00191A00 File Offset: 0x0018FE00
	public static Entity AddEntity(string[] _tags)
	{
		Entity entity = EntityManager.m_entities.AddItem();
		if (_tags != null)
		{
			for (int i = 0; i < _tags.Length; i++)
			{
				EntityManager.AddTagForEntity(entity, _tags[i]);
			}
		}
		entity.m_active = true;
		return entity;
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x00191A44 File Offset: 0x0018FE44
	public static TransformC AddTimedFXEntity(GameObject _resource, Vector3 _pos, Vector3 _rotation, float _destroyAfterSecs, string _tag, TransformC _parentTC = null)
	{
		Entity entity = EntityManager.AddEntity(_tag);
		TimerS.AddComponent(entity, "Timer", _destroyAfterSecs, 0f, true, null);
		TransformC transformC = TransformS.AddComponent(entity, string.Empty, _pos, _rotation);
		PrefabS.AddComponent(transformC, Vector3.zero, _resource);
		if (_parentTC != null)
		{
			TransformS.ParentComponent(transformC, _parentTC);
		}
		return transformC;
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x00191A98 File Offset: 0x0018FE98
	public static TransformC AddEntityWithTC()
	{
		string[] array = new string[0];
		return EntityManager.AddEntityWithTC(array);
	}

	// Token: 0x0600230A RID: 8970 RVA: 0x00191AB4 File Offset: 0x0018FEB4
	public static TransformC AddEntityWithTC(string _tag)
	{
		string[] array = new string[] { _tag };
		return EntityManager.AddEntityWithTC(array);
	}

	// Token: 0x0600230B RID: 8971 RVA: 0x00191AD4 File Offset: 0x0018FED4
	public static TransformC AddEntityWithTC(string[] _tags)
	{
		Entity entity = EntityManager.AddEntity(_tags);
		return TransformS.AddComponent(entity);
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x00191AF0 File Offset: 0x0018FEF0
	public static void RemoveEntity(Entity _e)
	{
		EntityManager.RemoveEntity(_e, true);
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x00191AFC File Offset: 0x0018FEFC
	public static void AddLogicDelegate(Entity _e, EntityLogicDelegate _logicDelegate)
	{
		if (_e.m_entityLogicCount == 0)
		{
			_e.d_entityLogic = _logicDelegate;
		}
		else
		{
			_e.d_entityLogic = (EntityLogicDelegate)Delegate.Combine(_e.d_entityLogic, _logicDelegate);
		}
		if (!EntityManager.m_updateList.Contains(_e))
		{
			EntityManager.m_updateList.Add(_e);
		}
		_e.m_entityLogicCount++;
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x00191B60 File Offset: 0x0018FF60
	public static void RemoveLogicDelegate(Entity _e, EntityLogicDelegate _logicDelegate)
	{
		if (_e.d_entityLogic != null)
		{
			Delegate[] invocationList = _e.d_entityLogic.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				if ((EntityLogicDelegate)@delegate == _logicDelegate)
				{
					_e.d_entityLogic = (EntityLogicDelegate)Delegate.Remove(_e.d_entityLogic, (EntityLogicDelegate)@delegate);
					_e.m_entityLogicCount--;
					break;
				}
			}
		}
		if (_e.m_entityLogicCount == 0)
		{
			_e.d_entityLogic = null;
			EntityManager.m_updateList.Remove(_e);
		}
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x00191BFC File Offset: 0x0018FFFC
	public static void RemoveAllLogicDelegates(Entity _e)
	{
		if (_e.d_entityLogic != null)
		{
			Delegate[] invocationList = _e.d_entityLogic.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				_e.d_entityLogic = (EntityLogicDelegate)Delegate.Remove(_e.d_entityLogic, (EntityLogicDelegate)@delegate);
			}
		}
		_e.m_entityLogicCount = 0;
		EntityManager.m_updateList.Remove(_e);
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x00191C69 File Offset: 0x00190069
	public static void RemoveEntitiesByTransformComponentHierarchy(TransformC _tc, bool _removeParents)
	{
		EntityManager.RemoveEntitiesByTransformComponentHierarchy(_tc, _removeParents, false);
	}

	// Token: 0x06002311 RID: 8977 RVA: 0x00191C74 File Offset: 0x00190074
	public static void RemoveEntitiesByTransformComponentHierarchy(TransformC _tc, bool _removeParents, bool _removeImmediately)
	{
		if (_tc != null)
		{
			if (_removeParents)
			{
				_tc = TransformS.GetRootTransformComponent(_tc);
			}
			while (_tc.childs.Count > 0)
			{
				int num = _tc.childs.Count - 1;
				EntityManager.RemoveEntitiesByTransformComponentHierarchy(_tc.childs[num], false, _removeImmediately);
				_tc.childs.RemoveAt(num);
			}
			EntityManager.RemoveEntity(_tc.p_entity, true, _removeImmediately);
		}
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x00191CE8 File Offset: 0x001900E8
	public static void RemoveEntityByTransformComponent(TransformC _tc, bool _removeChildren)
	{
		if (_removeChildren)
		{
			while (_tc.childs.Count > 0)
			{
				EntityManager.RemoveEntitiesByTransformComponentHierarchy(_tc.childs[0], false);
				_tc.childs.Remove(_tc.childs[0]);
			}
		}
		EntityManager.RemoveEntity(_tc.p_entity);
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x00191D46 File Offset: 0x00190146
	public static void RemoveEntity(Entity _e, bool _removeFromList)
	{
		EntityManager.RemoveEntity(_e, _removeFromList, false);
	}

	// Token: 0x06002314 RID: 8980 RVA: 0x00191D50 File Offset: 0x00190150
	public static void RemoveEntity(Entity _e, bool _removeFromList, bool _removeImmediately)
	{
		_e.m_active = false;
		for (int i = EntityManager.m_destroyLinks.Count - 1; i > -1; i--)
		{
			DestroyLink destroyLink = EntityManager.m_destroyLinks[i];
			if (destroyLink.primary == _e)
			{
				EntityManager.m_destroyLinks.RemoveAt(i);
				EntityManager.RemoveEntity(destroyLink.secondary, _removeFromList, _removeFromList);
			}
			else if (destroyLink.bothWays && destroyLink.secondary == _e)
			{
				EntityManager.m_destroyLinks.RemoveAt(i);
				EntityManager.RemoveEntity(destroyLink.primary, _removeFromList, _removeFromList);
			}
			else if (destroyLink.secondary == _e)
			{
				EntityManager.m_destroyLinks.RemoveAt(i);
			}
		}
		if (!_removeImmediately)
		{
			for (int j = 0; j < _e.m_components.Count; j++)
			{
				_e.m_components[j].m_active = false;
			}
			EntityManager.m_removeList.Add(_e);
			_e.m_state = PoolableState.MARKED_TO_BE_DELETED;
		}
		else
		{
			EntityManager.RemoveAllTagsFromEntity(_e, false);
			EntityManager.RemoveAllLogicDelegates(_e);
			while (_e.m_components.Count > 0)
			{
				IComponent component = _e.m_components[_e.m_components.Count - 1];
				switch (component.m_componentType)
				{
				case ComponentType.CameraTarget:
					CameraS.RemoveTargetComponent(component as CameraTargetC);
					break;
				case ComponentType.ChipmunkBody:
					ChipmunkProS.RemoveBody(component as ChipmunkBodyC);
					break;
				case ComponentType.ChipmunkConstraint:
					ChipmunkProS.RemoveConstraint(component as ChipmunkConstraintC);
					break;
				case ComponentType.Prefab:
					PrefabS.RemoveComponent(component as PrefabC, true);
					break;
				case ComponentType.Sound:
					SoundS.RemoveComponent(component as SoundC);
					break;
				case ComponentType.Sprite:
					SpriteS.RemoveComponent(component as SpriteC);
					break;
				case ComponentType.TextMesh:
					TextMeshS.RemoveComponent(component as TextMeshC);
					break;
				case ComponentType.Timer:
					TimerS.RemoveComponent(component as TimerC);
					break;
				case ComponentType.TouchArea:
					TouchAreaS.RemoveArea(component as TouchAreaC);
					break;
				case ComponentType.Transform:
					TransformS.RemoveComponent(component as TransformC);
					break;
				case ComponentType.Tween:
					TweenS.RemoveComponent(component as TweenC);
					break;
				case ComponentType.Projector:
					ProjectorS.RemoveComponent(component as ProjectorC);
					break;
				case ComponentType.Http:
					HttpS.RemoveComponent(component as HttpC);
					break;
				case ComponentType.MonoBehaviour:
					MonoBehaviourS.RemoveComponent(component as MonoBehaviourC);
					break;
				default:
					Main.m_currentGame.RemoveComponent(component);
					break;
				}
			}
			if (_removeFromList)
			{
				_e.m_persistent = false;
				EntityManager.m_entities.RemoveItem(_e);
			}
		}
	}

	// Token: 0x06002315 RID: 8981 RVA: 0x00191FE4 File Offset: 0x001903E4
	public static void AddDestroyLink(Entity _primary, Entity _seconday, bool _bothWays)
	{
		DestroyLink destroyLink = default(DestroyLink);
		destroyLink.primary = _primary;
		destroyLink.secondary = _seconday;
		destroyLink.bothWays = _bothWays;
		EntityManager.m_destroyLinks.Add(destroyLink);
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x0019201C File Offset: 0x0019041C
	public static void RemoveDestroyLinks(Entity _entity)
	{
		for (int i = EntityManager.m_destroyLinks.Count - 1; i > -1; i--)
		{
			DestroyLink destroyLink = EntityManager.m_destroyLinks[i];
			if (destroyLink.primary == _entity || destroyLink.secondary == _entity)
			{
				EntityManager.m_destroyLinks.RemoveAt(i);
			}
		}
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x00192078 File Offset: 0x00190478
	public static void RemoveDestroyLink(Entity _primary, Entity _secondary)
	{
		for (int i = EntityManager.m_destroyLinks.Count - 1; i > -1; i--)
		{
			DestroyLink destroyLink = EntityManager.m_destroyLinks[i];
			if (destroyLink.primary == _primary && destroyLink.secondary == _secondary)
			{
				EntityManager.m_destroyLinks.RemoveAt(i);
				break;
			}
		}
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x001920D8 File Offset: 0x001904D8
	public static void RemoveAllEntities()
	{
		EntityManager.Update();
		List<Entity> list = new List<Entity>();
		for (int i = 0; i < EntityManager.m_entities.m_aliveCount; i++)
		{
			Entity entity = EntityManager.m_entities.m_array[EntityManager.m_entities.m_aliveIndices[i]];
			if (!entity.m_persistent)
			{
				EntityManager.RemoveEntity(entity, false, true);
				list.Add(entity);
			}
		}
		while (list.Count > 0)
		{
			int num = list.Count - 1;
			EntityManager.m_entities.RemoveItem(list[num]);
			list.RemoveAt(num);
		}
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x0019216F File Offset: 0x0019056F
	public static void RemoveEntitiesByTag(string _tag)
	{
		EntityManager.RemoveEntitiesByTag(_tag, false);
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x00192178 File Offset: 0x00190578
	public static void RemoveEntitiesByTag(string _tag, bool _removeImmediately)
	{
		List<Entity> list = new List<Entity>();
		for (int i = EntityManager.m_tags.m_aliveCount - 1; i > -1; i--)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			if (tag.m_tag.Equals(_tag))
			{
				list.Add(tag.p_entity);
			}
		}
		while (list.Count > 0)
		{
			int num = list.Count - 1;
			EntityManager.RemoveEntity(list[num], true, _removeImmediately);
			list.RemoveAt(num);
		}
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x0019220C File Offset: 0x0019060C
	public static Entity GetEntityByIndex(int _entityIndex)
	{
		return EntityManager.m_entities.m_array[_entityIndex];
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x0019221C File Offset: 0x0019061C
	public static List<Entity> GetEntitiesByTag(string _tag)
	{
		List<Entity> list = new List<Entity>();
		for (int i = 0; i < EntityManager.m_tags.m_aliveCount; i++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			if (tag.m_tag == _tag)
			{
				list.Add(tag.p_entity);
			}
		}
		return list;
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x00192280 File Offset: 0x00190680
	public static List<IComponent> GetComponentsByEntity(ComponentType _componentType, Entity _e)
	{
		List<IComponent> list = new List<IComponent>();
		if (_e == null)
		{
			Debug.LogError("Trying to get components from null entity");
			return null;
		}
		for (int i = 0; i < _e.m_components.Count; i++)
		{
			if (_e.m_components[i].m_componentType == _componentType)
			{
				list.Add(_e.m_components[i]);
			}
		}
		return list;
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x001922EC File Offset: 0x001906EC
	public static IComponent GetComponentByEntity(ComponentType _componentType, Entity _e)
	{
		if (_e == null)
		{
			Debug.LogError("Trying to get components from null entity");
			return null;
		}
		for (int i = 0; i < _e.m_components.Count; i++)
		{
			if (_e.m_components[i].m_componentType == _componentType)
			{
				return _e.m_components[i];
			}
		}
		return null;
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x0019234C File Offset: 0x0019074C
	public static List<IComponent> GetComponentsByType(ComponentType _componentType)
	{
		List<IComponent> list = new List<IComponent>();
		for (int i = 0; i < EntityManager.m_entities.m_aliveCount; i++)
		{
			Entity entity = EntityManager.m_entities.m_array[EntityManager.m_entities.m_aliveIndices[i]];
			for (int j = 0; j < entity.m_components.Count; j++)
			{
				if (entity.m_components[j].m_componentType == _componentType)
				{
					list.Add(entity.m_components[j]);
				}
			}
		}
		return list;
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x001923D8 File Offset: 0x001907D8
	public static IComponent GetComponentByIdentifier(Entity _e, int _identifier)
	{
		for (int i = 0; i < _e.m_components.Count; i++)
		{
			IComponent component = _e.m_components[i];
			if (component.m_identifier == _identifier)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x00192420 File Offset: 0x00190820
	public static List<IComponent> GetComponentsByIdentifier(Entity _e, int _identifier)
	{
		List<IComponent> list = new List<IComponent>();
		for (int i = 0; i < _e.m_components.Count; i++)
		{
			IComponent component = _e.m_components[i];
			if (component.m_identifier == _identifier)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x00192470 File Offset: 0x00190870
	public static void AddTagForEntity(Entity _entity, string _tag)
	{
		Tag tag = EntityManager.m_tags.AddItem();
		tag.m_tag = _tag;
		tag.p_entity = _entity;
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x00192498 File Offset: 0x00190898
	public static void RemoveTagFromEntity(Entity _entity, string _tag)
	{
		for (int i = 0; i < EntityManager.m_tags.m_aliveCount; i++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			if (tag.p_entity == _entity && tag.m_tag == _tag)
			{
				EntityManager.m_tags.RemoveItem(tag);
				return;
			}
		}
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x00192504 File Offset: 0x00190904
	public static bool EntityHasTag(Entity _entity, string _tag)
	{
		for (int i = 0; i < EntityManager.m_tags.m_aliveCount; i++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			if (tag.p_entity == _entity && tag.m_tag == _tag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x00192564 File Offset: 0x00190964
	public static void RemoveAllTagsFromEntity(Entity _entity, bool _includeTransformChildren = false)
	{
		List<Tag> list = new List<Tag>();
		if (_includeTransformChildren)
		{
			for (int i = 0; i < _entity.m_components.Count; i++)
			{
				if (_entity.m_components[i].m_componentType == ComponentType.Transform)
				{
					TransformC transformC = _entity.m_components[i] as TransformC;
					for (int j = 0; j < transformC.childs.Count; j++)
					{
						if (transformC.childs[j].p_entity != _entity)
						{
							EntityManager.RemoveAllTagsFromEntity(transformC.childs[j].p_entity, true);
						}
					}
				}
			}
		}
		for (int k = 0; k < EntityManager.m_tags.m_aliveCount; k++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[k]];
			if (tag.p_entity == _entity)
			{
				list.Add(tag);
			}
		}
		while (list.Count > 0)
		{
			int num = list.Count - 1;
			EntityManager.m_tags.RemoveItem(list[num]);
			list.RemoveAt(num);
		}
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x00192690 File Offset: 0x00190A90
	public static List<string> GetAllTagsFromEntity(Entity _entity)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < EntityManager.m_tags.m_aliveCount; i++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			if (tag.p_entity == _entity)
			{
				list.Add(tag.m_tag);
			}
		}
		return list;
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x001926F0 File Offset: 0x00190AF0
	public static void RemoveTagFromAllEntities(string _tag)
	{
		List<Tag> list = new List<Tag>();
		for (int i = 0; i < EntityManager.m_tags.m_aliveCount; i++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			if (tag.m_tag == _tag)
			{
				list.Add(tag);
			}
		}
		while (list.Count > 0)
		{
			int num = list.Count - 1;
			EntityManager.m_tags.RemoveItem(list[num]);
			list.RemoveAt(num);
		}
	}

	// Token: 0x06002328 RID: 9000 RVA: 0x00192780 File Offset: 0x00190B80
	public static List<Entity> SetVisibilityOfEntitiesWithTag(string _tag, bool _visible)
	{
		List<Entity> list = new List<Entity>();
		for (int i = 0; i < EntityManager.m_tags.m_aliveCount; i++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			if (tag.m_tag == _tag && tag.p_entity.m_visible != _visible)
			{
				EntityManager.SetVisibilityOfEntity(tag.p_entity, _visible);
				list.Add(tag.p_entity);
			}
		}
		return list;
	}

	// Token: 0x06002329 RID: 9001 RVA: 0x00192804 File Offset: 0x00190C04
	public static void SetVisibilityOfEntity(Entity _e, bool _visible)
	{
		_e.m_visible = _visible;
		for (int i = 0; i < _e.m_components.Count; i++)
		{
			if (_e.m_components[i].m_componentType == ComponentType.Sprite)
			{
				SpriteC spriteC = _e.m_components[i] as SpriteC;
				if (spriteC.wasVisible)
				{
					SpriteS.SetVisibility(spriteC, _visible, false);
				}
			}
			else if (_e.m_components[i].m_componentType == ComponentType.Prefab)
			{
				PrefabC prefabC = _e.m_components[i] as PrefabC;
				if (prefabC.m_wasVisible)
				{
					PrefabS.SetVisibility(prefabC, _visible, false);
				}
			}
			else if (_e.m_components[i].m_componentType == ComponentType.TextMesh)
			{
				TextMeshC textMeshC = _e.m_components[i] as TextMeshC;
				if (textMeshC.m_wasVisible)
				{
					TextMeshS.SetVisibility(textMeshC, _visible, false);
				}
			}
		}
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x001928F4 File Offset: 0x00190CF4
	public static void SetActivityOfEntity(Entity _e, bool _active, bool _visibility = true, bool _physics = true, bool _prefabs = true, bool _tcChildren = true, bool _setWasActive = true)
	{
		_e.m_active = _active;
		if (_visibility)
		{
			_e.m_visible = _active;
		}
		for (int i = 0; i < _e.m_components.Count; i++)
		{
			IComponent component = _e.m_components[i];
			if (_setWasActive || component.m_wasActive)
			{
				component.m_active = _active;
				if (_visibility)
				{
					if (component.m_componentType == ComponentType.Transform && _tcChildren)
					{
						TransformC transformC = component as TransformC;
						for (int j = 0; j < transformC.childs.Count; j++)
						{
							if (transformC.childs[j].p_entity != _e)
							{
								EntityManager.SetActivityOfEntity(transformC.childs[j].p_entity, _active, _visibility, _tcChildren, true, true, true);
							}
						}
					}
					if (!_active && component.m_componentType == ComponentType.Prefab && _prefabs)
					{
						PrefabS.PauseParticleSystems(component as PrefabC, !_active);
						PrefabS.PauseAnimations(component as PrefabC, !_active, _visibility);
					}
					if (component.m_componentType == ComponentType.Sprite)
					{
						SpriteC spriteC = component as SpriteC;
						if (spriteC.wasVisible)
						{
							SpriteS.SetVisibility(spriteC, _active, false);
						}
					}
					else if (component.m_componentType == ComponentType.Prefab)
					{
						PrefabC prefabC = component as PrefabC;
						if (prefabC.m_wasVisible)
						{
							PrefabS.SetVisibility(prefabC, _active, false);
						}
					}
					else if (component.m_componentType == ComponentType.TextMesh)
					{
						TextMeshC textMeshC = component as TextMeshC;
						if (textMeshC.m_wasVisible)
						{
							TextMeshS.SetVisibility(textMeshC, _active, false);
						}
					}
					else if (component.m_componentType == ComponentType.Projector)
					{
						ProjectorC projectorC = component as ProjectorC;
						if (projectorC.m_wasVisible)
						{
							ProjectorS.SetVisibility(projectorC, _active, false);
						}
					}
				}
				else if (!_active && component.m_componentType == ComponentType.Prefab && _prefabs)
				{
					PrefabS.PauseParticleSystems(component as PrefabC, !_active);
					PrefabS.PauseAnimations(component as PrefabC, !_active, _visibility);
				}
				if (_active && component.m_componentType == ComponentType.Prefab && _prefabs)
				{
					PrefabS.PauseParticleSystems(component as PrefabC, !_active);
					PrefabS.PauseAnimations(component as PrefabC, !_active, _visibility);
				}
				if (component.m_componentType == ComponentType.Http && !_active)
				{
					HttpS.SetInActive(component as HttpC);
				}
				if (component.m_componentType == ComponentType.Sound)
				{
					SoundC soundC = component as SoundC;
					if (soundC.isPlaying)
					{
						if (_active)
						{
							SoundS.ResumeSound(soundC);
						}
						else
						{
							SoundS.PauseSound(soundC);
						}
					}
				}
				if (_physics)
				{
					if (component.m_componentType == ComponentType.ChipmunkBody)
					{
						ChipmunkBodyC chipmunkBodyC = component as ChipmunkBodyC;
						if (ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) == ucpBodyType.DYNAMIC)
						{
							if (!_active)
							{
								if (!chipmunkBodyC.m_isDisabled)
								{
									chipmunkBodyC.m_savedGravity = ChipmunkProWrapper.ucpBodyGetGravity(chipmunkBodyC.body);
									ChipmunkProWrapper.ucpBodySetGravity(chipmunkBodyC.body, Vector2.zero);
									ChipmunkProWrapper.ucpBodyResetForces(chipmunkBodyC.body);
									ChipmunkProWrapper.ucpBodySetAngVel(chipmunkBodyC.body, 0f);
									ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, Vector2.zero);
									ChipmunkProWrapper.ucpBodySetMoment(chipmunkBodyC.body, float.PositiveInfinity);
									for (int k = 0; k < chipmunkBodyC.shapes.Count; k++)
									{
										ChipmunkProWrapper.ucpShapeSetLayers(chipmunkBodyC.shapes[k].shapePtr, 0U);
									}
									chipmunkBodyC.m_isDisabled = true;
								}
							}
							else if (chipmunkBodyC.m_isDisabled)
							{
								ChipmunkProWrapper.ucpBodySetMoment(chipmunkBodyC.body, chipmunkBodyC.m_moment);
								ChipmunkProWrapper.ucpBodySetGravity(chipmunkBodyC.body, chipmunkBodyC.m_savedGravity);
								ChipmunkProWrapper.ucpBodyResetForces(chipmunkBodyC.body);
								ChipmunkProWrapper.ucpBodySetAngVel(chipmunkBodyC.body, 0f);
								ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, Vector2.zero);
								for (int l = 0; l < chipmunkBodyC.shapes.Count; l++)
								{
									ChipmunkProWrapper.ucpShapeSetLayers(chipmunkBodyC.shapes[l].shapePtr, chipmunkBodyC.shapes[l].layerMask);
								}
								chipmunkBodyC.m_isDisabled = false;
							}
						}
						else if (ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) == ucpBodyType.KINEMATIC)
						{
							if (!_active)
							{
								if (!chipmunkBodyC.m_isDisabled)
								{
									ChipmunkProWrapper.ucpBodyResetForces(chipmunkBodyC.body);
									ChipmunkProWrapper.ucpBodySetAngVel(chipmunkBodyC.body, 0f);
									ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, Vector2.zero);
									ChipmunkProWrapper.ucpBodySetMoment(chipmunkBodyC.body, float.PositiveInfinity);
									for (int m = 0; m < chipmunkBodyC.shapes.Count; m++)
									{
										ChipmunkProWrapper.ucpShapeSetLayers(chipmunkBodyC.shapes[m].shapePtr, 0U);
									}
									chipmunkBodyC.m_isDisabled = true;
								}
							}
							else if (chipmunkBodyC.m_isDisabled)
							{
								ChipmunkProWrapper.ucpBodySetMoment(chipmunkBodyC.body, chipmunkBodyC.m_moment);
								ChipmunkProWrapper.ucpBodyResetForces(chipmunkBodyC.body);
								ChipmunkProWrapper.ucpBodySetAngVel(chipmunkBodyC.body, 0f);
								ChipmunkProWrapper.ucpBodySetVel(chipmunkBodyC.body, Vector2.zero);
								for (int n = 0; n < chipmunkBodyC.shapes.Count; n++)
								{
									ChipmunkProWrapper.ucpShapeSetLayers(chipmunkBodyC.shapes[n].shapePtr, chipmunkBodyC.shapes[n].layerMask);
								}
								chipmunkBodyC.m_isDisabled = false;
							}
						}
					}
					if (component.m_componentType == ComponentType.ChipmunkConstraint)
					{
						ChipmunkConstraintC chipmunkConstraintC = component as ChipmunkConstraintC;
						if (!_active)
						{
							if (chipmunkConstraintC.addedToSpace)
							{
								ChipmunkProWrapper.ucpSpaceRemoveConstraint(chipmunkConstraintC.constraint);
								chipmunkConstraintC.addedToSpace = false;
							}
						}
						else if (!chipmunkConstraintC.addedToSpace)
						{
							ChipmunkProWrapper.ucpSpaceAddConstraint(chipmunkConstraintC.constraint);
							chipmunkConstraintC.addedToSpace = true;
						}
					}
				}
				if (_setWasActive)
				{
					component.m_wasActive = _active;
				}
			}
		}
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x00192EDC File Offset: 0x001912DC
	public static void SetActivityOfAllEntities(bool _active, bool _visibility = true, bool _physics = true, bool _prefabs = true, bool _tcChildren = false, bool _setWasActive = false)
	{
		int aliveCount = EntityManager.m_entities.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			Entity entity = EntityManager.m_entities.m_array[EntityManager.m_entities.m_aliveIndices[i]];
			EntityManager.SetActivityOfEntity(entity, _active, _visibility, _physics, _prefabs, _tcChildren, _setWasActive);
		}
	}

	// Token: 0x0600232C RID: 9004 RVA: 0x00192F2C File Offset: 0x0019132C
	public static List<Entity> SetActivityOfEntitiesWithTag(string _tag, bool _active, bool _visibility = true, bool _physics = true, bool _prefabs = true, bool _tcChildren = false, bool _setWasActive = false)
	{
		List<Entity> list = new List<Entity>();
		for (int i = 0; i < EntityManager.m_tags.m_aliveCount; i++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			if (tag.m_tag == _tag && tag.p_entity.m_active != _active)
			{
				EntityManager.SetActivityOfEntity(tag.p_entity, _active, _visibility, _physics, _prefabs, _tcChildren, _setWasActive);
				list.Add(tag.p_entity);
			}
		}
		return list;
	}

	// Token: 0x0600232D RID: 9005 RVA: 0x00192FB8 File Offset: 0x001913B8
	public static List<Entity> SetActivityOfEntitiesWithTag(string[] _tags, bool _active, bool _visibility = true, bool _physics = true, bool _prefabs = true, bool _tcChildren = false, bool _setWasActive = false)
	{
		List<Entity> list = new List<Entity>();
		for (int i = 0; i < EntityManager.m_tags.m_aliveCount; i++)
		{
			Tag tag = EntityManager.m_tags.m_array[EntityManager.m_tags.m_aliveIndices[i]];
			for (int j = 0; j < _tags.Length; j++)
			{
				if (tag.m_tag == _tags[j] && tag.p_entity.m_active != _active)
				{
					EntityManager.SetActivityOfEntity(tag.p_entity, _active, _visibility, _physics, _prefabs, _tcChildren, _setWasActive);
					list.Add(tag.p_entity);
				}
			}
		}
		return list;
	}

	// Token: 0x0600232E RID: 9006 RVA: 0x00193058 File Offset: 0x00191458
	public static void AddComponentToEntity(Entity _e, IComponent _c)
	{
		if (_c == null || _e == null)
		{
			Debug.LogError("AddComponentToEntity: entity or component is null!");
			return;
		}
		_c.m_active = true;
		_c.m_wasActive = true;
		_c.p_entity = _e;
		_e.m_components.Add(_c);
		_e.m_componentsChecksum++;
	}

	// Token: 0x0600232F RID: 9007 RVA: 0x001930AC File Offset: 0x001914AC
	public static void RemoveComponentFromEntity(IComponent _c)
	{
		if (_c.p_entity != null)
		{
			_c.p_entity.m_components.Remove(_c);
			_c.p_entity.m_componentsChecksum++;
		}
		_c.m_active = false;
		_c.m_wasActive = true;
		_c.p_entity = null;
	}

	// Token: 0x06002330 RID: 9008 RVA: 0x00193100 File Offset: 0x00191500
	public static void Update()
	{
		while (EntityManager.m_removeList.Count > 0)
		{
			int num = EntityManager.m_removeList.Count - 1;
			EntityManager.RemoveEntity(EntityManager.m_removeList[num], true, true);
			EntityManager.m_removeList.RemoveAt(num);
		}
		EntityManager.m_entities.Update();
		EntityManager.m_tags.Update();
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x00193160 File Offset: 0x00191560
	public static void UpdateLogic()
	{
		for (int i = 0; i < EntityManager.m_updateList.Count; i++)
		{
			if (EntityManager.m_updateList[i].m_active)
			{
				EntityManager.m_updateList[i].d_entityLogic();
			}
		}
	}

	// Token: 0x040029E4 RID: 10724
	public static DynamicArray<Entity> m_entities;

	// Token: 0x040029E5 RID: 10725
	public static List<Entity> m_updateList;

	// Token: 0x040029E6 RID: 10726
	private static List<Entity> m_removeList;

	// Token: 0x040029E7 RID: 10727
	public static DynamicArray<Tag> m_tags;

	// Token: 0x040029E8 RID: 10728
	private static List<DestroyLink> m_destroyLinks;
}
