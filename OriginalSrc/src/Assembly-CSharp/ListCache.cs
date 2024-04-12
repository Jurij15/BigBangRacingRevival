using System;
using System.Collections.Generic;

// Token: 0x020004B6 RID: 1206
public class ListCache<T> : ICache
{
	// Token: 0x06002268 RID: 8808 RVA: 0x0018F270 File Offset: 0x0018D670
	public ListCache(string _name, float _cacheExpireTimeMins = -1f)
	{
		CacheManager.AddCache(this);
		this.m_name = _name;
		this.m_expireDurationMins = (double)_cacheExpireTimeMins;
		this.m_expires = this.m_expireDurationMins > 0.0;
		if (this.m_expires)
		{
			this.m_expirationTime = Main.m_gameTimeSinceAppStarted + this.m_expireDurationMins * 60.0;
		}
		this.m_items = new List<CacheItem<T>>();
	}

	// Token: 0x06002269 RID: 8809 RVA: 0x0018F2EB File Offset: 0x0018D6EB
	public string GetName()
	{
		return this.m_name;
	}

	// Token: 0x0600226A RID: 8810 RVA: 0x0018F2F4 File Offset: 0x0018D6F4
	public CacheItem<T> AddItem(string _key, T _content)
	{
		CacheItem<T> cacheItem = new CacheItem<T>(_key, _content, -1f);
		this.AddItem(_key, cacheItem);
		return cacheItem;
	}

	// Token: 0x0600226B RID: 8811 RVA: 0x0018F317 File Offset: 0x0018D717
	public void AddItem(string _key, CacheItem<T> _item)
	{
		this.RemoveItem(_key);
		this.m_items.Add(_item);
		this.Refresh();
	}

	// Token: 0x0600226C RID: 8812 RVA: 0x0018F334 File Offset: 0x0018D734
	public void AddItems(string[] _keys, T[] _items)
	{
		for (int i = 0; i < _items.Length; i++)
		{
			this.AddItem(_keys[i], _items[i]);
		}
	}

	// Token: 0x0600226D RID: 8813 RVA: 0x0018F368 File Offset: 0x0018D768
	public CacheItem<T> GetItem(string _key)
	{
		for (int i = 0; i < this.m_items.Count; i++)
		{
			if (this.m_items[i].m_key == _key)
			{
				return this.m_items[i];
			}
		}
		return null;
	}

	// Token: 0x0600226E RID: 8814 RVA: 0x0018F3BB File Offset: 0x0018D7BB
	public int GetItemCount()
	{
		return this.m_items.Count;
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x0018F3C8 File Offset: 0x0018D7C8
	public T GetContent(int _index)
	{
		CacheItem<T> cacheItem = this.m_items[_index];
		if (cacheItem == null)
		{
			return default(T);
		}
		return cacheItem.m_content;
	}

	// Token: 0x06002270 RID: 8816 RVA: 0x0018F3F8 File Offset: 0x0018D7F8
	public T GetContent(string _key)
	{
		CacheItem<T> item = this.GetItem(_key);
		if (item == null)
		{
			return default(T);
		}
		return item.GetContent();
	}

	// Token: 0x06002271 RID: 8817 RVA: 0x0018F424 File Offset: 0x0018D824
	public T[] GetContents()
	{
		if (this.m_items.Count == 0)
		{
			return null;
		}
		T[] array = new T[this.m_items.Count];
		for (int i = 0; i < this.m_items.Count; i++)
		{
			array[i] = this.m_items[i].m_content;
		}
		return array;
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x0018F489 File Offset: 0x0018D889
	public void RemoveItem(int _index)
	{
		this.RemoveItem(this.m_items[_index]);
	}

	// Token: 0x06002273 RID: 8819 RVA: 0x0018F4A0 File Offset: 0x0018D8A0
	public void RemoveItem(string _key)
	{
		for (int i = 0; i < this.m_items.Count; i++)
		{
			if (this.m_items[i].m_key == _key)
			{
				this.RemoveItem(this.m_items[i]);
				return;
			}
		}
	}

	// Token: 0x06002274 RID: 8820 RVA: 0x0018F4F8 File Offset: 0x0018D8F8
	public void RemoveItem(CacheItem<T> _item)
	{
		this.m_items.Remove(_item);
	}

	// Token: 0x06002275 RID: 8821 RVA: 0x0018F507 File Offset: 0x0018D907
	public void Clear()
	{
		this.m_items.Clear();
	}

	// Token: 0x06002276 RID: 8822 RVA: 0x0018F514 File Offset: 0x0018D914
	public void Refresh()
	{
		if (this.m_expires)
		{
			this.m_expirationTime = Main.m_gameTimeSinceAppStarted + this.m_expireDurationMins * 60.0;
		}
	}

	// Token: 0x06002277 RID: 8823 RVA: 0x0018F53D File Offset: 0x0018D93D
	public void Update()
	{
		if (this.m_expires && Main.m_gameTimeSinceAppStarted > this.m_expirationTime)
		{
			this.Clear();
			this.Refresh();
		}
	}

	// Token: 0x04002891 RID: 10385
	public string m_name;

	// Token: 0x04002892 RID: 10386
	public bool m_expires;

	// Token: 0x04002893 RID: 10387
	public double m_expireDurationMins;

	// Token: 0x04002894 RID: 10388
	public double m_expirationTime;

	// Token: 0x04002895 RID: 10389
	private List<CacheItem<T>> m_items;
}
