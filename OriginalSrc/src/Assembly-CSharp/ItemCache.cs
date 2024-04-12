using System;
using System.Collections.Generic;

// Token: 0x020004B5 RID: 1205
public class ItemCache<T> : ICache
{
	// Token: 0x0600225C RID: 8796 RVA: 0x0018EFD0 File Offset: 0x0018D3D0
	public ItemCache(string _name, int _maxItems = 2147483647, float _cacheExpireTimeMins = -1f)
	{
		CacheManager.AddCache(this);
		this.m_name = _name;
		this.m_maxItems = _maxItems;
		this.m_expireDurationMins = (double)_cacheExpireTimeMins;
		this.m_expires = this.m_expireDurationMins > 0.0;
		if (this.m_expires)
		{
			this.m_expirationTime = Main.m_gameTimeSinceAppStarted + this.m_expireDurationMins * 60.0;
		}
		this.m_items = new LinkedList<CacheItem<T>>();
	}

	// Token: 0x0600225D RID: 8797 RVA: 0x0018F052 File Offset: 0x0018D452
	public string GetName()
	{
		return this.m_name;
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x0018F05C File Offset: 0x0018D45C
	public CacheItem<T> AddItem(string _key, T _content)
	{
		CacheItem<T> cacheItem = new CacheItem<T>(_key, _content, -1f);
		this.AddItem(_key, cacheItem);
		return cacheItem;
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x0018F07F File Offset: 0x0018D47F
	public void AddItem(string _key, CacheItem<T> _item)
	{
		this.RemoveItem(_key);
		if (this.m_items.Count >= this.m_maxItems)
		{
			this.m_items.RemoveFirst();
		}
		this.m_items.AddLast(_item);
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x0018F0B8 File Offset: 0x0018D4B8
	public CacheItem<T> GetItem(string _key)
	{
		CacheItem<T> cacheItem = null;
		foreach (CacheItem<T> cacheItem2 in this.m_items)
		{
			if (cacheItem2.m_key.Equals(_key))
			{
				cacheItem = cacheItem2;
				break;
			}
		}
		if (cacheItem != null)
		{
			this.m_items.Remove(cacheItem);
			this.m_items.AddLast(cacheItem);
			cacheItem.Refresh();
		}
		return cacheItem;
	}

	// Token: 0x06002261 RID: 8801 RVA: 0x0018F150 File Offset: 0x0018D550
	public int GetItemCount()
	{
		return this.m_items.Count;
	}

	// Token: 0x06002262 RID: 8802 RVA: 0x0018F160 File Offset: 0x0018D560
	public T GetContent(string _key)
	{
		CacheItem<T> item = this.GetItem(_key);
		if (item == null)
		{
			return default(T);
		}
		return item.GetContent();
	}

	// Token: 0x06002263 RID: 8803 RVA: 0x0018F18C File Offset: 0x0018D58C
	public void RemoveItem(string _key)
	{
		foreach (CacheItem<T> cacheItem in this.m_items)
		{
			if (cacheItem.m_key.Equals(_key))
			{
				this.RemoveItem(cacheItem);
				break;
			}
		}
	}

	// Token: 0x06002264 RID: 8804 RVA: 0x0018F200 File Offset: 0x0018D600
	public void RemoveItem(CacheItem<T> _item)
	{
		this.m_items.Remove(_item);
	}

	// Token: 0x06002265 RID: 8805 RVA: 0x0018F20F File Offset: 0x0018D60F
	public void Clear()
	{
		this.m_items.Clear();
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x0018F21C File Offset: 0x0018D61C
	public void Refresh()
	{
		if (this.m_expires)
		{
			this.m_expirationTime = Main.m_gameTimeSinceAppStarted + this.m_expireDurationMins * 60.0;
		}
	}

	// Token: 0x06002267 RID: 8807 RVA: 0x0018F245 File Offset: 0x0018D645
	public void Update()
	{
		if (this.m_expires && Main.m_gameTimeSinceAppStarted > this.m_expirationTime)
		{
			this.Clear();
			this.Refresh();
		}
	}

	// Token: 0x0400288B RID: 10379
	public string m_name;

	// Token: 0x0400288C RID: 10380
	public int m_maxItems;

	// Token: 0x0400288D RID: 10381
	public bool m_expires;

	// Token: 0x0400288E RID: 10382
	public double m_expireDurationMins;

	// Token: 0x0400288F RID: 10383
	public double m_expirationTime;

	// Token: 0x04002890 RID: 10384
	public LinkedList<CacheItem<T>> m_items;
}
