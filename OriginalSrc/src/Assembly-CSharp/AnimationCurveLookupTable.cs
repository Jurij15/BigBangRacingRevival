using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class AnimationCurveLookupTable
{
	// Token: 0x060003EE RID: 1006 RVA: 0x00037E48 File Offset: 0x00036248
	public AnimationCurveLookupTable(AnimationCurve _curve, int _steps = 128)
	{
		this.m_table = new float[_steps];
		for (int i = 0; i < _steps; i++)
		{
			float num = (float)i / (float)(_steps - 1);
			this.m_table[i] = _curve.Evaluate(num);
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00037E90 File Offset: 0x00036290
	public float GetValue(float _input)
	{
		float num = _input * (float)(this.m_table.Length - 1);
		int num2 = Mathf.FloorToInt(num);
		if (num2 == this.m_table.Length - 1)
		{
			return this.m_table[num2];
		}
		float num3 = num - (float)num2;
		float num4 = this.m_table[num2];
		float num5 = this.m_table[num2 + 1];
		return num4 + (num5 - num4) * num3;
	}

	// Token: 0x04000513 RID: 1299
	private float[] m_table;
}
