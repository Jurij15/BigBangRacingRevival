using System;
using System.Collections.Generic;

// Token: 0x020004B3 RID: 1203
public static class CacheManager
{
	// Token: 0x06002256 RID: 8790 RVA: 0x0018EF0B File Offset: 0x0018D30B
	public static void Initialize()
	{
		CacheManager.m_caches = new List<ICache>();
	}

	// Token: 0x06002257 RID: 8791 RVA: 0x0018EF17 File Offset: 0x0018D317
	public static void AddCache(ICache _cache)
	{
		CacheManager.m_caches.Add(_cache);
	}

	// Token: 0x06002258 RID: 8792 RVA: 0x0018EF24 File Offset: 0x0018D324
	public static ICache GetCache(string _name)
	{
		foreach (ICache cache in CacheManager.m_caches)
		{
			if (cache.GetName().Equals(_name))
			{
				return cache;
			}
		}
		return null;
	}

	// Token: 0x06002259 RID: 8793 RVA: 0x0018EF94 File Offset: 0x0018D394
	public static void Update()
	{
		for (int i = 0; i < CacheManager.m_caches.Count; i++)
		{
			ICache cache = CacheManager.m_caches[i];
			cache.Update();
		}
	}

	// Token: 0x0400288A RID: 10378
	private static List<ICache> m_caches;
}
