using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
internal abstract class GoalRepresentation
{
	// Token: 0x060001C5 RID: 453 RVA: 0x000152E7 File Offset: 0x000136E7
	protected GoalRepresentation(Goal _parent, bool _affectedByGravity = false)
	{
		this.m_parent = _parent;
		this.m_affectedByGravity = _affectedByGravity;
		this.m_radius = 45f;
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x060001C6 RID: 454 RVA: 0x00015308 File Offset: 0x00013708
	// (set) Token: 0x060001C7 RID: 455 RVA: 0x00015310 File Offset: 0x00013710
	public bool m_affectedByGravity { get; protected set; }

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x060001C8 RID: 456 RVA: 0x00015319 File Offset: 0x00013719
	// (set) Token: 0x060001C9 RID: 457 RVA: 0x00015321 File Offset: 0x00013721
	public float m_radius { get; protected set; }

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x060001CA RID: 458 RVA: 0x0001532A File Offset: 0x0001372A
	protected TransformC m_tc
	{
		get
		{
			return this.m_parent.m_mainTransform;
		}
	}

	// Token: 0x060001CB RID: 459
	public abstract ResourcePool.Asset GetAsset();

	// Token: 0x060001CC RID: 460
	public abstract void CreateBody(out ucpShape _shape, out ChipmunkBodyC _body);

	// Token: 0x060001CD RID: 461 RVA: 0x00015338 File Offset: 0x00013738
	public virtual PrefabC CreatePrefab()
	{
		GameObject gameObject = ResourceManager.GetGameObject(this.GetAsset());
		this.m_prefabC = PrefabS.AddComponent(this.m_tc, Vector3.zero, gameObject);
		PrefabS.SetCamera(this.m_prefabC, CameraS.m_mainCamera);
		return this.m_prefabC;
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0001537E File Offset: 0x0001377E
	public virtual void Update()
	{
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00015380 File Offset: 0x00013780
	public virtual void OnCollision()
	{
		Transform transform = this.m_prefabC.p_gameObject.transform;
		Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>();
		foreach (Transform transform2 in componentsInChildren)
		{
			if (transform2.name == "ConfettiLocator")
			{
				ResourcePool.Asset confettiBurst_GameObject = RESOURCE.ConfettiBurst_GameObject;
				PrefabC prefabC = this.LaunchParticles(confettiBurst_GameObject, transform2, false);
				prefabC.p_gameObject.transform.Rotate(new Vector3(-90f, 0f, 0f), 1);
			}
		}
		this.m_goalReached = true;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0001541C File Offset: 0x0001381C
	protected virtual PrefabC LaunchParticles(ResourcePool.Asset _asset, Transform locator, bool useMainCameraLayer = true)
	{
		GameObject gameObject = ResourceManager.GetGameObject(_asset);
		PrefabC prefabC = PrefabS.AddComponent(this.m_tc, Vector3.zero, gameObject);
		prefabC.p_gameObject.transform.position = locator.transform.position;
		if (useMainCameraLayer)
		{
			PrefabS.SetCamera(prefabC, CameraS.m_mainCamera);
		}
		return prefabC;
	}

	// Token: 0x0400019A RID: 410
	protected Goal m_parent;

	// Token: 0x0400019B RID: 411
	protected PrefabC m_prefabC;

	// Token: 0x0400019C RID: 412
	public string m_impactSound;

	// Token: 0x0400019D RID: 413
	public int m_maxForce;

	// Token: 0x040001A0 RID: 416
	protected bool m_goalReached;
}
