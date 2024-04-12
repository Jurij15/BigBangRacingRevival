using System;
using UnityEngine;

// Token: 0x02000554 RID: 1364
public static class ResourceManager
{
	// Token: 0x060027CF RID: 10191 RVA: 0x001AA9A8 File Offset: 0x001A8DA8
	public static AudioClip GetAudioClip(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as AudioClip;
	}

	// Token: 0x060027D0 RID: 10192 RVA: 0x001AA9B5 File Offset: 0x001A8DB5
	public static Texture GetTexture(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as Texture;
	}

	// Token: 0x060027D1 RID: 10193 RVA: 0x001AA9C2 File Offset: 0x001A8DC2
	public static Shader GetShader(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as Shader;
	}

	// Token: 0x060027D2 RID: 10194 RVA: 0x001AA9CF File Offset: 0x001A8DCF
	public static Material GetMaterial(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as Material;
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x001AA9DC File Offset: 0x001A8DDC
	public static GameObject GetGameObject(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as GameObject;
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x001AA9E9 File Offset: 0x001A8DE9
	public static Level GetLevel(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as Level;
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x001AA9F6 File Offset: 0x001A8DF6
	public static TextAsset GetTextAsset(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as TextAsset;
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x001AAA03 File Offset: 0x001A8E03
	public static Font GetFont(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as Font;
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x001AAA10 File Offset: 0x001A8E10
	public static AnimationClip GetAnimationClip(ResourcePool.Asset _assetInfo)
	{
		return _assetInfo.GetAsset() as AnimationClip;
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x001AAA1D File Offset: 0x001A8E1D
	public static T GetResource<T>(ResourcePool.Asset _assetInfo) where T : class
	{
		return _assetInfo.GetAsset() as T;
	}

	// Token: 0x060027D9 RID: 10201 RVA: 0x001AAA30 File Offset: 0x001A8E30
	public static AudioClip GetAudioClip(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as AudioClip;
		}
		return null;
	}

	// Token: 0x060027DA RID: 10202 RVA: 0x001AAA58 File Offset: 0x001A8E58
	public static Texture GetTexture(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as Texture;
		}
		return null;
	}

	// Token: 0x060027DB RID: 10203 RVA: 0x001AAA80 File Offset: 0x001A8E80
	public static Shader GetShader(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as Shader;
		}
		return null;
	}

	// Token: 0x060027DC RID: 10204 RVA: 0x001AAAA8 File Offset: 0x001A8EA8
	public static Material GetMaterial(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as Material;
		}
		return null;
	}

	// Token: 0x060027DD RID: 10205 RVA: 0x001AAAD0 File Offset: 0x001A8ED0
	public static GameObject GetGameObject(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as GameObject;
		}
		return null;
	}

	// Token: 0x060027DE RID: 10206 RVA: 0x001AAAF8 File Offset: 0x001A8EF8
	public static Level GetLevel(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as Level;
		}
		return null;
	}

	// Token: 0x060027DF RID: 10207 RVA: 0x001AAB20 File Offset: 0x001A8F20
	public static TextAsset GetTextAsset(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as TextAsset;
		}
		return null;
	}

	// Token: 0x060027E0 RID: 10208 RVA: 0x001AAB48 File Offset: 0x001A8F48
	public static Font GetFont(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as Font;
		}
		return null;
	}

	// Token: 0x060027E1 RID: 10209 RVA: 0x001AAB70 File Offset: 0x001A8F70
	public static AnimationClip GetAnimationClip(string _resourceName)
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as AnimationClip;
		}
		return null;
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x001AAB98 File Offset: 0x001A8F98
	public static T GetResource<T>(string _resourceName) where T : class
	{
		ResourcePool.Asset assetInfoByName = ResourcePool.GetAssetInfoByName(_resourceName);
		if (assetInfoByName != null)
		{
			return assetInfoByName.GetAsset() as T;
		}
		return (T)((object)null);
	}
}
