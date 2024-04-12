using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000556 RID: 1366
public abstract class ResourcePool
{
	// Token: 0x060027E4 RID: 10212 RVA: 0x00077384 File Offset: 0x00075784
	public static ResourcePool.Asset GetAssetInfoByName(string _name)
	{
		if (ResourcePool.m_assets.ContainsKey(_name))
		{
			return ResourcePool.m_assets[_name];
		}
		return null;
	}

	// Token: 0x060027E5 RID: 10213 RVA: 0x000773A4 File Offset: 0x000757A4
	public static List<ResourcePool.Asset> GetResourceGroup(RESOURCE_GROUP _group)
	{
		List<ResourcePool.Asset> list = new List<ResourcePool.Asset>(ResourcePool.m_assets.Values);
		List<ResourcePool.Asset> list2 = new List<ResourcePool.Asset>();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].group == _group)
			{
				list2.Add(list[i]);
			}
		}
		return list2;
	}

	// Token: 0x060027E6 RID: 10214 RVA: 0x00077400 File Offset: 0x00075800
	public static void LoadResourceGroup(RESOURCE_GROUP _group)
	{
		List<ResourcePool.Asset> list = new List<ResourcePool.Asset>(ResourcePool.m_assets.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].group == _group)
			{
				list[i].Load();
			}
		}
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x00077454 File Offset: 0x00075854
	public static void UnloadResourceGroup(RESOURCE_GROUP _group)
	{
		List<ResourcePool.Asset> list = new List<ResourcePool.Asset>(ResourcePool.m_assets.Values);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].group == _group)
			{
				list[i].Unload();
			}
		}
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x04002D69 RID: 11625
	private static Dictionary<string, ResourcePool.Asset> m_assets = new Dictionary<string, ResourcePool.Asset>();

	// Token: 0x02000557 RID: 1367
	public class Asset
	{
		// Token: 0x060027E9 RID: 10217 RVA: 0x000774B8 File Offset: 0x000758B8
		public Asset(RESOURCE_GROUP _group, string _name, string _path, string _GUID, string _suffix)
		{
			this.name = _name;
			this.group = _group;
			this.path = _path;
			this.GUID = _GUID;
			this.suffix = _suffix;
			if (!ResourcePool.m_assets.ContainsKey(this.name))
			{
				ResourcePool.m_assets.Add(this.name, this);
			}
			else if (ResourcePool.m_assets[this.name].GUID == _GUID)
			{
				Debug.LogError("Multiple resource pools contain the same asset! Asset: " + this.name + " Path: " + this.path);
			}
			else
			{
				Debug.LogError("Multiple resource pools contain an asset named \"" + this.name + "\"!", "1. Path: " + this.path + " 2. Path: " + _path);
			}
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x00077591 File Offset: 0x00075991
		public object GetAsset()
		{
			if (this.asset == null)
			{
				this.Load();
			}
			return this.asset;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000775AA File Offset: 0x000759AA
		public void Load()
		{
			if (this.asset == null)
			{
				this.asset = Resources.Load(this.path);
			}
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000775C8 File Offset: 0x000759C8
		public void Unload()
		{
			if (this.asset is Object && !(this.asset is GameObject))
			{
				Resources.UnloadAsset(this.asset as Object);
			}
			this.asset = null;
		}

		// Token: 0x04002D6A RID: 11626
		public readonly RESOURCE_GROUP group;

		// Token: 0x04002D6B RID: 11627
		public readonly string name;

		// Token: 0x04002D6C RID: 11628
		public readonly string path;

		// Token: 0x04002D6D RID: 11629
		public readonly string GUID;

		// Token: 0x04002D6E RID: 11630
		public readonly string suffix;

		// Token: 0x04002D6F RID: 11631
		private object asset;
	}
}
