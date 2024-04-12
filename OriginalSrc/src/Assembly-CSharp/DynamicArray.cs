using System;
using UnityEngine;

// Token: 0x020004BB RID: 1211
public class DynamicArray<T> where T : IPoolable, new()
{
	// Token: 0x0600228C RID: 8844 RVA: 0x00190044 File Offset: 0x0018E444
	public DynamicArray(int _resizeMinIncrementAndSize = 100, float _resizeIncreaseAmount = 0.5f, float _resizeDecreaseLimit = 0.25f, float _resizeDecreaseAmount = 0.5f)
	{
		this.m_resizeMinIncrementAndSize = _resizeMinIncrementAndSize;
		this.m_currentLength = this.m_resizeMinIncrementAndSize;
		this.m_freeCount = this.m_resizeMinIncrementAndSize;
		this.m_array = new T[this.m_resizeMinIncrementAndSize];
		this.m_resizeIncreaseAmount = _resizeIncreaseAmount;
		this.m_resizeDecreaseLimit = _resizeDecreaseLimit;
		this.m_resizeDecreaseAmount = _resizeDecreaseAmount;
		this.m_aliveIndices = new int[this.m_resizeMinIncrementAndSize];
		this.m_aliveCount = 0;
		this.m_freeIndices = new int[this.m_resizeMinIncrementAndSize];
		for (int i = 0; i < this.m_resizeMinIncrementAndSize; i++)
		{
			this.m_freeIndices[i] = i;
			this.m_aliveIndices[i] = -1;
			T t = new T();
			t.m_index = i;
			this.m_array[i] = t;
		}
	}

	// Token: 0x0600228D RID: 8845 RVA: 0x00190110 File Offset: 0x0018E510
	public void Destroy()
	{
		int aliveCount = this.m_aliveCount;
		for (int i = aliveCount - 1; i >= 0; i--)
		{
			this.RemoveItem(this.m_aliveIndices[i]);
		}
	}

	// Token: 0x0600228E RID: 8846 RVA: 0x00190148 File Offset: 0x0018E548
	private T AllocateNew()
	{
		if (this.m_freeCount > 0)
		{
			this.m_freeCount--;
			int num = this.m_freeIndices[this.m_freeCount];
			this.m_aliveIndices[this.m_aliveCount] = num;
			this.m_aliveCount++;
			return this.m_array[num];
		}
		this.IncreaseLength(this.m_currentLength + Mathf.Max(this.m_resizeMinIncrementAndSize, Mathf.RoundToInt((float)this.m_currentLength * this.m_resizeIncreaseAmount)));
		return this.AllocateNew();
	}

	// Token: 0x0600228F RID: 8847 RVA: 0x001901D8 File Offset: 0x0018E5D8
	public T AddItem()
	{
		T t = this.AllocateNew();
		t.Reset();
		return t;
	}

	// Token: 0x06002290 RID: 8848 RVA: 0x001901FA File Offset: 0x0018E5FA
	public void RemoveItem(T _item)
	{
		_item.Destroy();
		this.RemoveItem(_item.m_index);
	}

	// Token: 0x06002291 RID: 8849 RVA: 0x0019021C File Offset: 0x0018E61C
	public void RemoveItem(int _itemIndex)
	{
		if (_itemIndex > -1 && _itemIndex < this.m_currentLength)
		{
			bool flag = true;
			for (int i = 0; i < this.m_aliveCount; i++)
			{
				if (flag)
				{
					if (this.m_aliveIndices[i] == _itemIndex)
					{
						flag = false;
					}
				}
				else
				{
					this.m_aliveIndices[i - 1] = this.m_aliveIndices[i];
				}
			}
			if (!flag)
			{
				this.m_freeIndices[this.m_freeCount] = _itemIndex;
				this.m_freeCount++;
				this.m_aliveCount--;
				this.m_aliveIndices[this.m_aliveCount] = -1;
			}
			else
			{
				Debug.LogError("Trying to remove nonexisting item");
			}
		}
		else
		{
			Debug.LogError(string.Concat(new object[] { "Array index out of bounds: ", _itemIndex, " / ", this.m_currentLength }));
		}
	}

	// Token: 0x06002292 RID: 8850 RVA: 0x0019030B File Offset: 0x0018E70B
	public void AddSafetyThreshold(int _amount)
	{
		if (this.m_currentLength < this.m_aliveCount + _amount)
		{
			this.IncreaseLength(this.m_aliveCount + _amount);
		}
	}

	// Token: 0x06002293 RID: 8851 RVA: 0x00190330 File Offset: 0x0018E730
	private void IncreaseLength(int _newLength)
	{
		int currentLength = this.m_currentLength;
		int num = _newLength - currentLength;
		this.m_currentLength = _newLength;
		Array.Resize<T>(ref this.m_array, _newLength);
		Array.Resize<int>(ref this.m_aliveIndices, _newLength);
		int[] array = new int[_newLength];
		for (int i = 0; i < currentLength; i++)
		{
			array[num + i] = this.m_freeIndices[i];
		}
		for (int j = 0; j < num; j++)
		{
			array[j] = currentLength + j;
		}
		this.m_freeCount += num;
		this.m_freeIndices = array;
		for (int k = currentLength; k < _newLength; k++)
		{
			this.m_freeIndices[k] = k;
			this.m_aliveIndices[k] = -1;
			T t = new T();
			t.m_index = k;
			this.m_array[k] = t;
		}
	}

	// Token: 0x06002294 RID: 8852 RVA: 0x00190410 File Offset: 0x0018E810
	private void DecreaseLength(int _newLength)
	{
	}

	// Token: 0x06002295 RID: 8853 RVA: 0x00190412 File Offset: 0x0018E812
	public void Update()
	{
	}

	// Token: 0x06002296 RID: 8854 RVA: 0x00190414 File Offset: 0x0018E814
	public T[] ToArray()
	{
		T[] array = new T[this.m_aliveCount];
		for (int i = 0; i < this.m_aliveCount; i++)
		{
			array[i] = this.m_array[this.m_aliveIndices[i]];
		}
		return array;
	}

	// Token: 0x040028A6 RID: 10406
	public T[] m_array;

	// Token: 0x040028A7 RID: 10407
	public int m_currentLength;

	// Token: 0x040028A8 RID: 10408
	public float m_resizeIncreaseAmount;

	// Token: 0x040028A9 RID: 10409
	public float m_resizeDecreaseLimit;

	// Token: 0x040028AA RID: 10410
	public float m_resizeDecreaseAmount;

	// Token: 0x040028AB RID: 10411
	public int m_resizeMinIncrementAndSize;

	// Token: 0x040028AC RID: 10412
	public int[] m_aliveIndices;

	// Token: 0x040028AD RID: 10413
	public int m_aliveCount;

	// Token: 0x040028AE RID: 10414
	public int[] m_freeIndices;

	// Token: 0x040028AF RID: 10415
	public int m_freeCount;
}
