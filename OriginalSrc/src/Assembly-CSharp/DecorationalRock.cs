using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class DecorationalRock : Unit
{
	// Token: 0x0600023E RID: 574 RVA: 0x0001C6C4 File Offset: 0x0001AAC4
	public DecorationalRock(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_rockNode = base.m_graphElement as LevelDecorationNode;
		this.m_rockNode.m_isForegroundable = true;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.DecorationRocksPrefab_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "Rock");
		float num = ((!this.m_rockNode.m_inFront) ? 150f : (-75f));
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, 0f, num) + base.GetZBufferBias(), _graphElement.m_rotation);
		if (this.m_minigame.m_editing)
		{
			int num2 = Enum.GetNames(typeof(PsTreeIndex)).Length;
			if (this.m_rockNode.m_flipped)
			{
				uint num3 = this.m_rockNode.m_index + 1U;
				if ((ulong)num3 >= (ulong)((long)num2))
				{
					num3 = 0U;
				}
				this.m_rockNode.m_index = num3;
				this.m_rockNode.m_flipped = false;
				float num4 = 14f;
				base.m_graphElement.m_storedRotation += new Vector3(0f, num4, 0f);
			}
		}
		Transform transform = gameObject.transform;
		PsRockIndex index = (PsRockIndex)this.m_rockNode.m_index;
		GameObject gameObject2 = transform.Find(index.ToString()).gameObject;
		PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, gameObject2);
		this.m_touchEntity = EntityManager.AddEntity();
		TransformC transformC2 = TransformS.AddComponent(this.m_touchEntity, "TouchCollider");
		TransformS.ParentComponent(transformC2, transformC, Vector3.zero);
		if (base.m_graphElement.m_storedRotation == Vector3.zero)
		{
			float num5 = Random.Range(0f, 360f);
			int num6 = Random.Range(0, 2);
			base.m_graphElement.m_storedRotation = new Vector3((float)num6, num5, 0f);
		}
		if ((int)base.m_graphElement.m_storedRotation[0] < 1)
		{
			TransformS.SetScale(transformC, new Vector3(-1f, 1f, 1f));
		}
		prefabC.p_gameObject.transform.localRotation *= Quaternion.Euler(new Vector3(0f, base.m_graphElement.m_storedRotation[1], 0f));
		transformC2.transform.localRotation = prefabC.p_gameObject.transform.localRotation;
		this.CreateEditorTouchArea(gameObject2, transformC2);
		base.m_graphElement.m_isRotateable = true;
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0001C952 File Offset: 0x0001AD52
	public override void Destroy()
	{
		EntityManager.RemoveEntity(this.m_touchEntity);
		this.m_touchEntity = null;
		base.Destroy();
	}

	// Token: 0x04000259 RID: 601
	public LevelDecorationNode m_rockNode;

	// Token: 0x0400025A RID: 602
	public Entity m_touchEntity;
}
