using System;

// Token: 0x020004BE RID: 1214
public class GenericArray<T>
{
	// Token: 0x06002299 RID: 8857 RVA: 0x0019050C File Offset: 0x0018E90C
	public GenericArray(int _arrayLength)
	{
		this.m_array = new T[_arrayLength];
		this.m_arrayLength = _arrayLength;
		this.m_lastReserved = 0;
		this.m_aliveIndices = new int[_arrayLength];
		this.m_aliveCount = 0;
		this.m_freeIndices = new int[_arrayLength];
		this.m_freeCount = _arrayLength;
		for (int i = 0; i < _arrayLength; i++)
		{
			this.m_freeIndices[i] = i;
			this.m_aliveIndices[i] = -1;
		}
	}

	// Token: 0x0600229A RID: 8858 RVA: 0x00190584 File Offset: 0x0018E984
	private int AllocateNewIndex()
	{
		this.m_freeCount--;
		int num = this.m_freeIndices[this.m_freeCount];
		this.m_aliveIndices[this.m_aliveCount] = num;
		this.m_aliveCount++;
		return num;
	}

	// Token: 0x0600229B RID: 8859 RVA: 0x001905CC File Offset: 0x0018E9CC
	public int AddItem()
	{
		int num = this.AllocateNewIndex();
		if (num > this.m_lastReserved)
		{
			this.m_lastReserved = num;
		}
		return num;
	}

	// Token: 0x0600229C RID: 8860 RVA: 0x001905F4 File Offset: 0x0018E9F4
	public int AddItem(T item)
	{
		int num = this.AllocateNewIndex();
		this.m_array[num] = item;
		if (num > this.m_lastReserved)
		{
			this.m_lastReserved = num;
		}
		return num;
	}

	// Token: 0x0600229D RID: 8861 RVA: 0x0019062C File Offset: 0x0018EA2C
	public void RemoveItem(int index)
	{
		if (index > -1 && index < this.m_arrayLength)
		{
			bool flag = true;
			for (int i = 0; i < this.m_aliveCount; i++)
			{
				if (flag)
				{
					if (this.m_aliveIndices[i] == index)
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
				this.m_freeIndices[this.m_freeCount] = index;
				this.m_freeCount++;
				this.m_aliveCount--;
				this.m_aliveIndices[this.m_aliveCount] = -1;
			}
		}
	}

	// Token: 0x0600229E RID: 8862 RVA: 0x001906D0 File Offset: 0x0018EAD0
	public void Clear()
	{
		this.m_aliveCount = 0;
		this.m_lastReserved = 0;
		this.m_freeCount = this.m_arrayLength;
		for (int i = 0; i < this.m_arrayLength; i++)
		{
			this.m_freeIndices[i] = i;
			this.m_aliveIndices[i] = -1;
		}
	}

	// Token: 0x0600229F RID: 8863 RVA: 0x00190720 File Offset: 0x0018EB20
	public T[] ToArray()
	{
		T[] array = new T[this.m_aliveCount];
		for (int i = 0; i < this.m_aliveCount; i++)
		{
			array[i] = this.m_array[this.m_aliveIndices[i]];
		}
		return array;
	}

	// Token: 0x040028B3 RID: 10419
	public T[] m_array;

	// Token: 0x040028B4 RID: 10420
	public int m_arrayLength;

	// Token: 0x040028B5 RID: 10421
	public int m_lastReserved;

	// Token: 0x040028B6 RID: 10422
	public int[] m_aliveIndices;

	// Token: 0x040028B7 RID: 10423
	public int m_aliveCount;

	// Token: 0x040028B8 RID: 10424
	public int[] m_freeIndices;

	// Token: 0x040028B9 RID: 10425
	public int m_freeCount;
}
