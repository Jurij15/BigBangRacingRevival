using System;
using UnityEngine;

// Token: 0x020004CF RID: 1231
public class SpriteC : BasicComponent
{
	// Token: 0x060022E2 RID: 8930 RVA: 0x001912EA File Offset: 0x0018F6EA
	public SpriteC()
		: base(ComponentType.Sprite)
	{
		this.vertDataIndex = -1;
		SpriteC.m_componentCount++;
	}

	// Token: 0x060022E3 RID: 8931 RVA: 0x00191308 File Offset: 0x0018F708
	public override void Reset()
	{
		base.Reset();
		this.visible = true;
		this.wasVisible = true;
		this.updatePosition = true;
		this.updateColors = true;
		this.updateRotation = true;
		this.updateScale = true;
		this.updateUVs = true;
		this.offset = Vector3.zero;
		this.offsetRight = Vector3.right;
		this.offsetUp = Vector3.up;
		this.align = Vector3.zero;
		this.relRight = Vector3.right;
		this.relUp = Vector3.up;
		this.scaledRelRight = this.relRight;
		this.scaledRelUp = this.relUp;
		this.sortValue = 0f;
		this.wScale = 1f;
		this.hScale = 1f;
		this.dimensionScale = 1f;
		this.wDimension = 0f;
		this.hDimension = 0f;
		this.color = SpriteS.m_defaultColor;
		this.clipLeft = 0f;
		this.clipRight = 0f;
		this.clipBottom = 0f;
		this.clipTop = 0f;
		this.clip = false;
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x00191428 File Offset: 0x0018F828
	~SpriteC()
	{
		SpriteC.m_componentCount--;
	}

	// Token: 0x04002934 RID: 10548
	public static int m_componentCount;

	// Token: 0x04002935 RID: 10549
	public bool visible;

	// Token: 0x04002936 RID: 10550
	public bool wasVisible;

	// Token: 0x04002937 RID: 10551
	public Vector3 align;

	// Token: 0x04002938 RID: 10552
	public Vector3 offset;

	// Token: 0x04002939 RID: 10553
	public Vector3 offsetRight;

	// Token: 0x0400293A RID: 10554
	public Vector3 offsetUp;

	// Token: 0x0400293B RID: 10555
	public float width;

	// Token: 0x0400293C RID: 10556
	public float height;

	// Token: 0x0400293D RID: 10557
	public Vector3 scaledRelRight;

	// Token: 0x0400293E RID: 10558
	public Vector3 scaledRelUp;

	// Token: 0x0400293F RID: 10559
	public Vector3 scaledRelOffset;

	// Token: 0x04002940 RID: 10560
	public Vector3 relRight;

	// Token: 0x04002941 RID: 10561
	public Vector3 relUp;

	// Token: 0x04002942 RID: 10562
	public Vector3 relOffset;

	// Token: 0x04002943 RID: 10563
	public float wScale;

	// Token: 0x04002944 RID: 10564
	public float hScale;

	// Token: 0x04002945 RID: 10565
	public float dimensionScale;

	// Token: 0x04002946 RID: 10566
	public float wDimension;

	// Token: 0x04002947 RID: 10567
	public float hDimension;

	// Token: 0x04002948 RID: 10568
	public TransformC p_TC;

	// Token: 0x04002949 RID: 10569
	public SpriteSheet p_spriteSheet;

	// Token: 0x0400294A RID: 10570
	public Color color;

	// Token: 0x0400294B RID: 10571
	public Frame frame;

	// Token: 0x0400294C RID: 10572
	public int meshIndex;

	// Token: 0x0400294D RID: 10573
	public int vertDataIndex;

	// Token: 0x0400294E RID: 10574
	public float sortValue;

	// Token: 0x0400294F RID: 10575
	public float clipLeft;

	// Token: 0x04002950 RID: 10576
	public float clipRight;

	// Token: 0x04002951 RID: 10577
	public float clipBottom;

	// Token: 0x04002952 RID: 10578
	public float clipTop;

	// Token: 0x04002953 RID: 10579
	public bool clip;

	// Token: 0x04002954 RID: 10580
	public bool updatePosition;

	// Token: 0x04002955 RID: 10581
	public bool updateUVs;

	// Token: 0x04002956 RID: 10582
	public bool updateColors;

	// Token: 0x04002957 RID: 10583
	public bool updateRotation;

	// Token: 0x04002958 RID: 10584
	public bool updateScale;
}
