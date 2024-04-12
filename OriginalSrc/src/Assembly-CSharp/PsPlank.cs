using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class PsPlank : Unit
{
	// Token: 0x06000192 RID: 402 RVA: 0x00013050 File Offset: 0x00011450
	public PsPlank(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_tc = new List<TransformC>();
		this.m_cmb = new List<ChipmunkBodyC>();
		this.m_density = 0.01f;
		this.m_elasticity = 0.1f;
		this.m_friction = 0.9f;
		this.m_hitPoints = 1f;
		this.m_hitPointType = HitPointType.Lives;
		this.m_canBurn = true;
		this.m_checkForCrushing = true;
		GraphNode graphNode = base.m_graphElement as GraphNode;
		if (graphNode.m_childElements.Count == 0)
		{
			this.m_node1 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "PlankChildNode1", _graphElement.m_position + Vector3.right * 100f + Vector3.forward * -5f, Vector3.zero, Vector3.one);
			this.m_node2 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "PlankChildNode2", _graphElement.m_position + Vector3.right * -100f + Vector3.forward * -5f, Vector3.zero, Vector3.one);
			graphNode.AddElement(this.m_node1);
			graphNode.AddElement(this.m_node2);
		}
		else
		{
			this.m_node1 = graphNode.m_childElements[0] as GraphNode;
			this.m_node2 = graphNode.m_childElements[1] as GraphNode;
		}
		this.m_node1.m_minDistanceFromParent = 75f;
		this.m_node1.m_maxDistanceFromParent = 200f;
		this.m_node2.m_minDistanceFromParent = 75f;
		this.m_node2.m_maxDistanceFromParent = 200f;
		this.CreatePlank();
		this.CreateEditorTouchArea(this.m_length, 10f, null, default(Vector2));
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0001322A File Offset: 0x0001162A
	public override void SetAllBaseArmours()
	{
		base.SetAllBaseArmours();
		this.SetBaseArmor(DamageType.Weapon, 20f);
		this.SetBaseArmor(DamageType.Fire, 20f);
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0001324C File Offset: 0x0001164C
	public void CreatePlankMesh(TransformC _tc, Vector2[] verts)
	{
		if (this.m_pc != null)
		{
			PrefabS.RemoveComponent(this.m_pc, true);
		}
		GGData ggdata = new GGData(verts);
		ggdata.SetOffset(new Vector3(0f, 0f, -25f));
		for (int i = 0; i < ggdata.m_vertices.Count; i++)
		{
			GGVertex ggvertex = ggdata.m_vertices[i];
			ggvertex.uv = new Vector2(ggvertex.vertex.x / 70f, 0.8f + 0.2f * (ggvertex.vertex.y + 5f) / 10f);
		}
		Mesh mesh = GeometryGenerator.GenerateFlatMesh(ggdata);
		GGData ggdata2 = new GGData(verts);
		GGData ggdata3 = new GGData(verts);
		ggdata2.SetOffset(new Vector3(0f, 0f, -25f));
		ggdata3.SetOffset(new Vector3(0f, 0f, -25f));
		for (int j = 0; j < ggdata2.m_vertices.Count; j++)
		{
			GGVertex ggvertex2 = ggdata2.m_vertices[j];
			GGVertex ggvertex3 = ggdata3.m_vertices[j];
			ggvertex2.uv = new Vector2(ggvertex2.vertex.x / 70f, 0.79f);
			ggvertex3.uv = new Vector2(ggvertex3.vertex.x / 70f, 0.19f);
		}
		Mesh mesh2 = GeometryGenerator.GenerateBeltMesh(50f, ggdata2, ggdata3, true);
		this.m_pc = PrefabS.CreatePrefabFromMeshArray(_tc, new Mesh[] { mesh, mesh2 }, CameraS.m_mainCamera, ResourceManager.GetMaterial(RESOURCE.BambooMaterial_Material), true, true, true);
		this.m_pc.p_mesh.MarkDynamic();
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0001341C File Offset: 0x0001181C
	public override void SyncPositionToGraphElementPosition()
	{
		Vector2 vector = this.m_node2.m_position - this.m_node1.m_position;
		if (this.m_length != vector.magnitude)
		{
			this.CreatePlank();
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00013464 File Offset: 0x00011864
	public Vector2[] GetSegmentChainPoints(Vector2 _start, Vector2 _end, float _segmentLength)
	{
		Vector2 vector = _end - _start;
		float magnitude = vector.magnitude;
		int num = Mathf.CeilToInt(magnitude / _segmentLength);
		float num2 = magnitude / (float)num;
		Vector2[] array = new Vector2[num + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = _start + vector.normalized * num2 * (float)i;
		}
		return array;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000134E0 File Offset: 0x000118E0
	public Vector2[] GetSegmentChainVerts(Vector2 _offset, Vector2[] _chainPoints, float _plankHeight)
	{
		int num = _chainPoints.Length * 2;
		Vector2[] array = new Vector2[num];
		float magnitude = (_chainPoints[1] - _chainPoints[0]).magnitude;
		Vector2 vector;
		vector..ctor(-magnitude * 0.5f, 0f);
		for (int i = 0; i < _chainPoints.Length; i++)
		{
			Vector2 vector2 = new Vector2(0f, 1f) * _plankHeight * 0.5f;
			if (i > 0)
			{
				vector += new Vector2(magnitude, 0f);
			}
			array[i] = vector + vector2 + _offset;
			array[num - 1 - i] = vector - vector2 + _offset;
			if (i == 0 || i == _chainPoints.Length - 1)
			{
				array[i] += new Vector2(1f, 0f) * _plankHeight * ((i != 0) ? 0.5f : (-0.5f));
				array[num - 1 - i] += new Vector2(1f, 0f) * _plankHeight * ((i != 0) ? 0.5f : (-0.5f));
			}
		}
		return array;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00013670 File Offset: 0x00011A70
	private void CreatePlank()
	{
		for (int i = 0; i < this.m_tc.Count; i++)
		{
			if (this.m_cmb.Count == this.m_tc.Count)
			{
				ChipmunkProS.RemoveBody(this.m_cmb[i]);
			}
			TransformS.RemoveComponent(this.m_tc[i]);
		}
		this.m_cmb.Clear();
		this.m_tc.Clear();
		Vector2[] segmentChainPoints = this.GetSegmentChainPoints(this.m_node1.m_position, this.m_node2.m_position, 70f);
		this.m_length = (this.m_node2.m_position - this.m_node1.m_position).magnitude;
		float num = this.m_length * 10f * this.m_density;
		int num2 = segmentChainPoints.Length - 1;
		this.m_meshBinder = null;
		if (!this.m_minigame.m_editing)
		{
			this.m_meshBinder = new CmbMeshBinder();
		}
		for (int j = 0; j < segmentChainPoints.Length; j++)
		{
			if (j > 0)
			{
				Vector2 vector = segmentChainPoints[j] - segmentChainPoints[j - 1];
				Vector2 vector2 = segmentChainPoints[j - 1] + vector * 0.5f;
				float num3 = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
				TransformC transformC = TransformS.AddComponent(this.m_entity, base.m_graphElement.m_name + "_" + j);
				TransformS.SetPosition(transformC, vector2);
				TransformS.SetRotation(transformC, Vector3.forward * num3);
				if (this.m_minigame.m_editing)
				{
					TransformS.ParentComponent(transformC, base.m_graphElement.m_TC);
				}
				else
				{
					Vector2 vector3;
					vector3..ctor(-vector.magnitude * 0.5f, 0f);
					Vector2 vector4;
					vector4..ctor(vector.magnitude * 0.5f, 0f);
					ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddDynamicBody(transformC, new ucpSegmentShape(vector3, vector4, 5f, Vector2.zero, 17895696U, num / (float)num2, this.m_elasticity, this.m_friction, (ucpCollisionType)4, false)
					{
						group = base.GetGroup()
					}, this.m_unitC);
					if (j == 1)
					{
						this.m_meshBinder.AddBindPoint(chipmunkBodyC, vector3 + new Vector2(0f, 5f));
						this.m_meshBinder.AddBindPoint(chipmunkBodyC, vector3 + new Vector2(0f, -5f));
					}
					this.m_meshBinder.AddBindPoint(chipmunkBodyC, vector4 + new Vector2(0f, 5f));
					this.m_meshBinder.AddBindPoint(chipmunkBodyC, vector4 + new Vector2(0f, -5f));
					if (j > 1)
					{
						ChipmunkProWrapper.ucpAddConstraint(ChipmunkProWrapper.ucpPivotJointNew(this.m_cmb[this.m_cmb.Count - 1].body, chipmunkBodyC.body, segmentChainPoints[j - 1]), -1);
						TransformC transformC2 = TransformS.AddComponent(this.m_entity);
						ChipmunkProS.AddDampedRotarySpring(chipmunkBodyC, this.m_cmb[this.m_cmb.Count - 1], transformC2, 0f, (float)(8000000 * num2), 150000f);
					}
					this.m_cmb.Add(chipmunkBodyC);
				}
				this.m_tc.Add(transformC);
			}
		}
		Vector2[] segmentChainVerts = this.GetSegmentChainVerts(Vector2.zero, segmentChainPoints, 10f);
		this.CreatePlankMesh(this.m_tc[0], segmentChainVerts);
		if (this.m_meshBinder != null)
		{
			this.m_meshBinder.Bind(this.m_pc);
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00013A68 File Offset: 0x00011E68
	public override void Update()
	{
		base.Update();
		if (this.m_minigame.m_editing)
		{
			Vector2 vector = this.m_node1.m_position - this.m_node2.m_position;
			float magnitude = vector.magnitude;
			if (magnitude != this.m_length)
			{
				base.m_graphElement.m_position = this.m_node2.m_position + vector * 0.5f;
				TransformS.SetGlobalTransform(base.m_graphElement.m_TC, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
			}
		}
		else
		{
			if (this.m_pc.p_gameObject.GetComponent<Renderer>().isVisible)
			{
				this.m_meshBinder.UpdateMesh();
			}
			for (int i = 1; i < this.m_cmb.Count; i++)
			{
				float z = this.m_tc[i - 1].transform.eulerAngles.z;
				float z2 = this.m_tc[i].transform.eulerAngles.z;
				float num = z - z2;
				float num2 = z - 360f - z2;
				float num3 = z - (z2 - 360f);
				if (Mathf.Abs(num) > 25f && Mathf.Abs(num2) > 25f && Mathf.Abs(num3) > 25f)
				{
					this.EmergencyKill();
					break;
				}
			}
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00013BF8 File Offset: 0x00011FF8
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		if (this.m_isDead)
		{
			return;
		}
		base.Kill(_damageType, _totalDamage);
		if (_damageType != DamageType.BlackHole)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb[Mathf.RoundToInt((float)this.m_cmb.Count * 0.5f)].body);
			SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
			EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.WoodenCratePuff_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 1f, "GTAG_AUTODESTROY", null);
			PsAchievementManager.IncrementProgress("breakTwoHundredPlanks", 1);
			PsAchievementManager.IncrementProgress("breakTwoThousandPlanks", 1);
			this.Destroy();
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00013CB8 File Offset: 0x000120B8
	public override void EmergencyKill()
	{
		if (this.m_isDead)
		{
			return;
		}
		Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb[Mathf.RoundToInt((float)this.m_cmb.Count * 0.5f)].body);
		SoundS.PlaySingleShot("/Ingame/Units/WoodenCrateDestroy", vector, 1f);
		EntityManager.AddTimedFXEntity(ResourceManager.GetGameObject(RESOURCE.WoodenCratePuff_GameObject), new Vector3(vector.x, vector.y, 0f), Vector3.zero, 1f, "GTAG_AUTODESTROY", null);
		PsAchievementManager.IncrementProgress("breakTwoHundredPlanks", 1);
		PsAchievementManager.IncrementProgress("breakTwoThousandPlanks", 1);
		this.Destroy();
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00013D67 File Offset: 0x00012167
	public override void SetBurning(bool _burn)
	{
		if (_burn)
		{
			PrefabS.SetShaderColor(this.m_pc, Color.red);
		}
		else
		{
			PrefabS.SetShaderColor(this.m_pc, Color.white);
		}
	}

	// Token: 0x0400016C RID: 364
	private const float PLANK_HEIGHT = 10f;

	// Token: 0x0400016D RID: 365
	private const float PLANK_DEPTH = 50f;

	// Token: 0x0400016E RID: 366
	private List<TransformC> m_tc;

	// Token: 0x0400016F RID: 367
	private List<ChipmunkBodyC> m_cmb;

	// Token: 0x04000170 RID: 368
	private PrefabC m_pc;

	// Token: 0x04000171 RID: 369
	private CmbMeshBinder m_meshBinder;

	// Token: 0x04000172 RID: 370
	private GraphNode m_node1;

	// Token: 0x04000173 RID: 371
	private GraphNode m_node2;

	// Token: 0x04000174 RID: 372
	private float m_length;

	// Token: 0x04000175 RID: 373
	private float m_density;

	// Token: 0x04000176 RID: 374
	private float m_elasticity;

	// Token: 0x04000177 RID: 375
	private float m_friction;
}
