using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class PsBlock : Unit
{
	// Token: 0x0600045B RID: 1115 RVA: 0x00011264 File Offset: 0x0000F664
	public PsBlock(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.SetPhysicsParameters();
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.BlocksPrefab_GameObject);
		this.m_blockGraphNode = _graphElement as LevelBlockNode;
		this.m_tc = TransformS.AddComponent(this.m_entity, "Block");
		TransformS.SetTransform(this.m_tc, this.m_blockGraphNode.m_position, this.m_blockGraphNode.m_rotation);
		if (this.m_minigame.m_editing)
		{
			int num = Enum.GetNames(typeof(PsBlockShape)).Length;
			if (this.m_blockGraphNode.m_flipped || !this.m_blockGraphNode.m_created)
			{
				if (!this.m_blockGraphNode.m_created)
				{
					this.randomizeRotation = true;
				}
				if (this.m_blockGraphNode.m_flipped)
				{
					this.randomizeRotation = true;
					int num2 = (int)(this.m_blockGraphNode.m_shape + 1);
					if (num2 >= num)
					{
						num2 = 0;
					}
					this.m_blockGraphNode.m_shape = (PsBlockShape)Enum.GetValues(typeof(PsBlockShape)).GetValue(num2);
				}
				this.m_blockGraphNode.m_created = true;
				this.m_blockGraphNode.m_flipped = false;
			}
		}
		this.LoadObject();
		this.m_area = this.m_width * this.m_height;
		this.m_checkForCrushing = true;
		this.CreateEditorTouchArea(100f, 100f, null, default(Vector2));
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x000113D0 File Offset: 0x0000F7D0
	public virtual void SetPhysicsParameters()
	{
		this.m_massMultiplier = 0.015f;
		this.m_friction = 0.9f;
		this.m_elasticity = 0.1f;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x000113F3 File Offset: 0x0000F7F3
	public override void EmergencyKill()
	{
		if (!this.m_isDead)
		{
			this.Kill(DamageType.Impact, float.MaxValue);
		}
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0001140C File Offset: 0x0000F80C
	public virtual void LoadObject()
	{
		int num = 0;
		int num2 = 0;
		switch (this.m_blockGraphNode.m_shape)
		{
		case PsBlockShape.Rect5050:
			this.m_blockGameobject = this.m_mainPrefab.transform.Find("WoodenBox50x50").gameObject;
			this.m_width = 50f;
			this.m_height = 50f;
			if (this.randomizeRotation)
			{
				num = Random.Range(-1, 2);
				num2 = Random.Range(0, 2);
			}
			break;
		case PsBlockShape.Rect10050:
			this.m_blockGameobject = this.m_mainPrefab.transform.Find("WoodenBox100x50").gameObject;
			this.m_width = 100f;
			this.m_height = 50f;
			if (this.randomizeRotation)
			{
				num2 = Random.Range(0, 2);
			}
			break;
		case PsBlockShape.Rect10025:
			this.m_blockGameobject = this.m_mainPrefab.transform.Find("WoodenBox100x25").gameObject;
			this.m_width = 100f;
			this.m_height = 25f;
			if (this.randomizeRotation)
			{
				num2 = Random.Range(0, 2);
			}
			break;
		case PsBlockShape.Rect100100:
			this.m_blockGameobject = this.m_mainPrefab.transform.Find("WoodenBox100x100").gameObject;
			this.m_width = 100f;
			this.m_height = 100f;
			if (this.randomizeRotation)
			{
				Debug.Log("Randomize values", null);
				num = Random.Range(-1, 2);
				num2 = Random.Range(0, 2);
			}
			break;
		case PsBlockShape.HalfRect5050:
			this.m_blockGameobject = this.m_mainPrefab.transform.Find("WoodenBox50x50T").gameObject;
			this.m_width = 50f;
			this.m_height = 25f;
			break;
		}
		if (this.randomizeRotation)
		{
			base.m_graphElement.m_storedRotation = new Vector3(0f, (float)num2 * 180f, (float)num * 90f);
		}
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00011608 File Offset: 0x0000FA08
	public virtual void CreatePhysicsAndModel()
	{
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(this.m_blockGameobject.transform.GetChild(0).gameObject, Vector2.zero, this.m_area * this.m_massMultiplier, this.m_elasticity, this.m_friction, (ucpCollisionType)4, 17895696U, false, false);
		this.m_blockGameobject.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
		this.m_cmb = ChipmunkProS.AddDynamicBody(this.m_tc, ucpPolyShape, null);
		this.m_cmb.customComponent = this.m_unitC;
		this.m_pc = PrefabS.AddComponent(this.m_tc, Vector3.zero, this.m_blockGameobject);
		this.m_pc.p_gameObject.transform.localRotation = Quaternion.identity;
		this.m_pc.p_gameObject.GetComponent<Renderer>().material = ResourceManager.GetMaterial(this.m_materialName + "_Material");
		this.m_pc.p_gameObject.transform.Rotate(base.m_graphElement.m_storedRotation);
	}

	// Token: 0x040005AC RID: 1452
	public TransformC m_tc;

	// Token: 0x040005AD RID: 1453
	public PrefabC m_pc;

	// Token: 0x040005AE RID: 1454
	public ChipmunkBodyC m_cmb;

	// Token: 0x040005AF RID: 1455
	public LevelBlockNode m_blockGraphNode;

	// Token: 0x040005B0 RID: 1456
	public GameObject m_mainPrefab;

	// Token: 0x040005B1 RID: 1457
	public GameObject m_blockGameobject;

	// Token: 0x040005B2 RID: 1458
	public float m_area;

	// Token: 0x040005B3 RID: 1459
	private float m_width;

	// Token: 0x040005B4 RID: 1460
	private float m_height;

	// Token: 0x040005B5 RID: 1461
	protected float m_massMultiplier;

	// Token: 0x040005B6 RID: 1462
	protected float m_friction;

	// Token: 0x040005B7 RID: 1463
	protected float m_elasticity;

	// Token: 0x040005B8 RID: 1464
	protected string m_materialName;

	// Token: 0x040005B9 RID: 1465
	protected bool randomizeRotation;
}
