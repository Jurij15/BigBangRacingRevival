using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class PsPlanetBlockNode : PsPlanetNode
{
	// Token: 0x060008A2 RID: 2210 RVA: 0x0005EC48 File Offset: 0x0005D048
	public PsPlanetBlockNode(PsGameLoop _loop, bool _tutorial = false)
		: base(_loop, _tutorial)
	{
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0005EC54 File Offset: 0x0005D054
	public override void CreatePrefab()
	{
		if (this.m_prefab != null)
		{
			PrefabS.RemoveComponent(this.m_prefab, true);
			this.m_prefab = null;
		}
		if (this.m_loop is PsGameLoopAdventureBattle)
		{
			this.m_blockType = PsPathBlockType.Boss;
		}
		PsGameLoopBlockLevel psGameLoopBlockLevel = null;
		if (this.m_loop is PsGameLoopBlockLevel)
		{
			psGameLoopBlockLevel = this.m_loop as PsGameLoopBlockLevel;
			this.m_blockType = psGameLoopBlockLevel.m_blockType;
		}
		bool flag = false;
		ResourcePool.Asset asset = RESOURCE.ChestTextureBronze_Texture2D;
		GameObject gameObject;
		if (this.m_blockType == PsPathBlockType.Chest)
		{
			if (this.m_loop.m_path != null && this.m_loop.m_path.GetPathType() == PsPlanetPathType.main && psGameLoopBlockLevel != null)
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.CheckpointChestPrefab_GameObject);
				flag = psGameLoopBlockLevel.m_gachaType == GachaType.BRONZE || psGameLoopBlockLevel.m_gachaType == GachaType.GOLD;
				if (psGameLoopBlockLevel.m_gachaType == GachaType.GOLD)
				{
					asset = RESOURCE.ChestTextureGold_Texture2D;
				}
			}
			else
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.PathButtonChestPrefab_GameObject);
			}
		}
		else if (this.m_blockType == PsPathBlockType.Antenna)
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.CheckpointAntennaPrefab_GameObject);
		}
		else if (this.m_blockType == PsPathBlockType.Construction)
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.CheckpointCranePrefab_GameObject);
		}
		else if (this.m_blockType == PsPathBlockType.Boss)
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.CheckpointBossPrefab_GameObject);
		}
		else
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.CheckpointGaragePrefab_GameObject);
		}
		this.m_prefab = PrefabS.AddComponent(this.m_tc, Vector3.zero, gameObject);
		PrefabS.SetCamera(this.m_prefab.p_gameObject, this.m_loop.m_planet.m_planetCamera);
		Renderer[] componentsInChildren = this.m_prefab.p_gameObject.GetComponentsInChildren<Renderer>();
		if (flag)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].material != null && componentsInChildren[i].material.name.StartsWith("ChestTextureSilver"))
				{
					componentsInChildren[i].material.mainTexture = ResourceManager.GetTexture(asset);
				}
			}
		}
		this.m_checkpointEffect = this.m_prefab.p_gameObject.GetComponent<EffectCheckpoint>();
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0005EE70 File Offset: 0x0005D270
	public override void CreateUI()
	{
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0005EE72 File Offset: 0x0005D272
	public override void RemoveUI()
	{
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0005EE74 File Offset: 0x0005D274
	public override void Update()
	{
		base.Update();
		if (this.m_loop.m_nodeId < this.m_loop.m_path.m_currentNodeId)
		{
			return;
		}
		if (this.m_hideUI && this.m_loop.m_planet.m_banners != null)
		{
			for (int i = 0; i < this.m_loop.m_planet.m_banners.Count; i++)
			{
				if (this.m_loop.m_planet.m_banners[i].m_gameLoop == this.m_loop)
				{
					this.m_loop.m_planet.m_banners[i].Hide(null);
				}
			}
			this.m_hideUI = false;
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0005EF38 File Offset: 0x0005D338
	public override void Highlight()
	{
		this.CreateTouchArea();
		if (this.m_highlightTC == null)
		{
			Entity entity = EntityManager.AddEntity();
			this.m_highlightTC = TransformS.AddComponent(entity);
			this.m_highlightTC.transform.localScale = new Vector3(1f, 0f, 1f) * 1.5f;
			TransformS.ParentComponent(this.m_highlightTC, this.m_tc, Vector3.zero);
			this.m_highlightTC.transform.localRotation = Quaternion.identity;
			GameObject gameObject;
			if (this.m_blockType != PsPathBlockType.Boss)
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.PathButtonActiveEffectPrefab_GameObject);
			}
			else
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.PathButtonActiveEffectBossPrefab_GameObject);
			}
			this.m_highlightPrefab = PrefabS.AddComponent(this.m_highlightTC, Vector3.zero, gameObject);
			this.m_highlightPrefab.p_gameObject.transform.localScale = Vector3.one * 1.5f;
			PrefabS.SetCamera(this.m_highlightPrefab.p_gameObject, this.m_loop.m_planet.m_planetCamera);
			this.m_highlightTween = TweenS.AddTransformTween(this.m_highlightTC, TweenedProperty.Scale, TweenStyle.CubicOut, new Vector3(1f, 0f, 1f) * 1.5f, Vector3.one * 1.5f, 1f, 0f, false);
		}
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0005F090 File Offset: 0x0005D490
	public override void RemoveHighlight()
	{
		if (this.m_highlightTC != null)
		{
			this.m_highlightTween = TweenS.AddTransformTween(this.m_highlightTC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.one * 1.5f, Vector3.zero, 0.5f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_highlightTween, new TweenEventDelegate(base.RemoveHighlightDelegate));
		}
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0005F0F4 File Offset: 0x0005D4F4
	public override void Activate2(TimerC _c)
	{
		if (this.m_claimed)
		{
			return;
		}
		base.Activate2(_c);
		if (this.m_blockType == PsPathBlockType.Chest)
		{
			SoundS.PlaySingleShot("/Metagame/ChestBounce", Vector3.zero, 1f);
		}
		SoundS.PlaySingleShot("/Metagame/CheckpointHighlight", Vector3.zero, 1f);
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0005F148 File Offset: 0x0005D548
	public override void PlayDeactivateSound()
	{
		if (this.m_loop is PsGameLoopAdventureBattle)
		{
			SoundS.PlaySingleShot("/Metagame/BossNodeExplode", Vector3.zero, 1f);
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0005F16E File Offset: 0x0005D56E
	public override void SetInactive()
	{
		base.SetInactive();
		if (!this.m_loop.m_keepNodeHidden)
		{
			this.CreateUI();
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0005F18C File Offset: 0x0005D58C
	public override void Reveal()
	{
		base.Reveal();
		SoundS.PlaySingleShot("/Metagame/CheckpointAppear", Vector3.zero, 1f);
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0005F1A8 File Offset: 0x0005D5A8
	public override void Claim()
	{
		this.m_claimed = true;
		base.Claim();
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0005F1B7 File Offset: 0x0005D5B7
	public override void SetClaimed()
	{
		this.m_claimed = true;
		base.SetClaimed();
	}

	// Token: 0x04000829 RID: 2089
	private SpriteC m_bgSpriteC;

	// Token: 0x0400082A RID: 2090
	private PsPathBlockType m_blockType;

	// Token: 0x0400082B RID: 2091
	protected int m_lastEarnedStars;

	// Token: 0x0400082C RID: 2092
	private bool m_claimed;
}
