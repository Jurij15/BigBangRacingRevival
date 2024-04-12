using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class PsWhiteHole : PsBlackHole
{
	// Token: 0x06000272 RID: 626 RVA: 0x0001FA66 File Offset: 0x0001DE66
	public PsWhiteHole(GraphElement _graphElement)
		: base(_graphElement)
	{
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0001FA70 File Offset: 0x0001DE70
	public override void SetGraphics()
	{
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.WhiteholePrefab_GameObject);
		PrefabC prefabC = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 18.5f, 100f), this.m_mainPrefab);
		prefabC.p_gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
		Transform transform = prefabC.p_gameObject.transform.Find("WhiteholeOuterEdge");
		this.m_effect = prefabC.p_gameObject.GetComponent<EffectWhitehole>();
		if (transform != null)
		{
			transform.localScale *= this.m_pullRadiusMultiplier;
		}
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0001FB21 File Offset: 0x0001DF21
	protected override void AddSound()
	{
		this.m_blackHoleLoopSound = SoundS.AddCombineSoundComponent(this.m_tc, "WhiteHoleLoopSound", "/InGame/Units/WhiteHoleLoop", 2.4f);
	}
}
