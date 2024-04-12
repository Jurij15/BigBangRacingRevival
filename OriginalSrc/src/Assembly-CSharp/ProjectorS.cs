using System;
using UnityEngine;

// Token: 0x02000506 RID: 1286
public static class ProjectorS
{
	// Token: 0x0600253D RID: 9533 RVA: 0x0019BF50 File Offset: 0x0019A350
	public static void Initialize()
	{
		ProjectorS.m_components = new DynamicArray<ProjectorC>(100, 0.5f, 0.25f, 0.5f);
	}

	// Token: 0x0600253E RID: 9534 RVA: 0x0019BF70 File Offset: 0x0019A370
	public static void Update()
	{
		int aliveCount = ProjectorS.m_components.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			int num = ProjectorS.m_components.m_aliveIndices[i];
			ProjectorC projectorC = ProjectorS.m_components.m_array[num];
			TransformC p_TC = projectorC.p_TC;
			if (p_TC.updatedPosition)
			{
				projectorC.m_projector.gameObject.transform.position = p_TC.transform.position + projectorC.m_offset;
			}
		}
		ProjectorS.m_components.Update();
	}

	// Token: 0x0600253F RID: 9535 RVA: 0x0019BFFE File Offset: 0x0019A3FE
	public static void SetVisibility(ProjectorC _c, bool _visible, bool _markVisibility = true)
	{
		_c.m_projector.gameObject.SetActive(_visible);
		if (_markVisibility)
		{
			_c.m_wasVisible = _visible;
		}
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x0019C020 File Offset: 0x0019A420
	public static ProjectorC AddComponent(TransformC _parentTC, Material _mat, int _ignoreLayers, Vector3 _offset)
	{
		ProjectorC projectorC = ProjectorS.m_components.AddItem();
		projectorC.p_TC = _parentTC;
		GameObject gameObject = new GameObject(_parentTC.transform.name + " shadow");
		projectorC.m_projector = gameObject.AddComponent<Projector>();
		projectorC.m_projector.material = _mat;
		projectorC.m_projector.ignoreLayers = _ignoreLayers;
		projectorC.m_projector.gameObject.transform.Rotate(new Vector3(90f, 0f, 0f));
		projectorC.m_projector.gameObject.layer = CameraS.m_mainCamera.gameObject.layer;
		projectorC.m_offset = _offset;
		projectorC.m_projector.orthographic = true;
		projectorC.m_projector.orthographicSize = 55f;
		projectorC.m_projector.farClipPlane = 200f;
		projectorC.m_wasVisible = true;
		EntityManager.AddComponentToEntity(_parentTC.p_entity, projectorC);
		return projectorC;
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x0019C10D File Offset: 0x0019A50D
	public static void RemoveComponent(ProjectorC _c)
	{
		Object.DestroyImmediate(_c.m_projector.gameObject);
		_c.m_wasVisible = false;
		_c.m_projector = null;
		EntityManager.RemoveComponentFromEntity(_c);
		ProjectorS.m_components.RemoveItem(_c);
	}

	// Token: 0x04002AF1 RID: 10993
	public static DynamicArray<ProjectorC> m_components;
}
