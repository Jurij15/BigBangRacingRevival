using System;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class PsUI3DCanvasGachaChest : PsUI3DCanvasGacha
{
	// Token: 0x06001413 RID: 5139 RVA: 0x000CA74C File Offset: 0x000C8B4C
	public PsUI3DCanvasGachaChest(UIComponent _parent, string _tag, Vector3 _offset)
		: base(_parent, _tag, _offset)
	{
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x000CA757 File Offset: 0x000C8B57
	protected override void WinEffect()
	{
		if (this.m_prizeChestEffects != null)
		{
			this.m_prizeChestEffects.SetToActiveState();
		}
		base.WinEffect();
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x000CA77B File Offset: 0x000C8B7B
	public override bool IsSame<T>(T _first, T _second)
	{
		return (GachaType)((object)_first) == (GachaType)((object)_second);
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x000CA795 File Offset: 0x000C8B95
	public override void SetPrizeGameObject(GameObject _go)
	{
		base.SetPrizeGameObject(_go);
		this.m_prizeChestEffects = _go.GetComponent<VisualsRewardChest>();
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x000CA7AC File Offset: 0x000C8BAC
	public override GameObject GetOneGO<T>(T _item)
	{
		GachaType gachaType = (GachaType)((object)_item);
		Texture texture = null;
		ResourcePool.Asset asset = RESOURCE.RewardChest_GameObject;
		switch (gachaType)
		{
		case GachaType.WOOD:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureWood_Texture2D);
			break;
		case GachaType.COMMON:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureWood_Texture2D);
			break;
		case GachaType.BRONZE:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureBronze_Texture2D);
			break;
		case GachaType.SILVER:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureSilver_Texture2D);
			break;
		case GachaType.GOLD:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureGold_Texture2D);
			break;
		case GachaType.RARE:
			asset = RESOURCE.ShopRewardChestT1_GameObject;
			break;
		case GachaType.EPIC:
			asset = RESOURCE.ShopRewardChestT2_GameObject;
			break;
		case GachaType.SUPER:
			asset = RESOURCE.ShopRewardChestT3_GameObject;
			break;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(ResourceManager.GetGameObject(asset));
		if (texture != null)
		{
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].material.name.StartsWith("RewardChest"))
				{
					componentsInChildren[i].material.mainTexture = texture;
				}
			}
		}
		return gameObject;
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x000CA8D4 File Offset: 0x000C8CD4
	public override bool IsBigWin<T>(T _item)
	{
		GachaType gachaType = (GachaType)((object)_item);
		return gachaType != GachaType.BRONZE && gachaType != GachaType.COMMON;
	}

	// Token: 0x040016C8 RID: 5832
	protected VisualsRewardChest m_prizeChestEffects;
}
