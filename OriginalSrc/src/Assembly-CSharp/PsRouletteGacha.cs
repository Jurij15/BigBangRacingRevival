using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class PsRouletteGacha
{
	// Token: 0x0600038A RID: 906 RVA: 0x0003214C File Offset: 0x0003054C
	public static List<T> GetListInRandomOrder<T>(List<T> _data, T _prize, params string[] _parameters)
	{
		if (!_data.Contains(_prize))
		{
			int num = Random.Range(0, _data.Count - 1);
			_data[num] = _prize;
		}
		Random random = new Random();
		int i = _data.Count;
		while (i > 1)
		{
			i--;
			int num2 = random.Next(i + 1);
			T t = _data[num2];
			_data[num2] = _data[i];
			_data[i] = t;
		}
		return _data;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000321C4 File Offset: 0x000305C4
	public static List<T> ConvertDictionaryToList<T>(Dictionary<T, int> _dict)
	{
		List<T> list = new List<T>();
		foreach (KeyValuePair<T, int> keyValuePair in _dict)
		{
			for (int i = 0; i < keyValuePair.Value; i++)
			{
				list.Add(keyValuePair.Key);
			}
		}
		return list;
	}
}
