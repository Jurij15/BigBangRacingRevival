using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class FlyingResource
{
	// Token: 0x06000101 RID: 257 RVA: 0x0000C860 File Offset: 0x0000AC60
	public FlyingResource(ResourceType _type, float _size, RelativeTo _relativeTo, Vector2 _position, Camera _camera = null)
	{
		this.m_entity = EntityManager.AddEntity();
		float num = _size;
		if (_relativeTo == RelativeTo.ScreenHeight)
		{
			num *= (float)Screen.height;
		}
		else if (_relativeTo == RelativeTo.ScreenWidth)
		{
			num *= (float)Screen.width;
		}
		else if (_relativeTo == RelativeTo.ScreenShortest)
		{
			if (Screen.height < Screen.width)
			{
				num *= (float)Screen.height;
			}
			else
			{
				num *= (float)Screen.width;
			}
		}
		else if (_relativeTo == RelativeTo.ScreenLongest)
		{
			if (Screen.height > Screen.width)
			{
				num *= (float)Screen.height;
			}
			else
			{
				num *= (float)Screen.width;
			}
		}
		this.m_TC = TransformS.AddComponent(this.m_entity, "FlyingResource");
		TransformS.SetTransform(this.m_TC, _position, Vector3.zero);
		this.m_prefab = PrefabS.AddComponent(this.m_TC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.ResourcesAnimations_GameObject));
		if (_camera == null)
		{
			PrefabS.SetCamera(this.m_prefab, CameraS.m_uiCamera);
		}
		else
		{
			PrefabS.SetCamera(this.m_prefab, _camera);
		}
		Animator component = this.m_prefab.p_gameObject.GetComponent<Animator>();
		if (_type == ResourceType.Coins)
		{
			component.Play("ResourcesAnimationCoin");
		}
		else if (_type == ResourceType.Diamonds)
		{
			component.Play("ResourcesAnimationGem");
		}
		else if (_type == ResourceType.Shards)
		{
			component.Play("ResourcesAnimationKey");
		}
		this.m_prefab.p_gameObject.transform.localScale = new Vector3(num, num, num);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000C9F2 File Offset: 0x0000ADF2
	public void Destroy()
	{
		EntityManager.RemoveEntity(this.m_entity, true, true);
		this.m_entity = null;
		this.m_TC = null;
		this.m_prefab = null;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000CA16 File Offset: 0x0000AE16
	public void SetCamera(Camera _camera)
	{
		PrefabS.SetCamera(this.m_prefab, _camera);
	}

	// Token: 0x040000C1 RID: 193
	public TransformC m_TC;

	// Token: 0x040000C2 RID: 194
	public Entity m_entity;

	// Token: 0x040000C3 RID: 195
	public PrefabC m_prefab;
}
