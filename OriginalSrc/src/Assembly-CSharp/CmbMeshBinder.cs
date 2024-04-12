using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004B7 RID: 1207
public class CmbMeshBinder
{
	// Token: 0x06002278 RID: 8824 RVA: 0x0018F566 File Offset: 0x0018D966
	public CmbMeshBinder()
	{
		this.m_bindPoints = new List<CmbMeshBinder.CmbMeshBindPoint>();
	}

	// Token: 0x06002279 RID: 8825 RVA: 0x0018F57C File Offset: 0x0018D97C
	public void AddBindPoint(ChipmunkBodyC _cmb, Vector2 _localPoint)
	{
		CmbMeshBinder.CmbMeshBindPoint cmbMeshBindPoint = new CmbMeshBinder.CmbMeshBindPoint();
		cmbMeshBindPoint.m_cmb = _cmb;
		cmbMeshBindPoint.m_localPoint = _localPoint;
		cmbMeshBindPoint.m_originalWorldPoint = ChipmunkProWrapper.ucpBodyLocal2World(cmbMeshBindPoint.m_cmb.body, cmbMeshBindPoint.m_localPoint);
		this.m_bindPoints.Add(cmbMeshBindPoint);
	}

	// Token: 0x0600227A RID: 8826 RVA: 0x0018F5C8 File Offset: 0x0018D9C8
	public void Bind(PrefabC _prefab)
	{
		this.m_prefab = _prefab;
		this.m_originalVertPositions = this.m_prefab.p_mesh.vertices;
		for (int i = 0; i < this.m_bindPoints.Count; i++)
		{
			CmbMeshBinder.CmbMeshBindPoint cmbMeshBindPoint = this.m_bindPoints[i];
			cmbMeshBindPoint.m_pointInPrefabSpace = this.m_prefab.p_parentTC.transform.InverseTransformPoint(cmbMeshBindPoint.m_originalWorldPoint);
		}
		for (int j = 0; j < this.m_originalVertPositions.Length; j++)
		{
			Vector3 vector = this.m_prefab.p_gameObject.transform.TransformPoint(this.m_originalVertPositions[j]);
			CmbMeshBinder.CmbMeshBindPoint cmbMeshBindPoint2 = this.FindBindPointForVertex(vector);
			cmbMeshBindPoint2.m_vertexIndices.Add(j);
		}
	}

	// Token: 0x0600227B RID: 8827 RVA: 0x0018F69C File Offset: 0x0018DA9C
	public void UpdateMesh()
	{
		Vector3[] array = new Vector3[this.m_originalVertPositions.Length];
		for (int i = 0; i < this.m_bindPoints.Count; i++)
		{
			CmbMeshBinder.CmbMeshBindPoint cmbMeshBindPoint = this.m_bindPoints[i];
			Vector2 pointInPrefabSpace = cmbMeshBindPoint.m_pointInPrefabSpace;
			Vector2 vector = this.m_prefab.p_parentTC.transform.InverseTransformPoint(ChipmunkProWrapper.ucpBodyLocal2World(cmbMeshBindPoint.m_cmb.body, cmbMeshBindPoint.m_localPoint));
			Vector3 vector2 = pointInPrefabSpace - vector;
			for (int j = 0; j < cmbMeshBindPoint.m_vertexIndices.Count; j++)
			{
				int num = cmbMeshBindPoint.m_vertexIndices[j];
				array[num] = this.m_originalVertPositions[num] - vector2;
			}
		}
		this.m_prefab.p_mesh.vertices = array;
		this.m_prefab.p_mesh.RecalculateBounds();
		this.m_prefab.p_mesh.RecalculateNormals();
	}

	// Token: 0x0600227C RID: 8828 RVA: 0x0018F7B4 File Offset: 0x0018DBB4
	private CmbMeshBinder.CmbMeshBindPoint FindBindPointForVertex(Vector3 _vertexWorldCoord)
	{
		float num = float.MaxValue;
		int num2 = -1;
		for (int i = 0; i < this.m_bindPoints.Count; i++)
		{
			float sqrMagnitude = (_vertexWorldCoord - this.m_bindPoints[i].m_originalWorldPoint).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				num2 = i;
			}
		}
		if (num2 >= 0)
		{
			return this.m_bindPoints[num2];
		}
		return null;
	}

	// Token: 0x04002896 RID: 10390
	public List<CmbMeshBinder.CmbMeshBindPoint> m_bindPoints;

	// Token: 0x04002897 RID: 10391
	public PrefabC m_prefab;

	// Token: 0x04002898 RID: 10392
	private Vector3[] m_originalVertPositions;

	// Token: 0x020004B8 RID: 1208
	public class CmbMeshBindPoint
	{
		// Token: 0x0600227D RID: 8829 RVA: 0x0018F82B File Offset: 0x0018DC2B
		public CmbMeshBindPoint()
		{
			this.m_vertexIndices = new List<int>();
		}

		// Token: 0x04002899 RID: 10393
		public ChipmunkBodyC m_cmb;

		// Token: 0x0400289A RID: 10394
		public Vector2 m_localPoint;

		// Token: 0x0400289B RID: 10395
		public Vector2 m_originalWorldPoint;

		// Token: 0x0400289C RID: 10396
		public Vector2 m_pointInPrefabSpace;

		// Token: 0x0400289D RID: 10397
		public List<int> m_vertexIndices;
	}
}
