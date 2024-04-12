using System;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class VisualsPowerLine : MonoBehaviour
{
	// Token: 0x06000DE5 RID: 3557 RVA: 0x00082894 File Offset: 0x00080C94
	private void Awake()
	{
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x00082896 File Offset: 0x00080C96
	private void Start()
	{
		this.UpdateLine();
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x000828A0 File Offset: 0x00080CA0
	public void UpdateLine()
	{
		float num = Vector3.Distance(this.startObject.transform.position, this.endObject.transform.position);
		if (num < 0f)
		{
			num = 0f;
		}
		float num2 = Mathf.Abs(Vector3.Dot(new Vector3(1f, 0f, 0f), Vector3.Normalize(this.startObject.transform.position - this.endObject.transform.position)));
		float num3 = Mathf.Min(num, this.lineJotain) * Mathf.Min(num, this.lineJotain) * this.lineLooseness * 0.001f * num2;
		int num4 = Mathf.Min(20, Mathf.RoundToInt(num * this.lineSmoothness));
		if (num4 <= 4)
		{
			num4 = 5;
		}
		this.lineRenderer.positionCount = num4;
		Vector3 vector = this.startObject.transform.position + this.m_coilStartOffset;
		Vector3 vector2 = vector - (this.endObject.transform.position + this.m_coilEndOffset);
		bool flag = false;
		if (this.startObject.transform.position.x > this.endObject.transform.position.x)
		{
			flag = true;
		}
		for (int i = 0; i < num4; i++)
		{
			int num5 = i;
			if (flag)
			{
				num5 = num4 - 1 - i;
			}
			float num6 = (float)num5 / ((float)num4 - 1f);
			Vector3 vector3 = vector2.normalized * num6 * num;
			Vector3 vector4 = vector - vector3;
			vector4..ctor(vector4.x, vector4.y - Mathf.Abs(Mathf.Sin(3.1415927f * num6 * ((float)this.midPoints + 1f))) * num3, vector4.z);
			this.lineRenderer.SetPosition(i, vector4);
		}
		this.startObject.transform.hasChanged = false;
		this.endObject.transform.hasChanged = false;
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x00082AC6 File Offset: 0x00080EC6
	public Vector3 GetCenterPosition()
	{
		return this.lineRenderer.GetPosition(this.lineRenderer.positionCount / 2);
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00082AE0 File Offset: 0x00080EE0
	public void ChangeColor(Color _color)
	{
		this.lineRenderer.material.SetColor("_Color", _color);
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00082AF8 File Offset: 0x00080EF8
	private void Update()
	{
		if (this.startObject.transform.hasChanged || this.endObject.transform.hasChanged)
		{
			this.UpdateLine();
		}
	}

	// Token: 0x040010D6 RID: 4310
	public GameObject startObject;

	// Token: 0x040010D7 RID: 4311
	public GameObject endObject;

	// Token: 0x040010D8 RID: 4312
	public LineRenderer lineRenderer;

	// Token: 0x040010D9 RID: 4313
	public float lineSmoothness = 1f;

	// Token: 0x040010DA RID: 4314
	public float lineLooseness = 10f;

	// Token: 0x040010DB RID: 4315
	public float lineJotain = 800f;

	// Token: 0x040010DC RID: 4316
	public int midPoints;

	// Token: 0x040010DD RID: 4317
	public const int MAX_POINTS = 20;

	// Token: 0x040010DE RID: 4318
	private Vector3 m_coilStartOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x040010DF RID: 4319
	private Vector3 m_coilEndOffset = new Vector3(0f, 0f, 0f);
}
