using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class EffectSmokeTrail : MonoBehaviour
{
	// Token: 0x06000CED RID: 3309 RVA: 0x0007C830 File Offset: 0x0007AC30
	private void Start()
	{
		this.tr = base.transform;
		this.line = base.GetComponent<LineRenderer>();
		this.lineMaterial = this.line.material;
		this.lineSegment = 1f / (float)this.numberOfPoints;
		this.positions = new Vector3[this.numberOfPoints];
		this.directions = new Vector3[this.numberOfPoints];
		this.line.SetVertexCount(this.currentNumberOfPoints);
		this.i = 0;
		while (this.i < this.currentNumberOfPoints)
		{
			this.tempVec = this.getSmokeVec();
			this.directions[this.i] = this.tempVec;
			this.positions[this.i] = this.tr.position;
			this.line.SetPosition(this.i, this.positions[this.i]);
			this.i++;
		}
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0007C948 File Offset: 0x0007AD48
	private void Update()
	{
		this.timeSinceUpdate += Time.deltaTime;
		if (this.timeSinceUpdate > this.updateSpeed)
		{
			this.timeSinceUpdate -= this.updateSpeed;
			if (!this.allPointsAdded)
			{
				this.currentNumberOfPoints++;
				this.line.SetVertexCount(this.currentNumberOfPoints);
				this.tempVec = this.getSmokeVec();
				this.directions[0] = this.tempVec;
				this.positions[0] = this.tr.position;
				this.line.SetPosition(0, this.positions[0]);
			}
			if (!this.allPointsAdded && this.currentNumberOfPoints == this.numberOfPoints)
			{
				this.allPointsAdded = true;
			}
			this.i = this.currentNumberOfPoints - 1;
			while (this.i > 0)
			{
				this.tempVec = this.positions[this.i - 1];
				this.positions[this.i] = this.tempVec;
				this.tempVec = this.directions[this.i - 1];
				this.directions[this.i] = this.tempVec;
				this.i--;
			}
			this.tempVec = this.getSmokeVec();
			this.directions[0] = this.tempVec;
		}
		this.i = 1;
		while (this.i < this.currentNumberOfPoints)
		{
			this.tempVec = this.positions[this.i];
			this.tempVec += this.directions[this.i] * Time.deltaTime;
			this.positions[this.i] = this.tempVec;
			this.line.SetPosition(this.i, this.positions[this.i]);
			this.i++;
		}
		this.positions[0] = this.tr.position;
		this.line.SetPosition(0, this.tr.position);
		if (this.allPointsAdded)
		{
			Vector2 vector;
			vector..ctor(this.lineSegment * (this.timeSinceUpdate / this.updateSpeed), this.lineMaterial.mainTextureOffset.y);
			this.lineMaterial.mainTextureOffset = vector;
		}
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0007CC2C File Offset: 0x0007B02C
	private Vector3 getSmokeVec()
	{
		Vector3 vector;
		vector.x = Random.Range(-1f, 1f);
		vector.y = Random.Range(0f, 1f);
		vector.z = Random.Range(-1f, 1f);
		vector.Normalize();
		vector *= this.spread;
		vector.y += this.riseSpeed;
		return vector;
	}

	// Token: 0x04000E2A RID: 3626
	public LineRenderer line;

	// Token: 0x04000E2B RID: 3627
	private Transform tr;

	// Token: 0x04000E2C RID: 3628
	private Vector3[] positions;

	// Token: 0x04000E2D RID: 3629
	private Vector3[] directions;

	// Token: 0x04000E2E RID: 3630
	private int i;

	// Token: 0x04000E2F RID: 3631
	private float timeSinceUpdate;

	// Token: 0x04000E30 RID: 3632
	private Material lineMaterial;

	// Token: 0x04000E31 RID: 3633
	private float lineSegment;

	// Token: 0x04000E32 RID: 3634
	private int currentNumberOfPoints = 2;

	// Token: 0x04000E33 RID: 3635
	private bool allPointsAdded;

	// Token: 0x04000E34 RID: 3636
	public int numberOfPoints = 10;

	// Token: 0x04000E35 RID: 3637
	public float updateSpeed = 0.1f;

	// Token: 0x04000E36 RID: 3638
	public float riseSpeed = 150f;

	// Token: 0x04000E37 RID: 3639
	public float spread = 50f;

	// Token: 0x04000E38 RID: 3640
	private Vector3 tempVec;
}
