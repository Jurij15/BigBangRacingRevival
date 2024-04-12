using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class DecorationalSign : Unit
{
	// Token: 0x06000240 RID: 576 RVA: 0x0001C96C File Offset: 0x0001AD6C
	public DecorationalSign(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_signNode = base.m_graphElement as LevelDecorationNode;
		this.m_signNode.m_isForegroundable = true;
		this.m_tc = TransformS.AddComponent(this.m_entity, "Sign");
		float num = ((!this.m_signNode.m_inFront) ? 150f : (-75f));
		TransformS.SetTransform(this.m_tc, this.m_signNode.m_position + new Vector3(0f, 0f, num) + base.GetZBufferBias(), this.m_signNode.m_rotation);
		if (this.m_minigame.m_editing)
		{
			int num2 = Enum.GetNames(typeof(PsSignIndex)).Length;
			if (this.m_signNode.m_flipped)
			{
				uint num3 = this.m_signNode.m_index + 1U;
				if ((ulong)num3 >= (ulong)((long)num2))
				{
					num3 = 0U;
				}
				this.m_signNode.m_index = num3;
				this.m_signNode.m_flipped = false;
			}
		}
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.DecorationalSignsPrefab_GameObject);
		PsSignIndex index = (PsSignIndex)this.m_signNode.m_index;
		string text = index.ToString();
		if (this.m_signNode.m_index == 6U)
		{
			text = "Arrow";
		}
		GameObject gameObject = this.m_mainPrefab.transform.Find(text).gameObject;
		PrefabS.AddComponent(this.m_tc, Vector3.zero, gameObject);
		if (this.m_signNode.m_index == 6U)
		{
			TransformS.SetScale(this.m_tc, new Vector3(-1f, 1f, 1f));
		}
		this.CreateEditorTouchArea(gameObject, this.m_tc);
	}

	// Token: 0x04000263 RID: 611
	public LevelDecorationNode m_signNode;

	// Token: 0x04000264 RID: 612
	public GameObject m_mainPrefab;

	// Token: 0x04000265 RID: 613
	public TransformC m_tc;
}
