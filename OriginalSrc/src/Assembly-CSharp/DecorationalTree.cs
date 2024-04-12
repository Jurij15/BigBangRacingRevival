using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class DecorationalTree : Unit
{
	// Token: 0x06000241 RID: 577 RVA: 0x0001CB24 File Offset: 0x0001AF24
	public DecorationalTree(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_treeNode = base.m_graphElement as LevelDecorationNode;
		this.m_treeNode.m_isForegroundable = true;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.DecorationalTreesPrefab_GameObject);
		this.m_tc = TransformS.AddComponent(this.m_entity, "Tree");
		float num = ((!this.m_treeNode.m_inFront) ? 150f : (-75f));
		TransformS.SetTransform(this.m_tc, _graphElement.m_position + new Vector3(0f, 0f, num) + base.GetZBufferBias(), _graphElement.m_rotation);
		if (this.m_minigame.m_editing)
		{
			int num2 = Enum.GetNames(typeof(PsTreeIndex)).Length;
			if (this.m_treeNode.m_flipped)
			{
				uint num3 = this.m_treeNode.m_index + 1U;
				if ((ulong)num3 >= (ulong)((long)num2))
				{
					num3 = 0U;
				}
				this.m_treeNode.m_index = num3;
				this.m_treeNode.m_flipped = false;
				float num4 = 14f;
				base.m_graphElement.m_storedRotation += new Vector3(0f, num4, 0f);
			}
		}
		Transform transform = gameObject.transform;
		PsTreeIndex index = (PsTreeIndex)this.m_treeNode.m_index;
		GameObject gameObject2 = transform.Find(index.ToString()).gameObject;
		this.m_treePC = PrefabS.AddComponent(this.m_tc, Vector3.zero, gameObject2);
		this.m_touchEntity = EntityManager.AddEntity();
		TransformC transformC = TransformS.AddComponent(this.m_touchEntity, "TouchCollider");
		TransformS.ParentComponent(transformC, this.m_tc, Vector3.zero);
		if (base.m_graphElement.m_storedRotation == Vector3.zero)
		{
			float num5 = Random.Range(0f, 360f);
			int num6 = Random.Range(0, 2);
			base.m_graphElement.m_storedRotation = new Vector3((float)num6, num5, 0f);
		}
		if ((int)base.m_graphElement.m_storedRotation[0] < 1)
		{
			TransformS.SetScale(this.m_tc, new Vector3(-1f, 1f, 1f));
		}
		this.m_treePC.p_gameObject.transform.localRotation *= Quaternion.Euler(new Vector3(0f, base.m_graphElement.m_storedRotation[1], 0f));
		transformC.transform.localRotation = this.m_treePC.p_gameObject.transform.localRotation;
		this.CreateEditorTouchArea(gameObject2, transformC);
		base.m_graphElement.m_isRotateable = true;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0001CDD3 File Offset: 0x0001B1D3
	public override void Destroy()
	{
		EntityManager.RemoveEntity(this.m_touchEntity);
		this.m_touchEntity = null;
		base.Destroy();
	}

	// Token: 0x0400026A RID: 618
	public LevelDecorationNode m_treeNode;

	// Token: 0x0400026B RID: 619
	public Entity m_touchEntity;

	// Token: 0x0400026C RID: 620
	protected TransformC m_tc;

	// Token: 0x0400026D RID: 621
	protected PrefabC m_treePC;
}
