using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class Rock : Unit
{
	// Token: 0x0600019D RID: 413 RVA: 0x00013D94 File Offset: 0x00012194
	public Rock(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_rockGraphNode = _graphElement as LevelRockNode;
		TransformC transformC = TransformS.AddComponent(this.m_entity, "Rock");
		TransformS.SetTransform(transformC, this.m_rockGraphNode.m_position, this.m_rockGraphNode.m_rotation);
		if (this.m_rockGraphNode.m_rockVertices == null || this.m_rockGraphNode.m_flipped)
		{
			this.CreateVertices();
			this.m_rockGraphNode.m_flipped = false;
		}
		else
		{
			Vector2[] rockVertices = this.m_rockGraphNode.m_rockVertices;
			this.m_rockGraphNode.m_totalVertexCount = ChipmunkProWrapper.ucpConvexHull(rockVertices.Length, rockVertices, 0f);
			this.m_rockGraphNode.m_rockVertices = new Vector2[this.m_rockGraphNode.m_totalVertexCount];
			for (int i = 0; i < this.m_rockGraphNode.m_totalVertexCount; i++)
			{
				this.m_rockGraphNode.m_rockVertices[i] = rockVertices[i];
			}
		}
		this.m_centeroid = ChipmunkProWrapper.ucpCenteroidForPoly(this.m_rockGraphNode.m_totalVertexCount, this.m_rockGraphNode.m_rockVertices);
		this.m_area = ChipmunkProWrapper.ucpAreaForPoly(this.m_rockGraphNode.m_totalVertexCount, this.m_rockGraphNode.m_rockVertices, 0f);
		ucpPolyShape ucpPolyShape = new ucpPolyShape(this.m_rockGraphNode.m_rockVertices, -this.m_centeroid, 17895696U, this.m_area * 0.03f, 0.1f, 0.9f, (ucpCollisionType)4, false);
		this.m_cmb = ChipmunkProS.AddDynamicBody(transformC, ucpPolyShape, null);
		this.m_cmb.customComponent = this.m_unitC;
		GGData ggdata = new GGData(this.m_rockGraphNode.m_rockVertices);
		ggdata.ReverseVertexOrder();
		ggdata.SetOffset(-this.m_centeroid + Vector3.forward * (-15f - this.m_rockGraphNode.m_rockRadius * 0.25f));
		ggdata.RadialShift(-0.5f);
		ggdata.SetUvMapPlanar(0.012f, 0.012f, Vector2.zero, null);
		Mesh mesh = GeometryGenerator.GenerateFlatMesh(ggdata);
		GGData ggdata2 = new GGData(this.m_rockGraphNode.m_rockVertices);
		ggdata2.ReverseVertexOrder();
		ggdata2.SetOffset(-this.m_centeroid + Vector3.forward * -10f);
		ggdata2.SetUvMapPlanar(0.012f, 0.012f, Vector2.zero, null);
		ggdata2.SplitVertices(45f, true);
		ggdata.SplitVertices(45f, true);
		Mesh mesh2 = GeometryGenerator.GenerateBeltMesh(0f, ggdata2, ggdata, true);
		GGData ggdata3 = ggdata2.Copy();
		ggdata2.SetUvMapBelt(0.012f, 0f, true);
		ggdata3.SetUvMapBelt(0.012f, 0.5f, true);
		Mesh mesh3 = GeometryGenerator.GenerateBeltMesh(this.m_rockGraphNode.m_rockRadius * 0.8f, ggdata2, ggdata3, true);
		PrefabS.CreatePrefabFromMeshArray(transformC, new Mesh[] { mesh, mesh2, mesh3 }, CameraS.m_mainCamera, ResourceManager.GetMaterial(RESOURCE.RockMaterial_Material), true, true, true);
		this.m_checkForCrushing = true;
		this.CreateEditorTouchArea(100f, 100f, null, default(Vector2));
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000140D8 File Offset: 0x000124D8
	public override void EmergencyKill()
	{
		if (!this.m_isDead)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
			SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
			EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.WoodenCratePuff_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 1f, "GTAG_AUTODESTROY", null);
			this.Destroy();
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00014154 File Offset: 0x00012554
	public void CreateVertices()
	{
		int num = 10;
		this.m_rockGraphNode.m_rockRadius = Random.Range(30f, 60f);
		Vector2[] array = DebugDraw.GetCircle(this.m_rockGraphNode.m_rockRadius, Random.Range(3, num), Vector2.zero, false);
		DebugDraw.AddRadialRandom(array, this.m_rockGraphNode.m_rockRadius * 0.5f);
		GGData ggdata = new GGData(array);
		ggdata.ReverseVertexOrder();
		array = ggdata.ToVector2Array();
		this.m_rockGraphNode.m_totalVertexCount = ChipmunkProWrapper.ucpConvexHull(array.Length, array, 0f);
		this.m_rockGraphNode.m_rockVertices = new Vector2[this.m_rockGraphNode.m_totalVertexCount];
		for (int i = 0; i < this.m_rockGraphNode.m_totalVertexCount; i++)
		{
			this.m_rockGraphNode.m_rockVertices[i] = array[i];
		}
	}

	// Token: 0x04000178 RID: 376
	public ChipmunkBodyC m_cmb;

	// Token: 0x04000179 RID: 377
	private LevelRockNode m_rockGraphNode;

	// Token: 0x0400017A RID: 378
	public Vector3 m_centeroid;

	// Token: 0x0400017B RID: 379
	public float m_area;
}
