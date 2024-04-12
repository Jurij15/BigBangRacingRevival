using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class GachaMachine<T>
{
	// Token: 0x0600035B RID: 859 RVA: 0x00031E4F File Offset: 0x0003024F
	public GachaMachine()
	{
		this.m_items = new List<T>();
		this.m_probabilityValues = new List<float>();
		this.m_itemCounts = new List<int>();
		this.m_probabilityValueSum = 0f;
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600035C RID: 860 RVA: 0x00031E83 File Offset: 0x00030283
	public int count
	{
		get
		{
			return this.m_items.Count;
		}
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00031E90 File Offset: 0x00030290
	public T GetItem(bool _generateSeed = true)
	{
		if (_generateSeed)
		{
			Random.seed = (int)(Time.realtimeSinceStartup * 7431f * (Random.value + 1f));
		}
		float num = Random.Range(0f, this.m_probabilityValueSum);
		float num2 = 0f;
		for (int i = 0; i < this.m_items.Count; i++)
		{
			num2 += this.m_probabilityValues[i];
			if (num <= num2)
			{
				T t = this.m_items[i];
				if (this.m_itemCounts[i] > 0)
				{
					List<int> itemCounts;
					int num3;
					(itemCounts = this.m_itemCounts)[num3 = i] = itemCounts[num3] - 1;
					if (this.m_itemCounts[i] == 0)
					{
						this.m_probabilityValueSum -= this.m_probabilityValues[i];
						this.m_items.RemoveAt(i);
						this.m_probabilityValues.RemoveAt(i);
						this.m_itemCounts.RemoveAt(i);
					}
				}
				return t;
			}
		}
		return default(T);
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00031F9F File Offset: 0x0003039F
	public Dictionary<T, int> GetSeveral(int _count, int _maxSame, int _maxDifferend)
	{
		return this.GetSeveral(_count, _maxSame, _maxDifferend, true);
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00031FAB File Offset: 0x000303AB
	public Dictionary<T, int> GetSeveral(int _count, int _maxSame, int _maxDifferend, int _seed)
	{
		Random.seed = _seed;
		return this.GetSeveral(_count, _maxSame, _maxDifferend, false);
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00031FC0 File Offset: 0x000303C0
	private Dictionary<T, int> GetSeveral(int _count, int _maxSame, int _maxDifferend, bool _generateSeed)
	{
		if (_maxSame > 0 && this.m_items.Count * _maxSame <= _count)
		{
			_count = this.m_items.Count * _maxSame;
			Debug.LogError("There is not enough items.");
		}
		else if (_maxSame > 0 && _maxDifferend > 0 && _count > _maxSame * _maxDifferend)
		{
			_count = _maxSame * _maxDifferend;
			Debug.LogError("Count is too small.");
		}
		Dictionary<T, int> dictionary = new Dictionary<T, int>();
		int num = 0;
		while (num < _count && this.m_items.Count > 0)
		{
			T item = this.GetItem(_generateSeed);
			if (dictionary.ContainsKey(item))
			{
				if (_maxSame <= 0 || dictionary[item] < _maxSame)
				{
					Dictionary<T, int> dictionary2;
					T t;
					(dictionary2 = dictionary)[t = item] = dictionary2[t] + 1;
					num++;
				}
			}
			else if (_maxDifferend < 1 || dictionary.Count < _maxDifferend)
			{
				dictionary.Add(item, 1);
				num++;
			}
		}
		return dictionary;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x000320B8 File Offset: 0x000304B8
	public void AddItem(T _item, float _probabilityValue, int _itemCount = -1)
	{
		if (_probabilityValue <= 0f)
		{
			Debug.LogError("Probability value have to be greater then 0.");
			return;
		}
		if (_itemCount == 0)
		{
			Debug.LogWarning("Item count is 0.");
			return;
		}
		this.m_items.Add(_item);
		this.m_probabilityValues.Add(_probabilityValue);
		this.m_probabilityValueSum += _probabilityValue;
		this.m_itemCounts.Add(_itemCount);
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0003211E File Offset: 0x0003051E
	public void Clear()
	{
		this.m_items.Clear();
		this.m_probabilityValues.Clear();
		this.m_probabilityValueSum = 0f;
	}

	// Token: 0x04000453 RID: 1107
	private List<T> m_items;

	// Token: 0x04000454 RID: 1108
	private List<float> m_probabilityValues;

	// Token: 0x04000455 RID: 1109
	private List<int> m_itemCounts;

	// Token: 0x04000456 RID: 1110
	private float m_probabilityValueSum;
}
